using System;

namespace System.Text.Json
{
	// Token: 0x02000035 RID: 53
	internal sealed class JsonSnakeCaseLowerNamingPolicy : JsonSeparatorNamingPolicy
	{
		// Token: 0x0600025B RID: 603 RVA: 0x000063F4 File Offset: 0x000045F4
		public JsonSnakeCaseLowerNamingPolicy()
			: base(true, '_')
		{
		}
	}
}
