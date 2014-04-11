using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Csla;
using MagenicManila.Brownbag.BusinessObjects;
using MagenicManila.Brownbag.Core;
using MagenicManila.Brownbag.Data;
using MagenicManila.Brownbag.UI.Web.Binders;
using CS = Csla.Server;

namespace MagenicManila.Brownbag.UI.Web
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			ApplicationContext.DataPortalActivator = new ObjectActivator();
			CS.DataPortal.InterceptorType = typeof(ObjectInterceptor);

			AreaRegistration.RegisterAllAreas();

			WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

			ModelBinders.Binders.DefaultBinder = new ObjectBinder(true);

			var builder = new ContainerBuilder();
			MvcApplication.RunCompositions(builder);
			IoC.Container = builder.Build();
			DependencyResolver.SetResolver(new AutofacDependencyResolver(IoC.Container));
		}

		private static void RunCompositions(ContainerBuilder builder)
		{
			new CoreContainerBuilderComposition().Compose(builder);
			new EntitiesContainerBuilderComposition().Compose(builder);
			new BusinessObjectsContainerBuilderComposition().Compose(builder);
			new WebContainerBuilderComposition().Compose(builder);
		}
	}
}