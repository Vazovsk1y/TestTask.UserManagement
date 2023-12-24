using Microsoft.AspNetCore.Mvc;
using TestTask.Application.Contracts.Common;
using TestTask.Application.Services;
using TestTask.WebApi.Controllers.Base;
using TestTask.WebApi.ViewModels;

namespace TestTask.WebApi.Controllers;

public class RolesController(IRoleService roleService) : BaseController
{
	private readonly IRoleService _roleService = roleService;

	[HttpPost]
	public async Task<IActionResult> GetRoles(PagingOptionsModel? pagingOptions = null)
	{
		var dto = pagingOptions is null ? null : new PagingOptions(pagingOptions.PageIndex, pagingOptions.PageSize);
		var result = await _roleService.GetAsync(dto);
		if (result.IsSuccess)
		{
			return Ok(result.Value);
		}

		return BadRequest(result.ErrorMessage);
	}
}