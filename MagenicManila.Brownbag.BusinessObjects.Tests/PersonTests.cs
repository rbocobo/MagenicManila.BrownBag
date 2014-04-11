using System;
using Autofac;
using Csla;
using Csla.Rules;
using MagenicManila.Brownbag.BusinessObjects.Tests.Extensions;
using MagenicManila.Brownbag.Core;
using MagenicManila.Brownbag.Core.Extensions;
using MagenicManila.Brownbag.Core.Testing;
using MagenicManila.Brownbag.Data.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Spackle;
using Spackle.Extensions;
using DA = System.ComponentModel.DataAnnotations;
using E = MagenicManila.Brownbag.Data.Entities;

namespace MagenicManila.Brownbag.BusinessObjects.Tests
{
	[TestClass]
	public sealed class PersonTests
	{
		private static void Arrange(Action<Mock<IEntityContext>> arrange, Action act)
		{
			var context = new Mock<IEntityContext>(MockBehavior.Strict);

			if (arrange != null)
			{
				arrange(context);
			}

			var builder = new ContainerBuilder();
			builder.Register<IEntityContext>(_ => context.Object);
			using (builder.Build().Bind(() => IoC.Container))
			{
				if (act != null)
				{
					act();
				}

				context.VerifyAll();
			}
		}

		[TestMethod]
		public void FetchByPersonId()
		{
			// Arrange
			var generator = new RandomObjectGenerator();
			var expected = new E.Person
			{
				Id = generator.Generate<int>(),
				FirstName = generator.Generate<string>(),
				MiddleName = generator.Generate<string>(),
				LastName = generator.Generate<string>(),
				Gender = generator.Generate<string>()
			};

			var dbSet = new InMemoryDbSet<E.Person> { expected };

			PersonTests.Arrange(mockedContext =>
			{
				mockedContext.Setup(_ => _.People).Returns(dbSet);
				mockedContext.Setup(_ => _.Dispose());
			}, (() =>
			{
				// Act
				var expectedId = expected.Id;
				var actual = DataPortal.Fetch<Person>(expectedId);

				// Assert
				Assert.IsNotNull(actual);
				Assert.IsFalse(actual.IsNew, actual.GetPropertyName(_ => _.IsNew));
				Assert.AreEqual(expectedId, actual.Id, actual.GetPropertyName(_ => _.Id));
				Assert.AreEqual(expected.FirstName, actual.FirstName, actual.GetPropertyName(_ => _.FirstName));
				Assert.AreEqual(expected.MiddleName, actual.MiddleName, actual.GetPropertyName(_ => _.MiddleName));
				Assert.AreEqual(expected.LastName, actual.LastName, actual.GetPropertyName(_ => _.LastName));
				Assert.AreEqual(expected.Gender, actual.Gender, actual.GetPropertyName(_ => _.Gender));
			}));
		}

		[TestMethod]
		public void Create()
		{
			// Arrange
			// Act
			var actual = DataPortal.Create<Person>();

			// Assert
			Assert.IsNotNull(actual);
			Assert.IsTrue(actual.IsNew, actual.GetPropertyName(_ => _.IsNew));
			Assert.IsNull(actual.Id, actual.GetPropertyName(_ => _.Id));
			Assert.AreEqual(string.Empty, actual.FirstName, actual.GetPropertyName(_ => _.FirstName));
			Assert.AreEqual(string.Empty, actual.MiddleName, actual.GetPropertyName(_ => _.MiddleName));
			Assert.AreEqual(string.Empty, actual.LastName, actual.GetPropertyName(_ => _.LastName));
			Assert.AreEqual(string.Empty, actual.Gender, actual.GetPropertyName(_ => _.Gender));
		}

		[TestMethod]
		public void InsertValidPerson()
		{
			// Arrange
			PersonTests.Arrange(mockedContext =>
			{
				mockedContext.Setup(_ => _.Dispose());
				mockedContext.Setup(_ => _.People).Returns(new InMemoryDbSet<E.Person>());
				mockedContext.Setup(_ => _.SaveChanges()).Returns(1);
			}, () =>
			{
				var generator = new RandomObjectGenerator();
				var expectedFirstName = generator.Generate<string>();
				var expectedMiddleName = generator.Generate<string>();
				var expectedLastName = generator.Generate<string>();
				var expectedGender = generator.Generate<string>();

				var actual = DataPortal.Create<Person>();
				actual.FirstName = expectedFirstName;
				actual.MiddleName = expectedMiddleName;
				actual.LastName = expectedLastName;
				actual.Gender = expectedGender;

				// Act
				actual = actual.Save();

				// Assert
				Assert.IsNotNull(actual);
				Assert.IsFalse(actual.IsNew, actual.GetPropertyName(_ => _.IsNew));
				Assert.AreEqual(expectedFirstName, actual.FirstName, actual.GetPropertyName(_ => _.FirstName));
				Assert.AreEqual(expectedMiddleName, actual.MiddleName, actual.GetPropertyName(_ => _.MiddleName));
				Assert.AreEqual(expectedLastName, actual.LastName, actual.GetPropertyName(_ => _.LastName));
				Assert.AreEqual(expectedGender, actual.Gender, actual.GetPropertyName(_ => _.Gender));
			});
		}

		[TestMethod]
		public void InsertInvalidPerson()
		{
			// Arrange
			PersonTests.Arrange(mockedContext =>
			{
				mockedContext.Setup(_ => _.Dispose());
			}, () =>
			{
				var actual = DataPortal.Create<Person>();

				// Act
				// Assert
				Assert.IsFalse(actual.IsValid, actual.GetPropertyName(_ => _.IsValid));
				actual.GetBrokenRules().AssertDataAnnotationBusinessRuleExists(typeof(DA.RequiredAttribute), Person.FirstNameProperty, true);
				actual.GetBrokenRules().AssertDataAnnotationBusinessRuleExists(typeof(DA.RequiredAttribute), Person.LastNameProperty, true);
				ExceptionAssert.Throws<ValidationException>(() => { actual = actual.Save(); });
			});
		}
	}
}