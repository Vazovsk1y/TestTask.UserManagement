using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MikesPaging.AspNetCore;
using TestTask.Application;
using TestTask.Application.Contracts;
using TestTask.Domain.Constants;
using TestTask.Domain.Entities;
using TestTask.WebApi.Controllers.Base;
using TestTask.WebApi.Extensions;
using TestTask.WebApi.Infrastructure;
using TestTask.WebApi.ViewModels;

namespace TestTask.WebApi.Controllers;

public class UsersController(
	IUserService userService,
	IAuthenticationService authenticationService,
	IRoleService roleService) : BaseController
{
	[HttpPost]
	[PermittedTo(Roles.SuperAdmin, Roles.Admin, Roles.Support)]
	public async Task<IActionResult> GetUsers(UsersReceivingModel usersReceivingModel)
	{
		var sortingOptionsRes = usersReceivingModel.SortingOptionsModel.ToOptions<UsersSortingEnum>();
		if (sortingOptionsRes.IsFailure)
		{
			return BadRequest(sortingOptionsRes.Errors);
		}
		
		var pagingOptionsRes = usersReceivingModel.PagingOptionsModel.ToOptions();
		if (pagingOptionsRes.IsFailure)
		{
			return BadRequest(pagingOptionsRes.Errors);
		}
		
		var filteringOptionsRes = usersReceivingModel.FilteringOptionsModel.ToOptions<UsersFilteringEnum>();
		if (filteringOptionsRes.IsFailure)
		{
			return BadRequest(filteringOptionsRes.Errors);
		}

		var result = await userService.GetPageAsync(HttpContext.GetUserId(), sortingOptionsRes.Value, pagingOptionsRes.Value, filteringOptionsRes.Value);
		return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetUserById(Guid id)
	{
		var result = await userService.GetByIdAsync(HttpContext.GetUserId(), new UserId(id));
		return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors);
	}

	[HttpPost("sign-up")]
	[AllowAnonymous]
	public async Task<IActionResult> Register(UserRegisterModel registerModel)
	{
		var dto = new UserRegisterDTO(registerModel.FullName, registerModel.Age, new UserCredentialsDTO(registerModel.Credentials.Email, registerModel.Credentials.Password));
		var result = await userService.RegisterAsync(dto);
		return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors);
	}

	[HttpPost("sign-in")]
	[AllowAnonymous]
	public async Task<IActionResult> Login(UserCredentialsModel credentialsModel)
	{
		var dto = new UserCredentialsDTO(credentialsModel.Email, credentialsModel.Password);
		var result = await authenticationService.LoginAsync(dto);
		return result.IsSuccess ? Ok(result.Value.Value) : BadRequest(result.Errors);
	}

	[HttpPut]
	public async Task<IActionResult> UpdateUser(UserUpdateModel updateModel)
	{
		var dto = new UserUpdateDTO(updateModel.UserId, updateModel.FullName, updateModel.Age);
		var result = await userService.UpdateAsync(HttpContext.GetUserId(), dto);
		return result.IsSuccess ? Ok() : BadRequest(result.Errors);
	}

	[HttpPost("add-to-role")]
	[PermittedTo(Roles.SuperAdmin)]
	public async Task<IActionResult> AddUserToRole(UserAddToRoleModel userAddToRole)
	{
		var dto = new UserAddToRoleDTO(userAddToRole.UserId, userAddToRole.RoleId);
		var result = await roleService.AddToRoleAsync(HttpContext.GetUserId(), dto);
		return result.IsSuccess ? Ok() : BadRequest(result.Errors);
	}
}