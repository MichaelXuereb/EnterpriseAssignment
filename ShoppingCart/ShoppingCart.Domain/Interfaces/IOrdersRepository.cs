using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Domain.Interfaces
{
    public interface IOrdersRepository
    {
        void CreateOrder(Order o);
        Order GetOrder(string email);
    }
}
