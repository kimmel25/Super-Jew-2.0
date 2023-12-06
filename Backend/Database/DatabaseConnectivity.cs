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
        string _connectionString;
        DataBaseConnectivity()
        {
            var databaseConnectionString = new ConfigurationBuilder()
           .AddUserSecrets<string>(true)
           .Build()["DatabaseConnectionString"];
            _connectionString = "server=localhost;port=3306;database=myDatabase;user=root;password=myPassword;";
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
        
        public List<Shul> GetAvailableShuls() //string zipCode in future
        {
            List<Shul> availableShuls = new List<Shul>();

            using var connection = new MySqlConnection(_connectionString);
            using (var command = new MySqlCommand("GetAvailableShuls", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                //command.Parameters.AddWithValue("zipCode", zipCode);

                connection.Open();
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
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

                    availableShuls.Add(shul);
                }

                return availableShuls;
            }
        }
        
        public bool AddShulToUser(int userId, int shulId)
        {
            using var connection = new MySqlConnection(_connectionString);
            using (var command = new MySqlCommand("AddShulToUser", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("userId", userId);
                command.Parameters.AddWithValue("shulId", shulId);

                connection.Open();
                var result = command.ExecuteNonQuery();
                return result > 0; // returns true if it affected at least one record
            }
        }
        
        //Todo take out shull on front end also???? or refresh user ?
        public bool RemoveShulFromUser(int userId, int shulId)
        {
            using var connection = new MySqlConnection(_connectionString);
            using (var command = new MySqlCommand("RemoveShulFromUser", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("userId", userId);
                command.Parameters.AddWithValue("shulId", shulId);

                connection.Open();
                var result = command.ExecuteNonQuery();
                return result > 0; // returns true if it affected at least one record
            }
        }

    }
}
