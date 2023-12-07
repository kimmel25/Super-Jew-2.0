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
            Shul[] allShulls = ShulService.GetAllAvailableShuls();
            foreach (var shul in allShulls)
            {
                Console.WriteLine(shul.ShulName + " " + shul.Location);
            }*/
            

            User user1 = ShulService.GetFollowedShulsForUser("john_doe", "password123");
            Console.WriteLine(user1.UserID);
            Console.WriteLine("Shuls For Username:" + user1.Username);
            
            List<Shul> user1Shuls = user1.FollowedShuls;
            
            foreach (var shul in user1Shuls)
            {
                Console.WriteLine(shul.ShulName + " " + shul.Location + " " + "Shacharis Time: " + shul.ShachrisTime);
            }
        
            /*
            
                try
                {
                    using (var connection = new MySqlConnection("server=ls-01387c56e2e850b1cdd03466bf968f269762e5fb.ccj5p9bk5hpi.us-east-1.rds.amazonaws.com;port=3306;database=SuperJewDataBase;user=dbmasteruser;password=SuperJewPassword613;"))
                    {
                        connection.Open();
                        Console.WriteLine("Connection successful.");
                        // Optionally, execute a simple command like 'SELECT 1'
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
                */

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddSingleton<WeatherForecastService>();
            builder.Services.AddTransient<Class>();

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