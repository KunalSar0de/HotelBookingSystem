using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Models
{
    public class ResetPasswordAnswers
    {
        public int Id { get; set; }
		public int CustomerId { get; set; }
		public int QuestionId { get; set; }
		public string? FirstQuestionAns { get; set; }
		public string? SecondQuestionAns { get; set; }
    }
}