using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManager.Interface
{
    public interface IAdminManager
    {
        public string AdminLogin(string EmailID, string Password);
    }
}
