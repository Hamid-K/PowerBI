using System;
using System.Collections;

namespace Microsoft.OData.Core
{
	// Token: 0x020001B0 RID: 432
	internal class ReadOnlyEnumerable : IEnumerable
	{
		// Token: 0x06001007 RID: 4103 RVA: 0x00037862 File Offset: 0x00035A62
		internal ReadOnlyEnumerable(IEnumerable sourceEnumerable)
		{
			this.sourceEnumerable = sourceEnumerable;
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x00037871 File Offset: 0x00035A71
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.sourceEnumerable.GetEnumerator();
		}

		// Token: 0x0400074D RID: 1869
		private readonly IEnumerable sourceEnumerable;
	}
}
