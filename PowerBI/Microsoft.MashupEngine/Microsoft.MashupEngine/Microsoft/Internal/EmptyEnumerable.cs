using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Internal
{
	// Token: 0x02000193 RID: 403
	internal class EmptyEnumerable<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x060007D4 RID: 2004 RVA: 0x0000EDE9 File Offset: 0x0000CFE9
		public IEnumerator<T> GetEnumerator()
		{
			return EmptyEnumerator<T>.Instance;
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x0000EDE9 File Offset: 0x0000CFE9
		IEnumerator IEnumerable.GetEnumerator()
		{
			return EmptyEnumerator<T>.Instance;
		}

		// Token: 0x040004A1 RID: 1185
		public static readonly EmptyEnumerable<T> Instance = new EmptyEnumerable<T>();
	}
}
