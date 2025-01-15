using System;

namespace Microsoft.ReportingServices.ReportProcessing.Persistence
{
	// Token: 0x020007A5 RID: 1957
	internal sealed class Declaration
	{
		// Token: 0x06006C75 RID: 27765 RVA: 0x001B79DF File Offset: 0x001B5BDF
		internal Declaration(ObjectType baseType, MemberInfoList members)
		{
			this.m_baseType = baseType;
			Global.Tracer.Assert(members != null);
			this.m_members = members;
		}

		// Token: 0x170025C0 RID: 9664
		// (get) Token: 0x06006C76 RID: 27766 RVA: 0x001B7A03 File Offset: 0x001B5C03
		internal ObjectType BaseType
		{
			get
			{
				return this.m_baseType;
			}
		}

		// Token: 0x170025C1 RID: 9665
		// (get) Token: 0x06006C77 RID: 27767 RVA: 0x001B7A0B File Offset: 0x001B5C0B
		internal MemberInfoList Members
		{
			get
			{
				return this.m_members;
			}
		}

		// Token: 0x04003969 RID: 14697
		private ObjectType m_baseType;

		// Token: 0x0400396A RID: 14698
		private MemberInfoList m_members;
	}
}
