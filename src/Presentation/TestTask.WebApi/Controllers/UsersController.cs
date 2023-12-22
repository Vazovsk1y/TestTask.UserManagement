using Microsoft.AspNetCore.Mvc;
using TestTask.Application.Responses;
using TestTask.Application.Services;
using TestTask.WebApi.Controllers.Base;
using TestTask.WebApi.ViewModels;

namespace TestTask.WebApi.Controllers;

public class UsersController(IUserService userService) : BaseController
{
	private readonly IUserService _userService = userService;

	[HttpPost]
	public async Task<IActionResult> GetUsers(UsersReceivingModel receivingModel)
	{
		var sortingOptions = new UsersSortingOptions(Enum.Parse<SortDirection>(receivingModel.SortingModel.SortDirection, true), Enum.Parse<UsersSortingProperties>(receivingModel.SortingModel.SortBy, true));
		var pagingOptions = receivingModel.PagingOptionsModel is null ? null : new PagingOptions(receivingModel.PagingOptionsModel.PageIndex, receivingModel.PagingOptionsModel.PageSize);

		var result = await _userService.GetAsync(sortingOptions, pagingOptions);

		return result.IsSuccess ? Ok(result.Value) : BadRequest(result.ErrorMessage);
	}
}