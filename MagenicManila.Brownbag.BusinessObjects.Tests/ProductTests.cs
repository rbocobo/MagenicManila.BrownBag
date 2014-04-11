using Autofac;
using Csla;
using MagenicManila.Brownbag.Core;
using MagenicManila.Brownbag.Core.Extensions;
using MagenicManila.Brownbag.Data.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Spackle;
using Spackle.Extensions;
using E = MagenicManila.Brownbag.Data.Entities;

namespace MagenicManila.Brownbag.BusinessObjects.Tests
{
	[TestClass]
	public sealed class ProductTests
	{
		[TestMethod]
		public void FetchById()
		{
			var generator = new RandomObjectGenerator();
			var id = generator.Generate<int>();
			var products = new InMemoryDbSet<E.Product> { EntityCreator.Create<E.Product>(_ => { _.Id = id; }) };
			var context = new Mock<IEntityContext>(MockBehavior.Strict);
			context.Setup(_ => _.Products).Returns(products);
			context.Setup(_ => _.Dispose());
			var container = new ContainerBuilder();
			container.Register<IEntityContext>(_ => context.Object);
			using (container.Build().Bind(() => IoC.Container))
			{
				var actual = DataPortal.Fetch<Product>(id);
				Assert.AreEqual(id, actual.Id, actual.GetPropertyName(_ => _.Id));

				context.VerifyAll();
			}
		}
	}
}
