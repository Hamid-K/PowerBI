using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel
{
	// Token: 0x02000035 RID: 53
	public abstract class DataRegionExprHost : ReportItemExprHost
	{
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00002924 File Offset: 0x00000B24
		public virtual object NoRowsExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00002927 File Offset: 0x00000B27
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

		// Token: 0x0400003E RID: 62
		protected FilterExprHost[] FilterHosts;

		// Token: 0x0400003F RID: 63
		[CLSCompliant(false)]
		protected IList<FilterExprHost> m_filterHostsRemotable;

		// Token: 0x04000040 RID: 64
		public IndexedExprHost UserSortExpressionsHost;
	}
}
