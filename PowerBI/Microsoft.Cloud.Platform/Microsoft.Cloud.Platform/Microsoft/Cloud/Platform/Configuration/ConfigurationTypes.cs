using System;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Configuration
{
	// Token: 0x0200042F RID: 1071
	public static class ConfigurationTypes
	{
		// Token: 0x06002113 RID: 8467 RVA: 0x0007C47C File Offset: 0x0007A67C
		public static bool IsSupportedConfigurationType(Type type)
		{
			return type.IsPrimitive || type.IsEnum || type.Equals(typeof(string)) || type.Equals(typeof(Guid)) || type.Equals(typeof(TimeSpan)) || typeof(ConfigurationClass).IsAssignableFrom(type) || typeof(ISupportedConfigurationType).IsAssignableFrom(type);
		}

		// Token: 0x06002114 RID: 8468 RVA: 0x0007C504 File Offset: 0x0007A704
		public static bool IsSupportedGenericConfigurationArgument(Type type)
		{
			return type.IsPrimitive || type.IsEnum || type.Equals(typeof(string)) || type.Equals(typeof(Guid)) || typeof(ConfigurationClass).IsAssignableFrom(type);
		}

		// Token: 0x06002115 RID: 8469 RVA: 0x0007C564 File Offset: 0x0007A764
		public static bool IsConfigurationCollection(object o)
		{
			Type type = o.GetType();
			return type.IsGenericType && ExtendedType.IsSubclassOfRawGeneric(type, typeof(ConfigurationCollection<>));
		}

		// Token: 0x06002116 RID: 8470 RVA: 0x0007C594 File Offset: 0x0007A794
		public static bool IsConfigurationDictionary(object o)
		{
			Type type = o.GetType();
			return type.IsGenericType && ExtendedType.IsSubclassOfRawGeneric(type, typeof(ConfigurationDictionary<, >));
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06002117 RID: 8471 RVA: 0x0007C5C2 File Offset: 0x0007A7C2
		public static string SupportedConfigurationTypes
		{
			get
			{
				return "Primitives (numbers, booleans), enums, strings, Guids, TimeSpans, ConfigurationCollection, ConfigurationDictionary and any other Configuration class";
			}
		}
	}
}
