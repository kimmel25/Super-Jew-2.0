namespace Super_Jew_2._0.Backend.ShulServices
{
    public class UserService
    {
        public User CurrentUser { get; private set; }
        
        public string UserPassword { get; private set; }
        public event Action OnUserChanged;

        public void SetCurrentUser(User user)
        {
            CurrentUser = user;
            OnUserChanged?.Invoke();
        }
        
        public void SetPassword(string password)
        {
            UserPassword = password;
        }

        public void Logout()
        {
            CurrentUser = null;
            OnUserChanged?.Invoke();
        }
    }

}