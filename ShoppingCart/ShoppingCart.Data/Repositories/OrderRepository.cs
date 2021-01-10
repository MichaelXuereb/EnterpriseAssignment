using ShoppingCart.Data.Context;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
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

        public Order GetOrder(string email)
        {
            return _context.Order.OrderByDescending(x => x.Email == email).Last();
        }
    }
}
