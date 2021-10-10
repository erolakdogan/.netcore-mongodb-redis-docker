using AutoMapper;
using NetCoreMRD.Shared.Model;
using NetCoreMRD.Shared.Model.Request;
using NetCoreMRD.Shared.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreMRD.Api.Helper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<CategoryResponse, Category>().ForMember(d => d.Id, m => m.Ignore());
            ;
            CreateMap<Category, CategoryResponse>();

            CreateMap<Product, ProductResponse>();

            CreateMap<ProductRequest, Product>()
                .ForMember(d => d.CategoryId, m => m.Ignore());
        }
    }
}
