using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Autofac;
using Csla;
using Csla.Server;
using MagenicManila.Brownbag.Core;

namespace MagenicManila.Brownbag.BusinessObjects
{
	public sealed class ObjectInterceptor
		: IInterceptDataPortal
	{
		public const string ScopesKey = "AutofacScope";

		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
		public void Complete(InterceptArgs e)
		{
			var scopes = ApplicationContext.LocalContext[ObjectInterceptor.ScopesKey] as Stack<ILifetimeScope>;
			var scope = scopes.Pop();
			scope.Dispose();

			if (scopes.Count == 0)
			{
				ApplicationContext.LocalContext.Remove(ObjectInterceptor.ScopesKey);
			}
		}

		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
		public void Initialize(InterceptArgs e)
		{
			Stack<ILifetimeScope> scopes = null;

			if (!ApplicationContext.LocalContext.Contains(ObjectInterceptor.ScopesKey))
			{
				scopes = new Stack<ILifetimeScope>();
				ApplicationContext.LocalContext.Add(ObjectInterceptor.ScopesKey, scopes);
			}
			else
			{
				scopes = ApplicationContext.LocalContext[ObjectInterceptor.ScopesKey] as Stack<ILifetimeScope>;
			}

			var scope = IoC.Container.BeginLifetimeScope();
			scopes.Push(scope);
		}
	}
}
