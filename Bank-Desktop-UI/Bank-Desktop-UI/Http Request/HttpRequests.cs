using System.Net.Http;
using System.Net.Http.Json;

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
            try
            {
                var innerPath = $"{GetEndpointFromEnum(Controller)}/{Path}";
                var result = await HttpClient.GetFromJsonAsync<ReturnObject>(innerPath);
                return result;
            }
            catch (Exception ex)
            {
                //add error handling
                return null;
            }
        }

        public static async Task<ReturnObject?> SendHttpPostRequest<ReturnObject>(Enum Controller, string Path, object PostObject)
            where ReturnObject : class
        {
            try
            {
                var innerPath = $"{GetEndpointFromEnum(Controller)}/{Path}";
                var responseMessage = await HttpClient.PostAsJsonAsync(innerPath, PostObject);
                responseMessage.EnsureSuccessStatusCode();
                var result = await responseMessage.Content.ReadFromJsonAsync<ReturnObject>();
                return result;
            }
            catch(Exception ex)
            {
                return null;
            }
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
                    throw new ArgumentException("Invalid data");
            }
        }
    }
}
