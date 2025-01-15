using System;
using System.Collections;

namespace Microsoft.Data.OData
{
	// Token: 0x02000245 RID: 581
	internal class ReadOnlyEnumerable : IEnumerable
	{
		// Token: 0x060011BA RID: 4538 RVA: 0x0004354D File Offset: 0x0004174D
		internal ReadOnlyEnumerable(IEnumerable sourceEnumerable)
		{
			this.sourceEnumerable = sourceEnumerable;
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x0004355C File Offset: 0x0004175C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.sourceEnumerable.GetEnumerator();
		}

		// Token: 0x040006B1 RID: 1713
		private readonly IEnumerable sourceEnumerable;
	}
}
