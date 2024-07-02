using Microsoft.JSInterop;
using System.Text.Json;

namespace Bank_Web.Helpers
{
    public class LocalStorageService : ILocalStorageService
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly ILogger<LocalStorageService> _logger;

        public LocalStorageService(IJSRuntime jsRuntime, ILogger<LocalStorageService> logger)
        {
            _jsRuntime = jsRuntime;
            _logger = logger;
        }

        public async Task<T> GetItem<T>(string key)
        {
            try
            {
                var json = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);

                if (!string.IsNullOrEmpty(json))
                {
                    return JsonSerializer.Deserialize<T>(json);
                }

                _logger.LogError($"Key '{key}' was not found in local storage");
                return default;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Key '{key}' was not found in local storage");
                return default;
            }
        }

        public async Task SetItem<T>(string key, T value)
        {
            try
            {
                var json = JsonSerializer.Serialize(value);
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, json);
                _logger.LogInformation($"Key '{key}' has been set in local storage.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Could not set key'{key}' in local storage.");
                return;
            }
        }

        public async Task RemoveItem(string key)
        {
            try
            {
                await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
                _logger.LogInformation("Item with key '{Key}' has been removed from local storage.", key);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing item with key '{Key}' from local storage.", key);
                return;
            }
        }
    }

    public interface ILocalStorageService
    {
        Task<T> GetItem<T>(string key);
        Task SetItem<T>(string key, T value);
        Task RemoveItem(string key);
    }
}
