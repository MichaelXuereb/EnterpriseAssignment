using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ShoppingCart.Data.Context;

namespace ShoppingCart.Data.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private ShoppingCartDbContext _context;

        public ProductsRepository(ShoppingCartDbContext context)
        {
            _context = context;
        }

        public Product GetProduct(Guid id)
        {
            return _context.Products.SingleOrDefault(x => x.Id == id);
        }

        public IQueryable<Product> GetProducts()
        {
            return _context.Products;
        }

        public void AddProduct(Product p)
        {
            _context.Products.Add(p);
            _context.SaveChanges();
        }

        public void DeleteProduct(Guid id)
        {
            _context.Remove(GetProduct(id));
            _context.SaveChanges();
        }
    }
}
