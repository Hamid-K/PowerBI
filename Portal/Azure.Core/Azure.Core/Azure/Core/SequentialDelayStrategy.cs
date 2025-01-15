using System;
using System.Runtime.CompilerServices;

namespace Azure.Core
{
	// Token: 0x0200007E RID: 126
	internal class SequentialDelayStrategy : DelayStrategy
	{
		// Token: 0x06000419 RID: 1049 RVA: 0x0000C4DA File Offset: 0x0000A6DA
		public SequentialDelayStrategy()
			: base(new TimeSpan?(SequentialDelayStrategy._maxDelay), 0.0)
		{
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0000C4F8 File Offset: 0x0000A6F8
		[NullableContext(2)]
		protected override TimeSpan GetNextDelayCore(Response response, int retryNumber)
		{
			int num = retryNumber - 1;
			if (num < SequentialDelayStrategy._pollingSequence.Length)
			{
				return SequentialDelayStrategy._pollingSequence[num];
			}
			return SequentialDelayStrategy._maxDelay;
		}

		// Token: 0x040001B6 RID: 438
		[Nullable(1)]
		private static readonly TimeSpan[] _pollingSequence = new TimeSpan[]
		{
			TimeSpan.FromSeconds(1.0),
			TimeSpan.FromSeconds(1.0),
			TimeSpan.FromSeconds(1.0),
			TimeSpan.FromSeconds(2.0),
			TimeSpan.FromSeconds(4.0),
			TimeSpan.FromSeconds(8.0),
			TimeSpan.FromSeconds(16.0),
			TimeSpan.FromSeconds(32.0)
		};

		// Token: 0x040001B7 RID: 439
		private static readonly TimeSpan _maxDelay = SequentialDelayStrategy._pollingSequence[SequentialDelayStrategy._pollingSequence.Length - 1];
	}
}
