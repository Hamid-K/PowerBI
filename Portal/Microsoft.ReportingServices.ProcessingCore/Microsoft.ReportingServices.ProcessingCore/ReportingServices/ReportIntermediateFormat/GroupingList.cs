using System;
using System.Collections;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020003BA RID: 954
	[Serializable]
	public sealed class GroupingList : ArrayList
	{
		// Token: 0x060026B4 RID: 9908 RVA: 0x000B974F File Offset: 0x000B794F
		public GroupingList()
		{
		}

		// Token: 0x060026B5 RID: 9909 RVA: 0x000B9757 File Offset: 0x000B7957
		internal GroupingList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x170013E1 RID: 5089
		internal Grouping this[int index]
		{
			get
			{
				return (Grouping)base[index];
			}
		}

		// Token: 0x170013E2 RID: 5090
		// (get) Token: 0x060026B7 RID: 9911 RVA: 0x000B976E File Offset: 0x000B796E
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

		// Token: 0x060026B8 RID: 9912 RVA: 0x000B9788 File Offset: 0x000B7988
		internal object PublishClone(AutomaticSubtotalContext context, ReportHierarchyNode owner)
		{
			int count = this.Count;
			GroupingList groupingList = new GroupingList(count);
			for (int i = 0; i < count; i++)
			{
				groupingList.Add(this[i].PublishClone(context, owner));
			}
			return groupingList;
		}

		// Token: 0x060026B9 RID: 9913 RVA: 0x000B97C8 File Offset: 0x000B79C8
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
