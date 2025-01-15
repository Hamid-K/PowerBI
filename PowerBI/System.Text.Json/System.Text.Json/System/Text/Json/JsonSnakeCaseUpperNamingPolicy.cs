using System;

namespace System.Text.Json
{
	// Token: 0x02000036 RID: 54
	internal sealed class JsonSnakeCaseUpperNamingPolicy : JsonSeparatorNamingPolicy
	{
		// Token: 0x0600025C RID: 604 RVA: 0x000063FF File Offset: 0x000045FF
		public JsonSnakeCaseUpperNamingPolicy()
			: base(false, '_')
		{
		}
	}
}
