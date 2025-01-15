using System;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000E3 RID: 227
	public sealed class TraceStoppedEventArgs : EventArgs
	{
		// Token: 0x06000EF1 RID: 3825 RVA: 0x00073C84 File Offset: 0x00071E84
		internal TraceStoppedEventArgs(TraceStopCause stopCause, Exception exception)
		{
			this.stopCause = stopCause;
			this.exception = exception;
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000EF2 RID: 3826 RVA: 0x00073C9A File Offset: 0x00071E9A
		public TraceStopCause StopCause
		{
			get
			{
				return this.stopCause;
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000EF3 RID: 3827 RVA: 0x00073CA2 File Offset: 0x00071EA2
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x040001C5 RID: 453
		private TraceStopCause stopCause;

		// Token: 0x040001C6 RID: 454
		private Exception exception;
	}
}
