using System;

namespace MagenicManila.Brownbag.BusinessObjects.Attributes
{
	[AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class,
		Inherited = true, AllowMultiple = false)]
	public sealed class CacheableAttribute
		: Attribute { }
}
