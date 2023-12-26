using TestTask.Application.Contracts;
using TestTask.Application.Contracts.Common;

namespace TestTask.Application.Services;

public interface IAuthenticationService
{
	Task<Result<TokenDTO>> LoginAsync(UserCredentialsDTO credentialsDTO, CancellationToken cancellationToken = default);
}
