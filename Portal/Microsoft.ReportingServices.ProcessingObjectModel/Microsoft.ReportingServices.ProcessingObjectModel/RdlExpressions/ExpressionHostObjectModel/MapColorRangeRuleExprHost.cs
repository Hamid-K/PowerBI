using System;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x020000CE RID: 206
	public abstract class MapColorRangeRuleExprHost : MapColorRuleExprHost
	{
		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x00003B72 File Offset: 0x00001D72
		public virtual object StartColorExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x00003B75 File Offset: 0x00001D75
		public virtual object MiddleColorExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x00003B78 File Offset: 0x00001D78
		public virtual object EndColorExpr
		{
			get
			{
				return null;
			}
		}
	}
}
