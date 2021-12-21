using TheCoffeHouse.Enums;
using TheCoffeHouse.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TheCoffeHouse.Models;
using System.Collections.Generic;

namespace TheCoffeHouse.Services.ApiService
{
    public class ApiService
    {
        #region Properties
        public const int DEFAULT_TIME_OUT_SECONDS = 20;
        #endregion

        

        #region BaseRequest

        private static async Task<T> Get<T>(string url)
        {
            using (var client = new HttpClient())
            {
                
                try
                {
                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var responseObj = JsonConvert.DeserializeObject<List<T>>(json);

                        return responseObj.First();
                    }                    
                    else
                    {
                        return default;
                    }
                }
                catch (TaskCanceledException ex)
                {
#if DEBUG
                    Debug.WriteLine(
                        $"Canceled by Cancellation token: when calling HTTP request: method {nameof(Get)}" +
                        $"\nurl: {url}" +
                        $"\nmessage: {ex.Message}");
#endif
                    return default;
                }
                catch (Exception ex)
                {
#if DEBUG
                    Debug.WriteLine(
                        $"Error when calling HTTP request: method {nameof(Get)}" +
                        $"\nurl: {url}" +
                        $"\nmessage: {ex.Message}");
#endif
                    return default;
                }
                finally
                {
                    
                }
            }
        }

