using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Polly;
using Polly.Retry;
using SportNews.Domain.AggregateModels.CategoryAggregate;
using SportNews.Infrastructure.MongoDb.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportNews.Infrastructure.MongoDb
{
    public class SportNewsMongoDbSeeding
    {
        public async Task SeedAsync(IMongoClient mongoClient, IOptions<SportNewsSettings> settings,
              ILogger<SportNewsMongoDbSeeding> logger)
        {
            var policy = CreatePolicy(logger, nameof(SportNewsMongoDbSeeding));
            await policy.ExecuteAsync(async () =>
            {
                var databaseName = settings.Value.DatabaseSettings.DatabaseName;
                var database = mongoClient.GetDatabase(databaseName);
                var categoryId1 = ObjectId.GenerateNewId().ToString();
                var categoryId2 = ObjectId.GenerateNewId().ToString();
                var categoryId3 = ObjectId.GenerateNewId().ToString();
                var categoryId4 = ObjectId.GenerateNewId().ToString();
                if (await database.GetCollection<Category>(Constants.Collections.Category).EstimatedDocumentCountAsync() == 0)
                {
                    await database.GetCollection<Category>(Constants.Collections.Category)
                        .InsertManyAsync(new List<Category>()
                        {
                            new Category(categoryId1,"Category 1","category-1"),
                            new Category(categoryId2,"Category 2","category-1"),
                            new Category(categoryId3,"Category 3","category-3"),
                            new Category(categoryId4,"Category 4","category-4"),
                        });
                }

            });
        }

        private AsyncRetryPolicy CreatePolicy(ILogger<SportNewsMongoDbSeeding> logger, string prefix, int retries = 3)
        {
            return Policy.Handle<MongoException>().
                WaitAndRetryAsync(
                    retryCount: retries,
                    sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                    onRetry: (exception, timeSpan, retry, ctx) =>
                    {
                        logger.LogWarning(exception, "[{prefix}] Exception {ExceptionType} with message {Message} detected on attempt {retry} of {retries}", prefix, exception.GetType().Name, exception.Message, retry, retries);
                    }
                );
        }
    }
}
