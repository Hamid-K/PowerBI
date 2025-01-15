using System;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x020000D2 RID: 210
	public abstract class MapSizeRuleExprHost : MapAppearanceRuleExprHost
	{
		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x00003B9E File Offset: 0x00001D9E
		public virtual object StartSizeExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x00003BA1 File Offset: 0x00001DA1
		public virtual object EndSizeExpr
		{
			get
			{
				return null;
			}
		}
	}
}
