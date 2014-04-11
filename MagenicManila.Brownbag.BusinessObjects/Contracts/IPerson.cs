using MagenicManila.Brownbag.BusinessObjects.Core.Contracts;

namespace MagenicManila.Brownbag.BusinessObjects.Contracts
{
	public interface IPerson
		: IBusinessBaseCore
	{
		int? Id { get; }
		string FirstName { get; set; }
		string MiddleName { get; set; }
		string LastName { get; set; }
		string Gender { get; set; }
	}
}