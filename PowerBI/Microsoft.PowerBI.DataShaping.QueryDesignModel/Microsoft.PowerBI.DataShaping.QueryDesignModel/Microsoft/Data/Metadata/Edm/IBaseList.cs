using System;
using System.Collections;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x02000093 RID: 147
	internal interface IBaseList<T> : IList, ICollection, IEnumerable
	{
		// Token: 0x170003CB RID: 971
		T this[string identity] { get; }

		// Token: 0x170003CC RID: 972
		T this[int index] { get; }

		// Token: 0x06000A6F RID: 2671
		int IndexOf(T item);
	}
}
