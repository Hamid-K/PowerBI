using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel
{
	// Token: 0x02000048 RID: 72
	public abstract class CustomReportItemExprHost : DataRegionExprHost
	{
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00002B54 File Offset: 0x00000D54
		internal IList<DataGroupingExprHost> DataGroupingHostsRemotable
		{
			get
			{
				if (this.m_dataGroupingHostsRemotable == null && this.DataGroupingHosts != null)
				{
					this.m_dataGroupingHostsRemotable = new RemoteArrayWrapper<DataGroupingExprHost>(this.DataGroupingHosts);
				}
				return this.m_dataGroupingHostsRemotable;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00002B7D File Offset: 0x00000D7D
		internal IList<DataCellExprHost> DataCellHostsRemotable
		{
			get
			{
				if (this.m_dataCellHostsRemotable == null && this.DataCellHosts != null)
				{
					this.m_dataCellHostsRemotable = new RemoteArrayWrapper<DataCellExprHost>(this.DataCellHosts);
				}
				return this.m_dataCellHostsRemotable;
			}
		}

		// Token: 0x04000076 RID: 118
		protected DataGroupingExprHost[] DataGroupingHosts;

		// Token: 0x04000077 RID: 119
		[CLSCompliant(false)]
		protected IList<DataGroupingExprHost> m_dataGroupingHostsRemotable;

		// Token: 0x04000078 RID: 120
		protected DataCellExprHost[] DataCellHosts;

		// Token: 0x04000079 RID: 121
		[CLSCompliant(false)]
		protected IList<DataCellExprHost> m_dataCellHostsRemotable;
	}
}
