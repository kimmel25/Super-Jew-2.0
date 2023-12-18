namespace Super_Jew_2._0.Backend;

public class AuthenticationService
{
    public User CurrentUser { get; private set; }

    public bool IsAuthenticated => CurrentUser != null;

    public void Login(User user)
    {
        CurrentUser = user; // Set the user when logged in
    }

    public void Logout()
    {
        CurrentUser = null; // Clear the user on logout
    }
}
