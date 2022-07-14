using MediatR;
using SportNews.Shared.Categories;
using SportNews.Shared.Enums;
using SportNews.Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportNews.Application.Commands.V1.Categories.UpdateCategory
{
    public class UpdateCategoryCommand : IRequest<ApiResult<bool>>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public CategoryDto? ParentCategory { set; get; }
        public Status Status { set; get; }
    }
}