        private static async Task<T> GetList<T>(string url)
        {
            using (var client = new HttpClient())
            {

                try
                {
                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var responseObj = JsonConvert.DeserializeObject<T>(json);

                        return responseObj;
                    }
                    else
                    {
                        return default;
                    }
                }
                catch (TaskCanceledException ex)
                {
#if DEBUG
                    Debug.WriteLine(
                        $"Canceled by Cancellation token: when calling HTTP request: method {nameof(Get)}" +
                        $"\nurl: {url}" +
                        $"\nmessage: {ex.Message}");
#endif
                    return default;
                }
                catch (Exception ex)
                {
#if DEBUG
                    Debug.WriteLine(
                        $"Error when calling HTTP request: method {nameof(Get)}" +
                        $"\nurl: {url}" +
                        $"\nmessage: {ex.Message}");
#endif
                    return default;
                }
                finally
                {

                }
            }
        }

        private static async Task<T> Post<T>(string url, object obj)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    string jsonData = JsonConvert.SerializeObject(obj);
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var responseObj = JsonConvert.DeserializeObject<List<T>>(json);

                        return responseObj.First();
                    }
                    
                    else
                    {
                        return default;
                    }
                }
                catch (TaskCanceledException ex)
                {
#if DEBUG
                    Debug.WriteLine(
                        $"Canceled by Cancellation token when calling HTTP request: method {nameof(Post)}" +
                        $"\nurl: {url}" +
                        $"\nmessage: {ex.Message}");
#endif
                    return default;
                }
                catch (Exception ex)
                {
#if DEBUG
                    Debug.WriteLine(
                        $"Error when calling HTTP request: method {nameof(Post)}" +
                        $"\nurl: {url}" +
                        $"\nmessage: {ex.Message}");
#endif
                    return default;
                }
                finally
                {
                    
                }
            }
        }

        //        private static async Task<T> PostFile<T>(string url, string fileName, byte[] data)
        //        {
        //            using (var client = NewHttpClientWithHeaders())
        //            {
        //                CancellationTokenSource cancellationTokenSource = NewCancellationTokenSource();
        //                try
        //                {
        //                    var bac = new ByteArrayContent(data);
        //                    var multiContent = new MultipartFormDataContent();    
        //                    multiContent.Headers.ContentType.MediaType = "multipart/form-data";
        //                    multiContent.Add(bac, "imageData", fileName);
        //                    var response = await client.PostAsync(url, multiContent);

        //                    if (response.IsSuccessStatusCode)
        //                    {
        //                        var json = await response.Content.ReadAsStringAsync();
        //                        var responseObj = JsonConvert.DeserializeObject<T>(json);

        //                        return responseObj;
        //                    }
        //                    else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        //                    {
        //                        if (await GetNewAuthTokens())
        //                        {
        //                            return await PostFile<T>(url, fileName,data);
        //                        }

        //                        return default;
        //                    }
        //                    else
        //                    {
        //                        return default;
        //                    }
        //                }
        //                catch (TaskCanceledException ex)
        //                {
        //#if DEBUG
        //                    Debug.WriteLine(
        //                        $"Canceled by Cancellation token when calling HTTP request: method {nameof(Post)}" +
        //                        $"\nurl: {url}" +
        //                        $"\nmessage: {ex.Message}");
        //#endif
        //                    return default;
        //                }
        //                catch (Exception ex)
        //                {
        //#if DEBUG
        //                    Debug.WriteLine(
        //                        $"Error when calling HTTP request: method {nameof(Post)}" +
        //                        $"\nurl: {url}" +
        //                        $"\nmessage: {ex.Message}");
        //#endif
        //                    return default;
        //                }
        //                finally
        //                {
        //                    cancellationTokenSource.Dispose();
        //                }
        //            }
        //        }

        private static async Task<T> Put<T>(string url, object obj)
        {
            using (var client = new HttpClient())
            {
                //CancellationTokenSource cancellationTokenSource = NewCancellationTokenSource();
                try
                {
                    string jsonData = JsonConvert.SerializeObject(obj);
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    var response = await client.PutAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var responseObj = JsonConvert.DeserializeObject<T>(json);

                        return responseObj;
                    }
                    //else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    //{
                    //    if (await GetNewAuthTokens())
                    //    {
                    //        return await Put<T>(url, obj);
                    //    }

                    //    return default;
                    //}
                    else
                    {
                        return default;
                    }
                }
                catch (TaskCanceledException ex)
                {
#if DEBUG
                    Debug.WriteLine(
                        $"Canceled by Cancellation token when calling HTTP request: method {nameof(Put)}" +
                        $"\nurl: {url}" +
                        $"\nmessage: {ex.Message}");
#endif
                    return default;
                }
                catch (Exception ex)
                {
#if DEBUG
                    Debug.WriteLine(
                        $"Error when calling HTTP request: method {nameof(Put)}" +
                        $"\nurl: {url}" +
                        $"\nmessage: {ex.Message}");
#endif
                    return default;
                }
                finally
                {
                    //cancellationTokenSource.Dispose();
                }
            }
        }


        //        private static async Task<T> Delete<T>(string url)
        //        {
        //            using (var client = NewHttpClientWithHeaders())
        //            {
        //                CancellationTokenSource cancellationTokenSource = NewCancellationTokenSource();
        //                try
        //                {
        //                    var response = await client.DeleteAsync(url, cancellationTokenSource.Token);

        //                    if (response.IsSuccessStatusCode)
        //                    {
        //                        var json = await response.Content.ReadAsStringAsync();
        //                        var responseObj = JsonConvert.DeserializeObject<T>(json);

        //                        return responseObj;
        //                    }
        //                    else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        //                    {
        //                        if (await GetNewAuthTokens())
        //                        {
        //                            return await Delete<T>(url);
        //                        }

        //                        return default;
        //                    }
        //                    else
        //                    {
        //                        return default;
        //                    }
        //                }
        //                catch (TaskCanceledException ex)
        //                {
        //#if DEBUG
        //                    Debug.WriteLine(
        //                        $"Canceled by Cancellation token:when calling HTTP request: method {nameof(Delete)}" +
        //                        $"\nurl: {url}" +
        //                        $"\nmessage: {ex.Message}");
        //#endif
        //                    return default;
        //                }
        //                catch (Exception ex)
        //                {
        //#if DEBUG
        //                    Debug.WriteLine(
        //                        $"Error when calling HTTP request: method {nameof(Delete)}" +
        //                        $"\nurl: {url}" +
        //                        $"\nmessage: {ex.Message}");
        //#endif
        //                    return default;
        //                }
        //                finally
        //                {
        //                    cancellationTokenSource.Dispose();
        //                }
        //            }
        //        }

        #endregion



        #region GetPost

        public static async Task<ObservableCollection<HomePostItem>> GetPosts()
        {
            var url = ApiUrl.GetPosts();
            return await GetList<ObservableCollection<HomePostItem>>(url);
        }
        #endregion
        #region GetPosts
        public static async Task<HomePostItem> GetPost(string postID)
        {
            var url = ApiUrl.GetPost(postID);
            return await Get<HomePostItem>(url);
        }

        #endregion

        #region GetUserByID
        public static async Task<User> GetUserByID(int userID)
        {
            var url = ApiUrl.GetUserByID(userID);
            return await Get<User>(url);
        }

        #endregion
        #region ValidateUser
        public static async Task<User> ValidateUser(string phone, string password)
        {
            var url = ApiUrl.ValidateUser(phone, password);
            return await Get<User>(url);
        }

        #endregion
        #region RegisterUser
        public static async Task<Dictionary<string, int>> RegisterUser(User user)
        {
            var url = ApiUrl.RegisterUser();
            return await Post<Dictionary<string, int>>(url, user);
        }

        #endregion
        #region GetCoupon
        public static async Task<ObservableCollection<Coupon>> GetCoupons()
        {
            var url = ApiUrl.GetCoupons();
            return await GetList<ObservableCollection<Coupon>>(url);
        }

        #endregion
        #region GetCouponByCode
        public static async Task<Coupon> GetCouponByCode(string code)
        {
            var url = ApiUrl.GetCouponByCode(code);
            return await Get<Coupon>(url);
        }

        #endregion
        #region GetPromotion
        public static async Task<ObservableCollection<Promotion>> GetPromotions()
        {
            var url = ApiUrl.GetPromotions();
            return await GetList<ObservableCollection<Promotion>>(url);
        }
        #endregion
        #region GetPromotionById
        public static async Task<Promotion> GetPromotionById(int id)
        {
            var url = ApiUrl.GetPromotionById(id);
            return await Get<Promotion>(url);
        }

        #endregion
        #region GetCategory
        public static async Task<ObservableCollection<Category>> GetCategory()
        {
            var url = ApiUrl.GetCategory();
            return await GetList<ObservableCollection<Category>>(url);
        }
        #endregion
        #region GetCategoryById
        public static async Task<Category> GetCategoryById(int id)
        {
            var url = ApiUrl.GetCategoryById(id);
            return await Get<Category>(url);
        }

        #endregion
        #region GetDrink
        public static async Task<ObservableCollection<Drink>> GetDrink()
        {
            var url = ApiUrl.GetDrink();
            return await GetList<ObservableCollection<Drink>>(url);
        }
        #endregion
        #region GetDrinkById
        public static async Task<Drink> GetDrinkById(int id)
        {
            var url = ApiUrl.GetDrinkById(id);
            return await Get<Drink>(url);
        }
        #endregion
        #region GetDrinkByCate
        public static async Task<ObservableCollection<Drink>> GetDrinkByCate(int id)
        {
            var url = ApiUrl.GetDrinkByCate(id);
            return await Get<ObservableCollection<Drink>>(url);
        }
        #endregion
        #region GetDrinkImageById
        public static async Task<ObservableCollection<DrinkImage>> GetDrinkImageById(int id)
        {
            var url = ApiUrl.GetDrinkImageById(id);
            return await GetList<ObservableCollection<DrinkImage>>(url);
        }
        #endregion

        #region Cart
            #region GetCartByIDUser
            public static async Task<Cart> GetCartByIDUser(int IDUser)
            {
                var url = ApiUrl.GetCartByIDUser(IDUser);
                return await Get<Cart>(url);
            }
            #endregion
            # region CreateCartByIDUser
            public static async Task<Dictionary<string, int>> CreateCartByIDCust(Cart cart)
            {
                var url = ApiUrl.CreateCartByIDCust();
                return await Post<Dictionary<string, int>>(url, cart);
            }
            #endregion
            #region UpdateCartByIDUser  
            public static async Task<Dictionary<string, int>> UpdateCartByIDCust(Cart cart)
            {
                var url = ApiUrl.UpdateCartByIDCust();
                return await Put<Dictionary<string, int>>(url, cart);
            }
            #endregion
        #endregion

        #region DetailCart
            #region AddToCart
            public static async Task<Dictionary<string, int>> AddToCart(DetailCart detail)
            {
                var url = ApiUrl.AddToCart();
                return await Post<Dictionary<string, int>>(url, detail);
            }
            #endregion
            #region Get All Item in Cart
            public static async Task<ObservableCollection<DetailCart>> GetAllDetailCart(int IDCart)
            {
                var url = ApiUrl.GetAllDetailCart(IDCart);
                return await GetList<ObservableCollection<DetailCart>>(url);
            }
            #endregion
            #region Delete DetailCart in Cart
            public static async Task<DetailCart> DeleteItemCart(int IDDetailCart)
            {
                var url = ApiUrl.DeleteItemCart(IDDetailCart);
                return await Get<DetailCart>(url);
            }
        #endregion
        #endregion

        #region Order
            #region Create Order
            public static async Task<Dictionary<string, int>> CreateOrder(Order order)
            {
                var url = ApiUrl.CreateOrder();
                return await Post<Dictionary<string, int>>(url, order);
            }
            #endregion
            #region GetDetailOrderByIdOrder
            public static async Task<ObservableCollection<DetailOrder>> GetDetailOrderByIdOrder(int IDOrder)
            {
                var url = ApiUrl.GetDetailOrderByIdOrder(IDOrder);
                return await GetList< ObservableCollection<DetailOrder>>(url);
            }
            #endregion

            #region GetAllOrderByIduser
            public static async Task<ObservableCollection<Order>> GetAllOrderByIduser(int IDuser)
                {
                var url = ApiUrl.GetAllOrderByIduser(IDuser);
                return await GetList<ObservableCollection<Order>>(url);
            }
            #endregion
        #endregion
    }
}