using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transaction.Api.src.EntityFramework;
using Transaction.Api.src.Models;

namespace Transaction.Api.src.Repository.Impl
{
	public class ResetPasswordAnswerRepository : IResetPasswordAnswerRepository
	{
		private readonly TransactionContext _context;

		public ResetPasswordAnswerRepository(TransactionContext context)
		{
			_context = context;
		}

		public ResetPasswordAnswers add(ResetPasswordAnswers obj)
		{
			throw new NotImplementedException();
		}

		public ResetPasswordAnswers Get(int id)
		{
			return _context.ResetPasswordAnswers.Where(x=>x.CustomerId  == id).FirstOrDefault();
		}

		public List<ResetPasswordAnswers> GetAll()
		{
			throw new NotImplementedException();
		}

		public void Update(ResetPasswordAnswers obj)
		{
			throw new NotImplementedException();
		}
	}
}