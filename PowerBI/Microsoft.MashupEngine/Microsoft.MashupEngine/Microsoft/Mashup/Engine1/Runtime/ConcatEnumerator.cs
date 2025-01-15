using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020012A9 RID: 4777
	internal class ConcatEnumerator<T> : IEnumerator<T>, IDisposable, IEnumerator
	{
		// Token: 0x06007D6B RID: 32107 RVA: 0x001ADD58 File Offset: 0x001ABF58
		public ConcatEnumerator(IEnumerator<T> e1, IEnumerator<T> e2)
		{
			this.e1 = e1;
			this.e2 = e2;
		}

		// Token: 0x17002217 RID: 8727
		// (get) Token: 0x06007D6C RID: 32108 RVA: 0x001ADD6E File Offset: 0x001ABF6E
		public T Current
		{
			get
			{
				return this.e1.Current;
			}
		}

		// Token: 0x17002218 RID: 8728
		// (get) Token: 0x06007D6D RID: 32109 RVA: 0x001ADD7B File Offset: 0x001ABF7B
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x06007D6E RID: 32110 RVA: 0x0000EE09 File Offset: 0x0000D009
		public void Reset()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06007D6F RID: 32111 RVA: 0x001ADD88 File Offset: 0x001ABF88
		public void Dispose()
		{
			if (this.e1 != null)
			{
				this.e1.Dispose();
				this.e1 = null;
			}
			if (this.e2 != null)
			{
				this.e2.Dispose();
				this.e2 = null;
			}
		}

		// Token: 0x06007D70 RID: 32112 RVA: 0x001ADDBE File Offset: 0x001ABFBE
		public bool MoveNext()
		{
			while (this.e1 != null)
			{
				if (this.e1.MoveNext())
				{
					return true;
				}
				this.e1.Dispose();
				this.e1 = this.e2;
				this.e2 = null;
			}
			return false;
		}

		// Token: 0x04004516 RID: 17686
		private IEnumerator<T> e1;

		// Token: 0x04004517 RID: 17687
		private IEnumerator<T> e2;
	}
}
