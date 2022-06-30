using MediatR;
using Microsoft.AspNetCore.Mvc;
using SportNews.Application.Queries.V1.Categories.GetCategoriesPaging;
using SportNews.Shared.Categories;
using SportNews.Shared.SeedWork;
using System.Net;

namespace SportNews.API.Controllers.V1
{
    public class CategoriesController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CategoriesController> _logger;
        public CategoriesController(IMediator mediator, ILogger<CategoriesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("paging")]
        [ProducesResponseType(typeof(ApiSuccessResult<PagedList<CategoryDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCategoriesPagingAsync([FromQuery] GetCategoriesPagingQuery query)
        {
            _logger.LogInformation("BEGIN: GetCategoriesPagingAsync");

            var result = await _mediator.Send(query);
            var test = string.Empty;
            _logger.LogInformation("END: GetCategoriesPagingAsync");

            return Ok(result);
        }
    }
}
