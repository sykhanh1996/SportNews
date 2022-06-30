using SportNews.Domain.AggregateModels.CategoryAggregate;
using SportNews.Domain.AggregateModels.UserAggregate;
using SportNews.Infrastructure.MongoDb.Repositories;

namespace SportNews.API
{
    public static class ServicesRegister
    {
        public static void RegisterCustomServices(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
        }
    }
}
