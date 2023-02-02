using BookStoreManager.Interface;
using BookStoreModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BookStoreApplication.Controllers
{
    [Route("BookStore/[controller]")]
    [Authorize(Roles = Role.User)]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressManager addressManager;
        public AddressController(IAddressManager addressManager)
        {
            this.addressManager = addressManager;
        }
        [HttpPost]
        [Route("BookStore/AddAddress")]
        public IActionResult AddAddress(AddressModel addressModel)
        {
            try
            {
                addressModel.UserID = Convert.ToInt32(User.FindFirst("UserID").Value);
                AddressModel addressData = this.addressManager.AddAddress(addressModel);
                if (addressData != null)
                {
                    return this.Ok(new { success = true, message = "Address Added Successfully", result = addressData });
                }
                return this.Ok(new { success = true, message = "Address Already Exists" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpPut]
        [Route("BookStore/UpdateAddress")]
        public IActionResult UpdateAddress(AddressModel addressModel)
        {
            try
            {
                addressModel.UserID = Convert.ToInt32(User.FindFirst("UserID").Value);
                AddressModel addressData = this.addressManager.AddAddress(addressModel);
                if (addressData != null)
                {
                    return this.Ok(new { success = true, message = "Address Updated Successfully", result = addressData });
                }
                return this.Ok(new { success = true, message = "Address Not Updated" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpDelete]
        [Route("BookStore/DeleteAddress")]
        public IActionResult DeleteAddress(int AddressID)
        {
            try
            {
                int UserID = Convert.ToInt32(User.FindFirst("UserID").Value);
                bool addressDelete = this.addressManager.DeleteAddress(AddressID, UserID);
                if (addressDelete)
                {
                    return this.Ok(new { success = true, message = "Address Deleted Successfully", result = addressDelete });
                }
                return this.Ok(new { success = true, message = "Enter Valid AddressID" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpGet]
        [Route("BookStore/GetAllAddress")]
        public IActionResult GetAllAddress()
        {
            try
            {
                int UserID = Convert.ToInt32(User.FindFirst("UserID").Value);
                List<AddressModel> addressData = this.addressManager.GetAllAddress(UserID);
                if (addressData != null)
                {
                    return this.Ok(new { success = true, message = "Address Get Successfully", result = addressData });
                }
                return this.Ok(new { success = true, message = "Address is Empty" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}
