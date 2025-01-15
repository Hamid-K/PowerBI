using System;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x02000068 RID: 104
	public abstract class ChartMemberExprHost : MemberNodeExprHost<ChartMemberExprHost>
	{
		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000219 RID: 537 RVA: 0x00003109 File Offset: 0x00001309
		public virtual object MemberLabelExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x040000B1 RID: 177
		public ChartSeriesExprHost ChartSeriesHost;
	}
}
