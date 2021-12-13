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

        //        private static async Task<T> Put<T>(string url, object obj)
        //        {
        //            using(var client = NewHttpClientWithHeaders())
        //            {
        //                CancellationTokenSource cancellationTokenSource = NewCancellationTokenSource();
        //                try
        //                {
        //                    string jsonData = JsonConvert.SerializeObject(obj);
        //                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        //                    var response = await client.PutAsync(url, content, cancellationTokenSource.Token);

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
        //                            return await Put<T>(url, obj);
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
        //                        $"Canceled by Cancellation token when calling HTTP request: method {nameof(Put)}" +
        //                        $"\nurl: {url}" +
        //                        $"\nmessage: {ex.Message}");
        //#endif
        //                    return default;
        //                }
        //                catch (Exception ex)
        //                {
        //#if DEBUG
        //                    Debug.WriteLine(
        //                        $"Error when calling HTTP request: method {nameof(Put)}" +
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









    }
}
