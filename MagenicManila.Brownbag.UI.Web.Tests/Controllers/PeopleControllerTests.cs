using MagenicManila.Brownbag.BusinessObjects.Contracts;
using MagenicManila.Brownbag.BusinessObjects.Core.Contracts;
using MagenicManila.Brownbag.UI.Web.Controllers;
using MagenicManila.Brownbag.UI.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Spackle;
using System.Web.Mvc;

namespace MagenicManila.Brownbag.UI.Web.Tests.Controllers
{
    [TestClass]
    public class PeopleControllerTests
    {
        [TestMethod]
        public void Edit()
        {
            using (var target = new PeopleController())
            {
                var generator = new RandomObjectGenerator();
                var id = generator.Generate<int>();
                var person = new Mock<IPerson>(MockBehavior.Strict);
                person.Setup(_ => _.Id).Returns(id);
                var personFactory = new Mock<IObjectFactory<IPerson>>(MockBehavior.Strict);
                personFactory.Setup(_ => _.Fetch(It.Is<int>(__ => __ == id))).Returns(person.Object);
                target.PersonFactory = personFactory.Object;

                var actual = target.Edit(id) as ViewResult;
                Assert.AreEqual(string.Empty, actual.ViewName, "ViewName");
                Assert.AreSame(person.Object, (actual.Model as PersonModel).ModelObject, "Model");

                personFactory.VerifyAll();
            }
        }
    }
}
