using System;

namespace MagenicManila.Brownbag.BusinessObjects.Extensions
{
	public static class TypeExtensions
	{
		public static Type GetConcreteType(this Type @this)
		{
			if (@this == null || !@this.IsInterface || string.IsNullOrWhiteSpace(@this.Namespace))
			{
				return @this;
			}

			var concreteTypeName = string.Concat(@this.Namespace.Replace(".Contracts", string.Empty),
															 ".",
															 @this.Name.Substring(1),
															 ", ",
															 @this.Assembly.FullName);

			return Type.GetType(concreteTypeName);
		}
	}
}
