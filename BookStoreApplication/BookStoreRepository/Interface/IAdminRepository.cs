using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRepository.Interface
{
    public interface IAdminRepository
    {
        public string AdminLogin(string EmailID, string Password);
    }
}
