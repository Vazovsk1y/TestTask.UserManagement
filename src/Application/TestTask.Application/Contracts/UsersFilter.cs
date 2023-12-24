using System.Text.Json.Serialization;
using TestTask.Application.Contracts.Common;

namespace TestTask.Application.Contracts;

public record UsersFilter(
	[property: JsonConverter (typeof(JsonStringEnumConverter))]
	UsersFilterProperties FilterBy,
	[property: JsonConverter (typeof(JsonStringEnumConverter))]
	Operators Operator,
	string Value) : IFilter<UsersFilterProperties>;

public enum UsersFilterProperties
{
	FullName,
	Email,
	Age,
	Roles,
}