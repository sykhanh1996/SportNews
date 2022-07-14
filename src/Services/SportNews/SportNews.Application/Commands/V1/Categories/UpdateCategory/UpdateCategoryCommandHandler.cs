﻿using MediatR;
using Microsoft.Extensions.Logging;
using SportNews.Domain.AggregateModels.CategoryAggregate;
using SportNews.Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportNews.Application.Commands.V1.Categories.UpdateCategory
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, ApiResult<bool>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<UpdateCategoryCommandHandler> _logger;

        public UpdateCategoryCommandHandler(
                ICategoryRepository categoryRepository,
                ILogger<UpdateCategoryCommandHandler> logger
            )
        {
            _categoryRepository = categoryRepository;
            _logger = logger;

        }

        public async Task<ApiResult<bool>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var itemToUpdate = await _categoryRepository.GetCategoriesByIdAsync(request.Id);
            if (itemToUpdate == null)
            {
                _logger.LogError($"Item is not found {request.Id}");
                return new ApiErrorResult<bool>(400, $"Item is not found {request.Id}");
            }

            itemToUpdate.Name = request.Name;
            await _categoryRepository.UpdateAsync(itemToUpdate);
            return new ApiSuccessResult<bool>(200, true, "Update successful");
        }
    }
}
