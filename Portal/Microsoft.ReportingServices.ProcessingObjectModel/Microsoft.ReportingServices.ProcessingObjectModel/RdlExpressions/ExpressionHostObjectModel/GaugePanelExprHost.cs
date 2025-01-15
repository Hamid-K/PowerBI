using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x02000072 RID: 114
	public abstract class GaugePanelExprHost : DataRegionExprHost<GaugeMemberExprHost, GaugeCellExprHost>
	{
		// Token: 0x17000199 RID: 409
		// (get) Token: 0x0600024A RID: 586 RVA: 0x00003200 File Offset: 0x00001400
		internal IList<LinearGaugeExprHost> LinearGaugesHostsRemotable
		{
			get
			{
				return this.m_linearGaugesHostsRemotable;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x0600024B RID: 587 RVA: 0x00003208 File Offset: 0x00001408
		internal IList<RadialGaugeExprHost> RadialGaugesHostsRemotable
		{
			get
			{
				return this.m_radialGaugesHostsRemotable;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600024C RID: 588 RVA: 0x00003210 File Offset: 0x00001410
		internal IList<NumericIndicatorExprHost> NumericIndicatorsHostsRemotable
		{
			get
			{
				return this.m_numericIndicatorsHostsRemotable;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600024D RID: 589 RVA: 0x00003218 File Offset: 0x00001418
		internal IList<StateIndicatorExprHost> StateIndicatorsHostsRemotable
		{
			get
			{
				return this.m_stateIndicatorsHostsRemotable;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600024E RID: 590 RVA: 0x00003220 File Offset: 0x00001420
		internal IList<GaugeImageExprHost> GaugeImagesHostsRemotable
		{
			get
			{
				return this.m_gaugeImagesHostsRemotable;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x0600024F RID: 591 RVA: 0x00003228 File Offset: 0x00001428
		internal IList<GaugeLabelExprHost> GaugeLabelsHostsRemotable
		{
			get
			{
				return this.m_gaugeLabelsHostsRemotable;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000250 RID: 592 RVA: 0x00003230 File Offset: 0x00001430
		public virtual object AntiAliasingExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000251 RID: 593 RVA: 0x00003233 File Offset: 0x00001433
		public virtual object AutoLayoutExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000252 RID: 594 RVA: 0x00003236 File Offset: 0x00001436
		public virtual object ShadowIntensityExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000253 RID: 595 RVA: 0x00003239 File Offset: 0x00001439
		public virtual object TextAntiAliasingQualityExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x040000C3 RID: 195
		[CLSCompliant(false)]
		protected IList<LinearGaugeExprHost> m_linearGaugesHostsRemotable;

		// Token: 0x040000C4 RID: 196
		[CLSCompliant(false)]
		protected IList<RadialGaugeExprHost> m_radialGaugesHostsRemotable;

		// Token: 0x040000C5 RID: 197
		[CLSCompliant(false)]
		protected IList<NumericIndicatorExprHost> m_numericIndicatorsHostsRemotable;

		// Token: 0x040000C6 RID: 198
		[CLSCompliant(false)]
		protected IList<StateIndicatorExprHost> m_stateIndicatorsHostsRemotable;

		// Token: 0x040000C7 RID: 199
		[CLSCompliant(false)]
		protected IList<GaugeImageExprHost> m_gaugeImagesHostsRemotable;

		// Token: 0x040000C8 RID: 200
		[CLSCompliant(false)]
		protected IList<GaugeLabelExprHost> m_gaugeLabelsHostsRemotable;

		// Token: 0x040000C9 RID: 201
		public BackFrameExprHost BackFrameHost;

		// Token: 0x040000CA RID: 202
		public TopImageExprHost TopImageHost;
	}
}
