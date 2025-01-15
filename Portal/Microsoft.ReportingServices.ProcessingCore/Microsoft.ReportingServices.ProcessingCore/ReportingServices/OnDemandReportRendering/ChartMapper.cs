using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Reporting.Chart.Helpers;
using Microsoft.Reporting.Chart.WebForms;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000086 RID: 134
	internal class ChartMapper : MapperBase, IChartMapper, IDVMappingLayer, IDisposable
	{
		// Token: 0x06000794 RID: 1940 RVA: 0x0001BF4D File Offset: 0x0001A14D
		public ChartMapper(Chart chart, string defaultFontFamily)
			: base(defaultFontFamily)
		{
			this.m_chart = chart;
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x0001BF74 File Offset: 0x0001A174
		public void RenderChart()
		{
			try
			{
				if (this.m_chart != null)
				{
					this.InitializeChart();
					this.SetChartProperties();
					this.RenderChartStyle();
					this.RenderBorderSkin();
					this.RenderPalettes();
					this.RenderChartAreas();
					this.RenderLegends();
					this.RenderTitles();
					this.RenderAnnotations();
					this.RenderData();
					if (this.IsChartEmpty())
					{
						this.m_coreChart.Series.Clear();
						this.RenderNoDataMessage();
					}
					this.m_coreChart.SuppressExceptions = true;
				}
			}
			catch (RSException)
			{
				throw;
			}
			catch (Exception ex)
			{
				if (AsynchronousExceptionDetection.IsStoppingException(ex))
				{
					throw;
				}
				throw new RenderingObjectModelException(ex);
			}
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x0001C024 File Offset: 0x0001A224
		public Stream GetCoreXml()
		{
			Stream stream;
			try
			{
				this.m_coreChart.Serializer.Content = 7;
				this.m_coreChart.Serializer.NonSerializableContent = "";
				MemoryStream memoryStream = new MemoryStream();
				this.m_coreChart.Serializer.Save(memoryStream);
				memoryStream.Position = 0L;
				stream = memoryStream;
			}
			catch (Exception ex)
			{
				if (AsynchronousExceptionDetection.IsStoppingException(ex))
				{
					throw;
				}
				Global.Tracer.Trace(TraceLevel.Verbose, ex.Message);
				stream = null;
			}
			return stream;
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x0001C0AC File Offset: 0x0001A2AC
		public Stream GetImage(DynamicImageInstance.ImageType imageType)
		{
			Stream stream;
			try
			{
				if (this.m_coreChart == null)
				{
					stream = null;
				}
				else
				{
					ChartImageFormat chartImageFormat = 1;
					Stream stream2 = null;
					if (imageType != DynamicImageInstance.ImageType.PNG)
					{
						if (imageType == DynamicImageInstance.ImageType.EMF)
						{
							chartImageFormat = 4;
							stream2 = this.m_chart.RenderingContext.OdpContext.CreateStreamCallback(this.m_chart.Name, "emf", null, "image/emf", true, StreamOper.CreateOnly);
						}
					}
					else
					{
						chartImageFormat = 1;
						stream2 = new MemoryStream();
					}
					Chart coreChart = this.m_coreChart;
					coreChart.FormatNumberHandler = (FormatNumberHandler)Delegate.Combine(coreChart.FormatNumberHandler, new FormatNumberHandler(this.FormatNumber));
					this.m_coreChart.CustomizeLegend += new CustomizeLegendEventHandler(this.AdjustSeriesInLegend);
					this.m_coreChart.ImageResolution = base.DpiX;
					this.m_coreChart.Save(stream2, chartImageFormat);
					stream2.Position = 0L;
					stream = stream2;
				}
			}
			catch (RSException)
			{
				throw;
			}
			catch (Exception ex)
			{
				if (AsynchronousExceptionDetection.IsStoppingException(ex))
				{
					throw;
				}
				throw new RenderingObjectModelException(ex);
			}
			return stream;
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x0001C1AC File Offset: 0x0001A3AC
		private string FormatNumber(object sender, double value, string format, ChartValueTypes valueType, int elementId, ChartElementType elementType)
		{
			if (this.m_formatter == null)
			{
				this.m_formatter = new Formatter(this.m_chart.ChartDef.StyleClass, this.m_chart.RenderingContext.OdpContext, ObjectType.Chart, this.m_chart.Name);
			}
			bool flag = false;
			if (format.Length == 0)
			{
				if (valueType == 9 || valueType == 8 || valueType == 11)
				{
					format = "d";
				}
				else if (valueType == 10)
				{
					format = "t";
				}
				flag = valueType == 11;
			}
			TypeCode typeCode = this.GetTypeCode(valueType);
			object obj;
			if (typeCode == TypeCode.DateTime)
			{
				obj = DateTime.FromOADate(value);
			}
			else
			{
				obj = value;
			}
			return this.m_formatter.FormatValue(obj, format, typeCode, flag);
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x0001C263 File Offset: 0x0001A463
		private TypeCode GetTypeCode(ChartValueTypes chartValueType)
		{
			if (chartValueType - 8 <= 3)
			{
				return TypeCode.DateTime;
			}
			return TypeCode.Double;
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x0001C270 File Offset: 0x0001A470
		private ImageMapArea.ImageMapAreaShape GetMapAreaShape(MapAreaShape shape)
		{
			if (shape == null)
			{
				return ImageMapArea.ImageMapAreaShape.Rectangle;
			}
			if (1 == shape)
			{
				return ImageMapArea.ImageMapAreaShape.Circle;
			}
			return ImageMapArea.ImageMapAreaShape.Polygon;
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x0001C27E File Offset: 0x0001A47E
		public ActionInfoWithDynamicImageMapCollection GetImageMaps()
		{
			return MappingHelper.GetImageMaps(this.GetMapAreaInfoList(), this.m_actions, this.m_chart);
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0001C297 File Offset: 0x0001A497
		internal IEnumerable<MappingHelper.MapAreaInfo> GetMapAreaInfoList()
		{
			foreach (object obj in this.m_coreChart.MapAreas)
			{
				MapArea mapArea = (MapArea)obj;
				yield return new MappingHelper.MapAreaInfo(mapArea.ToolTip, mapArea.Tag, this.GetMapAreaShape(mapArea.Shape), mapArea.Coordinates);
			}
			IEnumerator enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x0001C2A8 File Offset: 0x0001A4A8
		private void InitializeChart()
		{
			this.m_coreChart = new Chart();
			if (RSTrace.ProcessingTracer.TraceVerbose)
			{
				((TraceManager)this.m_coreChart.GetService(typeof(TraceManager))).TraceContext = new ChartMapper.TraceContext();
			}
			this.m_coreChart.ChartAreas.Clear();
			this.m_coreChart.Series.Clear();
			this.m_coreChart.Titles.Clear();
			this.m_coreChart.Legends.Clear();
			this.m_coreChart.Annotations.Clear();
			this.OnPostInitialize();
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x0001C348 File Offset: 0x0001A548
		private void SetChartProperties()
		{
			int num = 300;
			int num2 = 300;
			if (base.WidthOverrideInPixels != null)
			{
				num = base.WidthOverrideInPixels.Value;
			}
			else if (this.m_chart.DynamicWidth != null)
			{
				if (!this.m_chart.DynamicWidth.IsExpression)
				{
					if (this.m_chart.DynamicWidth.Value != null)
					{
						num = MappingHelper.ToIntPixels(this.m_chart.DynamicWidth.Value, base.DpiX);
					}
				}
				else if (((ChartInstance)this.m_chart.Instance).DynamicWidth != null)
				{
					num = MappingHelper.ToIntPixels(((ChartInstance)this.m_chart.Instance).DynamicWidth, base.DpiX);
				}
			}
			this.m_coreChart.Width = num;
			if (base.HeightOverrideInPixels != null)
			{
				num2 = base.HeightOverrideInPixels.Value;
			}
			else if (this.m_chart.DynamicHeight != null)
			{
				if (!this.m_chart.DynamicHeight.IsExpression)
				{
					if (this.m_chart.DynamicHeight.Value != null)
					{
						num2 = MappingHelper.ToIntPixels(this.m_chart.DynamicHeight.Value, base.DpiY);
					}
				}
				else if (((ChartInstance)this.m_chart.Instance).DynamicHeight != null)
				{
					num2 = MappingHelper.ToIntPixels(((ChartInstance)this.m_chart.Instance).DynamicHeight, base.DpiY);
				}
			}
			this.m_coreChart.Height = num2;
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x0001C4D0 File Offset: 0x0001A6D0
		private void RenderNoDataMessage()
		{
			if (this.m_chart.NoDataMessage != null)
			{
				Title title = new Title();
				this.m_coreChart.Titles.Add(title);
				this.RenderTitle(this.m_chart.NoDataMessage, title);
			}
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0001C514 File Offset: 0x0001A714
		private void RenderPalettes()
		{
			this.RenderStandardPalettes();
			this.RenderCustomPalette();
			this.RenderPaletteHatchBehavior();
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x0001C528 File Offset: 0x0001A728
		private void RenderStandardPalettes()
		{
			if (this.m_chart.Palette == null)
			{
				this.m_coreChart.Palette = 1;
				return;
			}
			ChartPalette chartPalette;
			if (!this.m_chart.Palette.IsExpression)
			{
				chartPalette = this.m_chart.Palette.Value;
			}
			else
			{
				chartPalette = ((ChartInstance)this.m_chart.Instance).Palette;
			}
			switch (chartPalette)
			{
			case ChartPalette.Default:
				this.m_coreChart.Palette = 1;
				return;
			case ChartPalette.EarthTones:
				this.m_coreChart.Palette = 6;
				return;
			case ChartPalette.Excel:
				this.m_coreChart.Palette = 3;
				return;
			case ChartPalette.GrayScale:
				this.m_coreChart.Palette = 2;
				return;
			case ChartPalette.Light:
				this.m_coreChart.Palette = 4;
				return;
			case ChartPalette.Pastel:
				this.m_coreChart.Palette = 5;
				return;
			case ChartPalette.SemiTransparent:
				this.m_coreChart.Palette = 7;
				return;
			case ChartPalette.Berry:
				this.m_coreChart.Palette = 8;
				return;
			case ChartPalette.Chocolate:
				this.m_coreChart.Palette = 9;
				return;
			case ChartPalette.Fire:
				this.m_coreChart.Palette = 10;
				return;
			case ChartPalette.SeaGreen:
				this.m_coreChart.Palette = 11;
				return;
			case ChartPalette.BrightPastel:
				this.m_coreChart.Palette = 12;
				return;
			case ChartPalette.Pacific:
				this.m_coreChart.Palette = 13;
				return;
			case ChartPalette.PacificLight:
				this.m_coreChart.Palette = 14;
				return;
			case ChartPalette.PacificSemiTransparent:
				this.m_coreChart.Palette = 15;
				return;
			case ChartPalette.Custom:
				this.m_coreChart.Palette = 0;
				return;
			default:
				return;
			}
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x0001C6AC File Offset: 0x0001A8AC
		private void RenderPaletteHatchBehavior()
		{
			if (this.m_chart.PaletteHatchBehavior != null)
			{
				ReportEnumProperty<PaletteHatchBehavior> paletteHatchBehavior = this.m_chart.PaletteHatchBehavior;
				PaletteHatchBehavior paletteHatchBehavior2;
				if (!paletteHatchBehavior.IsExpression)
				{
					paletteHatchBehavior2 = paletteHatchBehavior.Value;
				}
				else
				{
					paletteHatchBehavior2 = ((ChartInstance)this.m_chart.Instance).PaletteHatchBehavior;
				}
				if (paletteHatchBehavior2 == PaletteHatchBehavior.Always)
				{
					this.m_hatcher = new ChartMapper.Hatcher();
				}
			}
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0001C70C File Offset: 0x0001A90C
		private void RenderCustomPalette()
		{
			if (this.m_chart.CustomPaletteColors == null)
			{
				return;
			}
			if (this.m_chart.CustomPaletteColors.Count == 0)
			{
				return;
			}
			if (this.m_coreChart.Palette != null)
			{
				return;
			}
			Color[] array = new Color[this.m_chart.CustomPaletteColors.Count];
			Color empty = Color.Empty;
			for (int i = 0; i < this.m_chart.CustomPaletteColors.Count; i++)
			{
				ChartCustomPaletteColor chartCustomPaletteColor = this.m_chart.CustomPaletteColors[i];
				ReportColorProperty color = chartCustomPaletteColor.Color;
				if (!color.IsExpression)
				{
					if (MappingHelper.GetColorFromReportColorProperty(color, ref empty))
					{
						array[i] = empty;
					}
				}
				else
				{
					array[i] = chartCustomPaletteColor.Instance.Color.ToColor();
				}
			}
			this.m_coreChart.PaletteCustomColors = array;
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0001C7E0 File Offset: 0x0001A9E0
		private void RenderChartStyle()
		{
			Border border = null;
			this.m_coreChart.BackColor = Color.Transparent;
			Style style = this.m_chart.Style;
			if (style != null)
			{
				StyleInstance style2 = this.m_chart.Instance.Style;
				if (MappingHelper.IsStylePropertyDefined(this.m_chart.Style.BackgroundColor))
				{
					this.m_coreChart.BackColor = MappingHelper.GetStyleBackgroundColor(style, style2);
				}
				if (MappingHelper.IsStylePropertyDefined(this.m_chart.Style.BackgroundGradientEndColor))
				{
					this.m_coreChart.BackGradientEndColor = MappingHelper.GetStyleBackGradientEndColor(style, style2);
				}
				if (MappingHelper.IsStylePropertyDefined(this.m_chart.Style.BackgroundGradientType))
				{
					this.m_coreChart.BackGradientType = this.GetGradientType(MappingHelper.GetStyleBackGradientType(style, style2));
				}
				if (MappingHelper.IsStylePropertyDefined(this.m_chart.Style.BackgroundHatchType))
				{
					this.m_coreChart.BackHatchStyle = this.GetHatchType(MappingHelper.GetStyleBackgroundHatchType(style, style2));
				}
				this.m_coreChart.RightToLeft = MappingHelper.GetStyleDirection(this.m_chart.Style, this.m_chart.Instance.Style);
				this.RenderChartBackgroundImage(this.m_chart.Style.BackgroundImage);
				border = this.m_chart.Style.Border;
			}
			if (this.m_coreChart.BackColor.A != 255)
			{
				this.m_coreChart.AntiAlias = 1;
			}
			if (this.m_chart.SpecialBorderHandling)
			{
				this.RenderChartBorder(border);
			}
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0001C95E File Offset: 0x0001AB5E
		private void RenderBorderSkin()
		{
			if (this.m_chart.BorderSkin != null)
			{
				this.RenderBorderSkinStyle(this.m_chart.BorderSkin);
				this.RenderBorderSkinType(this.m_chart.BorderSkin);
			}
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0001C990 File Offset: 0x0001AB90
		private void RenderBorderSkinType(ChartBorderSkin borderSkin)
		{
			if (borderSkin.BorderSkinType == null)
			{
				return;
			}
			if (!borderSkin.BorderSkinType.IsExpression)
			{
				ChartBorderSkinType value = borderSkin.BorderSkinType.Value;
			}
			else
			{
				ChartBorderSkinType borderSkinType = borderSkin.Instance.BorderSkinType;
			}
			BorderSkinStyle borderSkinStyle = 0;
			switch (borderSkin.Instance.BorderSkinType)
			{
			case ChartBorderSkinType.None:
				borderSkinStyle = 0;
				break;
			case ChartBorderSkinType.Emboss:
				borderSkinStyle = 1;
				break;
			case ChartBorderSkinType.Raised:
				borderSkinStyle = 2;
				break;
			case ChartBorderSkinType.Sunken:
				borderSkinStyle = 3;
				break;
			case ChartBorderSkinType.FrameThin1:
				borderSkinStyle = 4;
				break;
			case ChartBorderSkinType.FrameThin2:
				borderSkinStyle = 5;
				break;
			case ChartBorderSkinType.FrameThin3:
				borderSkinStyle = 6;
				break;
			case ChartBorderSkinType.FrameThin4:
				borderSkinStyle = 7;
				break;
			case ChartBorderSkinType.FrameThin5:
				borderSkinStyle = 8;
				break;
			case ChartBorderSkinType.FrameThin6:
				borderSkinStyle = 9;
				break;
			case ChartBorderSkinType.FrameTitle1:
				borderSkinStyle = 10;
				break;
			case ChartBorderSkinType.FrameTitle2:
				borderSkinStyle = 11;
				break;
			case ChartBorderSkinType.FrameTitle3:
				borderSkinStyle = 12;
				break;
			case ChartBorderSkinType.FrameTitle4:
				borderSkinStyle = 13;
				break;
			case ChartBorderSkinType.FrameTitle5:
				borderSkinStyle = 14;
				break;
			case ChartBorderSkinType.FrameTitle6:
				borderSkinStyle = 15;
				break;
			case ChartBorderSkinType.FrameTitle7:
				borderSkinStyle = 16;
				break;
			case ChartBorderSkinType.FrameTitle8:
				borderSkinStyle = 17;
				break;
			}
			this.m_coreChart.BorderSkin.SkinStyle = borderSkinStyle;
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x0001CA8C File Offset: 0x0001AC8C
		private void RenderBorderSkinStyle(ChartBorderSkin chartBorderSkin)
		{
			Style style = chartBorderSkin.Style;
			if (style == null)
			{
				return;
			}
			StyleInstance style2 = chartBorderSkin.Instance.Style;
			BorderSkinAttributes borderSkin = this.m_coreChart.BorderSkin;
			if (MappingHelper.IsStylePropertyDefined(chartBorderSkin.Style.Color))
			{
				borderSkin.PageColor = MappingHelper.GetStyleColor(style, style2);
			}
			if (MappingHelper.IsStylePropertyDefined(chartBorderSkin.Style.BackgroundColor))
			{
				borderSkin.FrameBackColor = MappingHelper.GetStyleBackgroundColor(style, style2);
			}
			if (MappingHelper.IsStylePropertyDefined(chartBorderSkin.Style.BackgroundGradientEndColor))
			{
				borderSkin.FrameBackGradientEndColor = MappingHelper.GetStyleBackGradientEndColor(style, style2);
			}
			if (MappingHelper.IsStylePropertyDefined(chartBorderSkin.Style.BackgroundGradientType))
			{
				borderSkin.FrameBackGradientType = this.GetGradientType(MappingHelper.GetStyleBackGradientType(style, style2));
			}
			if (MappingHelper.IsStylePropertyDefined(chartBorderSkin.Style.BackgroundHatchType))
			{
				borderSkin.FrameBackHatchStyle = this.GetHatchType(MappingHelper.GetStyleBackgroundHatchType(style, style2));
			}
			this.RenderBorderSkinBorder(chartBorderSkin.Style.Border, borderSkin);
			if (style.BackgroundImage != null)
			{
				this.RenderBorderSkinBackgroundImage(style.BackgroundImage, borderSkin);
			}
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x0001CB8C File Offset: 0x0001AD8C
		private void RenderChartAreas()
		{
			if (this.m_chart.ChartAreas == null || this.m_chart.ChartAreas.Count == 0)
			{
				this.m_coreChart.ChartAreas.Add(ChartMapper.m_defaulChartAreaName);
				return;
			}
			foreach (ChartArea chartArea in this.m_chart.ChartAreas)
			{
				this.RenderChartArea(chartArea);
			}
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x0001CC14 File Offset: 0x0001AE14
		private void RenderChartArea(ChartArea chartArea)
		{
			ChartArea chartArea2 = new ChartArea();
			this.m_coreChart.ChartAreas.Add(chartArea2);
			this.SetChartAreaProperties(chartArea, chartArea2);
			this.RenderElementPosition(chartArea.ChartElementPosition, chartArea2.Position);
			this.RenderElementPosition(chartArea.ChartInnerPlotPosition, chartArea2.InnerPlotPosition);
			this.RenderChartAreaStyle(chartArea, chartArea2);
			if (!this.m_chartAreaInfoDictionary.ContainsKey(chartArea2.Name))
			{
				this.m_chartAreaInfoDictionary.Add(chartArea2.Name, new ChartMapper.ChartAreaInfo());
			}
			this.RenderAxes(chartArea, chartArea2);
			this.Render3DProperties(chartArea.ThreeDProperties, chartArea2.Area3DStyle);
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x0001CCB0 File Offset: 0x0001AEB0
		private void SetChartAreaProperties(ChartArea chartArea, ChartArea area)
		{
			this.RenderAlignType(chartArea.ChartAlignType, area);
			if (chartArea.Name != null)
			{
				area.Name = chartArea.Name;
			}
			else
			{
				area.Name = ChartMapper.m_defaulChartAreaName;
			}
			if (chartArea.AlignOrientation != null)
			{
				if (!chartArea.AlignOrientation.IsExpression)
				{
					area.AlignOrientation = this.GetAreaAlignOrientation(chartArea.AlignOrientation.Value);
				}
				else
				{
					area.AlignOrientation = this.GetAreaAlignOrientation(chartArea.Instance.AlignOrientation);
				}
			}
			else
			{
				area.AlignOrientation = 0;
			}
			if (chartArea.AlignWithChartArea != null)
			{
				area.AlignWithChartArea = chartArea.AlignWithChartArea;
			}
			if (chartArea.EquallySizedAxesFont != null)
			{
				if (!chartArea.EquallySizedAxesFont.IsExpression)
				{
					area.EquallySizedAxesFont = chartArea.EquallySizedAxesFont.Value;
				}
				else
				{
					area.EquallySizedAxesFont = chartArea.Instance.EquallySizedAxesFont;
				}
			}
			if (chartArea.Hidden != null)
			{
				if (!chartArea.Hidden.IsExpression)
				{
					area.Visible = !chartArea.Hidden.Value;
					return;
				}
				area.Visible = !chartArea.Instance.Hidden;
			}
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x0001CDC4 File Offset: 0x0001AFC4
		private AreaAlignOrientations GetAreaAlignOrientation(ChartAreaAlignOrientations chartAreaOrientation)
		{
			switch (chartAreaOrientation)
			{
			case ChartAreaAlignOrientations.Vertical:
				return 1;
			case ChartAreaAlignOrientations.Horizontal:
				return 2;
			case ChartAreaAlignOrientations.All:
				return 3;
			default:
				return 0;
			}
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0001CDE4 File Offset: 0x0001AFE4
		private void RenderAlignType(ChartAlignType chartAlignType, ChartArea area)
		{
			area.AlignType = 0;
			if (chartAlignType == null)
			{
				return;
			}
			if (chartAlignType.AxesView != null)
			{
				if (!chartAlignType.AxesView.IsExpression)
				{
					if (chartAlignType.AxesView.Value)
					{
						area.AlignType |= 8;
					}
				}
				else if (chartAlignType.Instance.AxesView)
				{
					area.AlignType |= 8;
				}
			}
			if (chartAlignType.Cursor != null)
			{
				if (!chartAlignType.Cursor.IsExpression)
				{
					if (chartAlignType.Cursor.Value)
					{
						area.AlignType |= 4;
					}
				}
				else if (chartAlignType.Instance.Cursor)
				{
					area.AlignType |= 4;
				}
			}
			if (chartAlignType.InnerPlotPosition != null)
			{
				if (!chartAlignType.InnerPlotPosition.IsExpression)
				{
					if (chartAlignType.InnerPlotPosition.Value)
					{
						area.AlignType |= 2;
					}
				}
				else if (chartAlignType.Instance.InnerPlotPosition)
				{
					area.AlignType |= 2;
				}
			}
			if (chartAlignType.Position != null)
			{
				if (!chartAlignType.Position.IsExpression)
				{
					if (chartAlignType.Position.Value)
					{
						area.AlignType |= 1;
						return;
					}
				}
				else if (chartAlignType.Instance.Position)
				{
					area.AlignType |= 1;
				}
			}
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0001CF30 File Offset: 0x0001B130
		private void RenderChartAreaStyle(ChartArea chartArea, ChartArea area)
		{
			Style style = chartArea.Style;
			if (style == null)
			{
				return;
			}
			StyleInstance style2 = chartArea.Instance.Style;
			if (MappingHelper.IsStylePropertyDefined(chartArea.Style.BackgroundColor))
			{
				area.BackColor = MappingHelper.GetStyleBackgroundColor(style, style2);
			}
			if (MappingHelper.IsStylePropertyDefined(chartArea.Style.BackgroundGradientEndColor))
			{
				area.BackGradientEndColor = MappingHelper.GetStyleBackGradientEndColor(style, style2);
			}
			if (MappingHelper.IsStylePropertyDefined(chartArea.Style.BackgroundGradientType))
			{
				area.BackGradientType = this.GetGradientType(MappingHelper.GetStyleBackGradientType(style, style2));
			}
			if (MappingHelper.IsStylePropertyDefined(chartArea.Style.BackgroundHatchType))
			{
				area.BackHatchStyle = this.GetHatchType(MappingHelper.GetStyleBackgroundHatchType(style, style2));
			}
			if (MappingHelper.IsStylePropertyDefined(chartArea.Style.ShadowColor))
			{
				area.ShadowColor = MappingHelper.GetStyleShadowColor(style, style2);
			}
			if (MappingHelper.IsStylePropertyDefined(chartArea.Style.ShadowOffset))
			{
				area.ShadowOffset = MappingHelper.GetStyleShadowOffset(style, style2, base.DpiX);
			}
			this.RenderChartAreaBorder(chartArea.Style.Border, area);
			this.RenderChartAreaBackgroundImage(chartArea.Style.BackgroundImage, area);
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x0001D044 File Offset: 0x0001B244
		private void RenderAxes(ChartArea chartArea, ChartArea area)
		{
			if (chartArea.CategoryAxes != null)
			{
				this.RenderCategoryAxes(chartArea.CategoryAxes, area);
			}
			if (chartArea.ValueAxes != null)
			{
				this.RenderValueAxes(chartArea.ValueAxes, area);
			}
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x0001D070 File Offset: 0x0001B270
		private void RenderCategoryAxes(ChartAxisCollection categoryAxes, ChartArea area)
		{
			bool flag = false;
			foreach (ChartAxis chartAxis in categoryAxes)
			{
				if (chartAxis.Location != null)
				{
					if (!chartAxis.Location.IsExpression)
					{
						if (chartAxis.Location.Value == ChartAxisLocation.Default && !flag)
						{
							this.RenderAxis(chartAxis, area.AxisX, area, true);
							flag = true;
						}
						else
						{
							this.RenderAxis(chartAxis, area.AxisX2, area, true);
						}
					}
					else if (chartAxis.Instance.Location == ChartAxisLocation.Default && !flag)
					{
						this.RenderAxis(chartAxis, area.AxisX, area, true);
						flag = true;
					}
					else
					{
						this.RenderAxis(chartAxis, area.AxisX2, area, true);
					}
				}
				else if (!flag)
				{
					this.RenderAxis(chartAxis, area.AxisX, area, true);
					flag = true;
				}
				else
				{
					this.RenderAxis(chartAxis, area.AxisX2, area, true);
				}
			}
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x0001D15C File Offset: 0x0001B35C
		private void RenderValueAxes(ChartAxisCollection valueAxes, ChartArea area)
		{
			bool flag = false;
			foreach (ChartAxis chartAxis in valueAxes)
			{
				if (chartAxis.Location != null)
				{
					if (!chartAxis.Location.IsExpression)
					{
						if (chartAxis.Location.Value == ChartAxisLocation.Default && !flag)
						{
							this.RenderAxis(chartAxis, area.AxisY, area, false);
							flag = true;
						}
						else
						{
							this.RenderAxis(chartAxis, area.AxisY2, area, false);
						}
					}
					else if (chartAxis.Instance.Location == ChartAxisLocation.Default && !flag)
					{
						this.RenderAxis(chartAxis, area.AxisY, area, false);
						flag = true;
					}
					else
					{
						this.RenderAxis(chartAxis, area.AxisY2, area, false);
					}
				}
				else if (!flag)
				{
					this.RenderAxis(chartAxis, area.AxisY, area, false);
					flag = true;
				}
				else
				{
					this.RenderAxis(chartAxis, area.AxisY2, area, false);
				}
			}
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x0001D248 File Offset: 0x0001B448
		private void Render3DProperties(ChartThreeDProperties chartThreeDProperties, ChartArea3DStyle threeDProperties)
		{
			if (chartThreeDProperties == null)
			{
				return;
			}
			if (chartThreeDProperties.Clustered != null)
			{
				if (!chartThreeDProperties.Clustered.IsExpression)
				{
					threeDProperties.Clustered = chartThreeDProperties.Clustered.Value;
				}
				else
				{
					threeDProperties.Clustered = chartThreeDProperties.Instance.Clustered;
				}
			}
			if (chartThreeDProperties.DepthRatio != null)
			{
				if (!chartThreeDProperties.DepthRatio.IsExpression)
				{
					threeDProperties.PointDepth = chartThreeDProperties.DepthRatio.Value;
				}
				else
				{
					threeDProperties.PointDepth = chartThreeDProperties.Instance.DepthRatio;
				}
			}
			if (chartThreeDProperties.Enabled != null)
			{
				if (!chartThreeDProperties.Enabled.IsExpression)
				{
					threeDProperties.Enable3D = chartThreeDProperties.Enabled.Value;
				}
				else
				{
					threeDProperties.Enable3D = chartThreeDProperties.Instance.Enabled;
				}
			}
			if (chartThreeDProperties.GapDepth != null)
			{
				if (!chartThreeDProperties.GapDepth.IsExpression)
				{
					threeDProperties.PointGapDepth = chartThreeDProperties.GapDepth.Value;
				}
				else
				{
					threeDProperties.PointGapDepth = chartThreeDProperties.Instance.GapDepth;
				}
			}
			if (chartThreeDProperties.Inclination != null)
			{
				if (!chartThreeDProperties.Inclination.IsExpression)
				{
					threeDProperties.XAngle = chartThreeDProperties.Inclination.Value;
				}
				else
				{
					threeDProperties.XAngle = chartThreeDProperties.Instance.Inclination;
				}
			}
			if (chartThreeDProperties.Perspective != null)
			{
				if (!chartThreeDProperties.Perspective.IsExpression)
				{
					threeDProperties.Perspective = chartThreeDProperties.Perspective.Value;
				}
				else
				{
					threeDProperties.Perspective = chartThreeDProperties.Instance.Perspective;
				}
			}
			if (chartThreeDProperties.ProjectionMode != null)
			{
				ChartThreeDProjectionModes chartThreeDProjectionModes;
				if (!chartThreeDProperties.ProjectionMode.IsExpression)
				{
					chartThreeDProjectionModes = chartThreeDProperties.ProjectionMode.Value;
				}
				else
				{
					chartThreeDProjectionModes = chartThreeDProperties.Instance.ProjectionMode;
				}
				threeDProperties.RightAngleAxes = chartThreeDProjectionModes == ChartThreeDProjectionModes.Oblique;
			}
			else
			{
				threeDProperties.RightAngleAxes = true;
			}
			if (chartThreeDProperties.Rotation != null)
			{
				if (!chartThreeDProperties.Rotation.IsExpression)
				{
					threeDProperties.YAngle = chartThreeDProperties.Rotation.Value;
				}
				else
				{
					threeDProperties.YAngle = chartThreeDProperties.Instance.Rotation;
				}
			}
			if (chartThreeDProperties.Shading != null)
			{
				if (!chartThreeDProperties.Shading.IsExpression)
				{
					threeDProperties.Light = this.GetThreeDLight(chartThreeDProperties.Shading.Value);
				}
				else
				{
					threeDProperties.Light = this.GetThreeDLight(chartThreeDProperties.Instance.Shading);
				}
			}
			else
			{
				threeDProperties.Light = 2;
			}
			if (chartThreeDProperties.WallThickness != null)
			{
				if (!chartThreeDProperties.WallThickness.IsExpression)
				{
					threeDProperties.WallWidth = chartThreeDProperties.WallThickness.Value;
					return;
				}
				threeDProperties.WallWidth = chartThreeDProperties.Instance.WallThickness;
			}
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x0001D4B2 File Offset: 0x0001B6B2
		private LightStyle GetThreeDLight(ChartThreeDShadingTypes shading)
		{
			if (shading == ChartThreeDShadingTypes.Real)
			{
				return 2;
			}
			if (shading != ChartThreeDShadingTypes.Simple)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x0001D4C4 File Offset: 0x0001B6C4
		private void RenderAxis(ChartAxis chartAxis, Axis axis, ChartArea area, bool isCategory)
		{
			this.RenderAxisStyle(chartAxis, axis);
			this.RenderAxisTitle(chartAxis.Title, axis);
			this.RenderAxisStripLines(chartAxis, axis);
			this.RenderAxisGridLines(chartAxis.MajorGridLines, axis.MajorGrid, true);
			this.RenderAxisGridLines(chartAxis.MinorGridLines, axis.MinorGrid, false);
			this.RenderAxisTickMarks(chartAxis.MajorTickMarks, axis.MajorTickMark, true);
			this.RenderAxisTickMarks(chartAxis.MinorTickMarks, axis.MinorTickMark, false);
			this.RenderAxisScaleBreak(chartAxis.AxisScaleBreak, axis.ScaleBreakStyle);
			this.RenderCustomProperties(chartAxis.CustomProperties, axis);
			this.SetAxisProperties(chartAxis, axis, area, isCategory);
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x0001D564 File Offset: 0x0001B764
		private void SetAxisProperties(ChartAxis chartAxis, Axis axis, ChartArea area, bool isCategory)
		{
			this.SetAxisArrow(chartAxis, axis);
			this.SetAxisCrossing(chartAxis, axis);
			this.SetAxisLabelsProperties(chartAxis, axis);
			if (chartAxis.IncludeZero != null)
			{
				if (!chartAxis.IncludeZero.IsExpression)
				{
					axis.StartFromZero = chartAxis.IncludeZero.Value;
				}
				else
				{
					axis.StartFromZero = chartAxis.Instance.IncludeZero;
				}
			}
			if (chartAxis.Interlaced != null)
			{
				if (!chartAxis.Interlaced.IsExpression)
				{
					axis.Interlaced = chartAxis.Interlaced.Value;
				}
				else
				{
					axis.Interlaced = chartAxis.Instance.Interlaced;
				}
			}
			if (chartAxis.InterlacedColor != null)
			{
				Color empty = Color.Empty;
				if (MappingHelper.GetColorFromReportColorProperty(chartAxis.InterlacedColor, ref empty))
				{
					axis.InterlacedColor = empty;
				}
				else if (chartAxis.Instance.InterlacedColor != null)
				{
					axis.InterlacedColor = chartAxis.Instance.InterlacedColor.ToColor();
				}
			}
			if (chartAxis.VariableAutoInterval != null)
			{
				if (!chartAxis.VariableAutoInterval.IsExpression)
				{
					axis.IntervalAutoMode = this.GetIntervalAutoMode(chartAxis.VariableAutoInterval.Value);
				}
				else
				{
					axis.IntervalAutoMode = this.GetIntervalAutoMode(chartAxis.Instance.VariableAutoInterval);
				}
			}
			if (chartAxis.Interval != null)
			{
				double num;
				if (!chartAxis.Interval.IsExpression)
				{
					num = chartAxis.Interval.Value;
				}
				else
				{
					num = chartAxis.Instance.Interval;
				}
				if (num == 0.0)
				{
					num = double.NaN;
				}
				axis.Interval = num;
			}
			else
			{
				axis.Interval = double.NaN;
			}
			if (chartAxis.IntervalType != null)
			{
				if (!chartAxis.IntervalType.IsExpression)
				{
					axis.IntervalType = this.GetDateTimeIntervalType(chartAxis.IntervalType.Value);
				}
				else
				{
					axis.IntervalType = this.GetDateTimeIntervalType(chartAxis.Instance.IntervalType);
				}
			}
			if (chartAxis.IntervalOffset != null)
			{
				if (!chartAxis.IntervalOffset.IsExpression)
				{
					axis.IntervalOffset = chartAxis.IntervalOffset.Value;
				}
				else
				{
					axis.IntervalOffset = chartAxis.Instance.IntervalOffset;
				}
			}
			if (chartAxis.IntervalOffsetType != null)
			{
				if (!chartAxis.IntervalOffsetType.IsExpression)
				{
					axis.IntervalOffsetType = this.GetDateTimeIntervalType(chartAxis.IntervalOffsetType.Value);
				}
				else
				{
					axis.IntervalOffsetType = this.GetDateTimeIntervalType(chartAxis.Instance.IntervalOffsetType);
				}
			}
			if (chartAxis.LabelInterval != null)
			{
				double num2;
				if (!chartAxis.LabelInterval.IsExpression)
				{
					num2 = chartAxis.LabelInterval.Value;
				}
				else
				{
					num2 = chartAxis.Instance.LabelInterval;
				}
				if (num2 == 0.0)
				{
					num2 = double.NaN;
				}
				axis.LabelStyle.Interval = num2;
			}
			else
			{
				axis.LabelStyle.Interval = double.NaN;
			}
			if (chartAxis.LabelIntervalType != null)
			{
				if (!chartAxis.LabelIntervalType.IsExpression)
				{
					axis.LabelStyle.IntervalType = this.GetDateTimeIntervalType(chartAxis.LabelIntervalType.Value);
				}
				else
				{
					axis.LabelStyle.IntervalType = this.GetDateTimeIntervalType(chartAxis.Instance.LabelIntervalType);
				}
			}
			if (chartAxis.LabelIntervalOffset != null)
			{
				if (!chartAxis.LabelIntervalOffset.IsExpression)
				{
					axis.LabelStyle.IntervalOffset = chartAxis.LabelIntervalOffset.Value;
				}
				else
				{
					axis.LabelStyle.IntervalOffset = chartAxis.Instance.LabelIntervalOffset;
				}
			}
			if (chartAxis.LabelIntervalOffsetType != null)
			{
				if (!chartAxis.LabelIntervalOffsetType.IsExpression)
				{
					axis.LabelStyle.IntervalOffsetType = this.GetDateTimeIntervalType(chartAxis.LabelIntervalOffsetType.Value);
				}
				else
				{
					axis.LabelStyle.IntervalOffsetType = this.GetDateTimeIntervalType(chartAxis.Instance.LabelIntervalOffsetType);
				}
			}
			if (chartAxis.LogBase != null)
			{
				if (!chartAxis.LogBase.IsExpression)
				{
					axis.LogarithmBase = chartAxis.LogBase.Value;
				}
				else
				{
					axis.LogarithmBase = chartAxis.Instance.LogBase;
				}
			}
			if (chartAxis.LogScale != null)
			{
				if (!chartAxis.LogScale.IsExpression)
				{
					axis.Logarithmic = chartAxis.LogScale.Value;
				}
				else
				{
					axis.Logarithmic = chartAxis.Instance.LogScale;
				}
			}
			ChartAutoBool chartAutoBool;
			if (chartAxis.Margin != null)
			{
				if (!chartAxis.Margin.IsExpression)
				{
					chartAutoBool = chartAxis.Margin.Value;
				}
				else
				{
					chartAutoBool = chartAxis.Instance.Margin;
				}
			}
			else
			{
				chartAutoBool = ChartAutoBool.Auto;
			}
			if (chartAutoBool == ChartAutoBool.Auto && isCategory)
			{
				List<Axis> list = this.m_chartAreaInfoDictionary[area.Name].CategoryAxesAutoMargin;
				if (list == null)
				{
					list = new List<Axis>();
					this.m_chartAreaInfoDictionary[area.Name].CategoryAxesAutoMargin = list;
				}
				list.Add(axis);
				axis.Margin = false;
			}
			else
			{
				axis.Margin = this.GetMargin(chartAutoBool);
			}
			if (chartAxis.MarksAlwaysAtPlotEdge != null)
			{
				if (!chartAxis.MarksAlwaysAtPlotEdge.IsExpression)
				{
					axis.MarksNextToAxis = !chartAxis.MarksAlwaysAtPlotEdge.Value;
				}
				else
				{
					axis.MarksNextToAxis = !chartAxis.Instance.MarksAlwaysAtPlotEdge;
				}
			}
			if (chartAxis.Maximum != null)
			{
				if (!chartAxis.Maximum.IsExpression)
				{
					axis.Maximum = this.ConvertToDouble(chartAxis.Maximum.Value);
				}
				else
				{
					axis.Maximum = this.ConvertToDouble(chartAxis.Instance.Maximum);
				}
			}
			if (chartAxis.Minimum != null)
			{
				if (!chartAxis.Minimum.IsExpression)
				{
					axis.Minimum = this.ConvertToDouble(chartAxis.Minimum.Value);
				}
				else
				{
					axis.Minimum = this.ConvertToDouble(chartAxis.Instance.Minimum);
				}
			}
			if (chartAxis.Name != null)
			{
				axis.Name = chartAxis.Name;
			}
			if (chartAxis.Reverse != null)
			{
				if (!chartAxis.Reverse.IsExpression)
				{
					axis.Reverse = chartAxis.Reverse.Value;
				}
				else
				{
					axis.Reverse = chartAxis.Instance.Reverse;
				}
			}
			if (chartAxis.Scalar && isCategory && this.m_chartAreaInfoDictionary.ContainsKey(area.Name))
			{
				ChartMapper.ChartAreaInfo chartAreaInfo = this.m_chartAreaInfoDictionary[area.Name];
				if (chartAreaInfo.CategoryAxesScalar == null)
				{
					chartAreaInfo.CategoryAxesScalar = new List<string>();
				}
				chartAreaInfo.CategoryAxesScalar.Add(chartAxis.Name);
			}
			if (chartAxis.Visible != null)
			{
				if (!chartAxis.Visible.IsExpression)
				{
					axis.Enabled = this.GetAxisEnabled(chartAxis.Visible.Value);
				}
				else
				{
					axis.Enabled = this.GetAxisEnabled(chartAxis.Instance.Visible);
				}
			}
			this.SetAxisLabelAutoFitStyle(chartAxis, axis);
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x0001DBB4 File Offset: 0x0001BDB4
		private void SetAxisLabelsProperties(ChartAxis chartAxis, Axis axis)
		{
			if (chartAxis.HideLabels != null)
			{
				if (!chartAxis.HideLabels.IsExpression)
				{
					axis.LabelStyle.Enabled = !chartAxis.HideLabels.Value;
				}
				else
				{
					axis.LabelStyle.Enabled = !chartAxis.Instance.HideLabels;
				}
			}
			if (chartAxis.OffsetLabels != null)
			{
				if (!chartAxis.OffsetLabels.IsExpression)
				{
					axis.LabelStyle.OffsetLabels = chartAxis.OffsetLabels.Value;
				}
				else
				{
					axis.LabelStyle.OffsetLabels = chartAxis.Instance.OffsetLabels;
				}
			}
			if (chartAxis.HideEndLabels != null)
			{
				if (!chartAxis.HideEndLabels.IsExpression)
				{
					axis.LabelStyle.ShowEndLabels = !chartAxis.HideEndLabels.Value;
				}
				else
				{
					axis.LabelStyle.ShowEndLabels = !chartAxis.Instance.HideEndLabels;
				}
			}
			if (chartAxis.Angle != null)
			{
				if (!chartAxis.Angle.IsExpression)
				{
					axis.LabelStyle.FontAngle = (int)Math.Round(chartAxis.Angle.Value);
					return;
				}
				axis.LabelStyle.FontAngle = (int)Math.Round(chartAxis.Instance.Angle);
			}
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x0001DCE4 File Offset: 0x0001BEE4
		private void RenderAxisLabelFont(ChartAxis chartAxis, Axis axis)
		{
			Style style = chartAxis.Style;
			if (style == null)
			{
				axis.LabelStyle.Font = base.GetDefaultFont();
				return;
			}
			axis.LabelStyle.Font = base.GetFont(style, chartAxis.Instance.Style);
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x0001DD2C File Offset: 0x0001BF2C
		private void SetAxisCrossing(ChartAxis chartAxis, Axis axis)
		{
			if (chartAxis.CrossAt != null)
			{
				if (!chartAxis.CrossAt.IsExpression)
				{
					if (chartAxis.CrossAt.Value != null)
					{
						double num = this.ConvertToDouble(chartAxis.CrossAt.Value, true);
						if (!double.IsNaN(num))
						{
							axis.Crossing = num;
							return;
						}
					}
				}
				else if (chartAxis.Instance.CrossAt != null)
				{
					double num = this.ConvertToDouble(chartAxis.Instance.CrossAt, true);
					if (!double.IsNaN(num))
					{
						axis.Crossing = num;
					}
				}
			}
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x0001DDB0 File Offset: 0x0001BFB0
		private void SetAxisLabelAutoFitStyle(ChartAxis chartAxis, Axis axis)
		{
			if (chartAxis.MaxFontSize != null)
			{
				if (!chartAxis.MaxFontSize.IsExpression)
				{
					axis.LabelsAutoFitMaxFontSize = (int)Math.Round(chartAxis.MaxFontSize.Value.ToPoints());
				}
				else
				{
					axis.LabelsAutoFitMaxFontSize = (int)Math.Round(chartAxis.Instance.MaxFontSize.ToPoints());
				}
			}
			if (chartAxis.MinFontSize != null)
			{
				if (!chartAxis.MinFontSize.IsExpression)
				{
					axis.LabelsAutoFitMinFontSize = (int)Math.Round(chartAxis.MinFontSize.Value.ToPoints());
				}
				else
				{
					axis.LabelsAutoFitMinFontSize = (int)Math.Round(chartAxis.Instance.MinFontSize.ToPoints());
				}
			}
			axis.LabelsAutoFitStyle = 0;
			if (chartAxis.PreventFontGrow != null)
			{
				bool flag;
				if (!chartAxis.PreventFontGrow.IsExpression)
				{
					flag = chartAxis.PreventFontGrow.Value;
				}
				else
				{
					flag = chartAxis.Instance.PreventFontGrow;
				}
				if (!flag)
				{
					axis.LabelsAutoFitStyle |= 1;
				}
			}
			else
			{
				axis.LabelsAutoFitStyle |= 1;
			}
			if (chartAxis.PreventFontShrink != null)
			{
				bool flag2;
				if (!chartAxis.PreventFontShrink.IsExpression)
				{
					flag2 = chartAxis.PreventFontShrink.Value;
				}
				else
				{
					flag2 = chartAxis.Instance.PreventFontShrink;
				}
				if (!flag2)
				{
					axis.LabelsAutoFitStyle |= 2;
				}
			}
			else
			{
				axis.LabelsAutoFitStyle |= 2;
			}
			if (chartAxis.PreventLabelOffset != null)
			{
				bool flag3;
				if (!chartAxis.PreventLabelOffset.IsExpression)
				{
					flag3 = chartAxis.PreventLabelOffset.Value;
				}
				else
				{
					flag3 = chartAxis.Instance.PreventLabelOffset;
				}
				if (!flag3)
				{
					axis.LabelsAutoFitStyle |= 4;
				}
			}
			else
			{
				axis.LabelsAutoFitStyle |= 4;
			}
			if (chartAxis.AllowLabelRotation != null)
			{
				ChartAxisLabelRotation chartAxisLabelRotation;
				if (!chartAxis.AllowLabelRotation.IsExpression)
				{
					chartAxisLabelRotation = chartAxis.AllowLabelRotation.Value;
				}
				else
				{
					chartAxisLabelRotation = chartAxis.Instance.AllowLabelRotation;
				}
				switch (chartAxisLabelRotation)
				{
				case ChartAxisLabelRotation.Rotate30:
					axis.LabelsAutoFitStyle |= 8;
					break;
				case ChartAxisLabelRotation.Rotate45:
					axis.LabelsAutoFitStyle |= 16;
					break;
				case ChartAxisLabelRotation.Rotate90:
					axis.LabelsAutoFitStyle |= 32;
					break;
				}
			}
			else
			{
				axis.LabelsAutoFitStyle |= ChartMapper.m_defaultLabelsAngleStep;
			}
			if (chartAxis.PreventWordWrap != null)
			{
				bool flag4;
				if (!chartAxis.PreventWordWrap.IsExpression)
				{
					flag4 = chartAxis.PreventWordWrap.Value;
				}
				else
				{
					flag4 = chartAxis.Instance.PreventWordWrap;
				}
				if (!flag4)
				{
					axis.LabelsAutoFitStyle |= 64;
				}
			}
			else
			{
				axis.LabelsAutoFitStyle |= 64;
			}
			if (chartAxis.LabelsAutoFitDisabled == null)
			{
				axis.LabelsAutoFit = true;
				return;
			}
			if (!chartAxis.LabelsAutoFitDisabled.IsExpression)
			{
				axis.LabelsAutoFit = !chartAxis.LabelsAutoFitDisabled.Value;
				return;
			}
			axis.LabelsAutoFit = !chartAxis.Instance.LabelsAutoFitDisabled;
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x0001E078 File Offset: 0x0001C278
		private void SetAxisArrow(ChartAxis chartAxis, Axis axis)
		{
			if (chartAxis.Arrows != null)
			{
				ChartAxisArrow chartAxisArrow;
				if (!chartAxis.Arrows.IsExpression)
				{
					chartAxisArrow = chartAxis.Arrows.Value;
				}
				else
				{
					chartAxisArrow = chartAxis.Instance.Arrows;
				}
				switch (chartAxisArrow)
				{
				case ChartAxisArrow.None:
					axis.Arrows = 0;
					return;
				case ChartAxisArrow.Triangle:
					axis.Arrows = 1;
					break;
				case ChartAxisArrow.SharpTriangle:
					axis.Arrows = 2;
					return;
				case ChartAxisArrow.Lines:
					axis.Arrows = 3;
					return;
				default:
					return;
				}
			}
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x0001E0EC File Offset: 0x0001C2EC
		private void RenderAxisStripLines(ChartAxis chartAxis, Axis axis)
		{
			if (chartAxis.StripLines != null)
			{
				foreach (ChartStripLine chartStripLine in chartAxis.StripLines)
				{
					StripLine stripLine = new StripLine();
					this.RenderStripLine(chartStripLine, stripLine);
					axis.StripLines.Add(stripLine);
				}
			}
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x0001E158 File Offset: 0x0001C358
		private void RenderStripLine(ChartStripLine chartStripLine, StripLine stripLine)
		{
			this.SetStripLineProperties(chartStripLine, stripLine);
			this.RenderStripLineStyle(chartStripLine, stripLine);
			this.RenderActionInfo(chartStripLine.ActionInfo, stripLine.ToolTip, stripLine);
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x0001E180 File Offset: 0x0001C380
		private void SetStripLineProperties(ChartStripLine chartStripLine, StripLine stripLine)
		{
			if (chartStripLine.Interval != null)
			{
				if (!chartStripLine.Interval.IsExpression)
				{
					stripLine.Interval = chartStripLine.Interval.Value;
				}
				else
				{
					stripLine.Interval = chartStripLine.Instance.Interval;
				}
			}
			if (chartStripLine.IntervalType != null)
			{
				if (!chartStripLine.IntervalType.IsExpression)
				{
					stripLine.IntervalType = this.GetDateTimeIntervalType(chartStripLine.IntervalType.Value);
				}
				else
				{
					stripLine.IntervalType = this.GetDateTimeIntervalType(chartStripLine.Instance.IntervalType);
				}
			}
			if (chartStripLine.IntervalOffset != null)
			{
				if (!chartStripLine.IntervalOffset.IsExpression)
				{
					stripLine.IntervalOffset = chartStripLine.IntervalOffset.Value;
				}
				else
				{
					stripLine.IntervalOffset = chartStripLine.Instance.IntervalOffset;
				}
			}
			if (chartStripLine.IntervalOffsetType != null)
			{
				if (!chartStripLine.IntervalOffsetType.IsExpression)
				{
					stripLine.IntervalOffsetType = this.GetDateTimeIntervalType(chartStripLine.IntervalOffsetType.Value);
				}
				else
				{
					stripLine.IntervalOffsetType = this.GetDateTimeIntervalType(chartStripLine.Instance.IntervalOffsetType);
				}
			}
			if (chartStripLine.StripWidth != null)
			{
				if (!chartStripLine.StripWidth.IsExpression)
				{
					stripLine.StripWidth = chartStripLine.StripWidth.Value;
				}
				else
				{
					stripLine.StripWidth = chartStripLine.Instance.StripWidth;
				}
			}
			if (chartStripLine.StripWidthType != null)
			{
				if (!chartStripLine.StripWidthType.IsExpression)
				{
					stripLine.StripWidthType = this.GetDateTimeIntervalType(chartStripLine.StripWidthType.Value);
				}
				else
				{
					stripLine.StripWidthType = this.GetDateTimeIntervalType(chartStripLine.Instance.StripWidthType);
				}
			}
			if (chartStripLine.Title != null)
			{
				if (!chartStripLine.Title.IsExpression)
				{
					if (chartStripLine.Title.Value != null)
					{
						stripLine.Title = chartStripLine.Title.Value;
					}
				}
				else if (chartStripLine.Instance.Title != null)
				{
					stripLine.Title = chartStripLine.Instance.Title;
				}
			}
			if (chartStripLine.ToolTip != null)
			{
				if (!chartStripLine.ToolTip.IsExpression)
				{
					if (chartStripLine.ToolTip.Value != null)
					{
						stripLine.ToolTip = chartStripLine.ToolTip.Value;
					}
				}
				else if (chartStripLine.Instance.ToolTip != null)
				{
					stripLine.ToolTip = chartStripLine.Instance.ToolTip;
				}
			}
			if (chartStripLine.TextOrientation != null)
			{
				if (!chartStripLine.TextOrientation.IsExpression)
				{
					stripLine.TextOrientation = this.GetTextOrientation(chartStripLine.TextOrientation.Value);
					return;
				}
				stripLine.TextOrientation = this.GetTextOrientation(chartStripLine.Instance.TextOrientation);
			}
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x0001E3F4 File Offset: 0x0001C5F4
		private void RenderStripLineStyle(ChartStripLine chartStripLine, StripLine stripLine)
		{
			stripLine.TitleAlignment = StringAlignment.Near;
			Style style = chartStripLine.Style;
			Border border = null;
			if (style != null)
			{
				StyleInstance style2 = chartStripLine.Instance.Style;
				if (MappingHelper.IsStylePropertyDefined(chartStripLine.Style.Color))
				{
					stripLine.TitleColor = MappingHelper.GetStyleColor(style, style2);
				}
				if (MappingHelper.IsStylePropertyDefined(chartStripLine.Style.BackgroundColor))
				{
					stripLine.BackColor = MappingHelper.GetStyleBackgroundColor(style, style2);
				}
				if (MappingHelper.IsStylePropertyDefined(chartStripLine.Style.BackgroundGradientEndColor))
				{
					stripLine.BackGradientEndColor = MappingHelper.GetStyleBackGradientEndColor(style, style2);
				}
				if (MappingHelper.IsStylePropertyDefined(chartStripLine.Style.BackgroundGradientType))
				{
					stripLine.BackGradientType = this.GetGradientType(MappingHelper.GetStyleBackGradientType(style, style2));
				}
				if (MappingHelper.IsStylePropertyDefined(chartStripLine.Style.BackgroundHatchType))
				{
					stripLine.BackHatchStyle = this.GetHatchType(MappingHelper.GetStyleBackgroundHatchType(style, style2));
				}
				if (MappingHelper.IsStylePropertyDefined(chartStripLine.Style.TextAlign))
				{
					stripLine.TitleAlignment = this.GetStringAlignmentFromTextAlignments(MappingHelper.GetStyleTextAlign(style, style2));
				}
				if (MappingHelper.IsStylePropertyDefined(chartStripLine.Style.VerticalAlign))
				{
					stripLine.TitleLineAlignment = this.GetStringAlignmentFromVericalAlignments(MappingHelper.GetStyleVerticalAlignment(style, style2));
				}
				this.RenderStripLineBackgroundImage(chartStripLine.Style.BackgroundImage, stripLine);
				border = style.Border;
			}
			this.RenderStripLineBorder(border, stripLine);
			this.RenderStripLineFont(chartStripLine, stripLine);
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x0001E53D File Offset: 0x0001C73D
		private void RenderAxisTitle(ChartAxisTitle chartAxisTitle, Axis axis)
		{
			if (chartAxisTitle == null)
			{
				return;
			}
			this.SetAxisTitleProperties(chartAxisTitle, axis);
			this.RenderAxisTitleStyle(chartAxisTitle, axis);
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x0001E554 File Offset: 0x0001C754
		private void SetAxisTitleProperties(ChartAxisTitle chartAxisTitle, Axis axis)
		{
			if (chartAxisTitle.Caption != null)
			{
				if (chartAxisTitle.Caption.Value != null)
				{
					axis.Title = chartAxisTitle.Caption.Value;
				}
				else if (chartAxisTitle.Instance.Caption != null)
				{
					axis.Title = chartAxisTitle.Instance.Caption;
				}
			}
			if (chartAxisTitle.Position != null)
			{
				if (!chartAxisTitle.Position.IsExpression)
				{
					axis.TitleAlignment = this.GetAlignment(chartAxisTitle.Position.Value);
				}
				else
				{
					axis.TitleAlignment = this.GetAlignment(chartAxisTitle.Instance.Position);
				}
			}
			if (chartAxisTitle.TextOrientation != null)
			{
				if (!chartAxisTitle.TextOrientation.IsExpression)
				{
					axis.TextOrientation = this.GetTextOrientation(chartAxisTitle.TextOrientation.Value);
					return;
				}
				axis.TextOrientation = this.GetTextOrientation(chartAxisTitle.Instance.TextOrientation);
			}
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x0001E630 File Offset: 0x0001C830
		private StringAlignment GetAlignment(ChartAxisTitlePositions position)
		{
			if (position == ChartAxisTitlePositions.Center)
			{
				return StringAlignment.Center;
			}
			if (position != ChartAxisTitlePositions.Far)
			{
				return StringAlignment.Near;
			}
			return StringAlignment.Far;
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x0001E640 File Offset: 0x0001C840
		private void RenderAxisTitleStyle(ChartAxisTitle axisTitle, Axis axis)
		{
			Style style = axisTitle.Style;
			if (style != null)
			{
				StyleInstance style2 = axisTitle.Instance.Style;
				if (MappingHelper.IsStylePropertyDefined(axisTitle.Style.Color))
				{
					axis.TitleColor = MappingHelper.GetStyleColor(style, style2);
				}
			}
			this.RenderAxisTitleFont(axisTitle, axis);
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x0001E68C File Offset: 0x0001C88C
		private void RenderAxisTitleFont(ChartAxisTitle axisTitle, Axis axis)
		{
			Style style = axisTitle.Style;
			if (style == null)
			{
				axis.TitleFont = base.GetDefaultFont();
				return;
			}
			axis.TitleFont = base.GetFont(style, axisTitle.Instance.Style);
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x0001E6C8 File Offset: 0x0001C8C8
		private void RenderAxisStyle(ChartAxis chartAxis, Axis axis)
		{
			Style style = chartAxis.Style;
			if (style != null)
			{
				StyleInstance style2 = chartAxis.Instance.Style;
				if (MappingHelper.IsStylePropertyDefined(chartAxis.Style.Format))
				{
					axis.LabelStyle.Format = MappingHelper.GetStyleFormat(style, style2);
				}
				if (MappingHelper.IsStylePropertyDefined(chartAxis.Style.Color))
				{
					axis.LabelStyle.FontColor = MappingHelper.GetStyleColor(style, style2);
				}
				this.RenderAxisBorder(chartAxis.Style.Border, axis);
			}
			this.RenderAxisLabelFont(chartAxis, axis);
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x0001E750 File Offset: 0x0001C950
		private void RenderAxisBorder(Border border, Axis axis)
		{
			if (border == null)
			{
				return;
			}
			if (MappingHelper.IsStylePropertyDefined(border.Color))
			{
				axis.LineColor = MappingHelper.GetStyleBorderColor(border);
			}
			if (MappingHelper.IsStylePropertyDefined(border.Style))
			{
				axis.LineStyle = this.GetBorderStyle(MappingHelper.GetStyleBorderStyle(border), false);
			}
			axis.LineWidth = MappingHelper.GetStyleBorderWidth(border, base.DpiX);
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x0001E7AC File Offset: 0x0001C9AC
		private void RenderAxisGridLines(ChartGridLines chartGridLines, Grid gridLines, bool isMajor)
		{
			if (chartGridLines == null)
			{
				return;
			}
			this.SetAxisGridLinesProperties(chartGridLines, gridLines, isMajor);
			if (chartGridLines.Style != null)
			{
				this.RenderAxisGridLinesBorder(chartGridLines.Style.Border, gridLines);
			}
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x0001E7D8 File Offset: 0x0001C9D8
		private void RenderAxisGridLinesBorder(Border border, Grid gridLines)
		{
			if (border == null)
			{
				return;
			}
			if (MappingHelper.IsStylePropertyDefined(border.Color))
			{
				gridLines.LineColor = MappingHelper.GetStyleBorderColor(border);
			}
			if (MappingHelper.IsStylePropertyDefined(border.Style))
			{
				gridLines.LineStyle = this.GetBorderStyle(MappingHelper.GetStyleBorderStyle(border), false);
			}
			gridLines.LineWidth = MappingHelper.GetStyleBorderWidth(border, base.DpiX);
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x0001E834 File Offset: 0x0001CA34
		private void SetAxisGridLinesProperties(ChartGridLines chartGridLines, Grid gridLines, bool isMajor)
		{
			if (chartGridLines.Enabled != null)
			{
				ChartAutoBool chartAutoBool;
				if (!chartGridLines.Enabled.IsExpression)
				{
					chartAutoBool = chartGridLines.Enabled.Value;
				}
				else
				{
					chartAutoBool = chartGridLines.Instance.Enabled;
				}
				switch (chartAutoBool)
				{
				case ChartAutoBool.Auto:
					gridLines.Enabled = isMajor > false;
					break;
				case ChartAutoBool.True:
					gridLines.Enabled = true;
					break;
				case ChartAutoBool.False:
					gridLines.Enabled = false;
					break;
				}
			}
			if (chartGridLines.Interval != null)
			{
				double num;
				if (!chartGridLines.Interval.IsExpression)
				{
					num = chartGridLines.Interval.Value;
				}
				else
				{
					num = chartGridLines.Instance.Interval;
				}
				if (num == 0.0)
				{
					num = double.NaN;
				}
				gridLines.Interval = num;
			}
			else
			{
				gridLines.Interval = double.NaN;
			}
			if (chartGridLines.IntervalType != null)
			{
				if (!chartGridLines.IntervalType.IsExpression)
				{
					gridLines.IntervalType = this.GetDateTimeIntervalType(chartGridLines.IntervalType.Value);
				}
				else
				{
					gridLines.IntervalType = this.GetDateTimeIntervalType(chartGridLines.Instance.IntervalType);
				}
			}
			if (chartGridLines.IntervalOffset != null)
			{
				if (!chartGridLines.IntervalOffset.IsExpression)
				{
					gridLines.IntervalOffset = chartGridLines.IntervalOffset.Value;
				}
				else
				{
					gridLines.IntervalOffset = chartGridLines.Instance.IntervalOffset;
				}
			}
			if (chartGridLines.IntervalOffsetType != null)
			{
				if (!chartGridLines.IntervalOffsetType.IsExpression)
				{
					gridLines.IntervalOffsetType = this.GetDateTimeIntervalType(chartGridLines.IntervalOffsetType.Value);
					return;
				}
				gridLines.IntervalOffsetType = this.GetDateTimeIntervalType(chartGridLines.Instance.IntervalOffsetType);
			}
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x0001E9C1 File Offset: 0x0001CBC1
		private void RenderAxisTickMarks(ChartTickMarks chartTickMarks, TickMark tickMarks, bool isMajor)
		{
			if (chartTickMarks == null)
			{
				return;
			}
			this.SetAxisTickMarkProperties(chartTickMarks, tickMarks, isMajor);
			this.RenderTickMarkStyle(chartTickMarks, tickMarks);
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x0001E9D8 File Offset: 0x0001CBD8
		private void SetAxisTickMarkProperties(ChartTickMarks chartTickMarks, TickMark tickMarks, bool isMajor)
		{
			if (chartTickMarks.Enabled != null)
			{
				if (!chartTickMarks.Enabled.IsExpression)
				{
					tickMarks.Enabled = this.GetChartTickMarksEnabled(chartTickMarks.Enabled.Value, isMajor);
				}
				else
				{
					tickMarks.Enabled = this.GetChartTickMarksEnabled(chartTickMarks.Instance.Enabled, isMajor);
				}
			}
			if (chartTickMarks.Interval != null)
			{
				double num;
				if (!chartTickMarks.Interval.IsExpression)
				{
					num = chartTickMarks.Interval.Value;
				}
				else
				{
					num = chartTickMarks.Instance.Interval;
				}
				if (num == 0.0)
				{
					num = double.NaN;
				}
				tickMarks.Interval = num;
			}
			else
			{
				tickMarks.Interval = double.NaN;
			}
			if (chartTickMarks.IntervalOffset != null)
			{
				if (!chartTickMarks.IntervalOffset.IsExpression)
				{
					tickMarks.IntervalOffset = chartTickMarks.IntervalOffset.Value;
				}
				else
				{
					tickMarks.IntervalOffset = chartTickMarks.Instance.IntervalOffset;
				}
			}
			if (chartTickMarks.IntervalOffsetType != null)
			{
				if (!chartTickMarks.IntervalOffsetType.IsExpression)
				{
					tickMarks.IntervalOffsetType = this.GetDateTimeIntervalType(chartTickMarks.IntervalOffsetType.Value);
				}
				else
				{
					tickMarks.IntervalOffsetType = this.GetDateTimeIntervalType(chartTickMarks.Instance.IntervalOffsetType);
				}
			}
			if (chartTickMarks.IntervalType != null)
			{
				if (!chartTickMarks.IntervalType.IsExpression)
				{
					tickMarks.IntervalType = this.GetDateTimeIntervalType(chartTickMarks.IntervalType.Value);
				}
				else
				{
					tickMarks.IntervalType = this.GetDateTimeIntervalType(chartTickMarks.Instance.IntervalType);
				}
			}
			if (chartTickMarks.Length != null)
			{
				if (!chartTickMarks.Length.IsExpression)
				{
					tickMarks.Size = (float)chartTickMarks.Length.Value;
				}
				else
				{
					tickMarks.Size = (float)chartTickMarks.Instance.Length;
				}
			}
			if (chartTickMarks.Type != null)
			{
				if (!chartTickMarks.Type.IsExpression)
				{
					tickMarks.Style = this.GetTickMarkStyle(chartTickMarks.Type.Value);
					return;
				}
				tickMarks.Style = this.GetTickMarkStyle(chartTickMarks.Instance.Type);
			}
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x0001EBCB File Offset: 0x0001CDCB
		private bool GetChartTickMarksEnabled(ChartAutoBool enabled, bool isMajor)
		{
			return enabled == ChartAutoBool.True || (enabled != ChartAutoBool.False && isMajor);
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x0001EBDA File Offset: 0x0001CDDA
		private void RenderTickMarkStyle(ChartTickMarks chartTickMarks, TickMark tickMarks)
		{
			if (chartTickMarks.Style == null)
			{
				return;
			}
			this.RenderTickMarkBorder(chartTickMarks.Style.Border, tickMarks);
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x0001EBF8 File Offset: 0x0001CDF8
		private void RenderTickMarkBorder(Border border, TickMark tickMark)
		{
			if (border == null)
			{
				return;
			}
			if (MappingHelper.IsStylePropertyDefined(border.Color))
			{
				tickMark.LineColor = MappingHelper.GetStyleBorderColor(border);
			}
			if (MappingHelper.IsStylePropertyDefined(border.Style))
			{
				tickMark.LineStyle = this.GetBorderStyle(MappingHelper.GetStyleBorderStyle(border), false);
			}
			tickMark.LineWidth = MappingHelper.GetStyleBorderWidth(border, base.DpiX);
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x0001EC54 File Offset: 0x0001CE54
		private void RenderAxisScaleBreak(ChartAxisScaleBreak chartAxisScaleBreak, AxisScaleBreakStyle axisScaleBreak)
		{
			if (chartAxisScaleBreak == null)
			{
				return;
			}
			if (chartAxisScaleBreak.BreakLineType != null)
			{
				if (!chartAxisScaleBreak.BreakLineType.IsExpression)
				{
					axisScaleBreak.BreakLineType = this.GetScaleBreakLineType(chartAxisScaleBreak.BreakLineType.Value);
				}
				else
				{
					axisScaleBreak.BreakLineType = this.GetScaleBreakLineType(chartAxisScaleBreak.Instance.BreakLineType);
				}
			}
			if (chartAxisScaleBreak.CollapsibleSpaceThreshold != null)
			{
				if (!chartAxisScaleBreak.CollapsibleSpaceThreshold.IsExpression)
				{
					axisScaleBreak.CollapsibleSpaceThreshold = chartAxisScaleBreak.CollapsibleSpaceThreshold.Value;
				}
				else
				{
					axisScaleBreak.CollapsibleSpaceThreshold = chartAxisScaleBreak.Instance.CollapsibleSpaceThreshold;
				}
			}
			if (chartAxisScaleBreak.Enabled != null)
			{
				if (!chartAxisScaleBreak.Enabled.IsExpression)
				{
					axisScaleBreak.Enabled = chartAxisScaleBreak.Enabled.Value;
				}
				else
				{
					axisScaleBreak.Enabled = chartAxisScaleBreak.Instance.Enabled;
				}
			}
			if (chartAxisScaleBreak.IncludeZero != null)
			{
				if (!chartAxisScaleBreak.IncludeZero.IsExpression)
				{
					axisScaleBreak.StartFromZero = this.GetAutoBool(chartAxisScaleBreak.IncludeZero.Value);
				}
				else
				{
					axisScaleBreak.StartFromZero = this.GetAutoBool(chartAxisScaleBreak.Instance.IncludeZero);
				}
			}
			if (chartAxisScaleBreak.MaxNumberOfBreaks != null)
			{
				if (!chartAxisScaleBreak.MaxNumberOfBreaks.IsExpression)
				{
					axisScaleBreak.MaxNumberOfBreaks = chartAxisScaleBreak.MaxNumberOfBreaks.Value;
				}
				else
				{
					axisScaleBreak.MaxNumberOfBreaks = chartAxisScaleBreak.Instance.MaxNumberOfBreaks;
				}
			}
			if (chartAxisScaleBreak.Spacing != null)
			{
				if (!chartAxisScaleBreak.Spacing.IsExpression)
				{
					axisScaleBreak.Spacing = chartAxisScaleBreak.Spacing.Value;
				}
				else
				{
					axisScaleBreak.Spacing = chartAxisScaleBreak.Instance.Spacing;
				}
			}
			if (chartAxisScaleBreak.Style != null)
			{
				this.RenderAxisScaleBreakBorder(chartAxisScaleBreak.Style.Border, axisScaleBreak);
			}
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0001EDED File Offset: 0x0001CFED
		private void RenderCustomProperties(CustomPropertyCollection customProperties, Axis axis)
		{
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x0001EDF0 File Offset: 0x0001CFF0
		private void RenderLegends()
		{
			if (this.m_chart.Legends == null)
			{
				return;
			}
			foreach (ChartLegend chartLegend in this.m_chart.Legends)
			{
				Legend legend = new Legend();
				this.m_coreChart.Legends.Add(legend);
				this.RenderLegend(chartLegend, legend);
			}
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x0001EE6C File Offset: 0x0001D06C
		private void RenderLegend(ChartLegend chartLegend, Legend legend)
		{
			if (chartLegend.Name != null)
			{
				legend.Name = chartLegend.Name;
			}
			this.RenderLegendStyle(chartLegend, legend);
			this.SetLegendProperties(chartLegend, legend);
			this.RenderElementPosition(chartLegend.ChartElementPosition, legend.Position);
			this.RenderLegendTitle(chartLegend.LegendTitle, legend);
			this.RenderLegendColumns(chartLegend.LegendColumns, legend.CellColumns);
			this.RenderLegendCustomItems(chartLegend.LegendCustomItems, legend.CustomItems);
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x0001EEE0 File Offset: 0x0001D0E0
		private void SetLegendProperties(ChartLegend chartLegend, Legend legend)
		{
			if (chartLegend.Hidden != null)
			{
				if (!chartLegend.Hidden.IsExpression)
				{
					legend.Enabled = !chartLegend.Hidden.Value;
				}
				else
				{
					legend.Enabled = !chartLegend.Instance.Hidden;
				}
			}
			if (chartLegend.DockOutsideChartArea != null)
			{
				if (!chartLegend.DockOutsideChartArea.IsExpression)
				{
					legend.DockInsideChartArea = !chartLegend.DockOutsideChartArea.Value;
				}
				else
				{
					legend.DockInsideChartArea = !chartLegend.Instance.DockOutsideChartArea;
				}
			}
			if (chartLegend.DockToChartArea != null)
			{
				legend.DockToChartArea = chartLegend.DockToChartArea;
			}
			if (chartLegend.Position != null)
			{
				if (!chartLegend.Position.IsExpression)
				{
					legend.Alignment = this.GetLegendAlignment(chartLegend.Position.Value);
					legend.Docking = this.GetLegendDocking(chartLegend.Position.Value);
				}
				else
				{
					legend.Alignment = this.GetLegendAlignment(chartLegend.Instance.Position);
					legend.Docking = this.GetLegendDocking(chartLegend.Instance.Position);
				}
			}
			if (chartLegend.Layout != null)
			{
				if (!chartLegend.Layout.IsExpression)
				{
					this.SetLegendLayout(chartLegend.Layout.Value, legend);
				}
				else
				{
					this.SetLegendLayout(chartLegend.Instance.Layout, legend);
				}
			}
			if (chartLegend.AutoFitTextDisabled != null)
			{
				if (!chartLegend.AutoFitTextDisabled.IsExpression)
				{
					legend.AutoFitText = !chartLegend.AutoFitTextDisabled.Value;
				}
				else
				{
					legend.AutoFitText = !chartLegend.Instance.AutoFitTextDisabled;
				}
			}
			else
			{
				legend.AutoFitText = true;
			}
			ReportBoolProperty equallySpacedItems = chartLegend.EquallySpacedItems;
			if (chartLegend.InterlacedRows != null)
			{
				if (!chartLegend.InterlacedRows.IsExpression)
				{
					legend.InterlacedRows = chartLegend.InterlacedRows.Value;
				}
				else
				{
					legend.InterlacedRows = chartLegend.Instance.InterlacedRows;
				}
			}
			if (chartLegend.InterlacedRowsColor != null)
			{
				Color empty = Color.Empty;
				if (MappingHelper.GetColorFromReportColorProperty(chartLegend.InterlacedRowsColor, ref empty))
				{
					legend.InterlacedRowsColor = empty;
				}
				else if (chartLegend.Instance.InterlacedRowsColor != null)
				{
					legend.InterlacedRowsColor = chartLegend.Instance.InterlacedRowsColor.ToColor();
				}
			}
			if (chartLegend.MaxAutoSize != null)
			{
				if (!chartLegend.MaxAutoSize.IsExpression)
				{
					legend.MaxAutoSize = (float)chartLegend.MaxAutoSize.Value;
				}
				else
				{
					legend.MaxAutoSize = (float)chartLegend.Instance.MaxAutoSize;
				}
			}
			if (chartLegend.MinFontSize != null)
			{
				if (!chartLegend.MinFontSize.IsExpression)
				{
					legend.AutoFitMinFontSize = (int)Math.Round(chartLegend.MinFontSize.Value.ToPoints());
				}
				else
				{
					legend.AutoFitMinFontSize = (int)Math.Round(chartLegend.Instance.MinFontSize.ToPoints());
				}
			}
			if (chartLegend.Reversed != null)
			{
				ChartAutoBool chartAutoBool;
				if (!chartLegend.Reversed.IsExpression)
				{
					chartAutoBool = chartLegend.Reversed.Value;
				}
				else
				{
					chartAutoBool = chartLegend.Instance.Reversed;
				}
				switch (chartAutoBool)
				{
				case ChartAutoBool.Auto:
					legend.Reversed = 0;
					break;
				case ChartAutoBool.True:
					legend.Reversed = 1;
					break;
				case ChartAutoBool.False:
					legend.Reversed = 2;
					break;
				}
			}
			if (chartLegend.TextWrapThreshold != null)
			{
				if (!chartLegend.TextWrapThreshold.IsExpression)
				{
					legend.TextWrapThreshold = chartLegend.TextWrapThreshold.Value;
					return;
				}
				legend.TextWrapThreshold = chartLegend.Instance.TextWrapThreshold;
			}
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x0001F220 File Offset: 0x0001D420
		private void RenderLegendTitle(ChartLegendTitle chartLegendTitle, Legend legend)
		{
			if (chartLegendTitle == null)
			{
				return;
			}
			if (chartLegendTitle.Caption != null)
			{
				if (!chartLegendTitle.Caption.IsExpression)
				{
					if (chartLegendTitle.Caption.Value != null)
					{
						legend.Title = chartLegendTitle.Caption.Value;
					}
				}
				else if (chartLegendTitle.Instance.Caption != null)
				{
					legend.Title = chartLegendTitle.Instance.Caption;
				}
			}
			if (chartLegendTitle.TitleSeparator != null)
			{
				if (!chartLegendTitle.TitleSeparator.IsExpression)
				{
					legend.TitleSeparator = this.GetLegendSeparatorStyle(chartLegendTitle.TitleSeparator.Value);
				}
				else
				{
					legend.TitleSeparator = this.GetLegendSeparatorStyle(chartLegendTitle.Instance.TitleSeparator);
				}
			}
			this.RenderLegendTitleStyle(chartLegendTitle, legend);
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x0001F2D1 File Offset: 0x0001D4D1
		private LegendSeparatorType GetLegendSeparatorStyle(ChartSeparators chartLegendSeparator)
		{
			switch (chartLegendSeparator)
			{
			case ChartSeparators.Line:
				return 1;
			case ChartSeparators.ThickLine:
				return 2;
			case ChartSeparators.DoubleLine:
				return 3;
			case ChartSeparators.DashLine:
				return 4;
			case ChartSeparators.DotLine:
				return 5;
			case ChartSeparators.GradientLine:
				return 6;
			case ChartSeparators.ThickGradientLine:
				return 7;
			default:
				return 0;
			}
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x0001F308 File Offset: 0x0001D508
		private void RenderLegendTitleStyle(ChartLegendTitle chartLegendTitle, Legend legend)
		{
			Style style = chartLegendTitle.Style;
			if (style != null)
			{
				StyleInstance style2 = chartLegendTitle.Instance.Style;
				if (MappingHelper.IsStylePropertyDefined(style.Color))
				{
					legend.TitleColor = MappingHelper.GetStyleColor(style, style2);
				}
				if (MappingHelper.IsStylePropertyDefined(style.BackgroundColor))
				{
					legend.TitleBackColor = MappingHelper.GetStyleBackgroundColor(style, style2);
				}
				this.RenderLegendTitleBorder(style.Border, legend);
				legend.TitleAlignment = this.GetLegendTitleAlign(MappingHelper.GetStyleTextAlign(style, style2));
			}
			this.RenderLegendTitleFont(chartLegendTitle, legend);
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x0001F387 File Offset: 0x0001D587
		private void RenderLegendTitleBorder(Border border, Legend legend)
		{
			if (border == null)
			{
				return;
			}
			if (MappingHelper.IsStylePropertyDefined(border.Color))
			{
				legend.TitleSeparatorColor = MappingHelper.GetStyleBorderColor(border);
			}
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x0001F3A8 File Offset: 0x0001D5A8
		private void SetLegendLayout(ChartLegendLayouts layout, Legend legend)
		{
			switch (layout)
			{
			case ChartLegendLayouts.AutoTable:
				legend.LegendStyle = 2;
				legend.TableStyle = 0;
				return;
			case ChartLegendLayouts.Column:
				legend.LegendStyle = 0;
				return;
			case ChartLegendLayouts.Row:
				legend.LegendStyle = 1;
				return;
			case ChartLegendLayouts.WideTable:
				legend.LegendStyle = 2;
				legend.TableStyle = 1;
				return;
			case ChartLegendLayouts.TallTable:
				legend.LegendStyle = 2;
				legend.TableStyle = 2;
				return;
			default:
				return;
			}
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x0001F40C File Offset: 0x0001D60C
		private void GetChartTitlePosition(ChartTitlePositions position, out ContentAlignment alignment, out Docking docking)
		{
			docking = 0;
			alignment = ContentAlignment.MiddleLeft;
			switch (position)
			{
			case ChartTitlePositions.TopCenter:
				docking = 0;
				alignment = ContentAlignment.MiddleCenter;
				return;
			case ChartTitlePositions.TopLeft:
				docking = 0;
				alignment = ContentAlignment.MiddleLeft;
				return;
			case ChartTitlePositions.TopRight:
				docking = 0;
				alignment = ContentAlignment.MiddleRight;
				return;
			case ChartTitlePositions.LeftTop:
				docking = 3;
				alignment = ContentAlignment.MiddleRight;
				return;
			case ChartTitlePositions.LeftCenter:
				docking = 3;
				alignment = ContentAlignment.MiddleCenter;
				return;
			case ChartTitlePositions.LeftBottom:
				docking = 3;
				alignment = ContentAlignment.MiddleLeft;
				return;
			case ChartTitlePositions.RightTop:
				docking = 1;
				alignment = ContentAlignment.MiddleLeft;
				return;
			case ChartTitlePositions.RightCenter:
				docking = 1;
				alignment = ContentAlignment.MiddleCenter;
				return;
			case ChartTitlePositions.RightBottom:
				docking = 1;
				alignment = ContentAlignment.MiddleRight;
				return;
			case ChartTitlePositions.BottomRight:
				docking = 2;
				alignment = ContentAlignment.MiddleRight;
				return;
			case ChartTitlePositions.BottomCenter:
				docking = 2;
				alignment = ContentAlignment.MiddleCenter;
				return;
			case ChartTitlePositions.BottomLeft:
				docking = 2;
				alignment = ContentAlignment.MiddleLeft;
				return;
			default:
				return;
			}
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x0001F4B6 File Offset: 0x0001D6B6
		private TextOrientation GetTextOrientation(TextOrientations textOrientations)
		{
			switch (textOrientations)
			{
			case TextOrientations.Horizontal:
				return 1;
			case TextOrientations.Rotated90:
				return 2;
			case TextOrientations.Rotated270:
				return 3;
			case TextOrientations.Stacked:
				return 4;
			default:
				return 0;
			}
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x0001F4DB File Offset: 0x0001D6DB
		private StringAlignment GetLegendAlignment(ChartLegendPositions position)
		{
			switch (position)
			{
			case ChartLegendPositions.TopCenter:
			case ChartLegendPositions.LeftCenter:
			case ChartLegendPositions.RightCenter:
			case ChartLegendPositions.BottomCenter:
				return StringAlignment.Center;
			case ChartLegendPositions.TopRight:
			case ChartLegendPositions.LeftBottom:
			case ChartLegendPositions.RightBottom:
			case ChartLegendPositions.BottomRight:
				return StringAlignment.Far;
			}
			return StringAlignment.Near;
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x0001F514 File Offset: 0x0001D714
		private StringAlignment GetLegendTitleAlign(TextAlignments textAlignment)
		{
			if (textAlignment == TextAlignments.Left)
			{
				return StringAlignment.Near;
			}
			if (textAlignment != TextAlignments.Right)
			{
				return StringAlignment.Center;
			}
			return StringAlignment.Far;
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x0001F525 File Offset: 0x0001D725
		private LegendDocking GetLegendDocking(ChartLegendPositions position)
		{
			switch (position)
			{
			case ChartLegendPositions.TopLeft:
			case ChartLegendPositions.TopCenter:
			case ChartLegendPositions.TopRight:
				return 0;
			case ChartLegendPositions.LeftTop:
			case ChartLegendPositions.LeftCenter:
			case ChartLegendPositions.LeftBottom:
				return 3;
			case ChartLegendPositions.BottomLeft:
			case ChartLegendPositions.BottomCenter:
			case ChartLegendPositions.BottomRight:
				return 2;
			}
			return 1;
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x0001F564 File Offset: 0x0001D764
		private void RenderLegendStyle(ChartLegend chartLegend, Legend legend)
		{
			Border border = null;
			Style style = chartLegend.Style;
			if (style != null)
			{
				StyleInstance style2 = chartLegend.Instance.Style;
				if (MappingHelper.IsStylePropertyDefined(chartLegend.Style.Color))
				{
					legend.FontColor = MappingHelper.GetStyleColor(style, style2);
				}
				if (MappingHelper.IsStylePropertyDefined(chartLegend.Style.BackgroundColor))
				{
					legend.BackColor = MappingHelper.GetStyleBackgroundColor(style, style2);
				}
				if (MappingHelper.IsStylePropertyDefined(chartLegend.Style.BackgroundGradientEndColor))
				{
					legend.BackGradientEndColor = MappingHelper.GetStyleBackGradientEndColor(style, style2);
				}
				if (MappingHelper.IsStylePropertyDefined(chartLegend.Style.BackgroundGradientType))
				{
					legend.BackGradientType = this.GetGradientType(MappingHelper.GetStyleBackGradientType(style, style2));
				}
				if (MappingHelper.IsStylePropertyDefined(chartLegend.Style.BackgroundHatchType))
				{
					legend.BackHatchStyle = this.GetHatchType(MappingHelper.GetStyleBackgroundHatchType(style, style2));
				}
				if (MappingHelper.IsStylePropertyDefined(chartLegend.Style.ShadowColor))
				{
					legend.ShadowColor = MappingHelper.GetStyleShadowColor(style, style2);
				}
				if (MappingHelper.IsStylePropertyDefined(chartLegend.Style.ShadowOffset))
				{
					legend.ShadowOffset = MappingHelper.GetStyleShadowOffset(style, style2, base.DpiX);
				}
				this.RenderLegendBackgroundImage(chartLegend.Style.BackgroundImage, legend);
				border = chartLegend.Style.Border;
			}
			this.RenderLegendBorder(border, legend);
			this.RenderLegendFont(chartLegend, legend);
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x0001F6A5 File Offset: 0x0001D8A5
		private void RenderLegendColumns(ChartLegendColumnCollection chartLegendColumns, LegendCellColumnCollection legendColumns)
		{
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x0001F6A7 File Offset: 0x0001D8A7
		private void RenderLegendCustomItems(ChartLegendCustomItemCollection chartLegendCustomItems, LegendItemsCollection legendCustomItems)
		{
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x0001F6AC File Offset: 0x0001D8AC
		private void RenderElementPosition(ChartElementPosition chartElementPosition, ElementPosition elementPosition)
		{
			if (chartElementPosition == null)
			{
				return;
			}
			ReportDoubleProperty reportDoubleProperty = chartElementPosition.Left;
			if (reportDoubleProperty != null)
			{
				if (!reportDoubleProperty.IsExpression)
				{
					elementPosition.X = (float)reportDoubleProperty.Value;
				}
				else
				{
					elementPosition.X = (float)chartElementPosition.Instance.Left;
				}
			}
			else
			{
				elementPosition.X = 0f;
			}
			reportDoubleProperty = chartElementPosition.Top;
			if (reportDoubleProperty != null)
			{
				if (!reportDoubleProperty.IsExpression)
				{
					elementPosition.Y = (float)reportDoubleProperty.Value;
				}
				else
				{
					elementPosition.Y = (float)chartElementPosition.Instance.Top;
				}
			}
			else
			{
				elementPosition.Y = 0f;
			}
			reportDoubleProperty = chartElementPosition.Width;
			if (reportDoubleProperty != null)
			{
				if (!reportDoubleProperty.IsExpression)
				{
					elementPosition.Width = (float)reportDoubleProperty.Value;
				}
				else
				{
					elementPosition.Width = (float)chartElementPosition.Instance.Width;
				}
			}
			else
			{
				elementPosition.Width = 100f - elementPosition.X;
			}
			reportDoubleProperty = chartElementPosition.Height;
			if (reportDoubleProperty == null)
			{
				elementPosition.Height = 100f - elementPosition.Y;
				return;
			}
			if (!reportDoubleProperty.IsExpression)
			{
				elementPosition.Height = (float)reportDoubleProperty.Value;
				return;
			}
			elementPosition.Height = (float)chartElementPosition.Instance.Height;
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x0001F7CC File Offset: 0x0001D9CC
		private void RenderTitles()
		{
			if (this.m_chart.Titles == null)
			{
				return;
			}
			foreach (ChartTitle chartTitle in this.m_chart.Titles)
			{
				Title title = new Title();
				this.m_coreChart.Titles.Add(title);
				this.RenderTitle(chartTitle, title);
			}
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x0001F848 File Offset: 0x0001DA48
		private void RenderTitle(ChartTitle chartTitle, Title title)
		{
			this.SetTitleProperties(chartTitle, title);
			this.RenderElementPosition(chartTitle.ChartElementPosition, title.Position);
			this.RenderActionInfo(chartTitle.ActionInfo, title.ToolTip, title);
			this.RenderTitleStyle(chartTitle, title);
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x0001F880 File Offset: 0x0001DA80
		private void SetTitleProperties(ChartTitle chartTitle, Title title)
		{
			if (chartTitle.Name != null)
			{
				title.Name = chartTitle.Name;
			}
			if (chartTitle.Caption != null)
			{
				if (!chartTitle.Caption.IsExpression)
				{
					if (chartTitle.Caption.Value != null)
					{
						title.Text = chartTitle.Caption.Value;
					}
				}
				else if (chartTitle.Instance.Caption != null)
				{
					title.Text = chartTitle.Instance.Caption;
				}
			}
			if (chartTitle.Position != null)
			{
				ChartTitlePositions chartTitlePositions;
				if (!chartTitle.Position.IsExpression)
				{
					chartTitlePositions = chartTitle.Position.Value;
				}
				else
				{
					chartTitlePositions = chartTitle.Instance.Position;
				}
				ContentAlignment contentAlignment;
				Docking docking;
				this.GetChartTitlePosition(chartTitlePositions, out contentAlignment, out docking);
				title.Docking = docking;
				title.Alignment = contentAlignment;
			}
			if (chartTitle.DockOffset != null)
			{
				if (!chartTitle.DockOffset.IsExpression)
				{
					title.DockOffset = chartTitle.DockOffset.Value;
				}
				else
				{
					title.DockOffset = chartTitle.Instance.DockOffset;
				}
			}
			if (chartTitle.DockOutsideChartArea != null)
			{
				if (!chartTitle.DockOutsideChartArea.IsExpression)
				{
					title.DockInsideChartArea = !chartTitle.DockOutsideChartArea.Value;
				}
				else
				{
					title.DockInsideChartArea = !chartTitle.Instance.DockOutsideChartArea;
				}
			}
			if (chartTitle.DockToChartArea != null)
			{
				title.DockToChartArea = chartTitle.DockToChartArea;
			}
			if (chartTitle.Hidden != null)
			{
				if (!chartTitle.Hidden.IsExpression)
				{
					title.Visible = !chartTitle.Hidden.Value;
				}
				else
				{
					title.Visible = !chartTitle.Instance.Hidden;
				}
			}
			ReportStringProperty toolTip = chartTitle.ToolTip;
			if (toolTip != null)
			{
				if (!toolTip.IsExpression)
				{
					if (toolTip.Value != null)
					{
						title.ToolTip = toolTip.Value;
					}
				}
				else if (chartTitle.Instance.ToolTip != null)
				{
					title.ToolTip = chartTitle.Instance.ToolTip;
				}
			}
			if (chartTitle.TextOrientation != null)
			{
				if (!chartTitle.TextOrientation.IsExpression)
				{
					title.TextOrientation = this.GetTextOrientation(chartTitle.TextOrientation.Value);
					return;
				}
				title.TextOrientation = this.GetTextOrientation(chartTitle.Instance.TextOrientation);
			}
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x0001FA94 File Offset: 0x0001DC94
		private void RenderTitleStyle(ChartTitle chartTitle, Title title)
		{
			Border border = null;
			Style style = chartTitle.Style;
			if (style != null)
			{
				StyleInstance style2 = chartTitle.Instance.Style;
				if (MappingHelper.IsStylePropertyDefined(chartTitle.Style.Color))
				{
					title.Color = MappingHelper.GetStyleColor(style, style2);
				}
				if (MappingHelper.IsStylePropertyDefined(chartTitle.Style.BackgroundColor))
				{
					title.BackColor = MappingHelper.GetStyleBackgroundColor(style, style2);
				}
				else
				{
					title.BackColor = Color.Transparent;
				}
				if (MappingHelper.IsStylePropertyDefined(chartTitle.Style.BackgroundGradientEndColor))
				{
					title.BackGradientEndColor = MappingHelper.GetStyleBackGradientEndColor(style, style2);
				}
				if (MappingHelper.IsStylePropertyDefined(chartTitle.Style.BackgroundGradientType))
				{
					title.BackGradientType = this.GetGradientType(MappingHelper.GetStyleBackGradientType(style, style2));
				}
				if (MappingHelper.IsStylePropertyDefined(chartTitle.Style.BackgroundHatchType))
				{
					title.BackHatchStyle = this.GetHatchType(MappingHelper.GetStyleBackgroundHatchType(style, style2));
				}
				if (MappingHelper.IsStylePropertyDefined(chartTitle.Style.ShadowColor))
				{
					title.ShadowColor = MappingHelper.GetStyleShadowColor(style, style2);
				}
				if (MappingHelper.IsStylePropertyDefined(chartTitle.Style.ShadowOffset))
				{
					title.ShadowOffset = MappingHelper.GetStyleShadowOffset(style, style2, base.DpiX);
				}
				if (MappingHelper.IsStylePropertyDefined(chartTitle.Style.TextEffect))
				{
					title.Style = this.GetTextStyle(MappingHelper.GetStyleTextEffect(style, style2));
				}
				this.RenderTitleBackgroundImage(chartTitle.Style.BackgroundImage, title);
				border = chartTitle.Style.Border;
			}
			this.RenderTitleBorder(border, title);
			this.RenderTitleFont(chartTitle, title);
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x0001FC07 File Offset: 0x0001DE07
		private TextStyle GetTextStyle(TextEffects textEffects)
		{
			switch (textEffects)
			{
			case TextEffects.Shadow:
				return 1;
			case TextEffects.Emboss:
				return 2;
			case TextEffects.Embed:
				return 3;
			case TextEffects.Frame:
				return 4;
			default:
				return 0;
			}
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x0001FC2C File Offset: 0x0001DE2C
		private void RenderDataLabel(ChartDataLabel chartDataLabel, DataPointAttributes dataPointAttributes, bool isDataPoint)
		{
			if (chartDataLabel == null)
			{
				return;
			}
			this.SetDataLabelProperties(chartDataLabel, dataPointAttributes, isDataPoint);
			this.RenderDataLabelStyle(chartDataLabel, dataPointAttributes, isDataPoint);
			this.RenderDataLabelActionInfo(chartDataLabel.ActionInfo, dataPointAttributes.LabelToolTip, dataPointAttributes);
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x0001FC58 File Offset: 0x0001DE58
		private void SetDataLabelProperties(ChartDataLabel chartDataLabel, DataPointAttributes dataPointAttributes, bool isDataPoint)
		{
			if (chartDataLabel == null)
			{
				return;
			}
			if (chartDataLabel.Position != null)
			{
				ChartDataLabelPositions dataLabelPositionValue = this.GetDataLabelPositionValue(chartDataLabel);
				dataPointAttributes.SetAttribute("LabelStyle", this.GetDataLabelPosition(dataLabelPositionValue));
			}
			if (chartDataLabel.Rotation != null)
			{
				if (!chartDataLabel.Rotation.IsExpression)
				{
					dataPointAttributes.FontAngle = chartDataLabel.Rotation.Value;
				}
				else
				{
					dataPointAttributes.FontAngle = chartDataLabel.Instance.Rotation;
				}
			}
			if (chartDataLabel.UseValueAsLabel != null)
			{
				if (!chartDataLabel.UseValueAsLabel.IsExpression)
				{
					dataPointAttributes.ShowLabelAsValue = chartDataLabel.UseValueAsLabel.Value;
				}
				else
				{
					dataPointAttributes.ShowLabelAsValue = chartDataLabel.Instance.UseValueAsLabel;
				}
			}
			if (!dataPointAttributes.ShowLabelAsValue && chartDataLabel.Label != null)
			{
				if (!chartDataLabel.Label.IsExpression)
				{
					if (chartDataLabel.Label != null)
					{
						dataPointAttributes.Label = chartDataLabel.Label.Value;
					}
				}
				else if (chartDataLabel.Instance.Label != null)
				{
					dataPointAttributes.Label = chartDataLabel.Instance.Label;
				}
			}
			if (chartDataLabel.Visible != null)
			{
				bool flag;
				if (!chartDataLabel.Visible.IsExpression)
				{
					flag = chartDataLabel.Visible.Value;
				}
				else
				{
					flag = chartDataLabel.Instance.Visible;
				}
				if (!flag)
				{
					if (isDataPoint)
					{
						dataPointAttributes.DeleteAttribute(1);
						dataPointAttributes.DeleteAttribute(4);
						ChartMapper.HideDataPointLabels(dataPointAttributes);
					}
					else
					{
						dataPointAttributes.Label = "";
						dataPointAttributes.ShowLabelAsValue = false;
					}
				}
			}
			else if (isDataPoint)
			{
				dataPointAttributes.DeleteAttribute(1);
				dataPointAttributes.DeleteAttribute(4);
				ChartMapper.HideDataPointLabels(dataPointAttributes);
			}
			else
			{
				dataPointAttributes.Label = "";
				dataPointAttributes.ShowLabelAsValue = false;
			}
			ReportStringProperty toolTip = chartDataLabel.ToolTip;
			if (toolTip != null)
			{
				if (!toolTip.IsExpression)
				{
					if (toolTip.Value != null)
					{
						dataPointAttributes.LabelToolTip = toolTip.Value;
						return;
					}
				}
				else
				{
					string toolTip2 = chartDataLabel.Instance.ToolTip;
					if (toolTip2 != null)
					{
						dataPointAttributes.LabelToolTip = toolTip2;
					}
				}
			}
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x0001FE1C File Offset: 0x0001E01C
		private static void HideDataPointLabels(DataPointAttributes dataPointAttributes)
		{
			dataPointAttributes.SetAttribute("LabelsVisible", "false");
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x0001FE30 File Offset: 0x0001E030
		private ChartDataLabelPositions GetDataLabelPositionValue(ChartDataLabel chartDataLabel)
		{
			ChartDataLabelPositions chartDataLabelPositions;
			if (!chartDataLabel.Position.IsExpression)
			{
				chartDataLabelPositions = chartDataLabel.Position.Value;
			}
			else
			{
				chartDataLabelPositions = chartDataLabel.Instance.Position;
			}
			return chartDataLabelPositions;
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x0001FE68 File Offset: 0x0001E068
		private string GetDataLabelPosition(ChartDataLabelPositions position)
		{
			switch (position)
			{
			case ChartDataLabelPositions.Top:
				return "Top";
			case ChartDataLabelPositions.TopLeft:
				return "TopLeft";
			case ChartDataLabelPositions.TopRight:
				return "TopRight";
			case ChartDataLabelPositions.Left:
				return "Left";
			case ChartDataLabelPositions.Center:
				return "Center";
			case ChartDataLabelPositions.Right:
				return "Right";
			case ChartDataLabelPositions.BottomRight:
				return "BottomRight";
			case ChartDataLabelPositions.Bottom:
				return "Bottom";
			case ChartDataLabelPositions.BottomLeft:
				return "BottomLeft";
			case ChartDataLabelPositions.Outside:
				return "Outside";
			default:
				return "Auto";
			}
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x0001FEE8 File Offset: 0x0001E0E8
		private void RenderDataLabelStyle(ChartDataLabel chartDataLabel, DataPointAttributes dataPointAttributes, bool isDataPoint)
		{
			Border border = null;
			Style style = chartDataLabel.Style;
			if (style != null)
			{
				StyleInstance style2 = chartDataLabel.Instance.Style;
				if (MappingHelper.IsStylePropertyDefined(chartDataLabel.Style.BackgroundColor))
				{
					dataPointAttributes.LabelBackColor = MappingHelper.GetStyleBackgroundColor(style, style2);
				}
				if (MappingHelper.IsStylePropertyDefined(chartDataLabel.Style.Color))
				{
					dataPointAttributes.FontColor = MappingHelper.GetStyleColor(style, style2);
				}
				if (MappingHelper.IsStylePropertyDefined(chartDataLabel.Style.Format))
				{
					dataPointAttributes.LabelFormat = MappingHelper.GetStyleFormat(style, style2);
				}
				border = chartDataLabel.Style.Border;
			}
			this.RenderDataLabelBorder(border, dataPointAttributes);
			this.RenderDataLabelFont(chartDataLabel, dataPointAttributes, isDataPoint);
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x0001FF88 File Offset: 0x0001E188
		private void RenderDataLabelBorder(Border border, DataPointAttributes dataPointAttributes)
		{
			dataPointAttributes.LabelBorderColor = Color.Black;
			dataPointAttributes.LabelBorderStyle = 0;
			if (border == null)
			{
				return;
			}
			if (MappingHelper.IsStylePropertyDefined(border.Color))
			{
				dataPointAttributes.LabelBorderColor = MappingHelper.GetStyleBorderColor(border);
			}
			if (MappingHelper.IsStylePropertyDefined(border.Style))
			{
				dataPointAttributes.LabelBorderStyle = this.GetBorderStyle(MappingHelper.GetStyleBorderStyle(border), false);
			}
			dataPointAttributes.LabelBorderWidth = MappingHelper.GetStyleBorderWidth(border, base.DpiX);
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x0001FFF8 File Offset: 0x0001E1F8
		private void RenderDataLabelActionInfo(ActionInfo actionInfo, string toolTip, DataPointAttributes dataPointAttributes)
		{
			if (actionInfo == null && string.IsNullOrEmpty(toolTip))
			{
				return;
			}
			string text;
			ActionInfoWithDynamicImageMap actionInfoWithDynamicImageMap = MappingHelper.CreateActionInfoDynamic(this.m_chart, actionInfo, toolTip, out text);
			if (actionInfoWithDynamicImageMap != null)
			{
				if (text != null)
				{
					dataPointAttributes.LabelHref = text;
				}
				int count = this.m_actions.Count;
				this.m_actions.InternalList.Add(actionInfoWithDynamicImageMap);
				dataPointAttributes.LabelTag = count;
			}
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x00020057 File Offset: 0x0001E257
		private void RenderDataPointMarker(ChartMarker chartMarker, DataPoint dataPoint, ChartMapper.BackgroundImageInfo backgroundImageInfo)
		{
			if (chartMarker == null)
			{
				return;
			}
			this.SetMarkerProperties(chartMarker, dataPoint);
			this.RenderDataPointMarkerStyle(chartMarker, dataPoint, backgroundImageInfo);
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x0002006E File Offset: 0x0001E26E
		private void RenderSeriesMarker(ChartMarker chartMarker, Series series)
		{
			if (chartMarker == null)
			{
				return;
			}
			this.SetMarkerProperties(chartMarker, series);
			this.RenderSeriesMarkerStyle(chartMarker, series);
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x00020084 File Offset: 0x0001E284
		private void RenderEmptyPointMarker(ChartMarker chartMarker, DataPointAttributes dataPointAttributes)
		{
			if (chartMarker == null)
			{
				return;
			}
			this.SetMarkerProperties(chartMarker, dataPointAttributes);
			this.RenderEmptyPointMarkerStyle(chartMarker, dataPointAttributes);
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x0002009C File Offset: 0x0001E29C
		private void SetMarkerProperties(ChartMarker chartMarker, DataPointAttributes dataPointAttributes)
		{
			if (chartMarker.Size != null)
			{
				if (!chartMarker.Size.IsExpression)
				{
					if (chartMarker.Size.Value != null)
					{
						dataPointAttributes.MarkerSize = MappingHelper.ToIntPixels(chartMarker.Size.Value, base.DpiX);
					}
				}
				else if (chartMarker.Instance.Size != null)
				{
					dataPointAttributes.MarkerSize = MappingHelper.ToIntPixels(chartMarker.Instance.Size, base.DpiX);
				}
			}
			else
			{
				dataPointAttributes.MarkerSize = MappingHelper.ToIntPixels(ChartMapper.m_defaultMarkerSize, base.DpiX);
			}
			if (chartMarker.Type != null)
			{
				if (!chartMarker.Type.IsExpression)
				{
					dataPointAttributes.MarkerStyle = this.GetMarkerStyle(chartMarker.Type.Value);
					return;
				}
				dataPointAttributes.MarkerStyle = this.GetMarkerStyle(chartMarker.Instance.Type);
			}
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x00020170 File Offset: 0x0001E370
		private MarkerStyle GetMarkerStyle(ChartMarkerTypes chartMarkerType)
		{
			switch (chartMarkerType)
			{
			case ChartMarkerTypes.Square:
				return 1;
			case ChartMarkerTypes.Circle:
				return 2;
			case ChartMarkerTypes.Diamond:
				return 3;
			case ChartMarkerTypes.Triangle:
				return 4;
			case ChartMarkerTypes.Cross:
				return 5;
			case ChartMarkerTypes.Star4:
				return 6;
			case ChartMarkerTypes.Star5:
				return 7;
			case ChartMarkerTypes.Star6:
				return 8;
			case ChartMarkerTypes.Star10:
				return 9;
			case ChartMarkerTypes.Auto:
				if (this.m_autoMarker == null)
				{
					this.m_autoMarker = new ChartMapper.AutoMarker();
				}
				return this.m_autoMarker.Current;
			default:
				return 0;
			}
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x000201E2 File Offset: 0x0001E3E2
		private void RenderDataPointMarkerStyle(ChartMarker chartMarker, DataPoint dataPoint, ChartMapper.BackgroundImageInfo backgroundImageInfo)
		{
			this.RenderMarkerStyle(chartMarker, dataPoint);
			if (chartMarker.Style != null)
			{
				this.RenderMarkerBackgroundImage(chartMarker.Style.BackgroundImage, dataPoint, backgroundImageInfo);
			}
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x00020207 File Offset: 0x0001E407
		private void RenderSeriesMarkerStyle(ChartMarker chartMarker, Series series)
		{
			this.RenderMarkerStyle(chartMarker, series);
			if (chartMarker.Style != null)
			{
				this.RenderMarkerBackgroundImage(chartMarker.Style.BackgroundImage, series, null);
			}
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x0002022C File Offset: 0x0001E42C
		private void RenderEmptyPointMarkerStyle(ChartMarker chartMarker, DataPointAttributes emptyPoint)
		{
			this.RenderMarkerStyle(chartMarker, emptyPoint);
			if (chartMarker.Style != null)
			{
				this.RenderMarkerBackgroundImage(chartMarker.Style.BackgroundImage, emptyPoint, null);
			}
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x00020254 File Offset: 0x0001E454
		private void RenderMarkerStyle(ChartMarker chartMarker, DataPointAttributes dataPointAttributes)
		{
			Style style = chartMarker.Style;
			if (style == null)
			{
				return;
			}
			StyleInstance style2 = chartMarker.Instance.Style;
			if (MappingHelper.IsStylePropertyDefined(chartMarker.Style.Color))
			{
				dataPointAttributes.MarkerColor = MappingHelper.GetStyleColor(style, style2);
			}
			this.RenderMarkerBorder(chartMarker.Style.Border, dataPointAttributes);
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x000202A9 File Offset: 0x0001E4A9
		private void RenderMarkerBorder(Border border, DataPointAttributes dataPointAttributes)
		{
			if (border == null)
			{
				return;
			}
			if (MappingHelper.IsStylePropertyDefined(border.Color))
			{
				dataPointAttributes.MarkerBorderColor = MappingHelper.GetStyleBorderColor(border);
			}
			dataPointAttributes.MarkerBorderWidth = MappingHelper.GetStyleBorderWidth(border, base.DpiX);
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x000202DC File Offset: 0x0001E4DC
		private void RenderMarkerBackgroundImage(BackgroundImage backgroundImage, DataPointAttributes dataPointAttributes, ChartMapper.BackgroundImageInfo backgroundImageInfo)
		{
			dataPointAttributes.MarkerImage = this.GetImageName(backgroundImage, backgroundImageInfo);
			ChartImageWrapMode chartImageWrapMode;
			ChartImageAlign chartImageAlign;
			Color color;
			this.GetBackgroundImageProperties(backgroundImage, out chartImageWrapMode, out chartImageAlign, out color);
			dataPointAttributes.MarkerImageTransparentColor = color;
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x0002030C File Offset: 0x0001E50C
		private void RenderSmartLabels(ChartSmartLabel chartSmartLabels, SmartLabelsStyle smartLabels)
		{
			smartLabels.Enabled = true;
			smartLabels.CalloutLineWidth = MappingHelper.ToIntPixels(ChartMapper.m_defaultCalloutLineWidth, base.DpiX);
			smartLabels.MaxMovingDistance = MappingHelper.ToPixels(ChartMapper.m_defaultMaxMovingDistance, base.DpiX);
			if (chartSmartLabels == null)
			{
				return;
			}
			this.SetSmartLabelsProperties(chartSmartLabels, smartLabels);
			this.RenderNoMoveDirections(chartSmartLabels.NoMoveDirections, smartLabels);
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x00020368 File Offset: 0x0001E568
		private void SetSmartLabelsProperties(ChartSmartLabel chartSmartLabels, SmartLabelsStyle smartLabels)
		{
			if (chartSmartLabels.Disabled != null)
			{
				if (!chartSmartLabels.Disabled.IsExpression)
				{
					smartLabels.Enabled = !chartSmartLabels.Disabled.Value;
				}
				else
				{
					smartLabels.Enabled = !chartSmartLabels.Instance.Disabled;
				}
			}
			if (chartSmartLabels.AllowOutSidePlotArea != null)
			{
				if (!chartSmartLabels.AllowOutSidePlotArea.IsExpression)
				{
					smartLabels.AllowOutsidePlotArea = this.GetLabelOutsidePlotAreaStyle(chartSmartLabels.AllowOutSidePlotArea.Value);
				}
				else
				{
					smartLabels.AllowOutsidePlotArea = this.GetLabelOutsidePlotAreaStyle(chartSmartLabels.Instance.AllowOutSidePlotArea);
				}
			}
			Color empty = Color.Empty;
			if (chartSmartLabels.CalloutBackColor != null)
			{
				if (MappingHelper.GetColorFromReportColorProperty(chartSmartLabels.CalloutBackColor, ref empty))
				{
					smartLabels.CalloutBackColor = empty;
				}
				else if (chartSmartLabels.Instance.CalloutBackColor != null)
				{
					smartLabels.CalloutBackColor = chartSmartLabels.Instance.CalloutBackColor.ToColor();
				}
			}
			if (chartSmartLabels.CalloutLineAnchor != null)
			{
				if (!chartSmartLabels.CalloutLineAnchor.IsExpression)
				{
					smartLabels.CalloutLineAnchorCap = this.GetCalloutLineAnchor(chartSmartLabels.CalloutLineAnchor.Value);
				}
				else
				{
					smartLabels.CalloutLineAnchorCap = this.GetCalloutLineAnchor(chartSmartLabels.Instance.CalloutLineAnchor);
				}
			}
			if (chartSmartLabels.CalloutLineColor != null)
			{
				if (MappingHelper.GetColorFromReportColorProperty(chartSmartLabels.CalloutLineColor, ref empty))
				{
					smartLabels.CalloutLineColor = empty;
				}
				else if (chartSmartLabels.Instance.CalloutLineColor != null)
				{
					smartLabels.CalloutLineColor = chartSmartLabels.Instance.CalloutLineColor.ToColor();
				}
			}
			if (chartSmartLabels.CalloutLineStyle != null)
			{
				if (!chartSmartLabels.CalloutLineStyle.IsExpression)
				{
					smartLabels.CalloutLineStyle = this.GetCalloutLineStyle(chartSmartLabels.CalloutLineStyle.Value);
				}
				else
				{
					smartLabels.CalloutLineStyle = this.GetCalloutLineStyle(chartSmartLabels.Instance.CalloutLineStyle);
				}
			}
			if (chartSmartLabels.CalloutLineWidth != null)
			{
				if (!chartSmartLabels.CalloutLineWidth.IsExpression)
				{
					if (chartSmartLabels.CalloutLineWidth.Value != null)
					{
						smartLabels.CalloutLineWidth = MappingHelper.ToIntPixels(chartSmartLabels.CalloutLineWidth.Value, base.DpiX);
					}
				}
				else if (chartSmartLabels.Instance.CalloutLineWidth != null)
				{
					smartLabels.CalloutLineWidth = MappingHelper.ToIntPixels(chartSmartLabels.Instance.CalloutLineWidth, base.DpiX);
				}
			}
			if (chartSmartLabels.CalloutStyle != null)
			{
				if (!chartSmartLabels.CalloutStyle.IsExpression)
				{
					smartLabels.CalloutStyle = this.GetCalloutStyle(chartSmartLabels.CalloutStyle.Value);
				}
				else
				{
					smartLabels.CalloutStyle = this.GetCalloutStyle(chartSmartLabels.Instance.CalloutStyle);
				}
			}
			if (chartSmartLabels.ShowOverlapped != null)
			{
				if (!chartSmartLabels.ShowOverlapped.IsExpression)
				{
					smartLabels.HideOverlapped = !chartSmartLabels.ShowOverlapped.Value;
				}
				else
				{
					smartLabels.HideOverlapped = !chartSmartLabels.Instance.ShowOverlapped;
				}
			}
			if (chartSmartLabels.MarkerOverlapping != null)
			{
				if (!chartSmartLabels.MarkerOverlapping.IsExpression)
				{
					smartLabels.MarkerOverlapping = chartSmartLabels.MarkerOverlapping.Value;
				}
				else
				{
					smartLabels.MarkerOverlapping = chartSmartLabels.Instance.MarkerOverlapping;
				}
			}
			if (chartSmartLabels.MaxMovingDistance != null)
			{
				if (!chartSmartLabels.MaxMovingDistance.IsExpression)
				{
					if (chartSmartLabels.MaxMovingDistance.Value != null)
					{
						smartLabels.MaxMovingDistance = MappingHelper.ToPixels(chartSmartLabels.MaxMovingDistance.Value, base.DpiX);
					}
				}
				else if (chartSmartLabels.Instance.MaxMovingDistance != null)
				{
					smartLabels.MaxMovingDistance = MappingHelper.ToPixels(chartSmartLabels.Instance.MaxMovingDistance, base.DpiX);
				}
			}
			if (chartSmartLabels.MinMovingDistance != null)
			{
				if (!chartSmartLabels.MinMovingDistance.IsExpression)
				{
					if (chartSmartLabels.MinMovingDistance.Value != null)
					{
						smartLabels.MinMovingDistance = MappingHelper.ToPixels(chartSmartLabels.MinMovingDistance.Value, base.DpiX);
						return;
					}
				}
				else if (chartSmartLabels.Instance.MinMovingDistance != null)
				{
					smartLabels.MinMovingDistance = MappingHelper.ToPixels(chartSmartLabels.Instance.MinMovingDistance, base.DpiX);
				}
			}
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x00020706 File Offset: 0x0001E906
		private LineAnchorCap GetCalloutLineAnchor(ChartCalloutLineAnchor chartCalloutLineAnchor)
		{
			switch (chartCalloutLineAnchor)
			{
			case ChartCalloutLineAnchor.Arrow:
				return 1;
			case ChartCalloutLineAnchor.Diamond:
				return 2;
			case ChartCalloutLineAnchor.Square:
				return 3;
			case ChartCalloutLineAnchor.Round:
				return 4;
			default:
				return 0;
			}
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x00020729 File Offset: 0x0001E929
		private ChartDashStyle GetCalloutLineStyle(ChartCalloutLineStyle chartCalloutLineStyle)
		{
			switch (chartCalloutLineStyle)
			{
			case ChartCalloutLineStyle.Solid:
			case ChartCalloutLineStyle.Double:
				return 5;
			case ChartCalloutLineStyle.Dotted:
				return 4;
			case ChartCalloutLineStyle.Dashed:
				return 1;
			case ChartCalloutLineStyle.DashDot:
				return 2;
			case ChartCalloutLineStyle.DashDotDot:
				return 3;
			default:
				return 0;
			}
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x00020756 File Offset: 0x0001E956
		private LabelCalloutStyle GetCalloutStyle(ChartCalloutStyle chartCalloutStyle)
		{
			if (chartCalloutStyle == ChartCalloutStyle.Underline)
			{
				return 1;
			}
			if (chartCalloutStyle == ChartCalloutStyle.Box)
			{
				return 2;
			}
			return 0;
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x00020764 File Offset: 0x0001E964
		private void RenderNoMoveDirections(ChartNoMoveDirections chartNoMoveDirections, SmartLabelsStyle smartLabelsStyle)
		{
			if (chartNoMoveDirections == null)
			{
				return;
			}
			LabelAlignmentTypes labelAlignmentTypes = 0;
			if (chartNoMoveDirections.Down != null)
			{
				if (!chartNoMoveDirections.Down.IsExpression)
				{
					if (!chartNoMoveDirections.Down.Value)
					{
						labelAlignmentTypes |= 2;
					}
				}
				else if (!chartNoMoveDirections.Instance.Down)
				{
					labelAlignmentTypes |= 2;
				}
			}
			else
			{
				labelAlignmentTypes |= 2;
			}
			if (chartNoMoveDirections.DownLeft != null)
			{
				if (!chartNoMoveDirections.DownLeft.IsExpression)
				{
					if (!chartNoMoveDirections.DownLeft.Value)
					{
						labelAlignmentTypes |= 64;
					}
				}
				else if (!chartNoMoveDirections.Instance.DownLeft)
				{
					labelAlignmentTypes |= 64;
				}
			}
			else
			{
				labelAlignmentTypes |= 64;
			}
			if (chartNoMoveDirections.DownRight != null)
			{
				if (!chartNoMoveDirections.DownRight.IsExpression)
				{
					if (!chartNoMoveDirections.DownRight.Value)
					{
						labelAlignmentTypes |= 128;
					}
				}
				else if (!chartNoMoveDirections.Instance.DownRight)
				{
					labelAlignmentTypes |= 128;
				}
			}
			else
			{
				labelAlignmentTypes |= 128;
			}
			if (chartNoMoveDirections.Left != null)
			{
				if (!chartNoMoveDirections.Left.IsExpression)
				{
					if (!chartNoMoveDirections.Left.Value)
					{
						labelAlignmentTypes |= 8;
					}
				}
				else if (!chartNoMoveDirections.Instance.Left)
				{
					labelAlignmentTypes |= 8;
				}
			}
			else
			{
				labelAlignmentTypes |= 8;
			}
			if (chartNoMoveDirections.Right != null)
			{
				if (!chartNoMoveDirections.Right.IsExpression)
				{
					if (!chartNoMoveDirections.Right.Value)
					{
						labelAlignmentTypes |= 4;
					}
				}
				else if (!chartNoMoveDirections.Instance.Right)
				{
					labelAlignmentTypes |= 4;
				}
			}
			else
			{
				labelAlignmentTypes |= 4;
			}
			if (chartNoMoveDirections.Up != null)
			{
				if (!chartNoMoveDirections.Up.IsExpression)
				{
					if (!chartNoMoveDirections.Up.Value)
					{
						labelAlignmentTypes |= 1;
					}
				}
				else if (!chartNoMoveDirections.Instance.Up)
				{
					labelAlignmentTypes |= 1;
				}
			}
			else
			{
				labelAlignmentTypes |= 1;
			}
			if (chartNoMoveDirections.UpLeft != null)
			{
				if (!chartNoMoveDirections.UpLeft.IsExpression)
				{
					if (!chartNoMoveDirections.UpLeft.Value)
					{
						labelAlignmentTypes |= 16;
					}
				}
				else if (!chartNoMoveDirections.Instance.UpLeft)
				{
					labelAlignmentTypes |= 16;
				}
			}
			else
			{
				labelAlignmentTypes |= 16;
			}
			if (chartNoMoveDirections.UpRight != null)
			{
				if (!chartNoMoveDirections.UpRight.IsExpression)
				{
					if (!chartNoMoveDirections.UpRight.Value)
					{
						labelAlignmentTypes |= 32;
					}
				}
				else if (!chartNoMoveDirections.Instance.UpRight)
				{
					labelAlignmentTypes |= 32;
				}
			}
			else
			{
				labelAlignmentTypes |= 32;
			}
			smartLabelsStyle.MovingDirection = labelAlignmentTypes;
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x0002098B File Offset: 0x0001EB8B
		private void RenderAnnotations()
		{
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x0002098D File Offset: 0x0001EB8D
		private void RenderData()
		{
			this.RenderSeriesGroupings();
			this.PostProcessData();
			this.RenderCategoryGrouping();
			this.RenderSpecialChartTypes();
			this.OnPostApplyData();
			this.RenderDerivedSeriesCollecion();
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x000209B3 File Offset: 0x0001EBB3
		private void RenderSeriesGroupings()
		{
			this.RenderSeriesGroupingCollection(this.m_chart.SeriesHierarchy.MemberCollection);
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x000209CC File Offset: 0x0001EBCC
		private void RenderSeriesGroupingCollection(ChartMemberCollection seriesGroupingCollection)
		{
			if (!this.m_multiRow)
			{
				this.m_multiRow = seriesGroupingCollection.Count > 1;
			}
			foreach (ChartMember chartMember in seriesGroupingCollection)
			{
				this.RenderSeriesGrouping(chartMember);
			}
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x00020A2C File Offset: 0x0001EC2C
		private void RenderSeriesGrouping(ChartMember seriesGrouping)
		{
			if (!seriesGrouping.IsStatic)
			{
				ChartDynamicMemberInstance chartDynamicMemberInstance = (ChartDynamicMemberInstance)seriesGrouping.Instance;
				chartDynamicMemberInstance.ResetContext();
				this.m_multiRow = true;
				while (chartDynamicMemberInstance.MoveNext())
				{
					if (seriesGrouping.Children != null)
					{
						this.RenderSeriesGroupingCollection(seriesGrouping.Children);
					}
					else
					{
						this.RenderSeries(seriesGrouping);
					}
				}
				return;
			}
			if (seriesGrouping.Children != null)
			{
				this.RenderSeriesGroupingCollection(seriesGrouping.Children);
				return;
			}
			this.RenderSeries(seriesGrouping);
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x00020A9E File Offset: 0x0001EC9E
		private void RenderCategoryGroupings(ChartSeries chartSeries, ChartMember seriesGrouping, ChartMapper.SeriesInfo seriesInfo)
		{
			this.RenderCategoryGroupingCollection(chartSeries, seriesGrouping, this.m_chart.CategoryHierarchy.MemberCollection, seriesInfo);
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x00020ABC File Offset: 0x0001ECBC
		private void RenderCategoryGrouping(ChartSeries chartSeries, ChartMember seriesGrouping, ChartMember categoryGrouping, ChartMapper.SeriesInfo seriesInfo)
		{
			if (!categoryGrouping.IsStatic)
			{
				ChartDynamicMemberInstance chartDynamicMemberInstance = (ChartDynamicMemberInstance)categoryGrouping.Instance;
				chartDynamicMemberInstance.ResetContext();
				this.m_multiColumn = true;
				while (chartDynamicMemberInstance.MoveNext())
				{
					if (categoryGrouping.Children != null)
					{
						this.RenderCategoryGroupingCollection(chartSeries, seriesGrouping, categoryGrouping.Children, seriesInfo);
					}
					else
					{
						this.RenderDataPoint(chartSeries, seriesGrouping, categoryGrouping, seriesInfo, this.DataPointShowsInLegend(chartSeries));
					}
				}
				return;
			}
			if (categoryGrouping.Children != null)
			{
				this.RenderCategoryGroupingCollection(chartSeries, seriesGrouping, categoryGrouping.Children, seriesInfo);
				return;
			}
			this.RenderDataPoint(chartSeries, seriesGrouping, categoryGrouping, seriesInfo, this.DataPointShowsInLegend(chartSeries));
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x00020B4C File Offset: 0x0001ED4C
		private void RenderCategoryGroupingCollection(ChartSeries chartSeries, ChartMember seriesGrouping, ChartMemberCollection categoryGroupingCollection, ChartMapper.SeriesInfo seriesInfo)
		{
			int count = seriesInfo.Series.Points.Count;
			if (!this.m_multiColumn)
			{
				this.m_multiColumn = categoryGroupingCollection.Count > 1;
			}
			foreach (ChartMember chartMember in categoryGroupingCollection)
			{
				this.RenderCategoryGrouping(chartSeries, seriesGrouping, chartMember, seriesInfo);
			}
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x00020BC4 File Offset: 0x0001EDC4
		private void RenderCategoryGrouping()
		{
			foreach (KeyValuePair<string, ChartMapper.ChartAreaInfo> keyValuePair in this.m_chartAreaInfoDictionary)
			{
				bool flag = this.CanSetCategoryGroupingLabels(keyValuePair.Value);
				bool flag2 = this.VisualizeCategoryGrouping(keyValuePair.Value);
				CategoryNodeCollection categoryNodeCollection;
				if (flag2)
				{
					categoryNodeCollection = this.GetCategoryNodes(keyValuePair.Key);
				}
				else
				{
					categoryNodeCollection = null;
				}
				if (flag || flag2)
				{
					this.RenderChartAreaCategoryGroupings(keyValuePair, flag, categoryNodeCollection);
				}
			}
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x00020C54 File Offset: 0x0001EE54
		private bool VisualizeCategoryGrouping(ChartMapper.ChartAreaInfo chartAreaInfo)
		{
			using (List<ChartMapper.SeriesInfo>.Enumerator enumerator = chartAreaInfo.SeriesInfoList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Series.ChartType == 36)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x00020CB4 File Offset: 0x0001EEB4
		private void RenderChartAreaCategoryGroupings(KeyValuePair<string, ChartMapper.ChartAreaInfo> seriesInfoList, bool setCategoryGroupingLabels, CategoryNodeCollection categoryNodes)
		{
			int num = 0;
			foreach (ChartMember chartMember in this.m_chart.CategoryHierarchy.MemberCollection)
			{
				this.RenderCategoryGrouping(chartMember, seriesInfoList, ref num, setCategoryGroupingLabels, categoryNodes);
			}
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x00020D14 File Offset: 0x0001EF14
		private void RenderCategoryGrouping(ChartMember categoryGrouping, KeyValuePair<string, ChartMapper.ChartAreaInfo> seriesInfoList, ref int numberOfPoints, bool setCategoryGroupingLabels, CategoryNodeCollection categoryNodes)
		{
			if (!categoryGrouping.IsStatic)
			{
				ChartDynamicMemberInstance chartDynamicMemberInstance = (ChartDynamicMemberInstance)categoryGrouping.Instance;
				chartDynamicMemberInstance.ResetContext();
				while (chartDynamicMemberInstance.MoveNext())
				{
					CategoryNode categoryNode = this.GetCategoryNode(categoryGrouping, categoryNodes);
					if (categoryGrouping.Children != null)
					{
						this.RenderCategoryGroupingChildren(categoryGrouping, seriesInfoList, ref numberOfPoints, setCategoryGroupingLabels, categoryNode);
					}
					else
					{
						this.SetDataPointsCategoryGrouping(categoryGrouping, seriesInfoList, numberOfPoints, setCategoryGroupingLabels, categoryNode);
						numberOfPoints++;
					}
				}
				return;
			}
			CategoryNode categoryNode2 = this.GetCategoryNode(categoryGrouping, categoryNodes);
			if (categoryGrouping.Children != null)
			{
				this.RenderCategoryGroupingChildren(categoryGrouping, seriesInfoList, ref numberOfPoints, setCategoryGroupingLabels, categoryNode2);
				return;
			}
			this.SetDataPointsCategoryGrouping(categoryGrouping, seriesInfoList, numberOfPoints, setCategoryGroupingLabels, categoryNode2);
			numberOfPoints++;
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x00020DAC File Offset: 0x0001EFAC
		private CategoryNodeCollection GetCategoryNodes(string chartAreaName)
		{
			ChartArea chartArea = this.GetChartArea(chartAreaName);
			if (chartArea != null)
			{
				CategoryNodeCollection categoryNodeCollection = new CategoryNodeCollection(null);
				chartArea.CategoryNodes = categoryNodeCollection;
				return categoryNodeCollection;
			}
			return null;
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x00020DD8 File Offset: 0x0001EFD8
		private CategoryNode GetCategoryNode(ChartMember categoryGrouping, CategoryNodeCollection categoryNodes)
		{
			if (categoryNodes != null)
			{
				CategoryNode categoryNode = new CategoryNode(categoryNodes, this.IsCategoryEmpty(categoryGrouping), this.GetGroupingLabel(categoryGrouping));
				categoryNodes.Add(categoryNode);
				return categoryNode;
			}
			return null;
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x00020E07 File Offset: 0x0001F007
		private static CategoryNodeCollection GetCategoryChildren(CategoryNode categoryNode)
		{
			if (categoryNode != null)
			{
				categoryNode.Children = new CategoryNodeCollection(categoryNode);
				return categoryNode.Children;
			}
			return null;
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x00020E20 File Offset: 0x0001F020
		private bool IsCategoryEmpty(ChartMember categoryGrouping)
		{
			if (categoryGrouping.Group != null)
			{
				GroupExpressionValueCollection groupExpressions = categoryGrouping.Group.Instance.GroupExpressions;
				if (groupExpressions != null)
				{
					for (int i = 0; i < groupExpressions.Count; i++)
					{
						if (categoryGrouping.Group.Instance.GroupExpressions[i] != null)
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x00020E75 File Offset: 0x0001F075
		private bool IsDynamicOrHasDynamicParentMember(ChartMember member)
		{
			return member != null && (!member.IsStatic || this.IsDynamicOrHasDynamicParentMember(member.Parent));
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x00020E98 File Offset: 0x0001F098
		private bool HasDynamicMember(ChartMemberCollection members)
		{
			if (members == null)
			{
				return false;
			}
			foreach (ChartMember chartMember in members)
			{
				if (!chartMember.IsStatic)
				{
					return true;
				}
				if (this.HasDynamicMember(chartMember.Children))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x00020F00 File Offset: 0x0001F100
		private void RenderCategoryGroupingChildren(ChartMember categoryGrouping, KeyValuePair<string, ChartMapper.ChartAreaInfo> seriesInfoList, ref int numberOfPoints, bool setCategoryGroupingLabels, CategoryNode categoryNode)
		{
			int num = numberOfPoints;
			CategoryNodeCollection categoryChildren = ChartMapper.GetCategoryChildren(categoryNode);
			foreach (ChartMember chartMember in categoryGrouping.Children)
			{
				this.RenderCategoryGrouping(chartMember, seriesInfoList, ref numberOfPoints, setCategoryGroupingLabels, categoryChildren);
			}
			if (setCategoryGroupingLabels)
			{
				this.AddAxisGroupingLabel(categoryGrouping, seriesInfoList, (double)(num + 1), (double)numberOfPoints);
			}
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x00020F70 File Offset: 0x0001F170
		private void SetDataPointsCategoryGrouping(ChartMember categoryGrouping, KeyValuePair<string, ChartMapper.ChartAreaInfo> seriesInfoList, int index, bool setCategoryGroupingLabels, CategoryNode categoryNode)
		{
			foreach (ChartMapper.SeriesInfo seriesInfo in seriesInfoList.Value.SeriesInfoList)
			{
				DataPoint dataPoint;
				try
				{
					dataPoint = seriesInfo.Series.Points[index];
				}
				catch (Exception ex)
				{
					if (AsynchronousExceptionDetection.IsStoppingException(ex))
					{
						throw;
					}
					Global.Tracer.Trace(TraceLevel.Verbose, ex.Message);
					continue;
				}
				this.SetDataPointGrouping(categoryGrouping, seriesInfoList, dataPoint, seriesInfo, setCategoryGroupingLabels, categoryNode);
			}
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x00021010 File Offset: 0x0001F210
		private void SetDataPointGrouping(ChartMember categoryGrouping, KeyValuePair<string, ChartMapper.ChartAreaInfo> seriesInfoList, DataPoint dataPoint, ChartMapper.SeriesInfo seriesInfo, bool setCategoryGroupingLabels, CategoryNode categoryNode)
		{
			if (categoryNode == null)
			{
				if (setCategoryGroupingLabels && this.CanSetDataPointAxisLabel(seriesInfo.Series, dataPoint))
				{
					dataPoint.AxisLabel = this.GetFormattedGroupingLabel(categoryGrouping, seriesInfoList.Key, seriesInfo.ChartCategoryAxis);
					return;
				}
			}
			else
			{
				categoryNode.AddDataPoint(dataPoint);
			}
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x00021050 File Offset: 0x0001F250
		private void AddAxisGroupingLabel(ChartMember categoryGrouping, KeyValuePair<string, ChartMapper.ChartAreaInfo> seriesInfoList, double startPointIndex, double endPointIndex)
		{
			int groupingLevel = this.GetGroupingLevel(categoryGrouping);
			LabelMark labelMark = 2;
			double num = 0.4;
			bool flag = false;
			bool flag2 = false;
			foreach (ChartMapper.SeriesInfo seriesInfo in seriesInfoList.Value.SeriesInfoList)
			{
				if (seriesInfo.Series.XAxisType == null)
				{
					flag = true;
				}
				else if (seriesInfo.Series.XAxisType == 1)
				{
					flag2 = true;
				}
			}
			ChartArea chartArea = this.GetChartArea(seriesInfoList.Key);
			if (chartArea != null)
			{
				if (flag)
				{
					if (!chartArea.AxisX.Margin)
					{
						chartArea.AxisX.LabelStyle.ShowEndLabels = true;
					}
					chartArea.AxisX.CustomLabels.Add(startPointIndex - num, endPointIndex + num, this.GetGroupingLabel(categoryGrouping), groupingLevel, labelMark);
				}
				if (flag2 && !flag)
				{
					if (!chartArea.AxisX2.Margin)
					{
						chartArea.AxisX2.LabelStyle.ShowEndLabels = true;
					}
					chartArea.AxisX2.CustomLabels.Add(startPointIndex - num, endPointIndex + num, this.GetGroupingLabel(categoryGrouping), groupingLevel, labelMark);
				}
			}
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x00021188 File Offset: 0x0001F388
		private void RenderSeries(ChartMember seriesGrouping)
		{
			ChartSeries chartSeries = this.m_chart.ChartData.SeriesCollection[seriesGrouping.MemberCellIndex];
			bool flag = this.DataPointShowsInLegend(chartSeries);
			ChartMapper.SeriesInfo seriesInfo;
			if (flag)
			{
				seriesInfo = this.GetShapeSeriesOnSameChartArea(chartSeries);
				if (seriesInfo == null)
				{
					seriesInfo = this.CreateSeries(seriesGrouping, chartSeries);
					seriesInfo.IsExploded = this.IsSeriesExploded(chartSeries);
					seriesInfo.IsAttachedToScalarAxis = this.IsSeriesAttachedToScalarAxis(seriesInfo);
					seriesInfo.Series.SetAttribute(ChartMapper.m_pieAutoAxisLabelsName, "False");
					this.RenderSeries(seriesGrouping, seriesInfo.ChartSeries, seriesInfo.Series, seriesInfo.IsLine);
				}
			}
			else
			{
				seriesInfo = this.CreateSeries(seriesGrouping, chartSeries);
				seriesInfo.IsLine = this.IsSeriesLine(chartSeries);
				seriesInfo.IsRange = this.IsSeriesRange(chartSeries);
				seriesInfo.IsBubble = this.IsSeriesBubble(chartSeries);
				seriesInfo.IsAttachedToScalarAxis = this.IsSeriesAttachedToScalarAxis(seriesInfo);
				seriesInfo.IsGradientPerDataPointSupported = this.IsGradientPerDataPointSupported(chartSeries);
				this.RenderSeries(seriesGrouping, seriesInfo.ChartSeries, seriesInfo.Series, seriesInfo.IsLine);
			}
			seriesInfo.DataPointBackgroundImageInfoCollection.Initialize(chartSeries);
			this.RenderCategoryGroupings(chartSeries, seriesGrouping, seriesInfo);
			if (!flag)
			{
				this.AdjustNonShapeSeriesAppearance(seriesInfo);
			}
			this.OnPostApplySeriesData(seriesInfo.Series);
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x000212AC File Offset: 0x0001F4AC
		private void AdjustNonShapeSeriesAppearance(ChartMapper.SeriesInfo seriesInfo)
		{
			bool flag = false;
			if (seriesInfo.IsDataPointColorEmpty != null)
			{
				flag = seriesInfo.IsDataPointColorEmpty.Value;
			}
			else if (seriesInfo.DefaultDataPointAppearance != null)
			{
				flag = seriesInfo.DefaultDataPointAppearance.Color == Color.Empty;
			}
			if (!flag)
			{
				DataPoint dataPoint = this.GetFirstNonEmptyDataPoint(seriesInfo.Series);
				if (dataPoint == null)
				{
					dataPoint = seriesInfo.DefaultDataPointAppearance;
				}
				if (dataPoint != null)
				{
					seriesInfo.Series.Color = dataPoint.Color;
				}
				else
				{
					seriesInfo.Series.Color = Color.White;
				}
			}
			if (seriesInfo.IsDataPointHatchDefined && this.m_hatcher != null)
			{
				seriesInfo.Series.BackHatchStyle = 0;
			}
			if (!seriesInfo.IsGradientPerDataPointSupported && seriesInfo.IsGradientSupported)
			{
				if (seriesInfo.Color != null)
				{
					seriesInfo.Series.Color = seriesInfo.Color.Value;
				}
				if (seriesInfo.BackGradientEndColor != null)
				{
					seriesInfo.Series.BackGradientEndColor = seriesInfo.BackGradientEndColor.Value;
				}
				if (seriesInfo.BackGradientType != null)
				{
					seriesInfo.Series.BackGradientType = seriesInfo.BackGradientType.Value;
				}
			}
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x000213CC File Offset: 0x0001F5CC
		private void RenderSpecialChartTypes()
		{
			foreach (ChartMapper.ChartAreaInfo chartAreaInfo in this.m_chartAreaInfoDictionary.Values)
			{
				foreach (ChartMapper.SeriesInfo seriesInfo in chartAreaInfo.SeriesInfoList)
				{
					if (this.IsSeriesCollectedPie(seriesInfo.Series))
					{
						this.ShowPieAsCollected(seriesInfo.Series);
					}
					else
					{
						bool flag = this.IsSeriesPareto(seriesInfo.Series);
						bool flag2 = this.IsSeriesHistogram(seriesInfo.Series);
						if ((flag || flag2) && this.ChartAreaHasMultipleSeries(seriesInfo.Series) && this.IsCategoryHierarchyValidForHistogramAndPareto(seriesInfo) && !this.IsDynamicOrHasDynamicParentMember(seriesInfo.SeriesGrouping))
						{
							if (flag)
							{
								this.MakeParetoChart(seriesInfo.Series);
							}
							else if (flag2)
							{
								this.MakeHistogramChart(seriesInfo.Series);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x000214E8 File Offset: 0x0001F6E8
		private bool ChartAreaHasMultipleSeries(Series series)
		{
			bool flag = true;
			foreach (object obj in this.m_coreChart.Series)
			{
				Series series2 = (Series)obj;
				if (series != series2 && series.ChartArea == series2.ChartArea)
				{
					flag = false;
					break;
				}
			}
			return flag;
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x00021560 File Offset: 0x0001F760
		private bool IsCategoryHierarchyValidForHistogramAndPareto(ChartMapper.SeriesInfo seriesInfo)
		{
			return this.m_chart.CategoryHierarchy.MemberCollection.Count == 0 || (!this.HasDynamicMember(this.m_chart.CategoryHierarchy.MemberCollection[0].Children) && seriesInfo.ChartSeries.Count <= 1);
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x000215BC File Offset: 0x0001F7BC
		private void MakeParetoChart(Series series)
		{
			try
			{
				new ParetoHelper().MakeParetoChart(this.m_coreChart, series.Name, series.LegendText + " Pareto Line");
			}
			catch (Exception ex)
			{
				if (AsynchronousExceptionDetection.IsStoppingException(ex))
				{
					throw;
				}
				Global.Tracer.Trace(TraceLevel.Verbose, ex.Message);
			}
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x00021620 File Offset: 0x0001F820
		private void MakeHistogramChart(Series series)
		{
			try
			{
				HistogramHelper histogramHelper = new HistogramHelper();
				try
				{
					histogramHelper.SegmentIntervalNumber = int.Parse(series["HistogramSegmentIntervalNumber"], CultureInfo.InvariantCulture);
				}
				catch (Exception ex)
				{
					if (AsynchronousExceptionDetection.IsStoppingException(ex))
					{
						throw;
					}
					Global.Tracer.Trace(TraceLevel.Verbose, ex.Message);
				}
				try
				{
					histogramHelper.SegmentIntervalWidth = double.Parse(series["HistogramSegmentIntervalWidth"], CultureInfo.InvariantCulture);
				}
				catch (Exception ex2)
				{
					if (AsynchronousExceptionDetection.IsStoppingException(ex2))
					{
						throw;
					}
					Global.Tracer.Trace(TraceLevel.Verbose, ex2.Message);
				}
				try
				{
					histogramHelper.ShowPercentOnSecondaryYAxis = bool.Parse(series["HistogramShowPercentOnSecondaryYAxis"]);
				}
				catch (Exception ex3)
				{
					if (AsynchronousExceptionDetection.IsStoppingException(ex3))
					{
						throw;
					}
					Global.Tracer.Trace(TraceLevel.Verbose, ex3.Message);
				}
				histogramHelper.CreateHistogram(this.m_coreChart, series.Name, series.Name + "_Histogram", series.LegendText + " Histogram");
			}
			catch (Exception ex4)
			{
				if (AsynchronousExceptionDetection.IsStoppingException(ex4))
				{
					throw;
				}
				Global.Tracer.Trace(TraceLevel.Verbose, ex4.Message);
			}
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x0002176C File Offset: 0x0001F96C
		private void ShowPieAsCollected(Series series)
		{
			try
			{
				CollectedPieHelper collectedPieHelper = new CollectedPieHelper(this.m_coreChart);
				bool flag = false;
				bool.TryParse(series["CollectedChartShowLegend"], out flag);
				collectedPieHelper.ShowCollectedLegend = flag;
				bool flag2 = false;
				bool.TryParse(series["CollectedChartShowLabels"], out flag2);
				collectedPieHelper.ShowCollectedPointLabels = flag2;
				string text = series["CollectedLabel"];
				if (text != null)
				{
					collectedPieHelper.CollectedLabel = text;
				}
				string text2 = series["CollectedColor"];
				if (text2 != null && text2 != "")
				{
					Color color = Color.Empty;
					try
					{
						ColorConverter colorConverter = new ColorConverter();
						if (string.Compare(text2, "Empty", StringComparison.OrdinalIgnoreCase) == 0)
						{
							color = Color.Empty;
						}
						else
						{
							color = (Color)colorConverter.ConvertFromString(null, CultureInfo.InvariantCulture, text2);
						}
					}
					catch (Exception ex)
					{
						if (AsynchronousExceptionDetection.IsStoppingException(ex))
						{
							throw;
						}
						color = Color.Empty;
					}
					collectedPieHelper.SliceColor = color;
				}
				double num = 5.0;
				try
				{
					num = double.Parse(series["CollectedThreshold"], CultureInfo.InvariantCulture);
				}
				catch
				{
					num = 5.0;
				}
				collectedPieHelper.CollectedPercentage = num;
				collectedPieHelper.ShowCollectedDataAsOneSlice = true;
				collectedPieHelper.SupplementedAreaSizeRatio = 0.9f;
				collectedPieHelper.ShowSmallSegmentsAsSupplementalPie(series);
			}
			catch (Exception ex2)
			{
				if (AsynchronousExceptionDetection.IsStoppingException(ex2))
				{
					throw;
				}
				Global.Tracer.Trace(TraceLevel.Verbose, ex2.Message);
			}
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x0002190C File Offset: 0x0001FB0C
		private ChartMapper.SeriesInfo GetShapeSeriesOnSameChartArea(ChartSeries chartSeries)
		{
			foreach (KeyValuePair<string, ChartMapper.ChartAreaInfo> keyValuePair in this.m_chartAreaInfoDictionary)
			{
				if (keyValuePair.Key == this.GetSeriesChartAreaName(chartSeries))
				{
					foreach (ChartMapper.SeriesInfo seriesInfo in keyValuePair.Value.SeriesInfoList)
					{
						if (this.DataPointShowsInLegend(seriesInfo.ChartSeries))
						{
							return seriesInfo;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x000219C8 File Offset: 0x0001FBC8
		private ChartMapper.SeriesInfo CreateSeries(ChartMember seriesGrouping, ChartSeries chartSeries)
		{
			Series series = new Series();
			ChartMapper.SeriesInfo seriesInfo = new ChartMapper.SeriesInfo();
			seriesInfo.Series = series;
			seriesInfo.ChartSeries = chartSeries;
			seriesInfo.SeriesGrouping = seriesGrouping;
			series.ChartArea = this.GetSeriesChartAreaName(chartSeries);
			this.m_coreChart.Series.Add(series);
			this.AddSeriesToDictionary(seriesInfo);
			return seriesInfo;
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x00021A20 File Offset: 0x0001FC20
		private void RenderSeries(ChartMember seriesGrouping, ChartSeries chartSeries, Series series, bool isLine)
		{
			this.SetSeriesProperties(chartSeries, seriesGrouping, series);
			if (!this.DataPointShowsInLegend(chartSeries) && this.m_hatcher != null)
			{
				series.BackHatchStyle = this.m_hatcher.Current;
			}
			if (this.m_autoMarker != null)
			{
				this.m_autoMarker.MoveNext();
			}
			if (chartSeries.Style != null)
			{
				this.RenderSeriesStyle(chartSeries.Style, chartSeries.Instance.Style, series, isLine);
			}
			this.RenderItemInLegendActionInfo(chartSeries.ActionInfo, series.LegendToolTip, series);
			this.RenderItemInLegend(chartSeries.ChartItemInLegend, series, this.DataPointShowsInLegend(chartSeries));
			this.RenderCustomProperties(chartSeries.CustomProperties, series);
			this.RenderEmptyPoint(chartSeries.EmptyPoints, series.EmptyPointStyle, isLine);
			this.RenderSmartLabels(chartSeries.SmartLabel, series.SmartLabels);
			this.RenderDataLabel(chartSeries.DataLabel, series, false);
			this.RenderSeriesMarker(chartSeries.Marker, series);
			if (this.m_chartAreaInfoDictionary.ContainsKey(series.ChartArea))
			{
				ChartMapper.ChartAreaInfo chartAreaInfo = this.m_chartAreaInfoDictionary[series.ChartArea];
				if (chartAreaInfo != null)
				{
					ChartArea chartArea = this.GetChartArea(series.ChartArea);
					if (chartArea != null)
					{
						Axis categoryAxis = this.GetCategoryAxis(chartArea, series.XAxisType);
						if (categoryAxis != null && this.IsAxisAutoMargin(chartAreaInfo, categoryAxis) && this.DoesSeriesRequireMargin(chartSeries))
						{
							categoryAxis.Margin = true;
							chartAreaInfo.CategoryAxesAutoMargin.Remove(categoryAxis);
						}
					}
				}
			}
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x00021B74 File Offset: 0x0001FD74
		private Axis GetCategoryAxis(ChartArea area, AxisType axisType)
		{
			for (int i = 0; i < area.Axes.Length; i++)
			{
				Axis axis = area.Axes[i];
				if (axis.Type != 1 && axis.Type != 3 && ((axis.Type == null && axisType == null) || (axis.Type == 2 && axisType == 1)))
				{
					return axis;
				}
			}
			return null;
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x00021BCC File Offset: 0x0001FDCC
		private string GetSeriesChartAreaName(ChartSeries chartSeries)
		{
			if (chartSeries.ChartAreaName != null)
			{
				if (!chartSeries.ChartAreaName.IsExpression)
				{
					if (chartSeries.ChartAreaName.Value != null)
					{
						return chartSeries.ChartAreaName.Value;
					}
				}
				else if (chartSeries.Instance.ChartAreaName != null)
				{
					return chartSeries.Instance.ChartAreaName;
				}
			}
			return ChartMapper.m_defaulChartAreaName;
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x00021C28 File Offset: 0x0001FE28
		private string GetSeriesCategoryAxisName(ChartSeries chartSeries)
		{
			if (chartSeries.CategoryAxisName != null)
			{
				if (!chartSeries.CategoryAxisName.IsExpression)
				{
					if (chartSeries.CategoryAxisName.Value != null)
					{
						return chartSeries.CategoryAxisName.Value;
					}
				}
				else if (chartSeries.Instance.CategoryAxisName != null)
				{
					return chartSeries.Instance.CategoryAxisName;
				}
			}
			return "Primary";
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x00021C84 File Offset: 0x0001FE84
		private void SetSeriesProperties(ChartSeries chartSeries, ChartMember seriesGrouping, Series series)
		{
			this.SetSeriesType(chartSeries, series);
			if (seriesGrouping == null || !this.IsDynamicOrHasDynamicParentMember(seriesGrouping))
			{
				series.Name = chartSeries.Name;
			}
			if (chartSeries.LegendName != null)
			{
				if (!chartSeries.LegendName.IsExpression)
				{
					if (chartSeries.LegendName.Value != null)
					{
						series.Legend = chartSeries.LegendName.Value;
					}
				}
				else if (chartSeries.Instance.LegendName != null)
				{
					series.Legend = chartSeries.Instance.LegendName;
				}
			}
			if (chartSeries.LegendText != null)
			{
				if (!chartSeries.LegendText.IsExpression)
				{
					if (chartSeries.LegendText.Value != null)
					{
						series.LegendText = chartSeries.LegendText.Value;
					}
				}
				else if (chartSeries.Instance.LegendText != null)
				{
					series.LegendText = chartSeries.Instance.LegendText;
				}
			}
			if (chartSeries.HideInLegend != null)
			{
				if (!chartSeries.HideInLegend.IsExpression)
				{
					series.ShowInLegend = !chartSeries.HideInLegend.Value;
				}
				else
				{
					series.ShowInLegend = !chartSeries.Instance.HideInLegend;
				}
			}
			if (this.GetSeriesCategoryAxisName(chartSeries) == "Secondary")
			{
				series.XAxisType = 1;
			}
			else
			{
				series.XAxisType = 0;
			}
			if (chartSeries.ValueAxisName != null)
			{
				if (!chartSeries.ValueAxisName.IsExpression)
				{
					if (chartSeries.ValueAxisName.Value != null && chartSeries.ValueAxisName.Value == "Secondary")
					{
						series.YAxisType = 1;
					}
				}
				else if (chartSeries.Instance.ValueAxisName == "Secondary")
				{
					series.YAxisType = 1;
				}
			}
			ReportStringProperty toolTip = chartSeries.ToolTip;
			if (toolTip != null)
			{
				if (!toolTip.IsExpression)
				{
					if (toolTip.Value != null)
					{
						series.LegendToolTip = toolTip.Value;
					}
				}
				else
				{
					string toolTip2 = chartSeries.Instance.ToolTip;
					if (toolTip2 != null)
					{
						series.LegendToolTip = toolTip2;
					}
				}
			}
			ReportBoolProperty hidden = chartSeries.Hidden;
			if (hidden != null)
			{
				if (!hidden.IsExpression)
				{
					series.Enabled = !hidden.Value;
				}
				else
				{
					series.Enabled = !chartSeries.Instance.Hidden;
				}
			}
			if (seriesGrouping != null && !this.DataPointShowsInLegend(chartSeries) && series.LegendText == "")
			{
				series.LegendText = this.GetSeriesLegendText(seriesGrouping);
			}
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x00021EBC File Offset: 0x000200BC
		private void RenderDataPointStyle(Style style, StyleInstance styleInstance, DataPoint dataPoint, ChartMapper.SeriesInfo seriesInfo, int cellIndex)
		{
			this.RenderDataPointAttributesStyle(style, styleInstance, dataPoint, seriesInfo.IsLine);
			if (seriesInfo.IsGradientPerDataPointSupported)
			{
				this.RenderDataPointAttributesGradient(style, styleInstance, dataPoint);
			}
			else if (seriesInfo.IsGradientSupported)
			{
				seriesInfo.IsGradientSupported = this.CheckGradientSupport(style, styleInstance, dataPoint, seriesInfo);
			}
			this.RenderDataPointBackgroundImage(style.BackgroundImage, dataPoint, seriesInfo.DataPointBackgroundImageInfoCollection[cellIndex].DataPointBackgroundImage);
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x00021F28 File Offset: 0x00020128
		private bool CheckGradientSupport(Style style, StyleInstance styleInstance, DataPoint dataPoint, ChartMapper.SeriesInfo seriesInfo)
		{
			if (seriesInfo.BackGradientType == null)
			{
				seriesInfo.BackGradientType = new GradientType?(this.GetGradientType(MappingHelper.GetStyleBackGradientType(style, styleInstance)));
				GradientType? gradientType = seriesInfo.BackGradientType;
				GradientType gradientType2 = 0;
				if ((gradientType.GetValueOrDefault() == gradientType2) & (gradientType != null))
				{
					return false;
				}
			}
			else
			{
				GradientType? gradientType = seriesInfo.BackGradientType;
				GradientType gradientType2 = this.GetGradientType(MappingHelper.GetStyleBackGradientType(style, styleInstance));
				if (!((gradientType.GetValueOrDefault() == gradientType2) & (gradientType != null)))
				{
					return false;
				}
			}
			if (seriesInfo.Color == null)
			{
				seriesInfo.Color = new Color?(dataPoint.Color);
			}
			else if (seriesInfo.Color != dataPoint.Color)
			{
				return false;
			}
			if (seriesInfo.BackGradientEndColor == null)
			{
				seriesInfo.BackGradientEndColor = new Color?(MappingHelper.GetStyleBackGradientEndColor(style, styleInstance));
			}
			else if (seriesInfo.BackGradientEndColor != MappingHelper.GetStyleBackGradientEndColor(style, styleInstance))
			{
				return false;
			}
			return true;
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x00022048 File Offset: 0x00020248
		private string GetImageName(BackgroundImage backgroundImage, ChartMapper.BackgroundImageInfo backgroundImageInfo)
		{
			string text;
			if (backgroundImageInfo == null || !backgroundImageInfo.CanShareBackgroundImage || backgroundImageInfo.SharedBackgroundImageName == null)
			{
				text = this.CreateImage(backgroundImage);
				if (backgroundImageInfo != null && backgroundImageInfo.CanShareBackgroundImage)
				{
					backgroundImageInfo.SharedBackgroundImageName = text;
				}
			}
			else
			{
				text = backgroundImageInfo.SharedBackgroundImageName;
			}
			return text;
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x00022094 File Offset: 0x00020294
		private void RenderDataPointBackgroundImage(BackgroundImage backgroundImage, DataPoint dataPoint, ChartMapper.BackgroundImageInfo backgroundImageInfo)
		{
			dataPoint.BackImage = this.GetImageName(backgroundImage, backgroundImageInfo);
			ChartImageWrapMode chartImageWrapMode;
			ChartImageAlign chartImageAlign;
			Color color;
			this.GetBackgroundImageProperties(backgroundImage, out chartImageWrapMode, out chartImageAlign, out color);
			dataPoint.BackImageMode = chartImageWrapMode;
			dataPoint.BackImageAlign = chartImageAlign;
			dataPoint.BackImageTransparentColor = color;
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x000220D4 File Offset: 0x000202D4
		private void RenderSeriesStyle(Style style, StyleInstance styleInstance, Series series, bool isSeriesLine)
		{
			this.RenderDataPointAttributesStyle(style, styleInstance, series, isSeriesLine);
			this.RenderDataPointAttributesGradient(style, styleInstance, series);
			if (MappingHelper.IsStylePropertyDefined(style.ShadowColor))
			{
				series.ShadowColor = MappingHelper.GetStyleShadowColor(style, styleInstance);
			}
			if (MappingHelper.IsStylePropertyDefined(style.ShadowOffset))
			{
				series.ShadowOffset = MappingHelper.GetStyleShadowOffset(style, styleInstance, base.DpiX);
			}
			this.RenderDataPointAttributesBackgroundImage(style.BackgroundImage, series);
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x0002213C File Offset: 0x0002033C
		private void RenderEmptyPointStyle(Style style, StyleInstance styleInstance, DataPointAttributes dataPointAttributes, bool isSeriesLine)
		{
			this.RenderDataPointAttributesStyle(style, styleInstance, dataPointAttributes, isSeriesLine);
			this.RenderDataPointAttributesGradient(style, styleInstance, dataPointAttributes);
			this.RenderDataPointAttributesBackgroundImage(style.BackgroundImage, dataPointAttributes);
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x0002215F File Offset: 0x0002035F
		private void RenderDataPointAttributesGradient(Style style, StyleInstance styleInstance, DataPointAttributes dataPointAttributes)
		{
			if (MappingHelper.IsStylePropertyDefined(style.BackgroundGradientEndColor))
			{
				dataPointAttributes.BackGradientEndColor = MappingHelper.GetStyleBackGradientEndColor(style, styleInstance);
			}
			if (MappingHelper.IsStylePropertyDefined(style.BackgroundGradientType))
			{
				dataPointAttributes.BackGradientType = this.GetGradientType(MappingHelper.GetStyleBackGradientType(style, styleInstance));
			}
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x0002219C File Offset: 0x0002039C
		private void RenderDataPointAttributesStyle(Style style, StyleInstance styleInstance, DataPointAttributes dataPointAttributes, bool isSeriesLine)
		{
			if (MappingHelper.IsStylePropertyDefined(style.Color) && (!style.Color.IsExpression || (styleInstance.Color != null && styleInstance.Color.ToString() != "#00000000")))
			{
				dataPointAttributes.Color = MappingHelper.GetStyleColor(style, styleInstance);
			}
			if (MappingHelper.IsStylePropertyDefined(style.BackgroundHatchType))
			{
				dataPointAttributes.BackHatchStyle = this.GetHatchType(MappingHelper.GetStyleBackgroundHatchType(style, styleInstance));
			}
			this.RenderDataPointAttributesBorder(style.Border, dataPointAttributes, isSeriesLine);
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x00022220 File Offset: 0x00020420
		private void RenderDataLabelFont(ChartDataLabel chartDataLabel, DataPointAttributes dataPointAttributes, bool dataPoint)
		{
			Style style = chartDataLabel.Style;
			if (style == null)
			{
				if (dataPoint)
				{
					dataPointAttributes.Font = base.GetDefaultFontFromCache(this.m_coreChart.Series.Count);
					return;
				}
				dataPointAttributes.Font = base.GetDefaultFont();
				return;
			}
			else
			{
				if (dataPoint)
				{
					dataPointAttributes.Font = base.GetFontFromCache(this.m_coreChart.Series.Count, style, chartDataLabel.Instance.Style);
					return;
				}
				dataPointAttributes.Font = base.GetFont(style, chartDataLabel.Instance.Style);
				return;
			}
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x000222A8 File Offset: 0x000204A8
		private void RenderTitleFont(ChartTitle chartTitle, Title title)
		{
			Style style = chartTitle.Style;
			if (style == null)
			{
				title.Font = base.GetDefaultFont();
				return;
			}
			title.Font = base.GetFont(style, chartTitle.Instance.Style);
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x000222E4 File Offset: 0x000204E4
		private void RenderLegendTitleFont(ChartLegendTitle chartLegendTitle, Legend legend)
		{
			Style style = chartLegendTitle.Style;
			if (style == null)
			{
				legend.TitleFont = base.GetDefaultFont();
				return;
			}
			legend.TitleFont = base.GetFont(style, chartLegendTitle.Instance.Style);
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x00022320 File Offset: 0x00020520
		private void RenderLegendFont(ChartLegend chartLegend, Legend legend)
		{
			Style style = chartLegend.Style;
			if (style == null)
			{
				legend.Font = base.GetDefaultFont();
				return;
			}
			legend.Font = base.GetFont(style, chartLegend.Instance.Style);
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x0002235C File Offset: 0x0002055C
		private void RenderStripLineFont(ChartStripLine chartStripLine, StripLine stripLine)
		{
			Style style = chartStripLine.Style;
			if (style == null)
			{
				stripLine.TitleFont = base.GetDefaultFont();
				return;
			}
			stripLine.TitleFont = base.GetFont(style, chartStripLine.Instance.Style);
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x00022398 File Offset: 0x00020598
		private void RenderAxisScaleBreakBorder(Border border, AxisScaleBreakStyle axisScaleBreak)
		{
			if (border == null)
			{
				return;
			}
			if (MappingHelper.IsStylePropertyDefined(border.Color))
			{
				axisScaleBreak.LineColor = MappingHelper.GetStyleBorderColor(border);
			}
			if (MappingHelper.IsStylePropertyDefined(border.Style))
			{
				axisScaleBreak.LineStyle = this.GetBorderStyle(MappingHelper.GetStyleBorderStyle(border), true);
			}
			axisScaleBreak.LineWidth = MappingHelper.GetStyleBorderWidth(border, base.DpiX);
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x000223F4 File Offset: 0x000205F4
		private void RenderChartBorder(Border border)
		{
			this.m_coreChart.BorderColor = Color.Black;
			if (border == null)
			{
				return;
			}
			if (MappingHelper.IsStylePropertyDefined(border.Color))
			{
				this.m_coreChart.BorderColor = MappingHelper.GetStyleBorderColor(border);
			}
			if (MappingHelper.IsStylePropertyDefined(border.Style))
			{
				this.m_coreChart.BorderStyle = this.GetBorderStyle(MappingHelper.GetStyleBorderStyle(border), false);
			}
			this.m_coreChart.BorderWidth = MappingHelper.GetStyleBorderWidth(border, base.DpiX);
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x00022470 File Offset: 0x00020670
		private void RenderDataPointAttributesBorder(Border border, DataPointAttributes dataPointAttributes, bool isLine)
		{
			if (border == null)
			{
				return;
			}
			if (MappingHelper.IsStylePropertyDefined(border.Color))
			{
				dataPointAttributes.BorderColor = MappingHelper.GetStyleBorderColor(border);
			}
			if (MappingHelper.IsStylePropertyDefined(border.Style))
			{
				dataPointAttributes.BorderStyle = this.GetBorderStyle(MappingHelper.GetStyleBorderStyle(border), isLine);
			}
			dataPointAttributes.BorderWidth = MappingHelper.GetStyleBorderWidth(border, base.DpiX);
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x000224CC File Offset: 0x000206CC
		private void RenderTitleBorder(Border border, Title title)
		{
			title.BorderColor = Color.Black;
			title.BorderStyle = 0;
			if (border == null)
			{
				return;
			}
			if (MappingHelper.IsStylePropertyDefined(border.Color))
			{
				title.BorderColor = MappingHelper.GetStyleBorderColor(border);
			}
			if (MappingHelper.IsStylePropertyDefined(border.Style))
			{
				title.BorderStyle = this.GetBorderStyle(MappingHelper.GetStyleBorderStyle(border), false);
			}
			title.BorderWidth = MappingHelper.GetStyleBorderWidth(border, base.DpiX);
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x0002253C File Offset: 0x0002073C
		private void RenderChartAreaBorder(Border border, ChartArea area)
		{
			if (border == null)
			{
				return;
			}
			if (MappingHelper.IsStylePropertyDefined(border.Color))
			{
				area.BorderColor = MappingHelper.GetStyleBorderColor(border);
			}
			if (MappingHelper.IsStylePropertyDefined(border.Style))
			{
				area.BorderStyle = this.GetBorderStyle(MappingHelper.GetStyleBorderStyle(border), false);
			}
			area.BorderWidth = MappingHelper.GetStyleBorderWidth(border, base.DpiX);
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x00022598 File Offset: 0x00020798
		private void RenderStripLineBorder(Border border, StripLine stripLine)
		{
			stripLine.BorderStyle = 0;
			if (border == null)
			{
				return;
			}
			if (MappingHelper.IsStylePropertyDefined(border.Color))
			{
				stripLine.BorderColor = MappingHelper.GetStyleBorderColor(border);
			}
			if (MappingHelper.IsStylePropertyDefined(border.Style))
			{
				stripLine.BorderStyle = this.GetBorderStyle(MappingHelper.GetStyleBorderStyle(border), false);
			}
			stripLine.BorderWidth = MappingHelper.GetStyleBorderWidth(border, base.DpiX);
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x000225FC File Offset: 0x000207FC
		private void RenderBorderSkinBorder(Border border, BorderSkinAttributes borderSkin)
		{
			if (border == null)
			{
				return;
			}
			if (MappingHelper.IsStylePropertyDefined(border.Color))
			{
				borderSkin.FrameBorderColor = MappingHelper.GetStyleBorderColor(border);
			}
			if (MappingHelper.IsStylePropertyDefined(border.Style))
			{
				borderSkin.FrameBorderStyle = this.GetBorderStyle(MappingHelper.GetStyleBorderStyle(border), false);
			}
			borderSkin.FrameBorderWidth = MappingHelper.GetStyleBorderWidth(border, base.DpiX);
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x00022658 File Offset: 0x00020858
		private void RenderLegendBorder(Border border, Legend legend)
		{
			legend.BorderStyle = 0;
			if (border == null)
			{
				return;
			}
			if (MappingHelper.IsStylePropertyDefined(border.Color))
			{
				legend.BorderColor = MappingHelper.GetStyleBorderColor(border);
			}
			if (MappingHelper.IsStylePropertyDefined(border.Style))
			{
				legend.BorderStyle = this.GetBorderStyle(MappingHelper.GetStyleBorderStyle(border), false);
			}
			legend.BorderWidth = MappingHelper.GetStyleBorderWidth(border, base.DpiX);
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x000226BC File Offset: 0x000208BC
		private void RenderChartBackgroundImage(BackgroundImage backgroundImage)
		{
			string text;
			ChartImageWrapMode chartImageWrapMode;
			ChartImageAlign chartImageAlign;
			Color color;
			this.RenderBackgroundImage(backgroundImage, out text, out chartImageWrapMode, out chartImageAlign, out color);
			this.m_coreChart.BackImage = text;
			this.m_coreChart.BackImageMode = chartImageWrapMode;
			this.m_coreChart.BackImageAlign = chartImageAlign;
			this.m_coreChart.BackImageTransparentColor = color;
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x00022708 File Offset: 0x00020908
		private void RenderBorderSkinBackgroundImage(BackgroundImage backgroundImage, BorderSkinAttributes borderSkin)
		{
			string text;
			ChartImageWrapMode chartImageWrapMode;
			ChartImageAlign chartImageAlign;
			Color color;
			this.RenderBackgroundImage(backgroundImage, out text, out chartImageWrapMode, out chartImageAlign, out color);
			borderSkin.FrameBackImage = text;
			borderSkin.FrameBackImageMode = chartImageWrapMode;
			borderSkin.FrameBackImageAlign = chartImageAlign;
			borderSkin.FrameBackImageTransparentColor = color;
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x00022740 File Offset: 0x00020940
		private void RenderDataPointAttributesBackgroundImage(BackgroundImage backgroundImage, DataPointAttributes dataPointAttributes)
		{
			string text;
			ChartImageWrapMode chartImageWrapMode;
			ChartImageAlign chartImageAlign;
			Color color;
			this.RenderBackgroundImage(backgroundImage, out text, out chartImageWrapMode, out chartImageAlign, out color);
			dataPointAttributes.BackImage = text;
			dataPointAttributes.BackImageMode = chartImageWrapMode;
			dataPointAttributes.BackImageAlign = chartImageAlign;
			dataPointAttributes.BackImageTransparentColor = color;
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x00022778 File Offset: 0x00020978
		private void RenderTitleBackgroundImage(BackgroundImage backgroundImage, Title title)
		{
			string text;
			ChartImageWrapMode chartImageWrapMode;
			ChartImageAlign chartImageAlign;
			Color color;
			this.RenderBackgroundImage(backgroundImage, out text, out chartImageWrapMode, out chartImageAlign, out color);
			title.BackImage = text;
			title.BackImageMode = chartImageWrapMode;
			title.BackImageAlign = chartImageAlign;
			title.BackImageTransparentColor = color;
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x000227B0 File Offset: 0x000209B0
		private void RenderChartAreaBackgroundImage(BackgroundImage backgroundImage, ChartArea chartArea)
		{
			string text;
			ChartImageWrapMode chartImageWrapMode;
			ChartImageAlign chartImageAlign;
			Color color;
			this.RenderBackgroundImage(backgroundImage, out text, out chartImageWrapMode, out chartImageAlign, out color);
			chartArea.BackImage = text;
			chartArea.BackImageMode = chartImageWrapMode;
			chartArea.BackImageAlign = chartImageAlign;
			chartArea.BackImageTransparentColor = color;
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x000227E8 File Offset: 0x000209E8
		private void RenderLegendBackgroundImage(BackgroundImage backgroundImage, Legend legend)
		{
			string text;
			ChartImageWrapMode chartImageWrapMode;
			ChartImageAlign chartImageAlign;
			Color color;
			this.RenderBackgroundImage(backgroundImage, out text, out chartImageWrapMode, out chartImageAlign, out color);
			legend.BackImage = text;
			legend.BackImageMode = chartImageWrapMode;
			legend.BackImageAlign = chartImageAlign;
			legend.BackImageTransparentColor = color;
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x00022820 File Offset: 0x00020A20
		private void RenderBackgroundImage(BackgroundImage backgroundImage, out string backImage, out ChartImageWrapMode backImageMode, out ChartImageAlign backImageAlign, out Color backImageTransparentColor)
		{
			this.GetBackgroundImageProperties(backgroundImage, out backImageMode, out backImageAlign, out backImageTransparentColor);
			backImage = this.CreateImage(backgroundImage);
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x00022838 File Offset: 0x00020A38
		private void GetBackgroundImageProperties(BackgroundImage backgroundImage, out ChartImageWrapMode backImageMode, out ChartImageAlign backImageAlign, out Color backImageTransparentColor)
		{
			backImageMode = 4;
			backImageAlign = 8;
			backImageTransparentColor = Color.Empty;
			if (backgroundImage == null)
			{
				return;
			}
			if (MappingHelper.IsStylePropertyDefined(backgroundImage.BackgroundRepeat))
			{
				if (!backgroundImage.BackgroundRepeat.IsExpression)
				{
					backImageMode = this.GetBackImageMode(backgroundImage.BackgroundRepeat.Value);
				}
				else
				{
					backImageMode = this.GetBackImageMode(backgroundImage.Instance.BackgroundRepeat);
				}
			}
			if (MappingHelper.IsStylePropertyDefined(backgroundImage.Position))
			{
				if (!backgroundImage.Position.IsExpression)
				{
					backImageAlign = this.GetBackImageAlign(backgroundImage.Position.Value);
				}
				else
				{
					backImageAlign = this.GetBackImageAlign(backgroundImage.Instance.Position);
				}
			}
			if (MappingHelper.IsStylePropertyDefined(backgroundImage.TransparentColor))
			{
				Color empty = Color.Empty;
				if (MappingHelper.GetColorFromReportColorProperty(backgroundImage.TransparentColor, ref empty))
				{
					backImageTransparentColor = empty;
					return;
				}
				if (backgroundImage.Instance.TransparentColor != null)
				{
					backImageTransparentColor = backgroundImage.Instance.TransparentColor.ToColor();
				}
			}
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x00022930 File Offset: 0x00020B30
		private string CreateImage(BackgroundImage backgroundImage)
		{
			string text = "";
			if (backgroundImage != null)
			{
				global::System.Drawing.Image imageFromStream = this.GetImageFromStream(backgroundImage);
				if (imageFromStream != null)
				{
					text = ChartMapper.m_imagePrefix + this.m_coreChart.Images.Count.ToString();
					this.m_coreChart.Images.Add(text, imageFromStream);
				}
			}
			return text;
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x00022988 File Offset: 0x00020B88
		private void RenderStripLineBackgroundImage(BackgroundImage backgroundImage, StripLine stripLine)
		{
			string text;
			ChartImageWrapMode chartImageWrapMode;
			ChartImageAlign chartImageAlign;
			Color color;
			this.RenderBackgroundImage(backgroundImage, out text, out chartImageWrapMode, out chartImageAlign, out color);
			stripLine.BackImage = text;
			stripLine.BackImageMode = chartImageWrapMode;
			stripLine.BackImageAlign = chartImageAlign;
			stripLine.BackImageTransparentColor = color;
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x000229C0 File Offset: 0x00020BC0
		private global::System.Drawing.Image GetImageFromStream(BackgroundImage backgroundImage)
		{
			if (backgroundImage.Instance.ImageData == null)
			{
				return null;
			}
			return global::System.Drawing.Image.FromStream(new MemoryStream(backgroundImage.Instance.ImageData, false));
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x000229E8 File Offset: 0x00020BE8
		private void RenderActionInfo(ActionInfo actionInfo, string toolTip, IMapAreaAttributes mapAreaAttributes)
		{
			if (actionInfo == null && string.IsNullOrEmpty(toolTip))
			{
				return;
			}
			string text;
			ActionInfoWithDynamicImageMap actionInfoWithDynamicImageMap = MappingHelper.CreateActionInfoDynamic(this.m_chart, actionInfo, toolTip, out text);
			if (actionInfoWithDynamicImageMap != null)
			{
				if (text != null)
				{
					mapAreaAttributes.Href = text;
				}
				int count = this.m_actions.Count;
				this.m_actions.InternalList.Add(actionInfoWithDynamicImageMap);
				mapAreaAttributes.Tag = count;
			}
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x00022A48 File Offset: 0x00020C48
		private void RenderEmptyPoint(ChartEmptyPoints chartEmptyPoint, DataPointAttributes emptyPoint, bool isSeriesLine)
		{
			if (chartEmptyPoint == null)
			{
				return;
			}
			this.SetEmptyPointProperties(chartEmptyPoint, emptyPoint);
			if (chartEmptyPoint.Style != null)
			{
				this.RenderEmptyPointStyle(chartEmptyPoint.Style, chartEmptyPoint.Instance.Style, emptyPoint, isSeriesLine);
			}
			this.RenderActionInfo(chartEmptyPoint.ActionInfo, emptyPoint.ToolTip, emptyPoint);
			this.RenderEmptyPointMarker(chartEmptyPoint.Marker, emptyPoint);
			this.RenderDataLabel(chartEmptyPoint.DataLabel, emptyPoint, true);
			this.RenderCustomProperties(chartEmptyPoint.CustomProperties, emptyPoint);
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x00022AC0 File Offset: 0x00020CC0
		private void SetEmptyPointProperties(ChartEmptyPoints chartEmptyPoint, DataPointAttributes emptyPoint)
		{
			if (chartEmptyPoint.AxisLabel != null)
			{
				object obj;
				if (!chartEmptyPoint.AxisLabel.IsExpression)
				{
					obj = chartEmptyPoint.AxisLabel.Value;
				}
				else
				{
					obj = chartEmptyPoint.Instance.AxisLabel;
				}
				if (obj != null)
				{
					emptyPoint.AxisLabel = this.GetFormattedValue(obj, "");
				}
			}
			ReportStringProperty toolTip = chartEmptyPoint.ToolTip;
			if (toolTip != null)
			{
				if (!toolTip.IsExpression)
				{
					if (toolTip.Value != null)
					{
						emptyPoint.ToolTip = toolTip.Value;
						return;
					}
				}
				else
				{
					string toolTip2 = chartEmptyPoint.Instance.ToolTip;
					if (toolTip2 != null)
					{
						emptyPoint.ToolTip = toolTip2;
					}
				}
			}
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x00022B50 File Offset: 0x00020D50
		private void RenderItemInLegend(ChartItemInLegend chartItemInLegend, DataPointAttributes dataPointAttributes, bool dataPointShowsInLegend)
		{
			if (dataPointShowsInLegend)
			{
				return;
			}
			if (chartItemInLegend == null)
			{
				return;
			}
			if (chartItemInLegend.LegendText != null)
			{
				if (!chartItemInLegend.LegendText.IsExpression)
				{
					if (chartItemInLegend.LegendText.Value != null)
					{
						dataPointAttributes.LegendText = chartItemInLegend.LegendText.Value;
					}
				}
				else if (chartItemInLegend.Instance.LegendText != null)
				{
					dataPointAttributes.LegendText = chartItemInLegend.Instance.LegendText;
				}
			}
			ReportStringProperty toolTip = chartItemInLegend.ToolTip;
			if (toolTip != null)
			{
				if (!toolTip.IsExpression)
				{
					if (toolTip.Value != null)
					{
						dataPointAttributes.LegendToolTip = toolTip.Value;
					}
				}
				else
				{
					string toolTip2 = chartItemInLegend.Instance.ToolTip;
					if (toolTip2 != null)
					{
						dataPointAttributes.LegendToolTip = toolTip2;
					}
				}
			}
			ReportBoolProperty hidden = chartItemInLegend.Hidden;
			if (hidden != null)
			{
				if (!hidden.IsExpression)
				{
					dataPointAttributes.ShowInLegend = !hidden.Value;
				}
				else
				{
					dataPointAttributes.ShowInLegend = !chartItemInLegend.Instance.Hidden;
				}
			}
			this.RenderItemInLegendActionInfo(chartItemInLegend.ActionInfo, dataPointAttributes.LegendToolTip, dataPointAttributes);
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00022C40 File Offset: 0x00020E40
		private void RenderItemInLegendActionInfo(ActionInfo actionInfo, string toolTip, DataPointAttributes dataPointAttributes)
		{
			if (actionInfo == null && string.IsNullOrEmpty(toolTip))
			{
				return;
			}
			string text;
			ActionInfoWithDynamicImageMap actionInfoWithDynamicImageMap = MappingHelper.CreateActionInfoDynamic(this.m_chart, actionInfo, toolTip, out text);
			if (actionInfoWithDynamicImageMap != null)
			{
				if (text != null)
				{
					dataPointAttributes.LegendHref = text;
				}
				int count = this.m_actions.Count;
				this.m_actions.InternalList.Add(actionInfoWithDynamicImageMap);
				dataPointAttributes.LegendTag = count;
			}
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x00022CA0 File Offset: 0x00020EA0
		private bool GetCustomProperty(CustomProperty customProperty, ref string name, ref string value)
		{
			if (customProperty.Name == null)
			{
				return false;
			}
			if (customProperty.Value == null)
			{
				return false;
			}
			if (!customProperty.Name.IsExpression)
			{
				if (customProperty.Name.Value == null)
				{
					return false;
				}
				name = customProperty.Name.Value;
			}
			else
			{
				if (customProperty.Instance.Name == null)
				{
					return false;
				}
				name = customProperty.Instance.Name;
			}
			if (!customProperty.Value.IsExpression)
			{
				if (customProperty.Value.Value == null)
				{
					return false;
				}
				value = Convert.ToString(customProperty.Value.Value, CultureInfo.InvariantCulture);
			}
			else
			{
				if (customProperty.Instance.Value == null)
				{
					return false;
				}
				value = Convert.ToString(customProperty.Instance.Value, CultureInfo.InvariantCulture);
			}
			return true;
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x00022D68 File Offset: 0x00020F68
		private void RenderCustomProperties(CustomPropertyCollection customProperties, DataPointAttributes dataPointAttributes)
		{
			if (customProperties == null)
			{
				return;
			}
			string text = null;
			string text2 = null;
			foreach (CustomProperty customProperty in customProperties)
			{
				if (this.GetCustomProperty(customProperty, ref text, ref text2))
				{
					dataPointAttributes.SetAttribute(text, text2);
				}
			}
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x00022DC8 File Offset: 0x00020FC8
		private void SetSeriesType(ChartSeries chartSeries, Series series)
		{
			ChartSeriesType seriesType = this.GetSeriesType(chartSeries);
			ChartSeriesSubtype validSeriesSubType = this.GetValidSeriesSubType(seriesType, this.GetSeriesSubType(chartSeries));
			switch (seriesType)
			{
			case ChartSeriesType.Column:
				this.SetSeriesTypeColumn(series, validSeriesSubType);
				return;
			case ChartSeriesType.Bar:
				this.SetSeriesTypeBar(series, validSeriesSubType);
				return;
			case ChartSeriesType.Line:
				this.SetSeriesTypeLine(series, validSeriesSubType);
				return;
			case ChartSeriesType.Shape:
				this.SetSeriesTypeShape(series, validSeriesSubType);
				return;
			case ChartSeriesType.Scatter:
				this.SetSeriesTypeScatter(series, validSeriesSubType);
				return;
			case ChartSeriesType.Area:
				this.SetSeriesTypeArea(series, validSeriesSubType);
				return;
			case ChartSeriesType.Range:
				this.SetSeriesTypeRange(series, validSeriesSubType);
				return;
			case ChartSeriesType.Polar:
				this.SetSeriesTypePolar(series, validSeriesSubType);
				return;
			default:
				return;
			}
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x00022E5E File Offset: 0x0002105E
		private ChartSeriesType GetSeriesType(ChartSeries chartSeries)
		{
			if (chartSeries.Type != null)
			{
				if (!chartSeries.Type.IsExpression)
				{
					return chartSeries.Type.Value;
				}
				if (chartSeries.Instance != null)
				{
					return chartSeries.Instance.Type;
				}
			}
			return ChartSeriesType.Column;
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x00022E96 File Offset: 0x00021096
		private ChartSeriesSubtype GetSeriesSubType(ChartSeries chartSeries)
		{
			if (chartSeries.Subtype != null)
			{
				if (!chartSeries.Subtype.IsExpression)
				{
					return chartSeries.Subtype.Value;
				}
				if (chartSeries.Instance != null)
				{
					return chartSeries.Instance.Subtype;
				}
			}
			return ChartSeriesSubtype.Plain;
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x00022ED0 File Offset: 0x000210D0
		private ChartSeriesSubtype GetValidSeriesSubType(ChartSeriesType type, ChartSeriesSubtype subtype)
		{
			switch (type)
			{
			case ChartSeriesType.Column:
			case ChartSeriesType.Bar:
				if (subtype == ChartSeriesSubtype.Stacked || subtype == ChartSeriesSubtype.PercentStacked)
				{
					return subtype;
				}
				break;
			case ChartSeriesType.Line:
				if (subtype == ChartSeriesSubtype.Smooth || subtype == ChartSeriesSubtype.Stepped)
				{
					return subtype;
				}
				break;
			case ChartSeriesType.Shape:
				if (subtype - ChartSeriesSubtype.ExplodedPie <= 4 || subtype - ChartSeriesSubtype.TreeMap <= 1)
				{
					return subtype;
				}
				return ChartSeriesSubtype.Pie;
			case ChartSeriesType.Scatter:
				if (subtype == ChartSeriesSubtype.Bubble)
				{
					return subtype;
				}
				break;
			case ChartSeriesType.Area:
				if (subtype == ChartSeriesSubtype.Smooth || subtype == ChartSeriesSubtype.Stacked || subtype == ChartSeriesSubtype.PercentStacked)
				{
					return subtype;
				}
				break;
			case ChartSeriesType.Range:
				if (subtype == ChartSeriesSubtype.Smooth || subtype - ChartSeriesSubtype.Candlestick <= 5)
				{
					return subtype;
				}
				break;
			case ChartSeriesType.Polar:
				if (subtype == ChartSeriesSubtype.Radar)
				{
					return subtype;
				}
				break;
			}
			return ChartSeriesSubtype.Plain;
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x00022F54 File Offset: 0x00021154
		private void SetSeriesTypeArea(Series series, ChartSeriesSubtype subtype)
		{
			switch (subtype)
			{
			case ChartSeriesSubtype.Stacked:
				series.ChartType = 15;
				return;
			case ChartSeriesSubtype.PercentStacked:
				series.ChartType = 16;
				return;
			case ChartSeriesSubtype.Smooth:
				series.ChartType = 14;
				return;
			default:
				series.ChartType = 13;
				return;
			}
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x00022F8F File Offset: 0x0002118F
		private void SetSeriesTypeBar(Series series, ChartSeriesSubtype subtype)
		{
			if (subtype == ChartSeriesSubtype.Stacked)
			{
				series.ChartType = 8;
				return;
			}
			if (subtype != ChartSeriesSubtype.PercentStacked)
			{
				series.ChartType = 7;
				return;
			}
			series.ChartType = 9;
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x00022FB3 File Offset: 0x000211B3
		private void SetSeriesTypeColumn(Series series, ChartSeriesSubtype subtype)
		{
			if (subtype == ChartSeriesSubtype.Stacked)
			{
				series.ChartType = 11;
				return;
			}
			if (subtype != ChartSeriesSubtype.PercentStacked)
			{
				series.ChartType = 10;
				return;
			}
			series.ChartType = 12;
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x00022FD9 File Offset: 0x000211D9
		private void SetSeriesTypeLine(Series series, ChartSeriesSubtype subtype)
		{
			if (subtype == ChartSeriesSubtype.Smooth)
			{
				series.ChartType = 4;
				return;
			}
			if (subtype != ChartSeriesSubtype.Stepped)
			{
				series.ChartType = 3;
				return;
			}
			series.ChartType = 5;
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x00022FFC File Offset: 0x000211FC
		private void SetSeriesTypePolar(Series series, ChartSeriesSubtype subtype)
		{
			if (subtype == ChartSeriesSubtype.Radar)
			{
				series.ChartType = 25;
				return;
			}
			series.ChartType = 26;
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x00023014 File Offset: 0x00021214
		private void SetSeriesTypeRange(Series series, ChartSeriesSubtype subtype)
		{
			if (subtype == ChartSeriesSubtype.Smooth)
			{
				series.ChartType = 22;
				return;
			}
			switch (subtype)
			{
			case ChartSeriesSubtype.Candlestick:
				series.ChartType = 20;
				return;
			case ChartSeriesSubtype.Stock:
				series.ChartType = 19;
				return;
			case ChartSeriesSubtype.Bar:
				series.ChartType = 23;
				return;
			case ChartSeriesSubtype.Column:
				series.ChartType = 24;
				return;
			case ChartSeriesSubtype.BoxPlot:
				series.ChartType = 28;
				return;
			case ChartSeriesSubtype.ErrorBar:
				series.ChartType = 27;
				return;
			default:
				series.ChartType = 21;
				return;
			}
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x0002308F File Offset: 0x0002128F
		private void SetSeriesTypeScatter(Series series, ChartSeriesSubtype subtype)
		{
			if (subtype == ChartSeriesSubtype.Bubble)
			{
				series.ChartType = 2;
				return;
			}
			series.ChartType = 0;
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x000230A8 File Offset: 0x000212A8
		private void SetSeriesTypeShape(Series series, ChartSeriesSubtype subtype)
		{
			switch (subtype)
			{
			case ChartSeriesSubtype.Doughnut:
			case ChartSeriesSubtype.ExplodedDoughnut:
				series.ChartType = 18;
				return;
			case ChartSeriesSubtype.Funnel:
				series.ChartType = 33;
				return;
			case ChartSeriesSubtype.Pyramid:
				series.ChartType = 34;
				return;
			default:
				if (subtype == ChartSeriesSubtype.TreeMap)
				{
					series.ChartType = 35;
					return;
				}
				if (subtype != ChartSeriesSubtype.Sunburst)
				{
					series.ChartType = 17;
					return;
				}
				series.ChartType = 36;
				return;
			}
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x00023110 File Offset: 0x00021310
		private void RenderDataPoint(ChartSeries chartSeries, ChartMember seriesGrouping, ChartMember categoryGrouping, ChartMapper.SeriesInfo seriesInfo, bool dataPointShowsInLegend)
		{
			ChartDataPoint chartDataPoint = chartSeries[categoryGrouping.MemberCellIndex];
			if (chartDataPoint == null)
			{
				return;
			}
			DataPoint dataPoint = new DataPoint();
			int yvaluesCount = ChartMapper.GetYValuesCount(seriesInfo.Series.ChartType);
			if (yvaluesCount != 1)
			{
				dataPoint.YValues = new double[yvaluesCount];
			}
			this.RenderDataPointValues(chartDataPoint, dataPoint, seriesInfo, categoryGrouping);
			Style style = chartDataPoint.Style;
			if (!dataPoint.Empty)
			{
				this.SetDataPointProperties(chartDataPoint, dataPoint);
				if (dataPointShowsInLegend && this.m_hatcher != null)
				{
					dataPoint.BackHatchStyle = this.m_hatcher.Current;
				}
				if (style != null)
				{
					this.RenderDataPointStyle(style, chartDataPoint.Instance.Style, dataPoint, seriesInfo, categoryGrouping.MemberCellIndex);
					if (!seriesInfo.IsDataPointHatchDefined)
					{
						seriesInfo.IsDataPointHatchDefined = MappingHelper.IsStylePropertyDefined(style.BackgroundHatchType);
					}
				}
				if (seriesInfo.IsDataPointColorEmpty == null || !seriesInfo.IsDataPointColorEmpty.Value)
				{
					seriesInfo.IsDataPointColorEmpty = new bool?(dataPoint.Color == Color.Empty);
				}
				this.RenderActionInfo(chartDataPoint.ActionInfo, dataPoint.ToolTip, dataPoint);
				this.RenderDataLabel(chartDataPoint.DataLabel, dataPoint, true);
				this.RenderDataPointMarker(chartDataPoint.Marker, dataPoint, seriesInfo.DataPointBackgroundImageInfoCollection[categoryGrouping.MemberCellIndex].MarkerBackgroundImage);
				this.RenderItemInLegend(chartDataPoint.ItemInLegend, dataPoint, false);
				this.RenderCustomProperties(chartDataPoint.CustomProperties, dataPoint);
			}
			else if (!dataPointShowsInLegend && seriesInfo.DefaultDataPointAppearance == null)
			{
				seriesInfo.DefaultDataPointAppearance = new DataPoint();
				if (style != null)
				{
					this.RenderDefaultDataPointStyle(style, chartDataPoint.Instance.Style, seriesInfo.DefaultDataPointAppearance, seriesInfo, categoryGrouping.MemberCellIndex);
				}
				this.RenderDataPointMarker(chartDataPoint.Marker, seriesInfo.DefaultDataPointAppearance, seriesInfo.DataPointBackgroundImageInfoCollection[categoryGrouping.MemberCellIndex].MarkerBackgroundImage);
				this.RenderItemInLegend(chartDataPoint.ItemInLegend, seriesInfo.DefaultDataPointAppearance, false);
			}
			seriesInfo.Series.Points.Add(dataPoint);
			if (dataPointShowsInLegend)
			{
				if (seriesInfo.IsExploded)
				{
					dataPoint.SetAttribute("Exploded", "true");
				}
				if (this.CanSetPieDataPointLegendText(seriesInfo.Series, dataPoint))
				{
					dataPoint.LegendText = this.GetDataPointLegendText(categoryGrouping, seriesGrouping);
				}
			}
			this.OnPostApplySeriesPointData(seriesInfo.Series, seriesInfo.Series.Points.Count - 1);
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x00023354 File Offset: 0x00021554
		private void RenderDefaultDataPointStyle(Style style, StyleInstance styleInstance, DataPoint dataPoint, ChartMapper.SeriesInfo seriesInfo, int cellIndex)
		{
			this.RenderDataPointAttributesStyle(style, styleInstance, dataPoint, seriesInfo.IsLine);
			this.RenderDataPointAttributesGradient(style, styleInstance, dataPoint);
			this.RenderDataPointBackgroundImage(style.BackgroundImage, dataPoint, seriesInfo.DataPointBackgroundImageInfoCollection[cellIndex].DataPointBackgroundImage);
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x00023390 File Offset: 0x00021590
		public static int GetYValuesCount(SeriesChartType seriesType)
		{
			if (seriesType != 2)
			{
				switch (seriesType)
				{
				case 19:
				case 20:
					return 4;
				case 21:
				case 22:
				case 23:
				case 24:
				case 32:
					break;
				default:
					return 1;
				case 27:
					return 3;
				case 28:
					return 6;
				}
			}
			return 2;
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x000233EC File Offset: 0x000215EC
		private void SetDataPointProperties(ChartDataPoint chartDataPoint, DataPoint dataPoint)
		{
			if (chartDataPoint.AxisLabel != null)
			{
				object obj;
				if (!chartDataPoint.AxisLabel.IsExpression)
				{
					obj = chartDataPoint.AxisLabel.Value;
				}
				else
				{
					obj = chartDataPoint.Instance.AxisLabel;
				}
				if (obj != null)
				{
					dataPoint.AxisLabel = this.GetFormattedValue(obj, "");
				}
			}
			ReportStringProperty toolTip = chartDataPoint.ToolTip;
			if (toolTip != null)
			{
				if (!toolTip.IsExpression)
				{
					if (toolTip.Value != null)
					{
						dataPoint.ToolTip = toolTip.Value;
						return;
					}
				}
				else
				{
					string toolTip2 = chartDataPoint.Instance.ToolTip;
					if (toolTip2 != null)
					{
						dataPoint.ToolTip = toolTip2;
					}
				}
			}
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x0002347C File Offset: 0x0002167C
		private void RenderDataPointValues(ChartDataPoint chartDataPoint, DataPoint dataPoint, ChartMapper.SeriesInfo seriesInfo, ChartMember categoryGrouping)
		{
			if (chartDataPoint.DataPointValues != null)
			{
				this.SetDataPointXValue(chartDataPoint, dataPoint, seriesInfo, categoryGrouping);
				this.SetDataPointYValues(chartDataPoint, dataPoint, seriesInfo);
			}
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0002349A File Offset: 0x0002169A
		private void SetDataPointYValue(DataPoint dataPoint, int index, object value, ref ChartValueTypes? dateTimeType)
		{
			if (index < dataPoint.YValues.Length)
			{
				dataPoint.YValues[index] = this.ConvertToDouble(value, ref dateTimeType);
			}
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x000234B8 File Offset: 0x000216B8
		private void SetDataPointYValues(ChartDataPoint chartDataPoint, DataPoint dataPoint, ChartMapper.SeriesInfo seriesInfo)
		{
			ChartValueTypes? chartValueTypes = null;
			if (chartDataPoint.DataPointValues.Y != null)
			{
				this.SetDataPointYValue(dataPoint, 0, chartDataPoint.DataPointValues.Instance.Y, ref chartValueTypes);
			}
			if (seriesInfo.IsBubble && chartDataPoint.DataPointValues.Size != null)
			{
				this.SetDataPointYValue(dataPoint, 1, chartDataPoint.DataPointValues.Instance.Size, ref chartValueTypes);
			}
			if (seriesInfo.IsRange)
			{
				if (chartDataPoint.DataPointValues.High != null)
				{
					this.SetDataPointYValue(dataPoint, this.GetHighYValueIndex(seriesInfo), chartDataPoint.DataPointValues.Instance.High, ref chartValueTypes);
				}
				if (chartDataPoint.DataPointValues.Low != null)
				{
					this.SetDataPointYValue(dataPoint, this.GetLowYValueIndex(seriesInfo), chartDataPoint.DataPointValues.Instance.Low, ref chartValueTypes);
				}
				if (chartDataPoint.DataPointValues.Start != null)
				{
					this.SetDataPointYValue(dataPoint, this.GetStartYValueIndex(seriesInfo), chartDataPoint.DataPointValues.Instance.Start, ref chartValueTypes);
				}
				if (chartDataPoint.DataPointValues.End != null)
				{
					this.SetDataPointYValue(dataPoint, this.GetEndYValueIndex(seriesInfo), chartDataPoint.DataPointValues.Instance.End, ref chartValueTypes);
				}
				if (chartDataPoint.DataPointValues.Mean != null)
				{
					this.SetDataPointYValue(dataPoint, 4, chartDataPoint.DataPointValues.Instance.Mean, ref chartValueTypes);
				}
				if (chartDataPoint.DataPointValues.Median != null)
				{
					this.SetDataPointYValue(dataPoint, 5, chartDataPoint.DataPointValues.Instance.Median, ref chartValueTypes);
				}
			}
			if (chartValueTypes != null)
			{
				seriesInfo.Series.YValueType = chartValueTypes.Value;
			}
			for (int i = 0; i < dataPoint.YValues.Length; i++)
			{
				if (double.IsNaN(dataPoint.YValues[i]))
				{
					dataPoint.Empty = true;
					dataPoint.ShowLabelAsValue = false;
					return;
				}
			}
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x0002367C File Offset: 0x0002187C
		private int GetStartYValueIndex(ChartMapper.SeriesInfo seriesInfo)
		{
			if (seriesInfo.Series.ChartType == 23)
			{
				return 0;
			}
			return 2;
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x00023690 File Offset: 0x00021890
		private int GetEndYValueIndex(ChartMapper.SeriesInfo seriesInfo)
		{
			if (seriesInfo.Series.ChartType == 23)
			{
				return 1;
			}
			return 3;
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x000236A4 File Offset: 0x000218A4
		private int GetHighYValueIndex(ChartMapper.SeriesInfo seriesInfo)
		{
			SeriesChartType chartType = seriesInfo.Series.ChartType;
			if (chartType == 27)
			{
				return 2;
			}
			if (chartType == 28)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x000236CC File Offset: 0x000218CC
		private int GetLowYValueIndex(ChartMapper.SeriesInfo seriesInfo)
		{
			if (seriesInfo.Series.ChartType == 28)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x000236E0 File Offset: 0x000218E0
		private bool IsSeriesAttachedToScalarAxis(ChartMapper.SeriesInfo seriesInfo)
		{
			return seriesInfo.ChartAreaInfo.CategoryAxesScalar != null && seriesInfo.ChartAreaInfo.CategoryAxesScalar.Contains(this.GetSeriesCategoryAxisName(seriesInfo.ChartSeries));
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x0002370D File Offset: 0x0002190D
		private bool IsAxisAutoMargin(ChartMapper.ChartAreaInfo chartAreaInfo, Axis axis)
		{
			return chartAreaInfo.CategoryAxesAutoMargin != null && chartAreaInfo.CategoryAxesAutoMargin.Contains(axis);
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x00023728 File Offset: 0x00021928
		private void SetDataPointXValue(ChartDataPoint chartDataPoint, DataPoint dataPoint, ChartMapper.SeriesInfo seriesInfo, ChartMember categoryGrouping)
		{
			if (chartDataPoint.DataPointValues.X == null && !seriesInfo.IsAttachedToScalarAxis)
			{
				return;
			}
			object obj = null;
			ChartValueTypes? chartValueTypes = null;
			if (chartDataPoint.DataPointValues.X != null)
			{
				obj = chartDataPoint.DataPointValues.Instance.X;
			}
			else if (categoryGrouping.Group != null && categoryGrouping.Group.Instance.GroupExpressions != null && categoryGrouping.Group.Instance.GroupExpressions.Count > 0)
			{
				obj = categoryGrouping.Group.Instance.GroupExpressions[0];
			}
			if (obj == null)
			{
				seriesInfo.NullXValuePoints.Add(dataPoint);
			}
			else
			{
				double num = this.ConvertToDouble(obj, ref chartValueTypes);
				if (!double.IsNaN(num))
				{
					dataPoint.XValue = num;
					seriesInfo.XValueSet = true;
				}
				else
				{
					seriesInfo.XValueSetFailed = true;
					if (this.DataPointShowsInLegend(seriesInfo.ChartSeries))
					{
						if (this.CanSetPieDataPointLegendText(seriesInfo.Series, dataPoint))
						{
							dataPoint.LegendText = this.GetFormattedValue(obj, "");
						}
					}
					else if (this.CanSetDataPointAxisLabel(seriesInfo.Series, dataPoint))
					{
						ChartArea chartArea = null;
						try
						{
							chartArea = this.m_coreChart.ChartAreas[seriesInfo.Series.ChartArea];
						}
						catch (Exception ex)
						{
							if (AsynchronousExceptionDetection.IsStoppingException(ex))
							{
								throw;
							}
							Global.Tracer.Trace(TraceLevel.Verbose, ex.Message);
						}
						string text;
						if (chartArea != null && seriesInfo.ChartCategoryAxis != null && seriesInfo.ChartCategoryAxis.Style != null && MappingHelper.IsStylePropertyDefined(seriesInfo.ChartCategoryAxis.Style.Format))
						{
							text = MappingHelper.GetStyleFormat(seriesInfo.ChartCategoryAxis.Style, seriesInfo.ChartCategoryAxis.Instance.Style);
						}
						else
						{
							text = "";
						}
						dataPoint.AxisLabel = this.GetFormattedValue(obj, text);
					}
				}
			}
			if (chartValueTypes != null)
			{
				seriesInfo.Series.XValueType = chartValueTypes.Value;
			}
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x00023920 File Offset: 0x00021B20
		private void RenderDerivedSeriesCollecion()
		{
			if (this.m_chart.ChartData.DerivedSeriesCollection == null)
			{
				return;
			}
			foreach (ChartDerivedSeries chartDerivedSeries in this.m_chart.ChartData.DerivedSeriesCollection)
			{
				this.RenderDerivedSeries(chartDerivedSeries);
			}
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x0002398C File Offset: 0x00021B8C
		private void RenderDerivedSeries(ChartDerivedSeries chartDerivedSeries)
		{
			ChartSeriesFormula derivedSeriesFormula = chartDerivedSeries.DerivedSeriesFormula;
			string sourceChartSeriesName = chartDerivedSeries.SourceChartSeriesName;
			if (sourceChartSeriesName == null || sourceChartSeriesName == "")
			{
				return;
			}
			this.GetSeriesInfo(sourceChartSeriesName);
			string text = "";
			if (chartDerivedSeries.Series != null)
			{
				text = chartDerivedSeries.Series.Name;
			}
			if (text == "")
			{
				text = ChartMapper.FormulaHelper.GetDerivedSeriesName(sourceChartSeriesName);
			}
			string text2;
			string text3;
			string text4;
			bool flag;
			ChartMapper.FormulaHelper.RenderFormulaParameters(chartDerivedSeries.FormulaParameters, derivedSeriesFormula, sourceChartSeriesName, text, out text2, out text3, out text4, out flag);
			this.RenderDerivedSeriesProperties(chartDerivedSeries, text, sourceChartSeriesName, derivedSeriesFormula);
			this.ApplyFormula(derivedSeriesFormula, text2, text3, text4, flag);
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x00023A1C File Offset: 0x00021C1C
		private void RenderDerivedSeriesProperties(ChartDerivedSeries chartDerivedSeries, string derivedSeriesName, string sourceSeriesName, ChartSeriesFormula formula)
		{
			if (chartDerivedSeries.Series != null)
			{
				Series series = new Series();
				series.Name = derivedSeriesName;
				ChartMapper.SeriesInfo seriesInfo = this.GetSeriesInfo(sourceSeriesName);
				if (seriesInfo.DerivedSeries == null)
				{
					seriesInfo.DerivedSeries = new List<Series>();
				}
				seriesInfo.DerivedSeries.Add(series);
				this.RenderSeries(null, chartDerivedSeries.Series, series, this.IsSeriesLine(chartDerivedSeries.Series));
				if (ChartMapper.FormulaHelper.ShouldSendDerivedSeriesBack(series.ChartType))
				{
					this.m_coreChart.Series.Insert(0, series);
				}
				else
				{
					this.m_coreChart.Series.Add(series);
				}
				series.ChartArea = this.GetSeriesChartAreaName(chartDerivedSeries.Series);
				if (series.ChartArea == "#NewChartArea")
				{
					Series series2 = seriesInfo.Series;
					if (series2 != null)
					{
						ChartArea chartArea = this.GetChartArea(series2.ChartArea);
						if (chartArea != null)
						{
							ChartArea chartArea2 = this.CreateNewChartArea(chartArea, !ChartMapper.FormulaHelper.IsNewAreaRequired(formula));
							chartArea2.AlignWithChartArea = chartArea.Name;
							chartArea2.AlignType = 15;
							series.ChartArea = chartArea2.Name;
							return;
						}
						series.ChartArea = series2.ChartArea;
					}
				}
			}
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x00023B38 File Offset: 0x00021D38
		private void AdjustSeriesInLegend(object sender, CustomizeLegendEventArgs e)
		{
			foreach (ChartMapper.ChartAreaInfo chartAreaInfo in this.m_chartAreaInfoDictionary.Values)
			{
				foreach (ChartMapper.SeriesInfo seriesInfo in chartAreaInfo.SeriesInfoList)
				{
					LegendItem seriesLegendItem = this.GetSeriesLegendItem(seriesInfo.Series, e.LegendItems);
					if (seriesLegendItem != null)
					{
						if (!this.DataPointShowsInLegend(seriesInfo.ChartSeries))
						{
							this.AdjustSeriesAppearanceInLegend(seriesInfo, seriesLegendItem);
						}
						if (seriesInfo.DerivedSeries != null)
						{
							this.AdjustDerivedSeriesInLegend(seriesInfo, seriesLegendItem, e.LegendItems);
						}
					}
				}
			}
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x00023C04 File Offset: 0x00021E04
		private void AdjustSeriesAppearanceInLegend(ChartMapper.SeriesInfo seriesInfo, LegendItem seriesLegendItem)
		{
			DataPoint dataPoint = this.GetFirstNonEmptyDataPoint(seriesInfo.Series);
			if (dataPoint == null)
			{
				if (seriesInfo.DefaultDataPointAppearance == null)
				{
					return;
				}
				dataPoint = seriesInfo.DefaultDataPointAppearance;
			}
			if (dataPoint.Color != Color.Empty)
			{
				seriesLegendItem.Color = dataPoint.Color;
			}
			if (dataPoint.BackGradientEndColor != Color.Empty)
			{
				seriesLegendItem.BackGradientEndColor = dataPoint.BackGradientEndColor;
			}
			if (dataPoint.BackGradientType != null)
			{
				seriesLegendItem.BackGradientType = dataPoint.BackGradientType;
			}
			if (dataPoint.BackHatchStyle != null)
			{
				seriesLegendItem.BackHatchStyle = dataPoint.BackHatchStyle;
			}
			if (dataPoint.BorderColor != Color.Empty)
			{
				seriesLegendItem.BorderColor = dataPoint.BorderColor;
			}
			if (dataPoint.BorderStyle != ChartMapper.m_defaultCoreDataPointBorderStyle)
			{
				seriesLegendItem.BorderStyle = dataPoint.BorderStyle;
			}
			if (dataPoint.BorderWidth != ChartMapper.m_defaultCoreDataPointBorderWidth)
			{
				seriesLegendItem.BorderWidth = dataPoint.BorderWidth;
			}
			if (dataPoint.BackImage != "")
			{
				seriesLegendItem.Image = dataPoint.BackImage;
			}
			if (dataPoint.BackImageTransparentColor != Color.Empty)
			{
				seriesLegendItem.BackImageTransparentColor = dataPoint.BackImageTransparentColor;
			}
			if (dataPoint.MarkerColor != Color.Empty)
			{
				seriesLegendItem.MarkerColor = dataPoint.MarkerColor;
			}
			if (dataPoint.MarkerBorderColor != Color.Empty)
			{
				seriesLegendItem.MarkerBorderColor = dataPoint.MarkerBorderColor;
			}
			if (dataPoint.MarkerBorderWidth != ChartMapper.m_defaultCoreDataPointBorderWidth)
			{
				seriesLegendItem.MarkerBorderWidth = dataPoint.MarkerBorderWidth;
			}
			seriesLegendItem.MarkerSize = dataPoint.MarkerSize;
			if (dataPoint.MarkerStyle != null)
			{
				seriesLegendItem.MarkerStyle = dataPoint.MarkerStyle;
			}
			if (dataPoint.MarkerImage != "")
			{
				seriesLegendItem.MarkerImage = dataPoint.MarkerImage;
			}
			if (dataPoint.MarkerImageTransparentColor != Color.Empty)
			{
				seriesLegendItem.MarkerImageTransparentColor = dataPoint.MarkerImageTransparentColor;
			}
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x00023DD4 File Offset: 0x00021FD4
		private DataPoint GetFirstNonEmptyDataPoint(Series series)
		{
			foreach (object obj in series.Points)
			{
				DataPoint dataPoint = (DataPoint)obj;
				if (!dataPoint.Empty)
				{
					return dataPoint;
				}
			}
			return null;
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x00023E38 File Offset: 0x00022038
		private void AdjustDerivedSeriesInLegend(ChartMapper.SeriesInfo seriesInfo, LegendItem seriesLegendItem, LegendItemsCollection legendItems)
		{
			List<LegendItem> list = new List<LegendItem>();
			foreach (Series series in seriesInfo.DerivedSeries)
			{
				LegendItem seriesLegendItem2 = this.GetSeriesLegendItem(series, legendItems);
				if (seriesLegendItem2 != null)
				{
					list.Add(seriesLegendItem2);
					legendItems.Remove(seriesLegendItem2);
				}
			}
			int num = legendItems.IndexOf(seriesLegendItem);
			for (int i = 0; i < list.Count; i++)
			{
				num++;
				legendItems.Insert(num, list[i]);
			}
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x00023ED8 File Offset: 0x000220D8
		private LegendItem GetSeriesLegendItem(Series series, LegendItemsCollection legendItemCollection)
		{
			foreach (object obj in legendItemCollection)
			{
				LegendItem legendItem = (LegendItem)obj;
				if (series.Name == legendItem.SeriesName)
				{
					return legendItem;
				}
			}
			return null;
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x00023F40 File Offset: 0x00022140
		private Series GetSeries(string seriesName)
		{
			Series series;
			try
			{
				series = this.m_coreChart.Series[seriesName];
			}
			catch (Exception ex)
			{
				if (AsynchronousExceptionDetection.IsStoppingException(ex))
				{
					throw;
				}
				Global.Tracer.Trace(TraceLevel.Verbose, ex.Message);
				series = null;
			}
			return series;
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x00023F94 File Offset: 0x00022194
		private ChartMapper.SeriesInfo GetSeriesInfo(string seriesName)
		{
			foreach (ChartMapper.ChartAreaInfo chartAreaInfo in this.m_chartAreaInfoDictionary.Values)
			{
				foreach (ChartMapper.SeriesInfo seriesInfo in chartAreaInfo.SeriesInfoList)
				{
					if (seriesInfo.ChartSeries.Name == seriesName)
					{
						return seriesInfo;
					}
				}
			}
			return null;
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x00024038 File Offset: 0x00022238
		private ChartArea GetChartArea(string chartAreaName)
		{
			ChartArea chartArea;
			try
			{
				chartArea = this.m_coreChart.ChartAreas[chartAreaName];
			}
			catch
			{
				chartArea = null;
			}
			return chartArea;
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x00024070 File Offset: 0x00022270
		private ChartArea CreateNewChartArea(ChartArea originalChartArea, bool copyYAxisProperties)
		{
			ChartArea chartArea = new ChartArea();
			this.m_coreChart.ChartAreas.Add(chartArea);
			if (originalChartArea != null)
			{
				chartArea.BackColor = originalChartArea.BackColor;
				chartArea.BackGradientEndColor = originalChartArea.BackGradientEndColor;
				chartArea.BackGradientType = originalChartArea.BackGradientType;
				chartArea.BackHatchStyle = originalChartArea.BackHatchStyle;
				chartArea.BorderColor = originalChartArea.BorderColor;
				chartArea.BorderStyle = originalChartArea.BorderStyle;
				chartArea.BorderWidth = originalChartArea.BorderWidth;
				chartArea.ShadowColor = originalChartArea.ShadowColor;
				chartArea.ShadowOffset = originalChartArea.ShadowOffset;
				for (int i = 0; i < originalChartArea.Axes.Length; i++)
				{
					Axis axis = originalChartArea.Axes[i];
					Axis axis2 = chartArea.Axes[i];
					Grid majorGrid = axis2.MajorGrid;
					Grid majorGrid2 = axis.MajorGrid;
					Grid minorGrid = axis2.MinorGrid;
					Grid minorGrid2 = axis.MinorGrid;
					TickMark majorTickMark = axis2.MajorTickMark;
					TickMark majorTickMark2 = axis.MajorTickMark;
					TickMark minorTickMark = axis2.MinorTickMark;
					TickMark minorTickMark2 = axis.MinorTickMark;
					majorGrid.LineColor = majorGrid2.LineColor;
					minorGrid.LineColor = minorGrid2.LineColor;
					majorTickMark.LineColor = majorTickMark2.LineColor;
					minorTickMark.LineColor = minorTickMark2.LineColor;
					majorGrid.LineStyle = majorGrid2.LineStyle;
					minorGrid.LineStyle = minorGrid2.LineStyle;
					majorTickMark.LineStyle = majorTickMark2.LineStyle;
					minorTickMark.LineStyle = minorTickMark2.LineStyle;
					majorGrid.LineWidth = majorGrid2.LineWidth;
					minorGrid.LineWidth = minorGrid2.LineWidth;
					majorTickMark.LineWidth = majorTickMark2.LineWidth;
					minorTickMark.LineWidth = minorTickMark2.LineWidth;
					majorGrid.Enabled = majorGrid2.Enabled;
					minorGrid.Enabled = minorGrid2.Enabled;
					majorTickMark.Enabled = majorTickMark2.Enabled;
					minorTickMark.Enabled = minorTickMark2.Enabled;
					axis2.StartFromZero = axis.StartFromZero;
					axis2.Margin = axis.Margin;
					axis2.Enabled = axis.Enabled;
				}
				this.CopyAxisRootProperties(originalChartArea.AxisX, chartArea.AxisX);
				if (copyYAxisProperties)
				{
					this.CopyAxisRootProperties(originalChartArea.AxisY, chartArea.AxisY);
				}
			}
			return chartArea;
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x00024298 File Offset: 0x00022498
		private void CopyAxisRootProperties(Axis source, Axis target)
		{
			Label labelStyle = target.LabelStyle;
			Label labelStyle2 = source.LabelStyle;
			labelStyle.Font = labelStyle2.Font;
			labelStyle.FontAngle = labelStyle2.FontAngle;
			labelStyle.FontColor = labelStyle2.FontColor;
			labelStyle.Format = labelStyle2.Format;
			labelStyle.Interval = labelStyle2.Interval;
			labelStyle.IntervalOffset = labelStyle2.IntervalOffset;
			labelStyle.IntervalOffsetType = labelStyle2.IntervalOffsetType;
			labelStyle.IntervalType = labelStyle2.IntervalType;
			labelStyle.OffsetLabels = labelStyle2.OffsetLabels;
			labelStyle.ShowEndLabels = labelStyle2.ShowEndLabels;
			labelStyle.TruncatedLabels = labelStyle2.TruncatedLabels;
			Grid majorGrid = source.MajorGrid;
			Grid majorGrid2 = target.MajorGrid;
			if (majorGrid.Enabled)
			{
				majorGrid2.Interval = majorGrid.Interval;
				majorGrid2.IntervalOffset = majorGrid.IntervalOffset;
				majorGrid2.IntervalOffsetType = majorGrid.IntervalOffsetType;
				majorGrid2.IntervalType = majorGrid.IntervalType;
			}
			else
			{
				majorGrid2.Enabled = majorGrid.Enabled;
			}
			Grid minorGrid = source.MinorGrid;
			Grid minorGrid2 = target.MinorGrid;
			if (minorGrid.Enabled)
			{
				minorGrid2.Interval = minorGrid.Interval;
				minorGrid2.IntervalOffset = minorGrid.IntervalOffset;
				minorGrid2.IntervalOffsetType = minorGrid.IntervalOffsetType;
				minorGrid2.IntervalType = minorGrid.IntervalType;
			}
			else
			{
				minorGrid2.Enabled = minorGrid.Enabled;
			}
			TickMark majorTickMark = source.MajorTickMark;
			TickMark majorTickMark2 = target.MajorTickMark;
			if (majorTickMark.Enabled)
			{
				majorTickMark2.Interval = majorTickMark.Interval;
				majorTickMark2.IntervalOffset = majorTickMark.IntervalOffset;
				majorTickMark2.IntervalOffsetType = majorTickMark.IntervalOffsetType;
				majorTickMark2.IntervalType = majorTickMark.IntervalType;
				majorTickMark2.Size = majorTickMark.Size;
				majorTickMark2.Style = majorTickMark.Style;
			}
			else
			{
				majorTickMark2.Enabled = majorTickMark.Enabled;
			}
			TickMark minorTickMark = source.MinorTickMark;
			TickMark minorTickMark2 = target.MinorTickMark;
			if (minorTickMark.Enabled)
			{
				minorTickMark2.Interval = minorTickMark.Interval;
				minorTickMark2.IntervalOffset = minorTickMark.IntervalOffset;
				minorTickMark2.IntervalOffsetType = minorTickMark.IntervalOffsetType;
				minorTickMark2.IntervalType = minorTickMark.IntervalType;
				minorTickMark2.Size = minorTickMark.Size;
				minorTickMark2.Style = minorTickMark.Style;
			}
			else
			{
				minorTickMark2.Enabled = minorTickMark.Enabled;
			}
			target.Arrows = source.Arrows;
			target.Crossing = source.Crossing;
			target.Interlaced = source.Interlaced;
			target.InterlacedColor = source.InterlacedColor;
			target.Interval = source.Interval;
			target.IntervalAutoMode = source.IntervalAutoMode;
			target.IntervalOffset = source.IntervalOffset;
			target.IntervalOffsetType = source.IntervalOffsetType;
			target.IntervalType = source.IntervalType;
			target.LabelsAutoFitMaxFontSize = source.LabelsAutoFitMaxFontSize;
			target.LabelsAutoFitMinFontSize = source.LabelsAutoFitMinFontSize;
			target.LabelsAutoFitStyle = source.LabelsAutoFitStyle;
			target.LabelsAutoFit = source.LabelsAutoFit;
			target.LineColor = source.LineColor;
			target.LineStyle = source.LineStyle;
			target.LineWidth = source.LineWidth;
			target.LogarithmBase = source.LogarithmBase;
			target.Logarithmic = source.Logarithmic;
			target.MarksNextToAxis = source.MarksNextToAxis;
			target.Reverse = source.Reverse;
			target.ScaleBreakStyle = source.ScaleBreakStyle;
			target.ValueType = source.ValueType;
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x000245E8 File Offset: 0x000227E8
		private void ApplyFormula(ChartSeriesFormula formula, string formulaParameters, string inputValues, string outputValues, bool startFromFirst)
		{
			if (formula == ChartSeriesFormula.Mean || formula == ChartSeriesFormula.Median)
			{
				double num;
				if (formula == ChartSeriesFormula.Mean)
				{
					num = this.m_coreChart.DataManipulator.Statistics.Mean(inputValues);
				}
				else
				{
					num = this.m_coreChart.DataManipulator.Statistics.Median(inputValues);
				}
				Series series = this.GetSeries(inputValues);
				Series series2 = this.GetSeries(outputValues);
				if (series == null || series2 == null)
				{
					return;
				}
				using (IEnumerator enumerator = series.Points.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						DataPoint dataPoint = (DataPoint)obj;
						DataPoint dataPoint2 = new DataPoint();
						dataPoint2.XValue = dataPoint.XValue;
						dataPoint2.YValues = new double[dataPoint.YValues.Length];
						dataPoint.YValues.CopyTo(dataPoint2.YValues, 0);
						dataPoint2.AxisLabel = dataPoint.AxisLabel;
						if (dataPoint2.YValues.Length != 0)
						{
							dataPoint2.YValues[0] = num;
							series2.Points.Add(dataPoint2);
						}
					}
					return;
				}
			}
			this.m_coreChart.DataManipulator.StartFromFirst = startFromFirst;
			this.m_coreChart.DataManipulator.FormulaFinancial(this.GetFinancialFormula(formula), formulaParameters, inputValues, outputValues);
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x00024748 File Offset: 0x00022948
		private FinancialFormula GetFinancialFormula(ChartSeriesFormula formula)
		{
			switch (formula)
			{
			case ChartSeriesFormula.BollingerBands:
				return 2;
			case ChartSeriesFormula.MovingAverage:
				return 21;
			case ChartSeriesFormula.ExponentialMovingAverage:
				return 8;
			case ChartSeriesFormula.TriangularMovingAverage:
				return 24;
			case ChartSeriesFormula.MACD:
				return 10;
			case ChartSeriesFormula.DetrendedPriceOscillator:
				return 5;
			case ChartSeriesFormula.Envelopes:
				return 7;
			case ChartSeriesFormula.Performance:
				return 16;
			case ChartSeriesFormula.RateOfChange:
				return 19;
			case ChartSeriesFormula.RelativeStrengthIndex:
				return 20;
			case ChartSeriesFormula.StandardDeviation:
				return 22;
			case ChartSeriesFormula.TRIX:
				return 25;
			}
			return 30;
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x000247B4 File Offset: 0x000229B4
		private void PostProcessData()
		{
			foreach (KeyValuePair<string, ChartMapper.ChartAreaInfo> keyValuePair in this.m_chartAreaInfoDictionary)
			{
				this.AdjustChartAreaData(keyValuePair);
				this.AdjustAxesMargin(keyValuePair);
			}
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x00024810 File Offset: 0x00022A10
		private void AdjustAxesMargin(KeyValuePair<string, ChartMapper.ChartAreaInfo> chartAreaInfoKeyPair)
		{
			ChartArea chartArea = this.GetChartArea(chartAreaInfoKeyPair.Key);
			ChartMapper.ChartAreaInfo value = chartAreaInfoKeyPair.Value;
			if (chartArea == null)
			{
				return;
			}
			List<Axis> categoryAxesAutoMargin = value.CategoryAxesAutoMargin;
			if (categoryAxesAutoMargin == null)
			{
				return;
			}
			foreach (Axis axis in categoryAxesAutoMargin)
			{
				if (axis.Enabled == 1)
				{
					if (axis == chartArea.AxisX2)
					{
						axis.Margin = chartArea.AxisX.Margin;
					}
					else if (axis == chartArea.AxisX)
					{
						axis.Margin = chartArea.AxisX2.Margin;
					}
				}
			}
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x000248C4 File Offset: 0x00022AC4
		private void AdjustChartAreaData(KeyValuePair<string, ChartMapper.ChartAreaInfo> chartAreaInfo)
		{
			ChartMapper.ChartAreaInfo value = chartAreaInfo.Value;
			bool flag = this.IsXValueSet(value);
			bool flag2 = this.IsXValueSetFailed(value);
			if (flag && flag2)
			{
				using (List<ChartMapper.SeriesInfo>.Enumerator enumerator = value.SeriesInfoList.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ChartMapper.SeriesInfo seriesInfo = enumerator.Current;
						this.ClearSeriesXValues(seriesInfo.Series);
					}
					goto IL_009E;
				}
			}
			if (flag && !this.HasStackedSeries(value))
			{
				foreach (ChartMapper.SeriesInfo seriesInfo2 in value.SeriesInfoList)
				{
					this.ClearNullXValues(seriesInfo2);
				}
			}
			IL_009E:
			if ((flag && flag2) || !flag)
			{
				try
				{
					ChartArea chartArea = this.m_coreChart.ChartAreas[chartAreaInfo.Key];
					chartArea.AxisX.Logarithmic = false;
					chartArea.AxisX2.Logarithmic = false;
				}
				catch (Exception ex)
				{
					if (AsynchronousExceptionDetection.IsStoppingException(ex))
					{
						throw;
					}
					Global.Tracer.Trace(TraceLevel.Verbose, ex.Message);
				}
			}
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x000249F4 File Offset: 0x00022BF4
		private void AddSeriesToDictionary(ChartMapper.SeriesInfo seriesInfo)
		{
			string chartArea = seriesInfo.Series.ChartArea;
			if (!this.m_chartAreaInfoDictionary.ContainsKey(chartArea))
			{
				this.m_chartAreaInfoDictionary.Add(chartArea, new ChartMapper.ChartAreaInfo());
			}
			seriesInfo.ChartAreaInfo = this.m_chartAreaInfoDictionary[chartArea];
			if (this.m_chart.ChartAreas != null)
			{
				ChartArea byName = this.m_chart.ChartAreas.GetByName(chartArea);
				if (byName != null && byName.CategoryAxes != null)
				{
					seriesInfo.ChartCategoryAxis = byName.CategoryAxes.GetByName(this.GetSeriesCategoryAxisName(seriesInfo.ChartSeries));
				}
			}
			this.m_chartAreaInfoDictionary[chartArea].SeriesInfoList.Add(seriesInfo);
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x00024A9C File Offset: 0x00022C9C
		private void ClearNullXValues(ChartMapper.SeriesInfo seriesInfo)
		{
			foreach (DataPoint dataPoint in seriesInfo.NullXValuePoints)
			{
				seriesInfo.Series.Points.Remove(dataPoint);
			}
			seriesInfo.NullXValuePoints.Clear();
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x00024B04 File Offset: 0x00022D04
		private void ClearSeriesXValues(Series series)
		{
			foreach (object obj in series.Points)
			{
				DataPoint dataPoint = (DataPoint)obj;
				dataPoint.AxisLabel = "";
				dataPoint.XValue = 0.0;
			}
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x00024B70 File Offset: 0x00022D70
		public override void Dispose()
		{
			if (this.m_coreChart != null)
			{
				this.m_coreChart.Dispose();
			}
			this.m_coreChart = null;
			base.Dispose();
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x00024B92 File Offset: 0x00022D92
		private void OnPostInitialize()
		{
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x00024B94 File Offset: 0x00022D94
		private void OnPostApplySeriesPointData(Series series, int index)
		{
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x00024B96 File Offset: 0x00022D96
		private void OnPostApplySeriesData(Series series)
		{
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x00024B98 File Offset: 0x00022D98
		private void OnPostApplyData()
		{
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x00024B9C File Offset: 0x00022D9C
		private double ConvertToDouble(object value)
		{
			ChartValueTypes? chartValueTypes = null;
			return this.ConvertToDouble(value, false, ref chartValueTypes);
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x00024BBC File Offset: 0x00022DBC
		private double ConvertToDouble(object value, bool checkForMaxMinValue)
		{
			ChartValueTypes? chartValueTypes = null;
			return this.ConvertToDouble(value, checkForMaxMinValue, ref chartValueTypes);
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x00024BDB File Offset: 0x00022DDB
		private double ConvertToDouble(object value, ref ChartValueTypes? dateTimeType)
		{
			return this.ConvertToDouble(value, false, ref dateTimeType);
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x00024BE8 File Offset: 0x00022DE8
		private double ConvertToDouble(object value, bool checkForMaxMinValue, ref ChartValueTypes? dateTimeType)
		{
			if (value == null)
			{
				return double.NaN;
			}
			switch (Type.GetTypeCode(value.GetType()))
			{
			case TypeCode.Char:
				return (double)((char)value);
			case TypeCode.SByte:
				return (double)((sbyte)value);
			case TypeCode.Byte:
				return (double)((byte)value);
			case TypeCode.Int16:
				return (double)((short)value);
			case TypeCode.UInt16:
				return (double)((ushort)value);
			case TypeCode.Int32:
				return (double)((int)value);
			case TypeCode.UInt32:
				return (uint)value;
			case TypeCode.Int64:
				return (double)((long)value);
			case TypeCode.UInt64:
				return (ulong)value;
			case TypeCode.Single:
				return (double)((float)value);
			case TypeCode.Double:
				return (double)value;
			case TypeCode.Decimal:
				return decimal.ToDouble((decimal)value);
			case TypeCode.DateTime:
				dateTimeType = new ChartValueTypes?(8);
				return ChartMapper.ConvertDateTimeToDouble((DateTime)value);
			case TypeCode.String:
			{
				string text = value.ToString().Trim();
				double num;
				if (double.TryParse(text, out num))
				{
					return num;
				}
				if (checkForMaxMinValue)
				{
					if (text == "MaxValue")
					{
						return double.MaxValue;
					}
					if (text == "MinValue")
					{
						return double.MinValue;
					}
				}
				DateTimeOffset dateTimeOffset;
				bool flag;
				if (Microsoft.ReportingServices.Common.DateTimeUtil.TryParseDateTime(text, null, out dateTimeOffset, out flag))
				{
					if (flag)
					{
						return this.ConvertToDouble(dateTimeOffset, checkForMaxMinValue, ref dateTimeType);
					}
					return this.ConvertToDouble(dateTimeOffset.DateTime, checkForMaxMinValue, ref dateTimeType);
				}
				break;
			}
			}
			if (value is DateTimeOffset)
			{
				dateTimeType = new ChartValueTypes?(11);
				return ChartMapper.ConvertDateTimeToDouble(((DateTimeOffset)value).UtcDateTime);
			}
			if (value is TimeSpan)
			{
				dateTimeType = new ChartValueTypes?(10);
				return ChartMapper.ConvertDateTimeToDouble(DateTime.MinValue + (TimeSpan)value);
			}
			return double.NaN;
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x00024DB0 File Offset: 0x00022FB0
		private static double ConvertDateTimeToDouble(DateTime dateTime)
		{
			return dateTime.ToOADate();
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x00024DBC File Offset: 0x00022FBC
		private string GetFormattedValue(object value, string format)
		{
			if (this.m_formatter == null)
			{
				this.m_formatter = new Formatter(this.m_chart.ChartDef.StyleClass, this.m_chart.RenderingContext.OdpContext, ObjectType.Chart, this.m_chart.Name);
			}
			TypeCode typeCode = Type.GetTypeCode(value.GetType());
			if (typeCode == TypeCode.Object && value is DateTimeOffset)
			{
				typeCode = TypeCode.DateTime;
			}
			return this.m_formatter.FormatValue(value, format, typeCode);
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x00024E32 File Offset: 0x00023032
		private BreakLineType GetScaleBreakLineType(ChartBreakLineType chartBreakLineType)
		{
			switch (chartBreakLineType)
			{
			case ChartBreakLineType.None:
				return 0;
			case ChartBreakLineType.Straight:
				return 1;
			case ChartBreakLineType.Wave:
				return 2;
			default:
				return 3;
			}
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x00024E51 File Offset: 0x00023051
		private AutoBool GetAutoBool(ChartAutoBool autoBool)
		{
			if (autoBool == ChartAutoBool.True)
			{
				return 1;
			}
			if (autoBool != ChartAutoBool.False)
			{
				return 0;
			}
			return 2;
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00024E62 File Offset: 0x00023062
		private AxisEnabled GetAxisEnabled(ChartAutoBool autoBool)
		{
			if (autoBool == ChartAutoBool.True)
			{
				return 1;
			}
			if (autoBool != ChartAutoBool.False)
			{
				return 0;
			}
			return 2;
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00024E73 File Offset: 0x00023073
		private bool GetMargin(ChartAutoBool autoBool)
		{
			return autoBool != ChartAutoBool.False;
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x00024E7C File Offset: 0x0002307C
		private bool DoesSeriesRequireMargin(ChartSeries chartSeries)
		{
			ChartSeriesType seriesType = this.GetSeriesType(chartSeries);
			ChartSeriesSubtype seriesSubType = this.GetSeriesSubType(chartSeries);
			return seriesType != ChartSeriesType.Area && (seriesType != ChartSeriesType.Range || (seriesSubType != ChartSeriesSubtype.Plain && seriesSubType != ChartSeriesSubtype.Smooth));
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x00024EAB File Offset: 0x000230AB
		private StringAlignment GetStringAlignmentFromTextAlignments(TextAlignments value)
		{
			if (value == TextAlignments.Center)
			{
				return StringAlignment.Center;
			}
			if (value != TextAlignments.Right)
			{
				return StringAlignment.Near;
			}
			return StringAlignment.Far;
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x00024EBC File Offset: 0x000230BC
		private StringAlignment GetStringAlignmentFromVericalAlignments(VerticalAlignments value)
		{
			if (value == VerticalAlignments.Middle)
			{
				return StringAlignment.Center;
			}
			if (value != VerticalAlignments.Bottom)
			{
				return StringAlignment.Near;
			}
			return StringAlignment.Far;
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x00024ECD File Offset: 0x000230CD
		private string GetSeriesLegendText(ChartMember seriesGrouping)
		{
			return this.GetGroupingLegendText(seriesGrouping);
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x00024ED8 File Offset: 0x000230D8
		private string GetDataPointLegendText(ChartMember categoryGrouping, ChartMember seriesGrouping)
		{
			string text = "";
			if (this.m_multiColumn)
			{
				text = this.GetGroupingLegendText(categoryGrouping);
			}
			if (this.m_multiRow)
			{
				if (text != "")
				{
					text += ChartMapper.m_legendTextSeparator;
				}
				text += this.GetGroupingLegendText(seriesGrouping);
			}
			return text;
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x00024F2C File Offset: 0x0002312C
		private string GetGroupingLegendText(ChartMember grouping)
		{
			ChartMember chartMember = grouping;
			string text = "";
			do
			{
				string text2 = this.GetGroupingLabel(chartMember);
				if (chartMember.Children != null && text2 != "" && text != "")
				{
					text2 += ChartMapper.m_legendTextSeparator;
				}
				text = text.Insert(0, text2);
				chartMember = chartMember.Parent;
			}
			while (chartMember != null);
			return text;
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x00024F8B File Offset: 0x0002318B
		private string GetGroupingLabel(ChartMember grouping)
		{
			if (grouping.Instance.Label == null)
			{
				return "";
			}
			return grouping.Instance.Label;
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x00024FAC File Offset: 0x000231AC
		private string GetFormattedGroupingLabel(ChartMember categoryGrouping, string chartAreaName, ChartAxis categoryAxis)
		{
			object labelObject = categoryGrouping.Instance.LabelObject;
			if (labelObject == null)
			{
				return " ";
			}
			string text;
			if (this.GetChartArea(chartAreaName) != null && categoryAxis != null && categoryAxis.Style != null && MappingHelper.IsStylePropertyDefined(categoryAxis.Style.Format))
			{
				text = MappingHelper.GetStyleFormat(categoryAxis.Style, categoryAxis.Instance.Style);
			}
			else
			{
				text = "";
			}
			return this.GetFormattedValue(labelObject, text);
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0002501C File Offset: 0x0002321C
		private int GetGroupingLevel(ChartMember grouping)
		{
			int num = -1;
			if (grouping.Children != null)
			{
				foreach (ChartMember chartMember in grouping.Children)
				{
					int groupingLevel = this.GetGroupingLevel(chartMember);
					if (num < groupingLevel)
					{
						num = groupingLevel;
					}
				}
			}
			num++;
			return num;
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x00025080 File Offset: 0x00023280
		private bool IsChartEmpty()
		{
			using (IEnumerator enumerator = this.m_coreChart.Series.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((Series)enumerator.Current).Points.Count > 0)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x000250EC File Offset: 0x000232EC
		private bool DataPointShowsInLegend(ChartSeries chartSeries)
		{
			if (this.GetSeriesType(chartSeries) != ChartSeriesType.Shape)
			{
				return false;
			}
			ChartSeriesSubtype seriesSubType = this.GetSeriesSubType(chartSeries);
			return seriesSubType != ChartSeriesSubtype.TreeMap && seriesSubType != ChartSeriesSubtype.Sunburst;
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x0002511A File Offset: 0x0002331A
		private bool IsSeriesCollectedPie(Series series)
		{
			return (series.ChartType == 17 || series.ChartType == 18) && series["CollectedStyle"] != null && Microsoft.ReportingServices.ReportPublishing.Validator.CompareWithInvariantCulture(series["CollectedStyle"], "CollectedPie");
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x00025156 File Offset: 0x00023356
		private bool IsSeriesPareto(Series series)
		{
			return series.ChartType == 10 && series["ShowColumnAs"] != null && ReportProcessing.CompareWithInvariantCulture(series["ShowColumnAs"], "pareto", true) == 0;
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x0002518C File Offset: 0x0002338C
		private bool IsSeriesHistogram(Series series)
		{
			return series.ChartType == 10 && series["ShowColumnAs"] != null && ReportProcessing.CompareWithInvariantCulture(series["ShowColumnAs"], "histogram", true) == 0;
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x000251C4 File Offset: 0x000233C4
		private bool IsGradientPerDataPointSupported(ChartSeries chartSeries)
		{
			ChartSeriesType seriesType = this.GetSeriesType(chartSeries);
			if (seriesType == ChartSeriesType.Area)
			{
				return false;
			}
			if (seriesType == ChartSeriesType.Range)
			{
				ChartSeriesSubtype validSeriesSubType = this.GetValidSeriesSubType(seriesType, this.GetSeriesSubType(chartSeries));
				if (validSeriesSubType == ChartSeriesSubtype.Plain || validSeriesSubType == ChartSeriesSubtype.Smooth)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x000251FC File Offset: 0x000233FC
		private bool IsSeriesStacked(ChartSeries chartSeries)
		{
			ChartSeriesSubtype validSeriesSubType = this.GetValidSeriesSubType(this.GetSeriesType(chartSeries), this.GetSeriesSubType(chartSeries));
			return validSeriesSubType == ChartSeriesSubtype.Stacked || validSeriesSubType == ChartSeriesSubtype.PercentStacked;
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x00025228 File Offset: 0x00023428
		private bool IsSeriesLine(ChartSeries chartSeries)
		{
			return this.GetSeriesType(chartSeries) == ChartSeriesType.Line;
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x00025234 File Offset: 0x00023434
		private bool IsSeriesRange(ChartSeries chartSeries)
		{
			return this.GetSeriesType(chartSeries) == ChartSeriesType.Range;
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x00025240 File Offset: 0x00023440
		private bool IsSeriesBubble(ChartSeries chartSeries)
		{
			return this.GetValidSeriesSubType(this.GetSeriesType(chartSeries), this.GetSeriesSubType(chartSeries)) == ChartSeriesSubtype.Bubble;
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x0002525C File Offset: 0x0002345C
		private bool IsSeriesExploded(ChartSeries chartSeries)
		{
			ChartSeriesSubtype validSeriesSubType = this.GetValidSeriesSubType(this.GetSeriesType(chartSeries), this.GetSeriesSubType(chartSeries));
			return validSeriesSubType == ChartSeriesSubtype.ExplodedDoughnut || validSeriesSubType == ChartSeriesSubtype.ExplodedPie;
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x00025288 File Offset: 0x00023488
		private bool CanSetCategoryGroupingLabels(ChartMapper.ChartAreaInfo seriesInfoList)
		{
			bool flag = this.IsXValueSet(seriesInfoList);
			return (flag && this.IsXValueSetFailed(seriesInfoList)) || !flag;
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x000252AF File Offset: 0x000234AF
		private bool CanSetPieDataPointLegendText(Series series, DataPoint dataPoint)
		{
			if (!dataPoint.Empty)
			{
				return dataPoint.LegendText == string.Empty;
			}
			return series.EmptyPointStyle.LegendText == string.Empty;
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x000252DF File Offset: 0x000234DF
		private bool CanSetDataPointAxisLabel(Series series, DataPoint dataPoint)
		{
			if (!dataPoint.Empty)
			{
				return dataPoint.AxisLabel == string.Empty;
			}
			return series.EmptyPointStyle.AxisLabel == string.Empty;
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x00025310 File Offset: 0x00023510
		private bool IsXValueSet(ChartMapper.ChartAreaInfo seriesInfoList)
		{
			using (List<ChartMapper.SeriesInfo>.Enumerator enumerator = seriesInfoList.SeriesInfoList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.XValueSet)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x0002536C File Offset: 0x0002356C
		private bool IsXValueSetFailed(ChartMapper.ChartAreaInfo seriesInfoList)
		{
			using (List<ChartMapper.SeriesInfo>.Enumerator enumerator = seriesInfoList.SeriesInfoList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.XValueSetFailed)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x000253C8 File Offset: 0x000235C8
		private bool HasStackedSeries(ChartMapper.ChartAreaInfo seriesInfoList)
		{
			foreach (ChartMapper.SeriesInfo seriesInfo in seriesInfoList.SeriesInfoList)
			{
				if (this.IsSeriesStacked(seriesInfo.ChartSeries))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0002542C File Offset: 0x0002362C
		private GradientType GetGradientType(BackgroundGradients chartGradientType)
		{
			GradientType gradientType;
			switch (chartGradientType)
			{
			case BackgroundGradients.LeftRight:
				gradientType = 1;
				break;
			case BackgroundGradients.TopBottom:
				gradientType = 2;
				break;
			case BackgroundGradients.Center:
				gradientType = 3;
				break;
			case BackgroundGradients.DiagonalLeft:
				gradientType = 4;
				break;
			case BackgroundGradients.DiagonalRight:
				gradientType = 5;
				break;
			case BackgroundGradients.HorizontalCenter:
				gradientType = 6;
				break;
			case BackgroundGradients.VerticalCenter:
				gradientType = 7;
				break;
			default:
				gradientType = 0;
				break;
			}
			return gradientType;
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x00025480 File Offset: 0x00023680
		private ChartHatchStyle GetHatchType(BackgroundHatchTypes chartHatchType)
		{
			switch (chartHatchType)
			{
			case BackgroundHatchTypes.BackwardDiagonal:
				return 1;
			case BackgroundHatchTypes.Cross:
				return 2;
			case BackgroundHatchTypes.DarkDownwardDiagonal:
				return 3;
			case BackgroundHatchTypes.DarkHorizontal:
				return 4;
			case BackgroundHatchTypes.DarkUpwardDiagonal:
				return 5;
			case BackgroundHatchTypes.DarkVertical:
				return 6;
			case BackgroundHatchTypes.DashedDownwardDiagonal:
				return 7;
			case BackgroundHatchTypes.DashedHorizontal:
				return 8;
			case BackgroundHatchTypes.DashedUpwardDiagonal:
				return 9;
			case BackgroundHatchTypes.DashedVertical:
				return 10;
			case BackgroundHatchTypes.DiagonalBrick:
				return 11;
			case BackgroundHatchTypes.DiagonalCross:
				return 12;
			case BackgroundHatchTypes.Divot:
				return 13;
			case BackgroundHatchTypes.DottedDiamond:
				return 14;
			case BackgroundHatchTypes.DottedGrid:
				return 15;
			case BackgroundHatchTypes.ForwardDiagonal:
				return 16;
			case BackgroundHatchTypes.Horizontal:
				return 17;
			case BackgroundHatchTypes.HorizontalBrick:
				return 18;
			case BackgroundHatchTypes.LargeCheckerBoard:
				return 19;
			case BackgroundHatchTypes.LargeConfetti:
				return 20;
			case BackgroundHatchTypes.LargeGrid:
				return 21;
			case BackgroundHatchTypes.LightDownwardDiagonal:
				return 22;
			case BackgroundHatchTypes.LightHorizontal:
				return 23;
			case BackgroundHatchTypes.LightUpwardDiagonal:
				return 24;
			case BackgroundHatchTypes.LightVertical:
				return 25;
			case BackgroundHatchTypes.NarrowHorizontal:
				return 26;
			case BackgroundHatchTypes.OutlinedDiamond:
				return 28;
			case BackgroundHatchTypes.Percent05:
				return 29;
			case BackgroundHatchTypes.Percent10:
				return 30;
			case BackgroundHatchTypes.Percent20:
				return 31;
			case BackgroundHatchTypes.Percent25:
				return 32;
			case BackgroundHatchTypes.Percent30:
				return 33;
			case BackgroundHatchTypes.Percent40:
				return 34;
			case BackgroundHatchTypes.Percent50:
				return 35;
			case BackgroundHatchTypes.Percent60:
				return 36;
			case BackgroundHatchTypes.Percent70:
				return 37;
			case BackgroundHatchTypes.Percent75:
				return 38;
			case BackgroundHatchTypes.Percent80:
				return 39;
			case BackgroundHatchTypes.Percent90:
				return 40;
			case BackgroundHatchTypes.Plaid:
				return 41;
			case BackgroundHatchTypes.Shingle:
				return 42;
			case BackgroundHatchTypes.SmallCheckerBoard:
				return 43;
			case BackgroundHatchTypes.SmallConfetti:
				return 44;
			case BackgroundHatchTypes.SmallGrid:
				return 45;
			case BackgroundHatchTypes.SolidDiamond:
				return 46;
			case BackgroundHatchTypes.Sphere:
				return 47;
			case BackgroundHatchTypes.Trellis:
				return 48;
			case BackgroundHatchTypes.Vertical:
				return 49;
			case BackgroundHatchTypes.Wave:
				return 50;
			case BackgroundHatchTypes.Weave:
				return 51;
			case BackgroundHatchTypes.WideDownwardDiagonal:
				return 52;
			case BackgroundHatchTypes.WideUpwardDiagonal:
				return 53;
			case BackgroundHatchTypes.ZigZag:
				return 54;
			}
			return 0;
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x0002560C File Offset: 0x0002380C
		private ChartDashStyle GetBorderStyle(BorderStyles chartBorderStyle, bool isLine)
		{
			switch (chartBorderStyle)
			{
			case BorderStyles.None:
				return 0;
			case BorderStyles.Dotted:
				return 4;
			case BorderStyles.Dashed:
				return 1;
			case BorderStyles.Solid:
				return 5;
			case BorderStyles.DashDot:
				return 2;
			case BorderStyles.DashDotDot:
				return 3;
			}
			ChartDashStyle chartDashStyle;
			if (isLine)
			{
				chartDashStyle = 5;
			}
			else
			{
				chartDashStyle = 0;
			}
			return chartDashStyle;
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x00025661 File Offset: 0x00023861
		private ChartImageWrapMode GetBackImageMode(BackgroundRepeatTypes backgroundImageRepeatType)
		{
			if (backgroundImageRepeatType == BackgroundRepeatTypes.Repeat)
			{
				return 0;
			}
			if (backgroundImageRepeatType != BackgroundRepeatTypes.Clip)
			{
				return 4;
			}
			return 100;
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x00025674 File Offset: 0x00023874
		private ChartImageAlign GetBackImageAlign(Positions position)
		{
			switch (position)
			{
			case Positions.Top:
				return 1;
			case Positions.TopLeft:
				return 0;
			case Positions.TopRight:
				return 2;
			case Positions.Left:
				return 7;
			case Positions.Center:
				return 8;
			case Positions.Right:
				return 3;
			case Positions.BottomRight:
				return 4;
			case Positions.Bottom:
				return 5;
			case Positions.BottomLeft:
				return 6;
			default:
				return 0;
			}
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x000256C2 File Offset: 0x000238C2
		private LabelOutsidePlotAreaStyle GetLabelOutsidePlotAreaStyle(ChartAllowOutsideChartArea chartAllowOutsideChartArea)
		{
			if (chartAllowOutsideChartArea == ChartAllowOutsideChartArea.True)
			{
				return 0;
			}
			if (chartAllowOutsideChartArea == ChartAllowOutsideChartArea.False)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x000256D4 File Offset: 0x000238D4
		private DateTimeIntervalType GetDateTimeIntervalType(ChartIntervalType chartIntervalType)
		{
			switch (chartIntervalType)
			{
			case ChartIntervalType.Default:
				return 0;
			case ChartIntervalType.Number:
				return 1;
			case ChartIntervalType.Years:
				return 2;
			case ChartIntervalType.Months:
				return 3;
			case ChartIntervalType.Weeks:
				return 4;
			case ChartIntervalType.Days:
				return 5;
			case ChartIntervalType.Hours:
				return 6;
			case ChartIntervalType.Minutes:
				return 7;
			case ChartIntervalType.Seconds:
				return 8;
			case ChartIntervalType.Milliseconds:
				return 9;
			}
			return 0;
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x0002572B File Offset: 0x0002392B
		private IntervalAutoMode GetIntervalAutoMode(bool variableAutoInterval)
		{
			if (!variableAutoInterval)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x00025733 File Offset: 0x00023933
		private TickMarkStyle GetTickMarkStyle(ChartTickMarksType chartTickMarksType)
		{
			switch (chartTickMarksType)
			{
			case ChartTickMarksType.Inside:
				return 2;
			case ChartTickMarksType.Outside:
				return 1;
			case ChartTickMarksType.Cross:
				return 3;
			default:
				return 0;
			}
		}

		// Token: 0x0400020E RID: 526
		private Chart m_chart;

		// Token: 0x0400020F RID: 527
		private ActionInfoWithDynamicImageMapCollection m_actions = new ActionInfoWithDynamicImageMapCollection();

		// Token: 0x04000210 RID: 528
		private Chart m_coreChart;

		// Token: 0x04000211 RID: 529
		private bool m_multiColumn;

		// Token: 0x04000212 RID: 530
		private bool m_multiRow;

		// Token: 0x04000213 RID: 531
		private Formatter m_formatter;

		// Token: 0x04000214 RID: 532
		private Dictionary<string, ChartMapper.ChartAreaInfo> m_chartAreaInfoDictionary = new Dictionary<string, ChartMapper.ChartAreaInfo>();

		// Token: 0x04000215 RID: 533
		private ChartMapper.Hatcher m_hatcher;

		// Token: 0x04000216 RID: 534
		private ChartMapper.AutoMarker m_autoMarker;

		// Token: 0x04000217 RID: 535
		private static string m_legendTextSeparator = " - ";

		// Token: 0x04000218 RID: 536
		private static string m_defaulChartAreaName = "Default";

		// Token: 0x04000219 RID: 537
		private static string m_imagePrefix = "KatmaiChartMapperImage";

		// Token: 0x0400021A RID: 538
		private static string m_pieAutoAxisLabelsName = "AutoAxisLabels";

		// Token: 0x0400021B RID: 539
		private static string m_defaultMarkerSizeString = "3.75pt";

		// Token: 0x0400021C RID: 540
		private static string m_defaultCalloutLineWidthString = "0.75pt";

		// Token: 0x0400021D RID: 541
		private static string m_defaultMaxMovingDistanceString = "23pt";

		// Token: 0x0400021E RID: 542
		private static ReportSize m_defaultMarkerSize = new ReportSize(ChartMapper.m_defaultMarkerSizeString);

		// Token: 0x0400021F RID: 543
		private static ReportSize m_defaultCalloutLineWidth = new ReportSize(ChartMapper.m_defaultCalloutLineWidthString);

		// Token: 0x04000220 RID: 544
		private static ReportSize m_defaultMaxMovingDistance = new ReportSize(ChartMapper.m_defaultMaxMovingDistanceString);

		// Token: 0x04000221 RID: 545
		private static LabelsAutoFitStyles m_defaultLabelsAngleStep = 32;

		// Token: 0x04000222 RID: 546
		private static ChartDashStyle m_defaultCoreDataPointBorderStyle = 5;

		// Token: 0x04000223 RID: 547
		private static int m_defaultCoreDataPointBorderWidth = 1;

		// Token: 0x02000917 RID: 2327
		private class ChartAreaInfo
		{
			// Token: 0x04003F32 RID: 16178
			public List<ChartMapper.SeriesInfo> SeriesInfoList = new List<ChartMapper.SeriesInfo>();

			// Token: 0x04003F33 RID: 16179
			public List<string> CategoryAxesScalar;

			// Token: 0x04003F34 RID: 16180
			public List<Axis> CategoryAxesAutoMargin;

			// Token: 0x04003F35 RID: 16181
			public bool PrimaryAxisSet;

			// Token: 0x04003F36 RID: 16182
			public bool SecondaryAxisSet;
		}

		// Token: 0x02000918 RID: 2328
		private class SeriesInfo
		{
			// Token: 0x04003F37 RID: 16183
			public ChartMapper.ChartAreaInfo ChartAreaInfo;

			// Token: 0x04003F38 RID: 16184
			public ChartAxis ChartCategoryAxis;

			// Token: 0x04003F39 RID: 16185
			public Series Series;

			// Token: 0x04003F3A RID: 16186
			public List<Series> DerivedSeries;

			// Token: 0x04003F3B RID: 16187
			public ChartSeries ChartSeries;

			// Token: 0x04003F3C RID: 16188
			public ChartMember SeriesGrouping;

			// Token: 0x04003F3D RID: 16189
			public bool XValueSet;

			// Token: 0x04003F3E RID: 16190
			public bool XValueSetFailed;

			// Token: 0x04003F3F RID: 16191
			public List<DataPoint> NullXValuePoints = new List<DataPoint>();

			// Token: 0x04003F40 RID: 16192
			public bool IsLine;

			// Token: 0x04003F41 RID: 16193
			public bool IsExploded;

			// Token: 0x04003F42 RID: 16194
			public bool IsRange;

			// Token: 0x04003F43 RID: 16195
			public bool IsBubble;

			// Token: 0x04003F44 RID: 16196
			public bool IsAttachedToScalarAxis;

			// Token: 0x04003F45 RID: 16197
			public bool? IsDataPointColorEmpty;

			// Token: 0x04003F46 RID: 16198
			public bool IsDataPointHatchDefined;

			// Token: 0x04003F47 RID: 16199
			public ChartMapper.DataPointBackgroundImageInfoCollection DataPointBackgroundImageInfoCollection = new ChartMapper.DataPointBackgroundImageInfoCollection();

			// Token: 0x04003F48 RID: 16200
			public DataPoint DefaultDataPointAppearance;

			// Token: 0x04003F49 RID: 16201
			public bool IsGradientPerDataPointSupported = true;

			// Token: 0x04003F4A RID: 16202
			public bool IsGradientSupported = true;

			// Token: 0x04003F4B RID: 16203
			public GradientType? BackGradientType;

			// Token: 0x04003F4C RID: 16204
			public Color? Color;

			// Token: 0x04003F4D RID: 16205
			public Color? BackGradientEndColor;
		}

		// Token: 0x02000919 RID: 2329
		private class DataPointBackgroundImageInfoCollection : List<ChartMapper.DataPointBackgroundImageInfo>
		{
			// Token: 0x06007F1E RID: 32542 RVA: 0x0020C5C4 File Offset: 0x0020A7C4
			public void Initialize(ChartSeries chartSeries)
			{
				base.Clear();
				for (int i = 0; i < chartSeries.Count; i++)
				{
					ChartMapper.DataPointBackgroundImageInfo dataPointBackgroundImageInfo = new ChartMapper.DataPointBackgroundImageInfo();
					dataPointBackgroundImageInfo.Initialize(chartSeries[i]);
					base.Add(dataPointBackgroundImageInfo);
				}
			}
		}

		// Token: 0x0200091A RID: 2330
		private class DataPointBackgroundImageInfo
		{
			// Token: 0x06007F20 RID: 32544 RVA: 0x0020C60A File Offset: 0x0020A80A
			public void Initialize(ChartDataPoint chartDataPoint)
			{
				this.DataPointBackgroundImage.Initialize(chartDataPoint.Style);
				if (chartDataPoint.Marker != null)
				{
					this.MarkerBackgroundImage.Initialize(chartDataPoint.Marker.Style);
				}
			}

			// Token: 0x04003F4E RID: 16206
			public ChartMapper.BackgroundImageInfo DataPointBackgroundImage = new ChartMapper.BackgroundImageInfo();

			// Token: 0x04003F4F RID: 16207
			public ChartMapper.BackgroundImageInfo MarkerBackgroundImage = new ChartMapper.BackgroundImageInfo();
		}

		// Token: 0x0200091B RID: 2331
		private class BackgroundImageInfo
		{
			// Token: 0x06007F22 RID: 32546 RVA: 0x0020C65C File Offset: 0x0020A85C
			public void Initialize(Style style)
			{
				if (style == null || style.BackgroundImage == null || style.BackgroundImage.MIMEType == null || style.BackgroundImage.Value == null)
				{
					return;
				}
				this.CanShareBackgroundImage = !style.BackgroundImage.MIMEType.IsExpression && !style.BackgroundImage.Value.IsExpression;
			}

			// Token: 0x04003F50 RID: 16208
			public bool CanShareBackgroundImage;

			// Token: 0x04003F51 RID: 16209
			public string SharedBackgroundImageName;
		}

		// Token: 0x0200091C RID: 2332
		private class Hatcher
		{
			// Token: 0x17002959 RID: 10585
			// (get) Token: 0x06007F24 RID: 32548 RVA: 0x0020C6C5 File Offset: 0x0020A8C5
			internal ChartHatchStyle Current
			{
				get
				{
					this.m_current = (this.m_current + 1) % ChartMapper.Hatcher.m_hatchArray.Length;
					return ChartMapper.Hatcher.m_hatchArray[this.m_current];
				}
			}

			// Token: 0x06007F26 RID: 32550 RVA: 0x0020C6F8 File Offset: 0x0020A8F8
			// Note: this type is marked as 'beforefieldinit'.
			static Hatcher()
			{
				ChartHatchStyle[] array = new ChartHatchStyle[54];
				RuntimeHelpers.InitializeArray(array, fieldof(global::<PrivateImplementationDetails>.FEB857CFB235C86A748289A0F529A1851AAB70A038AD3495FCC392AAA15E6580).FieldHandle);
				ChartMapper.Hatcher.m_hatchArray = array;
			}

			// Token: 0x04003F52 RID: 16210
			private int m_current = -1;

			// Token: 0x04003F53 RID: 16211
			private static ChartHatchStyle[] m_hatchArray;
		}

		// Token: 0x0200091D RID: 2333
		private class AutoMarker
		{
			// Token: 0x1700295A RID: 10586
			// (get) Token: 0x06007F27 RID: 32551 RVA: 0x0020C711 File Offset: 0x0020A911
			internal MarkerStyle Current
			{
				get
				{
					this.m_currentUsed = true;
					return ChartMapper.AutoMarker.m_markerArray[this.m_current];
				}
			}

			// Token: 0x06007F28 RID: 32552 RVA: 0x0020C726 File Offset: 0x0020A926
			internal void MoveNext()
			{
				if (this.m_currentUsed)
				{
					this.m_currentUsed = false;
					this.m_current = (this.m_current + 1) % ChartMapper.AutoMarker.m_markerArray.Length;
				}
			}

			// Token: 0x06007F2A RID: 32554 RVA: 0x0020C755 File Offset: 0x0020A955
			// Note: this type is marked as 'beforefieldinit'.
			static AutoMarker()
			{
				MarkerStyle[] array = new MarkerStyle[9];
				RuntimeHelpers.InitializeArray(array, fieldof(global::<PrivateImplementationDetails>.E3D25E7590EDD76206831801F67D1EE231D8B90A2BB4BFE31A152BE21D2F536C).FieldHandle);
				ChartMapper.AutoMarker.m_markerArray = array;
			}

			// Token: 0x04003F54 RID: 16212
			private int m_current;

			// Token: 0x04003F55 RID: 16213
			private bool m_currentUsed;

			// Token: 0x04003F56 RID: 16214
			private static MarkerStyle[] m_markerArray;
		}

		// Token: 0x0200091E RID: 2334
		private static class FormulaHelper
		{
			// Token: 0x06007F2B RID: 32555 RVA: 0x0020C770 File Offset: 0x0020A970
			internal static void RenderFormulaParameters(ChartFormulaParameterCollection chartFormulaParameters, ChartSeriesFormula formula, string sourceSeriesName, string derivedSeriesName, out string formulaParameters, out string inputValues, out string outputValues, out bool startFromFirst)
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				ChartMapper.FormulaHelper.GetParameters(chartFormulaParameters, dictionary);
				formulaParameters = ChartMapper.FormulaHelper.ConstructFormulaParameters(dictionary, formula);
				outputValues = ChartMapper.FormulaHelper.GetOutputValues(dictionary, formula, derivedSeriesName);
				inputValues = ChartMapper.FormulaHelper.GetInputValues(dictionary, sourceSeriesName);
				if (ChartMapper.FormulaHelper.GetParameter(dictionary, "StartFromFirst").Equals(bool.TrueString, StringComparison.OrdinalIgnoreCase))
				{
					startFromFirst = true;
					return;
				}
				startFromFirst = false;
			}

			// Token: 0x06007F2C RID: 32556 RVA: 0x0020C7CC File Offset: 0x0020A9CC
			internal static string GetInputValues(Dictionary<string, string> parameters, string sourceSeriesName)
			{
				string text = ChartMapper.FormulaHelper.GetParameter(parameters, "Input");
				if (text == "")
				{
					text = sourceSeriesName;
				}
				return text;
			}

			// Token: 0x06007F2D RID: 32557 RVA: 0x0020C7F8 File Offset: 0x0020A9F8
			internal static string GetOutputValues(Dictionary<string, string> parameters, ChartSeriesFormula formula, string derivedSeriesName)
			{
				string text = ChartMapper.FormulaHelper.GetParameter(parameters, "Output");
				if (text != "")
				{
					text = text.Replace("#OUTPUTSERIES", derivedSeriesName);
				}
				else if (formula == ChartSeriesFormula.BollingerBands || formula - ChartSeriesFormula.DetrendedPriceOscillator <= 1)
				{
					text = derivedSeriesName + ":Y, " + derivedSeriesName + ":Y2";
				}
				else
				{
					text = derivedSeriesName;
				}
				return text;
			}

			// Token: 0x06007F2E RID: 32558 RVA: 0x0020C850 File Offset: 0x0020AA50
			internal static string ConstructFormulaParameters(Dictionary<string, string> parameters, ChartSeriesFormula formula)
			{
				string text = "";
				switch (formula)
				{
				case ChartSeriesFormula.BollingerBands:
					ChartMapper.FormulaHelper.AppendParameter(parameters, "Period", "2", ref text);
					ChartMapper.FormulaHelper.AppendParameter(parameters, "Deviation", "2", ref text);
					break;
				case ChartSeriesFormula.MovingAverage:
					ChartMapper.FormulaHelper.AppendParameter(parameters, "Period", "2", ref text);
					break;
				case ChartSeriesFormula.ExponentialMovingAverage:
					ChartMapper.FormulaHelper.AppendParameter(parameters, "Period", "2", ref text);
					break;
				case ChartSeriesFormula.TriangularMovingAverage:
					ChartMapper.FormulaHelper.AppendParameter(parameters, "Period", "2", ref text);
					break;
				case ChartSeriesFormula.WeightedMovingAverage:
					ChartMapper.FormulaHelper.AppendParameter(parameters, "Period", "2", ref text);
					break;
				case ChartSeriesFormula.MACD:
					ChartMapper.FormulaHelper.AppendParameter(parameters, "ShortPeriod", "12", ref text);
					ChartMapper.FormulaHelper.AppendParameter(parameters, "LongPeriod", "26", ref text);
					break;
				case ChartSeriesFormula.DetrendedPriceOscillator:
					ChartMapper.FormulaHelper.AppendParameter(parameters, "Period", "2", ref text);
					break;
				case ChartSeriesFormula.Envelopes:
					ChartMapper.FormulaHelper.AppendParameter(parameters, "Period", "2", ref text);
					ChartMapper.FormulaHelper.AppendParameter(parameters, "Shift", "7", ref text);
					break;
				case ChartSeriesFormula.RateOfChange:
					ChartMapper.FormulaHelper.AppendParameter(parameters, "Period", "10", ref text);
					break;
				case ChartSeriesFormula.RelativeStrengthIndex:
					ChartMapper.FormulaHelper.AppendParameter(parameters, "Period", "10", ref text);
					break;
				case ChartSeriesFormula.StandardDeviation:
					ChartMapper.FormulaHelper.AppendParameter(parameters, "Period", "2", ref text);
					break;
				case ChartSeriesFormula.TRIX:
					ChartMapper.FormulaHelper.AppendParameter(parameters, "Period", "2", ref text);
					break;
				}
				return text;
			}

			// Token: 0x06007F2F RID: 32559 RVA: 0x0020C9DC File Offset: 0x0020ABDC
			private static void GetParameters(ChartFormulaParameterCollection chartFormulaParameters, Dictionary<string, string> parameters)
			{
				if (chartFormulaParameters != null)
				{
					foreach (ChartFormulaParameter chartFormulaParameter in chartFormulaParameters)
					{
						string text;
						string text2;
						ChartMapper.FormulaHelper.GetParameter(chartFormulaParameter, out text, out text2);
						parameters.Add(text, text2);
					}
				}
			}

			// Token: 0x06007F30 RID: 32560 RVA: 0x0020CA30 File Offset: 0x0020AC30
			private static void GetParameter(ChartFormulaParameter chartFormulaParameter, out string name, out string value)
			{
				name = "";
				value = "";
				if (chartFormulaParameter.Name != null)
				{
					name = chartFormulaParameter.Name;
				}
				if (chartFormulaParameter.Value != null)
				{
					object obj;
					if (!chartFormulaParameter.Value.IsExpression)
					{
						obj = chartFormulaParameter.Value.Value;
					}
					else
					{
						obj = chartFormulaParameter.Instance.Value;
					}
					value = Convert.ToString(obj, CultureInfo.InvariantCulture);
				}
			}

			// Token: 0x06007F31 RID: 32561 RVA: 0x0020CA98 File Offset: 0x0020AC98
			private static void AppendParameter(Dictionary<string, string> parameters, string parameterName, string defaultValue, ref string formulaParameters)
			{
				if (formulaParameters != string.Empty)
				{
					formulaParameters += ",";
				}
				string text = null;
				if (parameters.ContainsKey(parameterName))
				{
					text = parameters[parameterName];
				}
				if (text != null)
				{
					formulaParameters += text;
					return;
				}
				formulaParameters += defaultValue;
			}

			// Token: 0x06007F32 RID: 32562 RVA: 0x0020CAEC File Offset: 0x0020ACEC
			private static string GetParameter(Dictionary<string, string> parameters, string parameterName)
			{
				string text = null;
				if (parameters.ContainsKey(parameterName))
				{
					text = parameters[parameterName];
				}
				if (text == null)
				{
					return "";
				}
				return text;
			}

			// Token: 0x06007F33 RID: 32563 RVA: 0x0020CB16 File Offset: 0x0020AD16
			internal static string GetDerivedSeriesName(string sourceSeriesName)
			{
				return sourceSeriesName + "_Formula";
			}

			// Token: 0x06007F34 RID: 32564 RVA: 0x0020CB23 File Offset: 0x0020AD23
			internal static bool IsNewAreaRequired(ChartSeriesFormula formula)
			{
				return formula == ChartSeriesFormula.MACD || formula - ChartSeriesFormula.Performance <= 4;
			}

			// Token: 0x06007F35 RID: 32565 RVA: 0x0020CB32 File Offset: 0x0020AD32
			internal static bool ShouldSendDerivedSeriesBack(SeriesChartType type)
			{
				return type != 3 && type != 4;
			}

			// Token: 0x04003F57 RID: 16215
			public const string PARAM_NAME_START_FROM_FIRST = "StartFromFirst";

			// Token: 0x04003F58 RID: 16216
			public const string PARAM_NAME_OUTPUT = "Output";

			// Token: 0x04003F59 RID: 16217
			public const string PARAM_NAME_INPUT = "Input";

			// Token: 0x04003F5A RID: 16218
			public const string PARAM_NAME_PERIOD = "Period";

			// Token: 0x04003F5B RID: 16219
			public const string PARAM_DEFAULT_PERIOD = "2";

			// Token: 0x04003F5C RID: 16220
			public const string PARAM_NAME_SHORT_PERIOD = "ShortPeriod";

			// Token: 0x04003F5D RID: 16221
			public const string PARAM_DEFAULT_SHORT_PERIOD = "12";

			// Token: 0x04003F5E RID: 16222
			public const string PARAM_NAME_LONG_PERIOD = "LongPeriod";

			// Token: 0x04003F5F RID: 16223
			public const string PARAM_DEFAULT_LONG_PERIOD = "26";

			// Token: 0x04003F60 RID: 16224
			public const string PARAM_NAME_DEVIATION = "Deviation";

			// Token: 0x04003F61 RID: 16225
			public const string PARAM_DEFAULT_DEVIATION = "2";

			// Token: 0x04003F62 RID: 16226
			public const string PARAM_NAME_SHIFT = "Shift";

			// Token: 0x04003F63 RID: 16227
			public const string PARAM_DEFAULT_SHIFT = "7";

			// Token: 0x04003F64 RID: 16228
			public const string OUTPUT_SERIES_KEYWORD = "#OUTPUTSERIES";

			// Token: 0x04003F65 RID: 16229
			public const string DERIVED_SERIES_NAME_SUFFIX = "_Formula";

			// Token: 0x04003F66 RID: 16230
			public const string NEW_CHART_AREA_NAME = "#NewChartArea";
		}

		// Token: 0x0200091F RID: 2335
		private class TraceContext : ITraceContext
		{
			// Token: 0x06007F36 RID: 32566 RVA: 0x0020CB44 File Offset: 0x0020AD44
			public TraceContext()
			{
				this.m_startTime = (this.m_lastOperation = DateTime.Now);
			}

			// Token: 0x1700295B RID: 10587
			// (get) Token: 0x06007F37 RID: 32567 RVA: 0x0020CB6B File Offset: 0x0020AD6B
			public bool TraceEnabled
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06007F38 RID: 32568 RVA: 0x0020CB70 File Offset: 0x0020AD70
			public void Write(string category, string message)
			{
				RSTrace.ProcessingTracer.Trace(string.Concat(new string[]
				{
					category,
					"; ",
					message,
					"; ",
					(DateTime.Now - this.m_startTime).TotalMilliseconds.ToString(),
					"; ",
					(DateTime.Now - this.m_lastOperation).TotalMilliseconds.ToString()
				}));
				this.m_lastOperation = DateTime.Now;
			}

			// Token: 0x04003F67 RID: 16231
			private DateTime m_startTime;

			// Token: 0x04003F68 RID: 16232
			private DateTime m_lastOperation;
		}
	}
}
