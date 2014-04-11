using MagenicManila.Brownbag.BusinessObjects.Core.Contracts;

namespace MagenicManila.Brownbag.BusinessObjects.Contracts
{
	public interface IPersonView
		: IReadOnlyBaseCore
	{
		int Id { get; }
		string FullName { get; }
		string Gender { get; }
	}
}