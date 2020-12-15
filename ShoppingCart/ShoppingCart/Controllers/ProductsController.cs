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

        public IActionResult Index()
        {
            var list = _productsService.GetProducts();
            return View(list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var catList = _categoriesService.GetCategories();

            ViewBag.Categories = catList;

            return View(); //model => ProductViewModel
        }

        [HttpPost]
        public IActionResult Create(ProductViewModel data, IFormFile file)
        {
            //validation
            try
            {  
                _productsService.AddProduct(data);
                ViewData["feedback"] = "Product was added successfully";
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                //log errors
                ViewData["warning"] = "Product was not added. Check your details";
            }
            var catList = _categoriesService.GetCategories();
            ViewBag.Categories = catList;

            return View();
        }
    }
}
