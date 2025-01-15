using System;

namespace Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel
{
	// Token: 0x02000026 RID: 38
	public abstract class ReportParamExprHost : IndexedExprHost
	{
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060000BD RID: 189 RVA: 0x000026B7 File Offset: 0x000008B7
		public virtual object ValidationExpressionExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04000028 RID: 40
		public IndexedExprHost ValidValuesHost;

		// Token: 0x04000029 RID: 41
		public IndexedExprHost ValidValueLabelsHost;
	}
}
