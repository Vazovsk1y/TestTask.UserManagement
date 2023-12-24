using TestTask.Application.Contracts.Common;

namespace TestTask.Application.Services;

public interface IFilteringOptionsChecker<T> where T : IFilteringOptions
{
	Result IsAppliсable(T filter);
}