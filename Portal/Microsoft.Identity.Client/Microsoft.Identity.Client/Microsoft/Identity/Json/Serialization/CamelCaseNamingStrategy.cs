using System;
using Microsoft.Identity.Json.Utilities;

namespace Microsoft.Identity.Json.Serialization
{
	// Token: 0x02000073 RID: 115
	internal class CamelCaseNamingStrategy : NamingStrategy
	{
		// Token: 0x0600060A RID: 1546 RVA: 0x00019599 File Offset: 0x00017799
		public CamelCaseNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames)
		{
			base.ProcessDictionaryKeys = processDictionaryKeys;
			base.OverrideSpecifiedNames = overrideSpecifiedNames;
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x000195AF File Offset: 0x000177AF
		public CamelCaseNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames, bool processExtensionDataNames)
			: this(processDictionaryKeys, overrideSpecifiedNames)
		{
			base.ProcessExtensionDataNames = processExtensionDataNames;
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x000195C0 File Offset: 0x000177C0
		public CamelCaseNamingStrategy()
		{
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x000195C8 File Offset: 0x000177C8
		protected override string ResolvePropertyName(string name)
		{
			return StringUtils.ToCamelCase(name);
		}
	}
}
