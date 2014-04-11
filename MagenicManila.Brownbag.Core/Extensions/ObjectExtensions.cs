using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Csla.Reflection;

namespace MagenicManila.Brownbag.Core.Extensions
{
	public static class ObjectExtensions
	{
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "this")]
		public static string GetPropertyName<T>(this T @this, Expression<Func<T, object>> propertyExpression)
		{
			return Reflect<T>.GetProperty(propertyExpression).Name;
		}

		public static int SafeGetHashCode<T>(this T @this)
			where T : class
		{
			return @this != null ? @this.GetHashCode() : 0;
		}
	}
}
