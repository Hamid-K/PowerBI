using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001617 RID: 5655
	internal class SkipTakeEnumerator<T> : IEnumerator<T>, IDisposable, IEnumerator
	{
		// Token: 0x06008E11 RID: 36369 RVA: 0x001DAE96 File Offset: 0x001D9096
		public SkipTakeEnumerator(IEnumerator<T> enumerator, RowCount skip, RowCount take)
		{
			this.enumerator = enumerator;
			this.skip = skip;
			this.take = take;
		}

		// Token: 0x17002549 RID: 9545
		// (get) Token: 0x06008E12 RID: 36370 RVA: 0x001DAEB3 File Offset: 0x001D90B3
		public T Current
		{
			get
			{
				return this.enumerator.Current;
			}
		}

		// Token: 0x1700254A RID: 9546
		// (get) Token: 0x06008E13 RID: 36371 RVA: 0x001DAEC0 File Offset: 0x001D90C0
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x06008E14 RID: 36372 RVA: 0x001DAECD File Offset: 0x001D90CD
		public void Dispose()
		{
			this.enumerator.Dispose();
		}

		// Token: 0x06008E15 RID: 36373 RVA: 0x0000EE09 File Offset: 0x0000D009
		public void Reset()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06008E16 RID: 36374 RVA: 0x001DAEDC File Offset: 0x001D90DC
		public bool MoveNext()
		{
			if (this.take.IsZero)
			{
				return false;
			}
			while (!this.skip.IsZero)
			{
				if (!this.enumerator.MoveNext())
				{
					return false;
				}
				this.skip = RowCount.op_Decrement(this.skip);
			}
			if (!this.enumerator.MoveNext())
			{
				return false;
			}
			this.take = RowCount.op_Decrement(this.take);
			return true;
		}

		// Token: 0x04004D4C RID: 19788
		private IEnumerator<T> enumerator;

		// Token: 0x04004D4D RID: 19789
		private RowCount skip;

		// Token: 0x04004D4E RID: 19790
		private RowCount take;
	}
}
