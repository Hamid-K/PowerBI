using System;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x02000076 RID: 118
	public class EqualityComparerShim<T, U> : IEqualityComparer<T>
	{
		// Token: 0x060004A2 RID: 1186 RVA: 0x0001CC02 File Offset: 0x0001AE02
		public EqualityComparerShim(Func<T, U> keySelector)
		{
			this.m_keySelector = keySelector;
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x0001CC28 File Offset: 0x0001AE28
		public bool Equals(T x, T y)
		{
			return this.m_comparison.Invoke(this.m_keySelector.Invoke(x), this.m_keySelector.Invoke(y));
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0001CC50 File Offset: 0x0001AE50
		public int GetHashCode(T x)
		{
			U u = this.m_keySelector.Invoke(x);
			return u.GetHashCode();
		}

		// Token: 0x040000D9 RID: 217
		private Func<T, U> m_keySelector;

		// Token: 0x040000DA RID: 218
		private Func<U, U, bool> m_comparison = new Func<U, U, bool>(EqualityComparer<U>.Default.Equals);
	}
}
