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
    public class OrderRepository : IOrderRepository
    {
        private string? connectionString;
        public OrderRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("UserDBConnection");
        }
        public PlaceOrderModel PlaceOrder(PlaceOrderModel placeOrderModel)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SPPlaceOrder", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", placeOrderModel.UserID);
                    command.Parameters.AddWithValue("@CartID", placeOrderModel.CartID);
                    command.Parameters.AddWithValue("@AddressID", placeOrderModel.AddressID);
                    command.Parameters.AddWithValue("@Date_Time", DateTime.Now);

                    connection.Open();
                    int AddOrNot = command.ExecuteNonQuery();

                    if (AddOrNot >= 1)
                    {
                        return placeOrderModel;
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
        public List<GetOrdersModel> GetAllOrders(int UserID)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                List<GetOrdersModel> orders = new List<GetOrdersModel>();
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SPGetAllOrders", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", UserID);


                    connection.Open();
                    SqlDataReader Reader = command.ExecuteReader();

                    if (Reader.HasRows)
                    {
                        while (Reader.Read())
                        {
                            GetOrdersModel order = new GetOrdersModel()
                            {
                                OrderID = Reader.IsDBNull("OrderID") ? 0 : Reader.GetInt32("OrderID"),
                                CartID = Reader.IsDBNull("CartID") ? 0 : Reader.GetInt32("CartID"),
                                AddressID = Reader.IsDBNull("AddressID") ? 0 : Reader.GetInt32("AddressID"),
                                Total_Price = Reader.IsDBNull("Total_Price") ? 0 : Reader.GetInt32("Total_Price"),
                                Date_Time = Reader.IsDBNull("Date_Time") ? DateTime.MinValue : Reader.GetDateTime("Date_Time"),
                                UserID = Reader.IsDBNull("UserID") ? 0 : Reader.GetInt32("UserID"),

                            };
                            orders.Add(order);
                        }
                        return orders;
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
        public bool DeleteOrder(int OrderID, int UserID)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SPDeleteOrder", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@OrderID", OrderID);
                    command.Parameters.AddWithValue("@UserID", UserID);

                    connection.Open();
                    int DeleteOrNot = command.ExecuteNonQuery();

                    if (DeleteOrNot >= 1)
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
    }
}
