using System;
using System.Web.Mvc;

namespace MagenicManila.Brownbag.UI.Web.Extensions
{
	public static class IValueProviderExtensions
	{
		public static T Extract<T>(this IValueProvider @this, string key)
		{
			if (@this == null)
			{
				throw new ArgumentNullException("this");
			}

			var result = @this.GetValue(key);

			if (result == null)
			{
				return default(T);
			}

			return (T)result.ConvertTo(typeof(T));
		}
	}
}