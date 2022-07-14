﻿using MongoDB.Bson.Serialization.Attributes;
using SportNews.Domain.SeedWork;
using SportNews.Shared.Enums;
using SportNews.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportNews.Domain.AggregateModels.CategoryAggregate
{
    [BsonIgnoreExtraElements]
    public class Category : Entity, IAggregateRoot, ISwitchable
    {
        public Category(string id, string name, Category? parentId, Status status) => (Id, Name, ParentId, Status) = (id, name, parentId, status);

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("parentId")]
        public Category? ParentId { set; get; }

        [BsonElement("status")]
        public Status Status { get; set; }


    }
}
