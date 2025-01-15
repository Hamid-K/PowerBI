using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x02000034 RID: 52
	internal class EqualityComparers
	{
		// Token: 0x04000139 RID: 313
		internal static readonly IEqualityComparer<ObjectType> ObjectTypeComparerInstance = new EqualityComparers.ObjectTypeEqualityComparer();

		// Token: 0x0400013A RID: 314
		internal static readonly EqualityComparers.Int32EqualityComparer Int32ComparerInstance = new EqualityComparers.Int32EqualityComparer();

		// Token: 0x0400013B RID: 315
		internal static readonly EqualityComparers.ReversedInt32EqualityComparer ReversedInt32ComparerInstance = new EqualityComparers.ReversedInt32EqualityComparer();

		// Token: 0x0400013C RID: 316
		internal static readonly EqualityComparers.Int64EqualityComparer Int64ComparerInstance = new EqualityComparers.Int64EqualityComparer();

		// Token: 0x0400013D RID: 317
		internal static readonly IEqualityComparer<string> StringComparerInstance = new EqualityComparers.StringEqualityComparer();

		// Token: 0x02000114 RID: 276
		private class ObjectTypeEqualityComparer : IEqualityComparer<ObjectType>
		{
			// Token: 0x06000D82 RID: 3458 RVA: 0x0002CDE2 File Offset: 0x0002AFE2
			internal ObjectTypeEqualityComparer()
			{
			}

			// Token: 0x06000D83 RID: 3459 RVA: 0x0002CDEA File Offset: 0x0002AFEA
			public bool Equals(ObjectType x, ObjectType y)
			{
				return x == y;
			}

			// Token: 0x06000D84 RID: 3460 RVA: 0x0002CDF0 File Offset: 0x0002AFF0
			public int GetHashCode(ObjectType obj)
			{
				return (int)obj;
			}
		}

		// Token: 0x02000115 RID: 277
		private class StringEqualityComparer : IEqualityComparer<string>
		{
			// Token: 0x06000D85 RID: 3461 RVA: 0x0002CDF3 File Offset: 0x0002AFF3
			internal StringEqualityComparer()
			{
			}

			// Token: 0x06000D86 RID: 3462 RVA: 0x0002CDFB File Offset: 0x0002AFFB
			public bool Equals(string str1, string str2)
			{
				return string.Equals(str1, str2, StringComparison.Ordinal);
			}

			// Token: 0x06000D87 RID: 3463 RVA: 0x0002CE05 File Offset: 0x0002B005
			public int GetHashCode(string str)
			{
				return str.GetHashCode();
			}
		}

		// Token: 0x02000116 RID: 278
		internal class Int32EqualityComparer : IEqualityComparer<int>, IComparer<int>
		{
			// Token: 0x06000D88 RID: 3464 RVA: 0x0002CE0D File Offset: 0x0002B00D
			internal Int32EqualityComparer()
			{
			}

			// Token: 0x06000D89 RID: 3465 RVA: 0x0002CE15 File Offset: 0x0002B015
			public bool Equals(int x, int y)
			{
				return x == y;
			}

			// Token: 0x06000D8A RID: 3466 RVA: 0x0002CE1B File Offset: 0x0002B01B
			public int GetHashCode(int obj)
			{
				return obj;
			}

			// Token: 0x06000D8B RID: 3467 RVA: 0x0002CE1E File Offset: 0x0002B01E
			public int Compare(int x, int y)
			{
				return x - y;
			}
		}

		// Token: 0x02000117 RID: 279
		internal class ReversedInt32EqualityComparer : IEqualityComparer<int>, IComparer<int>
		{
			// Token: 0x06000D8C RID: 3468 RVA: 0x0002CE23 File Offset: 0x0002B023
			internal ReversedInt32EqualityComparer()
			{
			}

			// Token: 0x06000D8D RID: 3469 RVA: 0x0002CE2B File Offset: 0x0002B02B
			public bool Equals(int x, int y)
			{
				return x == y;
			}

			// Token: 0x06000D8E RID: 3470 RVA: 0x0002CE31 File Offset: 0x0002B031
			public int GetHashCode(int obj)
			{
				return obj;
			}

			// Token: 0x06000D8F RID: 3471 RVA: 0x0002CE34 File Offset: 0x0002B034
			public int Compare(int x, int y)
			{
				return y - x;
			}
		}

		// Token: 0x02000118 RID: 280
		internal class Int64EqualityComparer : IEqualityComparer<long>, IComparer<long>
		{
			// Token: 0x06000D90 RID: 3472 RVA: 0x0002CE39 File Offset: 0x0002B039
			internal Int64EqualityComparer()
			{
			}

			// Token: 0x06000D91 RID: 3473 RVA: 0x0002CE41 File Offset: 0x0002B041
			public bool Equals(long x, long y)
			{
				return x == y;
			}

			// Token: 0x06000D92 RID: 3474 RVA: 0x0002CE47 File Offset: 0x0002B047
			public int GetHashCode(long obj)
			{
				return (int)obj;
			}

			// Token: 0x06000D93 RID: 3475 RVA: 0x0002CE4B File Offset: 0x0002B04B
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
