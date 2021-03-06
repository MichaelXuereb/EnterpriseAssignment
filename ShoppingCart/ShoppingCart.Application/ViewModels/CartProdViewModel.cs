﻿using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Application.ViewModels
{
    public class CartProdViewModel
    {
        public Guid Id { get; set; }

        public CartViewModel Cart { get; set; }

        public ProductViewModel Product { get; set; }

        public System.DateTime DateCreated { get; set; }

        public int Quantity { get; set; }
    }
}
