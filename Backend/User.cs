namespace Super_Jew_2._0.Backend;

public class User
{
    public int UserID { get; set; }
    public string Username { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string ReligiousDenomination { get; set; }
    public string AccountType { get; set; }
    public List<Shul> FollowedShuls { get; set; }
    

    //Add on a Shul to a users profile 
    public bool AddShul(string shulID)
    {
        


        return true;
    }
    

    
    //need method to get user shulls for front end. 
    //get Shulls
    
    //find user shull by id
}