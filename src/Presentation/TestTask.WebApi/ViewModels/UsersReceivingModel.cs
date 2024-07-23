using MikesPaging.AspNetCore.Common.ViewModels;

namespace TestTask.WebApi.ViewModels;

public record UsersReceivingModel(
	SortingOptionsModel SortingOptionsModel, 
	PagingOptionsModel PagingOptionsModel, 
	FilteringOptionsModel FilteringOptionsModel);