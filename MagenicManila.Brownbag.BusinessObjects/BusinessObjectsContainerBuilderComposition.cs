using Autofac;
using MagenicManila.Brownbag.BusinessObjects.Core.Contracts;
using MagenicManila.Brownbag.Core;
using MagenicManila.Brownbag.Core.BusinessObjects;

namespace MagenicManila.Brownbag.BusinessObjects
{
	public sealed class BusinessObjectsContainerBuilderComposition
		: IContainerBuilderComposition
	{
		public void Compose(ContainerBuilder builder)
		{
			builder.RegisterGeneric(typeof(ObjectFactory<>)).As(typeof(IObjectFactory<>));
		}
	}
}