using System;
using System.Diagnostics;
using Microsoft.Mashup.Common;

namespace Microsoft.Mashup.Tracing
{
	// Token: 0x020020B7 RID: 8375
	internal abstract class TraceTime
	{
		// Token: 0x0600CD06 RID: 52486
		public abstract void AddStart(Trace trace);

		// Token: 0x0600CD07 RID: 52487
		public abstract void AddDuration(Trace trace);

		// Token: 0x0600CD08 RID: 52488 RVA: 0x0028C591 File Offset: 0x0028A791
		public static TraceTime FromNow()
		{
			return new TraceTime.FromNowTraceTime();
		}

		// Token: 0x020020B8 RID: 8376
		private class FromTraceTime : TraceTime
		{
			// Token: 0x0600CD0A RID: 52490 RVA: 0x0028C598 File Offset: 0x0028A798
			public FromTraceTime(DateTime startTime)
			{
				this.startTime = startTime;
			}

			// Token: 0x0600CD0B RID: 52491 RVA: 0x0028C5A7 File Offset: 0x0028A7A7
			public override void AddStart(Trace trace)
			{
				trace.AddStart(this.startTime);
			}

			// Token: 0x0600CD0C RID: 52492 RVA: 0x0028C5B8 File Offset: 0x0028A7B8
			public override void AddDuration(Trace trace)
			{
				TimeSpan timeSpan = HiResDateTime.UtcNow - this.startTime;
				trace.AddDuration(timeSpan);
			}

			// Token: 0x040067CC RID: 26572
			private readonly DateTime startTime;
		}

		// Token: 0x020020B9 RID: 8377
		private class FromNowTraceTime : TraceTime
		{
			// Token: 0x0600CD0D RID: 52493 RVA: 0x0028C5DD File Offset: 0x0028A7DD
			public FromNowTraceTime()
			{
				this.startTime = HiResDateTime.UtcNow;
				this.stopwatch = new Stopwatch();
				this.stopwatch.Start();
			}

			// Token: 0x0600CD0E RID: 52494 RVA: 0x0028C606 File Offset: 0x0028A806
			public override void AddStart(Trace trace)
			{
				trace.AddStart(this.startTime);
			}

			// Token: 0x0600CD0F RID: 52495 RVA: 0x0028C614 File Offset: 0x0028A814
			public override void AddDuration(Trace trace)
			{
				this.stopwatch.Stop();
				trace.AddDuration(this.stopwatch.Elapsed);
			}

			// Token: 0x040067CD RID: 26573
			private readonly DateTime startTime;

			// Token: 0x040067CE RID: 26574
			private readonly Stopwatch stopwatch;
		}
	}
}
