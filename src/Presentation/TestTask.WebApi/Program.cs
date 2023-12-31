using FluentValidation;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using TestTask.Application.Implementations;
using TestTask.Application.Implementations.Services;
using TestTask.DAL;
using TestTask.WebApi;

var builder = WebApplication.CreateBuilder(args);

const string JwtSectionName = "Jwt";

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApplicationLayer();
builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(JwtSectionName));
builder.Services.AddAuthenticationWithJwtBearer(builder.Configuration.GetSection(JwtSectionName).Get<JwtOptions>()!);
builder.Services.AddSwaggerWithJwt();

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddFluentValidationAutoValidation();

var app = builder.Build();

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
