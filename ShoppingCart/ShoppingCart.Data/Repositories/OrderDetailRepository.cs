using ShoppingCart.Data.Context;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Data.Repositories
{
    public class OrderDetailRepository : IOrderDetailsRepository
    {
        private ShoppingCartDbContext _context;

        public OrderDetailRepository(ShoppingCartDbContext context)
        {
            _context = context;
        }
        public void AddToOrderDetails(OrderDetails order)
        {

            _context.OrderDetails.Add(order);
            _context.SaveChanges();
        }

        public IQueryable<OrderDetails> GetOrderDetails()
        {
            return _context.OrderDetails;
        }

        public OrderDetails GetOrderProducts(Guid id)
        {
            return _context.OrderDetails.SingleOrDefault(x => x.Product.Id == id);
        }
    }
}
