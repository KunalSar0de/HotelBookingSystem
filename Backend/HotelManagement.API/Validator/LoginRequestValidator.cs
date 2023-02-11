using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using HotelManagement.Dto.Request;
using HotelManagement.Exception;
using HotelManagement.Models;

namespace HotelManagement.API.Validator
{
	public class LoginRequestValidator : AbstractValidator<LoginRequest>
	{
		public LoginRequestValidator()
		{
			RuleFor(x=>x.UserName)
			.NotEmpty()
				.WithMessage(ValidationMessage.InvalidUserName)
			.NotNull()
				.WithMessage(ValidationMessage.InvalidUserName);

			Console.WriteLine("In Validator");
			RuleFor(x=>x.Password)
			.NotEmpty()
				.WithMessage(ValidationMessage.InvalidPassword)
			.NotNull()
				.WithMessage(ValidationMessage.InvalidPassword);
			
		}
	}
}