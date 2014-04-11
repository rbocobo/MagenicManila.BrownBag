using MagenicManila.Brownbag.BusinessObjects.Core.Contracts;

namespace MagenicManila.Brownbag.BusinessObjects.Contracts
{
	public interface IPersonCollectionView
		: IReadOnlyListBaseCoreCollection<IPersonView>
	{
	}
}
