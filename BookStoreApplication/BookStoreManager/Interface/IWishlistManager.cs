using BookStoreModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManager.Interface
{
    public interface IWishlistManager
    {
        public WishlistModel AddToWishlist(WishlistModel wishlistModel);

        public bool DeleteWishlist(int WishlistID, int UserID);

        public List<WishlistModel> GetAllWishlist(int UserID);
    }
}
