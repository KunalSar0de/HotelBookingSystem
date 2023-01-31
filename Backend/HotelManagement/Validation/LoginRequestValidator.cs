using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using HotelManagement.Dto.Request;
using HotelManagement.Exception;
using HotelManagement.Models;

namespace HotelManagement.Validation
{
	public class LoginRequestValidator : AbstractValidator<LoginRequest>
	{
		public LoginRequestValidator()
		{
			RuleFor(x=>x.UserName)
			.NotEmpty()
				.WithMessage(ValidationMessage.InvalidUserName);

			RuleFor(x=>x.Password)
			.NotEmpty()
				.WithMessage(ValidationMessage.InvalidPassword);
			
		}
	}
}