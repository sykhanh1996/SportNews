using MediatR;
using SportNews.Shared.Categories;
using SportNews.Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportNews.Application.Queries.V1.Categories.GetCategoryById
{
    public class GetCategoryByIdQuery : IRequest<ApiResult<CategoryDto>>
    {
        public GetCategoryByIdQuery(string id)
        {
            Id = id;
        }
        public string Id { set; get; }
    }
}
