using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestTask.Application.Contracts;
using TestTask.Application.Contracts.Common;
using TestTask.Application.Services;
using TestTask.Domain.Entities;
using TestTask.WebApi.Controllers.Base;
using TestTask.WebApi.ViewModels;

namespace TestTask.WebApi.Controllers;

public class UsersController(
	IUserService userService,
	IAuthenticationService authenticationService) : BaseController
{
	private readonly IUserService _userService = userService;
	private readonly IAuthenticationService _authenticationService = authenticationService;

	[HttpPost]
	public async Task<IActionResult> GetUsers(UsersReceivingModel usersReceivingModel)
	{
		var sortingOptions = new UsersSortingOptions
		(
			Enum.Parse<SortDirection>(usersReceivingModel.SortingOptions.SortDirection, true),
			Enum.Parse<UsersSortingProperties>(usersReceivingModel.SortingOptions.SortBy, true)
		);

		var pagingOptions = usersReceivingModel.PagingOptions is null ? null : new PagingOptions(usersReceivingModel.PagingOptions.PageIndex, usersReceivingModel.PagingOptions.PageSize);
		var filteringOptions = usersReceivingModel.FilteringOptions is null ? null :
			new UsersFilteringOptions
			(
				usersReceivingModel.FilteringOptions.Filters.Select(e => new UsersFilter
				(
					Enum.Parse<UsersFilterProperties>(e.FilterBy, true),
					Enum.Parse<Operators>(e.Operator, true),
					e.Value
				))
				.ToList(),
				Enum.Parse<Logic>(usersReceivingModel.FilteringOptions.Logic, true)
			);

		var result = await _userService.GetAsync(sortingOptions, pagingOptions, filteringOptions);

		return result.IsSuccess ? Ok(result.Value) : BadRequest(result.ErrorMessage);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetUserById(Guid id)
	{
		var result = await _userService.GetByIdAsync(new UserId(id));
		return result.IsSuccess ? Ok(result.Value) : BadRequest(result.ErrorMessage);
	}

	[HttpPost("sign-up")]
	[AllowAnonymous]
	public async Task<IActionResult> Register(UserRegisterModel registerModel)
	{
		var dto = new UserRegisterDTO(registerModel.FullName, registerModel.Age, new UserCredentialsDTO(registerModel.Credentials.Email, registerModel.Credentials.Password));
		var result = await _userService.RegisterAsync(dto);
		return result.IsSuccess ? Ok(result.Value) : BadRequest(result.ErrorMessage);
	}

	[HttpPost("sign-in")]
	[AllowAnonymous]
	public async Task<IActionResult> Login(UserCredentialsModel credentialsModel)
	{
		var dto = new UserCredentialsDTO(credentialsModel.Email, credentialsModel.Password);
		var result = await _authenticationService.LoginAsync(dto);
		return result.IsSuccess ? Ok(result.Value.Value) : BadRequest(result.ErrorMessage); 
	}
}