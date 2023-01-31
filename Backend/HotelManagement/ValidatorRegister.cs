using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.AspNetCore;
using HotelManagement.Dto.Request;
using HotelManagement.Validation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotelManagement
{
    public static class ValidatorRegister
    {
        public static IServiceCollection RegisterValidator(this IServiceCollection services,IConfiguration configuration)
		{
			services.AddFluentValidationAutoValidation();
			services.AddScoped<IValidator<LoginRequest>, LoginRequestValidator>();
			return services;
		}
    }
}