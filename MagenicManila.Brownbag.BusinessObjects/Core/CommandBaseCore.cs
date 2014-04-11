using System;
using Csla;
using MagenicManila.Brownbag.BusinessObjects.Core.Contracts;
using MagenicManila.Brownbag.Data.Contracts;

namespace MagenicManila.Brownbag.BusinessObjects.Core
{
	[Serializable]
	internal abstract class CommandBaseCore<T>
		: CommandBase<T>, ICommandBaseCore
		where T : CommandBaseCore<T>
	{
		[NonSerialized]
		private IEntityContext context;
		public IEntityContext Context
		{
			get { return this.context; }
			set { this.context = value; }
		}
	}
}