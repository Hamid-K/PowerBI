using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006BB RID: 1723
	[Serializable]
	internal sealed class DrillthroughHashtable : HashtableInstanceInfo
	{
		// Token: 0x06005CDE RID: 23774 RVA: 0x0017A50F File Offset: 0x0017870F
		internal DrillthroughHashtable()
		{
		}

		// Token: 0x06005CDF RID: 23775 RVA: 0x0017A517 File Offset: 0x00178717
		internal DrillthroughHashtable(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x17002097 RID: 8343
		internal DrillthroughInformation this[string key]
		{
			get
			{
				return (DrillthroughInformation)this.m_hashtable[key];
			}
			set
			{
				this.m_hashtable[key] = value;
			}
		}

		// Token: 0x06005CE2 RID: 23778 RVA: 0x0017A542 File Offset: 0x00178742
		internal void Add(string drillthroughId, DrillthroughInformation drillthroughInfo)
		{
			this.m_hashtable.Add(drillthroughId, drillthroughInfo);
		}
	}
}
