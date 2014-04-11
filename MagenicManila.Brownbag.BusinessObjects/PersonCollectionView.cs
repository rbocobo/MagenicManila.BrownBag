using MagenicManila.Brownbag.BusinessObjects.Contracts;
using MagenicManila.Brownbag.BusinessObjects.Core;
using MagenicManila.Brownbag.BusinessObjects.Core.Contracts;
using Spackle.Extensions;
using System;
using System.Linq;

namespace MagenicManila.Brownbag.BusinessObjects
{
    [Serializable]
    internal sealed class PersonCollectionView
        : ReadOnlyListBaseCoreCollection<PersonCollectionView, IPersonView>, IPersonCollectionView
    {
        private void DataPortal_Fetch()
        {
            using (false.Bind(() => this.IsReadOnly))
            {
                var people = this.Context.People.ToList();
                foreach (var person in people)
                {
                    this.Add(this.PersonViewFactory.FetchChild(person));
                }
            }
        }

        [NonSerialized]
        private IObjectFactory<IPersonView> personViewFactory;
        public IObjectFactory<IPersonView> PersonViewFactory
        {
            get { return this.personViewFactory; }
            set { this.personViewFactory = value; }
        }
    }
}