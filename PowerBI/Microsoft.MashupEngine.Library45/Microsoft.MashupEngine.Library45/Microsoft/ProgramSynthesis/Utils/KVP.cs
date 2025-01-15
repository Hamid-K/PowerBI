using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x020004A5 RID: 1189
	public static class KVP
	{
		// Token: 0x06001AB9 RID: 6841 RVA: 0x00050630 File Offset: 0x0004E830
		public static KeyValuePair<TKey, TValue> Create<TKey, TValue>(TKey key, TValue value)
		{
			return new KeyValuePair<TKey, TValue>(key, value);
		}
	}
}
