using System.Runtime.Caching;
using Autofac;
using Autofac.Integration.Mvc;
using MagenicManila.Brownbag.Core;

namespace MagenicManila.Brownbag.UI.Web
{
	public sealed class WebContainerBuilderComposition
		: IContainerBuilderComposition
	{
		public void Compose(ContainerBuilder builder)
		{
			var assembly = typeof(WebContainerBuilderComposition).Assembly;
			builder.RegisterControllers(assembly).PropertiesAutowired();
			builder.RegisterModelBinders(assembly);
			builder.RegisterModelBinderProvider();
			builder.RegisterFilterProvider();
			builder.RegisterInstance<ObjectCache>(MemoryCache.Default);
		}
	}
}