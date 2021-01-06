﻿using ShoppingCart.Application.ViewModels;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Application.Interfaces
{
    public interface IOrderDetailsService
    {
        void AddToOrder(string email);
        IQueryable<OrderDetailsViewModel> GetOrderProds(string email);
        OrderDetailsViewModel GetOrderProduct(Guid id);
    }
}