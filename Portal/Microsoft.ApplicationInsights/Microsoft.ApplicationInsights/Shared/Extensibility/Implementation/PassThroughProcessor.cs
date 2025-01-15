using System;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;

namespace Microsoft.ApplicationInsights.Shared.Extensibility.Implementation
{
	// Token: 0x02000050 RID: 80
	internal class PassThroughProcessor : ITelemetryProcessor
	{
		// Token: 0x06000289 RID: 649 RVA: 0x0000CFA4 File Offset: 0x0000B1A4
		public PassThroughProcessor(TelemetrySink sink)
		{
			if (sink == null)
			{
				throw new ArgumentNullException("sink");
			}
			this.Sink = sink;
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600028A RID: 650 RVA: 0x0000CFC1 File Offset: 0x0000B1C1
		internal TelemetrySink Sink { get; }

		// Token: 0x0600028B RID: 651 RVA: 0x0000CFC9 File Offset: 0x0000B1C9
		public void Process(ITelemetry item)
		{
			this.Sink.Process(item);
		}
	}
}
