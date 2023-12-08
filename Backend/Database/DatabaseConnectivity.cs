using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace Super_Jew_2._0.Backend.Database
{
    public static class DataBaseConnectivity
    {
        //static string _connectionString;

        private static readonly string ConnectionString =
            "server=ls-01387c56e2e850b1cdd03466bf968f269762e5fb.ccj5p9bk5hpi.us-east-1.rds.amazonaws.com;port=3306;database=SuperJewDataBase;user=dbmasteruser;password=SuperJewPassword613;Allow Zero Datetime=True";
        
        public static User? GetUserByPassword(string username, string password)
        {
            User? user = null;

            using var connection = new MySqlConnection(ConnectionString);
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
                        //Name = reader.GetString("Name"),TODO add to database name
                        DateOfBirth = reader.GetString("DateOfBirth"),
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
                        //ShachrisTime = reader["ShachrisTime"] != DBNull.Value ? reader.GetDateTime("ShachrisTime").ToString("HH:mm:ss") : null,
                        //MinchaTime = reader["MinchaTime"] != DBNull.Value ? reader.GetDateTime("MinchaTime").ToString("HH:mm:ss") : null,
                        //MaarivTime = reader["MaarivTime"] != DBNull.Value ? reader.GetDateTime("MaarivTime").ToString("HH:mm:ss") : null
                    };


                    user.FollowedShuls.Add(shul);
                }

                return user;
            }
        }
        
        public static List<Shul> GetAvailableShuls() //string zipCode in future
        {
            List<Shul> availableShuls = new List<Shul>();

            using var connection = new MySqlConnection(ConnectionString);
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
        
        public static bool AddShulToUser(int userId, int shulId)
        {
            using var connection = new MySqlConnection(ConnectionString);
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
        public static bool RemoveShulFromUser(int userId, int shulId)
        {
            using var connection = new MySqlConnection(ConnectionString);
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
