using BookStoreManager.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApplication.Controllers
{
    [Route("BookStore/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminManager adminManager;
        public AdminController(IAdminManager adminManager)
        {
            this.adminManager = adminManager;
        }
        [HttpPost]
        [Route("BookStore/AdminLogin")]
        public IActionResult AdminLogin(string EmailID, string Password)
        {
            try
            {
                string adminToken = this.adminManager.AdminLogin(EmailID, Password);
                if (adminToken != null)
                {
                    return this.Ok(new { success = true, message = "Login Successfull", result = adminToken });
                }
                return this.Ok(new { success = true, message = "Enter Valid EmailID or Password" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}

