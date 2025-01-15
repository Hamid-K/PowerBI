using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000073 RID: 115
	public class CamelCaseNamingStrategy : NamingStrategy
	{
		// Token: 0x06000613 RID: 1555 RVA: 0x00019B61 File Offset: 0x00017D61
		public CamelCaseNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames)
		{
			base.ProcessDictionaryKeys = processDictionaryKeys;
			base.OverrideSpecifiedNames = overrideSpecifiedNames;
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00019B77 File Offset: 0x00017D77
		public CamelCaseNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames, bool processExtensionDataNames)
			: this(processDictionaryKeys, overrideSpecifiedNames)
		{
			base.ProcessExtensionDataNames = processExtensionDataNames;
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00019B88 File Offset: 0x00017D88
		public CamelCaseNamingStrategy()
		{
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00019B90 File Offset: 0x00017D90
		[NullableContext(1)]
		protected override string ResolvePropertyName(string name)
		{
			return StringUtils.ToCamelCase(name);
		}
	}
}
