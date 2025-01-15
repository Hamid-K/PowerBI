using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x02000061 RID: 97
	public class BigDictionary<TKey, TValue>
	{
		// Token: 0x06000388 RID: 904 RVA: 0x000192C4 File Offset: 0x000174C4
		public BigDictionary()
		{
		}

		// Token: 0x06000389 RID: 905 RVA: 0x000192CC File Offset: 0x000174CC
		public BigDictionary(int N)
		{
			this.N = N;
			this.d = new List<Dictionary<TKey, TValue>>();
			for (int i = 0; i < N; i++)
			{
				this.d.Add(new Dictionary<TKey, TValue>());
			}
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00019310 File Offset: 0x00017510
		public void Add(TKey key, TValue value)
		{
			int hashCode = key.GetHashCode();
			int num = ((hashCode < 0) ? (-hashCode % this.N) : (hashCode % this.N));
			this.d[num].Add(key, value);
		}

		// Token: 0x0600038B RID: 907 RVA: 0x00019358 File Offset: 0x00017558
		public bool TryGetValue(TKey key, out TValue value)
		{
			int hashCode = key.GetHashCode();
			int num = ((hashCode < 0) ? (-hashCode % this.N) : (hashCode % this.N));
			return this.d[num].TryGetValue(key, ref value);
		}

		// Token: 0x17000087 RID: 135
		public TValue this[TKey key]
		{
			get
			{
				int hashCode = key.GetHashCode();
				int num = ((hashCode < 0) ? (-hashCode % this.N) : (hashCode % this.N));
				return this.d[num][key];
			}
			set
			{
				int hashCode = key.GetHashCode();
				int num = ((hashCode < 0) ? (-hashCode % this.N) : (hashCode % this.N));
				this.d[num][key] = value;
			}
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00019430 File Offset: 0x00017630
		public void Clear()
		{
			for (int i = 0; i < this.N; i++)
			{
				this.d[i].Clear();
			}
		}

		// Token: 0x0600038F RID: 911 RVA: 0x00019460 File Offset: 0x00017660
		public bool ContainsKey(TKey key)
		{
			int hashCode = key.GetHashCode();
			int num = ((hashCode < 0) ? (-hashCode % this.N) : (hashCode % this.N));
			return this.d[num].ContainsKey(key);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x000194A8 File Offset: 0x000176A8
		public bool Remove(TKey key)
		{
			int hashCode = key.GetHashCode();
			int num = ((hashCode < 0) ? (-hashCode % this.N) : (hashCode % this.N));
			return this.d[num].Remove(key);
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000391 RID: 913 RVA: 0x000194F0 File Offset: 0x000176F0
		public int Count
		{
			get
			{
				int num = 0;
				for (int i = 0; i < this.N; i++)
				{
					num += Enumerable.Count<KeyValuePair<TKey, TValue>>(this.d[i]);
				}
				return num;
			}
		}

		// Token: 0x0400008D RID: 141
		private List<Dictionary<TKey, TValue>> d;

		// Token: 0x0400008E RID: 142
		private int N;
	}
}
