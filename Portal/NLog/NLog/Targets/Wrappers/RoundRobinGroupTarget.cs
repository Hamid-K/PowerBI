using System;
using System.Threading;
using NLog.Common;

namespace NLog.Targets.Wrappers
{
	// Token: 0x02000074 RID: 116
	[Target("RoundRobinGroup", IsCompound = true)]
	public class RoundRobinGroupTarget : CompoundTargetBase
	{
		// Token: 0x06000922 RID: 2338 RVA: 0x000178CA File Offset: 0x00015ACA
		public RoundRobinGroupTarget()
			: this(new Target[0])
		{
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x000178D8 File Offset: 0x00015AD8
		public RoundRobinGroupTarget(string name, params Target[] targets)
			: this(targets)
		{
			base.Name = name;
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x000178E8 File Offset: 0x00015AE8
		public RoundRobinGroupTarget(params Target[] targets)
			: base(targets)
		{
			base.OptimizeBufferReuse = base.GetType() == typeof(RoundRobinGroupTarget);
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x00017913 File Offset: 0x00015B13
		protected override void WriteAsyncThreadSafe(AsyncLogEventInfo logEvent)
		{
			this.Write(logEvent);
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x0001791C File Offset: 0x00015B1C
		protected override void Write(AsyncLogEventInfo logEvent)
		{
			if (base.Targets.Count == 0)
			{
				logEvent.Continuation(null);
				return;
			}
			int num = (int)((ulong)Interlocked.Increment(ref this._currentTarget) % (ulong)((long)base.Targets.Count));
			base.Targets[num].WriteAsyncLogEvent(logEvent);
		}

		// Token: 0x0400020E RID: 526
		private int _currentTarget = -1;
	}
}
