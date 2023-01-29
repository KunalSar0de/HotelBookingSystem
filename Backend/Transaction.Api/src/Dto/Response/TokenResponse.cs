using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transaction.Api.src.Enums;

namespace Transaction.Api.src.Dto.Response
{
    public class TokenResponse
    {
        public string Token { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CustomerId { get; set; }
		public UserType UserType { get; set; }
    }
}