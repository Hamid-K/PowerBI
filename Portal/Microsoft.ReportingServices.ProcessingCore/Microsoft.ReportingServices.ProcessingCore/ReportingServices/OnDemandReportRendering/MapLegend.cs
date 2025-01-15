using System;
using System.Drawing;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000193 RID: 403
	public sealed class MapLegend : MapDockableSubItem
	{
		// Token: 0x0600105A RID: 4186 RVA: 0x00045827 File Offset: 0x00043A27
		internal MapLegend(MapLegend defObject, Map map)
			: base(defObject, map)
		{
		}

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x0600105B RID: 4187 RVA: 0x00045831 File Offset: 0x00043A31
		public string Name
		{
			get
			{
				return this.MapLegendDef.Name;
			}
		}

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x0600105C RID: 4188 RVA: 0x00045840 File Offset: 0x00043A40
		public ReportEnumProperty<MapLegendLayout> Layout
		{
			get
			{
				if (this.m_layout == null && this.MapLegendDef.Layout != null)
				{
					this.m_layout = new ReportEnumProperty<MapLegendLayout>(this.MapLegendDef.Layout.IsExpression, this.MapLegendDef.Layout.OriginalText, EnumTranslator.TranslateMapLegendLayout(this.MapLegendDef.Layout.StringValue, null));
				}
				return this.m_layout;
			}
		}

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x0600105D RID: 4189 RVA: 0x000458A9 File Offset: 0x00043AA9
		public MapLegendTitle MapLegendTitle
		{
			get
			{
				if (this.m_mapLegendTitle == null && this.MapLegendDef.MapLegendTitle != null)
				{
					this.m_mapLegendTitle = new MapLegendTitle(this.MapLegendDef.MapLegendTitle, this.m_map);
				}
				return this.m_mapLegendTitle;
			}
		}

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x0600105E RID: 4190 RVA: 0x000458E2 File Offset: 0x00043AE2
		public ReportBoolProperty AutoFitTextDisabled
		{
			get
			{
				if (this.m_autoFitTextDisabled == null && this.MapLegendDef.AutoFitTextDisabled != null)
				{
					this.m_autoFitTextDisabled = new ReportBoolProperty(this.MapLegendDef.AutoFitTextDisabled);
				}
				return this.m_autoFitTextDisabled;
			}
		}

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x0600105F RID: 4191 RVA: 0x00045915 File Offset: 0x00043B15
		public ReportSizeProperty MinFontSize
		{
			get
			{
				if (this.m_minFontSize == null && this.MapLegendDef.MinFontSize != null)
				{
					this.m_minFontSize = new ReportSizeProperty(this.MapLegendDef.MinFontSize);
				}
				return this.m_minFontSize;
			}
		}

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x06001060 RID: 4192 RVA: 0x00045948 File Offset: 0x00043B48
		public ReportBoolProperty InterlacedRows
		{
			get
			{
				if (this.m_interlacedRows == null && this.MapLegendDef.InterlacedRows != null)
				{
					this.m_interlacedRows = new ReportBoolProperty(this.MapLegendDef.InterlacedRows);
				}
				return this.m_interlacedRows;
			}
		}

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x06001061 RID: 4193 RVA: 0x0004597C File Offset: 0x00043B7C
		public ReportColorProperty InterlacedRowsColor
		{
			get
			{
				if (this.m_interlacedRowsColor == null && this.MapLegendDef.InterlacedRowsColor != null)
				{
					ExpressionInfo interlacedRowsColor = this.MapLegendDef.InterlacedRowsColor;
					if (interlacedRowsColor != null)
					{
						this.m_interlacedRowsColor = new ReportColorProperty(interlacedRowsColor.IsExpression, this.MapLegendDef.InterlacedRowsColor.OriginalText, interlacedRowsColor.IsExpression ? null : new ReportColor(interlacedRowsColor.StringValue.Trim(), true), interlacedRowsColor.IsExpression ? new ReportColor("", Color.Empty, true) : null);
					}
				}
				return this.m_interlacedRowsColor;
			}
		}

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x06001062 RID: 4194 RVA: 0x00045A0B File Offset: 0x00043C0B
		public ReportBoolProperty EquallySpacedItems
		{
			get
			{
				if (this.m_equallySpacedItems == null && this.MapLegendDef.EquallySpacedItems != null)
				{
					this.m_equallySpacedItems = new ReportBoolProperty(this.MapLegendDef.EquallySpacedItems);
				}
				return this.m_equallySpacedItems;
			}
		}

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x06001063 RID: 4195 RVA: 0x00045A40 File Offset: 0x00043C40
		public ReportIntProperty TextWrapThreshold
		{
			get
			{
				if (this.m_textWrapThreshold == null && this.MapLegendDef.TextWrapThreshold != null)
				{
					this.m_textWrapThreshold = new ReportIntProperty(this.MapLegendDef.TextWrapThreshold.IsExpression, this.MapLegendDef.TextWrapThreshold.OriginalText, this.MapLegendDef.TextWrapThreshold.IntValue, 0);
				}
				return this.m_textWrapThreshold;
			}
		}

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x06001064 RID: 4196 RVA: 0x00045AA4 File Offset: 0x00043CA4
		internal MapLegend MapLegendDef
		{
			get
			{
				return (MapLegend)this.m_defObject;
			}
		}

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x06001065 RID: 4197 RVA: 0x00045AB1 File Offset: 0x00043CB1
		public new MapLegendInstance Instance
		{
			get
			{
				return (MapLegendInstance)this.GetInstance();
			}
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x00045ABE File Offset: 0x00043CBE
		internal override MapSubItemInstance GetInstance()
		{
			if (this.m_map.RenderingContext.InstanceAccessDisallowed)
			{
				return null;
			}
			if (this.m_instance == null)
			{
				this.m_instance = new MapLegendInstance(this);
			}
			return (MapSubItemInstance)this.m_instance;
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x00045AF3 File Offset: 0x00043CF3
		internal override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_mapLegendTitle != null)
			{
				this.m_mapLegendTitle.SetNewContext();
			}
		}

		// Token: 0x040007A2 RID: 1954
		private ReportEnumProperty<MapLegendLayout> m_layout;

		// Token: 0x040007A3 RID: 1955
		private MapLegendTitle m_mapLegendTitle;

		// Token: 0x040007A4 RID: 1956
		private ReportBoolProperty m_autoFitTextDisabled;

		// Token: 0x040007A5 RID: 1957
		private ReportSizeProperty m_minFontSize;

		// Token: 0x040007A6 RID: 1958
		private ReportBoolProperty m_interlacedRows;

		// Token: 0x040007A7 RID: 1959
		private ReportColorProperty m_interlacedRowsColor;

		// Token: 0x040007A8 RID: 1960
		private ReportBoolProperty m_equallySpacedItems;

		// Token: 0x040007A9 RID: 1961
		private ReportIntProperty m_textWrapThreshold;
	}
}
