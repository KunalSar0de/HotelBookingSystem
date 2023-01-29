using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Transaction.Api.src.EntityFramework;
using Transaction.Api.src.Models;

namespace Transaction.Api.src.Repository.Impl
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