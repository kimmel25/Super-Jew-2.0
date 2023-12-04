using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace Super_Jew_2._0.Backend.Database
{
    public class DataBaseConnectivity
    {

        DataBaseConnectivity()
        {
            var databaseConnectionString = new ConfigurationBuilder()
           .AddUserSecrets<string>(true)
           .Build()["DatabaseConnectionString"];
            string _connectionString = "server=localhost;port=3306;database=myDatabase;user=root;password=myPassword;";

        }


        // Set your database connection string here



        public User? GetUserByPassword(string username, string password)
        {
            User? user = null;

            using var connection = new MySqlConnection(_connectionString);
            using (var command = new MySqlCommand("GetUserByPassword", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("inputUsername", username);
                command.Parameters.AddWithValue("inputPassword", password);

                connection.Open();
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    user ??= new User
                    {
                        UserID = reader.GetInt32("UserID"),
                        Username = reader.GetString("Username"),
                        DateOfBirth = reader.GetDateTime("DateOfBirth"),
                        ReligiousDenomination = reader.GetString("ReligiousDenomination"),
                        AccountType = reader.GetString("AccountType")
                    };

                    var shul = new Shul
                    {
                        ShulID = reader.GetInt32("ShulID"),
                        ShulName = reader.GetString("Name"),
                        Location = reader.GetString("Location"),
                        Denomination = reader.GetString("Denomination"),
                        ContactInfo = reader.GetString("ContactInfo"),
                        ShachrisTime = reader.GetString("ShachrisTime"),
                        MinchaTime = reader.GetString("MinchaTime"),
                        MaarivTime = reader.GetString("MaarivTime")
                    };

                    user.FollowedShuls.Add(shul);
                }

                return user;
            }
        }
    }
}
