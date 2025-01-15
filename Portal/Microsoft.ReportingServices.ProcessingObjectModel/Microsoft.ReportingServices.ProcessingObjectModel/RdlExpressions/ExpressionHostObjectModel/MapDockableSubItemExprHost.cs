using System;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x020000BC RID: 188
	public abstract class MapDockableSubItemExprHost : MapSubItemExprHost
	{
		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x00003A15 File Offset: 0x00001C15
		public virtual object MapPositionExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x00003A18 File Offset: 0x00001C18
		public virtual object DockOutsideViewportExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x00003A1B File Offset: 0x00001C1B
		public virtual object HiddenExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x00003A1E File Offset: 0x00001C1E
		public virtual object ToolTipExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0400013D RID: 317
		public ActionInfoExprHost ActionInfoHost;
	}
}
