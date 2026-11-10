using FeedHub.API.Middlewares;
using FeedHub.Application;
using Scalar.AspNetCore;
using FeedHub.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddApplication();

builder.Services.AddAuthentication(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseMiddleware<ApiExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
