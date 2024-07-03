using DataLayer.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
	public class MessageRepository : IMessageRepository
	{
		public Message Add(Message message)
		{
			throw new NotImplementedException();
		}

		public Task AddRangeAsync(IEnumerable<Message> entities)
		{
			throw new NotImplementedException();
		}

		public IQueryable<Message> AsNoTracking()
		{
			throw new NotImplementedException();
		}

		public IQueryable<Message> AsQueryable()
		{
			throw new NotImplementedException();
		}

		public IQueryable<Message> AsQueryable(Expression<Func<Message, bool>> predicate)
		{
			throw new NotImplementedException();
		}

		public Task<IDbContextTransaction> BeginTransaction(CancellationToken cancellationToken = default)
		{
			throw new NotImplementedException();
		}

		public EntityEntry<Message> Delete(Message entity)
		{
			throw new NotImplementedException();
		}

		public void DeleteRange(IQueryable<Message> entities)
		{
			throw new NotImplementedException();
		}

		public Task DetachEntity(Message entity)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Message> Find(Expression<Func<Message, bool>> predicate)
		{
			throw new NotImplementedException();
		}

		public IQueryable<Message> FindAll(Func<Message, bool> predicate)
		{
			throw new NotImplementedException();
		}

		public Task<Message> FindAsync(Expression<Func<Message, bool>> predicate)
		{
			throw new NotImplementedException();
		}

		public object FindByPostId(int postId)
		{
			throw new NotImplementedException();
		}

		public DbSet<Message> GetAll()
		{
			throw new NotImplementedException();
		}

		public IQueryable<Message> GetAllApart()
		{
			throw new NotImplementedException();
		}

		public Task<Message> GetById(int Id)
		{
			throw new NotImplementedException();
		}

		public Task<Message> GetByIdGuid(Guid Id)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Message>> GetWhere(Expression<Func<Message, bool>> predicate)
		{
			throw new NotImplementedException();
		}

		public Task HardDelete(int key)
		{
			throw new NotImplementedException();
		}

		public Task HardDeleteGuid(Guid key)
		{
			throw new NotImplementedException();
		}

		public void Insert(Message entity)
		{
			throw new NotImplementedException();
		}

		public Task InsertAsync(Message entity)
		{
			throw new NotImplementedException();
		}

		public void InsertRange(IQueryable<Message> entities)
		{
			throw new NotImplementedException();
		}

		public Task InsertRangeAsync(IQueryable<Message> entities)
		{
			throw new NotImplementedException();
		}

		public IQueryable<TResult?> ObjectMapper<TResult>(Expression<Func<Message, TResult>> selector, Expression<Func<Message, bool>> predicate = null, Func<IQueryable<Message>, IIncludableQueryable<Message, object>> include = null)
		{
			throw new NotImplementedException();
		}

		public Task Update(Message entity, int Id)
		{
			throw new NotImplementedException();
		}

		public Task UpdateDetached(Message entity)
		{
			throw new NotImplementedException();
		}

		public Task UpdateGuid(Message entity, Guid Id)
		{
			throw new NotImplementedException();
		}

		public void UpdateRange(IQueryable<Message> entities)
		{
			throw new NotImplementedException();
		}
	}
}
