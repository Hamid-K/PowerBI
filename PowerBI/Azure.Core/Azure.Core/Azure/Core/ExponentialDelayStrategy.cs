using System;
using System.Runtime.CompilerServices;

namespace Azure.Core
{
	// Token: 0x02000044 RID: 68
	internal class ExponentialDelayStrategy : DelayStrategy
	{
		// Token: 0x060001F2 RID: 498 RVA: 0x000062E4 File Offset: 0x000044E4
		public ExponentialDelayStrategy(TimeSpan? delay = null, TimeSpan? maxDelay = null)
			: base(maxDelay, 0.2)
		{
			this._delay = delay ?? TimeSpan.FromSeconds(0.8);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000632C File Offset: 0x0000452C
		[NullableContext(2)]
		protected override TimeSpan GetNextDelayCore(Response response, int retryNumber)
		{
			return TimeSpan.FromMilliseconds((double)(1 << retryNumber - 1) * this._delay.TotalMilliseconds);
		}

		// Token: 0x040000E3 RID: 227
		private readonly TimeSpan _delay;
	}
}
