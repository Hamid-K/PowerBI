using System;
using NLog.Common;

namespace NLog.Targets.Wrappers
{
	// Token: 0x02000071 RID: 113
	[Target("RandomizeGroup", IsCompound = true)]
	public class RandomizeGroupTarget : CompoundTargetBase
	{
		// Token: 0x0600090E RID: 2318 RVA: 0x000175A8 File Offset: 0x000157A8
		public RandomizeGroupTarget()
			: this(new Target[0])
		{
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x000175B6 File Offset: 0x000157B6
		public RandomizeGroupTarget(string name, params Target[] targets)
			: this(targets)
		{
			base.Name = name;
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x000175C6 File Offset: 0x000157C6
		public RandomizeGroupTarget(params Target[] targets)
			: base(targets)
		{
			base.OptimizeBufferReuse = base.GetType() == typeof(RandomizeGroupTarget);
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x000175F8 File Offset: 0x000157F8
		protected override void Write(AsyncLogEventInfo logEvent)
		{
			if (base.Targets.Count == 0)
			{
				logEvent.Continuation(null);
				return;
			}
			Random random = this._random;
			int num;
			lock (random)
			{
				num = this._random.Next(base.Targets.Count);
			}
			base.Targets[num].WriteAsyncLogEvent(logEvent);
		}

		// Token: 0x04000209 RID: 521
		private readonly Random _random = new Random();
	}
}
