using System.Text.Json.Serialization;
using TestTask.Application.Contracts.Common;

namespace TestTask.Application.Contracts;

public record UsersFilteringOptions(
	IReadOnlyCollection<UsersFilter> Filters, 
	[property: JsonConverter(typeof(JsonStringEnumConverter))] 
    Logic Logic) : IFilteringOptions<UsersFilter>;
