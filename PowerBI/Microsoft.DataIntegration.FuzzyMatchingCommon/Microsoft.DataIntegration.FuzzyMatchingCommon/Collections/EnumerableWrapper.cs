using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x02000074 RID: 116
	public class EnumerableWrapper<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x0600049D RID: 1181 RVA: 0x0001CBBB File Offset: 0x0001ADBB
		public EnumerableWrapper(IEnumerable<T> e)
		{
			this.m_e = e;
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0001CBCA File Offset: 0x0001ADCA
		public IEnumerator<T> GetEnumerator()
		{
			return this.m_e.GetEnumerator();
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0001CBD7 File Offset: 0x0001ADD7
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.m_e.GetEnumerator();
		}

		// Token: 0x040000D7 RID: 215
		public IEnumerable<T> m_e;
	}
}
