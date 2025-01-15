using System;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x02000075 RID: 117
	public abstract class BaseGaugeImageExprHost : ReportObjectModelProxy
	{
		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x0600025B RID: 603 RVA: 0x00003260 File Offset: 0x00001460
		public virtual object SourceExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x0600025C RID: 604 RVA: 0x00003263 File Offset: 0x00001463
		public virtual object ValueExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x0600025D RID: 605 RVA: 0x00003266 File Offset: 0x00001466
		public virtual object MIMETypeExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x0600025E RID: 606 RVA: 0x00003269 File Offset: 0x00001469
		public virtual object TransparentColorExpr
		{
			get
			{
				return null;
			}
		}
	}
}
