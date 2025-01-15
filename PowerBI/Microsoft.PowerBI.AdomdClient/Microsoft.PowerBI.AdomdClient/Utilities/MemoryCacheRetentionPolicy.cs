using System;

namespace Microsoft.AnalysisServices.AdomdClient.Utilities
{
	// Token: 0x0200014B RID: 331
	internal sealed class MemoryCacheRetentionPolicy
	{
		// Token: 0x06001070 RID: 4208 RVA: 0x00038974 File Offset: 0x00036B74
		private MemoryCacheRetentionPolicy(int kind, double miliseconds, int capacityLimit)
		{
			this.kind = kind;
			this.miliseconds = miliseconds;
			this.capacityLimit = capacityLimit;
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06001071 RID: 4209 RVA: 0x00038991 File Offset: 0x00036B91
		public bool HasActiveRetention
		{
			get
			{
				return this.kind != 0;
			}
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06001072 RID: 4210 RVA: 0x0003899C File Offset: 0x00036B9C
		public bool HasCapacityLimit
		{
			get
			{
				return this.capacityLimit > 0;
			}
		}

		// Token: 0x06001073 RID: 4211 RVA: 0x000389A7 File Offset: 0x00036BA7
		public static MemoryCacheRetentionPolicy GetNoRetentionPolicy()
		{
			if (MemoryCacheRetentionPolicy.noRetention == null)
			{
				MemoryCacheRetentionPolicy.noRetention = new MemoryCacheRetentionPolicy(0, -1.0, 0);
			}
			return MemoryCacheRetentionPolicy.noRetention;
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x000389CA File Offset: 0x00036BCA
		public static MemoryCacheRetentionPolicy BuildPolicyWithCapacityLimit(int capacityLimit)
		{
			return new MemoryCacheRetentionPolicy(0, -1.0, capacityLimit);
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x000389DC File Offset: 0x00036BDC
		public static MemoryCacheRetentionPolicy BuildAbsoluteExpirationPolicy(TimeSpan interval)
		{
			return new MemoryCacheRetentionPolicy(1, interval.TotalMilliseconds, 0);
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x000389EC File Offset: 0x00036BEC
		public static MemoryCacheRetentionPolicy BuildAbsoluteExpirationPolicyWithCapacityLimit(TimeSpan interval, int capacityLimit)
		{
			return new MemoryCacheRetentionPolicy(1, interval.TotalMilliseconds, capacityLimit);
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x000389FC File Offset: 0x00036BFC
		public static MemoryCacheRetentionPolicy BuildSlidingWindowExpirationPolicy(TimeSpan interval)
		{
			return new MemoryCacheRetentionPolicy(2, interval.TotalMilliseconds, 0);
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x00038A0C File Offset: 0x00036C0C
		public static MemoryCacheRetentionPolicy BuildSlidingWindowExpirationPolicyWithCapacityLimit(TimeSpan interval, int capacityLimit)
		{
			return new MemoryCacheRetentionPolicy(2, interval.TotalMilliseconds, capacityLimit);
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x00038A1C File Offset: 0x00036C1C
		public void GetCacheInsertionLimitation(out DateTime utcExpiration, out int capacityLimit)
		{
			utcExpiration = ((this.kind == 0) ? DateTime.MaxValue : DateTime.UtcNow.AddMilliseconds(this.miliseconds));
			capacityLimit = this.capacityLimit;
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x00038A5C File Offset: 0x00036C5C
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

		// Token: 0x0600107B RID: 4219 RVA: 0x00038AD8 File Offset: 0x00036CD8
		internal static MemoryCacheRetentionPolicy FromGlobalObject(object @object)
		{
			object[] array = (object[])@object;
			int num = (int)array[0];
			double num2 = (double)array[1];
			int num3 = (int)array[2];
			return new MemoryCacheRetentionPolicy(num, num2, num3);
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x00038B0D File Offset: 0x00036D0D
		internal object ToGlobalObject()
		{
			return new object[] { this.kind, this.miliseconds, this.capacityLimit };
		}

		// Token: 0x04000B0A RID: 2826
		private static MemoryCacheRetentionPolicy noRetention;

		// Token: 0x04000B0B RID: 2827
		private readonly int kind;

		// Token: 0x04000B0C RID: 2828
		private readonly double miliseconds;

		// Token: 0x04000B0D RID: 2829
		private readonly int capacityLimit;

		// Token: 0x02000201 RID: 513
		private static class PolicyKind
		{
			// Token: 0x04000EE8 RID: 3816
			public const int NoRetention = 0;

			// Token: 0x04000EE9 RID: 3817
			public const int AbsoluteExpiration = 1;

			// Token: 0x04000EEA RID: 3818
			public const int SlidingWindowExpiration = 2;
		}
	}
}
