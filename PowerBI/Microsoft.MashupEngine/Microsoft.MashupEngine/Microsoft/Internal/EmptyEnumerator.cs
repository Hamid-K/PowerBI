using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Internal
{
	// Token: 0x02000194 RID: 404
	internal class EmptyEnumerator<T> : IEnumerator<T>, IDisposable, IEnumerator
	{
		// Token: 0x17000263 RID: 611
		// (get) Token: 0x060007D8 RID: 2008 RVA: 0x0000EDFC File Offset: 0x0000CFFC
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x0000336E File Offset: 0x0000156E
		public void Dispose()
		{
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x0000336E File Offset: 0x0000156E
		public void Reset()
		{
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x060007DB RID: 2011 RVA: 0x0000EE09 File Offset: 0x0000D009
		public T Current
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x00002105 File Offset: 0x00000305
		public bool MoveNext()
		{
			return false;
		}

		// Token: 0x040004A2 RID: 1186
		public static readonly EmptyEnumerator<T> Instance = new EmptyEnumerator<T>();
	}
}
