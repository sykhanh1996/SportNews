using MongoDB.Bson.Serialization.Attributes;
using SportNews.Domain.AggregateModels.CategoryAggregate;
using SportNews.Domain.SeedWork;
using SportNews.Shared.Enums;
using SportNews.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportNews.Domain.AggregateModels.PostAggregate
{
    [BsonIgnoreExtraElements]
    public class Post : Entity, IAggregateRoot, ISwitchable, IDateTracking, IHasSeoMetaData
    {
        public Post(string id,string title, string description, string content, string thumbnail, string image, List<Category> categories, DateTime? publishedDate, string source, Status status, string? modifiedBy, string? createdBy, DateTime createdDate, DateTime? lastModifiedDate, string seoPageTitle, string seoAlias, string seoKeywords, string seoDescription)
        {
            Id = id;
            Title = title;
            Description = description;
            Content = content;
            Thumbnail = thumbnail;
            Image = image;
            this.Categories = categories;
            PublishedDate = publishedDate;
            Source = source;
            Status = status;
            ModifiedBy = modifiedBy;
            CreatedBy = createdBy;
            CreatedDate = createdDate;
            LastModifiedDate = lastModifiedDate;
            SeoPageTitle = seoPageTitle;
            SeoAlias = seoAlias;
            SeoKeywords = seoKeywords;
            SeoDescription = seoDescription;
        }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("content")]
        public string Content { get; set; }

        [BsonElement("thumbnail")]
        public string Thumbnail { get; set; }

        [BsonElement("image")]
        public string Image { get; set; }

        [BsonElement("categories")]
        public List<Category> Categories { get; set; }

        [BsonElement("publishedDate")]
        public DateTime? PublishedDate { get; set; }

        [BsonElement("source")]
        public string Source { get; set; }

        [BsonElement("status")]
        public Status Status { get; set; }

        [BsonElement("modifiedBy")]
        public string? ModifiedBy { get; set; }

        [BsonElement("createdBy")]
        public string? CreatedBy { get; set; }

        [BsonElement("createdDate")]
        public DateTime CreatedDate { get; set; }

        [BsonElement("lastModifiedDate")]
        public DateTime? LastModifiedDate { get; set; }

        [BsonElement("seoPageTitle")]
        public string SeoPageTitle { get; set; }

        [BsonElement("seoAlias")]
        public string SeoAlias { get; set; }

        [BsonElement("seoKeywords")]
        public string SeoKeywords { get; set; }

        [BsonElement("seoDescription")]
        public string SeoDescription { get; set; }
    }
}
