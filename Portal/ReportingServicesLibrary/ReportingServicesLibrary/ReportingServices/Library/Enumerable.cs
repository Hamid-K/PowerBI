using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002CA RID: 714
	internal struct Enumerable<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x06001985 RID: 6533 RVA: 0x00066E92 File Offset: 0x00065092
		public static Enumerable<T> Wrap(IEnumerator<T> enumerator)
		{
			return new Enumerable<T>(enumerator);
		}

		// Token: 0x06001986 RID: 6534 RVA: 0x00066E9A File Offset: 0x0006509A
		private Enumerable(IEnumerator<T> enumerator)
		{
			this.m_enumerator = enumerator;
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x00066EA3 File Offset: 0x000650A3
		public IEnumerator<T> GetEnumerator()
		{
			return this.m_enumerator;
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x00066EA3 File Offset: 0x000650A3
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.m_enumerator;
		}

		// Token: 0x0400094F RID: 2383
		private readonly IEnumerator<T> m_enumerator;
	}
}
