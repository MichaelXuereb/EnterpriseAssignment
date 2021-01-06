using AutoMapper;
using ShoppingCart.Application.ViewModels;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Application.AutoMapper
{
    public class DomainToViewModelProfile : Profile
    {
        public DomainToViewModelProfile() {
            CreateMap<Product, ProductViewModel>();
            CreateMap<Category,CategoryViewModel>();
            CreateMap<Member, MemberViewModel>();
            CreateMap<Cart, CartViewModel>();
            CreateMap<CartProd, CartProdViewModel>();
            CreateMap<Order, OrderViewModel>();
            CreateMap<OrderDetails, OrderDetailsViewModel>();
        }
    }
}
