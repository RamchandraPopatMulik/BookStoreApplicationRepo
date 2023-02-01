using BookStoreModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManager.Interface
{
    public interface IUserManager
    {
        public UserSignUpModel SignUp(UserSignUpModel userSignUp);
        public string Login(string EmailID, string Password);
        public string ForgotPassword(string emailID);
        public bool ResetPassword(string Password, string emailID);
    }
}
