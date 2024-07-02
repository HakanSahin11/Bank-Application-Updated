﻿using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.Threading.Tasks;
using static Bank_Web.Services.IHttpClientService;

namespace Bank_Web.Services
{
    public class HttpClientService : IHttpClientService
    {
        private readonly HttpClient _httpClient;
        public string? Token { get; set; }

        public HttpClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5205");
        }

        public async Task<ReturnObject?> SendHttpGetRequest<ReturnObject>(Enum Controller, string Path) where ReturnObject : class
        {
            var innerPath = await GetEndpoint(Controller) + $"/{Path}";
            return await ExecuteWithExceptionHandling(() => SendHttpRequest<ReturnObject>(HttpMethod.Get, innerPath));
        }

        public async Task<ReturnObject?> SendHttpPostRequest<ReturnObject>(Enum Controller, string Path, object PostObject) where ReturnObject : class
        {
            var innerPath = await GetEndpoint(Controller) + $"/{Path}";
            return await ExecuteWithExceptionHandling(() => SendHttpRequest<ReturnObject>(HttpMethod.Post, innerPath, PostObject));
        }

        public async Task<string> GetEndpoint(Enum endpoint)
        {
            var baseApiPath = "Api/";

            switch (endpoint)
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

        private async Task<ReturnObject?> ExecuteWithExceptionHandling<ReturnObject>(Func<Task<ReturnObject?>> httpRequestFunc) where ReturnObject : class
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
            }
            return null;
        }

        private async Task<ReturnObject?> SendHttpRequest<ReturnObject>(HttpMethod method, string innerPath, object? content = null)
            where ReturnObject : class
        {
            if (!string.IsNullOrEmpty(Token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            }
            else
            {
                _httpClient.DefaultRequestHeaders.Authorization = null;
            }

            HttpResponseMessage response;
            if (method == HttpMethod.Get)
            {
                response = await _httpClient.GetAsync(innerPath);
            }
            else if (method == HttpMethod.Post && content != null)
            {
                response = await _httpClient.PostAsJsonAsync(innerPath, content);
            }
            else
            {
                throw new ArgumentException("Unsupported HTTP method or missing content for POST request.");
            }

            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<ReturnObject>();
            return result;
        }
    }

    public interface IHttpClientService
    {
        string? Token { get; set; }
        Task<ReturnObject?> SendHttpGetRequest<ReturnObject>(Enum Controller, string Path) where ReturnObject : class;
        Task<ReturnObject?> SendHttpPostRequest<ReturnObject>(Enum Controller, string Path, object PostObject) where ReturnObject : class;
        Task<string> GetEndpoint(Enum endpoint);

        public enum Endpoints
        {
            UserAuthentication = 0,
            User = 1,
            Account = 2,
            Creditcard = 3,
            Transaction = 4,
        }
    }
}