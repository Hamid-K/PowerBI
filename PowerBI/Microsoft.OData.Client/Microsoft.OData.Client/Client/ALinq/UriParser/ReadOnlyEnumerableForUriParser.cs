using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.OData.Client.ALinq.UriParser
{
	// Token: 0x02000136 RID: 310
	internal sealed class ReadOnlyEnumerableForUriParser<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x06000CAA RID: 3242 RVA: 0x0002D2EF File Offset: 0x0002B4EF
		internal ReadOnlyEnumerableForUriParser(IEnumerable<T> sourceEnumerable)
		{
			this.sourceEnumerable = sourceEnumerable;
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x0002D2FE File Offset: 0x0002B4FE
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return this.sourceEnumerable.GetEnumerator();
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x0002D2FE File Offset: 0x0002B4FE
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.sourceEnumerable.GetEnumerator();
		}

		// Token: 0x040006A8 RID: 1704
		private IEnumerable<T> sourceEnumerable;
	}
}
