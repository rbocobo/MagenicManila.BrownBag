using Autofac;

namespace MagenicManila.Brownbag.Core
{
	public sealed class CoreContainerBuilderComposition
		: IContainerBuilderComposition
	{
		public void Compose(ContainerBuilder builder)
		{
			//builder.Register<ILog>(_ =>
			//{
			//	var renderers = ConfigurationItemFactory.Default.LayoutRenderers;
			//	renderers.RegisterDefinition(((LayoutRendererAttribute)typeof(FullExceptionLayoutRenderer).GetCustomAttributes(
			//		typeof(LayoutRendererAttribute), false)[0]).Name, typeof(FullExceptionLayoutRenderer));
			//	return new LogWrapper(LogManager.GetLogger("EnabledPlus"));
			//}).SingleInstance();
			//builder.RegisterType<Smtp>().As<ISmtp>();
		}
	}
}