namespace Super_Jew_2._0.Backend.ShulRequests;

public class ShulRequest
{
    //Data for Specific request by Gabbai
    public int RequestID { get; set; }
    public int GabbaiID { get; set; } //the Gabbai that made this request
    public string ApprovalStatus { get; set; }

    public string ShulName { get; set; }
    public string Location { get; set; }
    public string Denomination { get; set; }
    public string ContactInfo { get; set; }

    public string ShachrisTime { get; set; }
    public string MinchaTime { get; set; }
    public string MaarivTime { get; set; }
}