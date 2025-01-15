using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020003CD RID: 973
	internal sealed class DataShapeMemberList : HierarchyNodeList
	{
		// Token: 0x06002768 RID: 10088 RVA: 0x000BA9BE File Offset: 0x000B8BBE
		public DataShapeMemberList()
		{
		}

		// Token: 0x06002769 RID: 10089 RVA: 0x000BA9C6 File Offset: 0x000B8BC6
		internal DataShapeMemberList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x17001415 RID: 5141
		internal DataShapeMember this[int index]
		{
			get
			{
				return (DataShapeMember)base[index];
			}
		}

		// Token: 0x0600276B RID: 10091 RVA: 0x000BA9E0 File Offset: 0x000B8BE0
		internal DataShapeMember GetSingleDynamicMemberOrNull()
		{
			if (this.m_singleDynamicMember == null)
			{
				for (int i = 0; i < this.Count; i++)
				{
					DataShapeMember dataShapeMember = this[i];
					if (!dataShapeMember.IsStatic)
					{
						this.m_singleDynamicMember = dataShapeMember;
						break;
					}
				}
			}
			return this.m_singleDynamicMember;
		}

		// Token: 0x04001682 RID: 5762
		private DataShapeMember m_singleDynamicMember;
	}
}
