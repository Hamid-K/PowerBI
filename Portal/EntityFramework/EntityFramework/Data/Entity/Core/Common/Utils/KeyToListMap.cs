using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace System.Data.Entity.Core.Common.Utils
{
	// Token: 0x020005F7 RID: 1527
	internal class KeyToListMap<TKey, TValue> : InternalBase
	{
		// Token: 0x06004A96 RID: 19094 RVA: 0x0010862C File Offset: 0x0010682C
		internal KeyToListMap(IEqualityComparer<TKey> comparer)
		{
			this.m_map = new Dictionary<TKey, List<TValue>>(comparer);
		}

		// Token: 0x17000EA4 RID: 3748
		// (get) Token: 0x06004A97 RID: 19095 RVA: 0x00108640 File Offset: 0x00106840
		internal IEnumerable<TKey> Keys
		{
			get
			{
				return this.m_map.Keys;
			}
		}

		// Token: 0x17000EA5 RID: 3749
		// (get) Token: 0x06004A98 RID: 19096 RVA: 0x0010864D File Offset: 0x0010684D
		internal IEnumerable<TValue> AllValues
		{
			get
			{
				foreach (TKey tkey in this.Keys)
				{
					foreach (TValue tvalue in this.ListForKey(tkey))
					{
						yield return tvalue;
					}
					IEnumerator<TValue> enumerator2 = null;
				}
				IEnumerator<TKey> enumerator = null;
				yield break;
				yield break;
			}
		}

		// Token: 0x17000EA6 RID: 3750
		// (get) Token: 0x06004A99 RID: 19097 RVA: 0x0010865D File Offset: 0x0010685D
		internal IEnumerable<KeyValuePair<TKey, List<TValue>>> KeyValuePairs
		{
			get
			{
				return this.m_map;
			}
		}

		// Token: 0x06004A9A RID: 19098 RVA: 0x00108665 File Offset: 0x00106865
		internal bool ContainsKey(TKey key)
		{
			return this.m_map.ContainsKey(key);
		}

		// Token: 0x06004A9B RID: 19099 RVA: 0x00108674 File Offset: 0x00106874
		internal void Add(TKey key, TValue value)
		{
			List<TValue> list;
			if (!this.m_map.TryGetValue(key, out list))
			{
				list = new List<TValue>();
				this.m_map[key] = list;
			}
			list.Add(value);
		}

		// Token: 0x06004A9C RID: 19100 RVA: 0x001086AC File Offset: 0x001068AC
		internal void AddRange(TKey key, IEnumerable<TValue> values)
		{
			foreach (TValue tvalue in values)
			{
				this.Add(key, tvalue);
			}
		}

		// Token: 0x06004A9D RID: 19101 RVA: 0x001086F8 File Offset: 0x001068F8
		internal bool RemoveKey(TKey key)
		{
			return this.m_map.Remove(key);
		}

		// Token: 0x06004A9E RID: 19102 RVA: 0x00108706 File Offset: 0x00106906
		internal ReadOnlyCollection<TValue> ListForKey(TKey key)
		{
			return new ReadOnlyCollection<TValue>(this.m_map[key]);
		}

		// Token: 0x06004A9F RID: 19103 RVA: 0x0010871C File Offset: 0x0010691C
		internal bool TryGetListForKey(TKey key, out ReadOnlyCollection<TValue> valueCollection)
		{
			valueCollection = null;
			List<TValue> list;
			if (this.m_map.TryGetValue(key, out list))
			{
				valueCollection = new ReadOnlyCollection<TValue>(list);
				return true;
			}
			return false;
		}

		// Token: 0x06004AA0 RID: 19104 RVA: 0x00108747 File Offset: 0x00106947
		internal IEnumerable<TValue> EnumerateValues(TKey key)
		{
			List<TValue> list;
			if (this.m_map.TryGetValue(key, out list))
			{
				foreach (TValue tvalue in list)
				{
					yield return tvalue;
				}
				List<TValue>.Enumerator enumerator = default(List<TValue>.Enumerator);
			}
			yield break;
			yield break;
		}

		// Token: 0x06004AA1 RID: 19105 RVA: 0x00108760 File Offset: 0x00106960
		internal override void ToCompactString(StringBuilder builder)
		{
			foreach (TKey tkey in this.Keys)
			{
				StringUtil.FormatStringBuilder(builder, "{0}", new object[] { tkey });
				builder.Append(": ");
				IEnumerable<TValue> enumerable = this.ListForKey(tkey);
				StringUtil.ToSeparatedString(builder, enumerable, ",", "null");
				builder.Append("; ");
			}
		}

		// Token: 0x04001A3B RID: 6715
		private readonly Dictionary<TKey, List<TValue>> m_map;
	}
}
