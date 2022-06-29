using MediatR;
using SportNews.Shared.Categories;
using SportNews.Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportNews.Application.Queries.V1.Categories.GetAllCategories
{
    public class GetAllCategoriesQuery : IRequest<ApiResult<List<CategoryDto>>>
    {
    }
}
