using Microsoft.AspNetCore.Mvc;
using TestTask.Application;
using TestTask.WebApi.Controllers.Base;

namespace TestTask.WebApi.Controllers;

public class RolesController(IRoleService roleService) : BaseController
{
	[HttpGet]
	public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
	{
		var result = await roleService.GetAllAsync(cancellationToken);
		
		if (result.IsSuccess)
		{
			return Ok(result.Value);
		}

		return BadRequest(result.Errors);
	}
}