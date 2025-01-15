using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x020020D1 RID: 8401
	[NullableContext(1)]
	internal interface ISortKeyCollection<[Nullable(2)] in TKey>
	{
		// Token: 0x17002F51 RID: 12113
		// (get) Token: 0x06011A0A RID: 72202
		IComparer<TKey> KeyComparer { get; }
	}
}
