using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200009B RID: 155
	public class KebabCaseNamingStrategy : NamingStrategy
	{
		// Token: 0x0600081F RID: 2079 RVA: 0x00023E76 File Offset: 0x00022076
		public KebabCaseNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames)
		{
			base.ProcessDictionaryKeys = processDictionaryKeys;
			base.OverrideSpecifiedNames = overrideSpecifiedNames;
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x00023E8C File Offset: 0x0002208C
		public KebabCaseNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames, bool processExtensionDataNames)
			: this(processDictionaryKeys, overrideSpecifiedNames)
		{
			base.ProcessExtensionDataNames = processExtensionDataNames;
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x00023E9D File Offset: 0x0002209D
		public KebabCaseNamingStrategy()
		{
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x00023EA5 File Offset: 0x000220A5
		[NullableContext(1)]
		protected override string ResolvePropertyName(string name)
		{
			return StringUtils.ToKebabCase(name);
		}
	}
}
