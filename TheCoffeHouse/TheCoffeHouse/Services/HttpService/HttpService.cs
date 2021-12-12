using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Plugin.Connectivity;
using TheCoffeHouse.Enums;
using TheCoffeHouse.Helpers;
using TheCoffeHouse.Models.SystemModels;
using Xamarin.Essentials;

namespace TheCoffeHouse.Services.HttpService
{
    public class HttpService : IHttpService
    {
        #region Properties

        public const int DeactivationErrorCode = 1401;
        public int TimeoutSeconds = 20;

        public TimeSpan Timeout
        {
            get => BaseClient.Timeout;
            set => BaseClient.Timeout = value;
        }

        private SerializerService _crossJsonSerializer;
        public SerializerService CrossJsonSerializer
            => LazyInitializer.EnsureInitialized(ref _crossJsonSerializer, () => new SerializerService());

        public HttpClient BaseClient
        {
            get;
            private set;
        }

        public Uri BaseAddress
        {
            get => BaseClient.BaseAddress;
            set => BaseClient.BaseAddress = value;
        }

        public long MaxResponseContentBufferSize
        {
            get => BaseClient.MaxResponseContentBufferSize;
            set => BaseClient.MaxResponseContentBufferSize = value;
        }

        public HttpRequestHeaders DefaultRequestHeaders => BaseClient.DefaultRequestHeaders;

        public HttpContentHeaders DefaultContentHeaders { get; set; }

        public CancellationTokenSource CancelTokenSource { get; set; }

        #endregion

        #region Constructor

        public HttpService()
        {
            //BaseClient = new HttpClient() { Timeout = TimeSpan.FromSeconds(TimeoutSeconds) };
            //Timeout = TimeSpan.FromSeconds(TimeoutSeconds);

            HttpClientHandler clientHandler = new HttpClientHandler() { UseCookies = false };

            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            //clientHandler.CookieContainer = new CookieContainer();

            // Pass the handler to httpclient(from you are calling api)
            BaseClient = new HttpClient(clientHandler) { Timeout = TimeSpan.FromSeconds(TimeoutSeconds) };
            Timeout = TimeSpan.FromSeconds(TimeoutSeconds);
        }

        #endregion

        #region Implementation

        #region CancelTokenSourceExecute

        public void CancelTokenSourceExecute()
        {
            if (CancelTokenSource != null)
            {
                try
                {
                    CancelTokenSource.Cancel();
                }
                catch (Exception)
                { }
            }
        }

        #endregion

        #region AddHeaderToken

        public void AddHeaderToken()
        {
            var token = Preferences.Get(PreferencesKey.Token.ToString(), "");

            if (!string.IsNullOrEmpty(token))
            {
                BaseClient.DefaultRequestHeaders.Remove("Authorization");

                BaseClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            }
        }

        #endregion

        #region Get

        public async Task<T> GetTaskAsync<T>(string requestUri, 
            Func<T, Task> processorSuccess = null, 
            Func<HttpsError, Task> processorError = null, 
            HttpMessageHandler httpMesssageHandler = null) where T : class
        {

#if DEBUG
            Debug.WriteLine($"Get method with url: {requestUri}");
#endif
            if (!CrossConnectivity.Current.IsConnected)
            {
                App.IsNotBusy = true;
                await processorError(HttpsError.InternetConnection);
                return null;
            }
            try
            {
                CancelTokenSource = new CancellationTokenSource();
                CancelTokenSource.CancelAfter(Timeout);
                var responseMessage = await GetAsync(requestUri,
                    HttpCompletionOption.ResponseContentRead, CancelTokenSource.Token);

                if (responseMessage == null)
                {
                    App.IsNotBusy = true;
                    if (processorError != null) await processorError(HttpsError.Null);
                    return null;
                }
                else
                {
                    responseMessage = responseMessage.EnsureSuccessStatusCode();
                    using (var jsonStream = await responseMessage.Content.ReadAsStreamAsync())
                    {
                        var result = CrossJsonSerializer.DeserializeFromJsonStream<T>(jsonStream, requestUri);

                        if (result == null)
                        {
                            App.IsNotBusy = true;
                            if (processorError != null) await processorError(HttpsError.Null);
                            return null;
                        }
                        else
                        {
                            if (processorSuccess != null)
                                await processorSuccess(result);

                            if (result.GetType() == typeof(JObjectResponseModel))
                            {
                                var objectResponse = (JObjectResponseModel)(object)result;
                            }
                            return result;
                        }
                    }
                }
            }
            catch (TaskCanceledException ex)
            {
                if (ex.CancellationToken == CancelTokenSource.Token)
                {
                    if (processorError != null)
                    {
                        App.IsNotBusy = true;
                        await processorError(HttpsError.RequestCancellation);
                        return null;
                    }
                    var token = CancelTokenSource.Token;
#if DEBUG
                    Debug.WriteLine(
                        $"A real cancellation, triggered by the caller, token is: {token}, message: {ex.Message}, url: {requestUri}");
#endif
                }
                else
                {
                    if (processorError != null)
                    {
                        App.IsNotBusy = true;
                        await processorError(HttpsError.RequestTimeout);
                        return null;
                    }
#if DEBUG
                    Debug.WriteLine($"A web request timeout, message: {ex.Message}, url: {requestUri}");
#endif
                }

                return null;
            }
            catch (UnauthorizedAccessException unAuEx)
            {
                return null;
            }
            catch (Exception exception)
            {
                if (processorError != null)
                {
                    App.IsNotBusy = true;
                    await processorError(HttpsError.ServerError);
                    return null;
                }
#if DEBUG
                Debug.WriteLine($"Server die with error: {exception.Message}, url: {requestUri}");
#endif
                return null;
            }
            finally
            {
                CancelTokenSource.Dispose();
            }
        }

