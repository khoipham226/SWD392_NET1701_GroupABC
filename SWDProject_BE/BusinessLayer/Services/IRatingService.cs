using BusinessLayer.RequestModels.Rating;
using BusinessLayer.ResponseModels.Rating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public interface IRatingService
    {
        Task<List<RatingResponseModel>> GetAll();
        Task<bool> RatingPost(int userId, RatingRequestModel dto);
        Task<bool> GetRatingByUser(int userId, int postId);
        Task<int> CountRating(int userId);
    }
}
