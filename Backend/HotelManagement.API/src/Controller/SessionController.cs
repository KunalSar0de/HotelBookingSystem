using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using HotelManagement.Authorizer;
using HotelManagement.Dto.Request;
using HotelManagement.Enums;
using HotelManagement.Models;
using HotelManagement.Service;
//https://www.c-sharpcorner.com/article/login-and-role-based-custom-authentication-in-asp-net-core-3-1/

namespace HotelManagement.API.src.Controller
{
	public class SessionController : BaseController
	{
		private readonly IConfiguration _configuration;
		private readonly ICustomerService _customerService;
		private readonly IJwtAuthenticationManager _jwtAuthenticationManager;
		
		private readonly IPasswordManagementService _passwordManagementService;
		public SessionController(IIdEncodeAndDecode idEncodeAndDecode, IConfiguration configuration, ICustomerService customerService, IJwtAuthenticationManager jwtAuthenticationManager, IPasswordManagementService passwordManagementService) : base(idEncodeAndDecode)
		{
			_configuration = configuration;
			_customerService = customerService;
			_jwtAuthenticationManager = jwtAuthenticationManager;
			_passwordManagementService = passwordManagementService;
		}

		[HttpPost]
		[Route("user/session")]
		public IActionResult Post([FromBody]LoginRequest loginRequest)
		{
			if(loginRequest == null)
				return BadRequest();

			var token = _jwtAuthenticationManager.Authenticate(loginRequest.UserName,loginRequest.Password);

			if(token == null)
				return Unauthorized();
				
			return Ok(token);
		}
		
		
		[HttpPost]
		[Route("user/reset/password")]
		public IActionResult ResetPassword(ResetPasswordRequest resetPasswordRequest)
		{
			var pass = _passwordManagementService.GeneratePassword("AdminUser@123",'1');
			return Ok();
		}
	}
}