using Autofac;
using Csla;
using MagenicManila.Brownbag.BusinessObjects.Contracts;
using MagenicManila.Brownbag.BusinessObjects.Core.Contracts;
using MagenicManila.Brownbag.Core;
using MagenicManila.Brownbag.Data.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Spackle.Extensions;
using E = MagenicManila.Brownbag.Data.Entities;

namespace MagenicManila.Brownbag.BusinessObjects.Tests
{
    [TestClass]
    public class PersonCollectionViewTests
    {
        [TestMethod]
        public void Fetch()
        {
            var person = EntityCreator.Create<E.Person>();
            var people = new InMemoryDbSet<E.Person> { person };
            var context = new Mock<IEntityContext>(MockBehavior.Strict);
            context.Setup(_ => _.People).Returns(people);
            context.Setup(_ => _.Dispose());

            var personViewFactory = new Mock<IObjectFactory<IPersonView>>(MockBehavior.Strict);
            personViewFactory
                .Setup(_ => _.FetchChild(It.Is<int>(__ => __ == person.Id)))
                .Returns(Mock.Of<IPersonView>());

            var container = new ContainerBuilder();
            container.Register<IEntityContext>(_ => context.Object);
            container.Register<IObjectFactory<IPersonView>>(_ => personViewFactory.Object);

            using (container.Build().Bind(() => IoC.Container))
            {
                var actual = DataPortal.Fetch<PersonCollectionView>();
                Assert.IsNotNull(actual);
                Assert.AreEqual(1, actual.Count, "Count");
            }

            context.VerifyAll();
            personViewFactory.VerifyAll();
        }
    }
}
