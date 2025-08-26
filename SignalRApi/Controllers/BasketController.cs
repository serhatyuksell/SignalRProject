using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalR.BusinessLayer.Abstract;
using SignalR.DataAccessLayer.Concrete;
using SignalR.DtoLayer.BasketDto;
using SignalR.EntityLayer.Entities;
using SignalRApi.Models;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }
        [HttpGet]
        public IActionResult GetBasketByMenuTableID(int id)
        {
            var values = _basketService.TGetBasketByMenuTableNumber(id);
            return Ok(values);

        }
        [HttpGet("BasketListByMenuTableWithProductName")]
        public IActionResult GetBasketListByMenuTableWithProductName(int id)
        {
            using var context=new SignalRContext();

            var values =context.Baskets.Include(b => b.Product)
                                       .Where(b => b.MenuTableID == id)
                                       .Select(z => new ResultBasketListWithProducts
                                       {
                                           BasketID = z.BasketID,
                                             MenuTableID = z.MenuTableID,
                                             Count = z.Count,
                                             Price = z.Price,
                                           ProductID = z.ProductID,
                                             ProductName = z.Product.ProductName,
                                             TotalPrice = z.TotalPrice

                                       })
                                       .ToList();
            return Ok(values);
        }
        [HttpPost]
        public IActionResult CreateBasket(CreateBasketDto createBasketDto)
        {
            using var context = new SignalRContext();
            _basketService.TAdd(new Basket()
            {
                Count =1,
                Price =context.Products.Where(x => x.ProductID == createBasketDto.ProductID).Select(y => y.Price).FirstOrDefault(),
                TotalPrice = createBasketDto.TotalPrice,
                ProductID = createBasketDto.ProductID,
                MenuTableID = createBasketDto.MenuTableID,
            });
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBasket(int id)
        {
            var basket = _basketService.TGetByID(id);
            if (basket != null)
            {
                _basketService.TDelete(basket);
                return Ok("Sepetteki Seçilen Ürün Silindi!");
            }
            return NotFound();
        }

    }
}
