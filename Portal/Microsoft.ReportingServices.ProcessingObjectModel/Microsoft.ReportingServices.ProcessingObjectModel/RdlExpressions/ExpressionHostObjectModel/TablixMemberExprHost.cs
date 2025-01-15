using System;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x02000069 RID: 105
	public abstract class TablixMemberExprHost : MemberNodeExprHost<TablixMemberExprHost>, IVisibilityHiddenExprHost
	{
		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600021B RID: 539 RVA: 0x00003114 File Offset: 0x00001314
		public virtual object VisibilityHiddenExpr
		{
			get
			{
				return null;
			}
		}
	}
}
