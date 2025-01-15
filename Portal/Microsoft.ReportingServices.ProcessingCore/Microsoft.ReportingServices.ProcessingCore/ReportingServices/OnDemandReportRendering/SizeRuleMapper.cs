using System;
using System.Drawing;
using Microsoft.Reporting.Map.WebForms;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200016B RID: 363
	internal class SizeRuleMapper : RuleMapper
	{
		// Token: 0x06000F38 RID: 3896 RVA: 0x0004208B File Offset: 0x0004028B
		internal SizeRuleMapper(MapSizeRule mapColorRule, VectorLayerMapper vectorLayerMapper, CoreSpatialElementManager coreSpatialElementManager)
			: base(mapColorRule, vectorLayerMapper, coreSpatialElementManager)
		{
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x00042098 File Offset: 0x00040298
		internal void RenderSymbolRule(PointTemplateMapper symbolTemplateMapper, Color? color, MarkerStyle? markerStyle)
		{
			SymbolRule symbolRule = (SymbolRule)this.m_coreRule;
			base.SetRuleLegendProperties(symbolRule);
			base.SetRuleDistribution(symbolRule);
			this.SetSymbolRuleSizes(symbolRule.PredefinedSymbols);
			this.InitializePredefinedSymbols(symbolRule.PredefinedSymbols, symbolTemplateMapper, color, markerStyle);
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x000420DC File Offset: 0x000402DC
		private void InitializePredefinedSymbols(PredefinedSymbolCollection predefinedSymbols, PointTemplateMapper symbolTemplateMapper, Color? color, MarkerStyle? markerStyle)
		{
			foreach (object obj in predefinedSymbols)
			{
				PredefinedSymbol predefinedSymbol = (PredefinedSymbol)obj;
				if (color != null)
				{
					predefinedSymbol.Color = color.Value;
				}
				if (markerStyle != null)
				{
					predefinedSymbol.MarkerStyle = markerStyle.Value;
				}
				base.InitializePredefinedSymbols(predefinedSymbol, symbolTemplateMapper);
			}
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x00042160 File Offset: 0x00040360
		private void SetSymbolRuleSizes(PredefinedSymbolCollection customSymbols)
		{
			int bucketCount = base.GetBucketCount();
			if (bucketCount == 0)
			{
				return;
			}
			double startSize = this.GetStartSize();
			double num = (this.GetEndSize() - startSize) / (double)bucketCount;
			MapBucketCollection mapBuckets = this.m_mapRule.MapBuckets;
			bool flag = base.GetDistributionType() == MapRuleDistributionType.Custom;
			for (int i = 0; i < bucketCount; i++)
			{
				PredefinedSymbol predefinedSymbol = new PredefinedSymbol();
				predefinedSymbol.Width = (predefinedSymbol.Height = (float)((int)Math.Round(startSize + (double)i * num)));
				if (flag)
				{
					MapBucket mapBucket = mapBuckets[i];
					predefinedSymbol.FromValue = base.GetFromValue(mapBucket);
					predefinedSymbol.ToValue = base.GetToValue(mapBucket);
				}
				customSymbols.Add(predefinedSymbol);
			}
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x00042210 File Offset: 0x00040410
		private double GetStartSize()
		{
			MapSizeRule mapSizeRule = (MapSizeRule)this.m_mapRule;
			ReportSizeProperty startSize = mapSizeRule.StartSize;
			ReportSize reportSize;
			if (!startSize.IsExpression)
			{
				reportSize = startSize.Value;
			}
			else
			{
				reportSize = mapSizeRule.Instance.StartSize;
			}
			return MappingHelper.ToPixels(reportSize, this.m_mapMapper.DpiX);
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x00042260 File Offset: 0x00040460
		private double GetEndSize()
		{
			MapSizeRule mapSizeRule = (MapSizeRule)this.m_mapRule;
			ReportSizeProperty reportSizeProperty = mapSizeRule.StartSize;
			reportSizeProperty = mapSizeRule.EndSize;
			ReportSize reportSize;
			if (!reportSizeProperty.IsExpression)
			{
				reportSize = reportSizeProperty.Value;
			}
			else
			{
				reportSize = mapSizeRule.Instance.EndSize;
			}
			return MappingHelper.ToPixels(reportSize, this.m_mapMapper.DpiX);
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x000422B8 File Offset: 0x000404B8
		internal virtual PathWidthRule CreatePathRule()
		{
			PathWidthRule pathWidthRule = new PathWidthRule();
			this.m_coreRule = pathWidthRule;
			pathWidthRule.Text = "";
			pathWidthRule.Category = this.m_mapVectorLayer.Name;
			pathWidthRule.Field = "";
			this.m_coreMap.PathRules.Add(pathWidthRule);
			base.SetRuleFieldName();
			return pathWidthRule;
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x00042314 File Offset: 0x00040514
		internal void RenderLineRule(SpatialElementTemplateMapper spatialElementTemplateMapper, Color? color)
		{
			PathWidthRule pathWidthRule = (PathWidthRule)this.m_coreRule;
			pathWidthRule.UseCustomWidths = true;
			base.SetRuleLegendProperties(pathWidthRule);
			base.SetRuleDistribution(pathWidthRule);
			this.SetPathRuleSizes(pathWidthRule.CustomWidths);
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x00042350 File Offset: 0x00040550
		private void SetPathRuleSizes(CustomWidthCollection customWidths)
		{
			int bucketCount = base.GetBucketCount();
			if (bucketCount == 0)
			{
				return;
			}
			double startSize = this.GetStartSize();
			double num = (this.GetEndSize() - startSize) / (double)bucketCount;
			MapBucketCollection mapBuckets = this.m_mapRule.MapBuckets;
			bool flag = base.GetDistributionType() == MapRuleDistributionType.Custom;
			for (int i = 0; i < bucketCount; i++)
			{
				CustomWidth customWidth = new CustomWidth();
				customWidth.Width = (float)((int)Math.Round(startSize + (double)i * num));
				if (flag)
				{
					MapBucket mapBucket = mapBuckets[i];
					customWidth.FromValue = base.GetFromValue(mapBucket);
					customWidth.ToValue = base.GetToValue(mapBucket);
				}
				customWidths.Add(customWidth);
			}
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x000423F3 File Offset: 0x000405F3
		internal override SymbolRule CreateSymbolRule()
		{
			SymbolRule symbolRule = base.CreateSymbolRule();
			symbolRule.AffectedAttributes = 3;
			return symbolRule;
		}
	}
}
