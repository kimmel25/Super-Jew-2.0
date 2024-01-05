using Super_Jew_2._0.Backend.Services;
using Super_Jew_2._0.Backend.ShulServices;
using Super_Jew_2._0.Services;
using ShulLoginService = Super_Jew_2._0.Backend.Services.ShulLoginService;

namespace Super_Jew_2._0
{
    public class Program
    {
        public static void Main(string[] args)
        {

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