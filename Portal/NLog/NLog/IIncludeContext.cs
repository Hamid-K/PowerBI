using System;

namespace NLog
{
	// Token: 0x02000004 RID: 4
	internal interface IIncludeContext
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000015 RID: 21
		// (set) Token: 0x06000016 RID: 22
		bool IncludeMdc { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000017 RID: 23
		// (set) Token: 0x06000018 RID: 24
		bool IncludeNdc { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000019 RID: 25
		// (set) Token: 0x0600001A RID: 26
		bool IncludeAllProperties { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001B RID: 27
		// (set) Token: 0x0600001C RID: 28
		bool IncludeMdlc { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001D RID: 29
		// (set) Token: 0x0600001E RID: 30
		bool IncludeNdlc { get; set; }
	}
}
