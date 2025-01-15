using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Microsoft.Mashup.Engine.Interface.Tracing
{
	// Token: 0x02000131 RID: 305
	public sealed class MaxCountTraceSource : IHostTraceSource, IDisposable
	{
		// Token: 0x06000542 RID: 1346 RVA: 0x00007E7A File Offset: 0x0000607A
		public MaxCountTraceSource(IHostTraceSource traceSource, int maxCount)
		{
			this.traceSource = traceSource;
			this.maxCount = maxCount;
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00007E90 File Offset: 0x00006090
		public IHostTrace CreateTrace()
		{
			this.count++;
			if (this.trace != null)
			{
				return this.trace;
			}
			IHostTrace hostTrace = this.traceSource.CreateTrace();
			if (this.count == this.maxCount)
			{
				this.trace = new MaxCountTraceSource.AggregatingTrace(hostTrace);
				hostTrace = this.trace;
			}
			return hostTrace;
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x00007EE8 File Offset: 0x000060E8
		public void Dispose()
		{
			if (this.trace != null)
			{
				this.trace.FinishTrace(this.count);
				this.trace = null;
			}
		}

		// Token: 0x04000337 RID: 823
		private readonly IHostTraceSource traceSource;

		// Token: 0x04000338 RID: 824
		private readonly int maxCount;

		// Token: 0x04000339 RID: 825
		private int count;

		// Token: 0x0400033A RID: 826
		private MaxCountTraceSource.AggregatingTrace trace;

		// Token: 0x02000132 RID: 306
		public sealed class AggregatingTrace : IHostTrace, IDisposable
		{
			// Token: 0x06000545 RID: 1349 RVA: 0x00007F0A File Offset: 0x0000610A
			public AggregatingTrace(IHostTrace trace)
			{
				this.trace = trace;
				this.seen = new HashSet<string>();
			}

			// Token: 0x170001DA RID: 474
			// (get) Token: 0x06000546 RID: 1350 RVA: 0x00007F24 File Offset: 0x00006124
			public SourceLevels Levels
			{
				get
				{
					return this.trace.Levels;
				}
			}

			// Token: 0x06000547 RID: 1351 RVA: 0x00007F31 File Offset: 0x00006131
			public void Add(string name, object value, bool isPii)
			{
				if (this.seen.Add(name))
				{
					this.trace.Add(name, value, isPii);
				}
			}

			// Token: 0x06000548 RID: 1352 RVA: 0x00007F4F File Offset: 0x0000614F
			public void Add(Exception e, bool hasPii = true)
			{
				if (this.seen.Add("Exception"))
				{
					this.trace.Add(e, hasPii);
				}
			}

			// Token: 0x06000549 RID: 1353 RVA: 0x00007F70 File Offset: 0x00006170
			public void Add(Exception e, TraceEventType type, bool hasPii = true)
			{
				if (this.seen.Add("Exception"))
				{
					this.trace.Add(e, type, hasPii);
				}
			}

			// Token: 0x0600054A RID: 1354 RVA: 0x00007F92 File Offset: 0x00006192
			public IHostTraceValue Begin(string name, bool isPii)
			{
				if (this.seen.Add(name))
				{
					return this.trace.Begin(name, isPii);
				}
				return MaxCountTraceSource.AggregatingTrace.DummyHostTraceValue.Instance;
			}

			// Token: 0x0600054B RID: 1355 RVA: 0x00007FB5 File Offset: 0x000061B5
			public void Resume()
			{
				this.trace.Resume();
			}

			// Token: 0x0600054C RID: 1356 RVA: 0x00007FC2 File Offset: 0x000061C2
			public void Suspend()
			{
				this.trace.Suspend();
			}

			// Token: 0x0600054D RID: 1357 RVA: 0x0000336E File Offset: 0x0000156E
			public void Dispose()
			{
			}

			// Token: 0x0600054E RID: 1358 RVA: 0x00007FCF File Offset: 0x000061CF
			public void FinishTrace(int totalCount)
			{
				this.Add("TotalEntryCount", totalCount, false);
				this.trace.Dispose();
			}

			// Token: 0x0400033B RID: 827
			private const string ExceptionName = "Exception";

			// Token: 0x0400033C RID: 828
			private readonly IHostTrace trace;

			// Token: 0x0400033D RID: 829
			private readonly HashSet<string> seen;

			// Token: 0x02000133 RID: 307
			private sealed class DummyHostTraceValue : IHostTraceValue, IDisposable
			{
				// Token: 0x0600054F RID: 1359 RVA: 0x0000336E File Offset: 0x0000156E
				public void Add(object value)
				{
				}

				// Token: 0x06000550 RID: 1360 RVA: 0x0000336E File Offset: 0x0000156E
				public void Begin()
				{
				}

				// Token: 0x06000551 RID: 1361 RVA: 0x0000336E File Offset: 0x0000156E
				public void End()
				{
				}

				// Token: 0x06000552 RID: 1362 RVA: 0x0000336E File Offset: 0x0000156E
				public void Dispose()
				{
				}

				// Token: 0x0400033E RID: 830
				public static IHostTraceValue Instance = new MaxCountTraceSource.AggregatingTrace.DummyHostTraceValue();
			}
		}
	}
}
