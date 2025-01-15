using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x020005CB RID: 1483
	internal interface ICustomComparable
	{
		// Token: 0x06005396 RID: 21398
		int GetHashCode(IEqualityComparer comparer);

		// Token: 0x06005397 RID: 21399
		int CompareTo(ICustomComparable other, IComparer<object> comparer);
	}
}
