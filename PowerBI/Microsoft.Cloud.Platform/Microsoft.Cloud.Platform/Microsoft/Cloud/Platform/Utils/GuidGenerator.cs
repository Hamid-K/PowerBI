using System;
using System.Threading;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000221 RID: 545
	public static class GuidGenerator
	{
		// Token: 0x06000E5A RID: 3674 RVA: 0x000328CA File Offset: 0x00030ACA
		public static Guid NewGuid(short arg1, short arg2)
		{
			return GuidGenerator.NewGuid(arg1, arg2, GuidGenerator.sm_policy);
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x000328D8 File Offset: 0x00030AD8
		public static Guid NewGuid(short arg1, short arg2, GuidGeneratorPolicy effectivePolicy)
		{
			if ((effectivePolicy & GuidGeneratorPolicy.Faking) == GuidGeneratorPolicy.None)
			{
				return Guid.NewGuid();
			}
			int num = Interlocked.Increment(ref GuidGenerator.sm_counter);
			if (num < 0 && (effectivePolicy & GuidGeneratorPolicy.AllowOverflow) == GuidGeneratorPolicy.None)
			{
				throw new OverflowException();
			}
			byte[] array = (((effectivePolicy & GuidGeneratorPolicy.NoRandomBits) != GuidGeneratorPolicy.None) ? GuidGenerator.sm_notRandom : GuidGenerator.sm_random);
			return new Guid(num, arg1, arg2, array);
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x00032928 File Offset: 0x00030B28
		public static void SetLastFakeGuid(Guid guid)
		{
			byte[] array = guid.ToByteArray();
			int num = BitConverter.ToInt32(array, 0);
			byte[] array2 = new byte[8];
			bool flag = true;
			for (int i = 0; i < 8; i++)
			{
				array2[i] = array[8 + i];
				if (array2[i] != 0)
				{
					flag = false;
				}
			}
			object obj = GuidGenerator.sm_locker;
			lock (obj)
			{
				GuidGenerator.sm_counter = num;
				if (!flag)
				{
					GuidGenerator.sm_random = array2;
				}
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000E5D RID: 3677 RVA: 0x000329B0 File Offset: 0x00030BB0
		// (set) Token: 0x06000E5E RID: 3678 RVA: 0x000329B7 File Offset: 0x00030BB7
		public static GuidGeneratorPolicy Policy
		{
			get
			{
				return GuidGenerator.sm_policy;
			}
			set
			{
				GuidGenerator.sm_policy = value;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000E5F RID: 3679 RVA: 0x000329BF File Offset: 0x00030BBF
		// (set) Token: 0x06000E60 RID: 3680 RVA: 0x000329C7 File Offset: 0x00030BC7
		public static bool Faking
		{
			get
			{
				return GuidGenerator.GetPolicyFlag(GuidGeneratorPolicy.Faking);
			}
			set
			{
				GuidGenerator.SetPolicyFlag(GuidGeneratorPolicy.Faking, value);
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000E61 RID: 3681 RVA: 0x000329D0 File Offset: 0x00030BD0
		// (set) Token: 0x06000E62 RID: 3682 RVA: 0x000329D8 File Offset: 0x00030BD8
		public static bool AllowOverflow
		{
			get
			{
				return GuidGenerator.GetPolicyFlag(GuidGeneratorPolicy.AllowOverflow);
			}
			set
			{
				GuidGenerator.SetPolicyFlag(GuidGeneratorPolicy.AllowOverflow, value);
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000E63 RID: 3683 RVA: 0x000329E1 File Offset: 0x00030BE1
		// (set) Token: 0x06000E64 RID: 3684 RVA: 0x000329E9 File Offset: 0x00030BE9
		public static bool NoRandomBits
		{
			get
			{
				return GuidGenerator.GetPolicyFlag(GuidGeneratorPolicy.NoRandomBits);
			}
			set
			{
				GuidGenerator.SetPolicyFlag(GuidGeneratorPolicy.NoRandomBits, value);
			}
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x000329F4 File Offset: 0x00030BF4
		private static byte[] GenerateRandom()
		{
			Guid guid = Guid.NewGuid();
			byte[] array = new byte[8];
			Array.Copy(guid.ToByteArray(), array, 8);
			return array;
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x00032A1D File Offset: 0x00030C1D
		private static bool GetPolicyFlag(GuidGeneratorPolicy policyFlag)
		{
			return (GuidGenerator.sm_policy & policyFlag) > GuidGeneratorPolicy.None;
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x00032A2C File Offset: 0x00030C2C
		private static void SetPolicyFlag(GuidGeneratorPolicy policyFlag, bool value)
		{
			object obj = GuidGenerator.sm_locker;
			lock (obj)
			{
				GuidGenerator.sm_policy = (value ? (GuidGenerator.sm_policy | policyFlag) : (GuidGenerator.sm_policy & ~policyFlag));
			}
		}

		// Token: 0x04000595 RID: 1429
		private static object sm_locker = new object();

		// Token: 0x04000596 RID: 1430
		private static GuidGeneratorPolicy sm_policy = GuidGeneratorPolicy.None;

		// Token: 0x04000597 RID: 1431
		private static int sm_counter = 0;

		// Token: 0x04000598 RID: 1432
		private const int c_randomBytes = 8;

		// Token: 0x04000599 RID: 1433
		private static byte[] sm_random = GuidGenerator.GenerateRandom();

		// Token: 0x0400059A RID: 1434
		private static readonly byte[] sm_notRandom = new byte[8];
	}
}
