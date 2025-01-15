using System;
using System.Threading;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000064 RID: 100
	internal abstract class CancelablePhaseBase : IDisposable
	{
		// Token: 0x060002F4 RID: 756 RVA: 0x0000A9BC File Offset: 0x00008BBC
		protected CancelablePhaseBase(string jobId, ExternalItemPath requestPath, JobActionEnum action, JobType jobType, UserContext userContext, ExecutionLogLevel execLogLevel)
		{
			this.m_jobCtx = RunningJobList.Current.AddJob(jobId, requestPath, action, jobType, userContext, ProcessingContext.ReqContext);
			this.m_jobCtx.ExecutionInfo.ExecutionLogLevel = execLogLevel;
			this.m_crtThreadCtx = this.m_jobCtx.InitialAndOnlyThreadCtx;
			this.m_preservedContext.Capture();
			ProcessingContext.JobContext = this.m_jobCtx;
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000AA24 File Offset: 0x00008C24
		protected CancelablePhaseBase(RunningJobContext jobCtx, WaitCallback waitCallback, object callbackState, bool allowCancel)
		{
			this.m_jobCtx = jobCtx;
			if (this.m_jobCtx != null && allowCancel)
			{
				this.m_crtThreadCtx = this.m_jobCtx.AddWorkThread();
			}
			this.m_preservedContext.Capture();
			ProcessingContext.JobContext = this.m_jobCtx;
			this.m_waitCallback = waitCallback;
			this.m_callbackState = callbackState;
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060002F6 RID: 758 RVA: 0x0000AA84 File Offset: 0x00008C84
		// (remove) Token: 0x060002F7 RID: 759 RVA: 0x0000AAB8 File Offset: 0x00008CB8
		public static event CancelablePhaseBase.JobEventHandler JobEnd;

		// Token: 0x060002F8 RID: 760
		protected abstract void Execute();

		// Token: 0x060002F9 RID: 761 RVA: 0x0000AAEC File Offset: 0x00008CEC
		public void ExecuteWrapper()
		{
			try
			{
				if (this.ThreadContext == null)
				{
					if (this.m_waitCallback != null)
					{
						this.m_waitCallback(this.m_callbackState);
					}
					else
					{
						this.Execute();
					}
				}
				else
				{
					try
					{
						this.ThreadContext.BeginCancelableState();
						try
						{
							if (this.m_waitCallback != null)
							{
								this.m_waitCallback(this.m_callbackState);
							}
							else
							{
								this.Execute();
							}
						}
						finally
						{
							this.ThreadContext.EndCancelableState();
						}
						this.ThreadContext.WaitForCancelException();
					}
					catch (Exception ex)
					{
						Exception ex2 = this.ThreadContext.RunningJobContext.ExecutionInfo.SetStatusFromException(ex);
						if (ex2 != null)
						{
							throw ex2;
						}
						throw;
					}
				}
			}
			finally
			{
				if (CancelablePhaseBase.JobEnd != null)
				{
					CancelablePhaseBase.JobEnd(this, this.m_jobCtx);
				}
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060002FA RID: 762 RVA: 0x0000ABD4 File Offset: 0x00008DD4
		private ThreadJobContext ThreadContext
		{
			get
			{
				return this.m_crtThreadCtx;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060002FB RID: 763 RVA: 0x0000ABDC File Offset: 0x00008DDC
		public JobTypeEnum JobType
		{
			get
			{
				return this.m_jobCtx.Type;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060002FC RID: 764 RVA: 0x0000ABE9 File Offset: 0x00008DE9
		protected string JobId
		{
			get
			{
				return this.m_jobCtx.JobId;
			}
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000ABF6 File Offset: 0x00008DF6
		void IDisposable.Dispose()
		{
			if (this.m_jobCtx != null && this.m_crtThreadCtx != null)
			{
				this.m_jobCtx.RemoveWorkThread(this.m_crtThreadCtx);
			}
			this.m_preservedContext.Restore();
		}

		// Token: 0x04000169 RID: 361
		private RunningJobContext m_jobCtx;

		// Token: 0x0400016A RID: 362
		private ThreadJobContext m_crtThreadCtx;

		// Token: 0x0400016B RID: 363
		private CancelablePhaseBase.PreservedContext m_preservedContext;

		// Token: 0x0400016C RID: 364
		private WaitCallback m_waitCallback;

		// Token: 0x0400016D RID: 365
		private object m_callbackState;

		// Token: 0x020000EC RID: 236
		// (Invoke) Token: 0x060007BF RID: 1983
		public delegate void JobEventHandler(CancelablePhaseBase sender, RunningJobContext context);

		// Token: 0x020000ED RID: 237
		public struct PreservedContext
		{
			// Token: 0x060007C2 RID: 1986 RVA: 0x0001476A File Offset: 0x0001296A
			public void Capture()
			{
				this.m_previousJobContext = ProcessingContext.JobContext;
				this.m_previousRequestContext = ProcessingContext.ReqContext;
				this.m_captured = true;
			}

			// Token: 0x060007C3 RID: 1987 RVA: 0x00014789 File Offset: 0x00012989
			public void Restore()
			{
				if (this.m_captured)
				{
					ProcessingContext.JobContext = this.m_previousJobContext;
					ProcessingContext.ReqContext = this.m_previousRequestContext;
				}
			}

			// Token: 0x040004AB RID: 1195
			private bool m_captured;

			// Token: 0x040004AC RID: 1196
			private RunningJobContext m_previousJobContext;

			// Token: 0x040004AD RID: 1197
			private RequestContext m_previousRequestContext;
		}
	}
}
