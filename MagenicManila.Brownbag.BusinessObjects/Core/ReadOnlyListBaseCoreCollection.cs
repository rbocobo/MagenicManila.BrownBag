using System;
using System.Diagnostics.CodeAnalysis;
using Csla;
using MagenicManila.Brownbag.BusinessObjects.Core.Contracts;
using MagenicManila.Brownbag.Data.Contracts;

namespace MagenicManila.Brownbag.BusinessObjects.Core
{
	[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "C")]
	[SuppressMessage("Microsoft.Naming", "CA1715:IdentifiersShouldHaveCorrectPrefix", MessageId = "T")]
	[Serializable]
	public abstract class ReadOnlyListBaseCoreCollection<T, C>
		: ReadOnlyListBase<T, C>, IReadOnlyListBaseCoreCollection<C>
		where T : ReadOnlyListBaseCoreCollection<T, C>
	{
		protected ReadOnlyListBaseCoreCollection()
			: base() { }

		[NonSerialized]
		private IEntityContext context;
		public IEntityContext Context
		{
			get { return this.context; }
			set { this.context = value; }
		}
	}
}
