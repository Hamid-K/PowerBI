using System;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation
{
	// Token: 0x0200006B RID: 107
	public class HttpWebResponseWrapper
	{
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000347 RID: 839 RVA: 0x0000F398 File Offset: 0x0000D598
		// (set) Token: 0x06000348 RID: 840 RVA: 0x0000F3A0 File Offset: 0x0000D5A0
		public string Content { get; set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000349 RID: 841 RVA: 0x0000F3A9 File Offset: 0x0000D5A9
		// (set) Token: 0x0600034A RID: 842 RVA: 0x0000F3B1 File Offset: 0x0000D5B1
		public int StatusCode { get; set; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600034B RID: 843 RVA: 0x0000F3BA File Offset: 0x0000D5BA
		// (set) Token: 0x0600034C RID: 844 RVA: 0x0000F3C2 File Offset: 0x0000D5C2
		public string RetryAfterHeader { get; set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600034D RID: 845 RVA: 0x0000F3CB File Offset: 0x0000D5CB
		// (set) Token: 0x0600034E RID: 846 RVA: 0x0000F3D3 File Offset: 0x0000D5D3
		public string StatusDescription { get; set; }
	}
}
