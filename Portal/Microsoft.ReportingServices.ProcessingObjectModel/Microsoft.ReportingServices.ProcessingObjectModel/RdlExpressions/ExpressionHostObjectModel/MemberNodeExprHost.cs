using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x02000066 RID: 102
	public abstract class MemberNodeExprHost<TMemberType> : ReportObjectModelProxy, IMemberNode where TMemberType : IMemberNode
	{
		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000212 RID: 530 RVA: 0x000030D1 File Offset: 0x000012D1
		GroupExprHost IMemberNode.GroupHost
		{
			get
			{
				return this.m_groupHost;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000213 RID: 531 RVA: 0x000030D9 File Offset: 0x000012D9
		SortExprHost IMemberNode.SortHost
		{
			get
			{
				return this.m_sortHost;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000214 RID: 532 RVA: 0x000030E1 File Offset: 0x000012E1
		IList<DataValueExprHost> IMemberNode.CustomPropertyHostsRemotable
		{
			get
			{
				return this.m_customPropertyHostsRemotable;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000215 RID: 533 RVA: 0x000030E9 File Offset: 0x000012E9
		IList<IMemberNode> IMemberNode.MemberTreeHostsRemotable
		{
			get
			{
				return this.m_memberTreeHostsRemotable;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000216 RID: 534 RVA: 0x000030F1 File Offset: 0x000012F1
		IList<JoinConditionExprHost> IMemberNode.JoinConditionExprHostsRemotable
		{
			get
			{
				return this.m_joinConditionExprHostsRemotable;
			}
		}

		// Token: 0x040000AC RID: 172
		protected GroupExprHost m_groupHost;

		// Token: 0x040000AD RID: 173
		protected SortExprHost m_sortHost;

		// Token: 0x040000AE RID: 174
		[CLSCompliant(false)]
		protected IList<DataValueExprHost> m_customPropertyHostsRemotable;

		// Token: 0x040000AF RID: 175
		[CLSCompliant(false)]
		protected IList<IMemberNode> m_memberTreeHostsRemotable;

		// Token: 0x040000B0 RID: 176
		[CLSCompliant(false)]
		protected IList<JoinConditionExprHost> m_joinConditionExprHostsRemotable;
	}
}
