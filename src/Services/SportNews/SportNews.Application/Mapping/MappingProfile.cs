using AutoMapper;
using SportNews.Domain.AggregateModels.CategoryAggregate;
using SportNews.Shared.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportNews.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
        }
    }
}
