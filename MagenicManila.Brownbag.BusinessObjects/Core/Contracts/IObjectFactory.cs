using Csla;

namespace MagenicManila.Brownbag.BusinessObjects.Core.Contracts
{
	public interface IObjectFactory<T>
		: IDataPortal<T>
	{
		T CreateChild();
		T CreateChild(params object[] parameters);
		T FetchChild();
		T FetchChild(params object[] parameters);
	}
}