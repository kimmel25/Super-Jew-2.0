using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MySql.Data.MySqlClient;
using Super_Jew_2._0.Backend;
using Super_Jew_2._0.Backend.Services;
using Super_Jew_2._0.Data;

namespace Super_Jew_2._0
{
    public class Program
    {  
        public static void Main(string[] args)
        {
            /*
            //Test Console dummy data
            
            Console.WriteLine("Test All Available Shuls");
            List<Shul> allShulls = ShulService.GetAllAvailableShuls();
            foreach (var shul in allShulls)
            {
                Console.WriteLine(shul.ShulName + " " + shul.Location);
            } */
            
            Console.WriteLine("Getting followed Shuls for user");
            User user1 = ShulService.GetFollowedShulsForUser("john_doe", "password123");
            Console.WriteLine(user1.UserID);
            Console.WriteLine("Shuls For Username:" + user1.Username);
            
            List<Shul> user1Shuls = user1.FollowedShuls;
            foreach (var shul in user1Shuls)
            {
                Console.WriteLine(shul.ShulName + " " + shul.Location + " " + "Shacharis Time: " + shul.ShachrisTime);
            }
            
            
            /*
            Console.WriteLine("Attempting to Remove Shul from User!");
            bool addShulToUserProfile = ShulService.RemoveShulFromUserProfile(1, 2);
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
            */
            
          
            
            //Blazor Code
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddSingleton<WeatherForecastService>();
            builder.Services.AddTransient<Class>();
           
            builder.Services.AddTransient<ShulService>(); 


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}