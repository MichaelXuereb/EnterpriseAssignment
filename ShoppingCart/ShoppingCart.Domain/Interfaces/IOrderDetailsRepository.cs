using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Domain.Interfaces
{
    public interface IOrderDetailsRepository
    {
        void AddToOrderDetails(OrderDetails order);
        IQueryable<OrderDetails> GetOrderDetails();
        OrderDetails GetOrderProducts(Guid id);
    }
}
