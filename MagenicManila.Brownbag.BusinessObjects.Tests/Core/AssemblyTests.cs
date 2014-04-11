using System;
using System.Collections.Generic;
using Autofac;
using Csla;
using MagenicManila.Brownbag.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CS = Csla.Server;

namespace MagenicManila.Brownbag.BusinessObjects.Tests.Core
{
	[TestClass]
	public sealed class AssemblyTests
	{
		[AssemblyInitialize]
		public static void AssemblyInitialize(TestContext context)
		{
			ApplicationContext.DataPortalActivator = new ObjectActivator();
			CS.DataPortal.InterceptorType = typeof(ObjectInterceptor);
			IoC.Container = new ContainerBuilder().Build();
		}

		internal static void DoChildOperation(Action operation)
		{
			var scopes = new Stack<ILifetimeScope>();
			ApplicationContext.LocalContext.Add(ObjectInterceptor.ScopesKey, scopes);
			var scope = IoC.Container.BeginLifetimeScope();
			scopes.Push(scope);

			try
			{
				operation();
			}
			finally
			{
				ApplicationContext.LocalContext.Remove(ObjectInterceptor.ScopesKey);
			}
		}

		internal static T DoChildOperation<T>(Func<T> operation)
		{
			var scopes = new Stack<ILifetimeScope>();
			ApplicationContext.LocalContext.Add(ObjectInterceptor.ScopesKey, scopes);
			var scope = IoC.Container.BeginLifetimeScope();
			scopes.Push(scope);

			try
			{
				return operation();
			}
			finally
			{
				ApplicationContext.LocalContext.Remove(ObjectInterceptor.ScopesKey);
			}
		}
	}
}