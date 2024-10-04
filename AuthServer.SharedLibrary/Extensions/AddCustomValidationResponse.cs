using AuthServer.SharedLibrary.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.SharedLibrary.Extensions
{
    public static class AddCustomValidationResponse
    {
        public static void UseAddCustomValidationResponse(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState.Values.Where(x => x.Errors.Count > 0)
                    .SelectMany(x=> x.Errors).Select(x=>x.ErrorMessage);

                    ErrorDto errorDto = new ErrorDto(errors.ToList(), true);

                    var response = Response<NoContentDto>.Fail(errorDto, 400);
                    return new BadRequestObjectResult(response);
                };
            });
        }
    }
}
