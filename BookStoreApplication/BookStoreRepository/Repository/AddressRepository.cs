using BookStoreModel;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStoreRepository.Interface;

namespace BookStoreRepository.Repository
{
    public class AddressRepository : IAddressRepository
    {
        private string? connectionString;
        public AddressRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("UserDBConnection");
        }
        public AddressModel AddAddress(AddressModel addressModel)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SPAddAddress", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FullAddress", addressModel.FullAddress);
                    command.Parameters.AddWithValue("@city", addressModel.City);
                    command.Parameters.AddWithValue("@State", addressModel.State);
                    command.Parameters.AddWithValue("@TypeID", addressModel.TypeID);
                    command.Parameters.AddWithValue("@UserID", addressModel.UserID);

                    connection.Open();
                    int registerOrNot = command.ExecuteNonQuery();

                    if (registerOrNot >= 1)
                    {
                        return addressModel;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        public AddressModel UpdateAddress(AddressModel addressModel)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SPUpdateAddress", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@AddressID", addressModel.AddressID);
                    command.Parameters.AddWithValue("@FullAddress", addressModel.FullAddress);
                    command.Parameters.AddWithValue("@city", addressModel.City);
                    command.Parameters.AddWithValue("@State", addressModel.State);
                    command.Parameters.AddWithValue("@TypeID", addressModel.TypeID);
                    command.Parameters.AddWithValue("@UserID", addressModel.UserID);

                    connection.Open();
                    int registerOrNot = command.ExecuteNonQuery();

                    if (registerOrNot >= 1)
                    {
                        return addressModel;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        public bool DeleteAddress(int AddressID, int UserID)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SPDeleteAddress", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@AddressID", AddressID);
                    command.Parameters.AddWithValue("@UserID", UserID);
                    connection.Open();
                    int registerOrNot = command.ExecuteNonQuery();

                    if (registerOrNot >= 1)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        public List<AddressModel> GetAllAddress(int UserID)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                List<AddressModel> addresses = new List<AddressModel>();
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SPGetAllAddress", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", UserID);


                    connection.Open();
                    SqlDataReader Reader = command.ExecuteReader();

                    if (Reader.HasRows)
                    {
                        while (Reader.Read())
                        {
                            AddressModel address = new AddressModel()
                            {
                                AddressID = Reader.IsDBNull("AddressID") ? 0 : Reader.GetInt32("AddressID"),
                                FullAddress = Reader.IsDBNull("FullAddress") ? string.Empty : Reader.GetString("FullAddress"),
                                City = Reader.IsDBNull("city") ? string.Empty : Reader.GetString("city"),
                                State = Reader.IsDBNull("State") ? string.Empty : Reader.GetString("State"),
                                TypeID = Reader.IsDBNull("TypeID") ? 0 : Reader.GetInt32("TypeID"),
                                UserID = Reader.IsDBNull("UserID") ? 0 : Reader.GetInt32("UserID"),

                            };
                            addresses.Add(address);
                        }
                        return addresses;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
    }
}
