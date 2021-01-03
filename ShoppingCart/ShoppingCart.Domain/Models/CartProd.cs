using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShoppingCart.Domain.Models
{
    public class CartProd
    {
        [Key]
        public Guid id { get; set; }

        [ForeignKey("Cart")]
        public Guid ShoppingCartFk { get; set; }

        public virtual Cart Cart { get; set; }

        [ForeignKey("Product")]
        public Guid ProductFk { get; set; }

        public virtual Product Product { get; set; }

        public System.DateTime DateCreated { get; set; }

        public int Quantity { get; set; }
    }
}
