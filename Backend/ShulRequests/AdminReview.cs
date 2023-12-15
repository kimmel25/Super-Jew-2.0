namespace Super_Jew_2._0.Backend.ShulRequests;

public class AdminReview
{
    public AdminReview()
    {
        Requests = new List<ShulRequest>();
    }
    
    //All the requests made by Gabbai's in need of Admin Review
    public string AdminID { get; set; }
    public List<ShulRequest> Requests { get; set; }
}