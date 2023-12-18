using Super_Jew_2._0.Backend;

namespace Super_Jew_2._0.Services
{
    public interface ILoginService
    {
        Task<User> GetFollowedShulsForUser(string username, string password);
    }
}
