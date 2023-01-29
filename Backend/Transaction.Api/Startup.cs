using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Transaction.Api.src.Assembler;
using Transaction.Api.src.Authorizer;
using Transaction.Api.src.Authorizer.Impl;
using Transaction.Api.src.Dto;
using Transaction.Api.src.EntityFramework;
using Transaction.Api.src.Models;
using Transaction.Api.src.Repository;
using Transaction.Api.src.Repository.Impl;
using Transaction.Api.src.Service;
using Transaction.Api.src.Service.Impl;


namespace Transaction.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
			services.AddCors(
                op => op.AddPolicy("All",
                builder =>
                {
                    // Not a permanent solution, but just trying to isolate the problem
                    builder.AllowAnyOrigin()
                    .WithMethods(new string[] { "OPTION", "POST", "PUT", "DELETE", "GET" })
                    .WithHeaders(HeaderNames.ContentType, "application/json")
                    .WithHeaders(new string[] { HeaderNames.AccessControlAllowHeaders, HeaderNames.Origin, HeaderNames.ContentType, HeaderNames.Authorization })
                    .WithExposedHeaders("X-Requested-With");
                })
            );

	

            services.AddControllers().AddXmlDataContractSerializerFormatters();

			services.AddDbContext<TransactionContext>(options => 
			options.UseSqlServer(Configuration.GetConnectionString("TransactionDatabase"))
			);


			services.AddAuthentication( auth=>
			{
				auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.RequireHttpsMetadata = false;
				options.SaveToken = true;
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:IssuerSigningKey"])),
					ValidIssuer = Configuration["Token:Issuer"],
					ValidateIssuer = true,
					ValidAudience = Configuration["Token:Audiance"],
					ValidateAudience = true
				};
			});
			
			services.AddScoped<ICustomerRepository , CustomerRepository>();
			services.AddScoped<IPasswordRepository , PasswordRepository>();

			services.AddScoped<IIdEncodeAndDecode , IdEncodeAndDecode>();
			services.AddScoped<ICustomerService , CustomerService>();
			services.AddScoped<ISessionHelperService , SessionHelperService>();
			services.AddScoped<IPasswordManagementService , PasswordManagementService>();
			services.AddScoped<IJwtAuthenticationManager , JwtAuthenticationManager>();	

			services.AddScoped<IAssembler<Customers,CustomerResponseDto>,CustomerResponseAssembler>();
		}


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }else
			{
				app.UseExceptionHandler("/Error");
				app.UseHsts();
			}


            app.UseHttpsRedirection();

            app.UseRouting();

			//Auth and authenticati
			app.UseAuthentication();    
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
