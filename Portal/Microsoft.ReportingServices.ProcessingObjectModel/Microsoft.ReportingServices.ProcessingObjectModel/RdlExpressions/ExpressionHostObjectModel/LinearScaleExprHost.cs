using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x02000087 RID: 135
	public abstract class LinearScaleExprHost : GaugeScaleExprHost
	{
		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x000033E0 File Offset: 0x000015E0
		internal IList<LinearPointerExprHost> LinearPointersHostsRemotable
		{
			get
			{
				return this.m_linearPointersHostsRemotable;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x000033E8 File Offset: 0x000015E8
		public virtual object StartMarginExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060002BA RID: 698 RVA: 0x000033EB File Offset: 0x000015EB
		public virtual object EndMarginExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x040000E0 RID: 224
		[CLSCompliant(false)]
		protected IList<LinearPointerExprHost> m_linearPointersHostsRemotable;
	}
}
