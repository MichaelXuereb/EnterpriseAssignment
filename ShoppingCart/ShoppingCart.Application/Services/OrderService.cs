﻿using ShoppingCart.Application.Interfaces;
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
        private IMembersRepository _memberRepo;
        private IOrdersRepository _orderRepo;

        public OrderService(IMembersRepository memberRepo, IOrdersRepository orderRepo)
        {
            _memberRepo = memberRepo;
            _orderRepo = orderRepo;
        }

        public void CreateOrder(string email, DateTime created)
        {
            Order o = new Order();
            o.OrderDate = created;
            o.Email = email;
            _orderRepo.CreateOrder(o);
        }
    }
}
