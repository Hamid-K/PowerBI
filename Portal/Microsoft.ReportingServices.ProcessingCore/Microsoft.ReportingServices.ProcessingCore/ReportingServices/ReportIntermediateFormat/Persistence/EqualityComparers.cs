using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x02000552 RID: 1362
	internal class EqualityComparers
	{
		// Token: 0x040020B9 RID: 8377
		internal static readonly IEqualityComparer<ObjectType> ObjectTypeComparerInstance = new EqualityComparers.ObjectTypeEqualityComparer();

		// Token: 0x040020BA RID: 8378
		internal static readonly EqualityComparers.Int32EqualityComparer Int32ComparerInstance = new EqualityComparers.Int32EqualityComparer();

		// Token: 0x040020BB RID: 8379
		internal static readonly EqualityComparers.ReversedInt32EqualityComparer ReversedInt32ComparerInstance = new EqualityComparers.ReversedInt32EqualityComparer();

		// Token: 0x040020BC RID: 8380
		internal static readonly EqualityComparers.Int64EqualityComparer Int64ComparerInstance = new EqualityComparers.Int64EqualityComparer();

		// Token: 0x040020BD RID: 8381
		internal static readonly IEqualityComparer<string> StringComparerInstance = new EqualityComparers.StringEqualityComparer();

		// Token: 0x02000999 RID: 2457
		private class ObjectTypeEqualityComparer : IEqualityComparer<ObjectType>
		{
			// Token: 0x0600810C RID: 33036 RVA: 0x0021387C File Offset: 0x00211A7C
			internal ObjectTypeEqualityComparer()
			{
			}

			// Token: 0x0600810D RID: 33037 RVA: 0x00213884 File Offset: 0x00211A84
			public bool Equals(ObjectType x, ObjectType y)
			{
				return x == y;
			}

			// Token: 0x0600810E RID: 33038 RVA: 0x0021388A File Offset: 0x00211A8A
			public int GetHashCode(ObjectType obj)
			{
				return (int)obj;
			}
		}

		// Token: 0x0200099A RID: 2458
		private class StringEqualityComparer : IEqualityComparer<string>
		{
			// Token: 0x0600810F RID: 33039 RVA: 0x0021388D File Offset: 0x00211A8D
			internal StringEqualityComparer()
			{
			}

			// Token: 0x06008110 RID: 33040 RVA: 0x00213895 File Offset: 0x00211A95
			public bool Equals(string str1, string str2)
			{
				return string.Equals(str1, str2, StringComparison.Ordinal);
			}

			// Token: 0x06008111 RID: 33041 RVA: 0x0021389F File Offset: 0x00211A9F
			public int GetHashCode(string str)
			{
				return str.GetHashCode();
			}
		}

		// Token: 0x0200099B RID: 2459
		internal class Int32EqualityComparer : IEqualityComparer<int>, IComparer<int>
		{
			// Token: 0x06008112 RID: 33042 RVA: 0x002138A7 File Offset: 0x00211AA7
			internal Int32EqualityComparer()
			{
			}

			// Token: 0x06008113 RID: 33043 RVA: 0x002138AF File Offset: 0x00211AAF
			public bool Equals(int x, int y)
			{
				return x == y;
			}

			// Token: 0x06008114 RID: 33044 RVA: 0x002138B5 File Offset: 0x00211AB5
			public int GetHashCode(int obj)
			{
				return obj;
			}

			// Token: 0x06008115 RID: 33045 RVA: 0x002138B8 File Offset: 0x00211AB8
			public int Compare(int x, int y)
			{
				return x - y;
			}
		}

		// Token: 0x0200099C RID: 2460
		internal class ReversedInt32EqualityComparer : IEqualityComparer<int>, IComparer<int>
		{
			// Token: 0x06008116 RID: 33046 RVA: 0x002138BD File Offset: 0x00211ABD
			internal ReversedInt32EqualityComparer()
			{
			}

			// Token: 0x06008117 RID: 33047 RVA: 0x002138C5 File Offset: 0x00211AC5
			public bool Equals(int x, int y)
			{
				return x == y;
			}

			// Token: 0x06008118 RID: 33048 RVA: 0x002138CB File Offset: 0x00211ACB
			public int GetHashCode(int obj)
			{
				return obj;
			}

			// Token: 0x06008119 RID: 33049 RVA: 0x002138CE File Offset: 0x00211ACE
			public int Compare(int x, int y)
			{
				return y - x;
			}
		}

		// Token: 0x0200099D RID: 2461
		internal class Int64EqualityComparer : IEqualityComparer<long>, IComparer<long>
		{
			// Token: 0x0600811A RID: 33050 RVA: 0x002138D3 File Offset: 0x00211AD3
			internal Int64EqualityComparer()
			{
			}

			// Token: 0x0600811B RID: 33051 RVA: 0x002138DB File Offset: 0x00211ADB
			public bool Equals(long x, long y)
			{
				return x == y;
			}

			// Token: 0x0600811C RID: 33052 RVA: 0x002138E1 File Offset: 0x00211AE1
			public int GetHashCode(long obj)
			{
				return (int)obj;
			}

			// Token: 0x0600811D RID: 33053 RVA: 0x002138E5 File Offset: 0x00211AE5
			public int Compare(long x, long y)
			{
				if (x < y)
				{
					return -1;
				}
				if (x > y)
				{
					return 1;
				}
				return 0;
			}
		}
	}
}
