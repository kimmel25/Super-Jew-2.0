using Super_Jew_2._0.Backend.Database;
using Super_Jew_2._0.Backend.DummyData;
using Super_Jew_2._0.Backend.ShulRequests;


namespace Super_Jew_2._0.Backend.Services
{
    public class ShulService
    {
        private static DummyData.DummyData _dummyData;


        public ShulService()
        {
            _dummyData = new DummyData.DummyData();
        }

        public static List<Shul> GetAllAvailableShuls() //in future add zipcode option
        {
            //return DataBaseConnectivity.GetAvailableShuls();
            return _dummyData.GetAllAvailableShuls();
        }

        public static User GetFollowedShulsForUser(string userId, string password)
        {
            //return DataBaseConnectivity.GetUserByPassword(userId, password);
            return _dummyData.GetUserByPassword(userId, password);
        }

        public static bool AddShulToUserProfile(int userId, int shulId)
        {
            //return DataBaseConnectivity.AddShulToUser(userId, shulId);
            return _dummyData.AddShulToUserProfile(userId, shulId);
        }

        public static bool RemoveShulFromUserProfile(int userId, int shulId)
        {
            //return DataBaseConnectivity.RemoveShulFromUser(userId, shulId);
            return DummyData.DummyData.RemoveShulFromUserProfile(userId, shulId);
        }



        //Methods for Gabbai's Only!

        //query table by gabbai id
        public static GabbaiRequests GetGabaiShulAdditionRequests(int gabaiId)
        {
            return _dummyData.GetGabaiShulAdditionRequests();
        }


        //gabbai clicks on page to submit a request to add a shul
        //Param - ShulRequest - a request id, gabaiid, and shul info
        public static bool InitiateGabaiShulAddition(int gabaiId, ShulRequest shulRequest)
        {
            _dummyData.InitiateGabaiShulAddition(gabaiId, shulRequest);
            return true;
        }

        //This method is for a Gabai, where after he see's the the Admin's response to
        //his Shul addition request, he can delete that "Request" off his page. 
        public static bool ClearGabbaiShulAdditionStatus(int gabbaiId, int shulAdditionRequestId)
        {
            _dummyData.ClearGabbaiShulAdditionStatus(gabbaiId, shulAdditionRequestId);
            return true;
        }

        //Methods for Admins Only!
        public static AdminReview GetGabbaiRequestsSubmissions()
        {
            return _dummyData.GetGabbaiRequestsSubmissions();
        }

        public static bool AdminShulSubmitionDecision(int shulAdditionRequestId, string decision)
        {
            return _dummyData.AdminShulSubmitionDecision(shulAdditionRequestId, decision);
        }
    }
}