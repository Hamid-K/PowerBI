﻿using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x0200001F RID: 31
	[NullableContext(2)]
	internal interface IImmutableDictionaryInternal<TKey, TValue>
	{
		// Token: 0x0600007C RID: 124
		[NullableContext(1)]
		bool ContainsValue(TValue value);
	}
}
