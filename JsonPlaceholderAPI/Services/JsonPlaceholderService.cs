
using JsonPlaceholderAPI.Interfaces;
using JsonPlaceholderAPI.Models;
using Microsoft.VisualBasic;
using System.Text;
using System.Text.Json;

namespace JsonPlaceholderAPI.Services
{
    public class JsonPlaceholderService : IJsonPlaceholderService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<JsonPlaceholderService> _logger;

        public JsonPlaceholderService(IHttpClientFactory httpClientFactory, ILogger<JsonPlaceholderService> logger)
        {
            _httpClient = httpClientFactory.CreateClient("JsonPlaceholder");
            _logger = logger;
        }

        public async Task<List<Post>> GetPostsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("posts");
                if (!response.IsSuccessStatusCode)
                    return new List<Post>();

                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Post>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar posts.");
                return new List<Post>();
            }
        }

        public async Task<Post?> GetPostByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"posts/{id}");
                if (!response.IsSuccessStatusCode) return null;

                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Post>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar post com ID {Id}.", id);
                return null;
            }
        }

        public async Task<Post?> CreatePostAsync(Post post)
        {
            try
            {
                var postJson = new StringContent(JsonSerializer.Serialize(post), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("posts", postJson);
                if (!response.IsSuccessStatusCode) return null;

                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Post>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar post.");
                return null;
            }
        }

        public async Task<Post?> UpdatePostAsync(int id, Post post)
        {
            try
            {
                var postJson = new StringContent(JsonSerializer.Serialize(post), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"posts/{id}", postJson);
                if (!response.IsSuccessStatusCode) return null;

                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Post>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar post com ID {Id}.", id);
                return null;
            }
        }

        public async Task<bool> DeletePostAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"posts/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao excluir post com ID {Id}.", id);
                return false;
            }
        }
    }
}
