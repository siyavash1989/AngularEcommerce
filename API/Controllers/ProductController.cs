using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class ProductController : ApiBaseController
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductBrand> _brandRepository;
        private readonly IGenericRepository<ProductType> _typeRepository;
        private readonly IMapper _mapper;

        public ProductController(
            IGenericRepository<Product> productRepository,
            IGenericRepository<ProductBrand> brandRepository,
            IGenericRepository<ProductType> typeRepository,
            IMapper mapper)
        {
            _typeRepository = typeRepository;
            _mapper = mapper;
            _productRepository = productRepository;
            _brandRepository = brandRepository;

        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetProducts()
        {
            var spec = new ProductsWithTypesAndBrandsSpecifications();
            var products = await _productRepository.ListAsync(spec);//.GetAllAsync();
            return Ok(_mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductDto>>(products));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecifications(id);
            var product = await _productRepository.GetEntityWithSpec(spec);//.GetByIdAsync(id);
            
            if(product == null) return NotFound(new ApiResponse(404));

            return _mapper.Map<Product,ProductDto>(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetBrands()
        {
            var brands = await _brandRepository.GetAllAsync();
            return Ok(brands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> GetTypes()
        {
            var types = await _typeRepository.GetAllAsync();
            return Ok(types);
        }
    }
}