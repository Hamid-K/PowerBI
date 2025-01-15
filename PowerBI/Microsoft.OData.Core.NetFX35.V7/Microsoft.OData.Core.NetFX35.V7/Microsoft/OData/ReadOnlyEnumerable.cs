using System;
using System.Collections;

namespace Microsoft.OData
{
	// Token: 0x020000A8 RID: 168
	internal class ReadOnlyEnumerable : IEnumerable
	{
		// Token: 0x06000683 RID: 1667 RVA: 0x00011FD0 File Offset: 0x000101D0
		internal ReadOnlyEnumerable(IEnumerable sourceEnumerable)
		{
			this.sourceEnumerable = sourceEnumerable;
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x00011FDF File Offset: 0x000101DF
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.sourceEnumerable.GetEnumerator();
		}

		// Token: 0x040002E5 RID: 741
		private readonly IEnumerable sourceEnumerable;
	}
}
