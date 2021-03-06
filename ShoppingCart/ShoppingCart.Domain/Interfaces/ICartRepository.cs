﻿using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Domain.Interfaces
{
    public interface ICartRepository
    {
        void CreateCart(Cart c);
        Cart GetCart(string email);
        void DeleteCart(string email);
    }
}
