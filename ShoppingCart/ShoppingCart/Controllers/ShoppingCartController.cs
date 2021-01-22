using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Application.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

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

        [Authorize]
        public IActionResult Index(string email)
        {
            try
            {
                var list = _cartProdsService.GetCartProds(email);
                return View(list);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [Authorize]
        public IActionResult Delete(Guid id)
        {
            try
            {
                string email = User.Identity.Name;
                _cartProdsService.RemoveCartProduct(id, email);
                TempData["feedback"] = "Product from cart was deleted successfully";
                _logger.LogInformation("Product from cart was deleted successfully");
                return RedirectToAction("Index", new { Email = email });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [Authorize]
        public IActionResult Checkout()
        {
            try
            {
                string email = User.Identity.Name;
                _orderDetService.Checkout(email);
                TempData["feedback"] = "Added to Order";
                Message = "User: " + User.Identity.Name + " successfully made a purchase";
                _logger.LogInformation(Message);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

    }
}
