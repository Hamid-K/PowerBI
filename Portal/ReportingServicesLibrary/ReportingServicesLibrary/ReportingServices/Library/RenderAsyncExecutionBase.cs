using System;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000018 RID: 24
	internal abstract class RenderAsyncExecutionBase : AsyncExecution
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00004D6C File Offset: 0x00002F6C
		protected IExecutionDataProvider DataProvider
		{
			get
			{
				return this.m_dataProvider;
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00004D74 File Offset: 0x00002F74
		protected RSStream StartAsyncRender(IExecutionDataProvider dataProvider, bool waitPersistedStreamCompletion)
		{
			this.m_dataProvider = dataProvider;
			if (!waitPersistedStreamCompletion)
			{
				this.DataProvider.StreamManager.PersistedStreamManager.InitialStreamFinished += this.PersistedStreamManager_InitialStreamFinished;
			}
			base.StartAsyncThread();
			return this.DataProvider.StreamManager.PrimaryStream;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00004DC4 File Offset: 0x00002FC4
		protected override void PerformAsyncWork()
		{
			try
			{
				if (!base.IsNewThread)
				{
					this.DataProvider.StreamManager.PersistedStreamManager.PerformLocking = false;
				}
				this.DataProvider.SuspendStreamCleanup();
				this.PerformRender();
			}
			catch (Exception ex)
			{
				this.DataProvider.StreamManager.PersistedStreamManager.SetError(ex);
				throw ex;
			}
			finally
			{
				try
				{
					this.DataProvider.StreamManager.PersistedStreamManager.AllStreamsFinished();
				}
				finally
				{
					this.DataProvider.ResumeStreamCleanup();
				}
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00004E68 File Offset: 0x00003068
		protected override void ErrorOccurred(Exception error)
		{
			if (this.DataProvider.StreamManager != null && this.DataProvider.StreamManager.PersistedStreamManager != null)
			{
				this.DataProvider.StreamManager.PersistedStreamManager.SetError(error);
			}
		}

		// Token: 0x06000081 RID: 129
		protected abstract void PerformRender();

		// Token: 0x06000082 RID: 130 RVA: 0x00004E9F File Offset: 0x0000309F
		private void PersistedStreamManager_InitialStreamFinished(object sender, EventArgs e)
		{
			base.ContinueOriginalThread();
		}

		// Token: 0x040000A4 RID: 164
		private IExecutionDataProvider m_dataProvider;
	}
}
