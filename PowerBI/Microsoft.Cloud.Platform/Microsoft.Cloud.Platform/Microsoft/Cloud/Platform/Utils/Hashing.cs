using System;
using System.Collections.Generic;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001A5 RID: 421
	public static class Hashing
	{
		// Token: 0x06000AC3 RID: 2755 RVA: 0x000258A8 File Offset: 0x00023AA8
		public static long ComputeKnuthHashForString(string valueToHash)
		{
			if (valueToHash == null)
			{
				throw new ArgumentNullException("valueToHash");
			}
			ulong num = 3074457345618258791UL;
			for (int i = 0; i < valueToHash.Length; i++)
			{
				num += (ulong)valueToHash[i];
				num *= 3074457345618258799UL;
			}
			return (long)num;
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x000258F8 File Offset: 0x00023AF8
		public static long ComputeKnuthHashForByteArray(byte[] valueToHash)
		{
			if (valueToHash == null)
			{
				throw new ArgumentNullException("valueToHash");
			}
			ulong num = 3074457345618258791UL;
			for (int i = 0; i < valueToHash.Length; i++)
			{
				num += (ulong)valueToHash[i];
				num *= 3074457345618258799UL;
			}
			return (long)num;
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x00025940 File Offset: 0x00023B40
		public static int CombineHash<T>(IList<T> values, IEqualityComparer<T> comparer = null)
		{
			if (values == null)
			{
				return -48879;
			}
			int num = values.Count;
			comparer = comparer ?? EqualityComparer<T>.Default;
			for (int i = 0; i < values.Count; i++)
			{
				T t = values[i];
				int num2 = 0;
				if (t != null)
				{
					num2 = comparer.GetHashCode(t);
				}
				num = ((num << 5) + num) ^ num2;
			}
			return num;
		}

		// Token: 0x04000447 RID: 1095
		private const ulong c_initialHashValue = 3074457345618258791UL;

		// Token: 0x04000448 RID: 1096
		private const ulong c_hashValueMultiplier = 3074457345618258799UL;

		// Token: 0x04000449 RID: 1097
		private const int NullHashCode = -48879;
	}
}
