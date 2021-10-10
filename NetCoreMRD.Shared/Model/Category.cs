using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreMRD.Shared.Model
{
    [BsonIgnoreExtraElements]
    [BsonCollection("Categories")]
    public class Category : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
