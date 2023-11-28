using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Super_Jew_2._0.Backend.Database
{
    public class DatabaseConnectivity
    {
        private string connectionString;

        public DatabaseConnectivity(string server, string database)
        {
            connectionString = "Server = {server}; Database = {database}; Trusted_Connection = True";
        }

        public SqlConnection ConnectToDatabase()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }

    }
}
