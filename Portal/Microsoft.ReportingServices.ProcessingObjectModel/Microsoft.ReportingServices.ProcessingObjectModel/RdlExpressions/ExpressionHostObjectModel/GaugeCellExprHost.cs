using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x0200006E RID: 110
	public abstract class GaugeCellExprHost : DataCellExprHost
	{
		// Token: 0x1700018D RID: 397
		// (get) Token: 0x0600023A RID: 570 RVA: 0x00003199 File Offset: 0x00001399
		internal IList<GaugeInputValueExprHost> GaugeInputValueHostsRemotable
		{
			get
			{
				return this.m_gaugeInputValueHostsRemotable;
			}
		}

		// Token: 0x040000BB RID: 187
		[CLSCompliant(false)]
		protected IList<GaugeInputValueExprHost> m_gaugeInputValueHostsRemotable;
	}
}
