using System;
using System.Threading;
using Microsoft.ReportingServices.DataProcessing;
using Microsoft.ReportingServices.Diagnostics.Internal;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000064 RID: 100
	internal class ServerJobContext : IJobContext
	{
		// Token: 0x06000311 RID: 785 RVA: 0x0000D32E File Offset: 0x0000B52E
		public static ServerJobContext ConstructJobContext(RunningJobContext ctx)
		{
			if (ctx == null)
			{
				return null;
			}
			return new ServerJobContext(ctx);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000D33B File Offset: 0x0000B53B
		public static ServerJobContext ConstructJobContext(ReportExecutionInfo executionInfo)
		{
			if (executionInfo == null)
			{
				return null;
			}
			return new ServerJobContext(executionInfo);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000D348 File Offset: 0x0000B548
		private ServerJobContext(RunningJobContext ctx)
		{
			this.m_executionInfo = ctx.ExecutionInfo;
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000D367 File Offset: 0x0000B567
		private ServerJobContext(ReportExecutionInfo executionInfo)
		{
			this.m_executionInfo = executionInfo;
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000315 RID: 789 RVA: 0x0000D381 File Offset: 0x0000B581
		public object SyncRoot
		{
			get
			{
				return this.m_syncObj;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000316 RID: 790 RVA: 0x0000D389 File Offset: 0x0000B589
		public ExecutionLogLevel ExecutionLogLevel
		{
			get
			{
				return this.m_executionInfo.ExecutionLogLevel;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000317 RID: 791 RVA: 0x0000D396 File Offset: 0x0000B596
		// (set) Token: 0x06000318 RID: 792 RVA: 0x0000D3A3 File Offset: 0x0000B5A3
		public TimeSpan TimeDataRetrieval
		{
			get
			{
				return this.m_executionInfo.DataRetrievalTime;
			}
			set
			{
				this.m_executionInfo.DataRetrievalTime = value;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000319 RID: 793 RVA: 0x0000D3B1 File Offset: 0x0000B5B1
		// (set) Token: 0x0600031A RID: 794 RVA: 0x0000D3BE File Offset: 0x0000B5BE
		public TimeSpan TimeProcessing
		{
			get
			{
				return this.m_executionInfo.ProcessingTime;
			}
			set
			{
				this.m_executionInfo.ProcessingTime = value;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600031B RID: 795 RVA: 0x0000D3CC File Offset: 0x0000B5CC
		// (set) Token: 0x0600031C RID: 796 RVA: 0x0000D3D9 File Offset: 0x0000B5D9
		public TimeSpan TimeRendering
		{
			get
			{
				return this.m_executionInfo.RenderingTime;
			}
			set
			{
				this.m_executionInfo.RenderingTime = value;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600031D RID: 797 RVA: 0x0000D3E7 File Offset: 0x0000B5E7
		// (set) Token: 0x0600031E RID: 798 RVA: 0x0000D3F4 File Offset: 0x0000B5F4
		public long RowCount
		{
			get
			{
				return this.m_executionInfo.RowCount;
			}
			set
			{
				this.m_executionInfo.RowCount = value;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x0600031F RID: 799 RVA: 0x0000D402 File Offset: 0x0000B602
		public AdditionalInfo AdditionalInfo
		{
			get
			{
				return this.m_executionInfo.AdditionalInfo;
			}
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000D40F File Offset: 0x0000B60F
		public void AddAbortHelper(IAbortHelper abortHelper)
		{
			if (ProcessingContext.ThreadContext != null)
			{
				ProcessingContext.ThreadContext.AddAbortHelper(abortHelper);
			}
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000D423 File Offset: 0x0000B623
		public IAbortHelper GetAbortHelper()
		{
			if (ProcessingContext.ThreadContext != null)
			{
				return ProcessingContext.ThreadContext.GetAbortHelper();
			}
			return null;
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000D438 File Offset: 0x0000B638
		public void RemoveAbortHelper()
		{
			if (ProcessingContext.ThreadContext != null)
			{
				ProcessingContext.ThreadContext.RemoveAbortHelper();
			}
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000D44B File Offset: 0x0000B64B
		public void AddCommand(IDbCommand cmd)
		{
			if (ProcessingContext.ThreadContext != null)
			{
				ProcessingContext.ThreadContext.AddCommand(cmd);
			}
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000D45F File Offset: 0x0000B65F
		public void RemoveCommand(IDbCommand cmd)
		{
			if (ProcessingContext.ThreadContext != null)
			{
				ProcessingContext.ThreadContext.RemoveCommand(cmd);
			}
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00004F49 File Offset: 0x00003149
		public bool ApplyCommandMemoryLimit(IDbCommand cmd)
		{
			return false;
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00004F49 File Offset: 0x00003149
		public bool SetAdditionalCorrelation(IDbCommand cmd)
		{
			return false;
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000D473 File Offset: 0x0000B673
		public void TryQueueWorkItem(WaitCallback callback, object state)
		{
			ReportServerThreadPool.TryQueueWorkItem(new ThreadWorkItem(callback, state));
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000D481 File Offset: 0x0000B681
		public void QueueWorkItem(WaitCallback callback, object state)
		{
			ReportServerThreadPool.QueueUserWorkItem(callback, state);
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000329 RID: 809 RVA: 0x0000D48A File Offset: 0x0000B68A
		public string ExecutionId
		{
			get
			{
				return this.m_executionInfo.ExecutionId;
			}
		}

		// Token: 0x04000330 RID: 816
		private object m_syncObj = new object();

		// Token: 0x04000331 RID: 817
		private ReportExecutionInfo m_executionInfo;
	}
}
