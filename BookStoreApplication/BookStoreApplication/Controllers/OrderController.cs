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
    public class OrderController : ControllerBase
    {
        private readonly IOrderManager orderManager;
        public OrderController(IOrderManager orderManager)
        {
            this.orderManager = orderManager;
        }
        [HttpPost]
        [Route("BookStore/PlaceOrder")]
        public IActionResult PlaceOrder(PlaceOrderModel placeOrderModel)
        {
            try
            {
                placeOrderModel.UserID = Convert.ToInt32(User.FindFirst("UserID").Value);
                PlaceOrderModel orderData = this.orderManager.PlaceOrder(placeOrderModel);
                if (orderData != null)
                {
                    return this.Ok(new { success = true, message = "Order Placed Successfully", result = orderData });
                }
                return this.Ok(new { success = true, message = "Enter Valid CartID or AddressID" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpGet]
        [Route("BookStore/GetAllOrders")]
        public IActionResult GetAllOrders()
        {
            try
            {
                int UserID = Convert.ToInt32(User.FindFirst("UserID").Value);
                List<GetOrdersModel> orderData = this.orderManager.GetAllOrders(UserID);
                if (orderData != null)
                {
                    return this.Ok(new { success = true, message = "Orders Get Successfully", result = orderData });
                }
                return this.Ok(new { success = true, message = "No Order Present" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpDelete]
        [Route("BookStore/CancelOrder")]
        public IActionResult DeleteOrder(int OrderID)
        {
            try
            {
                int UserID = Convert.ToInt32(User.FindFirst("UserID").Value);
                bool orderDelete = this.orderManager.DeleteOrder(OrderID, UserID);
                if (orderDelete)
                {
                    return this.Ok(new { success = true, message = "Order Cancel Successfully", result = orderDelete });
                }
                return this.Ok(new { success = true, message = "Enter Valid OrderID" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}
