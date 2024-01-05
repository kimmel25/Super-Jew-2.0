namespace Super_Jew_2._0.Backend;

public class Shul
{
    public Shul()
    {
        shulEvents = new List<ShulEvent>();
    }
    
    public int ShulID { get; set; }
    public string ShulName { get; set; }
    public string Location { get; set; }
    public string Denomination { get; set; }
    public string ContactInfo { get; set; }
    
    public string ShachrisTime { get; set; }
    public string MinchaTime { get; set; }
    public string MaarivTime { get; set; }

    public List<ShulEvent> shulEvents { get; set; }
} 