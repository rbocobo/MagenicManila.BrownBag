using System;

namespace MagenicManila.Brownbag.BusinessObjects.Attributes
{
	[AttributeUsage(AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
	public sealed class ExcludeFromObjectFactoryAttribute
		: Attribute { }
}
