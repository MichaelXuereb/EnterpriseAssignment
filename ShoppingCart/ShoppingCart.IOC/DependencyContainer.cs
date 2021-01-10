using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShoppingCart.Application.AutoMapper;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.Services;
using ShoppingCart.Data.Context;
using ShoppingCart.Data.Repositories;
using ShoppingCart.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.IOC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ShoppingCartDbContext>(options =>
               options.UseSqlServer(connectionString
               ));

            services.AddScoped<IProductsRepository, ProductsRepository>();
            services.AddScoped<IProductsService, ProductsService>();

            services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            services.AddScoped<ICategoriesService, CategoriesService>();

            services.AddScoped<IMembersRepository, MembersRepository>();
            services.AddScoped<IMembersService, MembersSerivce>();

            services.AddScoped<ICartRepository, CartRepository>();

            services.AddScoped<ICartProdRepository, CartProdRepository>();
            services.AddScoped<ICartProdsService, CartProdsService>();

            services.AddScoped<IOrdersRepository, OrderRepository>();
            services.AddScoped<IOrderService, OrderService>();
            
            services.AddScoped<IOrderDetailsRepository, OrderDetailRepository>();
            services.AddScoped<IOrderDetailsService, OrderDetailService>();

            services.AddAutoMapper(typeof(AutoMapperConfig));
            AutoMapperConfig.RegisterMappings();
        }
    }
}
