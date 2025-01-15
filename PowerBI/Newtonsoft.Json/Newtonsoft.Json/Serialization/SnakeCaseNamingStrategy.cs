using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x020000A3 RID: 163
	public class SnakeCaseNamingStrategy : NamingStrategy
	{
		// Token: 0x06000845 RID: 2117 RVA: 0x000242E6 File Offset: 0x000224E6
		public SnakeCaseNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames)
		{
			base.ProcessDictionaryKeys = processDictionaryKeys;
			base.OverrideSpecifiedNames = overrideSpecifiedNames;
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x000242FC File Offset: 0x000224FC
		public SnakeCaseNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames, bool processExtensionDataNames)
			: this(processDictionaryKeys, overrideSpecifiedNames)
		{
			base.ProcessExtensionDataNames = processExtensionDataNames;
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0002430D File Offset: 0x0002250D
		public SnakeCaseNamingStrategy()
		{
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x00024315 File Offset: 0x00022515
		[NullableContext(1)]
		protected override string ResolvePropertyName(string name)
		{
			return StringUtils.ToSnakeCase(name);
		}
	}
}
