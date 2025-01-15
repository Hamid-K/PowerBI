using System;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x02000097 RID: 151
	public abstract class ChartTitleBaseExprHost : StyleExprHost
	{
		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000329 RID: 809 RVA: 0x000035BF File Offset: 0x000017BF
		public virtual object CaptionExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x0600032A RID: 810 RVA: 0x000035C2 File Offset: 0x000017C2
		public virtual object ChartTitlePositionExpr
		{
			get
			{
				return null;
			}
		}
	}
}
