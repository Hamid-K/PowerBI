using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x02000027 RID: 39
	public class MemberReference
	{
		// Token: 0x060001B2 RID: 434 RVA: 0x0000810D File Offset: 0x0000630D
		internal MemberReference(MemberName memberName, int refID)
		{
			this.m_memberName = memberName;
			this.m_refID = refID;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00008123 File Offset: 0x00006323
		internal MemberName MemberName
		{
			get
			{
				return this.m_memberName;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x0000812B File Offset: 0x0000632B
		internal int RefID
		{
			get
			{
				return this.m_refID;
			}
		}

		// Token: 0x04000128 RID: 296
		private MemberName m_memberName;

		// Token: 0x04000129 RID: 297
		private int m_refID;
	}
}
