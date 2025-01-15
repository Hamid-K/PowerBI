using System;
using System.Collections.Generic;
using NLog.Common;

namespace NLog.Targets.Wrappers
{
	// Token: 0x02000060 RID: 96
	internal abstract class AsyncRequestQueueBase
	{
		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000857 RID: 2135
		public abstract bool IsEmpty { get; }

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x000157AC File Offset: 0x000139AC
		// (set) Token: 0x06000859 RID: 2137 RVA: 0x000157B4 File Offset: 0x000139B4
		public int RequestLimit { get; set; }

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600085A RID: 2138 RVA: 0x000157BD File Offset: 0x000139BD
		// (set) Token: 0x0600085B RID: 2139 RVA: 0x000157C5 File Offset: 0x000139C5
		public AsyncTargetWrapperOverflowAction OnOverflow { get; set; }

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x0600085C RID: 2140 RVA: 0x000157D0 File Offset: 0x000139D0
		// (remove) Token: 0x0600085D RID: 2141 RVA: 0x00015808 File Offset: 0x00013A08
		public event EventHandler<LogEventDroppedEventArgs> LogEventDropped;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x0600085E RID: 2142 RVA: 0x00015840 File Offset: 0x00013A40
		// (remove) Token: 0x0600085F RID: 2143 RVA: 0x00015878 File Offset: 0x00013A78
		public event EventHandler<LogEventQueueGrowEventArgs> LogEventQueueGrow;

		// Token: 0x06000860 RID: 2144
		public abstract bool Enqueue(AsyncLogEventInfo logEventInfo);

		// Token: 0x06000861 RID: 2145
		public abstract AsyncLogEventInfo[] DequeueBatch(int count);

		// Token: 0x06000862 RID: 2146
		public abstract void DequeueBatch(int count, IList<AsyncLogEventInfo> result);

		// Token: 0x06000863 RID: 2147
		public abstract void Clear();

		// Token: 0x06000864 RID: 2148 RVA: 0x000158AD File Offset: 0x00013AAD
		protected void OnLogEventDropped(LogEventInfo logEventInfo)
		{
			EventHandler<LogEventDroppedEventArgs> logEventDropped = this.LogEventDropped;
			if (logEventDropped == null)
			{
				return;
			}
			logEventDropped(this, new LogEventDroppedEventArgs(logEventInfo));
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x000158C6 File Offset: 0x00013AC6
		protected void OnLogEventQueueGrows(long requestsCount)
		{
			EventHandler<LogEventQueueGrowEventArgs> logEventQueueGrow = this.LogEventQueueGrow;
			if (logEventQueueGrow == null)
			{
				return;
			}
			logEventQueueGrow(this, new LogEventQueueGrowEventArgs((long)this.RequestLimit, requestsCount));
		}
	}
}
