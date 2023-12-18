using Super_Jew_2._0.Services;

namespace Super_Jew_2._0.Backend.Services
{
    public class ShulLoginService : ILoginService
    {
        
        public User GetFollowedShulsForUser(string username, string password)
        {
            User user = ShulService.GetFollowedShulsForUser(username, password);
            return user;
        }
    }
}
