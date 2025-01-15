using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.Common;

namespace Microsoft.Reporting.Common.Internal
{
	// Token: 0x0200028C RID: 652
	internal sealed class SetEqualityComparator<T> : IEqualityComparer<ReadOnlyHashSetBase<T>>
	{
		// Token: 0x06001BE1 RID: 7137 RVA: 0x0004DCCF File Offset: 0x0004BECF
		public bool Equals(ReadOnlyHashSetBase<T> x, ReadOnlyHashSetBase<T> y)
		{
			return x == y || (x != null && x.SetEquals(y));
		}

		// Token: 0x06001BE2 RID: 7138 RVA: 0x0004DCE3 File Offset: 0x0004BEE3
		public int GetHashCode(ReadOnlyHashSetBase<T> obj)
		{
			return Hashing.CombineHashUnordered(obj.Select((T item) => item.GetHashCode()));
		}
	}
}
