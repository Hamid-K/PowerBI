using System;
using Microsoft.Identity.Json.Utilities;

namespace Microsoft.Identity.Json.Serialization
{
	// Token: 0x020000A3 RID: 163
	internal class SnakeCaseNamingStrategy : NamingStrategy
	{
		// Token: 0x0600083C RID: 2108 RVA: 0x00023CA6 File Offset: 0x00021EA6
		public SnakeCaseNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames)
		{
			base.ProcessDictionaryKeys = processDictionaryKeys;
			base.OverrideSpecifiedNames = overrideSpecifiedNames;
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x00023CBC File Offset: 0x00021EBC
		public SnakeCaseNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames, bool processExtensionDataNames)
			: this(processDictionaryKeys, overrideSpecifiedNames)
		{
			base.ProcessExtensionDataNames = processExtensionDataNames;
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x00023CCD File Offset: 0x00021ECD
		public SnakeCaseNamingStrategy()
		{
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x00023CD5 File Offset: 0x00021ED5
		protected override string ResolvePropertyName(string name)
		{
			return StringUtils.ToSnakeCase(name);
		}
	}
}
