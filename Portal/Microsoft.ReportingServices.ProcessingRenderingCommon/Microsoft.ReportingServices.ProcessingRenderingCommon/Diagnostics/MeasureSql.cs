using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200006F RID: 111
	internal sealed class MeasureSql : IDisposable
	{
		// Token: 0x0600030F RID: 783 RVA: 0x0000ADAA File Offset: 0x00008FAA
		public MeasureSql()
		{
			this.m_timer = new Timer();
			this.m_reqCtx = ProcessingContext.ReqContext;
			if (this.m_reqCtx != null)
			{
				this.m_timer.StartTimer();
			}
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000ADDB File Offset: 0x00008FDB
		public void Dispose()
		{
			if (this.m_reqCtx != null)
			{
				this.m_reqCtx.TotalDatabaseTime += TimeSpan.FromMilliseconds((double)this.m_timer.ElapsedTimeMs());
			}
		}

		// Token: 0x0400018D RID: 397
		private Timer m_timer;

		// Token: 0x0400018E RID: 398
		private RequestContext m_reqCtx;
	}
}
