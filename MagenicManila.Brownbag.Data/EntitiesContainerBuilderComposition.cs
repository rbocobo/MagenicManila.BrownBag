using Autofac;
using MagenicManila.Brownbag.Core;
using MagenicManila.Brownbag.Data.Contracts;

namespace MagenicManila.Brownbag.Data
{
	public sealed class EntitiesContainerBuilderComposition
		: IContainerBuilderComposition
	{
		public void Compose(ContainerBuilder builder)
		{
			builder.Register(c => EntityContext.GetContext()).As(typeof(IEntityContext));
		}
	}
}