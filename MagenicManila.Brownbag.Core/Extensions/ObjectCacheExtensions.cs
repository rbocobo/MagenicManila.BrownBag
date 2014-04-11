using System;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace MagenicManila.Brownbag.Core.Extensions
{
	public static class ObjectCacheExtensions
	{
		public const string CompositeKey = "{0}::{1}";

		public static string CreateCompositeKey(Type objectType, string key)
		{
			if (objectType == null)
			{
				throw new ArgumentNullException("objectType");
			}

			if (key == null)
			{
				throw new ArgumentNullException("key");
			}

			return string.Format(ObjectCacheExtensions.CompositeKey, objectType.FullName, key);
		}

		public static T GetOrAdd<T>(this ObjectCache @this, string key, Func<T> itemToAdd)
			where T : class
		{
			return @this.GetOrAdd<T>(key, itemToAdd, DateTimeOffset.MaxValue);
		}

		public static T GetOrAdd<T>(this ObjectCache @this, string key, Func<T> itemToAdd, DateTimeOffset absoluteExpiration)
			where T : class
		{
			if (@this == null)
			{
				throw new ArgumentNullException("this");
			}

			if (key == null)
			{
				throw new ArgumentNullException("key");
			}

			if (itemToAdd == null)
			{
				throw new ArgumentNullException("itemToAdd");
			}

			lock (@this)
			{
				var item = default(T);
				var compositeKey = ObjectCacheExtensions.CreateCompositeKey(typeof(T), key);

				if (@this.Contains(compositeKey))
				{
					item = @this.Get(compositeKey) as T;
				}
				else
				{
					item = itemToAdd();
					@this.Add(compositeKey, item, absoluteExpiration);
				}

				return item;
			}
		}

		public static T GetOrAdd<T>(this ObjectCache @this, string key, Func<T> itemToAdd, CacheItemPolicy policy)
			where T : class
		{
			if (@this == null)
			{
				throw new ArgumentNullException("this");
			}

			if (key == null)
			{
				throw new ArgumentNullException("key");
			}

			if (itemToAdd == null)
			{
				throw new ArgumentNullException("itemToAdd");
			}

			if (policy == null)
			{
				throw new ArgumentNullException("policy");
			}

			lock (@this)
			{
				var item = default(T);
				var compositeKey = ObjectCacheExtensions.CreateCompositeKey(typeof(T), key);

				if (@this.Contains(compositeKey))
				{
					item = @this.Get(compositeKey) as T;
				}
				else
				{
					item = itemToAdd();
					@this.Add(compositeKey, item, policy);
				}

				return item;
			}
		}

		public static async Task<T> GetOrAddAsync<T>(this ObjectCache @this, string key, Func<Task<T>> itemToAdd)
			where T : class
		{
			return await @this.GetOrAddAsync<T>(key, itemToAdd, DateTimeOffset.MaxValue);
		}

		public static async Task<T> GetOrAddAsync<T>(this ObjectCache @this, string key, Func<Task<T>> itemToAdd, DateTimeOffset absoluteExpiration)
			where T : class
		{
			if (@this == null)
			{
				throw new ArgumentNullException("this");
			}

			if (key == null)
			{
				throw new ArgumentNullException("key");
			}

			if (itemToAdd == null)
			{
				throw new ArgumentNullException("itemToAdd");
			}

			var item = default(T);
			var compositeKey = ObjectCacheExtensions.CreateCompositeKey(typeof(T), key);

			if (@this.Contains(compositeKey))
			{
				item = @this.Get(compositeKey) as T;
			}
			else
			{
				item = await itemToAdd();
				@this.Add(compositeKey, item, absoluteExpiration);
			}

			return item;
		}

		public static T Remove<T>(this ObjectCache @this, string key)
			where T : class
		{
			if (@this == null)
			{
				throw new ArgumentNullException("this");
			}

			if (key == null)
			{
				throw new ArgumentNullException("key");
			}

			lock (@this)
			{
				var targetType = typeof(T);
				var compositeKey = ObjectCacheExtensions.CreateCompositeKey(targetType, key);
				return @this.Remove(compositeKey) as T;
			}
		}
	}
}
