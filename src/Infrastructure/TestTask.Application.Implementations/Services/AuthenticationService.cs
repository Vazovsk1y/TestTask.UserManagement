using Azure;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TestTask.Application.Contracts;
using TestTask.Application.Contracts.Common;
using TestTask.Application.Services;
using TestTask.DAL;

namespace TestTask.Application.Implementations.Services;

internal class AuthenticationService(
	TestTaskDbContext dbContext,
	IOptions<JwtOptions> jwtOptions,
	IValidator<UserCredentialsDTO> credentialsValidator) : IAuthenticationService
{
	private readonly TestTaskDbContext _dbContext = dbContext;
	private readonly JwtOptions _jwtOptions = jwtOptions.Value;
	private readonly IValidator<UserCredentialsDTO> _credentialsValidator = credentialsValidator;

	public async Task<Result<TokenDTO>> LoginAsync(UserCredentialsDTO credentials, CancellationToken cancellationToken = default)
	{
		var validationResult = _credentialsValidator.Validate(credentials);
		if (!validationResult.IsValid)
		{
			return Result.Failure<TokenDTO>(validationResult.ToString());
		}

		var user = await _dbContext
			.Users
			.Include(e => e.Roles)
			.ThenInclude(e => e.Role)
			.SingleOrDefaultAsync(e => e.Email == credentials.Email, cancellationToken);

		if (user is null)
		{
			return Result.Failure<TokenDTO>(Errors.Auth.InvalidCredentials);
		}

		if (!BCrypt.Net.BCrypt.Verify(credentials.Password, user.PasswordHash))
		{
			return Result.Failure<TokenDTO>(Errors.Auth.InvalidCredentials);
		}

		string token = JwtTokenHelper.GenerateToken(user, _jwtOptions);

		return new TokenDTO(token);
	}
}