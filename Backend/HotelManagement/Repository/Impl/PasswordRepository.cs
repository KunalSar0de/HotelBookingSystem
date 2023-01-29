using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HotelManagement.EntityFramework;
using HotelManagement.Models;

namespace HotelManagement.Repository.Impl
{
	public class PasswordRepository : BaseRepository<CriticalData> , IPasswordRepository
	{
		private readonly TransactionContext _context;

		public PasswordRepository(TransactionContext context) : base(context)
		{
			_context = context;
		}


		
		public CriticalData GetByCustomerId(int customerId)
		{
			if(_context.CriticalData != null)
			{
				var criticalData = _context.CriticalData.Where(x => x.CustomerId == customerId).FirstOrDefault();
				if(criticalData != null)
					return criticalData;
			}
			return new CriticalData(); 
		}

	}
}