using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x020005C0 RID: 1472
	internal interface IDataComparer : IEqualityComparer, IEqualityComparer<object>, IComparer, IComparer<object>
	{
		// Token: 0x06005347 RID: 21319
		int Compare(object x, object y, bool extendedTypeComparisons);

		// Token: 0x06005348 RID: 21320
		int Compare(object x, object y, bool throwExceptionOnComparisonFailure, bool extendedTypeComparisons, out bool validComparisonResult);
	}
}
