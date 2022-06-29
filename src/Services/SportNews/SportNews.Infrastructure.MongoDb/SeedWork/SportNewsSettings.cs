using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportNews.Infrastructure.MongoDb.SeedWork
{
    public class SportNewsSettings
    {
        public string IdentityUrl { get; set; }

        public DatabaseSettings DatabaseSettings { get; set; }
    }
    public class DatabaseSettings
    {
        public string ConnectionString { set; get; }
        public string DatabaseName { get; set; }
    }
}
