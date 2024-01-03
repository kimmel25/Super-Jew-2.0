using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MySql.Data.MySqlClient;
using Super_Jew_2._0.Backend;
using Super_Jew_2._0.Backend.Database;
//using Super_Jew_2._0.Backend.DummyData;
using Super_Jew_2._0.Backend.Services;
using Super_Jew_2._0.Backend.ShulRequests;
using Super_Jew_2._0.Backend.ShulServices;
using Super_Jew_2._0.Data;
using Super_Jew_2._0.Services;
using ShulLoginService = Super_Jew_2._0.Backend.Services.ShulLoginService;

namespace Super_Jew_2._0
{
    public class Program
    {


        private static void testGetGabbaiRequests()
        {
            Console.WriteLine("TESTING GETTING GABBAI REQUESTS");
            var adminReview = ShulService.GetGabbaiRequestsSubmissionsForAdmin();


            Console.WriteLine("Shul Requests:");

            foreach (var shulRequest in adminReview)
            {
                Console.WriteLine($"RequestID: {shulRequest.RequestID}");
                Console.WriteLine($"GabbaiID: {shulRequest.GabbaiID}");
                Console.WriteLine($"ApprovalStatus: {shulRequest.ApprovalStatus}");
                Console.WriteLine($"ShulName: {shulRequest.ShulName}");
                Console.WriteLine($"Location: {shulRequest.Location}");
                Console.WriteLine($"Denomination: {shulRequest.Denomination}");
                Console.WriteLine($"ContactInfo: {shulRequest.ContactInfo}");
                Console.WriteLine($"ShachrisTime: {shulRequest.ShachrisTime}");
                Console.WriteLine($"MinchaTime: {shulRequest.MinchaTime}");
                Console.WriteLine($"MaarivTime: {shulRequest.MaarivTime}");
                Console.WriteLine();
            }

        }

        
        private static void AllAvailableShuls()
        {
            Console.WriteLine("Test All Available Shuls");
            List<Shul> allShulls = ShulService.GetAllAvailableShuls();
            foreach (var shul in allShulls)
            {
                Console.WriteLine(shul.ShulName + " " + shul.Location);
            }
        }

        //works
        private static void RemoveGabbaiPendingShul(int shulRequest)
        {
            Console.WriteLine("Testign ClearGabbaiAddedShul()");
            ShulService.ClearGabbaiShulAdditionStatus(shulRequest);
        }

        //works
        private static void GabbaiInittiateRequestTest(int requestID)
        {
            Console.WriteLine("Testing gabbai initiation");

            ShulRequest shulRequest = new ShulRequest();
            shulRequest.RequestID = requestID;
            shulRequest.GabbaiID = 2;
            shulRequest.ApprovalStatus = "Approved";
            shulRequest.ShulName = "Young israel statne island";
            shulRequest.Location = "staten island";
            shulRequest.Denomination = "Modern Orthodox";
            shulRequest.ContactInfo = "888-888-8888";
            shulRequest.ShachrisTime = "9:00, and 10";
            shulRequest.MinchaTime = "1pm ";
            shulRequest.MaarivTime = "10pm";

            ShulService.InitiateGabaiShulAddition(1, shulRequest);

        }

        private static void GetUserShuls(string username, string password)
        {
            Console.WriteLine("Getting followed Shuls for user");
            User? user1 = ShulService.GetFollowedShulsForUser(username, password);
            Console.WriteLine(user1.UserID);
            Console.WriteLine("Shuls For Username:" + user1.Username);

            List<Shul> user1Shuls = user1.FollowedShuls;
            foreach (var shul in user1Shuls)
            {
                Console.WriteLine(shul.ShulName + " " + shul.Location + " " + "Shacharis Time: " + shul.ShachrisTime);
            }
        }

