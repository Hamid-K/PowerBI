using System;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x020000BD RID: 189
	public abstract class MapSubItemExprHost : StyleExprHost
	{
		// Token: 0x1700033D RID: 829
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x00003A29 File Offset: 0x00001C29
		public virtual object LeftMarginExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x00003A2C File Offset: 0x00001C2C
		public virtual object RightMarginExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x00003A2F File Offset: 0x00001C2F
		public virtual object TopMarginExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x00003A32 File Offset: 0x00001C32
		public virtual object BottomMarginExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x00003A35 File Offset: 0x00001C35
		public virtual object ZIndexExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0400013E RID: 318
		public MapLocationExprHost MapLocationHost;

		// Token: 0x0400013F RID: 319
		public MapSizeExprHost MapSizeHost;
	}
}
