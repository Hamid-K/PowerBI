using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004F1 RID: 1265
	internal class IComparerToGeneric<T> : IComparer<T>
	{
		// Token: 0x06004060 RID: 16480 RVA: 0x0010FAAF File Offset: 0x0010DCAF
		internal IComparerToGeneric(IComparer comparer)
		{
			this.m_comparer = comparer;
		}

		// Token: 0x06004061 RID: 16481 RVA: 0x0010FABE File Offset: 0x0010DCBE
		public int Compare(T x, T y)
		{
			return this.m_comparer.Compare(x, y);
		}

		// Token: 0x04001D91 RID: 7569
		private IComparer m_comparer;
	}
}
