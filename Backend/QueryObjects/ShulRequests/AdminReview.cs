namespace Super_Jew_2._0.Backend.ShulRequests;

public class AdminReview
{
    public AdminReview()
    {
        Requests = new List<ShulRequest>();
    }

    public List<ShulRequest> Requests { get; set; }
}