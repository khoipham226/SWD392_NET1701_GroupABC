using DataLayer.Model;
using DataLayer.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Implements
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PostService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            return await _unitOfWork.Repository<Post>().GetAll().ToListAsync();
        }

        public async Task<Post> GetPostByIdAsync(int id)
        {
            return await _unitOfWork.Repository<Post>().GetById(id);
        }

        public async Task AddPostAsync(Post post)
        {
            await _unitOfWork.Repository<Post>().InsertAsync(post);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdatePostAsync(Post post)
        {
            await _unitOfWork.Repository<Post>().Update(post, post.Id);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeletePostAsync(int id)
        {
            await _unitOfWork.Repository<Post>().HardDelete(id);
            await _unitOfWork.CommitAsync();
        }
    }
}
