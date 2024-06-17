using Bank_Desktop_UI.Models;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;

namespace Bank_Desktop_UI.Http_Request
{
    public static class HttpRequests
    {
        private static Uri BaseUrl { get; set; } = new Uri("http://localhost:5205");
        static HttpClient HttpClient { get; set; } = new HttpClient()
        {
            BaseAddress = BaseUrl
        };

        public static async Task<ReturnObject?> SendHttpGetRequest<ReturnObject>(Enum Controller, string Path)
            where ReturnObject : class
        {
            var innerPath = $"{GetEndpointFromEnum(Controller)}/{Path}";
            return await ExecuteWithExceptionHandling(() => SendHttpRequest<ReturnObject>(HttpMethod.Get, innerPath));
        }

        public static async Task<ReturnObject?> SendHttpPostRequest<ReturnObject>(Enum Controller, string Path, object PostObject)
            where ReturnObject : class
        {
            var innerPath = $"{GetEndpointFromEnum(Controller)}/{Path}";
            return await ExecuteWithExceptionHandling(() => SendHttpRequest<ReturnObject>(HttpMethod.Post, innerPath, PostObject));
        }

        private static async Task<ReturnObject?> ExecuteWithExceptionHandling<ReturnObject>(Func<Task<ReturnObject?>> httpRequestFunc)
            where ReturnObject : class
        {
            try
            {
                return await httpRequestFunc();
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.BadRequest)
            {
                // Log error
            }
            catch 
            {
                // log general error
                MessageBox.Show("Connection to server could not be established");
            }
            return null;
        }

        private static async Task<ReturnObject?> SendHttpRequest<ReturnObject>(HttpMethod method, string innerPath, object? content = null)
            where ReturnObject : class
        {
            HttpResponseMessage response;
            if(!string.IsNullOrEmpty(BaseInfo.Token) && !HttpClient.DefaultRequestHeaders.Contains("Authorization"))
                HttpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + BaseInfo.Token);

            if (method == HttpMethod.Get)
            {
                response = await HttpClient.GetAsync(innerPath);
            }
            else if (method == HttpMethod.Post && content != null)
            {
                response = await HttpClient.PostAsJsonAsync(innerPath, content);
            }
            else
            {
                throw new ArgumentException("Unsupported HTTP method or missing content for POST request.");
            }

            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<ReturnObject>();
            return result;
        }

        public enum Endpoints
        {
            UserAuthentication = 0,
            User = 1,
            Account = 2,
            Creditcard = 3,
            Transaction = 4,
        }

        private static string GetEndpointFromEnum(Enum Endpoint)
        {
            var baseApiPath = "Api/";
            switch (Endpoint)
            {
                case Endpoints.UserAuthentication:
                    return baseApiPath + "UserAuthentication";
                case Endpoints.User:
                    return baseApiPath + "User";
                case Endpoints.Account:
                    return baseApiPath + "Account";
                case Endpoints.Creditcard:
                    return baseApiPath + "Creditcard";
                case Endpoints.Transaction:
                    return baseApiPath + "Transaction";
                default:
                    throw new NotImplementedException("Endpoint not implemented for Enum");
            }
        }
    }
}
