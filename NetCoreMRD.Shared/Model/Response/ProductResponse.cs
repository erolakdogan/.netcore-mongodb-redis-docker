using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreMRD.Shared.Model.Response
{
    public class ProductResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CategoryResponse CategoryId { get; set; }
        public double Price { get; set; }
        public string Currency { get; set; }
    }
}
