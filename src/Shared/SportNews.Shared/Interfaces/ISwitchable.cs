using SportNews.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportNews.Shared.Interfaces
{
    public interface ISwitchable
    {
        Status Status { set; get; }
    }
}
