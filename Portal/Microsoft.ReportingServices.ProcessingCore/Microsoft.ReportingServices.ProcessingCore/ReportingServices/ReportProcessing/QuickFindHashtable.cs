using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006BE RID: 1726
	[HashtableOfReferences]
	[Serializable]
	internal sealed class QuickFindHashtable : HashtableInstanceInfo
	{
		// Token: 0x06005CED RID: 23789 RVA: 0x0017A5F3 File Offset: 0x001787F3
		internal QuickFindHashtable()
		{
		}

		// Token: 0x06005CEE RID: 23790 RVA: 0x0017A5FB File Offset: 0x001787FB
		internal QuickFindHashtable(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x1700209A RID: 8346
		internal ReportItemInstance this[int key]
		{
			get
			{
				return (ReportItemInstance)this.m_hashtable[key];
			}
		}

		// Token: 0x06005CF0 RID: 23792 RVA: 0x0017A61C File Offset: 0x0017881C
		internal void Add(int key, ReportItemInstance val)
		{
			this.m_hashtable.Add(key, val);
		}
	}
}
