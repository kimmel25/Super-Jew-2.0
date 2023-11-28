using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super_Jew_2._0.Backend.Database
{
    //The info for this table is accessed by the username. Username can be accessed with ID
    public class UsersTable : DatabaseTable
    {
        private SqlConnection connection;

        public UsersTable(SqlConnection connection)
        {
            this.connection = connection;
        }

        public int GetUserID(string username)
        {
            string query = "SELECT UserID FROM Users WHERE Username = @Username";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Username", username);

            int userID = 0;

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    userID = reader.GetInt32(0);
                }
            }

            return userID;
        }

        public string GetUsername(int userID)
        {
            string query = "SELECT Username FROM Users WHERE UserID = @UserID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", userID);

            string userName = null;

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    userName = reader.GetString(0);
                }
            }

            return userName;
        }

        public string GetPassword(string username)
        {
            string query = "SELECT Password FROM Users WHERE Username = @Username";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Username", username);

            string password = null;

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    password = reader.GetString(0);
                }
            }

            return password;
        }

        public DateTime GetDateOfBirth(string username)
        {
            string query = "SELECT DateOfBirth FROM Users WHERE Username = @Username";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Username", username);

            DateTime dateOfBirth = default;

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    dateOfBirth = reader.GetDateTime(0);
                }
            }

            return dateOfBirth;
        }

        public string GetReligiousDenomination(string username)
        {
            string query = "SELECT ReligiousDenomination FROM Users WHERE Username = @Username";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Username", username);

            string religiousDenomination = null;

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    religiousDenomination = reader.GetString(0);
                }
            }

            return religiousDenomination;
        }

        public string GetAccountType(string username)
        {
            string query = "SELECT AccountType FROM Users WHERE Username = @Username";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Username", username);

            string accountType = null;

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    accountType = reader.GetString(0);
                }
            }

            return accountType;
        }
    }
}
