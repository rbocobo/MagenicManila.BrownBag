using System;
using System.Diagnostics.CodeAnalysis;
using Csla;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MagenicManila.Brownbag.Core.Testing
{
	public static class ExceptionAssert
	{
		private const string ExceptionNotExpected = "Expected exception of type {0}. Actual exception of type {1}.";
		private const string ExceptionNotThrown = "Expected exception of type {0} was not thrown.";

		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
		public static void Throws<T>(Action action) where T : Exception
		{
			ExceptionAssert.Throws<T>(action, null);
		}

		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
		public static void Throws<T>(Action action, Action<Exception> processException) where T : Exception
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}

			try
			{
				action();
			}
			catch (T ex)
			{
				if (processException != null)
				{
					processException(ex);
				}
				return;
			}
			catch (Exception ex)
			{
				throw new AssertFailedException(string.Format(ExceptionAssert.ExceptionNotExpected, typeof(T), ex.GetType()));
			}

			throw new AssertFailedException(string.Format(ExceptionAssert.ExceptionNotThrown, typeof(T)));
		}

		public static void ThrownFromDataPortal(Action action, Type exceptionType)
		{
			ExceptionAssert.Throws<DataPortalException>(action, ex =>
			{
				var currentException = ex;

				while (currentException != null)
				{
					if (currentException.GetType() == exceptionType)
					{
						return;
					}
					else
					{
						currentException = currentException.InnerException;
					}
				}
				throw new AssertFailedException(string.Format(ExceptionAssert.ExceptionNotExpected, exceptionType, ex.GetType()));
			});
		}
	}
}
