using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel
{
	// Token: 0x02000029 RID: 41
	public abstract class DataSetExprHost : ReportObjectModelProxy
	{
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x000026D8 File Offset: 0x000008D8
		internal IList<CalcFieldExprHost> FieldHostsRemotable
		{
			get
			{
				if (this.m_fieldHostsRemotable == null && this.FieldHosts != null)
				{
					this.m_fieldHostsRemotable = new RemoteArrayWrapper<CalcFieldExprHost>(this.FieldHosts);
				}
				return this.m_fieldHostsRemotable;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00002701 File Offset: 0x00000901
		public virtual object QueryCommandTextExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00002704 File Offset: 0x00000904
		internal IList<FilterExprHost> FilterHostsRemotable
		{
			get
			{
				if (this.m_filterHostsRemotable == null && this.FilterHosts != null)
				{
					this.m_filterHostsRemotable = new RemoteArrayWrapper<FilterExprHost>(this.FilterHosts);
				}
				return this.m_filterHostsRemotable;
			}
		}

		// Token: 0x0400002A RID: 42
		protected CalcFieldExprHost[] FieldHosts;

		// Token: 0x0400002B RID: 43
		[CLSCompliant(false)]
		protected IList<CalcFieldExprHost> m_fieldHostsRemotable;

		// Token: 0x0400002C RID: 44
		public IndexedExprHost QueryParametersHost;

		// Token: 0x0400002D RID: 45
		protected FilterExprHost[] FilterHosts;

		// Token: 0x0400002E RID: 46
		[CLSCompliant(false)]
		protected IList<FilterExprHost> m_filterHostsRemotable;

		// Token: 0x0400002F RID: 47
		public IndexedExprHost UserSortExpressionsHost;
	}
}
