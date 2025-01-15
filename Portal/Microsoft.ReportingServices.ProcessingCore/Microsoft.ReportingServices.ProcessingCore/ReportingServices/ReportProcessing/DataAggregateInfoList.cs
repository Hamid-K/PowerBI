using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000693 RID: 1683
	[Serializable]
	internal sealed class DataAggregateInfoList : ArrayList
	{
		// Token: 0x06005C42 RID: 23618 RVA: 0x00179641 File Offset: 0x00177841
		internal DataAggregateInfoList()
		{
		}

		// Token: 0x06005C43 RID: 23619 RVA: 0x00179649 File Offset: 0x00177849
		internal DataAggregateInfoList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x1700206D RID: 8301
		internal DataAggregateInfo this[int index]
		{
			get
			{
				return (DataAggregateInfo)base[index];
			}
		}
	}
}
