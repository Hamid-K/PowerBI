using System;

namespace Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel
{
	// Token: 0x02000032 RID: 50
	public abstract class ImageExprHost : ReportItemExprHost
	{
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600010E RID: 270 RVA: 0x000028B1 File Offset: 0x00000AB1
		public virtual object ValueExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600010F RID: 271 RVA: 0x000028B4 File Offset: 0x00000AB4
		public virtual object MIMETypeExpr
		{
			get
			{
				return null;
			}
		}
	}
}
