using TheCoffeHouse.Enums;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace TheCoffeHouse.Services
{
    public interface IHttpService
    {
        void CancelTokenSourceExecute();
        HttpClient BaseClient { get; }
        Uri BaseAddress { get; set; }
        long MaxResponseContentBufferSize { get; set; }
        HttpRequestHeaders DefaultRequestHeaders { get; }
        HttpContentHeaders DefaultContentHeaders { get; set; }
        TimeSpan Timeout { get; set; }

        #region Task

        Task<T> GetTaskAsync<T>(string requestUri, Func<T, Task> processorSuccess = null, Func<HttpsError, Task> processorError = null,
            HttpMessageHandler httpMesssageHandler = null) where T : class;

        Task PostTaskAsync<T>(string requestUri, IEnumerable<KeyValuePair<string, string>> keyvalues,
            Func<T, Task> processorSuccess, Func<HttpsError, Task> processorError = null, HttpMessageHandler httpMesssageHandler = null) where T : class;

        Task PostTaskAsync<T>(string requestUri, HttpContent content, Func<T, Task> processorSuccess,
            Func<HttpsError, Task> processorError = null, HttpMessageHandler httpMesssageHandler = null) where T : class;

        Task<T> PostTaskAsync<T>(string requestUri, HttpContent content, HttpMessageHandler httpMesssageHandler = null) where T : class;

        Task<T> DeleteTaskAsync<T>(string requestUri, Func<T, Task> processorSuccess = null, Func<HttpsError, Task> processorError = null,
            HttpMessageHandler httpMesssageHandler = null) where T : class;

        Task PutTaskAsync<T>(string requestUri, IEnumerable<KeyValuePair<string, string>> keyvalues,
            Func<T, Task> processorSuccess, Func<HttpsError, Task> processorError = null,
            HttpMessageHandler httpMesssageHandler = null) where T : class;

        Task PutTaskAsync<T>(string requestUri, HttpContent content, Func<T, Task> processorSuccess,
            Func<HttpsError, Task> processorError = null, HttpMessageHandler httpMesssageHandler = null) where T : class;

        #endregion
    }
}
