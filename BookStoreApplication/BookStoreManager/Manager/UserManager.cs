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
    public class UserManager : IUserManager
    {
        private readonly IUserRepository userRepository;

        public UserManager(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public UserSignUpModel SignUp(UserSignUpModel userSignUp)
        {
            try
            {
                return this.userRepository.SignUp(userSignUp);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string Login(string EmailID, string Password)
        {
            try
            {
                return this.userRepository.Login(EmailID, Password);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string ForgotPassword(string emailID)
        {
            try
            {
                return this.userRepository.ForgotPassword(emailID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ResetPassword(string Password, string emailID)
        {
            try
            {
                return this.userRepository.ResetPassword(Password, emailID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
