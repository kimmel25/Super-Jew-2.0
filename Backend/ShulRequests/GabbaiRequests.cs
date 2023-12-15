namespace Super_Jew_2._0.Backend.ShulRequests;

public class GabbaiRequests
{
    
    public GabbaiRequests()
    {
        Requests = new List<ShulRequest>();
    }
    
    //All the requests made by the Gabbai
    public string GabbaiID { get; set; }
    public List<ShulRequest> Requests { get; set; }
}