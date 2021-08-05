using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDBDemo.Models
{
    public interface IBaseObject
    {
        static string CollectionName { get; set; }
    }
}
