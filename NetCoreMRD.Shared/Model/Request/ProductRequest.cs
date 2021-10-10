using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreMRD.Shared.Model.Request
{
    public class ProductRequest
    {
        public ObjectId? Id { get; set; }
        public string CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Currency { get; set; }
    }
}
