using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006B7 RID: 1719
	[Serializable]
	internal abstract class HashtableInstanceInfo : InstanceInfo
	{
		// Token: 0x06005CC5 RID: 23749 RVA: 0x0017A2CE File Offset: 0x001784CE
		protected HashtableInstanceInfo()
		{
			this.m_hashtable = new Hashtable();
		}

		// Token: 0x06005CC6 RID: 23750 RVA: 0x0017A2E1 File Offset: 0x001784E1
		protected HashtableInstanceInfo(int capacity)
		{
			this.m_hashtable = new Hashtable(capacity);
		}

		// Token: 0x17002091 RID: 8337
		// (get) Token: 0x06005CC7 RID: 23751 RVA: 0x0017A2F5 File Offset: 0x001784F5
		internal int Count
		{
			get
			{
				return this.m_hashtable.Count;
			}
		}

		// Token: 0x06005CC8 RID: 23752 RVA: 0x0017A302 File Offset: 0x00178502
		internal bool ContainsKey(int key)
		{
			return this.m_hashtable.ContainsKey(key);
		}

		// Token: 0x06005CC9 RID: 23753 RVA: 0x0017A315 File Offset: 0x00178515
		internal IDictionaryEnumerator GetEnumerator()
		{
			return this.m_hashtable.GetEnumerator();
		}

		// Token: 0x04002F99 RID: 12185
		protected Hashtable m_hashtable;
	}
}
