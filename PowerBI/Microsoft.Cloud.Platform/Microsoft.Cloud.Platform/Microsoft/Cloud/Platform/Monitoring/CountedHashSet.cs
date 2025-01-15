using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Monitoring
{
	// Token: 0x02000071 RID: 113
	internal class CountedHashSet<TKey> : IEnumerable<TKey>, IEnumerable
	{
		// Token: 0x06000359 RID: 857 RVA: 0x0000CD5C File Offset: 0x0000AF5C
		public CountedHashSet()
		{
			this.m_dictionary = new Dictionary<TKey, CountedHashSet<TKey>.Box<int>>();
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000CD6F File Offset: 0x0000AF6F
		public CountedHashSet(IEqualityComparer<TKey> comparer)
		{
			this.m_dictionary = new Dictionary<TKey, CountedHashSet<TKey>.Box<int>>(comparer);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000CD84 File Offset: 0x0000AF84
		public int CountInstances(TKey key)
		{
			CountedHashSet<TKey>.Box<int> box;
			if (!this.m_dictionary.TryGetValue(key, out box))
			{
				return 0;
			}
			return box.Value;
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000CDAC File Offset: 0x0000AFAC
		public int CountInstances(Func<TKey, bool> condition)
		{
			return this.m_dictionary.Sum(delegate(KeyValuePair<TKey, CountedHashSet<TKey>.Box<int>> p)
			{
				if (!condition(p.Key))
				{
					return 0;
				}
				return p.Value.Value;
			});
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000CDE0 File Offset: 0x0000AFE0
		public void AddInstance(TKey key)
		{
			CountedHashSet<TKey>.Box<int> box;
			if (!this.m_dictionary.TryGetValue(key, out box))
			{
				box = new CountedHashSet<TKey>.Box<int>();
				this.m_dictionary.Add(key, box);
			}
			CountedHashSet<TKey>.Box<int> box2 = box;
			int value = box2.Value;
			box2.Value = value + 1;
			TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Key:{0} Counter:{1}", new object[] { key, box.Value });
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000CE4D File Offset: 0x0000B04D
		public void RemoveItem(TKey key)
		{
			this.m_dictionary.Remove(key);
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000CE5C File Offset: 0x0000B05C
		public IEnumerator<TKey> GetEnumerator()
		{
			return this.m_dictionary.Keys.GetEnumerator();
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000CE5C File Offset: 0x0000B05C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.m_dictionary.Keys.GetEnumerator();
		}

		// Token: 0x04000120 RID: 288
		private readonly Dictionary<TKey, CountedHashSet<TKey>.Box<int>> m_dictionary;

		// Token: 0x020005A0 RID: 1440
		private class Box<TValue> where TValue : struct
		{
			// Token: 0x170006EA RID: 1770
			// (get) Token: 0x06002B05 RID: 11013 RVA: 0x00099C4C File Offset: 0x00097E4C
			// (set) Token: 0x06002B06 RID: 11014 RVA: 0x00099C54 File Offset: 0x00097E54
			internal TValue Value { get; set; }
		}
	}
}
