﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SportNews.Domain.AggregateModels.CategoryAggregate;
using SportNews.Infrastructure.MongoDb.SeedWork;
using SportNews.Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportNews.Infrastructure.MongoDb.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(
          IMongoClient mongoClient,
          IOptions<SportNewsSettings> settings,
          IMediator mediator, IHttpContextAccessor httpContextAccessor)
      : base(mongoClient, settings, Constants.Collections.Category, httpContextAccessor)
        {
        }

        public async Task<Category> GetCategoriesByIdAsync(string id)
        {
            FilterDefinition<Category> filter = Builders<Category>.Filter.Eq(s => s.Id, id);
            return await Collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<Category> GetCategoriesByNameAsync(string name)
        {
            FilterDefinition<Category> filter = Builders<Category>.Filter.Eq(s => s.Name, name);
            return await Collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<PagedList<Category>> GetCategoriesPagingAsync(string searchKeyword, int pageIndex, int pageSize)
        {
            FilterDefinition<Category> filter = Builders<Category>.Filter.Empty;
            if (!string.IsNullOrEmpty(searchKeyword))
                filter = Builders<Category>.Filter.Where(s => s.Name.Contains(searchKeyword.ToLower()));

            var totalRow = await Collection.Find(filter).CountDocumentsAsync();
            var items = await Collection.Find(filter)
                .Skip((pageIndex - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();

            return new PagedList<Category>(items, totalRow, pageIndex, pageSize);
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            var items = await Collection.AsQueryable()
                .ToListAsync();

            return items;
        }
    }
}
