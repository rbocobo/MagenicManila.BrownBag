using System;
using System.Diagnostics.CodeAnalysis;
using Csla;
using MagenicManila.Brownbag.BusinessObjects.Core.Contracts;
using MagenicManila.Brownbag.Data.Contracts;

namespace MagenicManila.Brownbag.BusinessObjects.Core
{
	[Serializable]
	internal abstract class ReadOnlyBaseCore<T>
		: ReadOnlyBase<T>, IReadOnlyBaseCore
		where T : ReadOnlyBaseCore<T>
	{
		protected ReadOnlyBaseCore()
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
