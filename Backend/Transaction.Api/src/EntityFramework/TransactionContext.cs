using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Transaction.Api.src.Models;

namespace Transaction.Api.src.EntityFramework
{
	public class TransactionContext : DbContext
	{
		public TransactionContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<Customers> Customer { get; set; }
		public DbSet<CriticalData> CriticalData { get; set; }
		public DbSet<ResetPasswordAnswers> ResetPasswordAnswers { get; set; }
		public DbSet<LoginQuestion> LoginQuestion { get; set; }
	}


}