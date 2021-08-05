using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDBDemo.DataBaseContext
{
    public interface IDBContext
    {
         MongoDatabaseBase DBCTX { get;  }
    }
}
