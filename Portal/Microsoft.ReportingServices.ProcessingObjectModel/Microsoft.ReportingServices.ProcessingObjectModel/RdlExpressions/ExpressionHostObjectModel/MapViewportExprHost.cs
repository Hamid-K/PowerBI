using System;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x020000C3 RID: 195
	public abstract class MapViewportExprHost : MapSubItemExprHost
	{
		// Token: 0x17000349 RID: 841
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x00003A82 File Offset: 0x00001C82
		public virtual object MapCoordinateSystemExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x00003A85 File Offset: 0x00001C85
		public virtual object MapProjectionExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x00003A88 File Offset: 0x00001C88
		public virtual object ProjectionCenterXExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x00003A8B File Offset: 0x00001C8B
		public virtual object ProjectionCenterYExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x00003A8E File Offset: 0x00001C8E
		public virtual object MaximumZoomExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x00003A91 File Offset: 0x00001C91
		public virtual object MinimumZoomExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x00003A94 File Offset: 0x00001C94
		public virtual object ContentMarginExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x00003A97 File Offset: 0x00001C97
		public virtual object GridUnderContentExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x00003A9A File Offset: 0x00001C9A
		public virtual object SimplificationResolutionExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04000141 RID: 321
		public MapLimitsExprHost MapLimitsHost;

		// Token: 0x04000142 RID: 322
		public MapViewExprHost MapViewHost;

		// Token: 0x04000143 RID: 323
		public MapGridLinesExprHost MapMeridiansHost;

		// Token: 0x04000144 RID: 324
		public MapGridLinesExprHost MapParallelsHost;
	}
}
