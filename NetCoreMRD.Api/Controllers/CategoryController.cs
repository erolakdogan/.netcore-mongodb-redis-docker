using AutoMapper;
using NetCoreMRD.Shared.Business.Contract;
using LinqKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetCoreMRD.Shared.Model;
using NetCoreMRD.Shared.Model.Response;
using NetCoreMRD.Shared.Utilities;

namespace NetCoreMRD.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly IMapper _mapper;
        private ICacheService _redisCache;
        private readonly IMongoRepository<Category> _categoryRepository;
        private readonly IMongoRepository<Product> _productRepository;

        public CategoryController(ILogger<CategoryController> logger, IMapper mapper, ICacheService redisCache, IMongoRepository<Category> categoryRepository, IMongoRepository<Product> productRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _redisCache = redisCache;
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;

        }

        [HttpGet("details")]
        [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<CategoryResponse> CategoryDetail(string id)
        {
            var cacheData = _redisCache.Get<CategoryResponse>(id);
            if (cacheData != null) return cacheData;
            var category = await _categoryRepository.FindByIdAsync(ObjectId.Parse(id));
            if (category == null)
            {
                throw new Exception("not found");
            }
            var mapped = _mapper.Map<CategoryResponse>(category);
            var response = _redisCache.Set<CategoryResponse>(id, mapped);
            return response;
        }

        [HttpGet("list")]
        [ProducesResponseType(typeof(List<CategoryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public List<CategoryResponse> CategoryList(string name)
        {
            var filter = PredicateBuilder.New<Category>();
            filter.And(x => true);

            if (!string.IsNullOrEmpty(name))
            {
                filter.And(x => x.Name.ToLower().Contains(name.ToLower()));
            }

            var categories = _categoryRepository.FilterBy(filter).ToList();
            var response = _mapper.Map(categories, new List<CategoryResponse>());

            return response;
        }

        [HttpPost("create")]
        public async Task<Category> AddCategory(Category item)
        {
            await _categoryRepository.InsertOneAsync(item);
            return item;
        }

        [HttpPut("update")]
        public async Task<CategoryResponse> UpdateCategory(CategoryResponse _category)
        {
            var model = await _categoryRepository.FindByIdAsync(ObjectId.Parse(_category.Id));
            var map = _mapper.Map(_category, model);
            map.Id = ObjectId.Parse(_category.Id);
            await _categoryRepository.ReplaceOneAsync(map);
            var response = _mapper.Map<CategoryResponse>(map);

            return response;
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            await _categoryRepository.DeleteByIdAsync(id);
            return Ok("Başarılı");
        }
    }

}
