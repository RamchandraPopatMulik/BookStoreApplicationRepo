using BookStoreManager.Interface;
using BookStoreModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BookStoreApplication.Controllers
{
    [Route("BookStore/[controller]")]
    [Authorize(Roles = Role.User)]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistManager wishlistManager;
        public WishlistController(IWishlistManager wishlistManager)
        {
            this.wishlistManager = wishlistManager;
        }
        [HttpPost]
        [Route("BookStore/AddToWishlist")]
        public IActionResult AddToWishlist(WishlistModel wishlistModel)
        {
            try
            {
                wishlistModel.UserID = Convert.ToInt32(User.FindFirst("UserID").Value);
                WishlistModel wishlistData = this.wishlistManager.AddToWishlist(wishlistModel);
                if (wishlistData != null)
                {
                    return this.Ok(new { success = true, message = "Book Added to Wishlist Successfully", result = wishlistData });
                }
                return this.Ok(new { success = true, message = "Book Already Exists to Wishlist" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpDelete]
        [Route("BookStore/DeleteWishlist")]
        public IActionResult DeleteWishlist(int WishlistID)
        {
            try
            {
                int UserID = Convert.ToInt32(User.FindFirst("UserID").Value);
                bool wishlistDelete = this.wishlistManager.DeleteWishlist(WishlistID, UserID);
                if (wishlistDelete)
                {
                    return this.Ok(new { success = true, message = "Wishlist Deleted Successfully", result = wishlistDelete });
                }
                return this.Ok(new { success = true, message = "Enter Valid WishlistID" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpGet]
        [Route("BookStore/GetAllWishlist")]
        public IActionResult GetAllWishlist()
        {
            try
            {
                int UserID = Convert.ToInt32(User.FindFirst("UserID").Value);
                List<WishlistModel> wishlistData = this.wishlistManager.GetAllWishlist(UserID);
                if (wishlistData != null)
                {
                    return this.Ok(new { success = true, message = "All Wishlist Get Successfully", result = wishlistData });
                }
                return this.Ok(new { success = true, message = "Wishlist is Empty" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}
