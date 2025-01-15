using System;
using System.Globalization;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020012AA RID: 4778
	internal static class ConfigurationPropertyService
	{
		// Token: 0x06007D71 RID: 32113 RVA: 0x001ADDF8 File Offset: 0x001ABFF8
		public static bool TryGetConfigurationProperty(this IEngineHost engineHost, string propertyName, out object value)
		{
			IConfigurationPropertyService configurationPropertyService = engineHost.QueryService<IConfigurationPropertyService>();
			if (configurationPropertyService == null)
			{
				value = null;
				return false;
			}
			return configurationPropertyService.Values.TryGetValue(propertyName, out value);
		}

		// Token: 0x06007D72 RID: 32114 RVA: 0x001ADE24 File Offset: 0x001AC024
		public static T GetConfigurationProperty<T>(this IEngineHost engineHost, string featureSwitch, T defaultValue)
		{
			object obj;
			if (!engineHost.TryGetConfigurationProperty(featureSwitch, out obj))
			{
				return defaultValue;
			}
			if (obj is T)
			{
				return (T)((object)obj);
			}
			try
			{
				return (T)((object)Convert.ChangeType(obj, typeof(T), CultureInfo.InvariantCulture));
			}
			catch (InvalidCastException)
			{
			}
			catch (FormatException)
			{
			}
			catch (OverflowException)
			{
			}
			string fullName = typeof(T).FullName;
			string text = ((obj == null) ? "(null)" : obj.GetType().FullName);
			throw ValueException.NewExpressionError<Message3>(Strings.ConfigurationPropertyTypeError(featureSwitch, text, fullName), null, null);
		}
	}
}
