using MediatR;
using SportNews.Domain.AggregateModels.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportNews.Application.DomainEventHandlers.V1
{
    //public class SynchronizeUserWhenExamStartedDomainEventHandler : INotificationHandler<ExamStartedDomainEvent>
    public class SynchronizeUserWhenExamStartedDomainEventHandler
    {
        private readonly IUserRepository _userRepository;
        public SynchronizeUserWhenExamStartedDomainEventHandler(
            IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        //public async Task Handle(ExamStartedDomainEvent notification, CancellationToken cancellationToken)
        //{
        //    var user = await _userRepository.GetUserByIdAsync(notification.UserId);
        //    if (user == null)
        //    {
        //        user = User.CreateNewUser(notification.UserId, notification.FirstName, notification.LastName);
        //        await _userRepository.InsertAsync(user);
        //    }
        //}
    }
}
