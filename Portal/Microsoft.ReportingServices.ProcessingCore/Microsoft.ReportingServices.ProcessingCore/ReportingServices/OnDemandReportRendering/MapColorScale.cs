using System;
using System.Drawing;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200018F RID: 399
	public sealed class MapColorScale : MapDockableSubItem
	{
		// Token: 0x06001034 RID: 4148 RVA: 0x000450A6 File Offset: 0x000432A6
		internal MapColorScale(MapColorScale defObject, Map map)
			: base(defObject, map)
		{
		}

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x06001035 RID: 4149 RVA: 0x000450B0 File Offset: 0x000432B0
		public MapColorScaleTitle MapColorScaleTitle
		{
			get
			{
				if (this.m_mapColorScaleTitle == null && this.MapColorScaleDef.MapColorScaleTitle != null)
				{
					this.m_mapColorScaleTitle = new MapColorScaleTitle(this.MapColorScaleDef.MapColorScaleTitle, this.m_map);
				}
				return this.m_mapColorScaleTitle;
			}
		}

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x06001036 RID: 4150 RVA: 0x000450E9 File Offset: 0x000432E9
		public ReportSizeProperty TickMarkLength
		{
			get
			{
				if (this.m_tickMarkLength == null && this.MapColorScaleDef.TickMarkLength != null)
				{
					this.m_tickMarkLength = new ReportSizeProperty(this.MapColorScaleDef.TickMarkLength);
				}
				return this.m_tickMarkLength;
			}
		}

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x06001037 RID: 4151 RVA: 0x0004511C File Offset: 0x0004331C
		public ReportColorProperty ColorBarBorderColor
		{
			get
			{
				if (this.m_colorBarBorderColor == null && this.MapColorScaleDef.ColorBarBorderColor != null)
				{
					ExpressionInfo colorBarBorderColor = this.MapColorScaleDef.ColorBarBorderColor;
					if (colorBarBorderColor != null)
					{
						this.m_colorBarBorderColor = new ReportColorProperty(colorBarBorderColor.IsExpression, this.MapColorScaleDef.ColorBarBorderColor.OriginalText, colorBarBorderColor.IsExpression ? null : new ReportColor(colorBarBorderColor.StringValue.Trim(), true), colorBarBorderColor.IsExpression ? new ReportColor("", Color.Empty, true) : null);
					}
				}
				return this.m_colorBarBorderColor;
			}
		}

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x06001038 RID: 4152 RVA: 0x000451AC File Offset: 0x000433AC
		public ReportIntProperty LabelInterval
		{
			get
			{
				if (this.m_labelInterval == null && this.MapColorScaleDef.LabelInterval != null)
				{
					this.m_labelInterval = new ReportIntProperty(this.MapColorScaleDef.LabelInterval.IsExpression, this.MapColorScaleDef.LabelInterval.OriginalText, this.MapColorScaleDef.LabelInterval.IntValue, 0);
				}
				return this.m_labelInterval;
			}
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x06001039 RID: 4153 RVA: 0x00045210 File Offset: 0x00043410
		public ReportStringProperty LabelFormat
		{
			get
			{
				if (this.m_labelFormat == null && this.MapColorScaleDef.LabelFormat != null)
				{
					this.m_labelFormat = new ReportStringProperty(this.MapColorScaleDef.LabelFormat);
				}
				return this.m_labelFormat;
			}
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x0600103A RID: 4154 RVA: 0x00045244 File Offset: 0x00043444
		public ReportEnumProperty<MapLabelPlacement> LabelPlacement
		{
			get
			{
				if (this.m_labelPlacement == null && this.MapColorScaleDef.LabelPlacement != null)
				{
					this.m_labelPlacement = new ReportEnumProperty<MapLabelPlacement>(this.MapColorScaleDef.LabelPlacement.IsExpression, this.MapColorScaleDef.LabelPlacement.OriginalText, EnumTranslator.TranslateLabelPlacement(this.MapColorScaleDef.LabelPlacement.StringValue, null));
				}
				return this.m_labelPlacement;
			}
		}

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x0600103B RID: 4155 RVA: 0x000452B0 File Offset: 0x000434B0
		public ReportEnumProperty<MapLabelBehavior> LabelBehavior
		{
			get
			{
				if (this.m_labelBehavior == null && this.MapColorScaleDef.LabelBehavior != null)
				{
					this.m_labelBehavior = new ReportEnumProperty<MapLabelBehavior>(this.MapColorScaleDef.LabelBehavior.IsExpression, this.MapColorScaleDef.LabelBehavior.OriginalText, EnumTranslator.TranslateLabelBehavior(this.MapColorScaleDef.LabelBehavior.StringValue, null));
				}
				return this.m_labelBehavior;
			}
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x0600103C RID: 4156 RVA: 0x00045319 File Offset: 0x00043519
		public ReportBoolProperty HideEndLabels
		{
			get
			{
				if (this.m_hideEndLabels == null && this.MapColorScaleDef.HideEndLabels != null)
				{
					this.m_hideEndLabels = new ReportBoolProperty(this.MapColorScaleDef.HideEndLabels);
				}
				return this.m_hideEndLabels;
			}
		}

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x0600103D RID: 4157 RVA: 0x0004534C File Offset: 0x0004354C
		public ReportColorProperty RangeGapColor
		{
			get
			{
				if (this.m_rangeGapColor == null && this.MapColorScaleDef.RangeGapColor != null)
				{
					ExpressionInfo rangeGapColor = this.MapColorScaleDef.RangeGapColor;
					if (rangeGapColor != null)
					{
						this.m_rangeGapColor = new ReportColorProperty(rangeGapColor.IsExpression, this.MapColorScaleDef.RangeGapColor.OriginalText, rangeGapColor.IsExpression ? null : new ReportColor(rangeGapColor.StringValue.Trim(), true), rangeGapColor.IsExpression ? new ReportColor("", Color.Empty, true) : null);
					}
				}
				return this.m_rangeGapColor;
			}
		}

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x0600103E RID: 4158 RVA: 0x000453DB File Offset: 0x000435DB
		public ReportStringProperty NoDataText
		{
			get
			{
				if (this.m_noDataText == null && this.MapColorScaleDef.NoDataText != null)
				{
					this.m_noDataText = new ReportStringProperty(this.MapColorScaleDef.NoDataText);
				}
				return this.m_noDataText;
			}
		}

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x0600103F RID: 4159 RVA: 0x0004540E File Offset: 0x0004360E
		internal MapColorScale MapColorScaleDef
		{
			get
			{
				return (MapColorScale)this.m_defObject;
			}
		}

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x06001040 RID: 4160 RVA: 0x0004541B File Offset: 0x0004361B
		public new MapColorScaleInstance Instance
		{
			get
			{
				return (MapColorScaleInstance)this.GetInstance();
			}
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x00045428 File Offset: 0x00043628
		internal override MapSubItemInstance GetInstance()
		{
			if (this.m_map.RenderingContext.InstanceAccessDisallowed)
			{
				return null;
			}
			if (this.m_instance == null)
			{
				this.m_instance = new MapColorScaleInstance(this);
			}
			return (MapSubItemInstance)this.m_instance;
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x0004545D File Offset: 0x0004365D
		internal override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_mapColorScaleTitle != null)
			{
				this.m_mapColorScaleTitle.SetNewContext();
			}
		}

		// Token: 0x0400078E RID: 1934
		private MapColorScaleTitle m_mapColorScaleTitle;

		// Token: 0x0400078F RID: 1935
		private ReportSizeProperty m_tickMarkLength;

		// Token: 0x04000790 RID: 1936
		private ReportColorProperty m_colorBarBorderColor;

		// Token: 0x04000791 RID: 1937
		private ReportIntProperty m_labelInterval;

		// Token: 0x04000792 RID: 1938
		private ReportStringProperty m_labelFormat;

		// Token: 0x04000793 RID: 1939
		private ReportEnumProperty<MapLabelPlacement> m_labelPlacement;

		// Token: 0x04000794 RID: 1940
		private ReportEnumProperty<MapLabelBehavior> m_labelBehavior;

		// Token: 0x04000795 RID: 1941
		private ReportBoolProperty m_hideEndLabels;

		// Token: 0x04000796 RID: 1942
		private ReportColorProperty m_rangeGapColor;

		// Token: 0x04000797 RID: 1943
		private ReportStringProperty m_noDataText;
	}
}
