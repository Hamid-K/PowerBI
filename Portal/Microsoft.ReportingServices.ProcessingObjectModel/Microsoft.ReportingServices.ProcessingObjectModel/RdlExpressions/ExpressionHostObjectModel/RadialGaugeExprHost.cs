using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x0200008C RID: 140
	public abstract class RadialGaugeExprHost : GaugeExprHost
	{
		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x0000347E File Offset: 0x0000167E
		internal IList<RadialScaleExprHost> RadialScalesHostsRemotable
		{
			get
			{
				return this.m_radialScalesHostsRemotable;
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x00003486 File Offset: 0x00001686
		public virtual object PivotXExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x00003489 File Offset: 0x00001689
		public virtual object PivotYExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x040000E8 RID: 232
		[CLSCompliant(false)]
		protected IList<RadialScaleExprHost> m_radialScalesHostsRemotable;
	}
}
