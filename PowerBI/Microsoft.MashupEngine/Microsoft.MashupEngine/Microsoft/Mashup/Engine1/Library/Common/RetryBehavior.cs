using System;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001082 RID: 4226
	internal struct RetryBehavior
	{
		// Token: 0x06006EB2 RID: 28338 RVA: 0x0017E2D1 File Offset: 0x0017C4D1
		public RetryBehavior(bool retry, TimeSpan baseDelay)
		{
			this.Retry = retry;
			this.BaseDelay = baseDelay;
			this.RetryErrorCode = null;
		}

		// Token: 0x06006EB3 RID: 28339 RVA: 0x0017E2ED File Offset: 0x0017C4ED
		public RetryBehavior(bool retry, TimeSpan baseDelay, int? retryErrorCode)
		{
			this.Retry = retry;
			this.BaseDelay = baseDelay;
			this.RetryErrorCode = retryErrorCode;
		}

		// Token: 0x17001F38 RID: 7992
		// (get) Token: 0x06006EB4 RID: 28340 RVA: 0x0017E304 File Offset: 0x0017C504
		public bool Retry { get; }

		// Token: 0x17001F39 RID: 7993
		// (get) Token: 0x06006EB5 RID: 28341 RVA: 0x0017E30C File Offset: 0x0017C50C
		public TimeSpan BaseDelay { get; }

		// Token: 0x17001F3A RID: 7994
		// (get) Token: 0x06006EB6 RID: 28342 RVA: 0x0017E314 File Offset: 0x0017C514
		public int? RetryErrorCode { get; }
	}
}
