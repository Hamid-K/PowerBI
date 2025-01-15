using System;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x02000074 RID: 116
	public abstract class BackFrameExprHost : StyleExprHost
	{
		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0000324C File Offset: 0x0000144C
		public virtual object FrameStyleExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000257 RID: 599 RVA: 0x0000324F File Offset: 0x0000144F
		public virtual object FrameShapeExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000258 RID: 600 RVA: 0x00003252 File Offset: 0x00001452
		public virtual object FrameWidthExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000259 RID: 601 RVA: 0x00003255 File Offset: 0x00001455
		public virtual object GlassEffectExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x040000CB RID: 203
		public FrameBackgroundExprHost FrameBackgroundHost;

		// Token: 0x040000CC RID: 204
		public FrameImageExprHost FrameImageHost;
	}
}
