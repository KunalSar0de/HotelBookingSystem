using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HotelManagement.EntityFramework;

namespace HotelManagement.Repository.Impl
{
	public class BaseRepository<T> : IBaseRepository<T> where T : class
	{
		private readonly TransactionContext _dbContext;

		public BaseRepository(TransactionContext dbContext)
		{
			_dbContext = dbContext;
		}

		public T Add(T entity)
		{
			_dbContext.Set<T>().Add(entity);
			_dbContext.SaveChanges();
			return entity;
		}

		public T Get(int id)
		{
			return _dbContext.Set<T>().Find(id);
			
		}

		public List<T> GetAll()
		{
			return _dbContext.Set<T>().ToList();
		}

		public void Update(T entity)
		{
			_dbContext.Entry(entity).State = EntityState.Modified;
		}
	}
}