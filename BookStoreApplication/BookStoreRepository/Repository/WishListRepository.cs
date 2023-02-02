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
    public class WishListRepository : IWishListRepository
    {
        private string? connectionString;

        public WishListRepository(IConfiguration configuration, IConfiguration config)
        {
            connectionString = configuration.GetConnectionString("UserDBConnection");
        }
        public WishlistModel AddToWishlist(WishlistModel wishlistModel)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SPAddWishlist", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@BookID", wishlistModel.BookID);
                    command.Parameters.AddWithValue("@UserID", wishlistModel.UserID);

                    connection.Open();
                    int registerOrNot = command.ExecuteNonQuery();

                    if (registerOrNot >= 1)
                    {
                        return wishlistModel;
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
        public bool DeleteWishlist(int WishlistID, int UserID)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SPDeleteWishlist", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@WishlistID", WishlistID);
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
        public List<WishlistModel> GetAllWishlist(int UserID)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                List<WishlistModel> wishlist = new List<WishlistModel>();
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SPGetAllWishlist", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", UserID);


                    connection.Open();
                    SqlDataReader Reader = command.ExecuteReader();

                    if (Reader.HasRows)
                    {
                        while (Reader.Read())
                        {
                            WishlistModel book = new WishlistModel()
                            {
                                WishlistID = Reader.IsDBNull("WishlistID") ? 0 : Reader.GetInt32("WishlistID"),
                                BookID = Reader.IsDBNull("BookID") ? 0 : Reader.GetInt32("BookID"),
                                UserID = Reader.IsDBNull("UserID") ? 0 : Reader.GetInt32("UserID"),

                            };
                            wishlist.Add(book);
                        }
                        return wishlist;
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
