using System.Text.Json.Serialization;
using TestTask.Application.Contracts.Common;

namespace TestTask.Application.Contracts;

public record UsersSortingOptions(
	[property: JsonConverter(typeof(JsonStringEnumConverter))] 
    SortDirection SortDirection,
	[property: JsonConverter(typeof(JsonStringEnumConverter))]
	UsersSortingProperties SortBy) : ISortingOptions<UsersSortingProperties>;

public enum UsersSortingProperties
{
	FullName,
	Age,
	RolesCount,
	Email,
}

