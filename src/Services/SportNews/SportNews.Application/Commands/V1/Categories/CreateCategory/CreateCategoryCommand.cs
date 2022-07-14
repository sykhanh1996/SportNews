using MediatR;
using SportNews.Domain.AggregateModels.CategoryAggregate;
using SportNews.Shared.Categories;
using SportNews.Shared.Enums;
using SportNews.Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportNews.Application.Commands.V1.Categories.CreateCategory
{
    public class CreateCategoryCommand : IRequest<ApiResult<CategoryDto>>
    {
        public string Name { set; get; }

        public CategoryDto? ParentCategory { set; get; }

        public Status Status { set; get; }
    }
}
