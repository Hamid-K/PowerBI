using System;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x02000077 RID: 119
	public class EqualityComparerShim<T> : IEqualityComparer<T>
	{
		// Token: 0x060004A5 RID: 1189 RVA: 0x0001CC77 File Offset: 0x0001AE77
		public EqualityComparerShim(Func<T, T, bool> comparison, Func<T, int> getHashCode)
		{
			this.m_comparison = comparison;
			this.m_getHashCode = getHashCode;
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0001CC8D File Offset: 0x0001AE8D
		public bool Equals(T x, T y)
		{
			return this.m_comparison.Invoke(x, y);
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x0001CC9C File Offset: 0x0001AE9C
		public int GetHashCode(T x)
		{
			return this.m_getHashCode.Invoke(x);
		}

		// Token: 0x040000DB RID: 219
		private Func<T, T, bool> m_comparison;

		// Token: 0x040000DC RID: 220
		private Func<T, int> m_getHashCode;
	}
}
