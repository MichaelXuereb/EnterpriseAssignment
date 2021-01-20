using ShoppingCart.Application.ViewModels;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Application.Interfaces
{
    public interface ICartProdsService
    {
        void CartOptions(Guid id, string email);

        IQueryable<CartProdViewModel> GetCartProds(string email);

        void RemoveCartProduct(Guid ProdId, string email);

        void AddToCart(Guid id, string email);

        void UpdateToCart(Guid id, string email);

        void CreateCart(string email);
    }
}
