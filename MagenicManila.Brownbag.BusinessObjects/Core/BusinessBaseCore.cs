using System;
using Csla;
using MagenicManila.Brownbag.BusinessObjects.Core.Contracts;
using MagenicManila.Brownbag.Data.Contracts;

namespace MagenicManila.Brownbag.BusinessObjects.Core
{
	[Serializable]
	public abstract class BusinessBaseCore<T>
		: BusinessBase<T>, IBusinessBaseCore
		where T : BusinessBaseCore<T>
	{
		protected BusinessBaseCore()
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