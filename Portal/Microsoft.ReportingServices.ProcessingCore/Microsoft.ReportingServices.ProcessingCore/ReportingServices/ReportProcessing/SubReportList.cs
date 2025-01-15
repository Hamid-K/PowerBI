using System;
using System.Collections;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006A9 RID: 1705
	[ArrayOfReferences]
	[Serializable]
	internal sealed class SubReportList : ArrayList
	{
		// Token: 0x06005C90 RID: 23696 RVA: 0x00179A7C File Offset: 0x00177C7C
		internal SubReportList()
		{
		}

		// Token: 0x06005C91 RID: 23697 RVA: 0x00179A84 File Offset: 0x00177C84
		internal SubReportList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x17002083 RID: 8323
		internal SubReport this[int index]
		{
			get
			{
				return (SubReport)base[index];
			}
		}

		// Token: 0x06005C93 RID: 23699 RVA: 0x00179A9C File Offset: 0x00177C9C
		internal new SubReportList Clone()
		{
			int count = this.Count;
			SubReportList subReportList = new SubReportList(count);
			for (int i = 0; i < count; i++)
			{
				subReportList.Add(this[i]);
			}
			return subReportList;
		}
	}
}
