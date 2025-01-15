using System;
using System.Collections;

namespace Microsoft.OData
{
	// Token: 0x020000C6 RID: 198
	internal class ReadOnlyEnumerable : IEnumerable
	{
		// Token: 0x06000939 RID: 2361 RVA: 0x00016890 File Offset: 0x00014A90
		internal ReadOnlyEnumerable(IEnumerable sourceEnumerable)
		{
			this.sourceEnumerable = sourceEnumerable;
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x0001689F File Offset: 0x00014A9F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.sourceEnumerable.GetEnumerator();
		}

		// Token: 0x04000345 RID: 837
		private readonly IEnumerable sourceEnumerable;
	}
}
