using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200025B RID: 603
	public sealed class ChartMarkerInstance : BaseInstance
	{
		// Token: 0x06001783 RID: 6019 RVA: 0x0005F8A9 File Offset: 0x0005DAA9
		internal ChartMarkerInstance(ChartMarker markerDef)
			: base(markerDef.ReportScope)
		{
			this.m_markerDef = markerDef;
		}

		// Token: 0x17000D3E RID: 3390
		// (get) Token: 0x06001784 RID: 6020 RVA: 0x0005F8BE File Offset: 0x0005DABE
		public StyleInstance Style
		{
			get
			{
				if (this.m_style == null)
				{
					this.m_style = new StyleInstance(this.m_markerDef, this.m_markerDef.ReportScope, this.m_markerDef.ChartDef.RenderingContext);
				}
				return this.m_style;
			}
		}

		// Token: 0x17000D3F RID: 3391
		// (get) Token: 0x06001785 RID: 6021 RVA: 0x0005F8FC File Offset: 0x0005DAFC
		public ReportSize Size
		{
			get
			{
				if (this.m_size == null && !this.m_markerDef.ChartDef.IsOldSnapshot)
				{
					this.m_size = new ReportSize(this.m_markerDef.MarkerDef.EvaluateChartMarkerSize(this.ReportScopeInstance, this.m_markerDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_size;
			}
		}

		// Token: 0x17000D40 RID: 3392
		// (get) Token: 0x06001786 RID: 6022 RVA: 0x0005F960 File Offset: 0x0005DB60
		public ChartMarkerTypes Type
		{
			get
			{
				if (this.m_type == null && !this.m_markerDef.ChartDef.IsOldSnapshot)
				{
					this.m_type = new ChartMarkerTypes?(this.m_markerDef.MarkerDef.EvaluateChartMarkerType(this.ReportScopeInstance, this.m_markerDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_type.Value;
			}
		}

		// Token: 0x06001787 RID: 6023 RVA: 0x0005F9CD File Offset: 0x0005DBCD
		protected override void ResetInstanceCache()
		{
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
			this.m_type = null;
			this.m_size = null;
		}

		// Token: 0x04000BA6 RID: 2982
		private ChartMarker m_markerDef;

		// Token: 0x04000BA7 RID: 2983
		private StyleInstance m_style;

		// Token: 0x04000BA8 RID: 2984
		private ReportSize m_size;

		// Token: 0x04000BA9 RID: 2985
		private ChartMarkerTypes? m_type;
	}
}
