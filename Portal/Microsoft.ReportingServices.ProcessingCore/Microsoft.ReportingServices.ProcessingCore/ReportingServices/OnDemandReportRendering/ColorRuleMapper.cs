using System;
using System.Drawing;
using Microsoft.Reporting.Map.WebForms;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200016A RID: 362
	internal class ColorRuleMapper : RuleMapper
	{
		// Token: 0x06000F1A RID: 3866 RVA: 0x0004182B File Offset: 0x0003FA2B
		internal ColorRuleMapper(MapColorRule mapColorRule, VectorLayerMapper vectorLayerMapper, CoreSpatialElementManager coreSpatialElementManager)
			: base(mapColorRule, vectorLayerMapper, coreSpatialElementManager)
		{
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x00041838 File Offset: 0x0003FA38
		internal PathRule CreatePathRule()
		{
			PathRule pathRule = new PathRule();
			this.m_coreRule = pathRule;
			pathRule.BorderColor = Color.Empty;
			pathRule.Text = "";
			pathRule.Category = this.m_mapVectorLayer.Name;
			pathRule.Field = "";
			this.m_coreMap.PathRules.Add(pathRule);
			base.SetRuleFieldName();
			return pathRule;
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x000418A0 File Offset: 0x0003FAA0
		internal void RenderPolygonRule(PolygonTemplateMapper shapeTemplateMapper)
		{
			ShapeRule shapeRule = (ShapeRule)this.m_coreRule;
			base.SetRuleLegendProperties(shapeRule);
			base.SetRuleDistribution(shapeRule);
			shapeRule.ShowInColorSwatch = this.GetShowInColorScale();
			if (this.m_mapRule is MapColorRangeRule)
			{
				this.RenderPolygonColorRangeRule(shapeRule);
			}
			else if (this.m_mapRule is MapColorPaletteRule)
			{
				this.RenderPolygonColorPaletteRule(shapeRule);
			}
			else
			{
				this.RenderPolygonCustomColorRule(shapeRule);
			}
			this.InitializeCustomColors(shapeRule.CustomColors, shapeTemplateMapper);
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x00041914 File Offset: 0x0003FB14
		internal void RenderSymbolRule(PointTemplateMapper symbolTemplateMapper, int? size, MarkerStyle? markerStyle)
		{
			SymbolRule symbolRule = (SymbolRule)this.m_coreRule;
			base.SetRuleLegendProperties(symbolRule);
			base.SetRuleDistribution(symbolRule);
			symbolRule.ShowInColorSwatch = this.GetShowInColorScale();
			if (this.m_mapRule is MapColorRangeRule)
			{
				this.RenderSymbolColorRangeRule(symbolRule);
			}
			else if (this.m_mapRule is MapColorPaletteRule)
			{
				this.RenderSymbolColorPaletteRule(symbolRule);
			}
			else
			{
				this.SetSymbolRuleColors(this.GetCustomColors(((MapCustomColorRule)this.m_mapRule).MapCustomColors), symbolRule.PredefinedSymbols);
			}
			this.InitializePredefinedSymbols(symbolRule.PredefinedSymbols, symbolTemplateMapper, size, markerStyle);
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x000419A4 File Offset: 0x0003FBA4
		private void InitializePredefinedSymbols(PredefinedSymbolCollection predefinedSymbols, PointTemplateMapper symbolTemplateMapper, int? size, MarkerStyle? markerStyle)
		{
			foreach (object obj in predefinedSymbols)
			{
				PredefinedSymbol predefinedSymbol = (PredefinedSymbol)obj;
				if (size != null)
				{
					predefinedSymbol.Width = (predefinedSymbol.Height = (float)size.Value);
				}
				if (markerStyle != null)
				{
					predefinedSymbol.MarkerStyle = markerStyle.Value;
				}
				base.InitializePredefinedSymbols(predefinedSymbol, symbolTemplateMapper);
			}
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x00041A30 File Offset: 0x0003FC30
		internal void RenderLineRule(LineTemplateMapper pathTemplateMapper, int? size)
		{
			PathRule pathRule = (PathRule)this.m_coreRule;
			base.SetRuleLegendProperties(pathRule);
			base.SetRuleDistribution(pathRule);
			pathRule.ShowInColorSwatch = this.GetShowInColorScale();
			if (this.m_mapRule is MapColorRangeRule)
			{
				this.RenderLineColorRangeRule(pathRule);
			}
			else if (this.m_mapRule is MapColorPaletteRule)
			{
				this.RenderLineColorPaletteRule(pathRule);
			}
			else
			{
				this.RenderLineCustomColorRule(pathRule);
			}
			this.InitializePathRule(pathRule, pathTemplateMapper, size);
		}

		// Token: 0x06000F20 RID: 3872 RVA: 0x00041A9F File Offset: 0x0003FC9F
		private void InitializePathRule(PathRule pathRule, LineTemplateMapper pathTemplateMapper, int? size)
		{
			this.InitializeCustomColors(pathRule.CustomColors, pathTemplateMapper);
		}

		// Token: 0x06000F21 RID: 3873 RVA: 0x00041AB0 File Offset: 0x0003FCB0
		private void InitializeCustomColors(CustomColorCollection customColors, SpatialElementTemplateMapper spatialEementTemplateMapper)
		{
			foreach (object obj in customColors)
			{
				CustomColor customColor = (CustomColor)obj;
				customColor.BorderColor = spatialEementTemplateMapper.GetBorderColor(false);
				customColor.SecondaryColor = spatialEementTemplateMapper.GetBackGradientEndColor(false);
				customColor.GradientType = spatialEementTemplateMapper.GetGradientType(false);
				customColor.HatchStyle = spatialEementTemplateMapper.GetHatchStyle(false);
				customColor.LegendText = "";
				customColor.Text = "";
			}
		}

		// Token: 0x06000F22 RID: 3874 RVA: 0x00041B48 File Offset: 0x0003FD48
		private void RenderLineColorRangeRule(PathRule pathRule)
		{
			MapColorRangeRule mapColorRangeRule = (MapColorRangeRule)this.m_mapRule;
			this.RenderPathCustomColors(pathRule, 0, 3, this.GetFromColor(mapColorRangeRule), this.GetMiddleColor(mapColorRangeRule), this.GetToColor(mapColorRangeRule));
		}

		// Token: 0x06000F23 RID: 3875 RVA: 0x00041B7F File Offset: 0x0003FD7F
		private void RenderLineColorPaletteRule(PathRule pathRule)
		{
			this.RenderPathCustomColors(pathRule, 1, this.GetColorPalette(), Color.Empty, Color.Empty, Color.Empty);
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x00041B9E File Offset: 0x0003FD9E
		private void RenderLineCustomColorRule(PathRule pathRule)
		{
			pathRule.UseCustomColors = true;
			this.SetRuleColors(this.GetCustomColors(((MapCustomColorRule)this.m_mapRule).MapCustomColors), pathRule.CustomColors);
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x00041BCC File Offset: 0x0003FDCC
		private Color GetFromColor(MapColorRangeRule colorRangeRule)
		{
			ReportColorProperty startColor = colorRangeRule.StartColor;
			Color defaultFromColor = ColorRuleMapper.m_defaultFromColor;
			if (startColor != null)
			{
				if (MappingHelper.GetColorFromReportColorProperty(startColor, ref defaultFromColor))
				{
					return defaultFromColor;
				}
				if (colorRangeRule.Instance.StartColor != null)
				{
					return colorRangeRule.Instance.StartColor.ToColor();
				}
			}
			return defaultFromColor;
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x00041C14 File Offset: 0x0003FE14
		private Color GetMiddleColor(MapColorRangeRule colorRangeRule)
		{
			ReportColorProperty middleColor = colorRangeRule.MiddleColor;
			Color defaultMiddleColor = ColorRuleMapper.m_defaultMiddleColor;
			if (middleColor != null)
			{
				if (MappingHelper.GetColorFromReportColorProperty(middleColor, ref defaultMiddleColor))
				{
					return defaultMiddleColor;
				}
				if (colorRangeRule.Instance.MiddleColor != null)
				{
					return colorRangeRule.Instance.MiddleColor.ToColor();
				}
			}
			return defaultMiddleColor;
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x00041C5C File Offset: 0x0003FE5C
		private Color GetToColor(MapColorRangeRule colorRangeRule)
		{
			ReportColorProperty endColor = colorRangeRule.EndColor;
			Color defaultToColor = ColorRuleMapper.m_defaultToColor;
			if (endColor != null)
			{
				if (MappingHelper.GetColorFromReportColorProperty(endColor, ref defaultToColor))
				{
					return defaultToColor;
				}
				if (colorRangeRule.Instance.EndColor != null)
				{
					return colorRangeRule.Instance.EndColor.ToColor();
				}
			}
			return defaultToColor;
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x00041CA4 File Offset: 0x0003FEA4
		private bool GetShowInColorScale()
		{
			ReportBoolProperty showInColorScale = ((MapColorRule)this.m_mapRule).ShowInColorScale;
			if (showInColorScale == null)
			{
				return false;
			}
			if (!showInColorScale.IsExpression)
			{
				return showInColorScale.Value;
			}
			return ((MapColorRule)this.m_mapRule).Instance.ShowInColorScale;
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x00041CEC File Offset: 0x0003FEEC
		private MapColorPalette GetColorPalette()
		{
			MapColorPaletteRule mapColorPaletteRule = (MapColorPaletteRule)this.m_mapRule;
			ReportEnumProperty<MapPalette> palette = mapColorPaletteRule.Palette;
			if (palette == null)
			{
				return 0;
			}
			if (!palette.IsExpression)
			{
				return this.GetMapColorPalette(palette.Value);
			}
			return this.GetMapColorPalette(mapColorPaletteRule.Instance.Palette);
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x00041D38 File Offset: 0x0003FF38
		private Color[] GetCustomColors(MapCustomColorCollection mapCustomColors)
		{
			Color[] array = new Color[mapCustomColors.Count];
			for (int i = 0; i < mapCustomColors.Count; i++)
			{
				array[i] = this.GetCustomColor(mapCustomColors[i]);
			}
			return array;
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x00041D78 File Offset: 0x0003FF78
		private void SetRuleColors(Color[] colorRange, CustomColorCollection customColors)
		{
			MapBucketCollection mapBuckets = this.m_mapRule.MapBuckets;
			bool flag = base.GetDistributionType() == MapRuleDistributionType.Custom;
			int bucketCount = base.GetBucketCount();
			for (int i = 0; i < bucketCount; i++)
			{
				CustomColor customColor = new CustomColor();
				if (i < colorRange.Length)
				{
					customColor.Color = colorRange[i];
				}
				else
				{
					customColor.Color = Color.Empty;
				}
				if (flag)
				{
					MapBucket mapBucket = mapBuckets[i];
					customColor.FromValue = base.GetFromValue(mapBucket);
					customColor.ToValue = base.GetToValue(mapBucket);
				}
				customColors.Add(customColor);
			}
		}

		// Token: 0x06000F2C RID: 3884 RVA: 0x00041E10 File Offset: 0x00040010
		private void SetSymbolRuleColors(Color[] colorRange, PredefinedSymbolCollection customSymbols)
		{
			MapBucketCollection mapBuckets = this.m_mapRule.MapBuckets;
			bool flag = base.GetDistributionType() == MapRuleDistributionType.Custom;
			int bucketCount = base.GetBucketCount();
			for (int i = 0; i < bucketCount; i++)
			{
				PredefinedSymbol predefinedSymbol = new PredefinedSymbol();
				if (i < colorRange.Length)
				{
					predefinedSymbol.Color = colorRange[i];
				}
				else
				{
					predefinedSymbol.Color = Color.Empty;
				}
				if (flag)
				{
					MapBucket mapBucket = mapBuckets[i];
					predefinedSymbol.FromValue = base.GetFromValue(mapBucket);
					predefinedSymbol.ToValue = base.GetToValue(mapBucket);
				}
				customSymbols.Add(predefinedSymbol);
			}
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x00041EA8 File Offset: 0x000400A8
		private void RenderPolygonColorRangeRule(ShapeRule shapeRule)
		{
			MapColorRangeRule mapColorRangeRule = (MapColorRangeRule)this.m_mapRule;
			this.RenderShapeCustomColors(shapeRule, 0, 3, this.GetFromColor(mapColorRangeRule), this.GetMiddleColor(mapColorRangeRule), this.GetToColor(mapColorRangeRule));
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x00041EDF File Offset: 0x000400DF
		private void RenderPolygonColorPaletteRule(ShapeRule shapeRule)
		{
			this.RenderShapeCustomColors(shapeRule, 1, this.GetColorPalette(), Color.Empty, Color.Empty, Color.Empty);
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x00041EFE File Offset: 0x000400FE
		private void RenderPolygonCustomColorRule(ShapeRule shapeRule)
		{
			shapeRule.UseCustomColors = true;
			this.SetRuleColors(this.GetCustomColors(((MapCustomColorRule)this.m_mapRule).MapCustomColors), shapeRule.CustomColors);
		}

		// Token: 0x06000F30 RID: 3888 RVA: 0x00041F2C File Offset: 0x0004012C
		private void RenderSymbolColorRangeRule(SymbolRule symbolRule)
		{
			MapColorRangeRule mapColorRangeRule = (MapColorRangeRule)this.m_mapRule;
			this.SetSymbolRuleColors(symbolRule.GetColors(0, 3, this.GetFromColor(mapColorRangeRule), this.GetMiddleColor(mapColorRangeRule), this.GetToColor(mapColorRangeRule), base.GetBucketCount()), symbolRule.PredefinedSymbols);
		}

		// Token: 0x06000F31 RID: 3889 RVA: 0x00041F74 File Offset: 0x00040174
		private void RenderSymbolColorPaletteRule(SymbolRule symbolRule)
		{
			this.SetSymbolRuleColors(symbolRule.GetColors(1, this.GetColorPalette(), Color.Empty, Color.Empty, Color.Empty, base.GetBucketCount()), symbolRule.PredefinedSymbols);
		}

		// Token: 0x06000F32 RID: 3890 RVA: 0x00041FA4 File Offset: 0x000401A4
		private void RenderShapeCustomColors(ShapeRule shapeRule, ColoringMode coloringMode, MapColorPalette palette, Color fromColor, Color middleColor, Color toColor)
		{
			shapeRule.UseCustomColors = true;
			this.SetRuleColors(shapeRule.GetColors(coloringMode, palette, fromColor, middleColor, toColor, base.GetBucketCount()), shapeRule.CustomColors);
		}

		// Token: 0x06000F33 RID: 3891 RVA: 0x00041FCD File Offset: 0x000401CD
		private void RenderPathCustomColors(PathRule pathRule, ColoringMode coloringMode, MapColorPalette palette, Color fromColor, Color middleColor, Color toColor)
		{
			pathRule.UseCustomColors = true;
			this.SetRuleColors(pathRule.GetColors(coloringMode, palette, fromColor, middleColor, toColor, base.GetBucketCount()), pathRule.CustomColors);
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x00041FF6 File Offset: 0x000401F6
		private MapColorPalette GetMapColorPalette(MapPalette palette)
		{
			switch (palette)
			{
			case MapPalette.Light:
				return 2;
			case MapPalette.SemiTransparent:
				return 1;
			case MapPalette.BrightPastel:
				return 3;
			case MapPalette.Pacific:
				return 4;
			default:
				return 0;
			}
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x0004201C File Offset: 0x0004021C
		private Color GetCustomColor(MapCustomColor mapCustomColor)
		{
			ReportColorProperty color = mapCustomColor.Color;
			Color empty = Color.Empty;
			if (color != null)
			{
				if (MappingHelper.GetColorFromReportColorProperty(color, ref empty))
				{
					return empty;
				}
				ReportColor color2 = mapCustomColor.Instance.Color;
				if (color2 != null)
				{
					return color2.ToColor();
				}
			}
			return empty;
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x0004205C File Offset: 0x0004025C
		internal override SymbolRule CreateSymbolRule()
		{
			SymbolRule symbolRule = base.CreateSymbolRule();
			symbolRule.AffectedAttributes = 1;
			return symbolRule;
		}

		// Token: 0x04000728 RID: 1832
		private static Color m_defaultFromColor = Color.Green;

		// Token: 0x04000729 RID: 1833
		private static Color m_defaultMiddleColor = Color.Yellow;

		// Token: 0x0400072A RID: 1834
		private static Color m_defaultToColor = Color.Red;
	}
}
