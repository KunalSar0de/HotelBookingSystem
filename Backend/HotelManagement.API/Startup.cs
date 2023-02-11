using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using HotelManagement.API.src.Assembler;
using HotelManagement.EntityFramework;
using HotelManagement.Models;
using HotelManagementrc.Dto;
using FluentValidation.AspNetCore;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using HotelManagement.API.Errors;

namespace HotelManagement.API
{
	public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
			services.AddControllers()
			.AddFluentValidation(x=>x.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
			// services.AddControllers(
			// 		// options =>options.Filters.Add<ValidationFilter>()
            //     )
            //     .AddFluentValidation(x=>x.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
				
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

			

             
            var connectionString = Configuration.GetConnectionString("TransactionDatabase");

			services.AddDbContext<TransactionContext>(options => 
			options.UseMySql(connectionString ,x=>x.MigrationsAssembly("HotelManagement.API"))
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

			ServiceRegistration.RegisterServices(services,Configuration);
			// ValidatorRegister.RegisterValidator(services,Configuration);
			
			

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
