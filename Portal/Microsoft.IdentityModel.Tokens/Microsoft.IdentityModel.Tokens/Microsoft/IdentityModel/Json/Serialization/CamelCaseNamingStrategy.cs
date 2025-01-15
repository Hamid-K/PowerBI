using System;
using Microsoft.IdentityModel.Json.Utilities;

namespace Microsoft.IdentityModel.Json.Serialization
{
	// Token: 0x02000074 RID: 116
	internal class CamelCaseNamingStrategy : NamingStrategy
	{
		// Token: 0x06000614 RID: 1556 RVA: 0x00019B6D File Offset: 0x00017D6D
		public CamelCaseNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames)
		{
			base.ProcessDictionaryKeys = processDictionaryKeys;
			base.OverrideSpecifiedNames = overrideSpecifiedNames;
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00019B83 File Offset: 0x00017D83
		public CamelCaseNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames, bool processExtensionDataNames)
			: this(processDictionaryKeys, overrideSpecifiedNames)
		{
			base.ProcessExtensionDataNames = processExtensionDataNames;
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00019B94 File Offset: 0x00017D94
		public CamelCaseNamingStrategy()
		{
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x00019B9C File Offset: 0x00017D9C
		protected override string ResolvePropertyName(string name)
		{
			return StringUtils.ToCamelCase(name);
		}
	}
}
