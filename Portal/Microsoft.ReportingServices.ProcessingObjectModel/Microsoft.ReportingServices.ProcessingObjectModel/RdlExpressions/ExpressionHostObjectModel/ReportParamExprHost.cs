using System;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x02000054 RID: 84
	public abstract class ReportParamExprHost : IndexedExprHost
	{
		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00002F04 File Offset: 0x00001104
		public virtual object PromptExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00002F07 File Offset: 0x00001107
		public virtual object ValidationExpressionExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04000095 RID: 149
		public IndexedExprHost ValidValuesHost;

		// Token: 0x04000096 RID: 150
		public IndexedExprHost ValidValueLabelsHost;
	}
}
