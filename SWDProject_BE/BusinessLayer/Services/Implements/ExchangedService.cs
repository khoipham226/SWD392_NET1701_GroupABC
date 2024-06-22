using BusinessLayer.RequestModels;
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
    public class ExchangedService : IExchangedService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExchangedService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddExchangedAsync(Exchanged exchanged)
        {
            await _unitOfWork.Repository<Exchanged>().InsertAsync(exchanged);
            await _unitOfWork.CommitAsync();
        }

        public async Task AddExchangedProductAsync(ExchangedProduct exchangedProduct)
        {
            await _unitOfWork.Repository<ExchangedProduct>().InsertAsync(exchangedProduct);
            
            // update product status
            var product = await _unitOfWork.Repository<Product>().GetById(exchangedProduct.ProductId);
            if (product != null)
            {
                product.Status = false;
                await _unitOfWork.Repository<Product>().Update(product, product.Id);
            }
            await _unitOfWork.CommitAsync();

        }

        public async Task<IEnumerable<ExchangedResponseModel>> GetAllFinishedExchangedByUserIdAsync(int userId)
        {
            var exchangeds =  await _unitOfWork.Repository<Exchanged>()
                .GetAll()
                .Where(e => e.Status && (e.UserId == userId || e.Post.UserId == userId))
                .Include(e => e.User) 
                .Include(e => e.Post) 
                .ToListAsync();

            var exchangedResponseModels = exchangeds.Select(exchanged => new ExchangedResponseModel
            {
                Id = exchanged.Id,
                Description = exchanged.Description,
                Date = exchanged.Date,
                Status = (bool)exchanged.Status,
                User = new UserResponse
                {
                    Id = exchanged.User.Id,
                    UserName = exchanged.User.UserName,
                    ImgUrl = exchanged.User.ImgUrl,
                },
                Post = new PostResponse
                {
                    Id = exchanged.PostId,
                    Title = exchanged.Post.Title,
                    ImageUrl = exchanged.Post.ImageUrl
                }
            }).ToList();

            return exchangedResponseModels;
        }

        public async Task<IEnumerable<ExchangedResponseModel>> GetAllPendingExchangedByUserIdForCustomerAsync(int userId)
        {
            var exchangeds = await _unitOfWork.Repository<Exchanged>()
                .GetAll() 
                .Where(e => e.UserId == userId && !e.Status)
                .Include(e => e.User)
                .Include(e => e.Post) 
                .ToListAsync();

            var exchangedResponseModels = exchangeds.Select(exchanged => new ExchangedResponseModel
            {
                Id = exchanged.Id,
                Description = exchanged.Description,
                Date = exchanged.Date,
                Status = (bool)exchanged.Status,
                User = new UserResponse
                {
                    Id = exchanged.User.Id,
                    UserName = exchanged.User.UserName,
                    ImgUrl = exchanged.User.ImgUrl,
                },
                Post = new PostResponse
                {
                    Id = exchanged.PostId,
                    Title = exchanged.Post.Title,
                    ImageUrl = exchanged.Post.ImageUrl
                }
            }).ToList();

            return exchangedResponseModels;
        }

        public async Task<IEnumerable<ExchangedResponseModel>> GetAllPendingExchangedByUserIdForPosterAsync(int userId)
        {
            var exchangeds = await _unitOfWork.Repository<Exchanged>()
                .GetAll()
                .Include(e => e.User)
                .Include(e => e.Post)
                .Where(e => e.Post.UserId == userId && !e.Status)
                .ToListAsync();
            var exchangedResponseModels = exchangeds.Select(exchanged => new ExchangedResponseModel
            {
                Id = exchanged.Id,
                Description = exchanged.Description,
                Date = exchanged.Date,
                Status = (bool)exchanged.Status,
                User = new UserResponse
                {
                    Id = exchanged.User.Id,
                    UserName = exchanged.User.UserName,
                    ImgUrl = exchanged.User.ImgUrl,
                },
                Post = new PostResponse
                {
                    Id = exchanged.PostId,
                    Title = exchanged.Post.Title,
                    ImageUrl = exchanged.Post.ImageUrl
                }
            }).ToList();

            return exchangedResponseModels;
        }

        public async Task<Exchanged> GetExchangedByIdAsync(int id)
        {
            return await _unitOfWork.Repository<Exchanged>().GetById(id);
        }

        public async Task UpdateExchangedStatusAcceptAsync(int id)
        {
            var exchanged = await _unitOfWork.Repository<Exchanged>().GetById(id);
            if (exchanged == null)
            {
                throw new ArgumentException($"Exchanged with id {id} not found.");
            }

            // Update exchanged status
            exchanged.Status = true;

            // Get the associated post
            var post = await _unitOfWork.Repository<Post>().GetById(exchanged.PostId);
            if (post == null)
            {
                throw new ArgumentException($"Post with id {exchanged.PostId} not found for exchanged id {id}.");
            }

            // Update post statuses
            post.PublicStatus = false;
            post.ExchangedStatus = true;

            // Update entities and commit changes
            await _unitOfWork.Repository<Post>().Update(post, post.Id);
            await _unitOfWork.Repository<Exchanged>().Update(exchanged, id);

            // Delete related exchanges except the one being accepted
            var exchangesToDelete = await _unitOfWork.Repository<Exchanged>()
                .GetAll()
                .Where(e => e.PostId == exchanged.PostId && e.Id != id)
                .ToListAsync();

            foreach (var exchange in exchangesToDelete)
            {
                var exchangedProductsToDelete = await _unitOfWork.Repository<ExchangedProduct>()
                .GetAll()
                .Where(ep => ep.ExchangeId == exchange.Id)
                .ToListAsync();

                foreach (var exchangedProduct in exchangedProductsToDelete)
                {
                    var product = await _unitOfWork.Repository<Product>().GetById(exchangedProduct.ProductId);
                    if (product != null)
                    {
                        product.Status = true;
                        await _unitOfWork.Repository<Product>().Update(product, product.Id);
                    }
                    await _unitOfWork.Repository<ExchangedProduct>().HardDelete(exchangedProduct.Id);
                }

                await _unitOfWork.Repository<Exchanged>().HardDelete(exchange.Id);
            }

            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateExchangedStatusDenyAsync(int id)
        {
            var exchangedProductsToDelete = await _unitOfWork.Repository<ExchangedProduct>()
                .GetAll()
                .Where(ep => ep.ExchangeId == id)
                .ToListAsync();

            foreach (var exchangedProduct in exchangedProductsToDelete)
            {
                var product = await _unitOfWork.Repository<Product>().GetById(exchangedProduct.ProductId);
                if (product != null)
                {
                    product.Status = true;
                    await _unitOfWork.Repository<Product>().Update(product, product.Id);
                }
                await _unitOfWork.Repository<ExchangedProduct>().HardDelete(exchangedProduct.Id);
            }

            // Delete the exchanged entity itself
            await _unitOfWork.Repository<Exchanged>().HardDelete(id);

            // Commit changes
            await _unitOfWork.CommitAsync();
        }
    }
}
