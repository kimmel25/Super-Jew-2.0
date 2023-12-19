namespace Super_Jew_2._0.Backend.ShulServices
{
    public class UserService
    {
        public User CurrentUser { get; private set; }
        public event Action OnUserChanged;

        public void SetCurrentUser(User user)
        {
            CurrentUser = user;
            OnUserChanged?.Invoke();
        }

        public void Logout()
        {
            CurrentUser = null;
            OnUserChanged?.Invoke();
        }
    }

}