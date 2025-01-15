using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Data.Experimental.OData
{
	// Token: 0x0200002F RID: 47
	internal class ReadOnlyEnumerable<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x06000104 RID: 260 RVA: 0x00005EF6 File Offset: 0x000040F6
		internal ReadOnlyEnumerable(IEnumerable<T> sourceEnumerable)
		{
			this.sourceEnumerable = sourceEnumerable;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00005F05 File Offset: 0x00004105
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return this.sourceEnumerable.GetEnumerator();
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00005F12 File Offset: 0x00004112
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.sourceEnumerable.GetEnumerator();
		}

		// Token: 0x04000158 RID: 344
		private IEnumerable<T> sourceEnumerable;
	}
}
