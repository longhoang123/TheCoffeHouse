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
        public static string GetUserByID(int userID)
        {
            return Link($"UserProfileController/GetUserByID?userID={userID}");
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
            return Link($"CategoryController/GetCategory");
        }
        public static string GetCategoryById(int Id)
        {
            return Link($"CategoryController/GetCategoryById?Id={Id}");
        }
        public static string GetDrink()
        {
            return Link($"DrinkController/GetDrink");
        }
        public static string GetDrinkById(int Id)
        {
            return Link($"DrinkController/GetDrinkById?Id={Id}");
        }
        public static string GetDrinkByCate(int Id)
        {
            return Link($"DrinkController/GetDrinkByCate?Id={Id}");
        }
        public static string GetDrinkByKey(string key)
        {
            return Link($"DrinkController/GetDrinkByKey?key={key}");
        }
        public static string GetDrinkImageById(int Id)
        {
            return Link($"DrinkImageController/ReadDrinkImageById?Id={Id}");
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
        public static string UpdateCartByIDCust()
        {
            return Link($"CartController/UpdateCartByIDCust");
        }
        #endregion
        #region DetailCart
        public static string AddToCart()
        {
            return Link($"DetailCartController/AddToCart");
        }
        public static string GetAllDetailCart(int IDCart)
        {
            return Link($"DetailCartController/GetAllDetailCart?IDCart={IDCart}");
        }
        public static string DeleteItemCart(int IDDetailCart)
        {
            return Link($"DetailCartController/DeleteItemCart?IDDetailCart={IDDetailCart}");
        }
        #endregion
        #region Order
        public static string CreateOrder()
        {
            return Link($"OrderController/CreateOrder");
        }
        public static string GetDetailOrderByIdOrder(int IDOrder)
        {
            return Link($"OrderController/GetDetailOrderByIDOrder?IDOrder={IDOrder}");
        }
        public static string GetAllOrderByIduser(int IDUser)
        {
            return Link($"OrderController/GetAllOrderByIDUser?IDUser={IDUser}");
        }
        public static string UpdateStatusOrder(int IDOrder, string StatusOrder)
        {
            return Link($"OrderController/UpdateStatusOrder?IDOrder={IDOrder}&StatusOrder={StatusOrder}");
        }
        #endregion

        #region Store
        #region Create Store
        public static string CreateStore()
        {
            return Link($"StoreController/CreateStore");
        }
        #endregion

        #region Update Store
        public static string UpdateStore()
        {
            return Link($"StoreController/UpdateStore");
        }
        #endregion

        #region Delete Store
        public static string DeleteStore(int IDStore)
        {
            return Link($"StoreController/DeleteStore?IDStore={IDStore}");
        }
        #endregion

        #region CreateStoreImage
        public static string CreateStoreImage()
        {
            return Link($"StoreController/CreateStoreImage");
        }
        #endregion

        #region UpdateStoreImage
        public static string UpdateStoreImage()
        {
            return Link($"StoreController/UpdateStoreImage");
        }
        #endregion

        #region GetAllStore
        public static string GetAllStore()
        {
            return Link($"StoreController/GetAllStore");
        }
        #endregion

        #region GetImageByIDStore
        public static string GetImageByIDStore(int IDStore)
        {
            return Link($"StoreController/GetImageByIDStore?IDStore={IDStore}");
        }
        #endregion

        #region GetStoreByKey
        public static string GetStoreByKey(string key)
        {
            return Link($"StoreController/GetStoreByKey?key={key}");
        }
        #endregion

        #endregion
    }
}
