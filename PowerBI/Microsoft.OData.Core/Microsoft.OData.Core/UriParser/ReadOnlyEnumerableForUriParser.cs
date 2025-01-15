using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000169 RID: 361
	internal sealed class ReadOnlyEnumerableForUriParser<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x06001250 RID: 4688 RVA: 0x00037DF6 File Offset: 0x00035FF6
		internal ReadOnlyEnumerableForUriParser(IEnumerable<T> sourceEnumerable)
		{
			this.sourceEnumerable = sourceEnumerable;
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x00037E05 File Offset: 0x00036005
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return this.sourceEnumerable.GetEnumerator();
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x00037E05 File Offset: 0x00036005
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.sourceEnumerable.GetEnumerator();
		}

		// Token: 0x04000847 RID: 2119
		private IEnumerable<T> sourceEnumerable;
	}
}
