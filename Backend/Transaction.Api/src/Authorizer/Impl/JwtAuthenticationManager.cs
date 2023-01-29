using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Transaction.Api.src.Dto.Response;
using Transaction.Api.src.Enums;
using Transaction.Api.src.Exception;
using Transaction.Api.src.Models;
using Transaction.Api.src.Repository;
using Transaction.Api.src.Service;

namespace Transaction.Api.src.Authorizer.Impl
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
			if(string.IsNullOrEmpty(userName))
				throw new SystemException(ValidationMessage.InvalidUserName);
			
			if(string.IsNullOrEmpty(password))
				throw new SystemException(ValidationMessage.InvalidPassword);

			
			var customerDetails = _customerService.GetCustomerByUserName(userName);

			var criticalData = _passwordRespository.GetByCustomerId(customerDetails.Id);
			var decodedPassword = _passwordManagementService.DecodePassword(criticalData.CustomerId,criticalData.Password);
						

			if(decodedPassword == password)
				return CreateJwtToken(customerDetails.Id , customerDetails);
			return null;

		}

		private TokenResponse CreateJwtToken(int customerId , Customers customerDetails)
		{
			try{
				var key = Encoding.ASCII.GetBytes(_configuration["Token:Secret"]);  
			
				var JWToken = new JwtSecurityToken(  
					issuer: _configuration["Token:Issuer"],  
					audience: _configuration["Token:Audiance"],  
					claims: GetUserClaims(customerDetails),   
					signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)  
				);  
				
				var token = new JwtSecurityTokenHandler().WriteToken(JWToken); 

				var tokenResponse = new TokenResponse{
					Token = token,
					CreatedOn = DateTime.Now,
					CustomerId = _iIdEncodeAndDecode.EncodeId(customerDetails.Id),
					UserType = (UserType)customerDetails.UserType
				}; 
				return tokenResponse;
			}catch(SystemException ex)
			{
				Console.WriteLine("Exception : "+ex);
				return null;
			}
			
		}


		private IEnumerable<Claim> GetUserClaims(Customers user)  
        {  
            List<Claim> claims = new List<Claim>(){
				new Claim("UserName", user.UserName),
				new Claim("Country", "IN"),
				new Claim("Role", user.UserType.ToString()),
			};  
            
            return claims.AsEnumerable<Claim>();  
        }  
	}
}