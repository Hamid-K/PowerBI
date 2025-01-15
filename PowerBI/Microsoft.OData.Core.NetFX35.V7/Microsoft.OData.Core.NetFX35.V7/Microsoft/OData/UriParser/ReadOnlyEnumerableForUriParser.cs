using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000124 RID: 292
	internal sealed class ReadOnlyEnumerableForUriParser<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x06000D78 RID: 3448 RVA: 0x000284C2 File Offset: 0x000266C2
		internal ReadOnlyEnumerableForUriParser(IEnumerable<T> sourceEnumerable)
		{
			this.sourceEnumerable = sourceEnumerable;
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x000284D1 File Offset: 0x000266D1
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return this.sourceEnumerable.GetEnumerator();
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x000284D1 File Offset: 0x000266D1
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.sourceEnumerable.GetEnumerator();
		}

		// Token: 0x04000726 RID: 1830
		private IEnumerable<T> sourceEnumerable;
	}
}
