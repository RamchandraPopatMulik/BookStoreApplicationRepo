using BookStoreModel;
using BookStoreRepository.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRepository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration config;
        private string connectionString;
        public UserRepository(IConfiguration configuration, IConfiguration config)
        {
            connectionString = configuration.GetConnectionString("UserDBConnection");
            this.config = config;
        }
        public static string EncryptPassword(string password)
        {
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(encode);
        }
        public string GenerateJWTToken(string emailID, int UserID)
        {
            try
            {
                var loginSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.config[("Jwt:key")]));
                var loginTokenDescripter = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Role,"User"),
                        new Claim(ClaimTypes.Email, emailID),
                        new Claim("UserID",UserID.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(5),
                    SigningCredentials = new SigningCredentials(loginSecurityKey, SecurityAlgorithms.HmacSha256Signature)
                };
                var token = new JwtSecurityTokenHandler().CreateToken(loginTokenDescripter);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public UserSignUpModel SignUp(UserSignUpModel userSignUp)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SPSignUpUser", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FullName", userSignUp.FullName);
                    command.Parameters.AddWithValue("@EmailID", userSignUp.EmailID);
                    command.Parameters.AddWithValue("@MobileNumber", userSignUp.MobileNumber);
                    command.Parameters.AddWithValue("@Password", EncryptPassword(userSignUp.Password));

                    connection.Open();
                    int registerOrNot = command.ExecuteNonQuery();

                    if (registerOrNot >= 1)
                    {
                        return userSignUp;
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
        public string Login(string EmailID, string Password)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                int UserID = 0;
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SPLoginUser", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmailID", EmailID);
                    command.Parameters.AddWithValue("@Password", EncryptPassword(Password));

                    connection.Open();
                    SqlDataReader Reader = command.ExecuteReader();

                    if (Reader.HasRows)
                    {
                        while (Reader.Read())
                        {
                            UserID = Reader.IsDBNull("UserID") ? 0 : Reader.GetInt32("UserID");
                        }
                        string token = GenerateJWTToken(EmailID, UserID);
                        return token;
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
        public string ForgotPassword(string emailID)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                UserSignUpModel userSignUp = new UserSignUpModel();

                using (connection)
                {
                    SqlCommand command = new SqlCommand("SPForgotPassword", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmailID", emailID);

                    connection.Open();
                    SqlDataReader Reader = command.ExecuteReader();

                    if (Reader.HasRows)
                    {
                        while (Reader.Read())
                        {
                            userSignUp.UserID = Reader.IsDBNull("UserID") ? 0 : Reader.GetInt32("UserID");
                            userSignUp.FullName = Reader.IsDBNull("FullName") ? string.Empty : Reader.GetString("FullName");
                        }
                        string token = GenerateJWTToken(emailID, userSignUp.UserID);
                        MSMQModel mSMQModel = new MSMQModel();
                        mSMQModel.SendMessage(token, emailID, userSignUp.FullName);
                        return token.ToString();
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
        public bool ResetPassword(string Password, string emailID)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SPResetPassword", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmailID", emailID);
                    command.Parameters.AddWithValue("@Password", EncryptPassword(Password));
                    connection.Open();
                    int resetOrNot = command.ExecuteNonQuery();

                    if (resetOrNot >= 1)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
