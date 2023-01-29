using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagement.Enums;

namespace HotelManagement.Models
{
    public class Customers
    {
        public int Id { get; set; }
		public string? UserName { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public UserType UserType { get; set; }
		public bool IsActive { get; set; }
		public DateTime CreatedOn { get; set; }

		public void SetCreatedOn()
		{
			CreatedOn = DateTime.Now;
			IsActive = true;
		}

    }
}