using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel
{
	// Token: 0x02000046 RID: 70
	public abstract class DataCellExprHost : ReportObjectModelProxy
	{
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00002AC9 File Offset: 0x00000CC9
		internal IList<DataValueExprHost> DataValueHostsRemotable
		{
			get
			{
				if (this.m_dataValueHostsRemotable == null && this.DataValueHosts != null)
				{
					this.m_dataValueHostsRemotable = new RemoteArrayWrapper<DataValueExprHost>(this.DataValueHosts);
				}
				return this.m_dataValueHostsRemotable;
			}
		}

		// Token: 0x0400006E RID: 110
		protected DataValueExprHost[] DataValueHosts;

		// Token: 0x0400006F RID: 111
		[CLSCompliant(false)]
		protected IList<DataValueExprHost> m_dataValueHostsRemotable;
	}
}
