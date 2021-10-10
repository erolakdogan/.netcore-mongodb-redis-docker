using AutoMapper;
using LinqKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using NetCoreMRD.Shared.Business.Contract;
using NetCoreMRD.Shared.Model;
using NetCoreMRD.Shared.Model.Request;
using NetCoreMRD.Shared.Model.Response;
using NetCoreMRD.Shared.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NetCoreMRD.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IMapper _mapper;
        private ICacheService _redisCache;
        private readonly IMongoRepository<Product> _productRepository;
        private readonly IMongoRepository<Category> _categoryRepository;
        public ProductController(ILogger<ProductController> logger, IMapper mapper, ICacheService redisCache, IMongoRepository<Product> productRepository, IMongoRepository<Category> categoryRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _redisCache = redisCache;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        #region product detay
        [HttpGet("details")]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<ProductResponse> ProductDetail(string id)
        {
            //Redis cache
            var cacheData = _redisCache.Get<ProductResponse>(id);
            if (cacheData != null) return cacheData;
            var product = await _productRepository.FindByIdAsync(ObjectId.Parse(id));
            if (product == null)
            {
                throw new Exception("not found");
            }
            var mapped = _mapper.Map<ProductResponse>(product);
            var response = _redisCache.Set<ProductResponse>(id, mapped);
            return response;
        }
        #endregion

        [HttpGet("list")]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public List<ProductResponse> ProductList(string name)
        {
            var filter = PredicateBuilder.New<Product>();
            filter.And(x => true);

            if (!string.IsNullOrEmpty(name))
            {
                filter.And(x => x.Name.ToLower().Contains(name.ToLower()));
            }
            var products = _productRepository.FilterBy(filter).ToList();
            var response = _mapper.Map(products, new List<ProductResponse>());

            return response;
        }
        [HttpPost("create")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ProductResponse> AddProduct(ProductRequest _product)
        {
            var category = await _categoryRepository.FindByIdAsync(ObjectId.Parse(_product.CategoryId));
            var map = _mapper.Map<Product>(_product);
            map.CategoryId = _mapper.Map(category, new CategoryResponse());
            await _productRepository.InsertOneAsync(map);

            var response = _productRepository.FindById(map.Id);
            return _mapper.Map<ProductResponse>(response);
        }

        [HttpPut("update")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct(ProductRequest _product, string id)
        {
            var model = await _productRepository.FindByIdAsync(ObjectId.Parse(id));
            if (model != null)
            {
                var map = _mapper.Map(_product, model);
                map.Id = ObjectId.Parse(id);
                await _productRepository.ReplaceOneAsync(map);
                var response = _mapper.Map<ProductResponse>(map);
                return Ok(response);
            }
            throw new Exception("not found");
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            await _productRepository.DeleteByIdAsync(id);
            return Ok("Başarılı");
        }
    }
}
