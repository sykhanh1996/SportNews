using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SportNews.Domain.SeedWork;
using SportNews.Shared.Extensions;
using SportNews.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SportNews.Infrastructure.MongoDb.SeedWork
{
    public class BaseRepository<T> : IRepositoryBase<T> where T : Entity, IAggregateRoot
    {
        private readonly IMongoClient _mongoClient;
        private readonly string _collection;
        private readonly SportNewsSettings _settings;
        private IHttpContextAccessor _httpContextAccessor;

        public BaseRepository(IMongoClient mongoClient,
         IOptions<SportNewsSettings> settings,
         string collection,
         IHttpContextAccessor httpContextAccessor)
        {
            _settings = settings.Value;
            (_mongoClient, _collection) = (mongoClient, collection);

            if (!_mongoClient.GetDatabase(_settings.DatabaseSettings.DatabaseName).ListCollectionNames().ToList().Contains(collection))
                _mongoClient.GetDatabase(_settings.DatabaseSettings.DatabaseName).CreateCollection(collection);

            _httpContextAccessor = httpContextAccessor;
        }

        protected virtual IMongoCollection<T> Collection =>
                   _mongoClient.GetDatabase(_settings.DatabaseSettings.DatabaseName).GetCollection<T>(_collection);



        public async Task DeleteAsync(string id)
        {
            await Collection.DeleteOneAsync(f => f.Id == id);
        }

        public async Task InsertAsync(T obj)
        {
            var user = _httpContextAccessor.HttpContext?.User?.GetClaim<string>(ClaimTypes.NameIdentifier);

            if (obj is IDateTracking changedItem)
            {
                changedItem.CreatedDate = DateTimeOffset.Now;
                changedItem.CreatedBy = user;
            }
            await Collection.InsertOneAsync(obj);
        }

        public async Task UpdateAsync(T obj)
        {
            var user = _httpContextAccessor.HttpContext?.User?.GetClaim<string>(ClaimTypes.NameIdentifier);

            if (obj is IDateTracking changedItem)
            {
                changedItem.LastModifiedDate = DateTimeOffset.Now;
                changedItem.ModifiedBy = user;
            }
            Expression<Func<T, string>> func = f => f.Id;
            var value = (string)obj.GetType().GetProperty(func.Body.ToString().Split(".")[1])?.GetValue(obj, null);
            var filter = Builders<T>.Filter.Eq(func, value);

            await Collection.ReplaceOneAsync(filter, obj);
        }
    }
}
