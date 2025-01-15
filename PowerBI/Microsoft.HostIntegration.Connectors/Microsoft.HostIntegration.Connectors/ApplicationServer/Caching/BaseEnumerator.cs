using System;
using System.Collections;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001F9 RID: 505
	internal abstract class BaseEnumerator : IEnumerator
	{
		// Token: 0x06001079 RID: 4217 RVA: 0x00036FB8 File Offset: 0x000351B8
		internal BaseEnumerator()
		{
			this.Reset();
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x0600107A RID: 4218 RVA: 0x00036FC6 File Offset: 0x000351C6
		public object Current
		{
			get
			{
				if (this._current != null)
				{
					return this._current;
				}
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x00036FDC File Offset: 0x000351DC
		public bool MoveNext()
		{
			this._current = this.NextData(ref this._index);
			return this._current != null;
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x00036FFC File Offset: 0x000351FC
		public void Reset()
		{
			this._index = 0;
		}

		// Token: 0x0600107D RID: 4221
		protected abstract object NextData(ref int Index);

		// Token: 0x04000AC9 RID: 2761
		private int _index;

		// Token: 0x04000ACA RID: 2762
		private object _current;
	}
}
