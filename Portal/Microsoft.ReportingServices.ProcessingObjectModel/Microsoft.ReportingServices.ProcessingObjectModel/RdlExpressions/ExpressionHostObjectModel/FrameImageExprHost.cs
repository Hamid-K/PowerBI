using System;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x02000077 RID: 119
	public abstract class FrameImageExprHost : BaseGaugeImageExprHost
	{
		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000261 RID: 609 RVA: 0x0000327C File Offset: 0x0000147C
		public virtual object HueColorExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000262 RID: 610 RVA: 0x0000327F File Offset: 0x0000147F
		public virtual object TransparencyExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000263 RID: 611 RVA: 0x00003282 File Offset: 0x00001482
		public virtual object ClipImageExpr
		{
			get
			{
				return null;
			}
		}
	}
}
