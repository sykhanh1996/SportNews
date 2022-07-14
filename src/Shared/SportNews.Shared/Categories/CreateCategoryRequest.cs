using SportNews.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportNews.Shared.Categories
{
    public class CreateCategoryRequest
    {
        [Required]
        public string Name { get; set; }

        public CategoryDto? ParentId { get; set; }
        public Status Status { get; set; }
    }
}
