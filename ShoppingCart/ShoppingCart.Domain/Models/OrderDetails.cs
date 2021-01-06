using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShoppingCart.Domain.Models
{
    public class OrderDetails
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Product")]
        public Guid ProductFk { get; set; }
        public virtual Product Product { get; set; }

        [ForeignKey("Order")]
        public Guid OrderFk { get; set; }
        public virtual Order Order { get; set; }
        public int Quantity { get; set; }
    }
}
