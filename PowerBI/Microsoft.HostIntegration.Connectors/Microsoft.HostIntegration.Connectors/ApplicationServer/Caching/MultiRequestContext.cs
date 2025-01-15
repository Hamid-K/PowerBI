using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000321 RID: 801
	internal class MultiRequestContext
	{
		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06001D09 RID: 7433 RVA: 0x000582D1 File Offset: 0x000564D1
		// (set) Token: 0x06001D0A RID: 7434 RVA: 0x000582D9 File Offset: 0x000564D9
		public RequestBody Request { get; private set; }

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06001D0B RID: 7435 RVA: 0x000582E2 File Offset: 0x000564E2
		// (set) Token: 0x06001D0C RID: 7436 RVA: 0x000582EA File Offset: 0x000564EA
		public object Session { get; private set; }

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06001D0D RID: 7437 RVA: 0x000582F3 File Offset: 0x000564F3
		// (set) Token: 0x06001D0E RID: 7438 RVA: 0x000582FB File Offset: 0x000564FB
		public MultiRequest MRequest { get; private set; }

		// Token: 0x06001D0F RID: 7439 RVA: 0x00058304 File Offset: 0x00056504
		public MultiRequestContext(RequestBody originalRequest, MultiRequest mrequest)
		{
			this.Request = originalRequest;
			this.MRequest = mrequest;
		}

		// Token: 0x06001D10 RID: 7440 RVA: 0x0005831A File Offset: 0x0005651A
		public MultiRequestContext(MultiRequest mrequest, object state, int rqstCount)
		{
			this.MRequest = mrequest;
			this.Session = state;
			this.PendingRequestCount = rqstCount;
			this.TotalRequestCount = rqstCount;
		}

		// Token: 0x04001024 RID: 4132
		public int PendingRequestCount;

		// Token: 0x04001025 RID: 4133
		public readonly int TotalRequestCount;
	}
}
