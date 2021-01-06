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
    public class OrderService : IOrderService
    {

        private IOrderDetailsRepository _orderDetailsRepo;
        private IMembersRepository _memberRepo;
        private IOrdersRepository _orderRepo;
        private ICartProdsService _cartProdService;

        public OrderService(IMembersRepository memberRepo, IOrdersRepository orderRepo, IOrderDetailsRepository orderDetailsRepo, ICartProdsService cartProdService)
        {
            _memberRepo = memberRepo;
            _orderRepo = orderRepo;
            _orderDetailsRepo = orderDetailsRepo;
            _cartProdService = cartProdService;
        }

        public void CreateOrder(string email)
        {
            Order o = new Order();
            o.OrderDate = System.DateTime.Now;
            o.Email = _memberRepo.GetMember(email).Email;
            _orderRepo.CreateOrder(o);
        }

        public OrderViewModel GetOrder(string email)
        {
            OrderViewModel myViewModel = new OrderViewModel();
            var cartFromDb = _orderRepo.GetOrder(email);

            myViewModel.Email = cartFromDb.Email;
            myViewModel.OrderDate = cartFromDb.OrderDate;

            return myViewModel;
        }
    }
}
