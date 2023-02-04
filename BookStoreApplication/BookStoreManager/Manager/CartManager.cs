using BookStoreManager.Interface;
using BookStoreModel;
using BookStoreRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManager.Manager
{
    public class CartManager : ICartManager
    {
        private readonly ICartRepository cartRepository;
        public CartManager(ICartRepository cartRepository)
        {
            this.cartRepository = cartRepository;
        }
        public CartModel AddCart(CartModel cartModel)
        {
            try
            {
                return this.cartRepository.AddCart(cartModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public CartModel UpdateCart(CartModel cartModel)
        {
            try
            {
                return this.cartRepository.UpdateCart(cartModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteCart(int CartID, int UserID)
        {
            try
            {
                return this.cartRepository.DeleteCart(CartID, UserID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<CartModel> GetAllCart(int UserID)
        {
            try
            {
                return this.cartRepository.GetAllCart(UserID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public CartModel GetCartByID(int CartID)
        {
            try
            {
                return this.cartRepository.GetCartByID(CartID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        } 
    }
}

