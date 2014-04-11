using Csla;
using MagenicManila.Brownbag.BusinessObjects.Tests.Core;
using MagenicManila.Brownbag.Core.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spackle;
using E = MagenicManila.Brownbag.Data.Entities;

namespace MagenicManila.Brownbag.BusinessObjects.Tests
{
	[TestClass]
	public sealed class PersonViewTests
	{
		[TestMethod]
		public void ChildFetchWithMiddleName()
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
			var expectedFullname = string.Format("{0} {1} {2}", expected.FirstName, expected.MiddleName, expected.LastName);

			// Act
			var actual = AssemblyTests.DoChildOperation(() => DataPortal.FetchChild<PersonView>(expected));

			// Assert
			Assert.IsNotNull(actual);
			Assert.AreEqual(expected.Id, actual.Id, actual.GetPropertyName(_ => _.Id));
			Assert.AreEqual(expectedFullname, actual.FullName, actual.GetPropertyName(_ => _.FullName));
			Assert.AreEqual(expected.Gender, actual.Gender, actual.GetPropertyName(_ => _.Gender));
		}

		[TestMethod]
		public void ChildFetchWithOutMiddleName()
		{
			// Arrange
			var generator = new RandomObjectGenerator();
			var expected = new E.Person
			{
				Id = generator.Generate<int>(),
				FirstName = generator.Generate<string>(),
				MiddleName = string.Empty,
				LastName = generator.Generate<string>(),
				Gender = generator.Generate<string>()
			};
			var expectedFullname = string.Format("{0} {1}", expected.FirstName, expected.LastName);

			// Act
			var actual = AssemblyTests.DoChildOperation(() => DataPortal.FetchChild<PersonView>(expected));

			// Assert
			Assert.IsNotNull(actual);
			Assert.AreEqual(expected.Id, actual.Id, actual.GetPropertyName(_ => _.Id));
			Assert.AreEqual(expectedFullname, actual.FullName, actual.GetPropertyName(_ => _.FullName));
			Assert.AreEqual(expected.Gender, actual.Gender, actual.GetPropertyName(_ => _.Gender));
		}
	}
}
