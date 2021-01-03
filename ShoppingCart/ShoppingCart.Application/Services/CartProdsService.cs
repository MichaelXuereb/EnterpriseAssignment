using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Application.Services
{
    public class CartProdsService : ICartProdsService
    {

        private IProductsRepository _productRepo;
        private ICartRepository _cartRepo;
        private IMembersRepository _memberRepo;
        private ICartProdRepository _cartProdRepo;
        private ICartsService _cartService;

        public CartProdsService(IProductsRepository productRepo, ICartRepository cartRepo, ICartsService cartsService, IMembersRepository memberRepo, ICartProdRepository cartProdRepo)
        {
            _productRepo = productRepo;
            _cartRepo = cartRepo;
            _memberRepo = memberRepo;
            _cartProdRepo = cartProdRepo;
            _cartService = cartsService;
        }


        public void AddToCart(Guid id, string email)
        {
            if (_cartRepo.GetCart(_memberRepo.GetMember(email).Email) == null)
            {
                Cart c = new Cart();
                _cartService.CreateCart(c,email);
            }

            CartProd scp = new CartProd();
            scp.ShoppingCartFk = _cartRepo.GetCart(_memberRepo.GetMember(email).Email).Id;
            scp.Quantity += 1;
            scp.DateCreated = System.DateTime.Now;
            scp.ProductFk = _productRepo.GetProduct(id).Id;
            _cartProdRepo.AddToCart(scp);
        }

        public IQueryable<CartProdViewModel> GetCartProds(string email)
        {
            var cartDb = _cartRepo.GetCart(email);

            var list = from p in _cartProdRepo.GetCartProds()
                       where p.ShoppingCartFk == cartDb.Id
                       select new CartProdViewModel()
                       {
                           id = p.id,
                           cart = new CartViewModel() { Id = p.ShoppingCartFk, DatePlaced = p.DateCreated, Email = cartDb.Email},
                           product = new ProductViewModel() { Id = p.ProductFk, Name = p.Product.Name, Price = p.Product.Price, Description = p.Product.Description, ImageUrl = p.Product.ImageUrl, Quantity = p.Quantity},
                           DateCreated = p.DateCreated,
                           Quantity = p.Quantity
                    
                       };

            return list;
        }
    }
}
