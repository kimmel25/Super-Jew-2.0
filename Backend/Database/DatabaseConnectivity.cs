﻿using System;
using System.Data;
using MySql.Data.MySqlClient;
using Super_Jew_2._0.Backend.ShulRequests;

namespace Super_Jew_2._0.Backend.Database
{
    public static class DataBaseConnectivity
    {
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

                    Shul? shul = new Shul
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


                    if (shul != null)
                    {
                        user.FollowedShuls.Add(shul);
                    }
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
                connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("inputUserId", userId);
                command.Parameters.AddWithValue("inputShulId", shulId);

                var result = command.ExecuteNonQuery();
                return result > 0;
            }
        }

        //Todo take out shul on front end also, look into how to refresh
        public static bool RemoveShulFromUser(int userId, int shulId)
        {
            using var connection = new MySqlConnection(ConnectionString);
            using (var command = new MySqlCommand("RemoveShulFromUser", connection))
            {
                connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("inputUserId", userId);
                command.Parameters.AddWithValue("inputShulId", shulId);

                var result = command.ExecuteNonQuery();
                return result > 0; // returns true if it affected at least one record
            }
        }


        //this should function similar to AddShulToUser. procedure is created. Needs to be tested
        public static bool GetInitiatedGabbaiShul(ShulRequest shulRequest)
        {
            using var connection = new MySqlConnection(ConnectionString);
            using (var command = new MySqlCommand("GetInitiatedGabbaiShul", connection))
            {
                connection.Open();
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("NewRequestID", shulRequest.RequestID);
                command.Parameters.AddWithValue("NewName", shulRequest.ShulName);
                command.Parameters.AddWithValue("NewLocation", shulRequest.Location);
                command.Parameters.AddWithValue("NewDenomination", shulRequest.Denomination);
                command.Parameters.AddWithValue("NewContactInfo", shulRequest.ContactInfo);
                command.Parameters.AddWithValue("NewShachrisTime", shulRequest.ShachrisTime);
                command.Parameters.AddWithValue("NewMinchaTime", shulRequest.MinchaTime);
                command.Parameters.AddWithValue("NewMaarivTime", shulRequest.MaarivTime);

                var result = command.ExecuteNonQuery();
                return result > 0;

            }
        }

        public static bool ClearGabbaiAddedShul(int requestID)
        {
            using var connection = new MySqlConnection(ConnectionString);
            using (var command = new MySqlCommand("ClearGabbaiAddedShul", connection))
            {
                connection.Open();
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("inputRequestID", requestID);

                var result = command.ExecuteNonQuery();
                return result > 0;

            }
        }
        
        /**
         * Takes a Shul object and sends all of its fields that have just been updated by the Gabbai to the dataBase to
         * update that shul in the Database
         * @return bool true is successful 
         */

        public static bool UpdateShulDetails(Shul shulToUpdate)
        {
            using var connection = new MySqlConnection(ConnectionString);
            using (var command = new MySqlCommand("UpdateShulDetails", connection))
            {
                connection.Open();
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("_ShulID", shulToUpdate.ShulID);
                command.Parameters.AddWithValue("UpdatedName", shulToUpdate.ShulName);
                command.Parameters.AddWithValue("UpdatedLocation", shulToUpdate.Location);
                command.Parameters.AddWithValue("UpdatedDenomination", shulToUpdate.Denomination);
                command.Parameters.AddWithValue("UpdatedContactInfo", shulToUpdate.ContactInfo);
                command.Parameters.AddWithValue("UpdatedShachrisTime", shulToUpdate.ShachrisTime);
                command.Parameters.AddWithValue("UpdatedMinchaTime", shulToUpdate.MinchaTime);
                command.Parameters.AddWithValue("UpdatedMaarivTime", shulToUpdate.MaarivTime);


                var result = command.ExecuteNonQuery();
                return result > 0;

            }
        }
    }
}
