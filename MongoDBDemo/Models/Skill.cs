using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDBDemo.Models
{
    public class Skill
    {
        public string Name { get; set; }
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Rate { get; set; }
    }
}
