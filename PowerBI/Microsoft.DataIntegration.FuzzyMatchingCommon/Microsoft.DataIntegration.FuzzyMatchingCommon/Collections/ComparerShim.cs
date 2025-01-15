using System;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x02000075 RID: 117
	public class ComparerShim<T> : IComparer<T>
	{
		// Token: 0x060004A0 RID: 1184 RVA: 0x0001CBE4 File Offset: 0x0001ADE4
		public ComparerShim(Func<T, T, int> comparison)
		{
			this.m_comparison = comparison;
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x0001CBF3 File Offset: 0x0001ADF3
		public int Compare(T x, T y)
		{
			return this.m_comparison.Invoke(x, y);
		}

		// Token: 0x040000D8 RID: 216
		private Func<T, T, int> m_comparison;
	}
}
