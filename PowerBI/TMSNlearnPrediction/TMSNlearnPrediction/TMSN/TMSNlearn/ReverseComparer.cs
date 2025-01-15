using System;
using System.Collections.Generic;

namespace Microsoft.TMSN.TMSNlearn
{
	// Token: 0x020004AF RID: 1199
	public class ReverseComparer<T> : IComparer<T> where T : IComparable<T>
	{
		// Token: 0x060018D2 RID: 6354 RVA: 0x0008D40A File Offset: 0x0008B60A
		public int Compare(T x, T y)
		{
			return -x.CompareTo(y);
		}
	}
}
