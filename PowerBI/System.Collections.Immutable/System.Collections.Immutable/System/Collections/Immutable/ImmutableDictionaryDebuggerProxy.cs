using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x0200002F RID: 47
	[NullableContext(1)]
	[Nullable(new byte[] { 0, 0, 1, 1 })]
	internal class ImmutableDictionaryDebuggerProxy<TKey, [Nullable(2)] TValue> : ImmutableEnumerableDebuggerProxy<KeyValuePair<TKey, TValue>>
	{
		// Token: 0x060001E1 RID: 481 RVA: 0x0000622E File Offset: 0x0000442E
		public ImmutableDictionaryDebuggerProxy(IImmutableDictionary<TKey, TValue> dictionary)
			: base(dictionary)
		{
		}
	}
}
