using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.Services;
using ShoppingCart.Application.ViewModels;
using ShoppingCart.Domain.Models;
using ShoppingCart.Models;

namespace Presentation.Controllers
{
    public class ProductsController : Controller
    {
        private IProductsService _productsService;
        private ICategoriesService _categoriesService;
        private ICartProdsService _cartProdsService;
        private IWebHostEnvironment _env;
        private readonly ILogger<ProductsController> _logger;

        public string Message { get; set; }

        public ProductsController(IProductsService productsService,
            ICategoriesService categoriesService, ICartProdsService cartProdsService, IWebHostEnvironment env, ILoggerFactory logger)
        {
            _productsService = productsService;
            _categoriesService = categoriesService;
            _cartProdsService = cartProdsService;
            _env = env;
            _logger = logger.CreateLogger<ProductsController>();
        }

        public async Task<IActionResult> Index(string category, int pageNumber = 1){
            try
            {
                var list = _productsService.GetProducts(category);
                return View(await PageinatedList<ProductViewModel>.CreateAsync(list, pageNumber, 10));
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var catList = _categoriesService.GetCategories();

            ViewBag.Categories = catList;

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(ProductViewModel data, IFormFile file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        if (file.Length > 0)
                        {
                            string fileName = Guid.NewGuid() + System.IO.Path.GetExtension(file.FileName);
                            string path = _env.WebRootPath + @"\Images\";

                            using (var stream = System.IO.File.Create(path + fileName))
                            {
                                file.CopyTo(stream);
                            }

                            data.ImageUrl = @"\Images\" + fileName;

                            _productsService.AddProduct(data);
                            TempData["feedback"] = "Product was added successfully";
                            Message = "User: " + User.Identity.Name + " successfully added a Product to the database";
                            _logger.LogInformation(Message);
                            ModelState.Clear();
                        }
                    }
                    else {
                        Message = "Error while adding product to Database";
                        _logger.LogError(Message);
                        TempData["warning"] = Message;
                    }
                    
                }
                else {
                    Message = "Error while adding product to Database";
                    _logger.LogError(Message);
                    TempData["warning"] = Message;
                }
                
            }
            catch (Exception ex)
            {
                TempData["warning"] = "Product was not added. Check your details" + ex;
                Message = "Error while adding product to Database";
                _logger.LogWarning(Message);
                return RedirectToAction("Error", "Home");

            }
            var catList = _categoriesService.GetCategories();
            ViewBag.Categories = catList;

            return View();
        }

        public IActionResult Details(Guid id)
        {
            try
            {
                var myProduct = _productsService.GetProduct(id);
                return View(myProduct);
            }
            catch (Exception ex) {
                return RedirectToAction("Error", "Home");
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(Guid id, string cate)
        {
            try {
                _productsService.DeleteProduct(id);
                TempData["feedback"] = "Product was deleted successfully";
                _logger.LogInformation("Product was deleted successfully");
                return RedirectToAction("Index", new { category = cate });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [Authorize]
        public IActionResult AddToCart(Guid id, string cate)
        {
            try
            {
                string email = User.Identity.Name;
                _cartProdsService.CartOptions(id, email);
                Message = "User: " + User.Identity.Name + " added a Product to the Cart";
                _logger.LogInformation(Message);
                TempData["feedback"] = "Added to Cart";
                return RedirectToAction("Index", new { category = cate });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

    }
}
