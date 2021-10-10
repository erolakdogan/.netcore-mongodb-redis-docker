using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreMRD.Shared.Model.Request
{
    public class CategoryRequest
    {
        public ObjectId? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
