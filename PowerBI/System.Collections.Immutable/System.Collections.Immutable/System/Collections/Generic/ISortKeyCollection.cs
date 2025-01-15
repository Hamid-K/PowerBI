using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x0200004D RID: 77
	[NullableContext(1)]
	internal interface ISortKeyCollection<[Nullable(2)] in TKey>
	{
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600039F RID: 927
		IComparer<TKey> KeyComparer { get; }
	}
}
