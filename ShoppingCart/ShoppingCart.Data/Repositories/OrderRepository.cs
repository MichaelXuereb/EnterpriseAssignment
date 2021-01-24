using ShoppingCart.Data.Context;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System;
using System.Linq;

namespace ShoppingCart.Data.Repositories
{
    public class OrderRepository : IOrdersRepository
    {
        private ShoppingCartDbContext _context;

        public OrderRepository(ShoppingCartDbContext context)
        {
            _context = context;
        }

        public void CreateOrder(Order o)
        {
            _context.Order.Add(o);
            _context.SaveChanges();
        }

        public Order GetOrder(string email, DateTime created)
        {
            return _context.Order.SingleOrDefault(x => x.Email == email && x.OrderDate == created);
        }
    }
}
