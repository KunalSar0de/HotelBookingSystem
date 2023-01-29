using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transaction.Api.src.Dto.Request;
using Transaction.Api.src.EntityFramework;
using Transaction.Api.src.Enums;
using Transaction.Api.src.Models;
using Transaction.Api.src.Repository;

namespace Transaction.Api.src.Service.Impl
{
	public class CustomerService : ICustomerService
	{
		private readonly ICustomerRepository _customerRepository;
		private readonly TransactionContext _context;
		private readonly IIdEncodeAndDecode _iidEncodeAndDecode;
		private readonly IPasswordManagementService _passwordManagementService;
		private readonly IPasswordRepository _passwordRepository;
		public CustomerService(ICustomerRepository customerRepository, TransactionContext context, IIdEncodeAndDecode iidEncodeAndDecode, IPasswordManagementService passwordManagementService, IPasswordRepository passwordRepository)
		{
			_customerRepository = customerRepository;
			_context = context;
			_iidEncodeAndDecode = iidEncodeAndDecode;
			_passwordManagementService = passwordManagementService;
			_passwordRepository = passwordRepository;
		}


		public Customers CreateNewCustomer(CustomerRequest newCustomer)
		{
			var customer = GetCustomerObj(newCustomer);
			customer.SetCreatedOn();
			var newlyAddedcustomer = _customerRepository.Add(customer); 
			_context.SaveChanges();
			SetPassword(newlyAddedcustomer, newCustomer);
			_context.SaveChanges();

			return newlyAddedcustomer;
		}

		private Customers GetCustomerObj(CustomerRequest newCustomer)
		{
			return new Customers{
				UserName = newCustomer.UserName,
				FirstName = newCustomer.FirstName,
				LastName = newCustomer.LastName,
				UserType = UserType.Customer,
			};
		}

		public List<Customers> GetAllCustomer()
		{
			var listOfCustomers = _customerRepository.GetAll();
			return listOfCustomers;
		}

		public Customers GetCustomerById(int id)
		{

			var customerById = _customerRepository.Get(id);
			
			if(customerById != null)
				return customerById;
			return new Customers();
		}

		public void UpdateCustomer(Customers customer)
		{
			if(customer != null)
				_customerRepository.Update(customer);
			_context.SaveChanges();
		}

		private void SetPassword(Customers newlyAddedcustomer, CustomerRequest newCustomer)
		{
			var criticalData = new CriticalData()
			{
				CustomerId =  newlyAddedcustomer.Id,
				Password = _passwordManagementService.GeneratePassword(newCustomer.Password, newlyAddedcustomer.Id)
			};
			_passwordRepository.add(criticalData);
		}

		public void ActivateMerchant(int id)
		{
			try{
				var customerById = GetCustomerById(id);
				customerById.IsActive = true;
				_customerRepository.Update(customerById);
				_context.SaveChanges();
			}catch(SystemException ex){
				Console.WriteLine("Error in Customer Activation : "+ex);
				throw;	
			}
			 
		}

		public void DeactivateMerchant(int id)
		{
			try{
				var customerById = GetCustomerById(id);
				customerById.IsActive = false;
				_customerRepository.Update(customerById);
				_context.SaveChanges();
			}catch(SystemException ex){
				Console.WriteLine("Error in Customer DeActivation : "+ex);
				throw;	
			}
		}

		public Customers GetCustomerByUserName(string userName)
		{
			var customer = _customerRepository.GetCustomerByUserName(userName);
			if(customer != null)
				return customer;
			return null;
		}
	}
}