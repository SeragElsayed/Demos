using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDBDemo.DataBaseContext
{
    public class DatabaseSettings
    {
        public string SectionName { get; set; } = "DatabaseSettings";
        public string ConnectionString { get; set; }
        public string DBName { get; set; }
    }
    public interface IDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DBName { get; set; }
    }
}
