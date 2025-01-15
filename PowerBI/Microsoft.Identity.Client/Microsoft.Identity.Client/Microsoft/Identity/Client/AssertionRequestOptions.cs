using System;
using System.Threading;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000130 RID: 304
	public class AssertionRequestOptions
	{
		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000F70 RID: 3952 RVA: 0x000392EF File Offset: 0x000374EF
		// (set) Token: 0x06000F71 RID: 3953 RVA: 0x000392F7 File Offset: 0x000374F7
		public CancellationToken CancellationToken { get; set; }

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000F72 RID: 3954 RVA: 0x00039300 File Offset: 0x00037500
		// (set) Token: 0x06000F73 RID: 3955 RVA: 0x00039308 File Offset: 0x00037508
		public string ClientID { get; set; }

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000F74 RID: 3956 RVA: 0x00039311 File Offset: 0x00037511
		// (set) Token: 0x06000F75 RID: 3957 RVA: 0x00039319 File Offset: 0x00037519
		public string TokenEndpoint { get; set; }
	}
}
