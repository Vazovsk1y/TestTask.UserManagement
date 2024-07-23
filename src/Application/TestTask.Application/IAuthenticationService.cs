using TestTask.Application.Contracts;
using TestTask.Domain.Shared;

namespace TestTask.Application;

public interface IAuthenticationService
{
	Task<Result<AccessTokenDTO>> LoginAsync(UserCredentialsDTO dto, CancellationToken cancellationToken = default);
}
