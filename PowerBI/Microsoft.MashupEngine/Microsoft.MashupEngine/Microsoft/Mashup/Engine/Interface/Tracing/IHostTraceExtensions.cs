using System;

namespace Microsoft.Mashup.Engine.Interface.Tracing
{
	// Token: 0x0200012D RID: 301
	public static class IHostTraceExtensions
	{
		// Token: 0x06000536 RID: 1334 RVA: 0x00007E51 File Offset: 0x00006051
		public static IHostTraceExtensions.TimedScope NewTimedScope(this IHostTrace trace)
		{
			return new IHostTraceExtensions.TimedScope(trace);
		}

		// Token: 0x0200012E RID: 302
		public struct TimedScope : IDisposable
		{
			// Token: 0x06000537 RID: 1335 RVA: 0x00007E59 File Offset: 0x00006059
			public TimedScope(IHostTrace trace)
			{
				this.trace = trace;
				this.trace.Resume();
			}

			// Token: 0x06000538 RID: 1336 RVA: 0x00007E6D File Offset: 0x0000606D
			public void Dispose()
			{
				this.trace.Suspend();
			}

			// Token: 0x04000336 RID: 822
			private readonly IHostTrace trace;
		}
	}
}
