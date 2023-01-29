using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transaction.Api.src.Models;

namespace Transaction.Api.src.Repository
{
    public interface ICustomerRepository 
    {
		List<Customers> GetCustomersBySearchCriteria();

		Customers GetCustomerByUserName(string userName);
    }
}