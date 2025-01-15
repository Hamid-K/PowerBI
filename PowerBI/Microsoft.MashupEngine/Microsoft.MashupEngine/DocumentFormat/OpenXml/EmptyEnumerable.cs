using System;
using System.Collections;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml
{
	// Token: 0x02002115 RID: 8469
	internal class EmptyEnumerable<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x0600D13D RID: 53565 RVA: 0x0029A2B2 File Offset: 0x002984B2
		public IEnumerator<T> GetEnumerator()
		{
			return EmptyEnumerator<T>.EmptyEnumeratorSingleton;
		}

		// Token: 0x0600D13E RID: 53566 RVA: 0x0029A2B2 File Offset: 0x002984B2
		IEnumerator IEnumerable.GetEnumerator()
		{
			return EmptyEnumerator<T>.EmptyEnumeratorSingleton;
		}

		// Token: 0x0600D13F RID: 53567 RVA: 0x000020FD File Offset: 0x000002FD
		private EmptyEnumerable()
		{
		}

		// Token: 0x17003290 RID: 12944
		// (get) Token: 0x0600D140 RID: 53568 RVA: 0x0029A2B9 File Offset: 0x002984B9
		public static EmptyEnumerable<T> EmptyEnumerableSingleton
		{
			get
			{
				return EmptyEnumerable<T>._dummy;
			}
		}

		// Token: 0x0400693E RID: 26942
		private static readonly EmptyEnumerable<T> _dummy = new EmptyEnumerable<T>();
	}
}
