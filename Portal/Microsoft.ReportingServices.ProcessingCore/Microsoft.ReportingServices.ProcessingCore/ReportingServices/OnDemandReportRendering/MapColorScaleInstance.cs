using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000197 RID: 407
	public sealed class MapColorScaleInstance : MapDockableSubItemInstance
	{
		// Token: 0x0600107F RID: 4223 RVA: 0x0004611E File Offset: 0x0004431E
		internal MapColorScaleInstance(MapColorScale defObject)
			: base(defObject)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x06001080 RID: 4224 RVA: 0x00046130 File Offset: 0x00044330
		public ReportSize TickMarkLength
		{
			get
			{
				if (this.m_tickMarkLength == null)
				{
					this.m_tickMarkLength = new ReportSize(((MapColorScale)this.m_defObject.MapDockableSubItemDef).EvaluateTickMarkLength(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_tickMarkLength;
			}
		}

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x06001081 RID: 4225 RVA: 0x00046188 File Offset: 0x00044388
		public ReportColor ColorBarBorderColor
		{
			get
			{
				if (this.m_colorBarBorderColor == null)
				{
					this.m_colorBarBorderColor = new ReportColor(((MapColorScale)this.m_defObject.MapDockableSubItemDef).EvaluateColorBarBorderColor(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_colorBarBorderColor;
			}
		}

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x06001082 RID: 4226 RVA: 0x000461E0 File Offset: 0x000443E0
		public int LabelInterval
		{
			get
			{
				if (this.m_labelInterval == null)
				{
					this.m_labelInterval = new int?(((MapColorScale)this.m_defObject.MapDockableSubItemDef).EvaluateLabelInterval(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_labelInterval.Value;
			}
		}

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x06001083 RID: 4227 RVA: 0x00046240 File Offset: 0x00044440
		public string LabelFormat
		{
			get
			{
				if (this.m_labelFormat == null)
				{
					this.m_labelFormat = ((MapColorScale)this.m_defObject.MapDockableSubItemDef).EvaluateLabelFormat(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext);
				}
				return this.m_labelFormat;
			}
		}

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x06001084 RID: 4228 RVA: 0x00046294 File Offset: 0x00044494
		public MapLabelPlacement LabelPlacement
		{
			get
			{
				if (this.m_labelPlacement == null)
				{
					this.m_labelPlacement = new MapLabelPlacement?(((MapColorScale)this.m_defObject.MapDockableSubItemDef).EvaluateLabelPlacement(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_labelPlacement.Value;
			}
		}

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x06001085 RID: 4229 RVA: 0x000462F4 File Offset: 0x000444F4
		public MapLabelBehavior LabelBehavior
		{
			get
			{
				if (this.m_labelBehavior == null)
				{
					this.m_labelBehavior = new MapLabelBehavior?(((MapColorScale)this.m_defObject.MapDockableSubItemDef).EvaluateLabelBehavior(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_labelBehavior.Value;
			}
		}

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x06001086 RID: 4230 RVA: 0x00046354 File Offset: 0x00044554
		public bool HideEndLabels
		{
			get
			{
				if (this.m_hideEndLabels == null)
				{
					this.m_hideEndLabels = new bool?(((MapColorScale)this.m_defObject.MapDockableSubItemDef).EvaluateHideEndLabels(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_hideEndLabels.Value;
			}
		}

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x06001087 RID: 4231 RVA: 0x000463B4 File Offset: 0x000445B4
		public ReportColor RangeGapColor
		{
			get
			{
				if (this.m_rangeGapColor == null)
				{
					this.m_rangeGapColor = new ReportColor(((MapColorScale)this.m_defObject.MapDockableSubItemDef).EvaluateRangeGapColor(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_rangeGapColor;
			}
		}

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x06001088 RID: 4232 RVA: 0x0004640C File Offset: 0x0004460C
		public string NoDataText
		{
			get
			{
				if (this.m_noDataText == null)
				{
					this.m_noDataText = ((MapColorScale)this.m_defObject.MapDockableSubItemDef).EvaluateNoDataText(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext);
				}
				return this.m_noDataText;
			}
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x00046460 File Offset: 0x00044660
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
			this.m_tickMarkLength = null;
			this.m_colorBarBorderColor = null;
			this.m_labelInterval = null;
			this.m_labelFormat = null;
			this.m_labelPlacement = null;
			this.m_labelBehavior = null;
			this.m_hideEndLabels = null;
			this.m_rangeGapColor = null;
			this.m_noDataText = null;
		}

		// Token: 0x040007BE RID: 1982
		private MapColorScale m_defObject;

		// Token: 0x040007BF RID: 1983
		private ReportSize m_tickMarkLength;

		// Token: 0x040007C0 RID: 1984
		private ReportColor m_colorBarBorderColor;

		// Token: 0x040007C1 RID: 1985
		private int? m_labelInterval;

		// Token: 0x040007C2 RID: 1986
		private string m_labelFormat;

		// Token: 0x040007C3 RID: 1987
		private MapLabelPlacement? m_labelPlacement;

		// Token: 0x040007C4 RID: 1988
		private MapLabelBehavior? m_labelBehavior;

		// Token: 0x040007C5 RID: 1989
		private bool? m_hideEndLabels;

		// Token: 0x040007C6 RID: 1990
		private ReportColor m_rangeGapColor;

		// Token: 0x040007C7 RID: 1991
		private string m_noDataText;
	}
}
