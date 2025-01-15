using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x0200008E RID: 142
	public abstract class RadialScaleExprHost : GaugeScaleExprHost
	{
		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x000034A2 File Offset: 0x000016A2
		internal IList<RadialPointerExprHost> RadialPointersHostsRemotable
		{
			get
			{
				return this.m_radialPointersHostsRemotable;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x000034AA File Offset: 0x000016AA
		public virtual object RadiusExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060002EA RID: 746 RVA: 0x000034AD File Offset: 0x000016AD
		public virtual object StartAngleExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060002EB RID: 747 RVA: 0x000034B0 File Offset: 0x000016B0
		public virtual object SweepAngleExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x040000EA RID: 234
		[CLSCompliant(false)]
		protected IList<RadialPointerExprHost> m_radialPointersHostsRemotable;
	}
}
