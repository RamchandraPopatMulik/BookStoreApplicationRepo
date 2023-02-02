using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static BookStoreRepository.Repository.AdminRepository;
using BookStoreRepository.Interface;

namespace BookStoreRepository.Repository
{
        public class AdminRepository : IAdminRepository
        {
            private readonly IConfiguration config;
            private string? connectionString;
            public AdminRepository(IConfiguration configuration, IConfiguration config)
            {
                connectionString = configuration.GetConnectionString("UserDBConnection");
                this.config = config;
            }
            public string GenerateJWTToken(string emailID, int AdminID)
            {
                try
                {
                    var loginSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.config[("Jwt:key")]));
                    var loginTokenDescripter = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                        new Claim(ClaimTypes.Role,"Admin"),
                        new Claim(ClaimTypes.Email, emailID),
                        new Claim("AdminID",AdminID.ToString())
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
            public string AdminLogin(string EmailID, string Password)
            {
                SqlConnection connection = new SqlConnection(connectionString);
                try
                {
                    int AdminID = 0;
                    using (connection)
                    {
                        SqlCommand command = new SqlCommand("SPAdminLogin", connection);

                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@EmailID", EmailID);
                        command.Parameters.AddWithValue("@Password", Password);

                        connection.Open();
                        SqlDataReader Reader = command.ExecuteReader();

                        if (Reader.HasRows)
                        {
                            while (Reader.Read())
                            {
                                AdminID = Reader.IsDBNull("AdminID") ? 0 : Reader.GetInt32("AdminID");
                            }
                            string token = GenerateJWTToken(EmailID, AdminID);
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
        }
    
}
