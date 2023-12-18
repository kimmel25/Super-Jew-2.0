using Super_Jew_2._0.Services;

namespace Super_Jew_2._0.Backend.Services
{
    public class ShulLoginService : ILoginService
    {
        private readonly ShulService _shulService;

        public ShulLoginService(ShulService shulService)
        {
            _shulService = shulService;
        }

        public Task<User> GetFollowedShulsForUser(string username, string password)
        {
            User user = _shulService.GetFollowedShulsForUser(username, password);
            return Task.FromResult(user);
        }
    }
}
