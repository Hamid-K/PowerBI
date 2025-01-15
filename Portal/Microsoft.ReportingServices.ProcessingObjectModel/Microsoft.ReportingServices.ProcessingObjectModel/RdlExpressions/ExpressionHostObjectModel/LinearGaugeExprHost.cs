using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x02000084 RID: 132
	public abstract class LinearGaugeExprHost : GaugeExprHost
	{
		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x000033B4 File Offset: 0x000015B4
		internal IList<LinearScaleExprHost> LinearScalesHostsRemotable
		{
			get
			{
				return this.m_linearScalesHostsRemotable;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x000033BC File Offset: 0x000015BC
		public virtual object OrientationExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x040000DE RID: 222
		[CLSCompliant(false)]
		protected IList<LinearScaleExprHost> m_linearScalesHostsRemotable;
	}
}
