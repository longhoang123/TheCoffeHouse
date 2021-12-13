using System;

namespace TheCoffeHouse.Services.ApiService
{
    public static class ApiUrl
    {
        public static string BaseUrl = "http://192.168.1.17/thecoffehouseapi/api/";
        public static string Link(string endpoint)
        {
            return $"{BaseUrl}{endpoint}";
        }
        public static string GetPosts()
        {
            return Link($"PromoPostController/GetAllPosts");
        }

        public static string GetPost(string postID)
        {
            return Link($"PromoPostController/?postID={postID}");
        }

        public static string ValidateUser(string phone, string password)
        {
            return Link($"UserProfileController/ValidateUser/?phone={phone}&password={password}");
        }
        public static string RegisterUser()
        {
            return Link($"/UserProfileController/RegisterUser");
        }
    }
}
