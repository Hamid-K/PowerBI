using System;

namespace Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel
{
	// Token: 0x02000039 RID: 57
	public abstract class DynamicGroupExprHost : ReportObjectModelProxy, IVisibilityHiddenExprHost
	{
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600011F RID: 287 RVA: 0x000029C5 File Offset: 0x00000BC5
		public virtual object VisibilityHiddenExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0400004A RID: 74
		public GroupingExprHost GroupingHost;

		// Token: 0x0400004B RID: 75
		public SortingExprHost SortingHost;

		// Token: 0x0400004C RID: 76
		public DynamicGroupExprHost SubGroupHost;
	}
}
