using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x02000065 RID: 101
	internal sealed class RangeEqualityComparer : IEqualityComparer<IRange>
	{
		// Token: 0x060003C7 RID: 967 RVA: 0x0000A0EF File Offset: 0x000082EF
		private RangeEqualityComparer()
		{
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000A0F8 File Offset: 0x000082F8
		public bool Equals(IRange x, IRange y)
		{
			bool? flag = Util.AreEqual<IRange>(x, y);
			if (flag != null)
			{
				return flag.Value;
			}
			return x.FirstIndex == y.FirstIndex && x.LastIndex == y.LastIndex;
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000A13C File Offset: 0x0000833C
		public int GetHashCode(IRange obj)
		{
			return Hashing.CombineHash(obj.FirstIndex, obj.LastIndex);
		}

		// Token: 0x040000CF RID: 207
		internal static readonly RangeEqualityComparer Instance = new RangeEqualityComparer();
	}
}
