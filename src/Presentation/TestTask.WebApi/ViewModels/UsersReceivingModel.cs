namespace TestTask.WebApi.ViewModels;

public record UsersReceivingModel(
	UsersSortingOptionsModel SortingOptions, 
	PagingOptionsModel? PagingOptions = null, 
	UsersFilteringOptionsModel? FilteringOptions = null);