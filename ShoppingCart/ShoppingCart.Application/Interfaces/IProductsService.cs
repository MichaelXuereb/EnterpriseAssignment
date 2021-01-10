﻿using ShoppingCart.Application.ViewModels;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Application.Interfaces
{
    public interface IProductsService
    {
        IQueryable<ProductViewModel> GetProducts();

        ProductViewModel GetProduct(Guid id);

        void AddProduct(ProductViewModel data);

        void UpdateProduct(Guid id);

        void DeleteProduct(Guid id);
    }
}
