using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004A6 RID: 1190
	[Serializable]
	internal sealed class DataMemberList : HierarchyNodeList
	{
		// Token: 0x06003A88 RID: 14984 RVA: 0x000FE37B File Offset: 0x000FC57B
		public DataMemberList()
		{
		}

		// Token: 0x06003A89 RID: 14985 RVA: 0x000FE383 File Offset: 0x000FC583
		internal DataMemberList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x17001944 RID: 6468
		internal DataMember this[int index]
		{
			get
			{
				return (DataMember)base[index];
			}
		}
	}
}
