using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006D9 RID: 1753
	[Serializable]
	internal sealed class FieldPropertyHashtable
	{
		// Token: 0x06005F18 RID: 24344 RVA: 0x001818CF File Offset: 0x0017FACF
		internal FieldPropertyHashtable()
		{
			this.m_hashtable = new Hashtable();
		}

		// Token: 0x06005F19 RID: 24345 RVA: 0x001818E2 File Offset: 0x0017FAE2
		internal FieldPropertyHashtable(int capacity)
		{
			this.m_hashtable = new Hashtable(capacity);
		}

		// Token: 0x06005F1A RID: 24346 RVA: 0x001818F6 File Offset: 0x0017FAF6
		internal void Add(string key)
		{
			this.m_hashtable.Add(key, null);
		}

		// Token: 0x1700216C RID: 8556
		// (get) Token: 0x06005F1B RID: 24347 RVA: 0x00181905 File Offset: 0x0017FB05
		internal int Count
		{
			get
			{
				return this.m_hashtable.Count;
			}
		}

		// Token: 0x06005F1C RID: 24348 RVA: 0x00181912 File Offset: 0x0017FB12
		internal bool ContainsKey(string key)
		{
			return this.m_hashtable.ContainsKey(key);
		}

		// Token: 0x06005F1D RID: 24349 RVA: 0x00181920 File Offset: 0x0017FB20
		internal IDictionaryEnumerator GetEnumerator()
		{
			return this.m_hashtable.GetEnumerator();
		}

		// Token: 0x04003075 RID: 12405
		private Hashtable m_hashtable;
	}
}
