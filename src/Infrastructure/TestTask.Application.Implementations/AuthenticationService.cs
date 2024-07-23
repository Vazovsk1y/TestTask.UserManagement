using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TestTask.Application.Contracts;
using TestTask.Application.Implementations.Constants;
using TestTask.Application.Implementations.Extensions;
using TestTask.Application.Implementations.Infrastructure;
using TestTask.DAL;
using TestTask.Domain.Shared;

namespace TestTask.Application.Implementations;

internal class AuthenticationService(
	TestTaskDbContext dbContext,
	IOptions<JwtOptions> jwtOptions,
	IValidator<UserCredentialsDTO> credentialsValidator) : IAuthenticationService
{
	private readonly JwtOptions _jwtOptions = jwtOptions.Value;

	public async Task<Result<AccessTokenDTO>> LoginAsync(UserCredentialsDTO credentials, CancellationToken cancellationToken = default)
	{
		cancellationToken.ThrowIfCancellationRequested();
		
		var validationResult = await credentialsValidator.ValidateAsync(credentials, cancellationToken);
		if (!validationResult.IsValid)
		{
			return Result.Failure<AccessTokenDTO>(validationResult.Errors.ToSharedErrors());
		}

		var user = await dbContext
			.Users
			.Include(e => e.Roles)
			.ThenInclude(e => e.Role)
			.SingleOrDefaultAsync(e => e.Email == credentials.Email, cancellationToken);

		if (user is null)
		{
			return Result.Failure<AccessTokenDTO>(Errors.Auth.InvalidCredentials);
		}

		if (!BCrypt.Net.BCrypt.Verify(credentials.Password, user.PasswordHash))
		{
			return Result.Failure<AccessTokenDTO>(Errors.Auth.InvalidCredentials);
		}

		string token = JwtTokenHelper.GenerateToken(user, _jwtOptions);
		return new AccessTokenDTO(token);
	}
}