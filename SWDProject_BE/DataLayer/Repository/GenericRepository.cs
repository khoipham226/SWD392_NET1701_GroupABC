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
    //public class GenericRepository<T> : IGenericRepository<T> where T : class
    //{
    //	////private static StarasContext Context;
    //	//private static DbSet<T> Table { get; set; }

    //	//public GenericRepository(StarasContext context)
    //	//{
    //	//	Context = context;
    //	//	Table = Context.Set<T>();
    //	//}

    //	public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
    //	{
    //		return Context.Set<T>().AsQueryable().Where(predicate).ToList();
    //	}
    //	public async Task<T> FindAsync(Expression<Func<T, bool>> predicate)
    //	{
    //		return
    //			await Table.SingleOrDefaultAsync(predicate);
    //	}

    //	public IQueryable<T> FindAll(Func<T, bool> predicate)
    //	{
    //		return Table.Where(predicate).AsQueryable();
    //	}

    //	public DbSet<T> GetAll()
    //	{
    //		return Table;
    //	}


    //	public async Task<T> GetByIdGuid(Guid Id)
    //	{
    //		return await Table.FindAsync(Id);
    //	}

    //	public async Task<T> GetById(int Id)
    //	{
    //		return await Table.FindAsync(Id);
    //	}

    //	public async Task HardDeleteGuid(Guid key)
    //	{
    //		var rs = await GetByIdGuid(key);
    //		Table.Remove(rs);
    //	}

    //	public async Task HardDelete(int key)
    //	{
    //		var rs = await GetById(key);
    //		Table.Remove(rs);
    //	}

    //	public void Insert(T entity)
    //	{
    //		Table.Add(entity);
    //	}

    //	public async Task UpdateGuid(T entity, Guid Id)
    //	{
    //		var existEntity = await GetByIdGuid(Id);
    //		Context.Entry(existEntity).CurrentValues.SetValues(entity);
    //		Table.Update(existEntity);
    //	}

    //	public async Task Update(T entity, int Id)
    //	{
    //		var existEntity = await GetById(Id);
    //		Context.Entry(existEntity).CurrentValues.SetValues(entity);
    //		Table.Update(existEntity);
    //	}


    //	public void UpdateRange(IQueryable<T> entities)
    //	{
    //		Table.UpdateRange(entities);
    //	}

    //	public void DeleteRange(IQueryable<T> entities)
    //	{
    //		Table.RemoveRange(entities);
    //	}

    //	public void InsertRange(IQueryable<T> entities)
    //	{
    //		Table.AddRange(entities);
    //	}

    //	public EntityEntry<T> Delete(T entity)
    //	{
    //		return Table.Remove(entity);
    //	}

    //	//async
    //	public async Task InsertAsync(T entity)
    //	{
    //		await Table.AddAsync(entity);
    //	}

    //	public async Task AddRangeAsync(IEnumerable<T> entities)
    //	{
    //		await Table.AddRangeAsync(entities);
    //	}

    //	public async Task InsertRangeAsync(IQueryable<T> entities)
    //	{
    //		await Table.AddRangeAsync(entities);
    //	}

    //	public async Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate)
    //	{
    //		return await Table.Where(predicate).ToListAsync();
    //	}

    //	public IQueryable<T> GetAllApart()
    //	{
    //		return Table.Take(100);
    //	}
    //	public async Task<IDbContextTransaction> BeginTransaction(CancellationToken cancellationToken = default)
    //	{
    //		return await Context.Database.BeginTransactionAsync(cancellationToken);
    //	}

    //	public async Task UpdateDetached(T entity)
    //	{
    //		Table.Update(entity);
    //	}

    //	public async Task DetachEntity(T entity)
    //	{
    //		Context.Entry(entity).State = EntityState.Detached;
    //	}

    //	public IQueryable<T> AsNoTracking()
    //	{
    //		return Table.AsNoTracking();
    //	}

    //	public IQueryable<T> AsQueryable()
    //	{
    //		return Context.Set<T>().AsQueryable();
    //	}

    //	public IQueryable<T> AsQueryable(Expression<Func<T, bool>> predicate)
    //	{
    //		return Context.Set<T>().AsQueryable().Where(predicate);
    //	}

    //	public IQueryable<TResult?> ObjectMapper<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
    //	{
    //		IQueryable<T> query = Table;
    //		if (include != null) query = include(query);

    //		if (predicate != null) query = query.Where(predicate);

    //		return query.AsNoTracking().Select(selector);
    //	}

    //	public IQueryable<TResult?> ObjectMapper<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
    //	{
    //		throw new NotImplementedException();
    //	}
    //}
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly SWD392_DBContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(SWD392_DBContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task InsertAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task InsertRangeAsync(IQueryable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public DbSet<TEntity> GetAll()
        {
            return _dbSet;
        }

        public IQueryable<TEntity> GetAllApart()
        {
            return _dbSet.AsNoTracking();
        }

        public IQueryable<User> GetWhere(Expression<Func<TEntity, bool>> predicate)
        {
            return (IQueryable<User>)_dbSet.Where(predicate);
        }

        public IQueryable<TEntity> FindAll(Func<TEntity, bool> predicate)
        {
            return _dbSet.Where(predicate).AsQueryable();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public async Task<TEntity> GetById(int Id)
        {
            return await _dbSet.FindAsync(Id);
        }

        public async Task<TEntity> GetByIdGuid(Guid Id)
        {
            return await _dbSet.FindAsync(Id);
        }

        public void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public async Task Update(TEntity entity, int Id)
        {
            _dbSet.Update(entity);
        }

        public async Task UpdateGuid(TEntity entity, Guid Id)
        {
            _dbSet.Update(entity);
        }

        public void UpdateRange(IQueryable<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
        }

        public async Task HardDelete(int key)
        {
            var entity = await _dbSet.FindAsync(key);
            if (entity != null) _dbSet.Remove(entity);
        }

        public async Task HardDeleteGuid(Guid key)
        {
            var entity = await _dbSet.FindAsync(key);
            if (entity != null) _dbSet.Remove(entity);
        }

        public void DeleteRange(IQueryable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void InsertRange(IQueryable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }

        public EntityEntry<TEntity> Delete(TEntity entity)
        {
            return _dbSet.Remove(entity);
        }

        public async Task<IDbContextTransaction> BeginTransaction(CancellationToken cancellationToken = default)
        {
            return await _context.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task UpdateDetached(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public async Task DetachEntity(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }

        public IQueryable<TEntity> AsNoTracking()
        {
            return _dbSet.AsNoTracking();
        }

        public IQueryable<TEntity> AsQueryable()
        {
            return _dbSet.AsQueryable();
        }

        public IQueryable<TEntity> AsQueryable(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate).AsQueryable();
        }

        public IQueryable<TResult?> ObjectMapper<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return query.Select(selector);
        }
    }

}
