using System;
using System.IO;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000252 RID: 594
	public sealed class ChartInstance : DynamicImageInstance, IDynamicImageInstance
	{
		// Token: 0x06001719 RID: 5913 RVA: 0x0005D635 File Offset: 0x0005B835
		internal ChartInstance(Microsoft.ReportingServices.OnDemandReportRendering.Chart reportItemDef)
			: base(reportItemDef)
		{
		}

		// Token: 0x0600171A RID: 5914 RVA: 0x0005D63E File Offset: 0x0005B83E
		public override void SetDpi(int xDpi, int yDpi)
		{
			if (this.m_reportElementDef.IsOldSnapshot)
			{
				((Microsoft.ReportingServices.ReportRendering.Chart)this.m_reportElementDef.RenderReportItem).SetDpi(xDpi, yDpi);
				return;
			}
			base.SetDpi(xDpi, yDpi);
		}

		// Token: 0x0600171B RID: 5915 RVA: 0x0005D66D File Offset: 0x0005B86D
		protected override Stream GetImage(DynamicImageInstance.ImageType type, out bool hasImageMap)
		{
			if (this.m_reportElementDef.IsOldSnapshot)
			{
				return ((Microsoft.ReportingServices.ReportRendering.Chart)this.m_reportElementDef.RenderReportItem).GetImage((Microsoft.ReportingServices.ReportRendering.Chart.ImageType)type, out hasImageMap);
			}
			return base.GetImage(type, out hasImageMap);
		}

		// Token: 0x0600171C RID: 5916 RVA: 0x0005D69C File Offset: 0x0005B89C
		public override Stream GetImage(DynamicImageInstance.ImageType type, out ActionInfoWithDynamicImageMapCollection actionImageMaps)
		{
			actionImageMaps = null;
			bool flag = false;
			Stream stream;
			if (this.m_reportElementDef.IsOldSnapshot)
			{
				Microsoft.ReportingServices.ReportRendering.Chart chart = (Microsoft.ReportingServices.ReportRendering.Chart)this.m_reportElementDef.RenderReportItem;
				stream = chart.GetImage((Microsoft.ReportingServices.ReportRendering.Chart.ImageType)type, out flag);
				if (flag)
				{
					int dataPointSeriesCount = chart.DataPointSeriesCount;
					int dataPointCategoryCount = chart.DataPointCategoryCount;
					actionImageMaps = new ActionInfoWithDynamicImageMapCollection();
					for (int i = 0; i < dataPointSeriesCount; i++)
					{
						for (int j = 0; j < dataPointCategoryCount; j++)
						{
							Microsoft.ReportingServices.ReportRendering.ChartDataPoint chartDataPoint = chart.DataPointCollection[i, j];
							ActionInfo actionInfo = chartDataPoint.ActionInfo;
							if (actionInfo != null)
							{
								actionImageMaps.InternalList.Add(new ActionInfoWithDynamicImageMap(this.m_reportElementDef.RenderingContext, actionInfo, chartDataPoint.MapAreas));
							}
						}
					}
				}
			}
			else
			{
				stream = base.GetImage(type, out actionImageMaps);
			}
			return stream;
		}

		// Token: 0x0600171D RID: 5917 RVA: 0x0005D768 File Offset: 0x0005B968
		public Stream GetCoreXml()
		{
			if (this.m_reportElementDef.IsOldSnapshot)
			{
				return null;
			}
			Stream coreXml;
			using (IChartMapper chartMapper = ChartMapperFactory.CreateChartMapperInstance((Microsoft.ReportingServices.OnDemandReportRendering.Chart)this.m_reportElementDef, base.GetDefaultFontFamily()))
			{
				chartMapper.DpiX = this.m_dpiX;
				chartMapper.DpiY = this.m_dpiY;
				chartMapper.WidthOverride = this.m_widthOverride;
				chartMapper.HeightOverride = this.m_heightOverride;
				chartMapper.RenderChart();
				coreXml = chartMapper.GetCoreXml();
			}
			return coreXml;
		}

		// Token: 0x0600171E RID: 5918 RVA: 0x0005D7F8 File Offset: 0x0005B9F8
		protected override void GetImage(DynamicImageInstance.ImageType type, out ActionInfoWithDynamicImageMapCollection actionImageMaps, out Stream image)
		{
			using (IChartMapper chartMapper = ChartMapperFactory.CreateChartMapperInstance((Microsoft.ReportingServices.OnDemandReportRendering.Chart)this.m_reportElementDef, base.GetDefaultFontFamily()))
			{
				chartMapper.DpiX = this.m_dpiX;
				chartMapper.DpiY = this.m_dpiY;
				chartMapper.WidthOverride = this.m_widthOverride;
				chartMapper.HeightOverride = this.m_heightOverride;
				chartMapper.RenderChart();
				image = chartMapper.GetImage(type);
				actionImageMaps = chartMapper.GetImageMaps();
			}
		}

		// Token: 0x0600171F RID: 5919 RVA: 0x0005D880 File Offset: 0x0005BA80
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
			this.m_dynamicHeight = null;
			this.m_dynamicWidth = null;
		}

		// Token: 0x17000CFC RID: 3324
		// (get) Token: 0x06001720 RID: 5920 RVA: 0x0005D896 File Offset: 0x0005BA96
		protected override int WidthInPixels
		{
			get
			{
				return MappingHelper.ToIntPixels(this.DynamicWidth, this.m_dpiX);
			}
		}

		// Token: 0x17000CFD RID: 3325
		// (get) Token: 0x06001721 RID: 5921 RVA: 0x0005D8A9 File Offset: 0x0005BAA9
		protected override int HeightInPixels
		{
			get
			{
				return MappingHelper.ToIntPixels(this.DynamicHeight, this.m_dpiX);
			}
		}

		// Token: 0x17000CFE RID: 3326
		// (get) Token: 0x06001722 RID: 5922 RVA: 0x0005D8BC File Offset: 0x0005BABC
		public ReportSize DynamicHeight
		{
			get
			{
				if (this.m_dynamicHeight == null)
				{
					if (this.m_reportElementDef.IsOldSnapshot)
					{
						this.m_dynamicHeight = new ReportSize(this.m_reportElementDef.RenderReportItem.Height);
					}
					else
					{
						string text = ((Microsoft.ReportingServices.ReportIntermediateFormat.Chart)this.m_reportElementDef.ReportItemDef).EvaluateDynamicHeight(this, this.m_reportElementDef.RenderingContext.OdpContext);
						if (!string.IsNullOrEmpty(text))
						{
							this.m_dynamicHeight = new ReportSize(text);
						}
						else
						{
							this.m_dynamicHeight = ((Microsoft.ReportingServices.OnDemandReportRendering.ReportItem)this.m_reportElementDef).Height;
						}
					}
				}
				return this.m_dynamicHeight;
			}
		}

		// Token: 0x17000CFF RID: 3327
		// (get) Token: 0x06001723 RID: 5923 RVA: 0x0005D954 File Offset: 0x0005BB54
		public ReportSize DynamicWidth
		{
			get
			{
				if (this.m_dynamicWidth == null)
				{
					if (this.m_reportElementDef.IsOldSnapshot)
					{
						this.m_dynamicWidth = new ReportSize(this.m_reportElementDef.RenderReportItem.Width);
					}
					else
					{
						string text = ((Microsoft.ReportingServices.ReportIntermediateFormat.Chart)this.m_reportElementDef.ReportItemDef).EvaluateDynamicWidth(this, this.m_reportElementDef.RenderingContext.OdpContext);
						if (!string.IsNullOrEmpty(text))
						{
							this.m_dynamicWidth = new ReportSize(text);
						}
						else
						{
							this.m_dynamicWidth = ((Microsoft.ReportingServices.OnDemandReportRendering.ReportItem)this.m_reportElementDef).Width;
						}
					}
				}
				return this.m_dynamicWidth;
			}
		}

		// Token: 0x17000D00 RID: 3328
		// (get) Token: 0x06001724 RID: 5924 RVA: 0x0005D9EC File Offset: 0x0005BBEC
		public ChartPalette Palette
		{
			get
			{
				if (this.m_palette == null)
				{
					if (this.m_reportElementDef.IsOldSnapshot)
					{
						this.m_palette = new ChartPalette?(((Microsoft.ReportingServices.OnDemandReportRendering.Chart)this.m_reportElementDef).Palette.Value);
					}
					else
					{
						this.m_palette = new ChartPalette?(((Microsoft.ReportingServices.OnDemandReportRendering.Chart)this.m_reportElementDef).ChartDef.EvaluatePalette(this.ReportScopeInstance, this.m_reportElementDef.RenderingContext.OdpContext));
					}
				}
				return this.m_palette.Value;
			}
		}

		// Token: 0x17000D01 RID: 3329
		// (get) Token: 0x06001725 RID: 5925 RVA: 0x0005DA78 File Offset: 0x0005BC78
		public PaletteHatchBehavior PaletteHatchBehavior
		{
			get
			{
				if (this.m_paletteHatchBehavior == null)
				{
					this.m_paletteHatchBehavior = new PaletteHatchBehavior?(((Microsoft.ReportingServices.OnDemandReportRendering.Chart)this.m_reportElementDef).ChartDef.EvaluatePaletteHatchBehavior(this.ReportScopeInstance, this.m_reportElementDef.RenderingContext.OdpContext));
				}
				return this.m_paletteHatchBehavior.Value;
			}
		}

		// Token: 0x04000B5E RID: 2910
		private ReportSize m_dynamicHeight;

		// Token: 0x04000B5F RID: 2911
		private ReportSize m_dynamicWidth;

		// Token: 0x04000B60 RID: 2912
		private ChartPalette? m_palette;

		// Token: 0x04000B61 RID: 2913
		private PaletteHatchBehavior? m_paletteHatchBehavior;
	}
}
