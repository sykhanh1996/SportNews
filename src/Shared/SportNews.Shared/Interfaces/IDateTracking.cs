using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportNews.Shared.Interfaces
{
    public interface IDateTracking
    {
        string? ModifiedBy { get; set; }
        string? CreatedBy { get; set; }
        DateTimeOffset CreatedDate { get; set; }
        DateTimeOffset? LastModifiedDate { get; set; }
    }
}
