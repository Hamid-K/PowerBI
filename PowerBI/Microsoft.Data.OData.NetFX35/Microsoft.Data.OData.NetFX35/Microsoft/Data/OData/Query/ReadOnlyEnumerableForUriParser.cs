using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x02000087 RID: 135
	internal sealed class ReadOnlyEnumerableForUriParser<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x06000336 RID: 822 RVA: 0x0000B52F File Offset: 0x0000972F
		internal ReadOnlyEnumerableForUriParser(IEnumerable<T> sourceEnumerable)
		{
			this.sourceEnumerable = sourceEnumerable;
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000B53E File Offset: 0x0000973E
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return this.sourceEnumerable.GetEnumerator();
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000B54B File Offset: 0x0000974B
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.sourceEnumerable.GetEnumerator();
		}

		// Token: 0x040000F6 RID: 246
		private IEnumerable<T> sourceEnumerable;
	}
}
