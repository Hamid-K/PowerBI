using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.DataShaping.Common
{
	// Token: 0x02000014 RID: 20
	internal interface ICustomComparable
	{
		// Token: 0x060000B5 RID: 181
		int GetHashCode(IEqualityComparer comparer);

		// Token: 0x060000B6 RID: 182
		int CompareTo(ICustomComparable other, IComparer<object> comparer);
	}
}
