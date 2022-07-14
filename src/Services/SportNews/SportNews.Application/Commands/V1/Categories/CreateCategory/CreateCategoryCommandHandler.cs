using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using SportNews.Domain.AggregateModels.CategoryAggregate;
using SportNews.Shared.Categories;
using SportNews.Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportNews.Application.Commands.V1.Categories.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, ApiResult<CategoryDto>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCategoryCommandHandler> _logger;

        public CreateCategoryCommandHandler(
            ICategoryRepository categoryRepository,
            ILogger<CreateCategoryCommandHandler> logger,
            IMapper mapper
        )
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
            _mapper = mapper;

        }

        public async Task<ApiResult<CategoryDto>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var itemToAdd = await _categoryRepository.GetCategoriesByNameAsync(request.Name);
            if (itemToAdd != null)
            {
                _logger.LogError($"Item name existed: {request.Name}");
                return null;
            }
            Category parentCategory = null;
            if (request.ParentCategory != null)
            {
                parentCategory = await _categoryRepository.GetCategoriesByIdAsync(request.ParentCategory.Id);
            }
            itemToAdd = new Category(ObjectId.GenerateNewId().ToString(), request.Name, parentCategory, request.Status);
            await _categoryRepository.InsertAsync(itemToAdd);
            var result = _mapper.Map<Category, CategoryDto>(itemToAdd);
            return new ApiSuccessResult<CategoryDto>(200, result);
        }
    }
}
