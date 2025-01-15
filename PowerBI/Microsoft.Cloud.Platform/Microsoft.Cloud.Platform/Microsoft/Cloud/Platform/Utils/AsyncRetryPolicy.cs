using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000150 RID: 336
	public struct AsyncRetryPolicy
	{
		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060008C7 RID: 2247 RVA: 0x0001F1CD File Offset: 0x0001D3CD
		// (set) Token: 0x060008C8 RID: 2248 RVA: 0x0001F1D5 File Offset: 0x0001D3D5
		public Func<Exception, bool> IsPermanentException { get; set; }

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060008C9 RID: 2249 RVA: 0x0001F1DE File Offset: 0x0001D3DE
		// (set) Token: 0x060008CA RID: 2250 RVA: 0x0001F1E6 File Offset: 0x0001D3E6
		public Func<Exception, TimeSpan?> DetermineDelayBetweenRetries { get; set; }

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060008CB RID: 2251 RVA: 0x0001F1EF File Offset: 0x0001D3EF
		// (set) Token: 0x060008CC RID: 2252 RVA: 0x0001F1F7 File Offset: 0x0001D3F7
		public int NumberOfTries { get; set; }

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060008CD RID: 2253 RVA: 0x0001F200 File Offset: 0x0001D400
		// (set) Token: 0x060008CE RID: 2254 RVA: 0x0001F208 File Offset: 0x0001D408
		public TimeSpan DelayBetweenRetries { get; set; }

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060008CF RID: 2255 RVA: 0x0001F211 File Offset: 0x0001D411
		// (set) Token: 0x060008D0 RID: 2256 RVA: 0x0001F219 File Offset: 0x0001D419
		public Func<Exception, bool> UseExponentialBackoff { get; set; }
	}
}
