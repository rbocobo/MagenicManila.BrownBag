using System;
using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;
using Csla.Web.Mvc;

namespace MagenicManila.Brownbag.UI.Web.Binders
{
	public interface IModelFactory
	{
		object GenerateInstance(Type modelType, IValueProvider valueProvider);
	}

	public sealed class ObjectBinder
		: CslaModelBinder
	{
		public ObjectBinder(bool checkRules)
			: base(checkRules) { }

		[SuppressMessage("Microsoft.Design", "CA1047:DoNotDeclareProtectedMembersInSealedTypes")]
		protected override object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
		{
			if (controllerContext == null)
			{
				throw new ArgumentNullException("controllerContext");
			}

			if (bindingContext == null)
			{
				throw new ArgumentNullException("bindingContext");
			}

			var controller = controllerContext.Controller as IModelFactory;

			return controller == null ?
				base.CreateModel(controllerContext, bindingContext, modelType) :
				controller.GenerateInstance(modelType, bindingContext.ValueProvider);
		}
	}
}