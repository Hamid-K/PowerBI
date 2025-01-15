using System;

namespace Microsoft.OData
{
	// Token: 0x02000011 RID: 17
	internal interface IODataStreamReferenceInfo
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000D2 RID: 210
		// (set) Token: 0x060000D3 RID: 211
		Uri EditLink { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000D4 RID: 212
		// (set) Token: 0x060000D5 RID: 213
		Uri ReadLink { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000D6 RID: 214
		// (set) Token: 0x060000D7 RID: 215
		string ContentType { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000D8 RID: 216
		// (set) Token: 0x060000D9 RID: 217
		string ETag { get; set; }
	}
}
