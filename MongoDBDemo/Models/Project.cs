using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDBDemo.Models
{
    public class Project: IBaseObject
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [BsonElementAttribute("Skills")]
        public List<Skill> Skills { get; set; }
        [BsonIgnore]
        public static string CollectionName { get; set; } = "Project";
    }
}
