using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagement.EntityFramework;
using HotelManagement.Models;

namespace HotelManagement.Repository.Impl
{
	public class ResetPasswordAnswerRepository : BaseRepository<ResetPasswordAnswers>, IResetPasswordAnswerRepository
	{
		private readonly TransactionContext _context;

		public ResetPasswordAnswerRepository(TransactionContext context) : base(context)
		{
			_context = context;
		}

	}
}