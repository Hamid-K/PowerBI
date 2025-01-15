using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav
{
	// Token: 0x0200002C RID: 44
	public static class TupleExtensions
	{
		// Token: 0x06000238 RID: 568 RVA: 0x000070CC File Offset: 0x000052CC
		public static void Deconstruct<TKey, TValue>(this KeyValuePair<TKey, TValue> pair, out TKey key, out TValue value)
		{
			key = pair.Key;
			value = pair.Value;
		}
	}
}
