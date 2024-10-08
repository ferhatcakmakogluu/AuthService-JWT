﻿using AuthServer.SharedLibrary.DTOs;
using AuthServer.SharedLibrary.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AuthServer.SharedLibrary.Extensions
{
    public static class CustomExceptionHandler
    {
        public static void UseCustomException(this IApplicationBuilder builder)
        {
            builder.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";
                    var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                    
                    if (errorFeature != null)
                    {
                        var ex = errorFeature.Error;
                        ErrorDto ?errorDto = null;

                        if(ex is CustomException)
                        {
                            errorDto = new ErrorDto(errorFeature.Error.Message, true);
                        }
                        else
                        {
                            errorDto = new ErrorDto(errorFeature.Error.Message, false);
                        }
                        var response = Response<NoContentDto>.Fail(errorDto, 500);

                        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                    }

                    
                });
            });
        }
    }
}
