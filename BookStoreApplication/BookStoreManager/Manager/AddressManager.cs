using BookStoreManager.Interface;
using BookStoreModel;
using BookStoreRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BookStoreManager.Manager.AddressManager;

namespace BookStoreManager.Manager
{
   
        public class AddressManager : IAddressManager
        {
            private readonly IAddressRepository addressRepository;
            public AddressManager(IAddressRepository addressRepository)
            {
                this.addressRepository = addressRepository;
            }
            public AddressModel AddAddress(AddressModel addressModel)
            {
                try
                {
                    return this.addressRepository.AddAddress(addressModel);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            public AddressModel UpdateAddress(AddressModel addressModel)
            {
                try
                {
                    return this.addressRepository.UpdateAddress(addressModel);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            public bool DeleteAddress(int AddressID, int UserID)
            {
                try
                {
                    return this.addressRepository.DeleteAddress(AddressID, UserID);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            public List<AddressModel> GetAllAddress(int UserID)
            {
                try
                {
                    return this.addressRepository.GetAllAddress(UserID);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    
}
