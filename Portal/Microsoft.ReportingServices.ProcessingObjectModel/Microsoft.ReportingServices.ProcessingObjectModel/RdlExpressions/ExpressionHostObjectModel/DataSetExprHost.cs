using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x02000057 RID: 87
	public abstract class DataSetExprHost : ReportObjectModelProxy
	{
		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00002F28 File Offset: 0x00001128
		internal IList<CalcFieldExprHost> FieldHostsRemotable
		{
			get
			{
				return this.m_fieldHostsRemotable;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00002F30 File Offset: 0x00001130
		internal IList<JoinConditionExprHost> JoinConditionExprHostsRemotable
		{
			get
			{
				return this.m_joinConditionExprHostsRemotable;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00002F38 File Offset: 0x00001138
		public virtual object QueryCommandTextExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00002F3B File Offset: 0x0000113B
		internal IList<FilterExprHost> FilterHostsRemotable
		{
			get
			{
				return this.m_filterHostsRemotable;
			}
		}

		// Token: 0x04000097 RID: 151
		[CLSCompliant(false)]
		protected IList<CalcFieldExprHost> m_fieldHostsRemotable;

		// Token: 0x04000098 RID: 152
		[CLSCompliant(false)]
		protected IList<JoinConditionExprHost> m_joinConditionExprHostsRemotable;

		// Token: 0x04000099 RID: 153
		public IndexedExprHost QueryParametersHost;

		// Token: 0x0400009A RID: 154
		[CLSCompliant(false)]
		protected IList<FilterExprHost> m_filterHostsRemotable;

		// Token: 0x0400009B RID: 155
		public IndexedExprHost UserSortExpressionsHost;
	}
}
