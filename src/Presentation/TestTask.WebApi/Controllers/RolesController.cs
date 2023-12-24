using Microsoft.AspNetCore.Mvc;
using TestTask.Application.Services;
using TestTask.WebApi.Controllers.Base;

namespace TestTask.WebApi.Controllers;

public class RolesController(IRoleService roleService) : BaseController
{
	private readonly IRoleService _roleService = roleService;

	[HttpGet]
	public async Task<IActionResult> GetAllRoles()
	{
		var result = await _roleService.GetAllAsync();
		if (result.IsSuccess)
		{
			return Ok(result.Value);
		}

		return BadRequest(result.ErrorMessage);
	}
}