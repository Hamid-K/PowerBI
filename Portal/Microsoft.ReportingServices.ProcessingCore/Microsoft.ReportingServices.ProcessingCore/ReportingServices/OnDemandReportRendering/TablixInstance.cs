using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000366 RID: 870
	public sealed class TablixInstance : DataRegionInstance
	{
		// Token: 0x0600213A RID: 8506 RVA: 0x00080B7C File Offset: 0x0007ED7C
		internal TablixInstance(Tablix reportItemDef)
			: base(reportItemDef)
		{
			this.m_owner = reportItemDef;
		}

		// Token: 0x0600213B RID: 8507 RVA: 0x00080B8C File Offset: 0x0007ED8C
		private ReportSize GetOrEvaluateMarginProperty(ref ReportSize property, Tablix.MarginPosition marginPosition)
		{
			if (this.m_owner.IsOldSnapshot)
			{
				return null;
			}
			if (property == null)
			{
				property = new ReportSize(this.m_owner.TablixDef.EvaluateTablixMargin(this.ReportScopeInstance, marginPosition, this.m_reportElementDef.RenderingContext.OdpContext));
			}
			return property;
		}

		// Token: 0x170012C2 RID: 4802
		// (get) Token: 0x0600213C RID: 8508 RVA: 0x00080BDC File Offset: 0x0007EDDC
		public ReportSize TopMargin
		{
			get
			{
				return this.GetOrEvaluateMarginProperty(ref this.m_topMargin, Tablix.MarginPosition.TopMargin);
			}
		}

		// Token: 0x170012C3 RID: 4803
		// (get) Token: 0x0600213D RID: 8509 RVA: 0x00080BEB File Offset: 0x0007EDEB
		public ReportSize BottomMargin
		{
			get
			{
				return this.GetOrEvaluateMarginProperty(ref this.m_bottomMargin, Tablix.MarginPosition.BottomMargin);
			}
		}

		// Token: 0x170012C4 RID: 4804
		// (get) Token: 0x0600213E RID: 8510 RVA: 0x00080BFA File Offset: 0x0007EDFA
		public ReportSize LeftMargin
		{
			get
			{
				return this.GetOrEvaluateMarginProperty(ref this.m_leftMargin, Tablix.MarginPosition.LeftMargin);
			}
		}

		// Token: 0x170012C5 RID: 4805
		// (get) Token: 0x0600213F RID: 8511 RVA: 0x00080C09 File Offset: 0x0007EE09
		public ReportSize RightMargin
		{
			get
			{
				return this.GetOrEvaluateMarginProperty(ref this.m_rightMargin, Tablix.MarginPosition.RightMargin);
			}
		}

		// Token: 0x06002140 RID: 8512 RVA: 0x00080C18 File Offset: 0x0007EE18
		internal override void SetNewContext()
		{
			this.m_topMargin = (this.m_bottomMargin = (this.m_leftMargin = (this.m_rightMargin = null)));
			base.SetNewContext();
		}

		// Token: 0x040010B5 RID: 4277
		private Tablix m_owner;

		// Token: 0x040010B6 RID: 4278
		private ReportSize m_topMargin;

		// Token: 0x040010B7 RID: 4279
		private ReportSize m_bottomMargin;

		// Token: 0x040010B8 RID: 4280
		private ReportSize m_leftMargin;

		// Token: 0x040010B9 RID: 4281
		private ReportSize m_rightMargin;
	}
}
