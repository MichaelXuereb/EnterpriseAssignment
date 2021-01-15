using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Application.Interfaces;
using Microsoft.Extensions.Logging;
namespace ShoppingCart.Controllers
{
    public class ShoppingCartController : Controller
    {
        private ICartProdsService _cartProdsService;
        private IOrderDetailsService _orderDetService;
        private readonly ILogger<ShoppingCartController> _logger;
        public string Message { get; set; }

        public ShoppingCartController(IOrderDetailsService orderDetService, ICartProdsService cartProdsService, ILoggerFactory logger)
        {
            _orderDetService = orderDetService;
            _cartProdsService = cartProdsService;
            _logger = logger.CreateLogger<ShoppingCartController>();
        }
        public IActionResult Index()
        {
            string email = User.Identity.Name;
            var list = _cartProdsService.GetCartProds(email);
            return View(list);
        }

        public IActionResult Delete(Guid id)
        {
            _cartProdsService.RemoveCartProduct(id);
            TempData["feedback"] = "Product was deleted successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Checkout()
        {
            string email = User.Identity.Name;
            _orderDetService.Checkout(email);
            TempData["feedback"] = "Added to Order";
            Message = "User: " + User.Identity.Name + " successfully made a purchase";
            _logger.LogInformation(Message);
            return RedirectToAction("Index", "Products");
        }

    }
}
