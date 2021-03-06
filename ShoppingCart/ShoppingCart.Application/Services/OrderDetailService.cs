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
    public class OrderDetailService : IOrderDetailsService
    {
        private IOrderDetailsRepository _orderDetailsRepo;
        private IOrdersRepository _orderRepo;
        private IMembersRepository _memberRepo;
        private IOrderService _orderSer;
        private ICartProdsService _cartProdSer;
        private IProductsService _prodSer;

        public OrderDetailService(IOrderDetailsRepository orderDetailsRepo,IOrdersRepository orderRepo, IMembersRepository memberRepo, IOrderService orderService, ICartProdsService cartProdsSer, IProductsService prodSer)
        {
            _orderDetailsRepo = orderDetailsRepo;
            _orderRepo = orderRepo;
            _memberRepo = memberRepo;
            _orderSer = orderService;
            _cartProdSer = cartProdsSer;
            _prodSer = prodSer;
        }

        public void Checkout(string email)
        {
            DateTime created = System.DateTime.Now;

            _orderSer.CreateOrder(email, created);
            
            OrderDetails ord;
            CartProd cartProd;
            Guid orderId = _orderRepo.GetOrder(email, created).Id;
            var list = _cartProdSer.GetCartProds(email).ToList();

            foreach (var prod in list)
            {
                ord = new OrderDetails();
                ord.OrderFk = orderId;
                ord.ProductFk = prod.Product.Id;
                ord.Quantity = prod.Quantity;
                _orderDetailsRepo.AddToOrderDetails(ord);

                _prodSer.UpdateProduct(ord.ProductFk, ord.Quantity);

                cartProd = new CartProd();
                cartProd.ProductFk = prod.Product.Id;
                _cartProdSer.RemoveCartProduct(cartProd.ProductFk, email);
            }

        }
    }
}
