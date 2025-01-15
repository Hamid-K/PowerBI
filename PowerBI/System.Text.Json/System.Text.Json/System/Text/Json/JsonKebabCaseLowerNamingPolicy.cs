using System;

namespace System.Text.Json
{
	// Token: 0x02000031 RID: 49
	internal sealed class JsonKebabCaseLowerNamingPolicy : JsonSeparatorNamingPolicy
	{
		// Token: 0x06000254 RID: 596 RVA: 0x00006126 File Offset: 0x00004326
		public JsonKebabCaseLowerNamingPolicy()
			: base(true, '-')
		{
		}
	}
}
