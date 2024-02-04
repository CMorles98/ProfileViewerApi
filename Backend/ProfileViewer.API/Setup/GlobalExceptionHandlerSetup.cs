using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;
using ProfileViewer.Application.Exceptions;

namespace ProfileViewer.Api.Setup
{
    public static class ExceptionHandlerSetup
    {
        public class ErrorDetails
        {
            public int StatusCode { get; set; }
            public string? Message { get; set; }
            public override string ToString() => JsonSerializer.Serialize(this);
        }
        public static void UseExceptionHandler(this WebApplication app, Serilog.ILogger logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        logger.Error(contextFeature.Error.Message);

                        var errorDetails = new ErrorDetails
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error."
                        };

                        if (contextFeature.Error is NotFoundException notFoundException)
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                            errorDetails.Message = notFoundException.Message;
                        }
                        else if (contextFeature.Error is UnprocessableEntityException unprocessableException)
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                            errorDetails.Message = unprocessableException.Message;
                        }
                        else if (contextFeature.Error is BadRequestException badRequestException)
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                            errorDetails.Message = badRequestException.Message;
                        }
                        else
                        {
                            string message = $"Something went wrong: {contextFeature.Error}";
                        }

                        errorDetails.StatusCode = context.Response.StatusCode;
                        await context.Response.WriteAsync(errorDetails.ToString());
                    }
                });
            });
        }

    }
}
