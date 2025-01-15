using System;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000112 RID: 274
	internal sealed class PersistedStreamExecutionProxy : RenderAsyncExecutionBase, IReportExecution
	{
		// Token: 0x06000ACB RID: 2763 RVA: 0x00028BEA File Offset: 0x00026DEA
		public PersistedStreamExecutionProxy(IReportExecution wrappedExecution, IExecutionDataProvider provider, ClientRequest session, bool clearOldStreams, bool waitPersistedStreamCompletion)
		{
			this.m_wrappedReportExecution = wrappedExecution;
			this.m_provider = provider;
			this.m_session = session;
			this.m_clearOldStreams = clearOldStreams;
			this.m_waitPersistedStreamCompletion = waitPersistedStreamCompletion;
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x00028C18 File Offset: 0x00026E18
		public ExecutionResult ExecuteReport()
		{
			this.m_provider.StreamManager.SetPersistStreams(this.m_session.SessionID, this.m_clearOldStreams);
			RSStream rsstream = base.StartAsyncRender(this.m_provider, this.m_waitPersistedStreamCompletion);
			return new ExecutionResult
			{
				OutputStream = rsstream
			};
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x00028C65 File Offset: 0x00026E65
		protected override void PerformRender()
		{
			this.m_wrappedReportExecution.ExecuteReport();
		}

		// Token: 0x040004A4 RID: 1188
		private readonly IExecutionDataProvider m_provider;

		// Token: 0x040004A5 RID: 1189
		private readonly IReportExecution m_wrappedReportExecution;

		// Token: 0x040004A6 RID: 1190
		private readonly ClientRequest m_session;

		// Token: 0x040004A7 RID: 1191
		private readonly bool m_clearOldStreams;

		// Token: 0x040004A8 RID: 1192
		private readonly bool m_waitPersistedStreamCompletion;
	}
}
