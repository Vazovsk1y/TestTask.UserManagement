namespace TestTask.WebApi.ViewModels;

public record UsersFilteringOptionsModel(IReadOnlyCollection<UsersFilterModel> Filters, string Logic);
