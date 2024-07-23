using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TestTask.Application.Implementations.Infrastructure;

namespace TestTask.WebApi.Extensions;

public static class Registrator
{
	private const string Bearer = "Bearer";
	private const string Description = $"JWT Authorization header using the {Bearer} scheme. \r\n\r\n Enter '{Bearer}' [space] [your token value].";

	public static IServiceCollection AddSwaggerWithJwt(this IServiceCollection collection)
	{
		return collection.AddSwaggerGen(swagger =>
		{
			swagger.AddSecurityDefinition(Bearer, new OpenApiSecurityScheme
			{
				Name = "Authorization",
				Type = SecuritySchemeType.ApiKey,
				Scheme = Bearer,
				BearerFormat = "JWT",
				In = ParameterLocation.Header,
				Description = Description
			});

			swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
			{
				{
					new OpenApiSecurityScheme
					{
						Reference = new OpenApiReference
						{
							Type = ReferenceType.SecurityScheme,
							Id = Bearer
						}
					},
					Array.Empty<string>()
				}
			});
		});
	}

	public static void AddAuthenticationWithJwtBearer(this IServiceCollection collection, JwtOptions jwtOptions)
	{
		var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey));

		collection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
		.AddJwtBearer(options =>
		{
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,
				ValidIssuer = jwtOptions.Issuer,
				ValidAudience = jwtOptions.Audience,
				IssuerSigningKey = signingKey
			};
		});
	}
}
