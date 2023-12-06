using Super_Jew_2._0.Backend.Database;

namespace Super_Jew_2._0.Backend.Services
{
    public class ShulService
    {
        private readonly DataBaseConnectivity _database;

        public ShulService(DataBaseConnectivity database)
        {
            _database = database;
        }
        
        public List<Shul> GetAllAvailableShuls() //in future add zipcode option
        {
            return _database.GetAvailableShuls();
        }
        
        
        public User GetFollowedShulsForUser(string userId, string password)
        {
            return _database.GetUserByPassword(userId, password);
        }
        

        public bool AddShulToUserProfile(int userId, int shulId)
        {
            return _database.AddShulToUser(userId, shulId);
        }

    }
}