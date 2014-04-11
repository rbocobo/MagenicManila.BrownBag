using MagenicManila.Brownbag.BusinessObjects.Contracts;

namespace MagenicManila.Brownbag.UI.Web.Models
{
	public class PersonModel : Csla.Web.Mvc.IViewModel
	{
		public IPerson ModelObject { get; set; }

		object Csla.Web.Mvc.IViewModel.ModelObject
		{
			get
			{
				return this.ModelObject;
			}
			set
			{
				this.ModelObject = (IPerson)value;
			}
		}

		public PersonModel()
		{
		}

		public PersonModel(IPerson person)
		{
			this.ModelObject = person;
		}
	}
}