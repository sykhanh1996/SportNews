using MediatR;
using SportNews.Shared.Categories;
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
        public string UrlPath { get; set; }
    }
}