        private static void addShulToUserProfile(string username, string password, int shulId)
        {
            User? user1 = ShulService.GetFollowedShulsForUser(username, password);
            List<Shul> user1Shuls = user1.FollowedShuls;


            Console.WriteLine("Attempting to Add Shul from User!");
            bool addShulToUserProfile = ShulService.AddShulToUserProfile(user1.UserID, shulId);
            Console.WriteLine("Added Shul To User Profile: " + addShulToUserProfile);

            //update user one
            user1 = ShulService.GetFollowedShulsForUser(username, password);
            Console.WriteLine(user1.UserID);
            Console.WriteLine("Shuls For Username:" + user1.Username);

            user1Shuls = user1.FollowedShuls;
            foreach (var shul in user1Shuls)
            {
                Console.WriteLine(shul.ShulName + " " + shul.Location + " " + "Shacharis Time: " + shul.ShachrisTime);
            }
        }

        private static void removeShulFromUserProfile(string username, string password, int shulId)
        {
            User? user1 = ShulService.GetFollowedShulsForUser(username, password);
            List<Shul> user1Shuls = user1.FollowedShuls;


            Console.WriteLine("Attempting to Remove Shul from User!");
            bool addShulToUserProfile = ShulService.RemoveShulFromUserProfile(user1.UserID, shulId);
            Console.WriteLine("Removed Shul To User Profile: " + addShulToUserProfile);

            //update user one
            user1 = ShulService.GetFollowedShulsForUser("john_doe", "password123");
            Console.WriteLine(user1.UserID);
            Console.WriteLine("Shuls For Username:" + user1.Username);

            user1Shuls = user1.FollowedShuls;
            foreach (var shul in user1Shuls)
            {
                Console.WriteLine(shul.ShulName + " " + shul.Location + " " + "Shacharis Time: " + shul.ShachrisTime);
            }
        }

        private static void requestShulSubmitionSimulation()
        {
            var shulRequest1 = new ShulRequest
            {
                RequestID = 00,
                GabbaiID = 1,
                ShulName = "Beis Moon",
                Location = "1 Street, Planet Moon",
                Denomination = "Orthochikenpic",
                ContactInfo = "Rena Kapinsky, 201-345-2920",
                ShachrisTime = "2am, 4am, 6am, 230pm",
                MinchaTime = "3:30pm, 6pm, 15 minutes after zman",
                MaarivTime = "15 minutes before zman, 430pm, 330pm"
            };



        }
        private static void runRequestSimulation()
        {
            GetUserShuls("ykatz1", "yk123");
            addShulToUserProfile("ykatz1", "yk123", 00003);
        }

