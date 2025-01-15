using System;

namespace Microsoft.Owin.Host.HttpListener
{
	// Token: 0x02000009 RID: 9
	internal class PumpLimits
	{
		// Token: 0x06000025 RID: 37 RVA: 0x00002BEF File Offset: 0x00000DEF
		internal PumpLimits(int maxAccepts, int maxRequests)
		{
			this.MaxOutstandingAccepts = maxAccepts;
			this.MaxOutstandingRequests = maxRequests;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002C05 File Offset: 0x00000E05
		// (set) Token: 0x06000027 RID: 39 RVA: 0x00002C0D File Offset: 0x00000E0D
		internal int MaxOutstandingAccepts { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002C16 File Offset: 0x00000E16
		// (set) Token: 0x06000029 RID: 41 RVA: 0x00002C1E File Offset: 0x00000E1E
		internal int MaxOutstandingRequests { get; private set; }
	}
}
