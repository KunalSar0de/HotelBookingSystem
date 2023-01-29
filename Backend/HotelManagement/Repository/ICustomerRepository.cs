using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagement.Models;

namespace HotelManagement.Repository
{
    public interface ICustomerRepository : IBaseRepository<Customers>
    {
		List<Customers> GetCustomersBySearchCriteria();

		Customers GetCustomerByUserName(string userName);
    }
}