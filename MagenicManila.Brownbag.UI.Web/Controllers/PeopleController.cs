using System;
using System.Web.Mvc;
using MagenicManila.Brownbag.BusinessObjects.Contracts;
using MagenicManila.Brownbag.BusinessObjects.Core.Contracts;
using MagenicManila.Brownbag.UI.Web.Attributes;
using MagenicManila.Brownbag.UI.Web.Extensions;
using MagenicManila.Brownbag.UI.Web.Models;

namespace MagenicManila.Brownbag.UI.Web.Controllers
{
	public class PeopleController : CoreController
	{
		public ActionResult Index()
		{
			return this.View(this.PersonCollectionViewFactory.Fetch());
		}

		[HttpGet]
		public ActionResult Edit(int id)
		{
			var model = new PersonModel(this.PersonFactory.Fetch(id));
			return this.View(model);
		}

		[HttpPost]
		public ActionResult Edit(PersonModel model)
		{
			if (this.SaveObject<IPerson>(model.ModelObject) != null)
			{
				return this.RedirectToAction("Index");
			}
			return this.View(model);
		}

		public ActionResult Add()
		{
			var model = new PersonModel(this.PersonFactory.Create());
			return this.View(model);
		}


		[HttpPost]
		public ActionResult Add(PersonModel model)
		{
			if (this.SaveObject<IPerson>(model.ModelObject) != null)
			{
				return this.RedirectToAction("Index");
			}
			return this.View(model);
		}

		public override object GenerateInstance(Type modelType, IValueProvider valueProvider)
		{
			if (modelType == typeof(PersonModel))
			{
				var id = valueProvider.Extract<int>("ModelObject.Id");
				if (id == 0)
				{
					return new PersonModel(this.PersonFactory.Create());
				}
				else
				{
					return new PersonModel(this.PersonFactory.Fetch(id));
				}
			}

			return base.GenerateInstance(modelType, valueProvider);
		}

		[Dependency]
		public IObjectFactory<IPersonCollectionView> PersonCollectionViewFactory { get; set; }

		[Dependency]
		public IObjectFactory<IPerson> PersonFactory { get; set; }
	}
}