        public static void Main(string[] args)
        {
            //Console.WriteLine(DataBaseConnectivity.UpdateShulGabbai(9, 42));


            Console.WriteLine("TESTING GABBAI SHULS FOR FRONT");
            List<Shul> g1Shuls = ShulService.GetGabbaisShuls("9");
            foreach (var shul in g1Shuls)
            {
                Console.WriteLine(shul.ShulName);
            }

            Console.WriteLine("COMMUNITY REMOVED: " + ShulService.RemoveShul(2));
            
            
            
            
         /*   
            Console.WriteLine("SHOWING GABBAIS:::");
            List<User> currentGabbais = ShulService.GetAllGabbais();
            foreach (var gabbai in currentGabbais)
            {
                Console.WriteLine(gabbai.Name);
            }
            
            Console.WriteLine("SHOWING shul:::");
            List<Shul> cShuls = ShulService.GetAllAvailableShuls();
            foreach (var shul in cShuls)
            {
                foreach (var eEvent in shul.shulEvents)
                {
                    Console.WriteLine(eEvent.EventName);
                }
            }

            Console.WriteLine("EVENTY TEST!");
            List<ShulEvent> bethAbeevents = ShulService.GetEventsByShul(1);
            foreach (var shulEvent in bethAbeevents)
            {
                Console.WriteLine(shulEvent.EventName);
            } */



            
            //ShulService.DeleteEvent(7);

            //ShulService.CreateNewUserAccount(testUser, "dinkyp");
            //User resultUser = ShulService.GetFollowedShulsForUser("dannyctest", "test123");
            /*
            AllAvailableShuls();
            GetUserShuls("john_doe", "password123");
            addShulToUserProfile("john_doe", "password123", 1);
            removeShulFromUserProfile("john_doe", "password123", 1);
            */

            //runRequestSimulation();

/*
            User u1 = ShulService.GetFollowedShulsForUser("john_doe", "password123");
            Console.WriteLine(u1.AccountType);
            
            
            User u2 = ShulService.GetFollowedShulsForUser("gabbai1", "gabbai_pass");
            Console.WriteLine(u2.AccountType); */

/*
            Console.WriteLine("gabbais shuls: ");
            List<Shul> gShuls = ShulService.GetGabbaisShuls("4");
            foreach (var shul in gShuls)
            {
                Console.WriteLine(shul.ShulName);
            } */
            
            //Shor Yoshuv,1 CedarLawn,Yeshivish kinda,"Rabbi Jaeger, (917)-223-Torah","5 am, 7:45 am",1:30 pm,"9:00 pm, 10:00 pm"
            
            /*
            var shul1 = new Shul
            {
                ShulID = 4,
                ShulName = "Shor Yoshuv",
                Location = "1 CedarLawn",
                Denomination = "Yeshivish kinda",
                ContactInfo = "Rabbi Jaeger, (917)-223-Torah",
                ShachrisTime = "5 am, 7:45 am",
                MinchaTime = "1:30 pm, 2:30pm",
                MaarivTime = "9:00 pm, 10:00 pm"
            };
            
            ShulService.UpdateShulDetails(shul1); */
            

            /*
            ShulService.InitiateGabaiShulAddition(4, shul1);
            
            List<ShulRequest> gabbaisSubmittedRequests = ShulService.GetGabaiShulAdditionRequests(3);
            foreach (var shulRequest in gabbaisSubmittedRequests)
            {
                Console.WriteLine(shulRequest.ShulName);
            } */

            /*
            Console.WriteLine("GABBI RTESR MAIN");
            List<ShulRequest> gabbaisSubmittedRequests2 = ShulService.GetGabbaiRequestsSubmissionsForAdmin();
            foreach (var shulRequest in gabbaisSubmittedRequests2)
            {
                Console.WriteLine(shulRequest.ShulName);
            } */

            /*
            Console.WriteLine("GABBI SUBMITS:");
            List<ShulRequest> gabbaiShulls1 = ShulService.GetGabbaiRequestsSubmissionsForGabbai(4);
            foreach (var shulRequest in gabbaiShulls1)
            {
                Console.WriteLine(shulRequest.ShulName);
            } */

            /*
            Console.WriteLine("dinkys: ");
            List<Shul> dinkysShuls = ShulService.GetGabbaisShuls("4");
            foreach (var shul in dinkysShuls)
            {
                Console.WriteLine(shul.ShulName);
            } */
/*
            User userToTestEvent = ShulService.GetFollowedShulsForUser("john_doe", "password123");

            List<Shul> userJohnsShuls = userToTestEvent.FollowedShuls;

            foreach (var shul in userJohnsShuls)
            {
                Console.WriteLine(shul.ShulName);
            }
            
            /*
            foreach (var shul in userJohnsShuls)
            {
                Console.WriteLine("Shul Name: " + shul.ShulName);
                Console.WriteLine("Events: ");
                foreach (var eShulEvent in shul.shulEvents)
                {
                    Console.WriteLine("Event Name: " + eShulEvent.EventName);
                }
            } */

            
            
            //DataBaseConnectivity.AddGabbaiToShul(4, 30);
            
            
            //Blazor Code
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();

            builder.Services.AddTransient<ShulService>();


            builder.Services.AddTransient<ILoginService, ShulLoginService>();

            builder.Services.AddScoped<UserService>();

            builder.Services.AddTransient<ShulService>();
            
            builder.Services.AddScoped<ShulStateService>();




            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseRouting();


            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}