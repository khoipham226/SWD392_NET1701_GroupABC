using DataLayer.Model;
using DataLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Implements
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Comment>> GetAllCommentsByPostAsync(int postId)
        {
            return await _unitOfWork.Repository<Comment>().GetWhere(c => c.PostId == postId);
        }

        public async Task<Comment> GetCommentByIdAsync(int id)
        {
            return await _unitOfWork.Repository<Comment>().GetById(id);
        }

        public async Task AddCommentAsync(Comment comment)
        {
            await _unitOfWork.Repository<Comment>().InsertAsync(comment);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateCommentAsync(Comment comment)
        {
            await _unitOfWork.Repository<Comment>().Update(comment, comment.Id);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteCommentAsync(int id)
        {
            await _unitOfWork.Repository<Comment>().HardDelete(id);
            await _unitOfWork.CommitAsync();
        }
    }
}
