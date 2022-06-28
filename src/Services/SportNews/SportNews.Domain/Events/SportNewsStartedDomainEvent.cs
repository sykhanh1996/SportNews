using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportNews.Domain.Events
{
    public class SportNewsStartedDomainEvent : INotification
    {
        public SportNewsStartedDomainEvent(string userId, string firstName, string lastName)
           => (UserId, FirstName, LastName) = (userId, firstName, lastName);
        public string UserId { set; get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
