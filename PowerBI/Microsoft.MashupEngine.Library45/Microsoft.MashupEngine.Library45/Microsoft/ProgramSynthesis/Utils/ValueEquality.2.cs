using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x02000522 RID: 1314
	public class ValueEquality<T> : IEqualityComparer<T>
	{
		// Token: 0x06001D41 RID: 7489 RVA: 0x00002130 File Offset: 0x00000330
		private ValueEquality()
		{
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06001D42 RID: 7490 RVA: 0x00056F3C File Offset: 0x0005513C
		public static ValueEquality<T> Instance { get; } = new ValueEquality<T>();

		// Token: 0x06001D43 RID: 7491 RVA: 0x00056F43 File Offset: 0x00055143
		public bool Equals(T x, T y)
		{
			return ValueEquality.Comparer.Equals(x, y);
		}

		// Token: 0x06001D44 RID: 7492 RVA: 0x00056F5B File Offset: 0x0005515B
		public int GetHashCode(T obj)
		{
			return ValueEquality.Comparer.GetHashCode(obj);
		}
	}
}
