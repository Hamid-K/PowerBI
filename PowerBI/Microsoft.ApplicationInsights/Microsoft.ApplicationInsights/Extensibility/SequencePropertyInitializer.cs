using System;
using System.Threading;
using Microsoft.ApplicationInsights.Channel;

namespace Microsoft.ApplicationInsights.Extensibility
{
	// Token: 0x0200005C RID: 92
	public sealed class SequencePropertyInitializer : ITelemetryInitializer
	{
		// Token: 0x060002AB RID: 683 RVA: 0x0000D210 File Offset: 0x0000B410
		public void Initialize(ITelemetry telemetry)
		{
			if (string.IsNullOrEmpty(telemetry.Sequence))
			{
				telemetry.Sequence = this.stablePrefix + Interlocked.Increment(ref this.currentNumber);
			}
		}

		// Token: 0x04000123 RID: 291
		private readonly string stablePrefix = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).TrimEnd(new char[] { '=' }) + ":";

		// Token: 0x04000124 RID: 292
		private long currentNumber;
	}
}
