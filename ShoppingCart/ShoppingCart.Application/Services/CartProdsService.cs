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
    public class CartProdsService : ICartProdsService
    {

        private IProductsRepository _productRepo;
        private ICartRepository _cartRepo;
        private IMembersRepository _memberRepo;
        private ICartProdRepository _cartProdRepo;
        private IMapper _mapper;

        public CartProdsService(IProductsRepository productRepo, ICartRepository cartRepo, IMembersRepository memberRepo, ICartProdRepository cartProdRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _cartRepo = cartRepo;
            _memberRepo = memberRepo;
            _cartProdRepo = cartProdRepo;
            _mapper = mapper;
        }


        public void AddToCart(Guid prodID, string email) {
            CartProd scp = new CartProd();
            scp.ShoppingCartFk = _cartRepo.GetCart(email).Id;
            scp.Quantity += 1;
            scp.DateCreated = System.DateTime.Now;
            scp.ProductFk = _productRepo.GetProduct(prodID).Id;
            _cartProdRepo.AddToCart(scp);
        }

        public void UpdateToCart(Guid id, string email) {
            CartProd scp = _cartProdRepo.GetCartProduct(id);
            scp.ShoppingCartFk = _cartRepo.GetCart(email).Id;
            int currentQuantity = scp.Quantity;
            currentQuantity++;
            scp.Quantity = currentQuantity;
            scp.DateCreated = System.DateTime.Now;
            scp.ProductFk = _productRepo.GetProduct(id).Id;
            _cartProdRepo.UpdateCart(scp);
        }

        public void CreateCart(string email) {
            Cart c = new Cart();
            c.Email = _memberRepo.GetMember(email).Email;
            _cartRepo.CreateCart(c);
        }

        public void CartOptions(Guid ProdId, string email)
        {
            if (_cartRepo.GetCart(email) != null)
            {
                if (_productRepo.GetProduct(ProdId).Quantity != 0)
                {
                    if (_cartProdRepo.GetCartProduct(ProdId) == null)
                    {
                        AddToCart(ProdId, email);
                    }
                    else
                    {
                        UpdateToCart(ProdId, email);
                    }
                }
            }
            else {
                CreateCart(email);
            }            
        }

        public void RemoveCartProduct(Guid ProdId, string email)
        {
            if (_cartProdRepo.GetCartProduct(ProdId) != null)
            {
                _cartProdRepo.RemoveCartProduct(ProdId);
            }
        }

        public IQueryable<CartProdViewModel> GetCartProds(string email){
            var cartDb = _cartRepo.GetCart(email);
            return _cartProdRepo.GetCartProds().Where(e => e.ShoppingCartFk == cartDb.Id).ProjectTo<CartProdViewModel>(_mapper.ConfigurationProvider);
        }
    }
}
