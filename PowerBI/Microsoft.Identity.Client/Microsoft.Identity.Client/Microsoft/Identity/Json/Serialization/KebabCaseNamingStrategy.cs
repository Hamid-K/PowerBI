using System;
using Microsoft.Identity.Json.Utilities;

namespace Microsoft.Identity.Json.Serialization
{
	// Token: 0x0200009B RID: 155
	internal class KebabCaseNamingStrategy : NamingStrategy
	{
		// Token: 0x06000816 RID: 2070 RVA: 0x0002383A File Offset: 0x00021A3A
		public KebabCaseNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames)
		{
			base.ProcessDictionaryKeys = processDictionaryKeys;
			base.OverrideSpecifiedNames = overrideSpecifiedNames;
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x00023850 File Offset: 0x00021A50
		public KebabCaseNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames, bool processExtensionDataNames)
			: this(processDictionaryKeys, overrideSpecifiedNames)
		{
			base.ProcessExtensionDataNames = processExtensionDataNames;
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x00023861 File Offset: 0x00021A61
		public KebabCaseNamingStrategy()
		{
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x00023869 File Offset: 0x00021A69
		protected override string ResolvePropertyName(string name)
		{
			return StringUtils.ToKebabCase(name);
		}
	}
}
