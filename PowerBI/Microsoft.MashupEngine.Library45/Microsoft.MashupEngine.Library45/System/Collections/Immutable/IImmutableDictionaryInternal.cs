using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x0200206C RID: 8300
	[NullableContext(2)]
	internal interface IImmutableDictionaryInternal<TKey, TValue>
	{
		// Token: 0x06011415 RID: 70677
		[NullableContext(1)]
		bool ContainsValue(TValue value);
	}
}
