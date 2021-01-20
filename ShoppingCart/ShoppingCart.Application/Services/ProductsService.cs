using AutoMapper;
using AutoMapper.QueryableExtensions;
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
    public class ProductsService : IProductsService
    {
        private IProductsRepository _productRepo;
        private IMapper _mapper;

        public ProductsService(IProductsRepository productRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }

        public IQueryable<ProductViewModel> GetProducts(string category)
        {
            if (category != null)
            {
                return _productRepo.GetProducts().Where(e => e.Category.Name == category).ProjectTo<ProductViewModel>(_mapper.ConfigurationProvider);
            }
            else {
                return _productRepo.GetProducts().ProjectTo<ProductViewModel>(_mapper.ConfigurationProvider);
            }
        }

        public void AddProduct(ProductViewModel data)
        {
            var p = _mapper.Map<Product>(data);
            p.Category = null;
            _productRepo.AddProduct(p);
        }

        public ProductViewModel GetProduct(Guid id)
        {
            Product product = _productRepo.GetProduct(id);
            var resultingProductViewModel = _mapper.Map<ProductViewModel>(product);
            return resultingProductViewModel;
        }

        public void DeleteProduct(Guid id)
        {
            if (_productRepo.GetProduct(id) != null)
            {
                _productRepo.DeleteProduct(id);
            }
        }

        public void UpdateProduct(Guid id)
        {
            Product p = _productRepo.GetProduct(id);
            p.Quantity--;
            _productRepo.UpdateProductToDB(p);
        }
    }
}