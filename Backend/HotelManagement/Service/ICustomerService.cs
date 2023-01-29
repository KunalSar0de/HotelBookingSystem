using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagement.Dto.Request;
using HotelManagement.Models;

namespace HotelManagement.Service
{
    public interface ICustomerService
    {
        Customers CreateNewCustomer(CustomerRequest newCustomer);
		Customers GetCustomerById(int id);
		List<Customers> GetAllCustomer();
		void UpdateCustomer(Customers customer);
		void ActivateMerchant(int id);
		void DeactivateMerchant(int id);
		Customers GetCustomerByUserName(string userName);

    }
}