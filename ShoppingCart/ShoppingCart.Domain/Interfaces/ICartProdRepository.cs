﻿using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Domain.Interfaces
{
    public interface ICartProdRepository
    {
        void AddToCart(CartProd prod);
        IQueryable<CartProd> GetCartProds();
    }
}
