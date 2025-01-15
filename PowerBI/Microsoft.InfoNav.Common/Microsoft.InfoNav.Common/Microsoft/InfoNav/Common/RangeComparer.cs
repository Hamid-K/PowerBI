using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x02000064 RID: 100
	internal sealed class RangeComparer : IComparer<IRange>
	{
		// Token: 0x060003C4 RID: 964 RVA: 0x0000A09E File Offset: 0x0000829E
		private RangeComparer()
		{
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000A0A8 File Offset: 0x000082A8
		public int Compare(IRange x, IRange y)
		{
			int num = x.FirstIndex.CompareTo(y.FirstIndex);
			if (num == 0)
			{
				return x.LastIndex.CompareTo(y.LastIndex);
			}
			return num;
		}

		// Token: 0x040000CE RID: 206
		internal static readonly IComparer<IRange> Instance = new RangeComparer();
	}
}
