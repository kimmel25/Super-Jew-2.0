
﻿namespace Super_Jew_2._0.Backend.DummyData
{
    public static class DummyData
    {
        public static List<Shul> GetAllAvailableShuls()
        {

            var shul1 = new Shul
            {
                ShulID = 00001,
                ShulName = "Beth Abraham",
                Location = "396 New Bridge Rd, Bergenfield, NJ 07621",
                Denomination = "Orthodox",
                ContactInfo = "Yechezkel Dinkinpinky, 201-345-2920",
                ShachrisTime = "7am, 8am, 845am, 915am",
                MinchaTime = "1:45pm, 3pm, 15 minutes before zman",
                MaarivTime = "Zman, 845pm, 10pm"
            };

            var shul2 = new Shul
            {
                ShulID = 00002,
                ShulName = "Ohr Hatorah",
                Location = "36 Rector Ct, Bergenfield, NJ 07621",
                Denomination = "Orthodox",
                ContactInfo = "Belongi Shwartzenstein, 201-238-1230",
                ShachrisTime = "730am, 830am, 930am",
                MinchaTime = "1:45pm, 2:45pm, 15 minutes before zman",
                MaarivTime = "Zman, 9pm, 10:15pm"
            };

            var shul3 = new Shul
            {
                ShulID = 00003,
                ShulName = "Beis Medrash Of Bergenfield",
                Location = "371 S Prospect Ave, Bergenfield, NJ 07621",
                Denomination = "Orthodox",
                ContactInfo = "Takkapaka Shpinnigger, 201-892-1497",
                ShachrisTime = "630am, 730am, 8am, 915am",
                MinchaTime = "1:30pm, 3pm, 15 minutes before zman",
                MaarivTime = "Zman, 8pm, 9pm"
            };

            var shul4 = new Shul
            {
                ShulID = 00004,
                ShulName = "Bnei Yeshuran",
                Location = "641 W Englewood Ave, Teaneck, NJ 07666 ",
                Denomination = "Orthodox",
                ContactInfo = "Yeerbankoop Tribalinko, 917-111-7821",
                ShachrisTime = "5:45am, 6:30am, 7:30am, 8:30am, 9:15am",
                MinchaTime = "1:45pm, ,2pm, 3pm, 15 minutes before zman",
                MaarivTime = "Zman, 8pm, 9pm, 10:01pm, 11pm"
            };


            List<Shul> allShuls = new List<Shul>();
            
            allShuls.Add(shul1);
            allShuls.Add(shul2);
            allShuls.Add(shul3);
            allShuls.Add(shul4);

            return allShuls;
        }

        public static User GetUserByPassword(string username, string password)
        {

            //Make regular user and gabbai

            List<Shul> allShuls = GetAllAvailableShuls();

            var user1 = new User
            {
                UserID = 00001,
                Username = "ykatz1",
                DateOfBirth = "2001-04-30",
                ReligiousDenomination = "Very Very Orthodox, bgeder Chareidi",
                AccountType = "User"
            };

            user1.FollowedShuls.Add(allShuls[0]);
            user1.FollowedShuls.Add(allShuls[1]);

            var user2 = new User
            {
                UserID = 00002,
                Username = "mGreenberg",
                DateOfBirth = "1995-05-20",
                ReligiousDenomination = "Modern Orthodox",
                AccountType = "User"
            };

            user2.FollowedShuls.Add(allShuls[0]);
            user2.FollowedShuls.Add(allShuls[3]);

            var user3 = new User
            {
                UserID = 00003,
                Username = "rLevi1990",
                DateOfBirth = "1990-10-12",
                ReligiousDenomination = "Chasidish, Breslov",
                AccountType = "User"
            };

            user3.FollowedShuls.Add(allShuls[2]);
            user3.FollowedShuls.Add(allShuls[3]);

            var user4 = new User
            {
                UserID = 00004,
                Username = "sGoldstein77",
                DateOfBirth = "1977-03-08",
                ReligiousDenomination = "Litvish",
                AccountType = "User"
            };

            user4.FollowedShuls.Add(allShuls[0]);
            user4.FollowedShuls.Add(allShuls[2]);

            var user5 = new User
            {
                UserID = 00005,
                Username = "dWeiss22",
                DateOfBirth = "1982-12-17",
                ReligiousDenomination = "Chasidish, Satmar",
                AccountType = "Gabai"
            };

            user4.FollowedShuls.Add(allShuls[0]);
            user4.FollowedShuls.Add(allShuls[1]);

            string[] userPasswords = { "yk123", "mgreen123", "rLevi123", "sGold123", "dWeiss123" };

            User[] users = { user1, user2, user3, user4, user5 };

            for (int i = 0; i < users.Length; i++)
            {
                if (users[i].Username == username && userPasswords[i] == password)
                {
                    return users[i];
                }
            }

            return user2;
        }

    }
}

