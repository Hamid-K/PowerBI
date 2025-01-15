using System;
using System.Runtime.CompilerServices;

namespace Azure.Core
{
	// Token: 0x02000045 RID: 69
	internal class FixedDelayStrategy : DelayStrategy
	{
		// Token: 0x060001F4 RID: 500 RVA: 0x00006356 File Offset: 0x00004556
		public FixedDelayStrategy(TimeSpan delay)
			: base(new TimeSpan?(TimeSpan.FromMilliseconds(delay.TotalMilliseconds)), 0.2)
		{
			this._delay = delay;
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000637F File Offset: 0x0000457F
		[NullableContext(2)]
		protected override TimeSpan GetNextDelayCore(Response response, int retryNumber)
		{
			return this._delay;
		}

		// Token: 0x040000E4 RID: 228
		private readonly TimeSpan _delay;
	}
}
