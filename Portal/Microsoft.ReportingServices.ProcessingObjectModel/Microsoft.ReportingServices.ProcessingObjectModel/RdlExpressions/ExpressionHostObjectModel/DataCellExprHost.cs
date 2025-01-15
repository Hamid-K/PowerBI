using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x0200006C RID: 108
	public abstract class DataCellExprHost : CellExprHost
	{
		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000220 RID: 544 RVA: 0x00003137 File Offset: 0x00001337
		internal IList<DataValueExprHost> DataValueHostsRemotable
		{
			get
			{
				return this.m_dataValueHostsRemotable;
			}
		}

		// Token: 0x040000B4 RID: 180
		[CLSCompliant(false)]
		protected IList<DataValueExprHost> m_dataValueHostsRemotable;
	}
}
