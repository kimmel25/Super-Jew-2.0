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
            return DataBaseConnectivity.GetAvailableShuls();
            //return _dummyData.GetAllAvailableShuls();
        }

        public static User GetFollowedShulsForUser(string userId, string password)
        {

            Console.WriteLine("in shul login method");
            return DataBaseConnectivity.GetUserByPassword(userId, password);
            //return DummyData.DummyData.GetUserByPassword(userId,password);

        }

        public static bool AddShulToUserProfile(int userId, int shulId)
        {
            return DataBaseConnectivity.AddShulToUser(userId, shulId);

            //return _dummyData.AddShulToUserProfile(userId, shulId);

            //return DummyData.DummyData.AddShulToUserProfile(userId, shulId);

        }

        public static bool RemoveShulFromUserProfile(int userId, int shulId)
        {
            return DataBaseConnectivity.RemoveShulFromUser(userId, shulId);
            //return DummyData.DummyData.RemoveShulFromUserProfile(userId, shulId);
        }



        //Methods for Gabbai's Only!

        //query table by gabbai id
        public static GabbaiRequests GetGabaiShulAdditionRequests(int gabaiId)
        {

            return null;
        }


        //gabbai clicks on page to submit a request to add a shul
        //Param - ShulRequest - a request id, gabaiid, and shul info
        // !! tested and works !!
        public static bool InitiateGabaiShulAddition(int gabaiId, ShulRequest shulRequest)
        {
            //_dummyData.InitiateGabaiShulAddition(gabaiId, shulRequest);
            DataBaseConnectivity.GetInitiatedGabbaiShul(shulRequest);

            return _shulRequestDummy.GetGabaiShulAdditionRequests();
        }

        private static ShulRequestDummy _shulRequestDummy = new ShulRequestDummy();
        
        public static bool InitiateGabaiShulAddition(int gabaiId ,ShulRequest shulRequest)
        {
            _shulRequestDummy.InitiateGabaiShulAddition(gabaiId, shulRequest);

            return true;
        }

        //This method is for a Gabai, where after he see's the the Admin's response to
        //his Shul addition request, he can delete that "Request" off his page. 
        // !! tested and works. removes row from database !!
        public static bool ClearGabbaiShulAdditionStatus(int requestID)
        {

            DataBaseConnectivity.ClearGabbaiAddedShul(requestID);


            return true;
        }

        //Methods for Admins Only!

        //sends back all shul requests made by gabbai 
        public static AdminReview GetGabbaiRequestsSubmissionsForAdmin()
        {
            return DataBaseConnectivity.GetGabbaiRequestsForAdmin();
        }

        //sends back all shul requests for a specified gabbai
        public static List<ShulRequest> GetGabbaiRequestsSubmissionsForGabbai(int gabbaiID)
        {

            return DataBaseConnectivity.GetGabbaiRequests();

        }

        public static bool AdminShulSubmitionDecision(int shulAdditionRequestId, string decision)
        {

        }
    }
}