using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SportNews.Domain.AggregateModels.UserAggregate;
using SportNews.Infrastructure.MongoDb.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportNews.Infrastructure.MongoDb.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(
            IMongoClient mongoClient,
        IOptions<SportNewsSettings> settings, IMediator mediator, IHttpContextAccessor httpContextAccessor)
        : base(mongoClient, settings, Constants.Collections.User, httpContextAccessor)
        {
        }

        public async Task<User> GetUserByIdAsync(string externalId)
        {
            var filter = Builders<User>.Filter.Eq(s => s.ExternalId, externalId);
            return await Collection.Find(filter).FirstOrDefaultAsync();
        }
    }
}
