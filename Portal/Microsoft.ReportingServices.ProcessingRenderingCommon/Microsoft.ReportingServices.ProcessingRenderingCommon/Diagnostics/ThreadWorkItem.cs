using System;
using System.Diagnostics;
using System.Security.Principal;
using System.Threading;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000094 RID: 148
	public sealed class ThreadWorkItem
	{
		// Token: 0x0600049C RID: 1180 RVA: 0x0000E4C4 File Offset: 0x0000C6C4
		public ThreadWorkItem(WaitCallback callback, object state, PreQueueDelegate preQueue, object preQueueState, bool allowCancel)
		{
			this.m_callback = callback;
			this.m_state = state;
			this.m_preQueue = preQueue;
			this.m_preQueueState = preQueueState;
			this.m_allowCancel = allowCancel;
			this.GetOriginalImpersonationToken();
			this.m_serviceInstanceContext = ProcessingContext.ServiceInstanceContext;
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0000E502 File Offset: 0x0000C702
		public ThreadWorkItem(WaitCallback callback, object state)
			: this(callback, state, null, null, false)
		{
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0000E50F File Offset: 0x0000C70F
		internal void Close()
		{
			if (this.m_origThreadUser != null)
			{
				this.m_origThreadUser.Dispose();
				this.m_origThreadUser = null;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x0600049F RID: 1183 RVA: 0x0000E52B File Offset: 0x0000C72B
		public WaitCallback MonitoredCallback
		{
			get
			{
				return new WaitCallback(this.MonitoredThreadPoolCallback);
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x0000E539 File Offset: 0x0000C739
		public WaitCallback FullCallback
		{
			get
			{
				return new WaitCallback(this.FullSafeThreadPoolCallback);
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x0000E547 File Offset: 0x0000C747
		public WaitCallback OriginalCallback
		{
			get
			{
				return this.m_callback;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x0000E54F File Offset: 0x0000C74F
		public object State
		{
			get
			{
				return this.m_state;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x0000E557 File Offset: 0x0000C757
		public PreQueueDelegate PreQueue
		{
			get
			{
				return this.m_preQueue;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x0000E55F File Offset: 0x0000C75F
		public object PreQueueState
		{
			get
			{
				return this.m_preQueueState;
			}
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0000E568 File Offset: 0x0000C768
		private void MonitoredThreadPoolCallback(object state)
		{
			try
			{
				ReportServerThreadPool.IncrementCurrentReportThreads();
				ProcessingContext.InitializeRequestContext(new BackgroundRequestContext(this.m_serviceInstanceContext));
				this.m_callback(this.m_state);
			}
			finally
			{
				ProcessingContext.EndRequestContext();
				ReportServerThreadPool.DecrementCurrentReportThreads();
			}
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0000E5BC File Offset: 0x0000C7BC
		private void FullSafeThreadPoolCallback(object param)
		{
			RequestCache requestCache = null;
			try
			{
				ReportServerThreadPool.IncrementCurrentReportThreads();
				ProcessingContext.InitializeRequestContext(new BackgroundRequestContext(this.m_serviceInstanceContext));
				RunningJobContext ctx = ((ThreadWorkItem.WorkItemParameters)param).ctx;
				requestCache = RequestCache.BindToThread(null);
				RequestCache.InitializeForRequest();
				CancelablePhaseBase.PreservedContext preservedContext = default(CancelablePhaseBase.PreservedContext);
				preservedContext.Capture();
				WindowsImpersonationContext windowsImpersonationContext = null;
				bool flag = false;
				try
				{
					ProcessingContext.JobContext = ctx;
					if (!Thread.CurrentThread.IsThreadPoolThread)
					{
						ProcessingContext.JobsTracer.Trace(TraceLevel.Info, "Re-queuing on a threadpool thread");
						ReportServerThreadPool.TryQueueWorkItem(this);
						flag = true;
					}
					else
					{
						if (this.m_origThreadUser != null)
						{
							windowsImpersonationContext = this.m_origThreadUser.Impersonate();
						}
						ProcessingContext.JobsTracer.Trace(TraceLevel.Verbose, "Executing user callback");
						using (ThreadWorkItemCancelableStep threadWorkItemCancelableStep = new ThreadWorkItemCancelableStep(ctx, this.m_callback, this.m_state, this.m_allowCancel))
						{
							threadWorkItemCancelableStep.ExecuteWrapper();
						}
					}
				}
				finally
				{
					preservedContext.Restore();
					if (!flag && this.m_origThreadUser != null)
					{
						this.m_origThreadUser.Dispose();
						this.m_origThreadUser = null;
					}
					if (windowsImpersonationContext != null)
					{
						windowsImpersonationContext.Undo();
					}
				}
			}
			catch (Exception ex)
			{
				ProcessingContext.JobsTracer.Trace(TraceLevel.Error, "error in thread function: {0}", new object[] { ex });
			}
			finally
			{
				RequestCache.ReleaseReferenceAndDetach();
				RequestCache.BindToThread(requestCache);
				ProcessingContext.EndRequestContext();
				ReportServerThreadPool.DecrementCurrentReportThreads();
			}
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x0000E75C File Offset: 0x0000C95C
		private void GetOriginalImpersonationToken()
		{
			WindowsIdentity current = WindowsIdentity.GetCurrent(true);
			if (current != null && current.Token != IntPtr.Zero)
			{
				this.m_origThreadUser = current;
			}
		}

		// Token: 0x040002B4 RID: 692
		private WaitCallback m_callback;

		// Token: 0x040002B5 RID: 693
		private PreQueueDelegate m_preQueue;

		// Token: 0x040002B6 RID: 694
		private object m_state;

		// Token: 0x040002B7 RID: 695
		private object m_preQueueState;

		// Token: 0x040002B8 RID: 696
		private bool m_allowCancel;

		// Token: 0x040002B9 RID: 697
		private WindowsIdentity m_origThreadUser;

		// Token: 0x040002BA RID: 698
		private IServiceInstanceContext m_serviceInstanceContext;

		// Token: 0x020000FA RID: 250
		public struct WorkItemParameters
		{
			// Token: 0x060007DA RID: 2010 RVA: 0x000149A8 File Offset: 0x00012BA8
			public WorkItemParameters(RunningJobContext ctx, RequestCache requestCache)
			{
				this.ctx = ctx;
				this.requestCache = requestCache;
			}

			// Token: 0x040004D9 RID: 1241
			public RunningJobContext ctx;

			// Token: 0x040004DA RID: 1242
			public RequestCache requestCache;
		}
	}
}
