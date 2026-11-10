using FeedHub.API.Filters;
using FeedHub.Application;
using FeedHub.Infrastructure;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddApplication();

builder.Services.AddAuthentication(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddMvc(options => options.Filters.Add<ExceptionFilter>());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
