using System;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x0200008D RID: 141
	public abstract class RadialPointerExprHost : GaugePointerExprHost
	{
		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x00003494 File Offset: 0x00001694
		public virtual object TypeExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x00003497 File Offset: 0x00001697
		public virtual object NeedleStyleExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x040000E9 RID: 233
		public PointerCapExprHost PointerCapHost;
	}
}
