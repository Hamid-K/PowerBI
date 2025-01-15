using System;

namespace Microsoft.Identity.Json
{
	// Token: 0x02000016 RID: 22
	internal interface IJsonLineInfo
	{
		// Token: 0x06000017 RID: 23
		bool HasLineInfo();

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000018 RID: 24
		int LineNumber { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000019 RID: 25
		int LinePosition { get; }
	}
}
