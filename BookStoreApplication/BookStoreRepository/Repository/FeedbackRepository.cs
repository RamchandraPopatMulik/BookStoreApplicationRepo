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
    public class FeedbackRepository : IFeedbackRepository
    {
        private string? connectionString;
        public FeedbackRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("UserDBConnection");
        }
        public FeedbackModel AddFeedback(FeedbackModel feedbackModel)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SPAddFeedback", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Ratings", feedbackModel.Rating);
                    command.Parameters.AddWithValue("@Comment", feedbackModel.Comment);
                    command.Parameters.AddWithValue("@BookID", feedbackModel.BookID);
                    command.Parameters.AddWithValue("@UserID", feedbackModel.UserID);

                    connection.Open();
                    int registerOrNot = command.ExecuteNonQuery();

                    if (registerOrNot >= 1)
                    {
                        return feedbackModel;
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
        public List<FeedbackModel> GetAllFeedback(int BookID)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                List<FeedbackModel> feedbacks = new List<FeedbackModel>();
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SPGetAllFeedback", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@BookID", BookID);


                    connection.Open();
                    SqlDataReader Reader = command.ExecuteReader();

                    if (Reader.HasRows)
                    {
                        while (Reader.Read())
                        {
                            FeedbackModel feedback = new FeedbackModel()
                            {
                                FeedbackID = Reader.IsDBNull("FeedbackID") ? 0 : Reader.GetInt32("FeedbackID"),
                                Rating = Reader.IsDBNull("Ratings") ? 0 : Reader.GetInt32("Ratings"),
                                Comment = Reader.IsDBNull("Comment") ? string.Empty : Reader.GetString("Comment"),
                                BookID = Reader.IsDBNull("BookID") ? 0 : Reader.GetInt32("BookID"),
                                UserID = Reader.IsDBNull("UserID") ? 0 : Reader.GetInt32("UserID"),

                            };
                            feedbacks.Add(feedback);
                        }
                        return feedbacks;
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
