using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.Tracing
{
	// Token: 0x02000099 RID: 153
	internal class DiagnosticsListener : IDisposable
	{
		// Token: 0x060004CC RID: 1228 RVA: 0x0001498C File Offset: 0x00012B8C
		public DiagnosticsListener(IList<IDiagnosticsSender> senders)
		{
			if (senders == null || senders.Count < 1)
			{
				throw new ArgumentNullException("senders");
			}
			this.diagnosticsSenders = senders;
			this.eventListener = new DiagnosticsEventListener(this, this.LogLevel);
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x000149E1 File Offset: 0x00012BE1
		// (set) Token: 0x060004CE RID: 1230 RVA: 0x000149E9 File Offset: 0x00012BE9
		public EventLevel LogLevel
		{
			get
			{
				return this.logLevel;
			}
			set
			{
				if (this.LogLevel != value)
				{
					EventListener eventListener = this.eventListener;
					this.eventListener = new DiagnosticsEventListener(this, value);
					eventListener.Dispose();
				}
				this.logLevel = value;
			}
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00014A14 File Offset: 0x00012C14
		public void WriteEvent(TraceEvent eventData)
		{
			if (eventData.MetaData != null && eventData.MetaData.MessageFormat != null && eventData.MetaData.Level <= this.LogLevel)
			{
				foreach (IDiagnosticsSender diagnosticsSender in this.diagnosticsSenders)
				{
					diagnosticsSender.Send(eventData);
				}
			}
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00014A88 File Offset: 0x00012C88
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00014A97 File Offset: 0x00012C97
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.eventListener.Dispose();
			}
		}

		// Token: 0x040001E4 RID: 484
		private readonly IList<IDiagnosticsSender> diagnosticsSenders = new List<IDiagnosticsSender>();

		// Token: 0x040001E5 RID: 485
		private EventLevel logLevel = EventLevel.Error;

		// Token: 0x040001E6 RID: 486
		private DiagnosticsEventListener eventListener;
	}
}
