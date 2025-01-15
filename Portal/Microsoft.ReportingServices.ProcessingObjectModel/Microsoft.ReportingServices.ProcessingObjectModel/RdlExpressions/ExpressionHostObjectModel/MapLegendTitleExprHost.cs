using System;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x020000CA RID: 202
	public abstract class MapLegendTitleExprHost : StyleExprHost
	{
		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x00003B26 File Offset: 0x00001D26
		public virtual object CaptionExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x00003B29 File Offset: 0x00001D29
		public virtual object TitleSeparatorExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x00003B2C File Offset: 0x00001D2C
		public virtual object TitleSeparatorColorExpr
		{
			get
			{
				return null;
			}
		}
	}
}
