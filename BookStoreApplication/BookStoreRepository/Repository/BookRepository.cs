using BookStoreModel;
using BookStoreRepository.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRepository.Repository
{
    public class BookRepository : IBookRepository
    {
        //private readonly IConfiguration config;
        private string? connectionString;
        public BookRepository(IConfiguration configuration, IConfiguration config)
        {
            connectionString = configuration.GetConnectionString("UserDBConnection");
            // this.config = config;
        }
        public BookModel AddBook(BookModel bookModel)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SPAddBook", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@BookName", bookModel.BookName);
                    command.Parameters.AddWithValue("@AuthorName", bookModel.AuthorName);
                    command.Parameters.AddWithValue("@Ratings", bookModel.Ratings);
                    command.Parameters.AddWithValue("@No_of_People_Rated", bookModel.No_Of_People_Rated);
                    command.Parameters.AddWithValue("@Discount_Price", bookModel.Discount_Price);
                    command.Parameters.AddWithValue("@Original_Price", bookModel.Original_Price);
                    command.Parameters.AddWithValue("@Book_Detail", bookModel.Book_Details);
                    command.Parameters.AddWithValue("@Book_Image", bookModel.Book_Image);
                    command.Parameters.AddWithValue("@Book_Quantity", bookModel.Book_Quantity);

                    connection.Open();
                    int registerOrNot = command.ExecuteNonQuery();

                    if (registerOrNot >= 1)
                    {
                        return bookModel;
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
        public BookModel UpdateBook(BookModel bookModel)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SPUpdateBook", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@BookID", bookModel.BookID);
                    command.Parameters.AddWithValue("@BookName", bookModel.BookName);
                    command.Parameters.AddWithValue("@AuthorName", bookModel.AuthorName);
                    command.Parameters.AddWithValue("@Ratings", bookModel.Ratings);
                    command.Parameters.AddWithValue("@No_of_People_Rated", bookModel.No_Of_People_Rated);
                    command.Parameters.AddWithValue("@Discount_Price", bookModel.Discount_Price);
                    command.Parameters.AddWithValue("@Original_Price", bookModel.Original_Price);
                    command.Parameters.AddWithValue("@Book_Detail", bookModel.Book_Details);
                    command.Parameters.AddWithValue("@Book_Image", bookModel.Book_Image);
                    command.Parameters.AddWithValue("@Book_Quantity", bookModel.Book_Quantity);

                    connection.Open();
                    int registerOrNot = command.ExecuteNonQuery();

                    if (registerOrNot >= 1)
                    {
                        return bookModel;
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
        public bool DeleteBook(int BookID)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SPDeleteBook", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@BookID", BookID);

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
        public List<BookModel> GetAllBook()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                List<BookModel> books = new List<BookModel>();
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SPGetAllBooK", connection);

                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    SqlDataReader Reader = command.ExecuteReader();

                    if (Reader.HasRows)
                    {
                        while (Reader.Read())
                        {
                            BookModel book = new BookModel()
                            {
                                BookID = Reader.IsDBNull("BookID") ? 0 : Reader.GetInt32("BookID"),
                                BookName = Reader.IsDBNull("BookName") ? string.Empty : Reader.GetString("BookName"),
                                AuthorName = Reader.IsDBNull("AuthorName") ? string.Empty : Reader.GetString("AuthorName"),
                                Ratings = Reader.IsDBNull("Ratings") ? 0 : Reader.GetInt32("Ratings"),
                                No_Of_People_Rated = Reader.IsDBNull("No_of_People_Rated") ? 0 : Reader.GetInt64("No_of_People_Rated"),
                                Discount_Price = Reader.IsDBNull("Discount_Price") ? 0 : Reader.GetInt32("Discount_Price"),
                                Original_Price = Reader.IsDBNull("Original_Price") ? 0 : Reader.GetInt32("Original_Price"),
                                Book_Details = Reader.IsDBNull("Book_Detail") ? string.Empty : Reader.GetString("Book_Detail"),
                                Book_Image = Reader.IsDBNull("Book_Image") ? string.Empty : Reader.GetString("Book_Image"),
                                Book_Quantity = Reader.IsDBNull("Book_Quantity") ? 0 : Reader.GetInt32("Book_Quantity"),
                            };
                            books.Add(book);
                        }
                        return books;
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
        public BookModel GetBookByID(int BookID)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    BookModel book = new BookModel();
                    SqlCommand command = new SqlCommand("SPGetBooKByID", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@BookID", BookID);

                    connection.Open();
                    SqlDataReader Reader = command.ExecuteReader();

                    if (Reader.HasRows)
                    {
                        while (Reader.Read())
                        {

                            book.BookID = Reader.IsDBNull("BookID") ? 0 : Reader.GetInt32("BookID");
                            book.BookName = Reader.IsDBNull("BookName") ? string.Empty : Reader.GetString("BookName");
                            book.AuthorName = Reader.IsDBNull("AuthorName") ? string.Empty : Reader.GetString("AuthorName");
                            book.Ratings = Reader.IsDBNull("Ratings") ? 0 : Reader.GetInt32("Ratings");
                            book.No_Of_People_Rated = Reader.IsDBNull("No_of_People_Rated") ? 0 : Reader.GetInt64("No_of_People_Rated");
                            book.Discount_Price = Reader.IsDBNull("Discount_Price") ? 0 : Reader.GetInt32("Discount_Price");
                            book.Original_Price = Reader.IsDBNull("Original_Price") ? 0 : Reader.GetInt32("Original_Price");
                            book.Book_Details = Reader.IsDBNull("Book_Detail") ? string.Empty : Reader.GetString("Book_Detail");
                            book.Book_Image = Reader.IsDBNull("Book_Image") ? string.Empty : Reader.GetString("Book_Image");
                            book.Book_Quantity = Reader.IsDBNull("Book_Quantity") ? 0 : Reader.GetInt32("Book_Quantity");
                        }
                        return book;
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
