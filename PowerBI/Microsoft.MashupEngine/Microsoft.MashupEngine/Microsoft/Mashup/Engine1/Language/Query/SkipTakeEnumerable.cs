using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x02001814 RID: 6164
	internal class SkipTakeEnumerable : IEnumerable<IValueReference>, IEnumerable
	{
		// Token: 0x06009C18 RID: 39960 RVA: 0x00203540 File Offset: 0x00201740
		public SkipTakeEnumerable(IEnumerable<IValueReference> rows, RowRange rowRange)
		{
			this.rows = rows;
			this.rowRange = rowRange;
		}

		// Token: 0x06009C19 RID: 39961 RVA: 0x00203556 File Offset: 0x00201756
		public IEnumerator<IValueReference> GetEnumerator()
		{
			return new SkipTakeEnumerator<IValueReference>(this.rows.GetEnumerator(), this.rowRange.SkipCount, this.rowRange.TakeCount);
		}

		// Token: 0x06009C1A RID: 39962 RVA: 0x0020357E File Offset: 0x0020177E
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04005242 RID: 21058
		private IEnumerable<IValueReference> rows;

		// Token: 0x04005243 RID: 21059
		private RowRange rowRange;
	}
}
