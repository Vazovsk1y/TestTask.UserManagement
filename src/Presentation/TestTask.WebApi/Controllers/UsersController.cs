using Microsoft.AspNetCore.Mvc;
using TestTask.Application.Contracts;
using TestTask.Application.Contracts.Common;
using TestTask.Application.Services;
using TestTask.WebApi.Controllers.Base;
using TestTask.WebApi.ViewModels;

namespace TestTask.WebApi.Controllers;

public class UsersController(IUserService userService) : BaseController
{
	private readonly IUserService _userService = userService;

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
}