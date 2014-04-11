using System;

namespace MagenicManila.Brownbag.UI.Web.Attributes
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
	public sealed class DependencyAttribute : Attribute { }
}
