using Super_Jew_2._0.Backend.Database;

namespace Super_Jew_2._0.Backend.Services
{
    public class ShulService
    {

        public ShulService()//DataBaseConnectivity database)
        {
            //_database = database;
        }

        public static Shul[] GetAllAvailableShuls() //in future add zipcode option
        {
            //return _database.GetAvailableShuls();
            return DummyData.DummyData.GetAllAvailableShuls();
        }


        public static User GetFollowedShulsForUser(string userId, string password)
        {
            return DataBaseConnectivity.GetUserByPassword(userId, password);
            //return DummyData.DummyData.GetUserByPassword(userId,password);
        }

        public static bool AddShulToUserProfile(int userId, int shulId)
        {
            //return Super_Jew_2._0.Backend.Database.DataBaseConnectivity.AddShulToUser(userId, shulId);
            return false;
        }

        public static bool RemoveShulFromUserProfile(int userId, int shulId)
        {
            //return _database.RemoveShulFromUser(userId, shulId);
            return false;
        }

    }
}