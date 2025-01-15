using System;

namespace Microsoft.AnalysisServices.Utilities
{
	// Token: 0x02000140 RID: 320
	internal sealed class MemoryCacheRetentionPolicy
	{
		// Token: 0x0600110B RID: 4363 RVA: 0x0003B5A8 File Offset: 0x000397A8
		private MemoryCacheRetentionPolicy(int kind, double miliseconds, int capacityLimit)
		{
			this.kind = kind;
			this.miliseconds = miliseconds;
			this.capacityLimit = capacityLimit;
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x0600110C RID: 4364 RVA: 0x0003B5C5 File Offset: 0x000397C5
		public bool HasActiveRetention
		{
			get
			{
				return this.kind != 0;
			}
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x0600110D RID: 4365 RVA: 0x0003B5D0 File Offset: 0x000397D0
		public bool HasCapacityLimit
		{
			get
			{
				return this.capacityLimit > 0;
			}
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x0003B5DB File Offset: 0x000397DB
		public static MemoryCacheRetentionPolicy GetNoRetentionPolicy()
		{
			if (MemoryCacheRetentionPolicy.noRetention == null)
			{
				MemoryCacheRetentionPolicy.noRetention = new MemoryCacheRetentionPolicy(0, -1.0, 0);
			}
			return MemoryCacheRetentionPolicy.noRetention;
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x0003B5FE File Offset: 0x000397FE
		public static MemoryCacheRetentionPolicy BuildPolicyWithCapacityLimit(int capacityLimit)
		{
			return new MemoryCacheRetentionPolicy(0, -1.0, capacityLimit);
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x0003B610 File Offset: 0x00039810
		public static MemoryCacheRetentionPolicy BuildAbsoluteExpirationPolicy(TimeSpan interval)
		{
			return new MemoryCacheRetentionPolicy(1, interval.TotalMilliseconds, 0);
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x0003B620 File Offset: 0x00039820
		public static MemoryCacheRetentionPolicy BuildAbsoluteExpirationPolicyWithCapacityLimit(TimeSpan interval, int capacityLimit)
		{
			return new MemoryCacheRetentionPolicy(1, interval.TotalMilliseconds, capacityLimit);
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x0003B630 File Offset: 0x00039830
		public static MemoryCacheRetentionPolicy BuildSlidingWindowExpirationPolicy(TimeSpan interval)
		{
			return new MemoryCacheRetentionPolicy(2, interval.TotalMilliseconds, 0);
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x0003B640 File Offset: 0x00039840
		public static MemoryCacheRetentionPolicy BuildSlidingWindowExpirationPolicyWithCapacityLimit(TimeSpan interval, int capacityLimit)
		{
			return new MemoryCacheRetentionPolicy(2, interval.TotalMilliseconds, capacityLimit);
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x0003B650 File Offset: 0x00039850
		public void GetCacheInsertionLimitation(out DateTime utcExpiration, out int capacityLimit)
		{
			utcExpiration = ((this.kind == 0) ? DateTime.MaxValue : DateTime.UtcNow.AddMilliseconds(this.miliseconds));
			capacityLimit = this.capacityLimit;
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x0003B690 File Offset: 0x00039890
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

		// Token: 0x06001116 RID: 4374 RVA: 0x0003B70C File Offset: 0x0003990C
		internal static MemoryCacheRetentionPolicy FromGlobalObject(object @object)
		{
			object[] array = (object[])@object;
			int num = (int)array[0];
			double num2 = (double)array[1];
			int num3 = (int)array[2];
			return new MemoryCacheRetentionPolicy(num, num2, num3);
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x0003B741 File Offset: 0x00039941
		internal object ToGlobalObject()
		{
			return new object[] { this.kind, this.miliseconds, this.capacityLimit };
		}

		// Token: 0x04000AD0 RID: 2768
		private static MemoryCacheRetentionPolicy noRetention;

		// Token: 0x04000AD1 RID: 2769
		private readonly int kind;

		// Token: 0x04000AD2 RID: 2770
		private readonly double miliseconds;

		// Token: 0x04000AD3 RID: 2771
		private readonly int capacityLimit;

		// Token: 0x020001DE RID: 478
		private static class PolicyKind
		{
			// Token: 0x040011B4 RID: 4532
			public const int NoRetention = 0;

			// Token: 0x040011B5 RID: 4533
			public const int AbsoluteExpiration = 1;

			// Token: 0x040011B6 RID: 4534
			public const int SlidingWindowExpiration = 2;
		}
	}
}