        private async Task<HttpResponseMessage> GetAsync(string requestUri, HttpCompletionOption completionOption,
           CancellationToken cancellationToken)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return null;
            }

            AddHeaderToken();

            return await BaseClient.GetAsync(requestUri, completionOption, cancellationToken);
        }

        #endregion

        #region Post

        public async Task PostTaskAsync<T>(string requestUri, 
            IEnumerable<KeyValuePair<string, string>> keyvalues, 
            Func<T, Task> processorSuccess, 
            Func<HttpsError, Task> processorError = null, 
            HttpMessageHandler httpMesssageHandler = null) where T : class
        {
            if (Preferences.Get(PreferencesKey.CancelHttpRequest.ToString(), false))
            {
                Preferences.Set(PreferencesKey.CancelHttpRequest.ToString(), false);
                return;
            }

            if (!CrossConnectivity.Current.IsConnected)
            {
                App.IsNotBusy = true;
                await processorError(HttpsError.InternetConnection);
            }
            CancelTokenSource = new CancellationTokenSource();
            CancelTokenSource.CancelAfter(Timeout);
            try
            {
                var stringContent = keyvalues.KeyValuePairToStringContent(requestUri);
                var responseMessage = await PostAsync(requestUri, stringContent, CancelTokenSource.Token);
                if (responseMessage == null)
                {
                    if (processorError != null)
                    {
                        App.IsNotBusy = true;
                        await processorError(HttpsError.Null);
                    }

                }
                else
                {
                    responseMessage = responseMessage.EnsureSuccessStatusCode();
                    using (var jsonStream = await responseMessage.Content.ReadAsStreamAsync())
                    {
                        var result = CrossJsonSerializer.DeserializeFromJsonStream<T>(jsonStream, requestUri);

                        if (result == null)
                        {
                            if (processorError != null)
                            {
                                App.IsNotBusy = true;
                                await processorError(HttpsError.Null);
                            }
                        }
                        else
                        {
                            if (processorSuccess != null) await processorSuccess(result);

                            if (result.GetType() == typeof(JObjectResponseModel))
                            {
                                var objectResponse = (JObjectResponseModel)(object)result;
                            }
                        }
                    }
                }
            }
            catch (TaskCanceledException taskCanceledException)
            {
#if DEBUG
                Debug.WriteLine(
                    taskCanceledException.CancellationToken == CancelTokenSource.Token
                        ? $"A real cancellation, triggered by the caller, token is: {CancelTokenSource.Token}, message: {taskCanceledException.Message}, url: {requestUri}"
                        : $"A web request timeout, message: {taskCanceledException.Message}, url: {requestUri}");
#endif
                if (processorError != null)
                {
                    App.IsNotBusy = true;
                    await processorError(HttpsError.RequestCancellation);
                }
            }
            catch (UnauthorizedAccessException unAuEx)
            {
                //await MessagePopup.Instance.Show("Invalid API call");
            }
            catch (Exception exception)
            {
#if DEBUG
                Debug.WriteLine($"Server die with error: {exception.Message}, url: {requestUri}");
#endif
                if (processorError != null)
                {
                    App.IsNotBusy = true;
                    await processorError(HttpsError.ServerError);
                }
            }
            finally
            {
                CancelTokenSource.Dispose();
            }
        }

        public async Task PostTaskAsync<T>(string requestUri, 
            HttpContent content, 
            Func<T, Task> processorSuccess, 
            Func<HttpsError, Task> processorError = null, 
            HttpMessageHandler httpMesssageHandler = null) where T : class
        {
            if (Preferences.Get(PreferencesKey.CancelHttpRequest.ToString(), false))
            {
                Preferences.Set(PreferencesKey.CancelHttpRequest.ToString(), false);
                return;
            }
            if (!CrossConnectivity.Current.IsConnected)
            {
                App.IsNotBusy = true;
                await processorError(HttpsError.InternetConnection);
                return;
            }
            CancelTokenSource = new CancellationTokenSource();
            CancelTokenSource.CancelAfter(Timeout);
            try
            {
                var responseMessage = await PostAsync(requestUri, content, CancelTokenSource.Token);
                if (responseMessage == null)
                {
                    if (processorError != null)
                    {
                        App.IsNotBusy = true;
                        await processorError(HttpsError.Null);
                    }
                }
                else
                {
                    responseMessage = responseMessage.EnsureSuccessStatusCode();
                    using (var jsonStream = await responseMessage.Content.ReadAsStreamAsync())
                    {
                        var result = CrossJsonSerializer.DeserializeFromJsonStream<T>(jsonStream, requestUri);

                        if (result == null)
                        {
                            if (processorError != null)
                            {
                                App.IsNotBusy = true;
                                await processorError(HttpsError.Null);
                            }
                        }
                        else
                        {
                            if (processorSuccess != null) await processorSuccess(result);

                            if (result.GetType() == typeof(JObjectResponseModel))
                            {
                                var objectResponse = (JObjectResponseModel)(object)result;
                            }
                        }
                    }
                }
            }
            catch (TaskCanceledException taskCanceledException)
            {
#if DEBUG
                Debug.WriteLine(
                    taskCanceledException.CancellationToken == CancelTokenSource.Token
                        ? $"A real cancellation, triggered by the caller, token is: {CancelTokenSource.Token}, message: {taskCanceledException.Message}, url: {requestUri}"
                        : $"A web request timeout, message: {taskCanceledException.Message}, url: {requestUri}");
#endif
                if (processorError != null)
                {
                    App.IsNotBusy = true;
                    await processorError(HttpsError.RequestCancellation);
                }
            }
            catch (UnauthorizedAccessException unAuEx)
            { }
            catch (Exception exception)
            {
#if DEBUG
                Debug.WriteLine($"Server die with error: {exception.Message}, url: {requestUri}");
#endif
                if (processorError != null)
                {
                    App.IsNotBusy = true;
                    await processorError(HttpsError.ServerError);
                }
            }
            finally
            {
                CancelTokenSource.Dispose();
            }
        }

        public async Task<T> PostTaskAsync<T>(string requestUri, 
            HttpContent content, 
            HttpMessageHandler httpMesssageHandler = null) where T : class
        {
            if (Preferences.Get(PreferencesKey.CancelHttpRequest.ToString(), false))
            {
                Preferences.Set(PreferencesKey.CancelHttpRequest.ToString(), false);
                return null;
            }
            CancelTokenSource = new CancellationTokenSource();
            CancelTokenSource.CancelAfter(Timeout);
            try
            {
                var responseMessage = await PostAsync(requestUri, content, CancelTokenSource.Token);
                if (responseMessage == null)
                {
                    return null;
                }
                else
                {
                    var json = await responseMessage.Content.ReadAsStringAsync();

                    using (var jsonStream = await responseMessage.Content.ReadAsStreamAsync())
                    {
                        var result = CrossJsonSerializer.DeserializeFromJsonStream<T>(jsonStream, requestUri);

                        return result;
                    }
                }
            }
            catch (TaskCanceledException taskCanceledException)
            {
#if DEBUG
                Debug.WriteLine(
                    taskCanceledException.CancellationToken == CancelTokenSource.Token
                        ? $"A real cancellation, triggered by the caller, token is: {CancelTokenSource.Token}, " +
                          $"message: {taskCanceledException.Message}, url: {requestUri}"
                        : $"A web request timeout, message: {taskCanceledException.Message}, url: {requestUri}");
#endif
                return null;
            }
            catch (UnauthorizedAccessException unAuEx)
            {
                return null;
            }
            catch (Exception exception)
            {
#if DEBUG
                Debug.WriteLine($"Server die with error: {exception.Message}, url: {requestUri}");
#endif
                return null;
            }
            finally
            {
                CancelTokenSource.Dispose();
            }
        }

        private async Task<HttpResponseMessage> PostAsync(string requestUri,
          HttpContent content,
         CancellationToken cancellationToken)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return null;
            }

            AddHeaderToken();

            return await BaseClient.PostAsync(requestUri, content, cancellationToken);
        }

        #endregion

        #region Put

        public async Task PutTaskAsync<T>(string requestUri, 
            IEnumerable<KeyValuePair<string, string>> keyvalues, 
            Func<T, Task> processorSuccess, 
            Func<HttpsError, Task> processorError = null, 
            HttpMessageHandler httpMesssageHandler = null) where T : class
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                await processorError(HttpsError.InternetConnection);
            }
            CancelTokenSource = new CancellationTokenSource();
            CancelTokenSource.CancelAfter(Timeout);
            try
            {
                var stringContent = keyvalues.KeyValuePairToStringContent(requestUri);
                var responseMessage = await PutAsync(requestUri, stringContent, CancelTokenSource.Token);
                if (responseMessage == null)
                {
                    if (processorError != null)
                    {

                    }

                    App.IsNotBusy = true;
                    await processorError(HttpsError.Null);
                }
                else
                {
                    responseMessage = responseMessage.EnsureSuccessStatusCode();
                    using (var jsonStream = await responseMessage.Content.ReadAsStreamAsync())
                    {
                        var result = CrossJsonSerializer.DeserializeFromJsonStream<T>(jsonStream, requestUri);

                        if (result == null)
                        {
                            if (processorError != null)
                            {
                                App.IsNotBusy = true;
                                await processorError(HttpsError.Null);
                            }
                        }
                        else
                        {
                            await processorSuccess(result);
                        }
                    }
                }
            }
            catch (TaskCanceledException taskCanceledException)
            {
#if DEBUG
                Debug.WriteLine(
                    taskCanceledException.CancellationToken == CancelTokenSource.Token
                        ? $"A real cancellation, triggered by the caller, token is: {CancelTokenSource.Token}, message: {taskCanceledException.Message}, url: {requestUri}"
                        : $"A web request timeout, message: {taskCanceledException.Message}, url: {requestUri}");
#endif
                if (processorError != null)
                {
                    App.IsNotBusy = true;
                    await processorError(HttpsError.RequestCancellation);
                }

            }
            catch (UnauthorizedAccessException unAuEx)
            { }
            catch (Exception exception)
            {
#if DEBUG
                Debug.WriteLine($"Server die with error: {exception.Message}, url: {requestUri}");
#endif
                if (processorError != null)
                {
                    App.IsNotBusy = true;
                    await processorError(HttpsError.ServerError);
                }
            }
            finally
            {
                CancelTokenSource.Dispose();
            }
        }

        public async Task PutTaskAsync<T>(string requestUri, HttpContent content, Func<T, Task> processorSuccess, Func<HttpsError, Task> processorError = null, HttpMessageHandler httpMesssageHandler = null) where T : class
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                App.IsNotBusy = true;
                await processorError(HttpsError.InternetConnection);
                return;
            }
            CancelTokenSource = new CancellationTokenSource();
            CancelTokenSource.CancelAfter(Timeout);
            try
            {
                var responseMessage = await PutAsync(requestUri, content, CancelTokenSource.Token);
                if (responseMessage == null)
                {
                    if (processorError != null)
                    {
                        App.IsNotBusy = true;
                        await processorError(HttpsError.Null);
                    }
                }
                else
                {
                    responseMessage = responseMessage.EnsureSuccessStatusCode();
                    using (var jsonStream = await responseMessage.Content.ReadAsStreamAsync())
                    {
                        var result = CrossJsonSerializer.DeserializeFromJsonStream<T>(jsonStream, requestUri);

                        if (result == null)
                        {
                            if (processorError != null)
                            {
                                App.IsNotBusy = true;
                                await processorError(HttpsError.Null);
                            }
                        }
                        else
                        {
                            await processorSuccess(result);
                        }
                    }
                }
            }
            catch (TaskCanceledException taskCanceledException)
            {
#if DEBUG
                Debug.WriteLine(
                    taskCanceledException.CancellationToken == CancelTokenSource.Token
                        ? $"A real cancellation, triggered by the caller, token is: {CancelTokenSource.Token}, message: {taskCanceledException.Message}, url: {requestUri}"
                        : $"A web request timeout, message: {taskCanceledException.Message}, url: {requestUri}");
#endif
                if (processorError != null)
                {
                    App.IsNotBusy = true;
                    await processorError(HttpsError.RequestCancellation);
                }
            }
            catch (UnauthorizedAccessException unAuEx)
            { }
            catch (Exception exception)
            {
#if DEBUG
                Debug.WriteLine($"Server die with error: {exception.Message}, url: {requestUri}");
#endif 
                if (processorError != null)
                {
                    App.IsNotBusy = true;
                    await processorError(HttpsError.ServerError);
                }
            }
            finally
            {
                CancelTokenSource.Dispose();
            }
        }

        private async Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content,
           CancellationToken cancellationToken)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return null;
            }

            return await BaseClient.PutAsync(requestUri, content, cancellationToken);
        }

        #endregion

        #region Delete

        public async Task<T> DeleteTaskAsync<T>(string requestUri, Func<T, Task> processorSuccess = null, Func<HttpsError, Task> processorError = null, HttpMessageHandler httpMesssageHandler = null) where T : class
        {
#if DEBUG
            Debug.WriteLine($"Delete method with url: {requestUri}");
#endif
            if (!CrossConnectivity.Current.IsConnected)
            {
                await processorError(HttpsError.InternetConnection);
                return null;
            }
            CancelTokenSource = new CancellationTokenSource();
            CancelTokenSource.CancelAfter(Timeout);
            try
            {
                var responseMessage = await DeleteAsync(requestUri, CancelTokenSource.Token);
                if (responseMessage == null)
                {
                    if (processorError != null)
                    {
                    }

                    App.IsNotBusy = true;
                    await processorError(HttpsError.Null);
                    return null;
                }
                else
                {
                    responseMessage = responseMessage.EnsureSuccessStatusCode();
                    using (var jsonStream = await responseMessage.Content.ReadAsStreamAsync())
                    {
                        var result = CrossJsonSerializer.DeserializeFromJsonStream<T>(jsonStream, requestUri);

                        if (result == null)
                        {
                            if (processorError != null)
                            {
                            }

                            App.IsNotBusy = true;
                            await processorError(HttpsError.Null);
                            return null;
                        }
                        else
                        {
                            if (processorSuccess != null)
                            {
                                await processorSuccess(result);
                            }

                            return result;
                        }
                    }
                }
            }
            catch (TaskCanceledException ex)
            {
                if (ex.CancellationToken == CancelTokenSource.Token)
                {
                    if (processorError != null)
                    {
                        App.IsNotBusy = true;
                        await processorError(HttpsError.RequestCancellation);
                        return null;
                    }
                    var token = CancelTokenSource.Token;
#if DEBUG
                    Debug.WriteLine(
                        $"A real cancellation, triggered by the caller, token is: {token}, message: {ex.Message}, url: {requestUri}");
#endif
                }
                else
                {
                    if (processorError != null)
                    {
                        App.IsNotBusy = true;
                        await processorError(HttpsError.RequestTimeout);
                        return null;
                    }
#if DEBUG
                    Debug.WriteLine($"A web request timeout, message: {ex.Message}, url: {requestUri}");
#endif
                }
                return null;
            }
            catch (UnauthorizedAccessException unAuEx)
            {
                return null;
            }
            catch (Exception exception)
            {
                if (processorError != null)
                {
                    App.IsNotBusy = true;
                    await processorError(HttpsError.ServerError);
                    return null;
                }
#if DEBUG
                Debug.WriteLine($"Server die with error: {exception.Message}, url: {requestUri}");
#endif
                return null;
            }
            finally
            {
                CancelTokenSource.Dispose();
            }
        }

        private async Task<HttpResponseMessage> DeleteAsync(string requestUri, CancellationToken cancellationToken)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return null;
            }

            return await BaseClient.DeleteAsync(requestUri, cancellationToken);
        }

        #endregion

        #endregion
    }
}
