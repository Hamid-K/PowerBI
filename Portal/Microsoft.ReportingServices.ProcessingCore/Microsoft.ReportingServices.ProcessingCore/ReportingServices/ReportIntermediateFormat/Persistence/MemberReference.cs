using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x02000545 RID: 1349
	public class MemberReference
	{
		// Token: 0x0600499E RID: 18846 RVA: 0x0013732C File Offset: 0x0013552C
		internal MemberReference(MemberName memberName, int refID)
		{
			this.m_memberName = memberName;
			this.m_refID = refID;
		}

		// Token: 0x17001DDB RID: 7643
		// (get) Token: 0x0600499F RID: 18847 RVA: 0x00137342 File Offset: 0x00135542
		internal MemberName MemberName
		{
			get
			{
				return this.m_memberName;
			}
		}

		// Token: 0x17001DDC RID: 7644
		// (get) Token: 0x060049A0 RID: 18848 RVA: 0x0013734A File Offset: 0x0013554A
		internal int RefID
		{
			get
			{
				return this.m_refID;
			}
		}

		// Token: 0x040020A8 RID: 8360
		private MemberName m_memberName;

		// Token: 0x040020A9 RID: 8361
		private int m_refID;
	}
}
