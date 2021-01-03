using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Application.ViewModels
{
    public class CartProdViewModel
    {
        public Guid id { get; set; }

        public CartViewModel cart { get; set; }

        public ProductViewModel product { get; set; }

        public System.DateTime DateCreated { get; set; }

        public int Quantity { get; set; }
    }
}
