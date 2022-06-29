using MediatR;
using SportNews.Shared.Categories;
using SportNews.Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportNews.Application.Queries.V1.Categories.GetCategoriesPaging
{
    public class GetCategoriesPagingQuery : IRequest<ApiResult<PagedList<CategoryDto>>>
    {
        public string SearchKeyword { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
