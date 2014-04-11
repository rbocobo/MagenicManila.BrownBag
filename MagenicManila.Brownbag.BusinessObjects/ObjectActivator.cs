using System;
using System.Collections.Generic;
using Autofac;
using Csla;
using Csla.Server;
using MagenicManila.Brownbag.BusinessObjects.Extensions;

namespace MagenicManila.Brownbag.BusinessObjects
{
	public sealed class ObjectActivator
		: IDataPortalActivator
	{
		public object CreateInstance(Type requestedType)
		{
			if (requestedType == null)
			{
				throw new ArgumentNullException("requestedType");
			}

			var scopes = ApplicationContext.LocalContext[ObjectInterceptor.ScopesKey] as Stack<ILifetimeScope>;
			var scope = scopes.Peek();

			var concreteType = requestedType.GetConcreteType();

			return Activator.CreateInstance(concreteType);
		}

		public void InitializeInstance(object obj)
		{
			var scopes = ApplicationContext.LocalContext[ObjectInterceptor.ScopesKey] as Stack<ILifetimeScope>;
			scopes.Peek().InjectProperties(obj);
		}
	}
}