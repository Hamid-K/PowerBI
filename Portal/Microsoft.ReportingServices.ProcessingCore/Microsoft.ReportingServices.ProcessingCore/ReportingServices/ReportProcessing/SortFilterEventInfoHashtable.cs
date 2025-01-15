using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006BF RID: 1727
	[Serializable]
	internal sealed class SortFilterEventInfoHashtable : HashtableInstanceInfo
	{
		// Token: 0x06005CF1 RID: 23793 RVA: 0x0017A630 File Offset: 0x00178830
		internal SortFilterEventInfoHashtable()
		{
		}

		// Token: 0x06005CF2 RID: 23794 RVA: 0x0017A638 File Offset: 0x00178838
		internal SortFilterEventInfoHashtable(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x1700209B RID: 8347
		internal SortFilterEventInfo this[int key]
		{
			get
			{
				return (SortFilterEventInfo)this.m_hashtable[key];
			}
		}

		// Token: 0x06005CF4 RID: 23796 RVA: 0x0017A659 File Offset: 0x00178859
		internal void Add(int key, SortFilterEventInfo val)
		{
			this.m_hashtable.Add(key, val);
		}
	}
}
