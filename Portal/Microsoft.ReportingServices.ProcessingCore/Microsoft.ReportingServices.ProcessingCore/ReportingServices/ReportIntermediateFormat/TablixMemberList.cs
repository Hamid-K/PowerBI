using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x0200051C RID: 1308
	[Serializable]
	internal sealed class TablixMemberList : HierarchyNodeList
	{
		// Token: 0x0600462F RID: 17967 RVA: 0x00126FE9 File Offset: 0x001251E9
		public TablixMemberList()
		{
		}

		// Token: 0x06004630 RID: 17968 RVA: 0x00126FF1 File Offset: 0x001251F1
		internal TablixMemberList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x17001D51 RID: 7505
		internal TablixMember this[int index]
		{
			get
			{
				return (TablixMember)base[index];
			}
		}
	}
}
