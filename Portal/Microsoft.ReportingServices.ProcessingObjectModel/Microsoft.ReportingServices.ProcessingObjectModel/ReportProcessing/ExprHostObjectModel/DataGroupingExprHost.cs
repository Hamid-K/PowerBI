using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel
{
	// Token: 0x02000047 RID: 71
	public abstract class DataGroupingExprHost : ReportObjectModelProxy
	{
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00002AFA File Offset: 0x00000CFA
		internal IList<DataValueExprHost> CustomPropertyHostsRemotable
		{
			get
			{
				if (this.m_customPropertyHostsRemotable == null && this.CustomPropertyHosts != null)
				{
					this.m_customPropertyHostsRemotable = new RemoteArrayWrapper<DataValueExprHost>(this.CustomPropertyHosts);
				}
				return this.m_customPropertyHostsRemotable;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00002B23 File Offset: 0x00000D23
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

		// Token: 0x04000070 RID: 112
		public GroupingExprHost GroupingHost;

		// Token: 0x04000071 RID: 113
		public SortingExprHost SortingHost;

		// Token: 0x04000072 RID: 114
		protected DataValueExprHost[] CustomPropertyHosts;

		// Token: 0x04000073 RID: 115
		[CLSCompliant(false)]
		protected IList<DataValueExprHost> m_customPropertyHostsRemotable;

		// Token: 0x04000074 RID: 116
		protected DataGroupingExprHost[] DataGroupingHosts;

		// Token: 0x04000075 RID: 117
		[CLSCompliant(false)]
		protected IList<DataGroupingExprHost> m_dataGroupingHostsRemotable;
	}
}
