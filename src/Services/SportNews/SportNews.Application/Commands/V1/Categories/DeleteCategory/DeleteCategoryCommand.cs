using MediatR;
using SportNews.Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportNews.Application.Commands.V1.Categories.DeleteCategory
{
    public class DeleteCategoryCommand : IRequest<ApiResult<bool>>
    {
        public DeleteCategoryCommand(string id)
        {
            Id = id;
        }
        public string Id { get; set; }
    }
}
