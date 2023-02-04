using BookStoreModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManager.Interface
{
    public interface ICartManager
    {
        public CartModel AddCart(CartModel cartModel);

        public CartModel UpdateCart(CartModel cartModel);

        public bool DeleteCart(int CartID, int UserID);

        public List<CartModel> GetAllCart(int UserID);

        public CartModel GetCartByID(int CartID);
    }
}
