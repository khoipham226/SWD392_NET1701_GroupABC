using BusinessLayer.ResponseModels;
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

        public async Task<IEnumerable<PostResponseModel>> GetAllValidPostsAsync()
        {
            var posts = await _unitOfWork.Repository<Post>()
                            .GetAll()
                            .Where(p => p.PublicStatus == true && p.ExchangedStatus == false)
                            .Include(p => p.User)
                            .Include(p => p.Product)
                            .ToListAsync();

            var postResponseModels = posts.Select(post => new PostResponseModel
            {
                Id = post.Id,
                Title = post.Title,
                Description = post.Description,
                Date = post.Date,
                PublicStatus = (bool)post.PublicStatus,
                User = new UserResponse
                {
                    Id = post.User.Id,
                    UserName = post.User.UserName,
                    ImgUrl = post.User.ImgUrl,
                },
                Product = new ProductResponse
                {
                    Id = post.Product.Id,
                    Name = post.Product.Name,
                    UrlImg = post.Product.UrlImg
                }
            }).ToList();

            return postResponseModels;
        }

        public async Task<IEnumerable<PostResponseModel>> GetAllUnpublicPostsAsync()
        {
            var posts = await _unitOfWork.Repository<Post>()
                            .GetAll()
                            .Where(p => p.PublicStatus == false &&  p.ExchangedStatus == false)
                            .Include(p => p.User)
                            .Include(p => p.Product)
                            .ToListAsync();

            var postResponseModels = posts.Select(post => new PostResponseModel
            {
                Id = post.Id,
                Title = post.Title,
                Description = post.Description,
                Date = post.Date,
                PublicStatus = (bool)post.PublicStatus,
                User = new UserResponse
                {
                    Id = post.User.Id,
                    UserName = post.User.UserName,
                    ImgUrl = post.User.ImgUrl,
                },
                Product = new ProductResponse
                {
                    Id = post.Product.Id,
                    Name = post.Product.Name,
                    UrlImg = post.Product.UrlImg
                }
            }).ToList();

            return postResponseModels;
        }

        public async Task<IEnumerable<PostResponseModel>> GetAllPostsByUserIdAsync(int userId)
        {
            var posts = await _unitOfWork.Repository<Post>()
                                 .GetAll()
                                 .Where(p => p.UserId == userId)
                                 .Include(p => p.User)
                                 .Include(p => p.Product)
                                 .ToListAsync();

            var postResponseModels = posts.Select(post => new PostResponseModel
            {
                Id = post.Id,
                Title = post.Title,
                Description = post.Description,
                Date = post.Date,
                PublicStatus = (bool)post.PublicStatus,
                User = new UserResponse
                {
                    Id = post.User.Id,
                    UserName = post.User.UserName,
                    ImgUrl = post.User.ImgUrl,
                },
                Product = new ProductResponse
                {
                    Id = post.Product.Id,
                    Name = post.Product.Name,
                    UrlImg = post.Product.UrlImg
                }
            }).ToList();

            return postResponseModels;
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

        public async Task UpdatePostStatusAsync(int id, bool newPublicStatus)
        {
            // Retrieve the post by its id
            var post = await _unitOfWork.Repository<Post>().GetById(id);

            post.PublicStatus = newPublicStatus;

            // Update the post in the repository
            await _unitOfWork.Repository<Post>().Update(post, post.Id);

            // Commit the transaction
            await _unitOfWork.CommitAsync();
        }

        public async Task DeletePostAsync(int id)
        {
            await _unitOfWork.Repository<Post>().HardDelete(id);
            await _unitOfWork.CommitAsync();
        }
    }
}
