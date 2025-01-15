using System;
using Microsoft.IdentityModel.Json.Utilities;

namespace Microsoft.IdentityModel.Json.Serialization
{
	// Token: 0x020000A4 RID: 164
	internal class SnakeCaseNamingStrategy : NamingStrategy
	{
		// Token: 0x06000846 RID: 2118 RVA: 0x0002430A File Offset: 0x0002250A
		public SnakeCaseNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames)
		{
			base.ProcessDictionaryKeys = processDictionaryKeys;
			base.OverrideSpecifiedNames = overrideSpecifiedNames;
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00024320 File Offset: 0x00022520
		public SnakeCaseNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames, bool processExtensionDataNames)
			: this(processDictionaryKeys, overrideSpecifiedNames)
		{
			base.ProcessExtensionDataNames = processExtensionDataNames;
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x00024331 File Offset: 0x00022531
		public SnakeCaseNamingStrategy()
		{
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x00024339 File Offset: 0x00022539
		protected override string ResolvePropertyName(string name)
		{
			return StringUtils.ToSnakeCase(name);
		}
	}
}
