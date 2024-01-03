using System.Security.Cryptography;
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
         * @param username: The username of the new Users account
         * @param password: Password the user sets when creating the account
         * This Method creats a new user account from scratch for a new user
         * @returns boolean: True for a proper update, false if nothing was returned from the database call which means there is an issue
         */
        public static bool CreateNewUserAccount(User user, string password)
        {
            var salt = PasswordHasher.GenerateSalt();
            var hashedPassword = PasswordHasher.HashPassword(password, salt);
            Console.WriteLine(hashedPassword);
            return DataBaseConnectivity.CreateNewUserAccount(user, hashedPassword, salt);

        }

        /**
         * @param username: The username of the Users account
         * @param password: Password to the users log in
         * Gets the user objct with all the users' followed shuls.
         * Essentially logging in to an existing account
         * @returns a user object mapped with information from the database results which return user info and the users shul info
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
        * @returns boolean: True for a proper update, false if nothing was returned from the database call which means there is an issue
        */

        public static bool AddShulToUserProfile(int userId, int shulId)
        {
            return DataBaseConnectivity.AddShulToUser(userId, shulId);
        }

        /**
       * @param userID: An int representing the users ID, generated in the database and held in the User object
       * @param shulID: An int representing the shuls ID, generated in the database and held in the Shul object
       * Updates the Database when a shul is removed from the users list of followed shuls
       * @returns boolean: True for a proper update, false if nothing was returned from the database call which means there is an issue
       */

        public static bool RemoveShulFromUserProfile(int userId, int shulId)
        {
            return DataBaseConnectivity.RemoveShulFromUser(userId, shulId);
            //return DummyData.DummyData.RemoveShulFromUserProfile(userId, shulId);
        }

        /**
         * @param userID: An int representing the users ID, generated in the database and held in the User object
         * Turns a regular user into a Gabbai and affords him all the functionality a Gabbai has
         * @returns boolean: True for a proper update, false if nothing was returned from the database call which means there is an issue
         */
        public static bool UpgradeUserToGabbai(int userId)
        {
            return DataBaseConnectivity.UpgradeUserToGabbai(userId);
        }

        //Methods for Gabbai's Only!

        //gabbai clicks on page to submit a request to add a shul
        //Param - ShulRequest - a request id, gabaiid, and shul info

        /**
         * @param: An int representing the gabbais ID, generated in the database and held in the User object
         * @param shulRequest: A ShulRequest object which represents a request made by a Gabbai to add a shul.
         * This Method updates the Database to hold all the most current requests until an Admin approves or denys them
         * @returns boolean: True for a proper update, false if nothing was returned from the database call which means there is an issue
         */

        public static bool InitiateGabaiShulAddition(int gabaiId, ShulRequest shulRequest)
        {
            //_dummyData.InitiateGabaiShulAddition(gabaiId, shulRequest);

            return DataBaseConnectivity.GabbaiAddShulRequest(gabaiId, shulRequest);
        }


        /**
         * @param: An int representing the gabbais ID, generated in the database and held in the User object
         * @returns a List of Shuls that Gabbai has control over. These are the shuls the Gabbai can edit.
         */

        public static List<Shul> GetGabbaisShuls(string userId) //TODO change from string to int
        {
            return DataBaseConnectivity.GetGabbaiShuls(userId);
            //return _dummyData.GetAllAvailableShuls();
        }


        //private static ShulRequestDummy _shulRequestDummy = new ShulRequestDummy();


        /**
         * @param: An int representing the ID of the request, generated in the database and held in the shulRequest object
         * After the admin aproves or denys the Gabbais request, the pending request is cleared.
         * @returns boolean: True for a proper update, false if nothing was returned from the database call which means there is an issue
         * 
         */
        public static async Task<bool> ClearGabbaiShulAdditionStatus(int requestId)
        {
            return DataBaseConnectivity.ClearGabbaiShulAdditionStatus(requestId);
        }





        /**
         * @param The Shul object that had its details updated by the Gabbai. Method sends it to the backend for the Database to update it.
         * @returns boolean: True for a proper update, false if nothing was returned from the database call which means there is an issue
         */

        public static bool UpdateShulDetails(Shul shulToUpdate)
        {
            return DataBaseConnectivity.UpdateShulDetails(shulToUpdate);
        }

        public static List<ShulRequest> GetGabbaiRequestsSubmissionsForGabbai(int gabbaiID)
        {
            return DataBaseConnectivity.GetGabbaiRequestsForGabbai(gabbaiID);
        }


        /**
         * Update Gabbai first checks to see if the shul has an existing Gabbai, if it does it updates it.
         * If not it creates a new row in the Gabbai table and links a shul to it
         */
        public static bool UpdateGabbai(int userId, int shulId)
        {
            return DataBaseConnectivity.UpdateShulGabbai(userId, shulId);

        }



        //Methods for Admins Only!

        //sends back all shul requests made by gabbai 
        public static List<ShulRequest> GetGabbaiRequestsSubmissionsForAdmin()
        {
            return DataBaseConnectivity.GetGabbaiRequestsForAdmin();
        }


        public static async Task AdminShulSubmitionDecision(int requestID, string decision, ShulRequest request)
        {
            DataBaseConnectivity.AdminDecisionOnShul(requestID, decision, request);
        }


        //METHODS FOR EVENTS
        public static ShulEvent GetEvent(int eventID)
        {
            return null;
        }



        public static bool CreateEvent(ShulEvent sEvent)
        {
            return DataBaseConnectivity.CreateEventDB(sEvent);
        }

        public static List<User> GetAllGabbais()
        {
            return DataBaseConnectivity.GetAllGabbais();
        }

        /**
         * @ sEventId: An int representing the event ID, generated in the database and held in the Event object
         * Deletes an event from the Database
         * @returns boolean: True for a proper update, false if nothing was returned from the database call which means there is an issue
         */
        public static bool DeleteEvent(int eventId)
        {
            return DataBaseConnectivity.DeleteEvent(eventId);
        }

        public static List<ShulEvent> GetEventsByShul(int shulId)
        {
            return DataBaseConnectivity.GetEventsByShul(shulId);
        }

        public static bool AddShul(Shul shul) //for admin
        {
            return DataBaseConnectivity.AddShul(shul);
        }

        public static bool RemoveShul(int shulID)
        {
            return DataBaseConnectivity.RemoveShul(shulID);
        }

    }
}