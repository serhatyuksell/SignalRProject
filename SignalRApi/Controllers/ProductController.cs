﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalR.BusinessLayer.Abstract;
using SignalR.DataAccessLayer.Concrete;
using SignalR.DtoLayer.DiscountDto;
using SignalR.DtoLayer.FeatureDto;
using SignalR.DtoLayer.ProductDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult ProductList()
        {
            var value = _mapper.Map<List<ResultProductDto>>(_productService.TGetListAll());
            return Ok(value);
        }
        [HttpGet("ProductCount")]
        public IActionResult ProductCount()
        {
            var value = _productService.TProductCount();
            return Ok(value);
        }
        [HttpGet("TotalPriceByDrinkCategory")]
        public IActionResult TotalPriceByDrinkCategory()
        {
            var value = _productService.TTotalPriceByDrinkCategory();
            return Ok(value);
        }
        [HttpGet("TotalPriceBySaladCategory")]
        public IActionResult TotalPriceBySaladCategory()
        {
            var value = _productService.TTotalPriceBySaladCategory();
            return Ok(value);
        }
        [HttpGet("ProductAvgPriceByHamburger")]
        public IActionResult ProductAvgPriceByHamburger()
        {
            var value = _productService.TProductAvgPriceByHamburger();
            return Ok(value);
        }

        [HttpGet("ProductNameByMaxPrice")]
        public IActionResult ProductNameByMaxPrice()
        {
            var value = _productService.TProductNameByMaxPrice();
            return Ok(value);
        }
        [HttpGet("ProductNameByMinPrice")]
        public IActionResult ProductNameByMinPrice()
        {
            var value = _productService.TProductNameByMinPrice();
            return Ok(value);
        }
        [HttpGet("ProductPriceAvg")]
        public IActionResult ProductPriceAvg()
        {
            var value = _productService.TProductPriceAvg();
            return Ok(value);
        }
        [HttpGet("ProductPriceBySteakBurger")]
        public IActionResult ProductPriceBySteakBurger()
        {
            var value = _productService.TProductPriceBySteakBurger();
            return Ok(value);
        }
        [HttpGet("ProductCountByCategoryNameDrink")]
        public IActionResult ProductCountByCategoryNameDrink()
        {
            var value = _productService.TProductCountByCategoryNameDrink();
            return Ok(value);
        }
        [HttpGet("ProductCountByCategoryNameHamburger")]
        public IActionResult ProductCountByCategoryNameHamburger()
        {
            var value = _productService.TProductCountByCategoryNameHamburger();
            return Ok(value);
        }
        [HttpGet("ProductListWithCategory")]
        public IActionResult ProductListWithCategory()
        {
            var context = new SignalRContext();
            var values = context.Products.Include(x => x.Category).Select(x => new ResultProductWithCategory
            {
                ProductID = x.ProductID,
                ProductName = x.ProductName,
                Description = x.Description,
                Price = x.Price,
                ImageUrl = x.ImageUrl,
                ProductStatus = x.ProductStatus,
                CategoryName = x.Category.CategoryName


            });
            return Ok(values.ToList());
        }



            [HttpPost]
        public IActionResult CreateProduct(CreateProductDto createProductDto)
        {
            var value = _mapper.Map<Product>(createProductDto);
            _productService.TAdd(value);


            return Ok("Ürün Bilgisi Eklendi");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var value = _productService.TGetByID(id);
            _productService.TDelete(value);

            return Ok("Ürün Bilgisi silindi");
        }
        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var value = _productService.TGetByID(id);


            return Ok(_mapper.Map<GetProductDto>(value));
        }
        [HttpPut]
        public IActionResult ProductUpdate(UpdateProductDto updateProductDto)
        {
            var value = _mapper.Map<Product>(updateProductDto);
            _productService.TUpdate(value);

            return Ok("Product Bilgisi güncellendi");

        }
        [HttpGet("GetLast9Products")]
        public IActionResult GetLast9Products()
        {
            var value = _productService.TGetLast9Products();
            return Ok(value);

        }


    }
}
