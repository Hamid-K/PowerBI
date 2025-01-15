using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000480 RID: 1152
	[Serializable]
	internal sealed class ChartMemberList : HierarchyNodeList
	{
		// Token: 0x0600357A RID: 13690 RVA: 0x000EA94F File Offset: 0x000E8B4F
		public ChartMemberList()
		{
		}

		// Token: 0x0600357B RID: 13691 RVA: 0x000EA957 File Offset: 0x000E8B57
		internal ChartMemberList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x170017BD RID: 6077
		internal ChartMember this[int index]
		{
			get
			{
				return (ChartMember)base[index];
			}
		}
	}
}
