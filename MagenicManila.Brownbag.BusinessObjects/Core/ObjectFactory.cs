using Csla;
using Csla.Core;
using Csla.Serialization.Mobile;
using MagenicManila.Brownbag.BusinessObjects.Attributes;
using MagenicManila.Brownbag.BusinessObjects.Core.Contracts;
using MagenicManila.Brownbag.Core.Extensions;
using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace MagenicManila.Brownbag.Core.BusinessObjects
{
#pragma warning disable 67
    public sealed class ObjectFactory<T>
        : IObjectFactory<T>
        where T : class, IMobileObject
    {
        public ObjectFactory(ObjectCache cache)
        {
            if (cache == null)
            {
                throw new ArgumentNullException("cache");
            }

            var tType = typeof(T);

            if (tType.GetCustomAttribute<ExcludeFromObjectFactoryAttribute>() != null)
            {
                throw new NotSupportedException(string.Format(
                    "Type {0} cannot be used by ObjectFactory.", tType.FullName));
            }

            this.cache = cache;
        }

        public void BeginCreate(object criteria, object userState)
        {
            throw new NotImplementedException();
        }

        public void BeginCreate(object criteria)
        {
            throw new NotImplementedException();
        }

        public void BeginCreate()
        {
            throw new NotImplementedException();
        }

        public void BeginDelete(object criteria, object userState)
        {
            throw new NotImplementedException();
        }

        public void BeginDelete(object criteria)
        {
            throw new NotImplementedException();
        }

        public void BeginExecute(T command, object userState)
        {
            throw new NotImplementedException();
        }

        public void BeginExecute(T command)
        {
            throw new NotImplementedException();
        }

        public void BeginFetch(object criteria, object userState)
        {
            throw new NotImplementedException();
        }

        public void BeginFetch(object criteria)
        {
            throw new NotImplementedException();
        }

        public void BeginFetch()
        {
            throw new NotImplementedException();
        }

        public void BeginUpdate(T obj, object userState)
        {
            throw new NotImplementedException();
        }

        public void BeginUpdate(T obj)
        {
            throw new NotImplementedException();
        }

        public T Create()
        {
            return DataPortal.Create<T>();
        }

        public T Create(object criteria)
        {
            return DataPortal.Create<T>(criteria);
        }

        public async Task<T> CreateAsync(object criteria)
        {
            return await DataPortal.CreateAsync<T>(criteria);
        }

        public async Task<T> CreateAsync()
        {
            return await DataPortal.CreateAsync<T>();
        }

        public T CreateChild()
        {
            var command = new ChildCommand<T>()
            {
                Operation = DataPortalOperations.Create
            };
            return DataPortal.Execute(command).Child;
        }

        public T CreateChild(params object[] parameters)
        {
            var command = new ChildCommand<T>()
            {
                Operation = DataPortalOperations.Create,
                Parameters = new MobileList<object>(parameters)
            };
            return DataPortal.Execute(command).Child;
        }

        public event EventHandler<DataPortalResult<T>> CreateCompleted;

        public void Delete(object criteria)
        {
            DataPortal.Delete<T>(criteria);
        }

        public Task DeleteAsync(object criteria)
        {
            return DataPortal.DeleteAsync<T>(criteria);
        }

        public event EventHandler<DataPortalResult<T>> DeleteCompleted;

        public T Execute(T obj)
        {
            var caching = typeof(T).GetCustomAttributes<CacheableAttribute>(true).FirstOrDefault();

            return caching != null ?
                this.cache.GetOrAdd<T>(obj.ToString(), () => DataPortal.Execute<T>(obj)) :
                DataPortal.Execute<T>(obj);
        }

        public async Task<T> ExecuteAsync(T command)
        {
            var caching = typeof(T).GetCustomAttributes<CacheableAttribute>(true).FirstOrDefault();

            return caching != null ?
                await this.cache.GetOrAddAsync<T>(command.ToString(), () => DataPortal.ExecuteAsync<T>(command)) :
                await DataPortal.ExecuteAsync<T>(command);
        }

        public event EventHandler<DataPortalResult<T>> ExecuteCompleted;

        public T Fetch()
        {
            return DataPortal.Fetch<T>();
            //var caching = typeof(T).GetCustomAttributes<CacheableAttribute>(true).FirstOrDefault();

            //return caching != null ?
            //    this.cache.GetOrAdd<T>(string.Empty, () => DataPortal.Fetch<T>()) :
            //    DataPortal.Fetch<T>();
        }

        public T Fetch(object criteria)
        {
            var caching = typeof(T).GetCustomAttributes<CacheableAttribute>(true).FirstOrDefault();

            return caching != null ?
                this.cache.GetOrAdd<T>(criteria.ToString(), () => DataPortal.Fetch<T>(criteria)) :
                DataPortal.Fetch<T>(criteria);
        }

        public async Task<T> FetchAsync()
        {
            var caching = typeof(T).GetCustomAttributes<CacheableAttribute>(true).FirstOrDefault();

            return caching != null ?
                await this.cache.GetOrAddAsync<T>(string.Empty, () => DataPortal.FetchAsync<T>()) :
                await DataPortal.FetchAsync<T>();
        }

        public async Task<T> FetchAsync(object criteria)
        {
            var caching = typeof(T).GetCustomAttributes<CacheableAttribute>(true).FirstOrDefault();

            return caching != null ?
                await this.cache.GetOrAddAsync<T>(criteria.ToString(), () => DataPortal.FetchAsync<T>(criteria)) :
                await DataPortal.FetchAsync<T>(criteria);
        }

        public T FetchChild()
        {
            var command = new ChildCommand<T>()
            {
                Operation = DataPortalOperations.Fetch
            };
            return DataPortal.Execute(command).Child;
        }

        public T FetchChild(params object[] parameters)
        {
            var command = new ChildCommand<T>()
            {
                Operation = DataPortalOperations.Fetch,
                Parameters = new MobileList<object>(parameters)
            };
            return DataPortal.Execute(command).Child;
        }

        public event EventHandler<DataPortalResult<T>> FetchCompleted;

        public ContextDictionary GlobalContext
        {
            get { return ApplicationContext.GlobalContext; }
        }

        public T Update(T obj)
        {
            return DataPortal.Update<T>(obj);
        }

        public async Task<T> UpdateAsync(T obj)
        {
            return await DataPortal.UpdateAsync<T>(obj);
        }

        public event EventHandler<DataPortalResult<T>> UpdateCompleted;
#pragma warning restore

        private ObjectCache cache;
    }
}
