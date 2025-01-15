using System;
using Microsoft.IdentityModel.Json.Utilities;

namespace Microsoft.IdentityModel.Json.Serialization
{
	// Token: 0x0200009C RID: 156
	internal class KebabCaseNamingStrategy : NamingStrategy
	{
		// Token: 0x06000820 RID: 2080 RVA: 0x00023E9A File Offset: 0x0002209A
		public KebabCaseNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames)
		{
			base.ProcessDictionaryKeys = processDictionaryKeys;
			base.OverrideSpecifiedNames = overrideSpecifiedNames;
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x00023EB0 File Offset: 0x000220B0
		public KebabCaseNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames, bool processExtensionDataNames)
			: this(processDictionaryKeys, overrideSpecifiedNames)
		{
			base.ProcessExtensionDataNames = processExtensionDataNames;
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x00023EC1 File Offset: 0x000220C1
		public KebabCaseNamingStrategy()
		{
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x00023EC9 File Offset: 0x000220C9
		protected override string ResolvePropertyName(string name)
		{
			return StringUtils.ToKebabCase(name);
		}
	}
}
