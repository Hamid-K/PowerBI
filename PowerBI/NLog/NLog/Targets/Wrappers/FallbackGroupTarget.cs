using System;
using System.Threading;
using NLog.Common;

namespace NLog.Targets.Wrappers
{
	// Token: 0x02000068 RID: 104
	[Target("FallbackGroup", IsCompound = true)]
	public class FallbackGroupTarget : CompoundTargetBase
	{
		// Token: 0x060008BA RID: 2234 RVA: 0x00016BA0 File Offset: 0x00014DA0
		public FallbackGroupTarget()
			: this(new Target[0])
		{
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x00016BAE File Offset: 0x00014DAE
		public FallbackGroupTarget(string name, params Target[] targets)
			: this(targets)
		{
			base.Name = name;
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x00016BBE File Offset: 0x00014DBE
		public FallbackGroupTarget(params Target[] targets)
			: base(targets)
		{
			base.OptimizeBufferReuse = base.GetType() == typeof(FallbackGroupTarget);
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060008BD RID: 2237 RVA: 0x00016BE2 File Offset: 0x00014DE2
		// (set) Token: 0x060008BE RID: 2238 RVA: 0x00016BEA File Offset: 0x00014DEA
		public bool ReturnToFirstOnSuccess { get; set; }

		// Token: 0x060008BF RID: 2239 RVA: 0x00016BF3 File Offset: 0x00014DF3
		protected override void WriteAsyncThreadSafe(AsyncLogEventInfo logEvent)
		{
			this.Write(logEvent);
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x00016BFC File Offset: 0x00014DFC
		protected override void Write(AsyncLogEventInfo logEvent)
		{
			AsyncContinuation continuation = null;
			int tryCounter = 0;
			int targetToInvoke = 0;
			continuation = delegate(Exception ex)
			{
				if (ex == null)
				{
					if (this.ReturnToFirstOnSuccess && Interlocked.Read(ref this._currentTarget) != 0L)
					{
						InternalLogger.Debug<string, Target>("FallbackGroup(Name={0}): Target '{1}' succeeded. Returning to the first one.", this.Name, this.Targets[targetToInvoke]);
						Interlocked.Exchange(ref this._currentTarget, 0L);
					}
					logEvent.Continuation(null);
					return;
				}
				InternalLogger.Warn(ex, "FallbackGroup(Name={0}): Target '{1}' failed. Proceeding to the next one.", new object[]
				{
					this.Name,
					this.Targets[targetToInvoke]
				});
				int tryCounter2 = tryCounter;
				tryCounter = tryCounter2 + 1;
				int num = (targetToInvoke + 1) % this.Targets.Count;
				Interlocked.CompareExchange(ref this._currentTarget, (long)num, (long)targetToInvoke);
				if (tryCounter >= this.Targets.Count)
				{
					targetToInvoke = -1;
				}
				else
				{
					targetToInvoke = num;
				}
				if (targetToInvoke >= 0)
				{
					this.Targets[targetToInvoke].WriteAsyncLogEvent(logEvent.LogEvent.WithContinuation(continuation));
					return;
				}
				logEvent.Continuation(ex);
			};
			targetToInvoke = (int)Interlocked.Read(ref this._currentTarget);
			for (int i = 0; i < base.Targets.Count; i++)
			{
				if (i != targetToInvoke)
				{
					base.Targets[i].PrecalculateVolatileLayouts(logEvent.LogEvent);
				}
			}
			base.Targets[targetToInvoke].WriteAsyncLogEvent(logEvent.LogEvent.WithContinuation(continuation));
		}

		// Token: 0x040001EF RID: 495
		private long _currentTarget;
	}
}
