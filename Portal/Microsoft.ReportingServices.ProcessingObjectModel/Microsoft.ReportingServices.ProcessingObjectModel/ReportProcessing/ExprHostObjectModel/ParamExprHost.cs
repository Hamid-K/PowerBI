using System;

namespace Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel
{
	// Token: 0x0200002B RID: 43
	public abstract class ParamExprHost : ReportObjectModelProxy
	{
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00002740 File Offset: 0x00000940
		public virtual object ValueExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00002743 File Offset: 0x00000943
		public virtual object OmitExpr
		{
			get
			{
				return null;
			}
		}
	}
}
