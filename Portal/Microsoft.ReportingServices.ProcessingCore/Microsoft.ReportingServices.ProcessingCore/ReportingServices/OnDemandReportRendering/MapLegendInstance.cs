using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200019B RID: 411
	public sealed class MapLegendInstance : MapDockableSubItemInstance
	{
		// Token: 0x06001097 RID: 4247 RVA: 0x000467C3 File Offset: 0x000449C3
		internal MapLegendInstance(MapLegend defObject)
			: base(defObject)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x06001098 RID: 4248 RVA: 0x000467D4 File Offset: 0x000449D4
		public MapLegendLayout Layout
		{
			get
			{
				if (this.m_layout == null)
				{
					this.m_layout = new MapLegendLayout?(((MapLegend)this.m_defObject.MapDockableSubItemDef).EvaluateLayout(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_layout.Value;
			}
		}

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x06001099 RID: 4249 RVA: 0x00046834 File Offset: 0x00044A34
		public bool AutoFitTextDisabled
		{
			get
			{
				if (this.m_autoFitTextDisabled == null)
				{
					this.m_autoFitTextDisabled = new bool?(((MapLegend)this.m_defObject.MapDockableSubItemDef).EvaluateAutoFitTextDisabled(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_autoFitTextDisabled.Value;
			}
		}

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x0600109A RID: 4250 RVA: 0x00046894 File Offset: 0x00044A94
		public ReportSize MinFontSize
		{
			get
			{
				if (this.m_minFontSize == null)
				{
					this.m_minFontSize = new ReportSize(((MapLegend)this.m_defObject.MapDockableSubItemDef).EvaluateMinFontSize(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_minFontSize;
			}
		}

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x0600109B RID: 4251 RVA: 0x000468EC File Offset: 0x00044AEC
		public bool InterlacedRows
		{
			get
			{
				if (this.m_interlacedRows == null)
				{
					this.m_interlacedRows = new bool?(((MapLegend)this.m_defObject.MapDockableSubItemDef).EvaluateInterlacedRows(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_interlacedRows.Value;
			}
		}

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x0600109C RID: 4252 RVA: 0x0004694C File Offset: 0x00044B4C
		public ReportColor InterlacedRowsColor
		{
			get
			{
				if (this.m_interlacedRowsColor == null)
				{
					this.m_interlacedRowsColor = new ReportColor(((MapLegend)this.m_defObject.MapDockableSubItemDef).EvaluateInterlacedRowsColor(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_interlacedRowsColor;
			}
		}

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x0600109D RID: 4253 RVA: 0x000469A4 File Offset: 0x00044BA4
		public bool EquallySpacedItems
		{
			get
			{
				if (this.m_equallySpacedItems == null)
				{
					this.m_equallySpacedItems = new bool?(((MapLegend)this.m_defObject.MapDockableSubItemDef).EvaluateEquallySpacedItems(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_equallySpacedItems.Value;
			}
		}

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x0600109E RID: 4254 RVA: 0x00046A04 File Offset: 0x00044C04
		public int TextWrapThreshold
		{
			get
			{
				if (this.m_textWrapThreshold == null)
				{
					this.m_textWrapThreshold = new int?(((MapLegend)this.m_defObject.MapDockableSubItemDef).EvaluateTextWrapThreshold(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_textWrapThreshold.Value;
			}
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x00046A64 File Offset: 0x00044C64
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
			this.m_layout = null;
			this.m_autoFitTextDisabled = null;
			this.m_minFontSize = null;
			this.m_interlacedRows = null;
			this.m_interlacedRowsColor = null;
			this.m_equallySpacedItems = null;
			this.m_textWrapThreshold = null;
		}

		// Token: 0x040007D4 RID: 2004
		private MapLegend m_defObject;

		// Token: 0x040007D5 RID: 2005
		private MapLegendLayout? m_layout;

		// Token: 0x040007D6 RID: 2006
		private bool? m_autoFitTextDisabled;

		// Token: 0x040007D7 RID: 2007
		private ReportSize m_minFontSize;

		// Token: 0x040007D8 RID: 2008
		private bool? m_interlacedRows;

		// Token: 0x040007D9 RID: 2009
		private ReportColor m_interlacedRowsColor;

		// Token: 0x040007DA RID: 2010
		private bool? m_equallySpacedItems;

		// Token: 0x040007DB RID: 2011
		private int? m_textWrapThreshold;
	}
}
