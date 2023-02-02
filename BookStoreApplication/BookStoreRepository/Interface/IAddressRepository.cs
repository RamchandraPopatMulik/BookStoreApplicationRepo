using BookStoreModel;
using BookStoreRepository.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRepository.Interface
{
    public interface IAddressRepository
    {
        public AddressModel AddAddress(AddressModel addressModel);
        public AddressModel UpdateAddress(AddressModel addressModel);
        public bool DeleteAddress(int AddressID, int UserID);
        public List<AddressModel> GetAllAddress(int UserID);
    }
}
