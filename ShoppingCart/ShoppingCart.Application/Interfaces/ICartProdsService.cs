﻿using ShoppingCart.Application.ViewModels;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Application.Interfaces
{
    public interface ICartProdsService
    {
        void AddToCart(Guid id, string email);

        IQueryable<CartProdViewModel> GetCartProds(string email);
    }
}