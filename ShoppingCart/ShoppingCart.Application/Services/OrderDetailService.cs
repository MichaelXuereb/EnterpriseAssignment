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
    public class OrderDetailService : IOrderDetailsService
    {
        private IOrderDetailsRepository _orderDetailsRepo;
        private IOrdersRepository _orderRepo;
        private IMembersRepository _memberRepo;
        private IOrderService _orderSer;
        private ICartProdsService _cartProdSer;
        private ICartRepository _cartRepo;

        public OrderDetailService(IOrderDetailsRepository orderDetailsRepo, ICartRepository cartRepo,IOrdersRepository orderRepo, IMembersRepository memberRepo, IOrderService orderService, ICartProdsService cartProdsSer, ICartsService cartsService)
        {
            _orderDetailsRepo = orderDetailsRepo;
            _orderRepo = orderRepo;
            _memberRepo = memberRepo;
            _orderSer = orderService;
            _cartProdSer = cartProdsSer;
            _cartRepo = cartRepo;
        }

        public void AddToOrder(string email)
        {
            if (_orderRepo.GetOrder(_memberRepo.GetMember(email).Email) == null)
            {
                _orderSer.CreateOrder(email);
            }

            OrderDetails scp;
            CartProd cartProd;
            Guid orderId = _orderRepo.GetOrder(_memberRepo.GetMember(email).Email).Id;
            var list = _cartProdSer.GetCartProds(email).ToList();

            foreach (var prod in list)
            {
                scp = new OrderDetails();
                scp.OrderFk = orderId;
                scp.ProductFk = prod.Product.Id;
                scp.Quantity = prod.Quantity;
                _orderDetailsRepo.AddToOrderDetails(scp);

                cartProd = new CartProd();
                cartProd.ProductFk = prod.Product.Id;
                _cartProdSer.RemoveCartProduct(cartProd.ProductFk);
            }

        }

        public IQueryable<OrderDetailsViewModel> GetOrderProds(string email)
        {
            //CartViewModel cart = new CartViewModel();
            var cartDb = _orderRepo.GetOrder(email);

            var list = from p in _orderDetailsRepo.GetOrderDetails()
                       where p.OrderFk == cartDb.Id
                       select new OrderDetailsViewModel()
                       {
                           Order = new OrderViewModel() { Id = p.OrderFk, Email = cartDb.Email },
                           Product = new ProductViewModel() { Id = p.ProductFk, Name = p.Product.Name, Price = p.Product.Price, Description = p.Product.Description, ImageUrl = p.Product.ImageUrl, Quantity = p.Quantity },
                           Quantity = p.Quantity
                       };

            return list;
        }

        public OrderDetailsViewModel GetOrderProduct(Guid id)
        {
            // Getting the quantity of the selected product from the database
            OrderDetailsViewModel orderProd = new OrderDetailsViewModel();
            var productFromDb = _orderDetailsRepo.GetOrderProducts(id);

            orderProd.Product.Quantity = productFromDb.Product.Quantity;

            return orderProd;
        }
    }
}
