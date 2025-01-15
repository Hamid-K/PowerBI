using System;
using Microsoft.ApplicationInsights.Channel;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation
{
	// Token: 0x02000086 RID: 134
	internal class TransmissionProcessor : ITelemetryProcessor
	{
		// Token: 0x06000445 RID: 1093 RVA: 0x00013108 File Offset: 0x00011308
		internal TransmissionProcessor(TelemetrySink sink)
		{
			if (sink == null)
			{
				throw new ArgumentNullException("sink");
			}
			this.sink = sink;
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00013125 File Offset: 0x00011325
		public void Process(ITelemetry item)
		{
			TelemetryDebugWriter.WriteTelemetry(item, null);
			this.sink.TelemetryChannel.Send(item);
		}

		// Token: 0x040001AA RID: 426
		private readonly TelemetrySink sink;
	}
}
