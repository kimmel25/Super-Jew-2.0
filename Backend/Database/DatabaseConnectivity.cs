﻿using System;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Ocsp;
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
                DateOfBirth = reader.GetString("DateOfBirth"),
                ReligiousDenomination = reader.GetString("ReligiousDenomination"),
                AccountType = reader.GetString("AccountType"),
                FollowedShuls = new List<Shul>() // Initialize the FollowedShuls list
            };

            if (!reader.IsDBNull(reader.GetOrdinal("ShulID")))
            {
                Shul shul = new Shul
                {
                    ShulID = reader.GetInt32("ShulID"),
                    ShulName = reader.GetString("ShulName"),
                    Location = reader.GetString("Location"),
                    Denomination = reader.GetString("Denomination"),
                    ContactInfo = reader.GetString("ContactInfo"),
                    ShachrisTime = reader.GetString("ShachrisTime"),
                    MinchaTime = reader.GetString("MinchaTime"),
                    MaarivTime = reader.GetString("MaarivTime"),
                    shulEvents = new List<ShulEvent>() // Initialize the shulEvents list
                };
                user.FollowedShuls.Add(shul);
            }
        }

        // Advance to the next result set
        if (reader.NextResult())
        {
            // Second result set: Events
            while (reader.Read())
            {
                // Assuming you have a way to find the right shul object in user.FollowedShuls
                // For example, by ShulID
                int shulId = reader.GetInt32("ShulID");
                Shul shul = user.FollowedShuls.FirstOrDefault(s => s.ShulID == shulId);
                if (shul != null)
                {
                    ShulEvent sEvent = new ShulEvent()
                    {
                        ShulID = shulId,
                        EventName = reader.GetString("EventName"),
                        TimeOfEvent = reader.GetString("TimeOfEvent"),
                        Location = reader.GetString("Location"),
                        Subscription = reader.GetString("Subscription"),
                        Description = reader.GetString("Description"),
                    };
                    shul.shulEvents.Add(sEvent);
                }
            }
        }

                return user;
            }
        }

        public static bool AddGabbaiToShul(int userId, int shulId)
        {
            using var connection = new MySqlConnection(ConnectionString);
            using (var command = new MySqlCommand("AddGabbaiToShul", connection))
            {
                connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("InputUserId", userId);
                command.Parameters.AddWithValue("InputShulId", shulId);

                var result = command.ExecuteNonQuery();
                return result > 0; // returns true if it affected at least one record
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
                        ShulName = reader.GetString("ShulName"),
                        Location = reader.GetString("Location"),
                        Denomination = reader.GetString("Denomination"),
                        ContactInfo = reader.GetString("ContactInfo"),
                        ShachrisTime = reader.GetString("ShachrisTime"),
                        MinchaTime = reader.GetString("MinchaTime"),
                        MaarivTime = reader.GetString("MaarivTime"),
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


        
        //Methods for Gabbais Only!
        
        //this should function similar to AddShulToUser. procedure is created.
        public static bool GabbaiAddShulRequest(int userId, ShulRequest shulRequest)
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
                command.Parameters.AddWithValue("TheGabbaiID", userId);

                var result = command.ExecuteNonQuery();
                return result > 0;

            }
        }
        
        public static List<Shul> GetGabbaiShuls(string userId)
        {
            List<Shul> gabbaiShuls = new List<Shul>();

            using var connection = new MySqlConnection(ConnectionString);
            using (var command = new MySqlCommand("GetGabbaiShuls", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("userId", userId);


                connection.Open();
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var shulToAdd = new Shul
                    {
                        ShulID = reader.GetInt32("ShulId"),
                        ShulName = reader.GetString("ShulName"),
                        Location = reader.GetString("Location"),
                        Denomination = reader.GetString("Denomination"),
                        ContactInfo = reader.GetString("ContactInfo"),
                        ShachrisTime = reader.GetString("ShachrisTime"),
                        MinchaTime = reader.GetString("MinchaTime"),
                        MaarivTime = reader.GetString("MaarivTime"),
                    };
                    
                    gabbaiShuls.Add(shulToAdd);
                }
            }
            
            return gabbaiShuls;
        }

        public static bool ClearGabbaiShulAdditionStatus(int requestID)
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

        
        public static List<ShulRequest> GetGabbaiRequestsForGabbai(int gabbaiID)
        {
            List<ShulRequest> shulRequests = new List<ShulRequest>();

            using var connection = new MySqlConnection(ConnectionString);
            using (var command = new MySqlCommand("GetGabbaiRequestsForGabbai", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("NewGabbaiID", gabbaiID);
                connection.Open();
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var pending = new ShulRequest
                    {
                        RequestID = reader.GetInt32("RequestID"),
                        ShulName = reader.GetString("ShulName"),
                        Location = reader.GetString("Location"),
                        Denomination = reader.GetString("Denomination"),
                        ContactInfo = reader.GetString("ContactInfo"),
                        ShachrisTime = reader.GetString("ShachrisTime"),
                        MinchaTime = reader.GetString("MinchaTime"),
                        MaarivTime = reader.GetString("MaarivTime"),
                        ApprovalStatus = reader.GetString("ApprovalStatus")
                    };

                    shulRequests.Add(pending);
                }
            }

            return shulRequests;
        }


        //Methods for Admins only
        public static List<ShulRequest> GetGabbaiRequestsForAdmin()
        {
            List<ShulRequest> shulRequests = new List<ShulRequest>();
            //AdminReview shulsToReview = new AdminReview();

            using var connection = new MySqlConnection(ConnectionString);
            using (var command = new MySqlCommand("GetGabbaiRequestsForAdmin", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var pending = new ShulRequest
                    {
                        RequestID = reader.GetInt32("RequestID"),
                        GabbaiID = reader.GetInt32("UserID"),
                        ShulName = reader.GetString("ShulName"),
                        Location = reader.GetString("Location"),
                        Denomination = reader.GetString("Denomination"),
                        ContactInfo = reader.GetString("ContactInfo"),
                        ShachrisTime = reader.GetString("ShachrisTime"),
                        MinchaTime = reader.GetString("MinchaTime"),
                        MaarivTime = reader.GetString("MaarivTime"),
                        ApprovalStatus = reader.GetString("ApprovalStatus")
                    };

                    shulRequests.Add(pending);
                }
            }

            return shulRequests;
        }

       


        public static void AdminDecisionOnShul(int requestID, string decision, ShulRequest request)
        {

            Shul shul = new Shul();
            using var connection = new MySqlConnection(ConnectionString);
            using (var command = new MySqlCommand("MakeAdminDecision", connection))
            {
                connection.Open();
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("NewRequestID", requestID);
                command.Parameters.AddWithValue("NewApprovalStatus", decision);



                if (decision == "Approved")
                {
                    using (var commandTwo = new MySqlCommand("GetGabbaiShulByRequestID", connection))
                    {
                        commandTwo.CommandType = CommandType.StoredProcedure;

                        commandTwo.Parameters.AddWithValue("inputRequestID", requestID);

                        using var reader = commandTwo.ExecuteReader();
                        while (reader.Read())
                        {
                            shul = new Shul
                            {
                                ShulName = reader.GetString("ShulName"),
                                Location = reader.GetString("Location"),
                                Denomination = reader.GetString("Denomination"),
                                ContactInfo = reader.GetString("ContactInfo"),
                                ShachrisTime = reader.GetString("ShachrisTime"),
                                MinchaTime = reader.GetString("MinchaTime"),
                                MaarivTime = reader.GetString("MaarivTime"),
                            };
                        }

                    }

                    AddShul(shul);

                    List<Shul> allShulsToCheck = GetAvailableShuls();
                    foreach (var eachShul in allShulsToCheck)
                    {
                        if (eachShul.ShulName == shul.ShulName)
                        {
                            AddGabbaiToShul(request.GabbaiID, eachShul.ShulID);
                            Console.WriteLine("REQUEST BY ADD GABAI: " + eachShul.ShulName + " " + request.GabbaiID);
                        }
                    }

                }

                //var result = command.ExecuteNonQuery();
                //return result > 0;

                command.ExecuteNonQuery();
            }

        }

        public static bool AddShul(Shul shul)
        {
            using var connection = new MySqlConnection(ConnectionString);
            using (var command = new MySqlCommand("AddShul", connection))
            {
                connection.Open();
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("inputShulID", shul.ShulID);
                command.Parameters.AddWithValue("inputName", shul.ShulName);
                command.Parameters.AddWithValue("inputLocation", shul.Location);
                command.Parameters.AddWithValue("inputDenomination", shul.Denomination);
                command.Parameters.AddWithValue("inputContactInfo", shul.ContactInfo);
                command.Parameters.AddWithValue("inputShachrisTime", shul.ShachrisTime);
                command.Parameters.AddWithValue("inputMinchaTime", shul.MinchaTime);
                command.Parameters.AddWithValue("inputMaarivTime", shul.MaarivTime);


                var result = command.ExecuteNonQuery();
                return result > 0;

            }
        }

        //public static Shul GetShulByID(int shulID)
        //{
        //    var shul = new Shul();
        //    using var connection = new MySqlConnection(ConnectionString);
        //    using (var command = new MySqlCommand("GetShulByID", connection))
        //    {
        //        command.CommandType = CommandType.StoredProcedure;
        //        command.Parameters.AddWithValue("inputShulID", shulID);

        //        connection.Open();
        //        using var reader = command.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            shul = new Shul
        //            {
        //                ShulID = reader.GetInt32("ShulID"),
        //                ShulName = reader.GetString("Name"),
        //                Location = reader.GetString("Location"),
        //                Denomination = reader.GetString("Denomination"),
        //                ContactInfo = reader.GetString("ContactInfo"),
        //                ShachrisTime = reader.GetString("ShachrisTime"),
        //                MinchaTime = reader.GetString("MinchaTime"),
        //                MaarivTime = reader.GetString("MaarivTime"),

        //            };

        //EVENTS
        public static bool CreateEventDB(ShulEvent sEvent)
        {
            //int shulID, string eventName, string timeOfEvent, string location, string subscription, string description
            
            using var connection = new MySqlConnection(ConnectionString);
            using (var command = new MySqlCommand("CreateEvent", connection))
            {
                connection.Open();
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("inputShulID", sEvent.ShulID);
                command.Parameters.AddWithValue("inputEventName", sEvent.EventName);
                command.Parameters.AddWithValue("inputTimeOfEvent", sEvent.TimeOfEvent);
                command.Parameters.AddWithValue("inputLocation", sEvent.Location);
                command.Parameters.AddWithValue("inputSubscription", sEvent.Subscription);
                command.Parameters.AddWithValue("inputDescription", sEvent.Description);

                var result = command.ExecuteNonQuery();
                return result > 0;
            }
        }


        //        }

        //        return shul;
        //    }
        //}

        /*
                            gabbaiShuls.Add(shulToAdd);
                        }
                    }

                    return gabbaiShuls;
                }
            }

                */
    }
}
