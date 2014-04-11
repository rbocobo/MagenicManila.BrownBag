using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using Csla;
using MagenicManila.Brownbag.BusinessObjects.Contracts;
using MagenicManila.Brownbag.BusinessObjects.Core;
using MagenicManila.Brownbag.Core;
using E = MagenicManila.Brownbag.Data.Entities;

namespace MagenicManila.Brownbag.BusinessObjects
{
	[Serializable]
	internal sealed class Person
		: BusinessBaseCore<Person>, IPerson
	{
		protected override void DataPortal_Create()
		{
			this.BusinessRules.CheckRules();
		}

		private void DataPortal_Fetch(int id)
		{
			var person = this.Context.People.Single(_ => _.Id == id);
			using (this.BypassPropertyChecks)
			{
				this.Id = person.Id;
				this.FirstName = person.FirstName;
				this.MiddleName = person.MiddleName;
				this.LastName = person.LastName;
				this.Gender = person.Gender;
			}
		}
				
		[Transactional(TransactionalTypes.TransactionScope, TransactionIsolationLevel.ReadCommitted)]
		protected override void DataPortal_Insert()
		{
			var entity = this.LoadPropertiesToData();
			this.Context.People.Add(entity);
			this.Context.SaveChanges();
			this.LoadProperty(Person.IdProperty, entity.Id);
		}

		[Transactional(TransactionalTypes.TransactionScope, TransactionIsolationLevel.ReadCommitted)]
		protected override void DataPortal_Update()
		{
			var entity = this.LoadPropertiesToData();
			this.Context.People.Attach(entity);
			this.Context.SetState(entity, EntityState.Modified);
			this.Context.SaveChanges();
		}

		[Transactional(TransactionalTypes.TransactionScope, TransactionIsolationLevel.ReadCommitted)]
		protected override void DataPortal_DeleteSelf()
		{
			var entity = this.LoadPropertiesToData();
			this.Context.People.Attach(entity);
			this.Context.People.Remove(entity);
			this.Context.SaveChanges();
		}

		private E.Person LoadPropertiesToData()
		{
			using (this.BypassPropertyChecks)
			{
				var entity = new E.Person
				{
					Id = this.Id.GetValueOrDefault(),
					FirstName = this.FirstName,
					MiddleName = this.MiddleName,
					LastName = this.LastName,
					Gender = this.Gender
				};
				return entity;
			}
		}

		public static readonly PropertyInfo<int?> IdProperty =
			PropertyInfoRegistration.Register<Person, int?>(_ => _.Id);
		public int? Id
		{
			get { return this.ReadProperty(Person.IdProperty); }
			private set { this.LoadProperty(Person.IdProperty, value); }
		}

		public static readonly PropertyInfo<string> FirstNameProperty =
			PropertyInfoRegistration.Register<Person, string>(_ => _.FirstName);
		[Required]
		[MaxLength(50)]
		public string FirstName
		{
			get { return this.GetProperty(Person.FirstNameProperty); }
			set { this.SetProperty(Person.FirstNameProperty, value); }
		}

		public static readonly PropertyInfo<string> MiddleNameProperty =
			PropertyInfoRegistration.Register<Person, string>(_ => _.MiddleName);
		[MaxLength(50)]
		public string MiddleName
		{
			get { return this.GetProperty(Person.MiddleNameProperty); }
			set { this.SetProperty(Person.MiddleNameProperty, value); }
		}

		public static readonly PropertyInfo<string> LastNameProperty =
			PropertyInfoRegistration.Register<Person, string>(_ => _.LastName);
		[Required]
		[MaxLength(50)]
		public string LastName
		{
			get { return this.GetProperty(Person.LastNameProperty); }
			set { this.SetProperty(Person.LastNameProperty, value); }
		}

		public static readonly PropertyInfo<string> GenderProperty =
			PropertyInfoRegistration.Register<Person, string>(_ => _.Gender);
		[MaxLength(50)]
		public string Gender
		{
			get { return this.GetProperty(Person.GenderProperty); }
			set { this.SetProperty(Person.GenderProperty, value); }
		}
	}
}