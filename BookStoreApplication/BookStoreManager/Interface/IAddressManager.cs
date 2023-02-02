using BookStoreModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManager.Interface
{
    public interface IAddressManager
    {
        public AddressModel AddAddress(AddressModel addressModel);
        public AddressModel UpdateAddress(AddressModel addressModel);
        public bool DeleteAddress(int AddressID, int UserID);
        public List<AddressModel> GetAllAddress(int UserID);
    }
}
