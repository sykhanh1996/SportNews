using MongoDB.Bson.Serialization.Attributes;
using SportNews.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportNews.Domain.AggregateModels.UserAggregate
{
    public class User : Entity, IAggregateRoot
    {
        public User(string externalId, string firstName, string lastName)
        {
            (ExternalId, FirstName, LastName) = (externalId, firstName, lastName);
        }

        [BsonElement("externalId")]
        public string ExternalId { get; set; }

        [BsonElement("firstName")]
        public string FirstName { get; set; }

        [BsonElement("lastName")]

        public string LastName { get; set; }

        public static User CreateNewUser(string externalId, string firstName, string lastName)
        {
            return new User(externalId, firstName, lastName);
        }
    }
}
