using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006AA RID: 1706
	[Serializable]
	internal sealed class GroupingList : ArrayList
	{
		// Token: 0x06005C94 RID: 23700 RVA: 0x00179AD2 File Offset: 0x00177CD2
		internal GroupingList()
		{
		}

		// Token: 0x06005C95 RID: 23701 RVA: 0x00179ADA File Offset: 0x00177CDA
		internal GroupingList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x17002084 RID: 8324
		internal Grouping this[int index]
		{
			get
			{
				return (Grouping)base[index];
			}
		}

		// Token: 0x17002085 RID: 8325
		// (get) Token: 0x06005C97 RID: 23703 RVA: 0x00179AF1 File Offset: 0x00177CF1
		internal Grouping LastEntry
		{
			get
			{
				if (this.Count == 0)
				{
					return null;
				}
				return this[this.Count - 1];
			}
		}

		// Token: 0x06005C98 RID: 23704 RVA: 0x00179B0C File Offset: 0x00177D0C
		internal new GroupingList Clone()
		{
			int count = this.Count;
			GroupingList groupingList = new GroupingList(count);
			for (int i = 0; i < count; i++)
			{
				groupingList.Add(this[i]);
			}
			return groupingList;
		}
	}
}
