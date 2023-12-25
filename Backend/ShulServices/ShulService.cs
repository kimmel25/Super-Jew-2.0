using Super_Jew_2._0.Backend.Database;
//using Super_Jew_2._0.Backend.DummyData;
using Super_Jew_2._0.Backend.ShulRequests;


namespace Super_Jew_2._0.Backend.Services
{
    
    /**
     * This class serves as the in between between the Blazor front end and the back end where the database is queried.
     * Separating the front and back in such a way allows any front end to be created which can simply call these methods to get to the backend.
     * It also allows for front end testing while backend operations are not complete (such as with dummy data or manual true or false returns)
     * 
     */
    public class ShulService
    {
    
        /**
         * Gets the user objct with all the users followed shul.
         * Essentially logging in
         * @param username: The username of the Users account
         * @param password: Password to the users log in
         * @Returns a user object mapped with information from the database results which return user info and the users shul info
         * If a user has no followed shuls, that field in FollowedShuls field in the User object will be null
         */

        public static User? GetFollowedShulsForUser(string username, string password)
        {

            Console.WriteLine("in shul login method");
            return DataBaseConnectivity.GetUserByPassword(username, password);
        }
        
        /**
         * Gets all the shuls which are available to be added to the users subscription
         * @returns a List of Shuls from the Database
         */
        
        public static List<Shul> GetAllAvailableShuls() //TODO in future add zipcode option
        {
            return DataBaseConnectivity.GetAvailableShuls();
            //return _dummyData.GetAllAvailableShuls();
        }

        /**
        * @param userID: An int representing the users ID, generated in the database and held in the User object
        * @param shulID: An int representing the shuls ID, generated in the database and held in the Shul object
        * Updates the Database when a shul is added to the users list of followed shuls
        * @returns boolean: True for a proper update, false if some sort is issue occured.
        */
        
        public static bool AddShulToUserProfile(int userId, int shulId)
        {
            return DataBaseConnectivity.AddShulToUser(userId, shulId);
        }
        
        /**
       * @param userID: An int representing the users ID, generated in the database and held in the User object
       * @param shulID: An int representing the shuls ID, generated in the database and held in the Shul object
       * Updates the Database when a shul is removed from the users list of followed shuls
       * @returns boolean: True for a proper update, false if some sort is issue occured.
       */

        public static bool RemoveShulFromUserProfile(int userId, int shulId)
        {
            return DataBaseConnectivity.RemoveShulFromUser(userId, shulId);
            //return DummyData.DummyData.RemoveShulFromUserProfile(userId, shulId);
        }
        
        //Methods for Gabbai's Only!
        
        //gabbai clicks on page to submit a request to add a shul
        //Param - ShulRequest - a request id, gabaiid, and shul info
        
        /**
         * @param: An int representing the gabbais ID, generated in the database and held in the User object
         * @param shulRequest: A ShulRequest object which represents a request made by a Gabbai to add a shul.
         * This Method updates the Database to hold all the most current requests until an Admin approves or denys them
         * @returns boolean: True for a proper update, false if some sort is issue occured.
         */
        
        public static bool InitiateGabaiShulAddition(int gabaiId, ShulRequest shulRequest) //TODO whats going on with Gabbai ID??
        {
            //_dummyData.InitiateGabaiShulAddition(gabaiId, shulRequest);
            
            return DataBaseConnectivity.GabbaiAddShulRequest(shulRequest);
        }

        
        /**
         * @param: An int representing the gabbais ID, generated in the database and held in the User object
         * @returns a List of Shuls that Gabbai has control over. These are the shuls the Gabbai can edit.
         */
        
        public static List<Shul> GetGabbaisShuls(string userId) 
        {
            return DataBaseConnectivity.GetGabbaiShuls(userId);
            //return _dummyData.GetAllAvailableShuls();
        }
        
        
        //private static ShulRequestDummy _shulRequestDummy = new ShulRequestDummy();


        /**
         * @param: An int representing the ID of the request, generated in the database and held in the shulRequest object
         * After the admin aproves or denys the Gabbais request, the pending request is cleared.
         * @returns boolean: True for a proper update, false if some sort is issue occured.
         * 
         */
        public static bool ClearGabbaiShulAdditionStatus(int requestId)
        {
            return DataBaseConnectivity.ClearGabbaiShulAdditionStatus(requestId);
        }
        
        

        //TODO query table by gabbai id
        public static GabbaiRequests GetGabaiShulAdditionRequests(int gabaiId)
        {
            return null;
        }
       
        /**
         * @param The Shul object that had its details updated by the Gabbai. Method sends it to the backend for the Database to update it.
         * @returns boolean: True for a proper update, false if some sort is issue occured.
         */

        public static bool UpdateShulDetails(Shul shulToUpdate)
        {
            return DataBaseConnectivity.UpdateShulDetails(shulToUpdate);
        }
        
        public static List<ShulRequest> GetGabbaiRequestsSubmissionsForGabbai(int gabbaiID)
        {
            return DataBaseConnectivity.GetGabbaiRequestsForGabbai(gabbaiID);
        }

        //Methods for Admins Only!

        //sends back all shul requests made by gabbai 
        public static AdminReview GetGabbaiRequestsSubmissionsForAdmin()
        {
            return DataBaseConnectivity.GetGabbaiRequestsForAdmin();
        }
        

        public static void AdminShulSubmitionDecision(int requestID, string decision)
        {
            DataBaseConnectivity.AdminDecisionOnShul(requestID, decision);
        }
    }
}