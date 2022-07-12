using HealthChecks.UI.Client;
using MediatR;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using SportNews.API.Extensions;
using SportNews.API.Filters;
using SportNews.Application.Mapping;
using SportNews.Application.Queries.V1.Categories.GetCategoriesPaging;
using SportNews.Infrastructure.MongoDb.SeedWork;
using System.Net.Mime;
using System.Text.Json;

namespace SportNews.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var user = Configuration.GetValue<string>("DatabaseSettings:User");
            var password = Configuration.GetValue<string>("DatabaseSettings:Password");
            var server = Configuration.GetValue<string>("DatabaseSettings:Server");
            var databaseName = Configuration.GetValue<string>("DatabaseSettings:DatabaseName");
            var mongodbConnectionString = "mongodb://" + user + ":" + password + "@" + server + "/" + databaseName + "?authSource=admin";
            services.AddHttpContextAccessor();

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
            });
            services.AddVersionedApiExplorer(
                        options =>
                        {
                            // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                            // note: the specified format code will format the version as "'v'major[.minor][-status]"
                            options.GroupNameFormat = "'v'VVV";

                            // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                            // can also be used to control the format of the API version in route templates
                            options.SubstituteApiVersionInUrl = true;
                        });

            services.AddSingleton<IMongoClient>(c =>
            {
                return new MongoClient(mongodbConnectionString);
            });
            services.AddScoped(c => c.GetService<IMongoClient>()?.StartSession());
            services.AddAutoMapper(cfg => { cfg.AddProfile(new MappingProfile()); });
            services.AddMediatR(typeof(GetCategoriesPagingQuery).Assembly);
            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .SetIsOriginAllowed((host) => true)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SportNews.API", Version = "v1" });

                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows()
                    {
                        Implicit = new OpenApiOAuthFlow()
                        {
                            AuthorizationUrl = new Uri($"{Configuration.GetValue<string>("IdentityUrl")}/connect/authorize"),
                            TokenUrl = new Uri($"{Configuration.GetValue<string>("IdentityUrl")}/connect/token"),
                            Scopes = new Dictionary<string, string>()
                            {
                                {"full_access", "Full Access"},
                            }
                        }
                    }
                });
                c.OperationFilter<AuthorizeCheckOperationFilter>();
            });

            var identityUrl = Configuration.GetValue<string>("IdentityUrl");
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults
                    .AuthenticationScheme;
                options.DefaultChallengeScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults
                    .AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = identityUrl;
                options.RequireHttpsMetadata = false;
                options.Audience = "sportnews_api";
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
                //Fix SSL
                options.BackchannelHttpHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = delegate { return true; }
                };
            });


            services.Configure<SportNewsSettings>(Configuration);

            //Health check
            services.AddHealthChecks()
                    .AddCheck("self", () => HealthCheckResult.Healthy())
                    .AddMongoDb(mongodbConnectionString: mongodbConnectionString,
                                name: "mongo",
                                failureStatus: HealthStatus.Unhealthy);

            services.AddHealthChecksUI(opt =>
            {
                opt.SetEvaluationTimeInSeconds(60*60); //time in seconds between check
                opt.MaximumHistoryEntriesPerEndpoint(60*60*2); //maximum history of checks
                opt.SetApiMaxActiveRequests(1); //api requests concurrency

                opt.AddHealthCheckEndpoint("SportNews API", "/hc"); //map health check api
            })
                    .AddInMemoryStorage();

            services.RegisterCustomServices();
        }

        //This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.OAuthClientId("sportnews_api_swaggerui");
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SportNews.API v1");
                });
            }
            // migrate any database changes on startup (includes initial db creation)

            app.UseErrorWrapping();

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();
            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapHealthChecksUI(options => options.UIPath = "/hc-ui");
                endpoints.MapHealthChecks("/liveness", new HealthCheckOptions
                {
                    Predicate = r => r.Name.Contains("self")
                });
                endpoints.MapHealthChecks("/hc-details",
                            new HealthCheckOptions
                            {
                                ResponseWriter = async (context, report) =>
                                {
                                    var result = JsonSerializer.Serialize(
                                        new
                                        {
                                            status = report.Status.ToString(),
                                            monitors = report.Entries.Select(e => new { key = e.Key, value = Enum.GetName(typeof(HealthStatus), e.Value.Status) })
                                        });
                                    context.Response.ContentType = MediaTypeNames.Application.Json;
                                    await context.Response.WriteAsync(result);
                                }
                            }
                        );
                endpoints.MapControllers();
            });
        }
    }
}
