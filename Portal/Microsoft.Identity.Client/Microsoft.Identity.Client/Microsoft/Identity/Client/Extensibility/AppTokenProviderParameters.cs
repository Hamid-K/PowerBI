using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.Identity.Client.Extensibility
{
	// Token: 0x02000295 RID: 661
	public class AppTokenProviderParameters
	{
		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06001923 RID: 6435 RVA: 0x00052C5B File Offset: 0x00050E5B
		// (set) Token: 0x06001924 RID: 6436 RVA: 0x00052C63 File Offset: 0x00050E63
		public IEnumerable<string> Scopes { get; internal set; }

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06001925 RID: 6437 RVA: 0x00052C6C File Offset: 0x00050E6C
		// (set) Token: 0x06001926 RID: 6438 RVA: 0x00052C74 File Offset: 0x00050E74
		public string CorrelationId { get; internal set; }

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06001927 RID: 6439 RVA: 0x00052C7D File Offset: 0x00050E7D
		// (set) Token: 0x06001928 RID: 6440 RVA: 0x00052C85 File Offset: 0x00050E85
		public string Claims { get; internal set; }

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06001929 RID: 6441 RVA: 0x00052C8E File Offset: 0x00050E8E
		// (set) Token: 0x0600192A RID: 6442 RVA: 0x00052C96 File Offset: 0x00050E96
		public string TenantId { get; internal set; }

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x0600192B RID: 6443 RVA: 0x00052C9F File Offset: 0x00050E9F
		// (set) Token: 0x0600192C RID: 6444 RVA: 0x00052CA7 File Offset: 0x00050EA7
		public CancellationToken CancellationToken { get; internal set; }
	}
}
