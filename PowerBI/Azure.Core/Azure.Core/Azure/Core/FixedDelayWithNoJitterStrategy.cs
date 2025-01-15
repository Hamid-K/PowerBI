using System;
using System.Runtime.CompilerServices;

namespace Azure.Core
{
	// Token: 0x02000071 RID: 113
	internal class FixedDelayWithNoJitterStrategy : DelayStrategy
	{
		// Token: 0x060003B0 RID: 944 RVA: 0x0000AF80 File Offset: 0x00009180
		public FixedDelayWithNoJitterStrategy(TimeSpan? suggestedDelay = null)
			: base(new TimeSpan?((suggestedDelay != null) ? DelayStrategy.Max(suggestedDelay.Value, FixedDelayWithNoJitterStrategy.DefaultDelay) : FixedDelayWithNoJitterStrategy.DefaultDelay), 0.0)
		{
			this._delay = ((suggestedDelay != null) ? DelayStrategy.Max(suggestedDelay.Value, FixedDelayWithNoJitterStrategy.DefaultDelay) : FixedDelayWithNoJitterStrategy.DefaultDelay);
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000AFE9 File Offset: 0x000091E9
		[NullableContext(2)]
		protected override TimeSpan GetNextDelayCore(Response response, int retryNumber)
		{
			return this._delay;
		}

		// Token: 0x04000185 RID: 389
		private static readonly TimeSpan DefaultDelay = TimeSpan.FromSeconds(1.0);

		// Token: 0x04000186 RID: 390
		private readonly TimeSpan _delay;
	}
}
