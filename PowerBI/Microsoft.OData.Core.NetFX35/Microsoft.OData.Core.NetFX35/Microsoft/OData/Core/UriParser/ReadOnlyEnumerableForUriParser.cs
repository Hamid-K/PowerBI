using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.OData.Core.UriParser
{
	// Token: 0x0200021C RID: 540
	internal sealed class ReadOnlyEnumerableForUriParser<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x060013A3 RID: 5027 RVA: 0x0004866F File Offset: 0x0004686F
		internal ReadOnlyEnumerableForUriParser(IEnumerable<T> sourceEnumerable)
		{
			this.sourceEnumerable = sourceEnumerable;
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x0004867E File Offset: 0x0004687E
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return this.sourceEnumerable.GetEnumerator();
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x0004868B File Offset: 0x0004688B
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.sourceEnumerable.GetEnumerator();
		}

		// Token: 0x04000852 RID: 2130
		private IEnumerable<T> sourceEnumerable;
	}
}
