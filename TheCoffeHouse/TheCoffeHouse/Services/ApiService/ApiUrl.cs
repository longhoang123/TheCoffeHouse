using System;

namespace TheCoffeHouse.Services.ApiService
{
    public static class ApiUrl
    {
        public static string BaseUrl = "http://www.tchie307.somee.com/api/";
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

        #region Thanh Long
        public static string GetCoupons()
        {
            return Link($"CouponController/GetCoupon");
        }
        public static string GetCouponByCode(string code)
        {
            return Link($"CouponController/GetCouponByCode?Code={code}");
        }
        public static string GetPromotions()
        {
            return Link($"PromotionController/GetPromotion");
        }
        public static string GetPromotionById(int Id)
        {
            return Link($"PromotionController/GetPromotionById? Id = {Id}");
        }
        public static string GetCategory()
        {
            return Link($"api/CategoryController/GetCategory");
        }
        public static string GetCategoryById(int Id)
        {
            return Link($"api/CategoryController/GetCategoryById?Id={Id}");
        }
        #endregion
        #region Cart
        public static string GetCartByIDUser(int IDUser)
        {
            return Link($"CartController/GetCartByIDCust?IDUser={IDUser}");
        }
        public static string CreateCartByIDCust()
        {
            return Link($"CartController/CreateCartByIDCust");
        }
        #endregion
    }
}
