using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using NLog.Internal;

namespace NLog.Config
{
	// Token: 0x02000191 RID: 401
	internal static class ILoggingConfigurationSectionExtensions
	{
		// Token: 0x06001269 RID: 4713 RVA: 0x00031D65 File Offset: 0x0002FF65
		public static bool MatchesName(this ILoggingConfigurationElement section, string expectedName)
		{
			string text;
			if (section == null)
			{
				text = null;
			}
			else
			{
				string name = section.Name;
				text = ((name != null) ? name.Trim() : null);
			}
			return string.Equals(text, expectedName, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x00031D88 File Offset: 0x0002FF88
		public static void AssertName(this ILoggingConfigurationElement section, params string[] allowedNames)
		{
			foreach (string text in allowedNames)
			{
				if (section.MatchesName(text))
				{
					return;
				}
			}
			throw new InvalidOperationException(string.Concat(new string[]
			{
				"Assertion failed. Expected element name '",
				string.Join("|", allowedNames),
				"', actual: '",
				(section != null) ? section.Name : null,
				"'."
			}));
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x00031DF8 File Offset: 0x0002FFF8
		public static string GetRequiredValue(this ILoggingConfigurationElement element, string attributeName, string section)
		{
			string optionalValue = element.GetOptionalValue(attributeName, null);
			if (optionalValue == null)
			{
				throw new NLogConfigurationException(string.Concat(new string[] { "Expected ", attributeName, " on ", element.Name, " in ", section }));
			}
			if (StringHelpers.IsNullOrWhiteSpace(optionalValue))
			{
				throw new NLogConfigurationException(string.Concat(new string[] { "Expected non-empty ", attributeName, " on ", element.Name, " in ", section }));
			}
			return optionalValue;
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x00031E90 File Offset: 0x00030090
		public static string GetOptionalValue(this ILoggingConfigurationElement section, string attributeName, string defaultValue)
		{
			string text = (from configItem in section.Values
				where string.Equals(configItem.Key, attributeName, StringComparison.OrdinalIgnoreCase)
				select configItem.Value).FirstOrDefault<string>();
			if (text == null)
			{
				return defaultValue;
			}
			return text;
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x00031EF4 File Offset: 0x000300F4
		public static bool GetOptionalBooleanValue(this ILoggingConfigurationElement section, string attributeName, bool defaultValue)
		{
			string optionalValue = section.GetOptionalValue(attributeName, null);
			if (string.IsNullOrEmpty(optionalValue))
			{
				return defaultValue;
			}
			bool flag;
			try
			{
				flag = Convert.ToBoolean(optionalValue.Trim(), CultureInfo.InvariantCulture);
			}
			catch (Exception ex)
			{
				if (new NLogConfigurationException(ex, "'{0}' hasn't a valid boolean value '{1}'. {2} will be used", new object[] { attributeName, optionalValue, defaultValue }).MustBeRethrown())
				{
					throw;
				}
				flag = defaultValue;
			}
			return flag;
		}
	}
}
