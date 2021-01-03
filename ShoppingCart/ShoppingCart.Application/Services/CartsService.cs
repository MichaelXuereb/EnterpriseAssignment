using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Application.Services
{
    public class CartsService : ICartsService
    {
        private ICartRepository _cartRepo;
        private IMembersRepository _memberRepo;

        public CartsService(ICartRepository cartRepo, IMembersRepository memberRepo)
        {
            _cartRepo = cartRepo;
            _memberRepo = memberRepo;
        }

        public void CreateCart(Cart c, string email)
        {
            c.DataPlaced = System.DateTime.Now;
            c.Email = _memberRepo.GetMember(email).Email;
            _cartRepo.CreateCart(c);
        }

        public CartViewModel GetCart(string email)
        {
            
            CartViewModel myViewModel = new CartViewModel();
            var cartFromDb = _cartRepo.GetCart(email);
            myViewModel.Id = cartFromDb.Id;
            myViewModel.Email = cartFromDb.Email;
            myViewModel.DatePlaced = cartFromDb.DataPlaced;

            return myViewModel;
            
        }

    }
}
