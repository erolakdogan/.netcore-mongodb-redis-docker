using NetCoreMRD.Shared.Model.Response;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NetCoreMRD.Shared.Model
{
    [BsonIgnoreExtraElements]
    [BsonCollection("Products")]
    public  class Product : BaseModel
    {
        public CategoryResponse CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Currency { get; set; }
       
    }
}
