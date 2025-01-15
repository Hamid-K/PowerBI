using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.BIServer.HostingEnvironment
{
	// Token: 0x02000009 RID: 9
	public static class DictionaryExtensions
	{
		// Token: 0x06000033 RID: 51 RVA: 0x000027E0 File Offset: 0x000009E0
		public static IEnumerable<K> Differences<K, V>(this IDictionary<K, V> first, IDictionary<K, V> second)
		{
			return (from x in first.Except(second)
				select x.Key).Union(from x in second.Except(first)
				select x.Key).Distinct<K>().ToList<K>();
		}
	}
}
