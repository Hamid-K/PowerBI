using System;
using System.Collections.Generic;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.Tracing
{
	// Token: 0x020000A2 RID: 162
	internal class PortalDiagnosticsQueueSender : IDiagnosticsSender
	{
		// Token: 0x060004F5 RID: 1269 RVA: 0x00014F2C File Offset: 0x0001312C
		public PortalDiagnosticsQueueSender()
		{
			this.EventData = new List<TraceEvent>();
			this.IsDisabled = false;
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x00014F46 File Offset: 0x00013146
		public IList<TraceEvent> EventData { get; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x00014F4E File Offset: 0x0001314E
		// (set) Token: 0x060004F8 RID: 1272 RVA: 0x00014F56 File Offset: 0x00013156
		public bool IsDisabled { get; set; }

		// Token: 0x060004F9 RID: 1273 RVA: 0x00014F5F File Offset: 0x0001315F
		public void Send(TraceEvent eventData)
		{
			if (!this.IsDisabled)
			{
				this.EventData.Add(eventData);
			}
		}
	}
}
