using Csla;
using MagenicManila.Brownbag.BusinessObjects.Contracts;
using MagenicManila.Brownbag.BusinessObjects.Core;
using MagenicManila.Brownbag.Core;
using System;
using E = MagenicManila.Brownbag.Data.Entities;

namespace MagenicManila.Brownbag.BusinessObjects
{
    [Serializable]
    internal sealed class PersonView
        : ReadOnlyBaseCore<PersonView>, IPersonView
    {
        private void Child_Fetch(E.Person person)
        {
            this.MapProperties(person);
        }

        private void Child_Fetch(int id)
        {
            //var order = this.Context.Orders.First();
            //Console.WriteLine(order.Id);
            //this.MapProperties(person);
        }

        private void MapProperties(E.Person person)
        {
            this.Id = person.Id;
            this.FullName = string.Format("{0} {1}{2}",
                person.FirstName,
                string.IsNullOrWhiteSpace(person.MiddleName) ? string.Empty : person.MiddleName + " ",
                person.LastName);
            this.Gender = person.Gender;
        }

        public static readonly PropertyInfo<int> IdProperty =
            PropertyInfoRegistration.Register<PersonView, int>(_ => _.Id);
        public int Id
        {
            get { return this.ReadProperty(PersonView.IdProperty); }
            private set { this.LoadProperty(PersonView.IdProperty, value); }
        }

        public static readonly PropertyInfo<string> FullNameProperty =
            PropertyInfoRegistration.Register<PersonView, string>(_ => _.FullName);
        public string FullName
        {
            get { return this.ReadProperty(PersonView.FullNameProperty); }
            private set { this.LoadProperty(PersonView.FullNameProperty, value); }
        }

        public static readonly PropertyInfo<string> GenderProperty =
            PropertyInfoRegistration.Register<PersonView, string>(_ => _.Gender);
        public string Gender
        {
            get { return this.ReadProperty(PersonView.GenderProperty); }
            private set { this.LoadProperty(PersonView.GenderProperty, value); }
        }
    }
}