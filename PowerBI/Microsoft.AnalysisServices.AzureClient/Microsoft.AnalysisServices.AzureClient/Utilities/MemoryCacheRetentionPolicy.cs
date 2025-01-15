using System;

namespace Microsoft.AnalysisServices.AzureClient.Utilities
{
	// Token: 0x0200002F RID: 47
	internal sealed class MemoryCacheRetentionPolicy
	{
		// Token: 0x0600017D RID: 381 RVA: 0x000076F0 File Offset: 0x000058F0
		private MemoryCacheRetentionPolicy(int kind, double miliseconds, int capacityLimit)
		{
			this.kind = kind;
			this.miliseconds = miliseconds;
			this.capacityLimit = capacityLimit;
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600017E RID: 382 RVA: 0x0000770D File Offset: 0x0000590D
		public bool HasActiveRetention
		{
			get
			{
				return this.kind != 0;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00007718 File Offset: 0x00005918
		public bool HasCapacityLimit
		{
			get
			{
				return this.capacityLimit > 0;
			}
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00007723 File Offset: 0x00005923
		public static MemoryCacheRetentionPolicy GetNoRetentionPolicy()
		{
			if (MemoryCacheRetentionPolicy.noRetention == null)
			{
				MemoryCacheRetentionPolicy.noRetention = new MemoryCacheRetentionPolicy(0, -1.0, 0);
			}
			return MemoryCacheRetentionPolicy.noRetention;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00007746 File Offset: 0x00005946
		public static MemoryCacheRetentionPolicy BuildPolicyWithCapacityLimit(int capacityLimit)
		{
			return new MemoryCacheRetentionPolicy(0, -1.0, capacityLimit);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00007758 File Offset: 0x00005958
		public static MemoryCacheRetentionPolicy BuildAbsoluteExpirationPolicy(TimeSpan interval)
		{
			return new MemoryCacheRetentionPolicy(1, interval.TotalMilliseconds, 0);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00007768 File Offset: 0x00005968
		public static MemoryCacheRetentionPolicy BuildAbsoluteExpirationPolicyWithCapacityLimit(TimeSpan interval, int capacityLimit)
		{
			return new MemoryCacheRetentionPolicy(1, interval.TotalMilliseconds, capacityLimit);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00007778 File Offset: 0x00005978
		public static MemoryCacheRetentionPolicy BuildSlidingWindowExpirationPolicy(TimeSpan interval)
		{
			return new MemoryCacheRetentionPolicy(2, interval.TotalMilliseconds, 0);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00007788 File Offset: 0x00005988
		public static MemoryCacheRetentionPolicy BuildSlidingWindowExpirationPolicyWithCapacityLimit(TimeSpan interval, int capacityLimit)
		{
			return new MemoryCacheRetentionPolicy(2, interval.TotalMilliseconds, capacityLimit);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00007798 File Offset: 0x00005998
		public void GetCacheInsertionLimitation(out DateTime utcExpiration, out int capacityLimit)
		{
			utcExpiration = ((this.kind == 0) ? DateTime.MaxValue : DateTime.UtcNow.AddMilliseconds(this.miliseconds));
			capacityLimit = this.capacityLimit;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000077D8 File Offset: 0x000059D8
		public bool HasItemExpired(ref DateTime utcExpiration, out bool hasExpirationChanged)
		{
			switch (this.kind)
			{
			case 0:
				hasExpirationChanged = false;
				return false;
			case 1:
				hasExpirationChanged = false;
				return utcExpiration < DateTime.UtcNow;
			case 2:
				if (utcExpiration < DateTime.UtcNow)
				{
					hasExpirationChanged = false;
					return true;
				}
				utcExpiration = DateTime.UtcNow.AddMilliseconds(this.miliseconds);
				hasExpirationChanged = true;
				return false;
			default:
				hasExpirationChanged = false;
				return false;
			}
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00007854 File Offset: 0x00005A54
		internal static MemoryCacheRetentionPolicy FromGlobalObject(object @object)
		{
			object[] array = (object[])@object;
			int num = (int)array[0];
			double num2 = (double)array[1];
			int num3 = (int)array[2];
			return new MemoryCacheRetentionPolicy(num, num2, num3);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00007889 File Offset: 0x00005A89
		internal object ToGlobalObject()
		{
			return new object[] { this.kind, this.miliseconds, this.capacityLimit };
		}

		// Token: 0x040000D5 RID: 213
		private static MemoryCacheRetentionPolicy noRetention;

		// Token: 0x040000D6 RID: 214
		private readonly int kind;

		// Token: 0x040000D7 RID: 215
		private readonly double miliseconds;

		// Token: 0x040000D8 RID: 216
		private readonly int capacityLimit;

		// Token: 0x02000073 RID: 115
		private static class PolicyKind
		{
			// Token: 0x04000226 RID: 550
			public const int NoRetention = 0;

			// Token: 0x04000227 RID: 551
			public const int AbsoluteExpiration = 1;

			// Token: 0x04000228 RID: 552
			public const int SlidingWindowExpiration = 2;
		}
	}
}
