using System;

namespace System.Text.Json
{
	// Token: 0x02000032 RID: 50
	internal sealed class JsonKebabCaseUpperNamingPolicy : JsonSeparatorNamingPolicy
	{
		// Token: 0x06000255 RID: 597 RVA: 0x00006131 File Offset: 0x00004331
		public JsonKebabCaseUpperNamingPolicy()
			: base(false, '-')
		{
		}
	}
}
