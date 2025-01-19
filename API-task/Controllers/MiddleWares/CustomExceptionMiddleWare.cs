

using Business.Wrappers;
using Common.Exceptions;
using System.Text.Json;

namespace API_task.Controllers.MiddleWares
{
	public class CustomExceptionMiddleWare
	{
        private readonly RequestDelegate next;
        public CustomExceptionMiddleWare(RequestDelegate requestDelegate)
        {
            next = requestDelegate;
        }

        public async Task InvokeAsync (HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
               var response = new Response();
               switch (e)
                {
                    case ValidationException ex:
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        response.Errors = ex.Errors;     
                        break;
                    case NotFoundException ex:
                        context.Response.StatusCode = StatusCodes.Status404NotFound;
                        response.Errors = ex.Errors;
                        break;
					case UnauthorizedException ex:
						context.Response.StatusCode = StatusCodes.Status401Unauthorized;
						response.Errors = ex.Errors;
						break;
					default:
                        response.Message = "Xeta bas verdi";
                        break;
                }

				await context.Response.WriteAsJsonAsync(response);
			}
        }
    }
}
