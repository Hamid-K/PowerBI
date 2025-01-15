using System;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x02000090 RID: 144
	public abstract class ScalePinExprHost : TickMarkStyleExprHost
	{
		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x000034E1 File Offset: 0x000016E1
		public virtual object LocationExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x000034E4 File Offset: 0x000016E4
		public virtual object EnableExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x040000EB RID: 235
		public PinLabelExprHost PinLabelHost;
	}
}
