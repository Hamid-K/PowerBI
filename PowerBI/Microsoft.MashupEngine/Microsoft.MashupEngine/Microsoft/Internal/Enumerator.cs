using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Internal
{
	// Token: 0x020001AA RID: 426
	internal abstract class Enumerator<T> : IEnumerator<T>, IDisposable, IEnumerator
	{
		// Token: 0x0600081D RID: 2077 RVA: 0x0000336E File Offset: 0x0000156E
		public virtual void Dispose()
		{
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x0600081E RID: 2078 RVA: 0x0000F6D1 File Offset: 0x0000D8D1
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x000033E7 File Offset: 0x000015E7
		public void Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000820 RID: 2080
		public abstract T Current { get; }

		// Token: 0x06000821 RID: 2081
		public abstract bool MoveNext();
	}
}
