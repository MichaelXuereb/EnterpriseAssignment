using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private ICartsService _cartsService;
        private ICartProdsService _cartProdsService;
        private IWebHostEnvironment _env;

        public ProductsController(IProductsService productsService,
            ICategoriesService categoriesService, ICartsService cartsService, ICartProdsService cartProdsService, IWebHostEnvironment env)
        {
            _productsService = productsService;
            _categoriesService = categoriesService;
            _cartsService = cartsService;
            _cartProdsService = cartProdsService;
            _env = env;
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
                if(file != null) {
                    if(file.Length > 0) {
                        string fileName = Guid.NewGuid() + System.IO.Path.GetExtension(file.FileName);
                        string path = _env.WebRootPath + @"\Images\";

                        using (var stream = System.IO.File.Create(path + fileName)) {
                            file.CopyTo(stream);
                        }

                        data.ImageUrl = @"\Images\" + fileName;
                    }
                }
                _productsService.AddProduct(data);
                ViewData["feedback"] = "Product was added successfully";
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                ViewData["warning"] = "Product was not added. Check your details" + ex;
            }
            var catList = _categoriesService.GetCategories();
            ViewBag.Categories = catList;

            return View();
        }

        public async Task<IActionResult> Index(int pageNumber = 1) {
            var list = _productsService.GetProducts().Where(x =>x.Category.Name == "Computer");

            return View(await PageinatedList<ProductViewModel>.CreateAsync(list,pageNumber,6));
        }

        public IActionResult Delete(Guid id)
        {
            _productsService.DeleteProduct(id);
            TempData["feedback"] = "Product was deleted successfully"; //change wherever we are using ViewData to use TempData data
            return RedirectToAction("Index");
        }

        public IActionResult AddToCart(Guid id, string email)
        {
            _cartProdsService.AddToCart(id,email);
            TempData["feedback"] = "Added to Cart";
            return RedirectToAction("Index");
        }

    }
}
