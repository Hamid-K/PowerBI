using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.DataShaping.Common
{
	// Token: 0x02000013 RID: 19
	internal static class Hashing
	{
		// Token: 0x060000A3 RID: 163 RVA: 0x0000373C File Offset: 0x0000193C
		public static uint CombineHash(uint u1, uint u2)
		{
			return ((u1 << 7) | (u1 >> 25)) ^ u2;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003748 File Offset: 0x00001948
		public static int CombineHash(int n1, int n2)
		{
			return (int)Hashing.CombineHash((uint)n1, (uint)n2);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003751 File Offset: 0x00001951
		internal static int CombineHash(int n1, int n2, int n3)
		{
			return (int)Hashing.CombineHash(Hashing.CombineHash((uint)n1, (uint)n2), (uint)n3);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003760 File Offset: 0x00001960
		internal static int CombineHash(int n1, int n2, int n3, int n4)
		{
			return (int)Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash((uint)n1, (uint)n2), (uint)n3), (uint)n4);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003775 File Offset: 0x00001975
		internal static int CombineHash(int n1, int n2, int n3, int n4, int n5)
		{
			return (int)Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash((uint)n1, (uint)n2), (uint)n3), (uint)n4), (uint)n5);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003791 File Offset: 0x00001991
		internal static int CombineHash(int n1, int n2, int n3, int n4, int n5, int n6)
		{
			return (int)Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash((uint)n1, (uint)n2), (uint)n3), (uint)n4), (uint)n5), (uint)n6);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000037B4 File Offset: 0x000019B4
		internal static int CombineHash(int n1, int n2, int n3, int n4, int n5, int n6, int n7)
		{
			return (int)Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash((uint)n1, (uint)n2), (uint)n3), (uint)n4), (uint)n5), (uint)n6), (uint)n7);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000037DE File Offset: 0x000019DE
		internal static int CombineHash(int n1, int n2, int n3, int n4, int n5, int n6, int n7, int n8)
		{
			return (int)Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash((uint)n1, (uint)n2), (uint)n3), (uint)n4), (uint)n5), (uint)n6), (uint)n7), (uint)n8);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000380F File Offset: 0x00001A0F
		internal static int CombineHash(int n1, int n2, int n3, int n4, int n5, int n6, int n7, int n8, int n9)
		{
			return (int)Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash((uint)n1, (uint)n2), (uint)n3), (uint)n4), (uint)n5), (uint)n6), (uint)n7), (uint)n8), (uint)n9);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003847 File Offset: 0x00001A47
		internal static int CombineHash(int n1, int n2, int n3, int n4, int n5, int n6, int n7, int n8, int n9, int n10)
		{
			return (int)Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash((uint)n1, (uint)n2), (uint)n3), (uint)n4), (uint)n5), (uint)n6), (uint)n7), (uint)n8), (uint)n9), (uint)n10);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003888 File Offset: 0x00001A88
		public static int CombineHashUnordered(IEnumerable<int> hashes)
		{
			if (hashes == null)
			{
				return -48879;
			}
			int num = 0;
			int num2 = 0;
			foreach (int num3 in hashes)
			{
				num |= num3;
				num2++;
			}
			return Hashing.CombineHash(num2, num);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000038E8 File Offset: 0x00001AE8
		public static int CombineHashUnordered<T>(IList<T> hashes)
		{
			if (hashes == null)
			{
				return -48879;
			}
			int num = 0;
			int num2 = 0;
			foreach (T t in hashes)
			{
				num |= Hashing.GetHashCode<T>(t, null);
				num2++;
			}
			return Hashing.CombineHash(num2, num);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0000394C File Offset: 0x00001B4C
		public static int CombineHash<T>(IList<T> values, IEqualityComparer<T> comparer = null)
		{
			if (values == null)
			{
				return -48879;
			}
			int num = values.Count;
			for (int i = 0; i < values.Count; i++)
			{
				int hashCode = Hashing.GetHashCode<T>(values[i], comparer);
				num = ((num << 5) + num) ^ hashCode;
			}
			return num;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003994 File Offset: 0x00001B94
		public static int CombineHashWithComparer<T>(IList<T> values, IEqualityComparer comparer)
		{
			if (values == null)
			{
				return -48879;
			}
			int num = values.Count;
			for (int i = 0; i < values.Count; i++)
			{
				int hashCode = Hashing.GetHashCode<T>(values[i], comparer);
				num = ((num << 5) + num) ^ hashCode;
			}
			return num;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000039DC File Offset: 0x00001BDC
		public static int CombineHash<T>(IList<List<T>> values, IEqualityComparer<T> comparer = null)
		{
			if (values == null)
			{
				return -48879;
			}
			int num = values.Count;
			for (int i = 0; i < values.Count; i++)
			{
				int num2 = Hashing.CombineHash<T>(values[i], comparer);
				num = ((num << 5) + num) ^ num2;
			}
			return num;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003A24 File Offset: 0x00001C24
		public static int CombineHashReadonly<T>(IReadOnlyList<T> values, IEqualityComparer<T> comparer = null)
		{
			if (values == null)
			{
				return -48879;
			}
			int num = values.Count;
			for (int i = 0; i < values.Count; i++)
			{
				int hashCode = Hashing.GetHashCode<T>(values[i], comparer);
				num = ((num << 5) + num) ^ hashCode;
			}
			return num;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003A69 File Offset: 0x00001C69
		public static int GetHashCode<T>(T value, IEqualityComparer<T> comparer = null)
		{
			if (value == null)
			{
				return -48879;
			}
			if (comparer != null)
			{
				return comparer.GetHashCode(value);
			}
			return value.GetHashCode();
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003A91 File Offset: 0x00001C91
		public static int GetHashCode<T>(T value, IEqualityComparer comparer)
		{
			if (value == null)
			{
				return -48879;
			}
			return comparer.GetHashCode(value);
		}

		// Token: 0x04000043 RID: 67
		internal const int NullHashCode = -48879;
	}
}
