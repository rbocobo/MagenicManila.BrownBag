using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;

namespace MagenicManila.Brownbag.Core
{
	/// <summary>
	/// Lifted from: http://romiller.com/2012/02/14/testing-with-a-fake-dbcontext/.
	/// Useful in testing/mocking scenarios.
	/// </summary>
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
	[ExcludeFromCodeCoverage]
	public class InMemoryDbSet<T>
		: IDbSet<T>
		where T : class
	{
		public InMemoryDbSet()
		{
			this.data = new ObservableCollection<T>();
			this.Adds = new ObservableCollection<T>();
			this.Removes = new ObservableCollection<T>();
			this.Attaches = new ObservableCollection<T>();
			this.Detaches = new ObservableCollection<T>();
			this.query = this.data.AsQueryable();
		}

		public void ClearCollections()
		{
			Adds.Clear();
			Removes.Clear();
			Attaches.Clear();
			Detaches.Clear();
		}

		public virtual T Find(params object[] keyValues)
		{
			throw new NotImplementedException("Derive from InMemoryDbSet<T> and override Find.");
		}

		public T Add(T entity)
		{
			this.data.Add(entity);
			this.Adds.Add(entity);
			return entity;
		}

		public T Remove(T entity)
		{
			this.data.Remove(entity);
			this.Removes.Add(entity);
			return entity;
		}

		public T Attach(T entity)
		{
			this.data.Add(entity);
			this.Attaches.Add(entity);
			return entity;
		}

		public T Detach(T entity)
		{
			this.data.Remove(entity);
			this.Detaches.Add(entity);
			return entity;
		}

		public T Create()
		{
			return Activator.CreateInstance<T>();
		}

		public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
		{
			return Activator.CreateInstance<TDerivedEntity>();
		}

		public ObservableCollection<T> Local
		{
			get { return this.data; }
		}

		public Type ElementType
		{
			get { return this.query.ElementType; }
		}

		public Expression Expression
		{
			get { return this.query.Expression; }
		}

		public IQueryProvider Provider
		{
			get { return this.query.Provider; }
		}

		public IEnumerator GetEnumerator()
		{
			return this.data.GetEnumerator();
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return this.data.GetEnumerator();
		}

		public ObservableCollection<T> Adds { get; private set; }
		public ObservableCollection<T> Removes { get; private set; }
		public ObservableCollection<T> Attaches { get; private set; }
		public ObservableCollection<T> Detaches { get; private set; }

		private ObservableCollection<T> data;
		private IQueryable query;
	}
}
