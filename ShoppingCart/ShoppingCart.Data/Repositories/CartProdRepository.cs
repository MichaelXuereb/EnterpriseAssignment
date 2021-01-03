using ShoppingCart.Data.Context;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Data.Repositories
{
    public class CartProdRepository : ICartProdRepository
    {
        private ShoppingCartDbContext _context;

        public CartProdRepository(ShoppingCartDbContext context)
        {
            _context = context;
        }
       
        public void AddToCart(CartProd p)
        {
            _context.CartProd.Add(p);
            _context.SaveChanges();
        }

        public IQueryable<CartProd> GetCartProds()
        {
            return _context.CartProd;
        }
    }
}
