using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Mashup.Engine.Interface.Tracing;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001C20 RID: 7200
	public sealed class SplitsTrace : IDisposable
	{
		// Token: 0x0600B3B5 RID: 46005 RVA: 0x00247F48 File Offset: 0x00246148
		public SplitsTrace(IHostTrace trace)
		{
			this.trace = trace;
			this.splitCount = 1;
		}

		// Token: 0x0600B3B6 RID: 46006 RVA: 0x00247F5E File Offset: 0x0024615E
		public IHostTrace CreateSplit(string name)
		{
			this.AddRef();
			return new SplitsTrace.SplitTrace(this, name, HiResDateTime.UtcNow);
		}

		// Token: 0x0600B3B7 RID: 46007 RVA: 0x00247F72 File Offset: 0x00246172
		public void Dispose()
		{
			if (Interlocked.Exchange(ref this.disposed, 1) != 1)
			{
				this.Release();
			}
		}

		// Token: 0x0600B3B8 RID: 46008 RVA: 0x00247F89 File Offset: 0x00246189
		private void DisposeSplit(string name, TimeSpan duration)
		{
			this.trace.Add(name, duration, false);
			this.Release();
		}

		// Token: 0x0600B3B9 RID: 46009 RVA: 0x00247FA4 File Offset: 0x002461A4
		private void AddRef()
		{
			if (Interlocked.CompareExchange(ref this.disposed, 1, 1) == 1)
			{
				throw new InvalidOperationException("SplitsTrace was disposed");
			}
			Interlocked.Increment(ref this.splitCount);
		}

		// Token: 0x0600B3BA RID: 46010 RVA: 0x00247FCD File Offset: 0x002461CD
		private void Release()
		{
			if (Interlocked.Decrement(ref this.splitCount) == 0)
			{
				this.trace.Dispose();
				this.trace = null;
			}
		}

		// Token: 0x04005B94 RID: 23444
		private const int disposedSentinel = 1;

		// Token: 0x04005B95 RID: 23445
		private IHostTrace trace;

		// Token: 0x04005B96 RID: 23446
		private int splitCount;

		// Token: 0x04005B97 RID: 23447
		private int disposed;

		// Token: 0x02001C21 RID: 7201
		private sealed class SplitTrace : IHostTrace, IDisposable
		{
			// Token: 0x0600B3BB RID: 46011 RVA: 0x00247FEE File Offset: 0x002461EE
			public SplitTrace(SplitsTrace trace, string name, DateTime start)
			{
				this.trace = trace;
				this.name = name;
				this.lastStart = new DateTime?(start);
				this.accumulated = TimeSpan.Zero;
			}

			// Token: 0x17002D03 RID: 11523
			// (get) Token: 0x0600B3BC RID: 46012 RVA: 0x0024801B File Offset: 0x0024621B
			public SourceLevels Levels
			{
				get
				{
					return this.trace.trace.Levels;
				}
			}

			// Token: 0x0600B3BD RID: 46013 RVA: 0x00248030 File Offset: 0x00246230
			public void Suspend()
			{
				if (this.lastStart != null)
				{
					this.accumulated += HiResDateTime.UtcNow - this.lastStart.Value;
					this.lastStart = null;
				}
			}

			// Token: 0x0600B3BE RID: 46014 RVA: 0x0024807C File Offset: 0x0024627C
			public void Resume()
			{
				if (this.lastStart == null)
				{
					this.lastStart = new DateTime?(HiResDateTime.UtcNow);
				}
			}

			// Token: 0x0600B3BF RID: 46015 RVA: 0x0024809B File Offset: 0x0024629B
			public IHostTraceValue Begin(string name, bool isPii)
			{
				return this.trace.trace.Begin(name, isPii);
			}

			// Token: 0x0600B3C0 RID: 46016 RVA: 0x002480AF File Offset: 0x002462AF
			public void Add(string name, object value, bool isPii)
			{
				this.trace.trace.Add(name, value, isPii);
			}

			// Token: 0x0600B3C1 RID: 46017 RVA: 0x002480C4 File Offset: 0x002462C4
			public void Add(Exception e, bool hasPii = true)
			{
				this.trace.trace.Add(e, hasPii);
			}

			// Token: 0x0600B3C2 RID: 46018 RVA: 0x002480D8 File Offset: 0x002462D8
			public void Add(Exception e, TraceEventType type, bool hasPii = true)
			{
				this.trace.trace.Add(e, type, hasPii);
			}

			// Token: 0x0600B3C3 RID: 46019 RVA: 0x002480ED File Offset: 0x002462ED
			public void Dispose()
			{
				if (this.trace != null)
				{
					this.Suspend();
					this.trace.DisposeSplit(this.name, this.accumulated);
					this.trace = null;
				}
			}

			// Token: 0x04005B98 RID: 23448
			private readonly string name;

			// Token: 0x04005B99 RID: 23449
			private SplitsTrace trace;

			// Token: 0x04005B9A RID: 23450
			private DateTime? lastStart;

			// Token: 0x04005B9B RID: 23451
			private TimeSpan accumulated;
		}
	}
}
