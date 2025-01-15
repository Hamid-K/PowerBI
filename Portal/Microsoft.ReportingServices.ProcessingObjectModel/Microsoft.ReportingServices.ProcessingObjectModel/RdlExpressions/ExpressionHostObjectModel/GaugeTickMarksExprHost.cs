using System;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x02000085 RID: 133
	public abstract class GaugeTickMarksExprHost : TickMarkStyleExprHost
	{
		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x000033C7 File Offset: 0x000015C7
		public virtual object IntervalExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x000033CA File Offset: 0x000015CA
		public virtual object IntervalOffsetExpr
		{
			get
			{
				return null;
			}
		}
	}
}
