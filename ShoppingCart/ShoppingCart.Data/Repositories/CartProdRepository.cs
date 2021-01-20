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

        public CartProd GetCartProduct(Guid ProdId)
        {
            return _context.CartProd.SingleOrDefault(x => x.Product.Id == ProdId);
        }

        public void RemoveCartProduct(Guid ProdId)
        {
            _context.CartProd.Remove(GetCartProduct(ProdId));
            _context.SaveChanges();
        }

        public void UpdateCart(CartProd prod)
        {
            _context.CartProd.Update(prod);
            _context.SaveChanges();
        }
    }
}
