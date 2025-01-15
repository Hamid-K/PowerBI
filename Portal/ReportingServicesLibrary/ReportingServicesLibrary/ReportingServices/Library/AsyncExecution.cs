using System;
using System.Globalization;
using System.Runtime.Remoting.Messaging;
using System.Security.Permissions;
using System.Threading;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000017 RID: 23
	[StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class AsyncExecution
	{
		// Token: 0x06000074 RID: 116 RVA: 0x00004B28 File Offset: 0x00002D28
		private void AsyncStartMain(object state)
		{
			AsyncExecution.ThreadStartInfo threadStartInfo = (AsyncExecution.ThreadStartInfo)state;
			try
			{
				if (threadStartInfo.StartThreadID == AppDomain.GetCurrentThreadId())
				{
					this.m_isNewThread = false;
				}
				if (this.m_isNewThread)
				{
					this.SetThreadStateInformation();
				}
				this.PerformAsyncWork();
			}
			catch (Exception ex)
			{
				if (RSTrace.CatalogTrace.TraceError)
				{
					RSTrace.CatalogTrace.Trace(ex.ToString());
				}
				this.m_exception = ex;
				if (this.m_continuedOriginalThread)
				{
					this.ErrorOccurred(this.m_exception);
				}
			}
			finally
			{
				this.ContinueOriginalThread();
				threadStartInfo.ThreadRunning = false;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00004BCC File Offset: 0x00002DCC
		protected bool IsNewThread
		{
			get
			{
				return this.m_isNewThread;
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00004BD4 File Offset: 0x00002DD4
		protected void ContinueOriginalThread()
		{
			ManualResetEvent continueEvent = this.m_continueEvent;
			lock (continueEvent)
			{
				if (!this.m_continueEvent.SafeWaitHandle.IsClosed)
				{
					this.m_continueEvent.Set();
					this.m_continuedOriginalThread = true;
				}
			}
		}

		// Token: 0x06000077 RID: 119
		protected abstract void PerformAsyncWork();

		// Token: 0x06000078 RID: 120
		protected abstract void ErrorOccurred(Exception error);

		// Token: 0x06000079 RID: 121 RVA: 0x00004C34 File Offset: 0x00002E34
		protected void StartAsyncThread()
		{
			try
			{
				this.m_continueEvent = new ManualResetEvent(false);
				this.GetThreadStateInformation();
				AsyncExecution.ThreadStartInfo threadStartInfo = new AsyncExecution.ThreadStartInfo();
				threadStartInfo.StartThreadID = AppDomain.GetCurrentThreadId();
				ReportServerThreadPool.TryQueueWorkItem(new ThreadWorkItem(new WaitCallback(this.AsyncStartMain), threadStartInfo));
				TimeSpan timeSpan = new TimeSpan(0, 5, 0);
				while (!this.m_continueEvent.WaitOne(timeSpan, false) && threadStartInfo.ThreadRunning)
				{
				}
				if (this.m_exception != null)
				{
					throw AsyncExecution.AsyncExecutionException.RethrowException(this.m_exception);
				}
			}
			finally
			{
				if (this.m_continueEvent != null)
				{
					ManualResetEvent continueEvent = this.m_continueEvent;
					lock (continueEvent)
					{
						this.m_continueEvent.Close();
					}
				}
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004D00 File Offset: 0x00002F00
		private void GetThreadStateInformation()
		{
			this.m_uiCulture = Thread.CurrentThread.CurrentUICulture;
			this.m_threadCulture = Thread.CurrentThread.CurrentCulture;
			this.m_clientCulture = CallContext.GetData(Localization.ClientPrimaryCultureKey);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00004D32 File Offset: 0x00002F32
		private void SetThreadStateInformation()
		{
			Thread.CurrentThread.CurrentUICulture = this.m_uiCulture;
			Thread.CurrentThread.CurrentCulture = this.m_threadCulture;
			CallContext.SetData(Localization.ClientPrimaryCultureKey, this.m_clientCulture);
		}

		// Token: 0x0400009D RID: 157
		private ManualResetEvent m_continueEvent = new ManualResetEvent(false);

		// Token: 0x0400009E RID: 158
		private Exception m_exception;

		// Token: 0x0400009F RID: 159
		private bool m_isNewThread = true;

		// Token: 0x040000A0 RID: 160
		private bool m_continuedOriginalThread;

		// Token: 0x040000A1 RID: 161
		private CultureInfo m_uiCulture;

		// Token: 0x040000A2 RID: 162
		private CultureInfo m_threadCulture;

		// Token: 0x040000A3 RID: 163
		private object m_clientCulture;

		// Token: 0x02000435 RID: 1077
		private class ThreadStartInfo
		{
			// Token: 0x04000F10 RID: 3856
			public int StartThreadID;

			// Token: 0x04000F11 RID: 3857
			public bool ThreadRunning = true;
		}

		// Token: 0x02000436 RID: 1078
		private class AsyncExecutionException : RSException
		{
			// Token: 0x060022E1 RID: 8929 RVA: 0x00082BEA File Offset: 0x00080DEA
			private AsyncExecutionException(RSException inner)
				: base(inner)
			{
			}

			// Token: 0x060022E2 RID: 8930 RVA: 0x00082BF3 File Offset: 0x00080DF3
			public static Exception RethrowException(Exception e)
			{
				if (e is RSException)
				{
					return new AsyncExecution.AsyncExecutionException(e as RSException);
				}
				return new InternalCatalogException(e, "Unknown error occurred on async thread");
			}
		}
	}
}
