using System;

namespace NLog.Targets.Wrappers
{
	// Token: 0x0200006E RID: 110
	public class LogEventQueueGrowEventArgs : EventArgs
	{
		// Token: 0x060008FD RID: 2301 RVA: 0x00017345 File Offset: 0x00015545
		public LogEventQueueGrowEventArgs(long newQueueSize, long requestsCount)
		{
			this.NewQueueSize = newQueueSize;
			this.RequestsCount = requestsCount;
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060008FE RID: 2302 RVA: 0x0001735B File Offset: 0x0001555B
		public long NewQueueSize { get; }

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060008FF RID: 2303 RVA: 0x00017363 File Offset: 0x00015563
		public long RequestsCount { get; }
	}
}
