using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagement.Authorizer;
using HotelManagement.Authorizer.Impl;
using HotelManagement.Repository;
using HotelManagement.Repository.Impl;
using HotelManagement.Service;
using HotelManagement.Service.Impl;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotelManagement
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services,IConfiguration configuration)
		{
			services.AddScoped<ICustomerRepository , CustomerRepository>();
			services.AddScoped<IPasswordRepository , PasswordRepository>();

			services.AddScoped<IIdEncodeAndDecode , IdEncodeAndDecode>();
			services.AddScoped<ICustomerService , CustomerService>();
			services.AddScoped<ISessionHelperService , SessionHelperService>();
			services.AddScoped<IPasswordManagementService , PasswordManagementService>();
			services.AddScoped<IJwtAuthenticationManager , JwtAuthenticationManager>();	
			return services;
		}
    }
}