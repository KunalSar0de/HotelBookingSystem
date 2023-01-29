using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Transaction.Api.src.Assembler;
using Transaction.Api.src.Dto;
using Transaction.Api.src.Dto.Request;
using Transaction.Api.src.Enums;
using Transaction.Api.src.Models;
using Transaction.Api.src.Service;

namespace Transaction.Api.src.Controller
{
	public class CustomerController : BaseController
	{

		private readonly ICustomerService _customerService;
		private readonly IAssembler<Customers , CustomerResponseDto> _customerResponseAssembler;
		private readonly IPasswordManagementService _passwordManagementService;

		public CustomerController(IIdEncodeAndDecode idEncodeAndDecode, ICustomerService customerService, IAssembler<Customers, CustomerResponseDto> customerResponseAssembler, IPasswordManagementService passwordManagementService) : base(idEncodeAndDecode)
		{
			_customerService = customerService;
			_customerResponseAssembler = customerResponseAssembler;
			_passwordManagementService = passwordManagementService;
		}

		// [Authorize]
		[Route("create/customer")]
		[HttpPost]
		public IActionResult Post([FromBody]CustomerRequest customer)
		{
			if(customer == null)
				return BadRequest();
			
			var newlyAddedCustomer = _customerService.CreateNewCustomer(customer);
			var response = _customerResponseAssembler.GetDTO(newlyAddedCustomer);
			return Ok(response);
		}


		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
		[HttpGet]
		[Route("customer/{parentId}")]
		public IActionResult Get([Bind (Prefix = "parentId")] string parentId)
		{
			var decodedId = DecodeId(parentId);
			if(decodedId == 0)
				return BadRequest();

			var customerResponse = _customerService.GetCustomerById(decodedId);
			if(customerResponse == null)
				return NotFound();

			return Ok(_customerResponseAssembler.GetDTO(customerResponse));
		}

		[HttpPut]
		[Route("customer/{parentId}")]
		public IActionResult Put([Bind (Prefix = "parentId")] string parentId,[FromBody] CustomerRequest customerRequest )
		{
			if(customerRequest == null)
				return BadRequest();

			var customer = _customerService.CreateNewCustomer(customerRequest);
			_customerService.UpdateCustomer(customer);
			return Created("~/customer/{parentId}",customer);
		}

		[HttpPost]
		[Route("customer/{parentId}/activate")]
		public IActionResult Activate([Bind (Prefix = "parentId")] string parentId)
		{
			var decodedId = DecodeId(parentId);
			
			if(decodedId == 0)
				return BadRequest();
			
			_customerService.ActivateMerchant(decodedId);
			return Ok();
		}


		[HttpDelete]
		[Route("customer/{parentId}/deactivate")]
		public IActionResult DeActivate([Bind (Prefix = "parentId")] string parentId)
		{
			var decodedId = DecodeId(parentId);
			
			if(decodedId == 0)
				return BadRequest();
			
			_customerService.DeactivateMerchant(decodedId);
			return Ok();
		}
	}
}