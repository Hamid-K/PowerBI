using System;
using System.Collections.Generic;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200023E RID: 574
	public class KeyedFromToCache<TFrom, TTo> where TFrom : IEquatable<TFrom>
	{
		// Token: 0x06000ED2 RID: 3794 RVA: 0x000333FC File Offset: 0x000315FC
		public KeyedFromToCache()
		{
			this.m_cache = new Dictionary<string, Pair<TFrom, TTo>>();
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x00033410 File Offset: 0x00031610
		public bool TryGet(string key, TFrom from, out TTo to)
		{
			Dictionary<string, Pair<TFrom, TTo>> cache = this.m_cache;
			bool flag2;
			lock (cache)
			{
				Pair<TFrom, TTo> pair = null;
				if (!this.m_cache.TryGetValue(key, out pair))
				{
					to = default(TTo);
					flag2 = false;
				}
				else
				{
					TFrom first = pair.First;
					if (first.Equals(from))
					{
						to = pair.Second;
						flag2 = true;
					}
					else
					{
						to = default(TTo);
						flag2 = false;
					}
				}
			}
			return flag2;
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x0003349C File Offset: 0x0003169C
		public void Put(string key, TFrom from, TTo to)
		{
			Dictionary<string, Pair<TFrom, TTo>> cache = this.m_cache;
			lock (cache)
			{
				Pair<TFrom, TTo> pair = new Pair<TFrom, TTo>(from, to);
				this.m_cache[key] = pair;
			}
		}

		// Token: 0x040005AA RID: 1450
		private Dictionary<string, Pair<TFrom, TTo>> m_cache;
	}
}
