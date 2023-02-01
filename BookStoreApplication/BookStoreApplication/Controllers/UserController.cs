using BookStoreManager.Interface;
using BookStoreModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookStoreApplication.Controllers
{
    [Route("BookStore/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager userManager;

        public UserController(IUserManager userManager)
        {
            this.userManager = userManager;
        }
        [HttpPost]
        [Route("BookStore/SignUp")]
        public IActionResult SignUp(UserSignUpModel userSignUp)
        {
            try
            {
                UserSignUpModel registrationData = this.userManager.SignUp(userSignUp);
                if (registrationData != null)
                {
                    return this.Ok(new { success = true, message = "Registration Successfull", result = registrationData });
                }
                return this.Ok(new { success = true, message = "User Already Exists" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpPost]
        [Route("BookStore/Login")]
        public IActionResult Login(string EmailID, string Password)
        {
            try
            {
                string userToken = this.userManager.Login(EmailID, Password);
                if (userToken != null)
                {
                    return this.Ok(new { success = true, message = "Login Successfull", result = userToken });
                }
                return this.Ok(new { success = true, message = "Enter Valid EmailID or Password" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpGet]
        [Route("BookStore/ForgotPassword")]
        public IActionResult ForgotPassword(string EmailID)
        {
            try
            {
                string emailToken = this.userManager.ForgotPassword(EmailID);
                if (emailToken != null)
                {
                    return this.Ok(new { success = true, message = "Password Forgot Sucessfull", result = emailToken });
                }
                return this.Ok(new { success = true, message = "Enter Valid EmailID" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [Authorize]
        [HttpPut]
        [Route("BookStore/ResetPassword")]
        public IActionResult ResetPassword(string password, string confirmPassword)
        {
            try
            {
                string emailID = User.FindFirst(ClaimTypes.Email).Value.ToString();
                if (password == confirmPassword)
                {
                    bool userPassword = this.userManager.ResetPassword(password, emailID);
                    if (userPassword)
                    {
                        return this.Ok(new { success = true, message = "Password Reset Successfully", result = userPassword });
                    }
                }
                return this.Ok(new { success = true, message = "Enter Password same as above" });

            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}
