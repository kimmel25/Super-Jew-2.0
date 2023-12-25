//using Super_Jew_2._0.Backend.ShulRequests;

//namespace Super_Jew_2._0.Backend.DummyData;

//public class ShulRequestDummy
//{
//    private GabbaiRequests gabaiShulAdditionRequests;
//    private AdminReview adminShulAdditionRequests;

//    public ShulRequestDummy()
//    {
//        gabaiShulAdditionRequests = new GabbaiRequests();
//    }

//    public GabbaiRequests GetGabaiShulAdditionRequests()
//    {
//       return gabaiShulAdditionRequests;
//    }

//    public void InitiateGabaiShulAddition(int gabaiId, ShulRequest shulRequest)
//    {
//        shulRequest.ApprovalStatus = "Awaiting Review";

//        gabaiShulAdditionRequests.GabbaiID = gabaiId;
//        gabaiShulAdditionRequests.Requests.Add(shulRequest);

//        adminShulAdditionRequests.Requests.Add(shulRequest);
//    }

//    public bool ClearGabbaiShulAdditionStatus(int gabbaiId, int shulAdditionRequestId)
//    {
//        //int remove;
//        for (int i = 0; i < gabaiShulAdditionRequests.Requests.Count; i++)
//        {
//            if (shulAdditionRequestId == gabaiShulAdditionRequests.Requests[i].RequestID)
//            {
//                //remove = i;
//                gabaiShulAdditionRequests.Requests.RemoveAt(i);
//            }
//        }
//        return true;
//    }

//    //Methods for Admins Only!
//    public AdminReview GetGabbaiRequestsSubmissions()
//    {
//        return adminShulAdditionRequests;
//    }

//    public bool AdminShulSubmitionDecision(int shulAdditionRequestId, string decision)
//    {
//        for (int i = 0; i < adminShulAdditionRequests.Requests.Count; i++)
//        {
//            if (shulAdditionRequestId == adminShulAdditionRequests.Requests[i].RequestID)
//            {
//                //remove = i;
//                if (decision == "approved")
//                {
//                    var newShul = new Shul
//                    {
//                        ShulID = 11111111, //fake bc databse would generate random
//                        ShulName = adminShulAdditionRequests.Requests[i].ShulName,
//                        Location = adminShulAdditionRequests.Requests[i].Location,
//                        Denomination = adminShulAdditionRequests.Requests[i].Denomination,
//                        ContactInfo = adminShulAdditionRequests.Requests[i].ContactInfo,
//                        ShachrisTime = adminShulAdditionRequests.Requests[i].ShachrisTime,
//                        MinchaTime = adminShulAdditionRequests.Requests[i].MinchaTime,
//                        MaarivTime = adminShulAdditionRequests.Requests[i].MaarivTime
//                    };

//                    DummyData.AddVerifiedShul(newShul);
//                }

//                adminShulAdditionRequests.Requests[i].ApprovalStatus = decision;
//                gabaiShulAdditionRequests.Requests[i] = adminShulAdditionRequests.Requests[i];
//            }
//        }
//        return true;
//    }


//}
