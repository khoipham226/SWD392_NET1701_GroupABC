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
	public class GroupRepository : IGroupRepository
	{
		public Group Add(Group group)
		{
			throw new NotImplementedException();
		}

		public Task AddRangeAsync(IEnumerable<Group> entities)
		{
			throw new NotImplementedException();
		}

		public IQueryable<Group> AsNoTracking()
		{
			throw new NotImplementedException();
		}

		public IQueryable<Group> AsQueryable()
		{
			throw new NotImplementedException();
		}

		public IQueryable<Group> AsQueryable(Expression<Func<Group, bool>> predicate)
		{
			throw new NotImplementedException();
		}

		public Task<IDbContextTransaction> BeginTransaction(CancellationToken cancellationToken = default)
		{
			throw new NotImplementedException();
		}

		public EntityEntry<Group> Delete(Group entity)
		{
			throw new NotImplementedException();
		}

		public void DeleteRange(IQueryable<Group> entities)
		{
			throw new NotImplementedException();
		}

		public Task DetachEntity(Group entity)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Group> Find(Expression<Func<Group, bool>> predicate)
		{
			throw new NotImplementedException();
		}

		public IQueryable<Group> FindAll(Func<Group, bool> predicate)
		{
			throw new NotImplementedException();
		}

		public List<Group> FindAllByUserId(int userId)
		{
			throw new NotImplementedException();
		}

		public Task<Group> FindAsync(Expression<Func<Group, bool>> predicate)
		{
			throw new NotImplementedException();
		}

		public DbSet<Group> GetAll()
		{
			throw new NotImplementedException();
		}

		public IQueryable<Group> GetAllApart()
		{
			throw new NotImplementedException();
		}

		public Task<Group> GetById(int Id)
		{
			throw new NotImplementedException();
		}

		public Task<Group> GetByIdGuid(Guid Id)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Group>> GetWhere(Expression<Func<Group, bool>> predicate)
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

		public void Insert(Group entity)
		{
			throw new NotImplementedException();
		}

		public Task InsertAsync(Group entity)
		{
			throw new NotImplementedException();
		}

		public void InsertRange(IQueryable<Group> entities)
		{
			throw new NotImplementedException();
		}

		public Task InsertRangeAsync(IQueryable<Group> entities)
		{
			throw new NotImplementedException();
		}

		public IQueryable<TResult?> ObjectMapper<TResult>(Expression<Func<Group, TResult>> selector, Expression<Func<Group, bool>> predicate = null, Func<IQueryable<Group>, IIncludableQueryable<Group, object>> include = null)
		{
			throw new NotImplementedException();
		}

		public Task Update(Group entity, int Id)
		{
			throw new NotImplementedException();
		}

		public Task UpdateDetached(Group entity)
		{
			throw new NotImplementedException();
		}

		public Task UpdateGuid(Group entity, Guid Id)
		{
			throw new NotImplementedException();
		}

		public void UpdateRange(IQueryable<Group> entities)
		{
			throw new NotImplementedException();
		}
	}
}
