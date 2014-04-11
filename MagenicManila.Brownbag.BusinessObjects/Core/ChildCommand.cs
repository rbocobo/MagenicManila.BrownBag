using System;
using System.Reflection;
using Csla;
using Csla.Core;
using MagenicManila.Brownbag.BusinessObjects.Core;
using MagenicManila.Brownbag.Core;

namespace MagenicManila.Brownbag.Core.BusinessObjects
{
	[Serializable]
	internal sealed class ChildCommand<T>
		: CommandBaseCore<ChildCommand<T>>
		where T : class
	{
		public ChildCommand()
			: base() { }

		protected override void DataPortal_Execute()
		{
			if (this.Operation == DataPortalOperations.Create)
			{
				this.Child = this.Parameters == null ?
					DataPortal.CreateChild<T>() : this.CallChildMethod("CreateChild");
			}
			else if (this.Operation == DataPortalOperations.Fetch)
			{
				this.Child = this.Parameters == null ?
					DataPortal.FetchChild<T>() : this.CallChildMethod("FetchChild");
			}
			else
			{
				throw new NotSupportedException(string.Format(
					"The operation {0} is not supported in this command.", this.Operation));
			}
		}

		private T CallChildMethod(string methodName)
		{
			var method = typeof(DataPortal).GetMethod(methodName,
				BindingFlags.Static | BindingFlags.Public, null,
				new[] { typeof(object[]) }, null)
				.MakeGenericMethod(typeof(T));
			return method.Invoke(null, new[] { this.Parameters.ToArray() }) as T;
		}

		public static readonly PropertyInfo<T> ChildProperty =
			PropertyInfoRegistration.Register<ChildCommand<T>, T>(_ => _.Child);
		public T Child
		{
			get { return this.ReadProperty(ChildCommand<T>.ChildProperty); }
			private set { this.LoadProperty(ChildCommand<T>.ChildProperty, value); }
		}

		public static readonly PropertyInfo<DataPortalOperations> OperationProperty =
			PropertyInfoRegistration.Register<ChildCommand<T>, DataPortalOperations>(_ => _.Operation);
		public DataPortalOperations Operation
		{
			get { return this.ReadProperty(ChildCommand<T>.OperationProperty); }
			set { this.LoadProperty(ChildCommand<T>.OperationProperty, value); }
		}

		public static readonly PropertyInfo<MobileList<object>> ParametersProperty =
			PropertyInfoRegistration.Register<ChildCommand<T>, MobileList<object>>(_ => _.Parameters);
		public MobileList<object> Parameters
		{
			get { return this.ReadProperty(ChildCommand<T>.ParametersProperty); }
			set { this.LoadProperty(ChildCommand<T>.ParametersProperty, value); }
		}
	}
}
