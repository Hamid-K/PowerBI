using System;
using System.Drawing;
using Microsoft.Reporting.Map.WebForms;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200016C RID: 364
	internal class MarkerRuleMapper : RuleMapper
	{
		// Token: 0x06000F42 RID: 3906 RVA: 0x00042402 File Offset: 0x00040602
		internal MarkerRuleMapper(MapMarkerRule mapColorRule, VectorLayerMapper vectorLayerMapper, CoreSpatialElementManager coreSpatialElementManager)
			: base(mapColorRule, vectorLayerMapper, coreSpatialElementManager)
		{
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x00042410 File Offset: 0x00040610
		internal void RenderPointRule(PointTemplateMapper pointTemplateMapper, Color? color, int? size)
		{
			SymbolRule symbolRule = (SymbolRule)this.m_coreRule;
			base.SetRuleLegendProperties(symbolRule);
			base.SetRuleDistribution(symbolRule);
			this.SetSymbolRuleMarkers(symbolRule.PredefinedSymbols);
			this.InitializePredefinedSymbols(symbolRule.PredefinedSymbols, pointTemplateMapper, color, size);
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x00042454 File Offset: 0x00040654
		private void InitializePredefinedSymbols(PredefinedSymbolCollection predefinedSymbols, PointTemplateMapper spatialElementTemplateMapper, Color? color, int? size)
		{
			foreach (object obj in predefinedSymbols)
			{
				PredefinedSymbol predefinedSymbol = (PredefinedSymbol)obj;
				if (color != null)
				{
					predefinedSymbol.Color = color.Value;
				}
				if (size != null)
				{
					predefinedSymbol.Width = (predefinedSymbol.Height = (float)size.Value);
				}
				base.InitializePredefinedSymbols(predefinedSymbol, spatialElementTemplateMapper);
			}
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x000424E0 File Offset: 0x000406E0
		private void SetSymbolRuleMarkers(PredefinedSymbolCollection customSymbols)
		{
			int bucketCount = base.GetBucketCount();
			MapMarkerCollection mapMarkers = ((MapMarkerRule)this.m_mapRule).MapMarkers;
			int count = mapMarkers.Count;
			MapBucketCollection mapBuckets = this.m_mapRule.MapBuckets;
			bool flag = base.GetDistributionType() == MapRuleDistributionType.Custom;
			for (int i = 0; i < bucketCount; i++)
			{
				PredefinedSymbol predefinedSymbol = new PredefinedSymbol();
				if (i < count)
				{
					this.RenderMarker(predefinedSymbol, mapMarkers[i]);
				}
				else
				{
					predefinedSymbol.MarkerStyle = 0;
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

		// Token: 0x06000F46 RID: 3910 RVA: 0x00042590 File Offset: 0x00040790
		private void RenderMarker(PredefinedSymbol customSymbol, MapMarker mapMarker)
		{
			MapMarkerStyle markerStyle = MapMapper.GetMarkerStyle(mapMarker, true);
			if (markerStyle != MapMarkerStyle.Image)
			{
				customSymbol.MarkerStyle = MapMapper.GetMarkerStyle(markerStyle);
				return;
			}
			MapMarkerImage mapMarkerImage = mapMarker.MapMarkerImage;
			if (mapMarkerImage == null)
			{
				throw new RenderingObjectModelException(RPResWrapper.rsMapLayerMissingProperty(RPRes.rsObjectTypeMap, this.m_mapRule.MapDef.Name, this.m_mapVectorLayer.Name, "MapMarkerImage"));
			}
			customSymbol.Image = this.m_mapMapper.AddImage(mapMarkerImage);
			customSymbol.ImageResizeMode = this.m_mapMapper.GetImageResizeMode(mapMarkerImage);
			customSymbol.ImageTransColor = this.m_mapMapper.GetImageTransColor(mapMarkerImage);
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x00042627 File Offset: 0x00040827
		internal override SymbolRule CreateSymbolRule()
		{
			SymbolRule symbolRule = base.CreateSymbolRule();
			symbolRule.AffectedAttributes = 2;
			return symbolRule;
		}
	}
}
