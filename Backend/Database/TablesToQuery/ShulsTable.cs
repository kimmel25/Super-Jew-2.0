using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super_Jew_2._0.Backend.Database
{
    public class ShulsTable : DatabaseTable
    {
        private SqlConnection connection;

        public ShulsTable(SqlConnection connection)
        {
            this.connection = connection;
        }

        public int GetShulID(int inputShulID)
        {
            string query = "SELECT ShulID FROM Shuls WHERE ShulID = @ShulID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ShulID", inputShulID);

            int shulID = 0;

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    shulID = reader.GetInt32(0);
                }
            }

            return shulID;
        }


        public string GetName(int shulID)
        {
            string query = "SELECT Name FROM Shuls WHERE ShulID = @ShulID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ShulID", shulID);

            string name = null;

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    name = reader.GetString(0);
                }
            }

            return name;
        }


        public string GetLocation(int shulID)
        {
            string query = "SELECT Location FROM Shuls WHERE ShulID = @ShulID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ShulID", shulID);

            string location = null;

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    location = reader.GetString(0);
                }
            }

            return location;
        }

        public string GetDenomination(int shulID)
        {
            string query = "SELECT Denomination FROM Shuls WHERE ShulID = @ShulID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ShulID", shulID);

            string denomination = null;

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    denomination = reader.GetString(0);
                }
            }

            return denomination;
        }

        public string ContactInfo(int shulID)
        {
            string query = "SELECT ContactInfo FROM Shuls WHERE ShulID = @ShulID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ShulID", shulID);

            string contactInfo = null;

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    contactInfo = reader.GetString(0);
                }
            }

            return contactInfo;
        }

        public DateTime GetShachrisTime(int shulID)
        {
            string query = "SELECT ShachrisTime FROM Shuls WHERE ShulID = @ShulID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ShulID", shulID);

            DateTime shachrisTime = default;

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    shachrisTime = reader.GetDateTime(0);
                }
            }

            return shachrisTime;
        }

        public DateTime GetMinchaTime(int shulID)
        {
            string query = "SELECT MinchaTime FROM Shuls WHERE ShulID = @ShulID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ShulID", shulID);

            DateTime minchaTime = default;

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    minchaTime = reader.GetDateTime(0);
                }
            }

            return minchaTime;
        }

        public DateTime GetMaarivTime(int shulID)
        {
            string query = "SELECT MaarivTime FROM Shuls WHERE ShulID = @ShulID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ShulID", shulID);

            DateTime maarivTime = default;

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    maarivTime = reader.GetDateTime(0);
                }
            }

            return maarivTime;
        }




    }
}
