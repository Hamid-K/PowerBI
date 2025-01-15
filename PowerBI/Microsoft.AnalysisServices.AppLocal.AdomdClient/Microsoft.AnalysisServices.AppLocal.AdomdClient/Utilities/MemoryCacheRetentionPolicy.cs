using System;

namespace Microsoft.AnalysisServices.AdomdClient.Utilities
{
	// Token: 0x0200014B RID: 331
	internal sealed class MemoryCacheRetentionPolicy
	{
		// Token: 0x0600107D RID: 4221 RVA: 0x00038CA4 File Offset: 0x00036EA4
		private MemoryCacheRetentionPolicy(int kind, double miliseconds, int capacityLimit)
		{
			this.kind = kind;
			this.miliseconds = miliseconds;
			this.capacityLimit = capacityLimit;
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x0600107E RID: 4222 RVA: 0x00038CC1 File Offset: 0x00036EC1
		public bool HasActiveRetention
		{
			get
			{
				return this.kind != 0;
			}
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x0600107F RID: 4223 RVA: 0x00038CCC File Offset: 0x00036ECC
		public bool HasCapacityLimit
		{
			get
			{
				return this.capacityLimit > 0;
			}
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x00038CD7 File Offset: 0x00036ED7
		public static MemoryCacheRetentionPolicy GetNoRetentionPolicy()
		{
			if (MemoryCacheRetentionPolicy.noRetention == null)
			{
				MemoryCacheRetentionPolicy.noRetention = new MemoryCacheRetentionPolicy(0, -1.0, 0);
			}
			return MemoryCacheRetentionPolicy.noRetention;
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x00038CFA File Offset: 0x00036EFA
		public static MemoryCacheRetentionPolicy BuildPolicyWithCapacityLimit(int capacityLimit)
		{
			return new MemoryCacheRetentionPolicy(0, -1.0, capacityLimit);
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x00038D0C File Offset: 0x00036F0C
		public static MemoryCacheRetentionPolicy BuildAbsoluteExpirationPolicy(TimeSpan interval)
		{
			return new MemoryCacheRetentionPolicy(1, interval.TotalMilliseconds, 0);
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x00038D1C File Offset: 0x00036F1C
		public static MemoryCacheRetentionPolicy BuildAbsoluteExpirationPolicyWithCapacityLimit(TimeSpan interval, int capacityLimit)
		{
			return new MemoryCacheRetentionPolicy(1, interval.TotalMilliseconds, capacityLimit);
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x00038D2C File Offset: 0x00036F2C
		public static MemoryCacheRetentionPolicy BuildSlidingWindowExpirationPolicy(TimeSpan interval)
		{
			return new MemoryCacheRetentionPolicy(2, interval.TotalMilliseconds, 0);
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x00038D3C File Offset: 0x00036F3C
		public static MemoryCacheRetentionPolicy BuildSlidingWindowExpirationPolicyWithCapacityLimit(TimeSpan interval, int capacityLimit)
		{
			return new MemoryCacheRetentionPolicy(2, interval.TotalMilliseconds, capacityLimit);
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x00038D4C File Offset: 0x00036F4C
		public void GetCacheInsertionLimitation(out DateTime utcExpiration, out int capacityLimit)
		{
			utcExpiration = ((this.kind == 0) ? DateTime.MaxValue : DateTime.UtcNow.AddMilliseconds(this.miliseconds));
			capacityLimit = this.capacityLimit;
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x00038D8C File Offset: 0x00036F8C
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

		// Token: 0x06001088 RID: 4232 RVA: 0x00038E08 File Offset: 0x00037008
		internal static MemoryCacheRetentionPolicy FromGlobalObject(object @object)
		{
			object[] array = (object[])@object;
			int num = (int)array[0];
			double num2 = (double)array[1];
			int num3 = (int)array[2];
			return new MemoryCacheRetentionPolicy(num, num2, num3);
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x00038E3D File Offset: 0x0003703D
		internal object ToGlobalObject()
		{
			return new object[] { this.kind, this.miliseconds, this.capacityLimit };
		}

		// Token: 0x04000B17 RID: 2839
		private static MemoryCacheRetentionPolicy noRetention;

		// Token: 0x04000B18 RID: 2840
		private readonly int kind;

		// Token: 0x04000B19 RID: 2841
		private readonly double miliseconds;

		// Token: 0x04000B1A RID: 2842
		private readonly int capacityLimit;

		// Token: 0x02000201 RID: 513
		private static class PolicyKind
		{
			// Token: 0x04000EFE RID: 3838
			public const int NoRetention = 0;

			// Token: 0x04000EFF RID: 3839
			public const int AbsoluteExpiration = 1;

			// Token: 0x04000F00 RID: 3840
			public const int SlidingWindowExpiration = 2;
		}
	}
}
