﻿namespace WebApiM1Sopra2025_26.Models
{
    public class UserModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public string Role { get; set; }
        public string Surname { get; set; }
        public string GivenName { get; set; }
    }


    public class UserLogin
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class UserConstants
    {
        public static List<UserModel> Users = new List<UserModel>()
        {
            new UserModel() { Username = "jason_admin", EmailAddress = "jason.admin@email.com", Password = "MyPass_w0rd", GivenName = "Jason", Surname = "Bryant", Role = "Administrator" },
            new UserModel() { Username = "elyse_seller", EmailAddress = "elyse.seller@email.com", Password = "MyPass_w0rd", GivenName = "Elyse", Surname = "Lambert", Role = "Seller" },
        };
    }

}
