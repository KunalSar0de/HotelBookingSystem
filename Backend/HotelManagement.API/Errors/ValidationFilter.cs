using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HotelManagement.API.Errors
{
	public class ValidationFilter : IAsyncActionFilter
	{
		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			//before controller
			if (!context.ModelState.IsValid)
			{
				var errorsInModelState = context.ModelState
				.Where(x => x.Value != null && x.Value.Errors.Count > 0)
				.ToDictionary(kvp => kvp.Key, kvp => kvp.Value?.Errors.Select(x => x.ErrorMessage))
				.ToArray();

				var errorResponse = new ErrorResponse();
				foreach (var error in errorsInModelState)
				{
					if(error.Value != null)
					{
						foreach (var subError in error.Value)
						{
							var errorModel = new ErrorModel
							{
								FieldName = error.Key,
								Message = subError
							};
							errorResponse.Errors.Add(errorModel);
							return;
						}
					}
					
				}

			}
			await next();
			//after controller
		}
	}
}