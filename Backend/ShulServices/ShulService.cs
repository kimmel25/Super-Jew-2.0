using Super_Jew_2._0.Backend.Database;

namespace Super_Jew_2._0.Backend.Services
{
    public class ShulService
    {

        public ShulService()//DataBaseConnectivity database)
        {
            //_database = database;
        }

        public static List<Shul> GetAllAvailableShuls() //in future add zipcode option
        {
            return DataBaseConnectivity.GetAvailableShuls();
            //return DummyData.DummyData.GetAllAvailableShuls();
        }


        public static User GetFollowedShulsForUser(string userId, string password)
        {
            return DataBaseConnectivity.GetUserByPassword(userId, password);
            //return DummyData.DummyData.GetUserByPassword(userId,password);
        }

        public static bool AddShulToUserProfile(int userId, int shulId)
        {
            return DataBaseConnectivity.AddShulToUser(userId, shulId);
        }

        public static bool RemoveShulFromUserProfile(int userId, int shulId)
        {
            //return _database.RemoveShulFromUser(userId, shulId);
            return false;
        }

    }
}