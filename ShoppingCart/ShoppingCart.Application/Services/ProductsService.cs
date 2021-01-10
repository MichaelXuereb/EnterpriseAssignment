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

        public IQueryable<ProductViewModel> GetProducts()
        {
            return _productRepo.GetProducts().ProjectTo<ProductViewModel>(_mapper.ConfigurationProvider);
            /*
            var list = from p in _productRepo.GetProducts()
                       select new ProductViewModel()
                       {
                           Id = p.Id,
                           Name = p.Name,
                           Price = p.Price,
                           Description = p.Description,
                           ImageUrl = p.ImageUrl,
                           Quantity = p.Quantity,
                           Category = new CategoryViewModel() { Id = p.Category.Id, Name = p.Category.Name }
                       };

            return list;
            */
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
            /*
            ProductViewModel myViewModel = new ProductViewModel();
            var productFromDb = _productRepo.GetProduct(id);

            myViewModel.Description = productFromDb.Description;
            myViewModel.Id = productFromDb.Id;
            myViewModel.ImageUrl = productFromDb.ImageUrl;
            myViewModel.Quantity = productFromDb.Quantity;
            myViewModel.Name = productFromDb.Name;
            myViewModel.Price = productFromDb.Price;
            myViewModel.Category = new CategoryViewModel();
            myViewModel.Category.Id = productFromDb.Category.Id;
            myViewModel.Category.Name = productFromDb.Category.Name;

            return myViewModel;
            */
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