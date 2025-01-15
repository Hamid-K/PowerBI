using System;
using System.Collections;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004C4 RID: 1220
	internal interface IBaseList<T> : IList, ICollection, IEnumerable
	{
		// Token: 0x17000BDC RID: 3036
		T this[string identity] { get; }

		// Token: 0x17000BDD RID: 3037
		T this[int index] { get; }

		// Token: 0x06003C4C RID: 15436
		int IndexOf(T item);
	}
}
