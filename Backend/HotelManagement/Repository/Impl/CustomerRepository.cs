using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HotelManagement.EntityFramework;
using HotelManagement.Models;

namespace HotelManagement.Repository.Impl
{
	public class CustomerRepository : BaseRepository<Customers> , ICustomerRepository
	{
		private readonly TransactionContext _dbContext;

		public CustomerRepository(TransactionContext context) : base(context)
		{
			_dbContext = context;
		}

		public Customers GetCustomerByUserName(string userName)
		{
			throw new NotImplementedException();
		}

		public List<Customers> GetCustomersBySearchCriteria()
		{
			throw new NotImplementedException();
		}
	}
}