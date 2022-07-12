using SportNews.Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportNews.Shared.Categories
{
    public class CategorySearch : PagingParameters
    {
        public string Name { get; set; }
    }
}
