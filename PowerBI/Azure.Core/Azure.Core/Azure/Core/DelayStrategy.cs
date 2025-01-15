using System;
using System.Runtime.CompilerServices;
using Azure.Core.Pipeline;

namespace Azure.Core
{
	// Token: 0x02000041 RID: 65
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class DelayStrategy
	{
		// Token: 0x060001D3 RID: 467 RVA: 0x00005D68 File Offset: 0x00003F68
		protected DelayStrategy(TimeSpan? maxDelay = null, double jitterFactor = 0.2)
		{
			this._minJitterFactor = 1.0 - jitterFactor;
			this._maxJitterFactor = 1.0 + jitterFactor;
			this._maxDelay = maxDelay ?? TimeSpan.FromMinutes(1.0);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00005DD0 File Offset: 0x00003FD0
		public static DelayStrategy CreateExponentialDelayStrategy(TimeSpan? initialDelay = null, TimeSpan? maxDelay = null)
		{
			return new ExponentialDelayStrategy(new TimeSpan?(initialDelay ?? TimeSpan.FromSeconds(0.8)), new TimeSpan?(maxDelay ?? TimeSpan.FromMinutes(1.0)));
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00005E30 File Offset: 0x00004030
		public static DelayStrategy CreateFixedDelayStrategy(TimeSpan? delay = null)
		{
			return new FixedDelayStrategy(delay ?? TimeSpan.FromSeconds(0.8));
		}

		// Token: 0x060001D6 RID: 470
		[NullableContext(2)]
		protected abstract TimeSpan GetNextDelayCore(Response response, int retryNumber);

		// Token: 0x060001D7 RID: 471 RVA: 0x00005E64 File Offset: 0x00004064
		[NullableContext(2)]
		public TimeSpan GetNextDelay(Response response, int retryNumber)
		{
			return DelayStrategy.Max(((response != null) ? response.Headers.RetryAfter : null) ?? TimeSpan.Zero, DelayStrategy.Min(this.ApplyJitter(this.GetNextDelayCore(response, retryNumber)), this._maxDelay));
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00005EC4 File Offset: 0x000040C4
		private TimeSpan ApplyJitter(TimeSpan delay)
		{
			double num = this._random.NextDouble();
			num = num * (this._maxJitterFactor - this._minJitterFactor) + this._minJitterFactor;
			return TimeSpan.FromMilliseconds(delay.TotalMilliseconds * num);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00005F02 File Offset: 0x00004102
		protected static TimeSpan Max(TimeSpan val1, TimeSpan val2)
		{
			if (!(val1 > val2))
			{
				return val2;
			}
			return val1;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00005F10 File Offset: 0x00004110
		protected static TimeSpan Min(TimeSpan val1, TimeSpan val2)
		{
			if (!(val1 < val2))
			{
				return val2;
			}
			return val1;
		}

		// Token: 0x040000D5 RID: 213
		private readonly Random _random = new ThreadSafeRandom();

		// Token: 0x040000D6 RID: 214
		private readonly double _minJitterFactor;

		// Token: 0x040000D7 RID: 215
		private readonly double _maxJitterFactor;

		// Token: 0x040000D8 RID: 216
		private readonly TimeSpan _maxDelay;

		// Token: 0x040000D9 RID: 217
		internal const double DefaultJitterFactor = 0.2;
	}
}
