﻿using MongoDB.Bson.Serialization.Attributes;
using SportNews.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportNews.Domain.AggregateModels.CategoryAggregate
{
    [BsonIgnoreExtraElements]
    public class Category : Entity, IAggregateRoot
    {
        public Category(string id, string name, string urlPath) => (Id, Name, UrlPath) = (id, name, urlPath);

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("urlPath")]
        public string UrlPath { set; get; } //domain/exam-category-1/
    }
}
