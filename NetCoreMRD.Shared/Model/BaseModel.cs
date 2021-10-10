using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreMRD.Shared.Model
{
    public class BaseModel : IBaseModel
    {
        public ObjectId Id { get; set; }
    }

    public interface IBaseModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        ObjectId Id { get; set; }
    }
}
