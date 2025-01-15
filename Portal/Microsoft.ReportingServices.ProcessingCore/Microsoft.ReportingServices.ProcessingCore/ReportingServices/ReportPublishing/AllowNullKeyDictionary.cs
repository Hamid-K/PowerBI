using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.ReportPublishing
{
	// Token: 0x0200039C RID: 924
	internal sealed class AllowNullKeyDictionary<TKey, TValue> where TKey : class where TValue : class
	{
		// Token: 0x060025AE RID: 9646 RVA: 0x000B4628 File Offset: 0x000B2828
		internal void Add(TKey key, TValue value)
		{
			if (key == null)
			{
				this.m_valueForNullKey = value;
				return;
			}
			this.m_hashtable.Add(key, value);
		}

		// Token: 0x060025AF RID: 9647 RVA: 0x000B4647 File Offset: 0x000B2847
		internal bool TryGetValue(TKey key, out TValue value)
		{
			value = default(TValue);
			if (key != null)
			{
				return this.m_hashtable.TryGetValue(key, out value);
			}
			if (this.m_valueForNullKey == null)
			{
				return false;
			}
			value = this.m_valueForNullKey;
			return true;
		}

		// Token: 0x170013C9 RID: 5065
		internal TValue this[TKey key]
		{
			get
			{
				TValue tvalue = default(TValue);
				this.TryGetValue(key, out tvalue);
				return tvalue;
			}
			set
			{
				this.Add(key, value);
			}
		}

		// Token: 0x040015FE RID: 5630
		private Dictionary<TKey, TValue> m_hashtable = new Dictionary<TKey, TValue>();

		// Token: 0x040015FF RID: 5631
		private TValue m_valueForNullKey;
	}
}
