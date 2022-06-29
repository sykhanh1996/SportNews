﻿namespace SportNews.API
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

            ////1. Setup entity framework
            //services.AddDbContextPool<ApplicationDbContext>(options =>
            //    options.UseSqlServer(
            //        Configuration.GetConnectionString("DefaultConnection")));
            ////2. Setup idetntity
            //services.AddIdentity<AppUser, IdentityRole>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>()
            //    .AddDefaultTokenProviders();
            //services.AddDbContext<ApplicationDbContext>(x => x.UseSqlite("Data Source=LocalDatabase.db"));

            //services.AddApiVersioning(options =>
            //{
            //    options.ReportApiVersions = true;
            //});
            //services.AddVersionedApiExplorer(
            //            options =>
            //            {
            //                // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
            //                // note: the specified format code will format the version as "'v'major[.minor][-status]"
            //                options.GroupNameFormat = "'v'VVV";

            //                // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
            //                // can also be used to control the format of the API version in route templates
            //                options.SubstituteApiVersionInUrl = true;
            //            });

            //services.AddSingleton<IMongoClient>(c =>
            //{
            //    return new MongoClient(mongodbConnectionString);
            //});
            //services.AddScoped(c => c.GetService<IMongoClient>()?.StartSession());
            //services.AddAutoMapper(cfg => { cfg.AddProfile(new MappingProfile()); });
            //services.AddMediatR(typeof(GetCategoriesPagingQuery).Assembly);
            //services.AddControllers();
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("CorsPolicy",
            //        builder => builder
            //            .SetIsOriginAllowed((host) => true)
            //            .AllowAnyMethod()
            //            .AllowAnyHeader()
            //            .AllowCredentials());
            //});

            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "News.API", Version = "v1" });

            //    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            //    {
            //        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n
            //          Enter 'Bearer' [space] and then your token in the text input below.
            //          \r\n\r\nExample: 'Bearer 12345abcdef'",
            //        Name = "Authorization",
            //        In = ParameterLocation.Header,
            //        Type = SecuritySchemeType.ApiKey,
            //        Scheme = "Bearer"
            //    });
            //    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            //    {
            //        {
            //            new OpenApiSecurityScheme
            //            {
            //                Reference = new OpenApiReference
            //                {
            //                    Type = ReferenceType.SecurityScheme,
            //                    Id = "Bearer"
            //                },
            //                Scheme = "oauth2",
            //                Name = "Bearer",
            //                In = ParameterLocation.Header,
            //            },
            //            new List<string>()
            //        }
            //    });
            //    //c.OperationFilter<AuthorizeCheckOperationFilter>();
            //});

            string issuer = Configuration.GetValue<string>("Tokens:Issuer");
            string signingKey = Configuration.GetValue<string>("Tokens:Key");
            byte[] signingKeyBytes = System.Text.Encoding.UTF8.GetBytes(signingKey);

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults
            //        .AuthenticationScheme;
            //    options.DefaultChallengeScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults
            //        .AuthenticationScheme;
            //}).AddJwtBearer(options =>
            //{
            //    options.RequireHttpsMetadata = false;
            //    options.SaveToken = true;
            //    options.TokenValidationParameters = new TokenValidationParameters()
            //    {
            //        ValidateIssuer = true,
            //        ValidIssuer = issuer,
            //        ValidateAudience = true,
            //        ValidAudience = issuer,
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = true,
            //        ClockSkew = System.TimeSpan.Zero,
            //        IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes)
            //    };
            //});


            //services.Configure<NewsSettings>(Configuration);

            ////Health check
            //services.AddHealthChecks()
            //        .AddCheck("self", () => HealthCheckResult.Healthy())
            //        .AddMongoDb(mongodbConnectionString: mongodbConnectionString,
            //                    name: "mongo",
            //                    failureStatus: HealthStatus.Unhealthy);

            //services.AddHealthChecksUI(opt =>
            //{
            //    opt.SetEvaluationTimeInSeconds(15); //time in seconds between check
            //    opt.MaximumHistoryEntriesPerEndpoint(60); //maximum history of checks
            //    opt.SetApiMaxActiveRequests(1); //api requests concurrency

            //    opt.AddHealthCheckEndpoint("News API", "/hc"); //map health check api
            //})
            //        .AddInMemoryStorage();

            //services.RegisterCustomServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext dataContext)
        //{
        //    if (env.IsDevelopment())
        //    {
        //        app.UseDeveloperExceptionPage();
        //        app.UseSwagger();
        //        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SportNews.API v1"));
        //    }
        //    // migrate any database changes on startup (includes initial db creation)

        //    app.UseHttpsRedirection();

        //    app.UseRouting();
        //    app.UseCors("CorsPolicy");

        //    app.UseAuthorization();
        //    dataContext.Database.Migrate();

        //    app.UseEndpoints(endpoints =>
        //    {
        //        //endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
        //        //{
        //        //    Predicate = _ => true,
        //        //    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        //        //});
        //        //endpoints.MapHealthChecksUI(options => options.UIPath = "/hc-ui");
        //        //endpoints.MapHealthChecks("/liveness", new HealthCheckOptions
        //        //{
        //        //    Predicate = r => r.Name.Contains("self")
        //        //});
        //        //endpoints.MapHealthChecks("/hc-details",
        //        //            new HealthCheckOptions
        //        //            {
        //        //                ResponseWriter = async (context, report) =>
        //        //                {
        //        //                    var result = JsonSerializer.Serialize(
        //        //                        new
        //        //                        {
        //        //                            status = report.Status.ToString(),
        //        //                            monitors = report.Entries.Select(e => new { key = e.Key, value = Enum.GetName(typeof(HealthStatus), e.Value.Status) })
        //        //                        });
        //        //                    context.Response.ContentType = MediaTypeNames.Application.Json;
        //        //                    await context.Response.WriteAsync(result);
        //        //                }
        //        //            }
        //        //        );
        //        endpoints.MapControllers();
        //    });


        //}
    }
}
