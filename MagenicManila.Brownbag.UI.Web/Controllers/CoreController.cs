using System;
using System.Web.Mvc;
using Csla;
using Csla.Core;
using Csla.Rules;
using MagenicManila.Brownbag.UI.Web.Binders;

namespace MagenicManila.Brownbag.UI.Web.Controllers
{
	public abstract class CoreController : Controller, IModelFactory
	{
		/// <summary>
		/// Performs a Save() operation on an
		/// editable business object, with appropriate
		/// validation and exception handling.
		/// </summary>
		/// <typeparam name="T">Type of business object.</typeparam>
		/// <param name="item">The business object to insert.</param>
		/// <returns>the DB updated version, or null on failure.</returns>
		protected virtual T SaveObject<T>(T item) where T : class, ISavable, ITrackStatus
		{
			return this.SaveObject(item, null);
		}

		/// <summary>
		/// Performs a Save() operation on an
		/// editable business object, with appropriate
		/// validation and exception handling.
		/// </summary>
		/// <typeparam name="T">Type of business object.</typeparam>
		/// <param name="item">The business object to insert.</param>
		/// <param name="updateModel">Delegate that invokes the UpdateModel() method.</param>
		/// <returns>the DB updated version, or null on failure.</returns>
		protected virtual T SaveObject<T>(T item, Action<T> updateModel) where T : class, ISavable, ITrackStatus
		{
			try
			{
				if (updateModel != null)
				{
					updateModel(item);
				}

				if (!item.IsValid)
				{
					return null;
				}

				return (T)item.Save();
			}
			catch (ValidationException ex)
			{
				this.ModelState.AddModelError("", ex.Message);
				return null;
			}
			catch (DataPortalException ex)
			{
				var inner = ex.BusinessException ?? ex;

				while (inner.InnerException != null)
				{
					inner = inner.InnerException;
				}
				this.ModelState.AddModelError("", inner.Message);
				return null;
			}
		}

		public virtual object GenerateInstance(Type modelType, IValueProvider valueProvider)
		{
			return Activator.CreateInstance(modelType);
		}
	}
}