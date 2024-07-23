using FluentValidation;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using TestTask.Application.Implementations.Extensions;
using TestTask.Application.Implementations.Infrastructure;
using TestTask.DAL;
using TestTask.WebApi.Extensions;
using TestTask.WebApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

const string JwtSectionName = "Jwt";

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApplicationLayer();
builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(JwtSectionName));
builder.Services.AddAuthenticationWithJwtBearer(builder.Configuration.GetSection(JwtSectionName).Get<JwtOptions>()!);
builder.Services.AddSwaggerWithJwt();
builder.Services.AddExceptionHandler<ExceptionsHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddFluentValidationAutoValidation();

var app = builder.Build();

app.UseExceptionHandler();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
	app.MigrateDatabase();
}

app.Run();
