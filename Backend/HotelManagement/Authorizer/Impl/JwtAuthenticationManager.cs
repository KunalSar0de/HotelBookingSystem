using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using HotelManagement.Dto.Response;
using HotelManagement.Enums;
using HotelManagement.Models;
using HotelManagement.Repository;
using HotelManagement.Service;
using HotelManagement.Exception;

namespace HotelManagement.Authorizer.Impl
{
	public class JwtAuthenticationManager : IJwtAuthenticationManager
	{
		private readonly IConfiguration _configuration;
		private readonly IPasswordRepository _passwordRespository;
		private readonly IPasswordManagementService _passwordManagementService;
		private readonly ICustomerService _customerService;
		private readonly IIdEncodeAndDecode _iIdEncodeAndDecode;

		public JwtAuthenticationManager(IPasswordRepository passwordRespository,
		IPasswordManagementService passwordManagementService,
		ICustomerService customerService, IConfiguration configuration,
		IIdEncodeAndDecode iIdEncodeAndDecode)
		{
			_passwordRespository = passwordRespository;
			_passwordManagementService = passwordManagementService;
			_customerService = customerService;
			_configuration = configuration;
			_iIdEncodeAndDecode = iIdEncodeAndDecode;
		}
		

		public TokenResponse Authenticate(string userName, string password)
		{
			if (string.IsNullOrEmpty(userName))
				throw new ValidationException(ValidationMessage.InvalidUserName);

			if (string.IsNullOrEmpty(password))
				throw new ValidationException(ValidationMessage.InvalidPassword);


			var customerDetails = _customerService.GetCustomerByUserName(userName);

			var criticalData = _passwordRespository.GetByCustomerId(customerDetails.Id);

			string decodedPassword = string.Empty;
			if (criticalData != null && criticalData.Password != null)
				decodedPassword = _passwordManagementService.DecodePassword(criticalData.CustomerId, criticalData.Password);


			if (decodedPassword == password)
				return CreateJwtToken(customerDetails.Id, customerDetails);
			return new TokenResponse();

		}


		private TokenResponse CreateJwtToken(int customerId, Customers customerDetails)
		{
			try
			{
				var key = Encoding.ASCII.GetBytes(_configuration["Token:Secret"]);

				var JWToken = new JwtSecurityToken(
					issuer: _configuration["Token:Issuer"],
					audience: _configuration["Token:Audiance"],
					claims: GetUserClaims(customerDetails),
					signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
				);

				var token = new JwtSecurityTokenHandler().WriteToken(JWToken);

				var tokenResponse = new TokenResponse
				{
					Token = token,
					CreatedOn = DateTime.Now,
					CustomerId = _iIdEncodeAndDecode.EncodeId(customerDetails.Id),
					UserType = (UserType)customerDetails.UserType
				};
				return tokenResponse;
			}
			catch (System.Exception ex)
			{
				Console.WriteLine("Exception : " + ex);
				return new TokenResponse();
			}

		}


		private IEnumerable<Claim> GetUserClaims(Customers user)
		{
			List<Claim> claims = new List<Claim>();
			if (user != null && user.UserName != null)
			{
				claims.Add(new Claim("UserName", user.UserName));
				claims.Add(new Claim("Country", "IN"));
				claims.Add(new Claim("Role", user.UserType.ToString()));
			};
			return claims.AsEnumerable<Claim>();
		}
	}
}