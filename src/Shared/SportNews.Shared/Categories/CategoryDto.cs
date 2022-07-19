using SportNews.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportNews.Shared.Categories
{
    public class CategoryDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ParentId { get; set; }

        public Status Status { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string? CreatedBy { get; set; }

    }
}
