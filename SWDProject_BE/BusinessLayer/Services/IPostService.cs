using BusinessLayer.ResponseModels;
using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public interface IPostService
    {
        Task<IEnumerable<PostResponseModel>> GetAllPostsAsync();
        Task<IEnumerable<PostResponseModel>> GetAllPostsByUserIdAsync(int userId);
        Task<Post> GetPostByIdAsync(int id);
        Task AddPostAsync(Post post);
        Task UpdatePostAsync(Post post);
        Task DeletePostAsync(int id);
    }
}
