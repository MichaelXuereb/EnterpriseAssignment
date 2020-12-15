using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Application.Services
{
    public class ProductsService : IProductsService
    {
        private IProductsRepository _productRepo;
        public ProductsService(IProductsRepository productRepo)
        {
            _productRepo = productRepo;
        }

        public IQueryable<ProductViewModel> GetProducts()
        {
            var list = from p in _productRepo.GetProducts()
                       select new ProductViewModel()
                       {
                           Id = p.Id,
                           Name = p.Name,
                           Price = p.Price,
                           Description = p.Description,
                           ImageUrl = p.ImageUrl,
                           Category = new CategoryViewModel() { Id = p.Category.Id, Name = p.Category.Name }
                       };

            return list;
        }

        public void AddProduct(ProductViewModel data)
        {
            Product p = new Product();
            p.Description = data.Description;
            p.ImageUrl = data.ImageUrl;
            p.Name = data.Name;
            p.Price = data.Price;
            p.CategoryId = data.Category.Id;


            _productRepo.AddProduct(p);
        }
    }
}
