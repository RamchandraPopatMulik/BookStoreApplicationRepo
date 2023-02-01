using BookStoreManager.Interface;
using BookStoreRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BookStoreManager.Manager.AdminManager;

namespace BookStoreManager.Manager
{
    public class AdminManager : IAdminManager
    {
        private readonly IAdminRepository adminRepository;

        public AdminManager(IAdminRepository adminRepository)
        {
            this.adminRepository = adminRepository;
        }
        public string AdminLogin(string EmailID, string Password)
        {
            try
            {
                return this.adminRepository.AdminLogin(EmailID, Password);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
