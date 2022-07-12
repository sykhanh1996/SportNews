﻿using MediatR;
using SportNews.Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportNews.Application.Commands.V1.Categories.UpdateCategory
{
    public class UpdateCategoryCommand : IRequest<ApiResult<bool>>
    {
        public string Id { get; set; }
        public string Name { set; get; }
        public string UrlPath { get; set; }
    }
}
