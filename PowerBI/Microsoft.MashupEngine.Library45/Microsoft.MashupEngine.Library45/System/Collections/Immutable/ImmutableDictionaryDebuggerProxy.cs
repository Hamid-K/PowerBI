using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x0200209F RID: 8351
	[NullableContext(1)]
	[Nullable(new byte[] { 0, 0, 1, 1 })]
	internal class ImmutableDictionaryDebuggerProxy<TKey, [Nullable(2)] TValue> : ImmutableEnumerableDebuggerProxy<KeyValuePair<TKey, TValue>>
	{
		// Token: 0x060116AA RID: 71338 RVA: 0x003BBFA6 File Offset: 0x003BA1A6
		public ImmutableDictionaryDebuggerProxy(IImmutableDictionary<TKey, TValue> dictionary)
			: base(dictionary)
		{
		}
	}
}
