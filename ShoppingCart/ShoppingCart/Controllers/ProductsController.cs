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
        public ProductsController(IProductsService productsService,
            ICategoriesService categoriesService)
        {
            _productsService = productsService;
            _categoriesService = categoriesService;
        }
        /*
        public IActionResult Index()
        {
            var list = _productsService.GetProducts();
            return View(list);
        }*/

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
                _productsService.AddProduct(data);
                ViewData["feedback"] = "Product was added successfully";
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                ViewData["warning"] = "Product was not added. Check your details";
            }
            var catList = _categoriesService.GetCategories();
            ViewBag.Categories = catList;

            return View();
        }

        public async Task<IActionResult> Index(int pageNumber = 1) {
            var list = _productsService.GetProducts();

            return View(await PageinatedList<ProductViewModel>.CreateAsync(list,pageNumber,6));
        }

        public IActionResult Delete(Guid id)
        {
            _productsService.DeleteProduct(id);
            TempData["feedback"] = "Product was deleted successfully"; //change wherever we are using ViewData to use TempData data
            return RedirectToAction("Index");
        }
    }
}
