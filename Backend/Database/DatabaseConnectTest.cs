using MySql.Data.MySqlClient;
/*
try
{
    using (var connection = new MySqlConnection("server=ls-01387c56e2e850b1cdd03466bf968f269762e5fb.ccj5p9bk5hpi.us-east-1.rds.amazonaws.com;port=3306;database=SuperJewDataBase;user=dbmasteruser;password=SuperJewPassword613;"))
    {
        connection.Open();
        Console.WriteLine("Connection successful.");

        string query = @"
            SELECT Shuls.*
            FROM Shuls
            INNER JOIN FollowedShuls ON Shuls.ShulID = FollowedShuls.ShulID
            WHERE FollowedShuls.UserID = 1;
        ";

        using (var command = new MySqlCommand(query, connection))
        {
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    // Assuming the Shul table has these columns, adjust if necessary
                    Console.WriteLine($"Shul Name: {reader["Name"]}, Location: {reader["Location"]}");
                    // Add more columns as needed
                }
            }
        }
    }
                
    Console.WriteLine("out");
}
catch (MySqlException ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}

*/

Console.WriteLine("test");