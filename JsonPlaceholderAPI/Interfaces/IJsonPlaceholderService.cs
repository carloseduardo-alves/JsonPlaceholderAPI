using JsonPlaceholderAPI.Models;

namespace JsonPlaceholderAPI.Interfaces
{
    public interface IJsonPlaceholderService
    {
        Task<List<Post>> GetPostsAsync();
        Task<Post?> GetPostByIdAsync(int id);
        Task<Post?> CreatePostAsync(Post post);
        Task<Post?> UpdatePostAsync(int id, Post post);
        Task<bool> DeletePostAsync(int id);
    }
}
