using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagement.Enums;

namespace HotelManagementrc.Dto
{
    public class CustomerResponseDto
    {
        public string? CustomerId { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public bool IsActive { get; set; }
		public DateTime CreatedOn { get; set; }
		public UserType UserType { get; set; }
    }
}