using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Application.Interfaces;

namespace ShoppingCart.Controllers
{
    public class ShoppingCartController : Controller
    {
        private ICartsService _cartsService;
        private ICartProdsService _cartProdsService;

        public ShoppingCartController(ICartsService cartsService, ICartProdsService cartProdsService)
        {
            _cartsService = cartsService;
            _cartProdsService = cartProdsService;
        }
        public IActionResult Index(string email)
        {
            var list = _cartProdsService.GetCartProds(email);
            return View(list);
        }
    }
}
