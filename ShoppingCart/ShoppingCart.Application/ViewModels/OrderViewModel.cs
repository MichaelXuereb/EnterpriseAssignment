using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Application.ViewModels
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public DateTime OrderDate { get; set; }
        
    }
}
