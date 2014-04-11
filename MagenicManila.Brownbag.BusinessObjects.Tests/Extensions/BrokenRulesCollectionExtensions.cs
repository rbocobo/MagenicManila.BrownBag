using System;
using System.Linq;
using System.Reflection;
using Csla.Core;
using Csla.Rules;
using Csla.Rules.CommonRules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MagenicManila.Brownbag.BusinessObjects.Tests.Extensions
{
	internal static class BrokenRulesCollectionExtensions
	{
		internal static void AssertRuleCount(this BrokenRulesCollection @this, int expectedRuleCount)
		{
			Assert.AreEqual(expectedRuleCount, @this.Count);
		}

		internal static void AssertRuleCount(this BrokenRulesCollection @this, IPropertyInfo property, int expectedRuleCount)
		{
			@this.AssertRuleCount(property, expectedRuleCount, string.Empty);
		}

		internal static void AssertRuleCount(this BrokenRulesCollection @this, IPropertyInfo property, int expectedRuleCount, string message)
		{
			var ruleCount = @this.Where(_ => _.Property == property.FriendlyName).Count();
			var assertMessage = string.IsNullOrWhiteSpace(message) ?
				 string.Format(
					  "Rule count was unexpected for property {0}", property.FriendlyName) :
				 string.Format(
					  "Rule count was unexpected for property {0} : {1}", property.FriendlyName, message);
			Assert.AreEqual(expectedRuleCount, ruleCount, assertMessage);
		}

		internal static void AssertValidationRuleExists(this BrokenRulesCollection @this, MemberInfo validationAttributeType, IPropertyInfo property, bool shouldExist)
		{
			var ruleTypeName = validationAttributeType.Name;

			var foundRule = @this.Where(_ => _.Property == property.Name &&
				  _.RuleName.Replace("/", string.Empty).ToLower().Contains(ruleTypeName.ToLower())).SingleOrDefault();

			if (shouldExist)
			{
				Assert.IsNotNull(foundRule, string.Format(
					 "Property {0} does not contain rule {1}", property.Name, ruleTypeName));
			}
			else
			{
				Assert.IsNull(foundRule, string.Format(
					 "Property {0} contains rule {1}", property.Name, ruleTypeName));
			}
		}

		internal static void AssertBusinessRuleExists(this BrokenRulesCollection @this, Type businessRuleType, IPropertyInfo property, bool shouldExist)
		{
			var ruleTypeName = businessRuleType.FullName;
			var ruleUri = new RuleUri(ruleTypeName, property.Name);

			var foundRule = @this.Where(_ => _.Property == property.FriendlyName &&
				  _.RuleName == ruleUri.ToString()).SingleOrDefault();

			if (shouldExist)
			{
				Assert.IsNotNull(foundRule, string.Format(
					 "Property {0} does not contain rule {1}", property.FriendlyName, ruleTypeName));
			}
			else
			{
				Assert.IsNull(foundRule, string.Format(
					 "Property {0} contains rule {1}", property.FriendlyName, ruleTypeName));
			}
		}

		internal static void AssertDataAnnotationBusinessRuleExists(this BrokenRulesCollection @this, Type dataAnnotationAttribute, IPropertyInfo property, bool shouldExist)
		{
			var ruleTypeName = dataAnnotationAttribute.FullName;
			var ruleUri = new RuleUri(typeof(DataAnnotation).FullName, property.Name);

			var dataAnnotationRuleUri = string.Format("{0}?a={1}", ruleUri.ToString(), ruleTypeName);

			var foundRule = @this.Where(_ => _.Property == property.FriendlyName &&
				  _.RuleName == dataAnnotationRuleUri).SingleOrDefault();

			if (shouldExist)
			{
				Assert.IsNotNull(foundRule, string.Format(
					 "Property {0} does not contain rule {1}", property.FriendlyName, ruleTypeName));
			}
			else
			{
				Assert.IsNull(foundRule, string.Format(
					 "Property {0} contains rule {1}", property.FriendlyName, ruleTypeName));
			}
		}

		internal static void AssertBusinessRuleExists(this BrokenRulesCollection @this, Type businessRuleType, IPropertyInfo property, IPropertyInfo targetProperty, bool shouldExist)
		{
			var ruleTypeName = businessRuleType.FullName;

			var foundRule = @this.Where(_ => _.Property == targetProperty.FriendlyName &&
				  _.RuleName == new RuleUri(ruleTypeName, property.Name).ToString()).SingleOrDefault();

			if (shouldExist)
			{
				Assert.IsNotNull(foundRule, string.Format(
					 "Property {0} does not contain rule {1}", property.FriendlyName, ruleTypeName));
			}
			else
			{
				Assert.IsNull(foundRule, string.Format(
					 "Property {0} contains rule {1}", property.FriendlyName, ruleTypeName));
			}
		}
	}
}
