using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Web.UI.WebControls;
using Dundas.Charting.WebControl;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000054 RID: 84
	internal sealed class DundasChart
	{
		// Token: 0x0600061A RID: 1562 RVA: 0x000151F9 File Offset: 0x000133F9
		internal DundasChart(Chart owner)
		{
			this.m_owner = owner;
			this.m_chart = (Chart)this.m_owner.ReportItemDef;
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x0600061B RID: 1563 RVA: 0x00015230 File Offset: 0x00013430
		internal ChartInstanceInfo ChartInstanceInfo
		{
			get
			{
				if (this.m_owner.ReportItemInstance == null)
				{
					return null;
				}
				if (this.m_chartInstanceInfo == null)
				{
					this.m_chartInstanceInfo = (ChartInstanceInfo)this.m_owner.ReportItemInstance.GetInstanceInfo(this.m_owner.RenderingContext.ChunkManager);
				}
				return this.m_chartInstanceInfo;
			}
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x00015285 File Offset: 0x00013485
		internal MemoryStream GetImage(Chart.ImageType type, ChartInstanceInfo instanceInfo, out bool hasImageMap)
		{
			this.m_chartInstanceInfo = instanceInfo;
			this.m_compareInfo = Thread.CurrentThread.CurrentCulture.CompareInfo;
			return this.GetImage(type, out hasImageMap);
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x000152AC File Offset: 0x000134AC
		private MemoryStream GetImage(Chart.ImageType type, out bool hasImageMap)
		{
			hasImageMap = false;
			if (this.m_dundasChart == null)
			{
				this.m_dundasChart = new Chart();
				this.m_dundasChart.RenderType = 2;
				this.m_dundasChart.SoftShadows = true;
				this.m_dundasChart.MapEnabled = true;
				this.m_dundasChart.Height = Unit.Pixel((int)Math.Round(this.m_chart.HeightValue * 96.0 * 0.03937007874));
				this.m_dundasChart.Width = Unit.Pixel((int)Math.Round(this.m_chart.WidthValue * 96.0 * 0.03937007874));
				this.m_dundasChart.TextAntiAliasingQuality = 2;
				this.m_dundasChart.SuppressExceptions = true;
				this.m_currentCulture = Thread.CurrentThread.CurrentCulture;
				if (this.m_chartInstanceInfo != null)
				{
					string text = (string)Microsoft.ReportingServices.ReportRendering.Style.GetStyleValue("NumeralLanguage", this.m_chart.StyleClass, this.m_chartInstanceInfo.StyleAttributeValues);
					if (text != null)
					{
						this.m_numeralCulture = new CultureInfo(text, false);
					}
				}
			}
			this.m_dundasChart.ImageType = this.DundasImageType(type);
			if (this.ChartInstanceInfo != null)
			{
				this.RenderChart();
			}
			MemoryStream memoryStream = new MemoryStream();
			if (this.m_owner.ScaleX > 0f && this.m_owner.ScaleY > 0f)
			{
				this.m_dundasChart.SaveWithScaleFactors(memoryStream, this.m_owner.ScaleX, this.m_owner.ScaleY);
			}
			else
			{
				this.m_dundasChart.Save(memoryStream);
			}
			memoryStream.Position = 0L;
			this.m_mapAreasCollection = this.m_dundasChart.MapAreas;
			if (this.m_mapAreasCollection != null && this.m_mapAreasCollection.Count > 0)
			{
				hasImageMap = true;
			}
			return memoryStream;
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x00015473 File Offset: 0x00013673
		private ChartImageType DundasImageType(Chart.ImageType type)
		{
			if (type == Chart.ImageType.EMF)
			{
				return 3;
			}
			return 2;
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0001547C File Offset: 0x0001367C
		private bool ShowShadows(ChartMember seriesHeader)
		{
			return Chart.ChartTypes.Line != this.m_chart.Type && (this.m_chart.Type != Chart.ChartTypes.Column || !seriesHeader.IsPlotTypeLine()) && Chart.ChartTypes.Scatter != this.m_chart.Type;
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x000154B8 File Offset: 0x000136B8
		private string DundasChartType(ChartMember seriesHeader, out bool isLineChart)
		{
			isLineChart = false;
			switch (this.m_chart.Type)
			{
			case Chart.ChartTypes.Column:
			{
				if (seriesHeader.IsPlotTypeLine())
				{
					isLineChart = true;
					return "Line";
				}
				Chart.ChartSubTypes chartSubTypes = this.m_chart.SubType;
				if (chartSubTypes == Chart.ChartSubTypes.Stacked)
				{
					return "StackedColumn";
				}
				if (chartSubTypes != Chart.ChartSubTypes.PercentStacked)
				{
					return "Column";
				}
				return "100%StackedColumn";
			}
			case Chart.ChartTypes.Bar:
			{
				Chart.ChartSubTypes chartSubTypes = this.m_chart.SubType;
				if (chartSubTypes == Chart.ChartSubTypes.Stacked)
				{
					return "StackedBar";
				}
				if (chartSubTypes != Chart.ChartSubTypes.PercentStacked)
				{
					return "Bar";
				}
				return "100%StackedBar";
			}
			case Chart.ChartTypes.Line:
				isLineChart = true;
				if (this.m_chart.SubType == Chart.ChartSubTypes.Smooth)
				{
					return "Spline";
				}
				return "Line";
			case Chart.ChartTypes.Pie:
				return "Pie";
			case Chart.ChartTypes.Scatter:
			{
				Chart.ChartSubTypes chartSubTypes = this.m_chart.SubType;
				if (chartSubTypes == Chart.ChartSubTypes.Line)
				{
					isLineChart = true;
					return "Line";
				}
				if (chartSubTypes != Chart.ChartSubTypes.SmoothLine)
				{
					return "Point";
				}
				isLineChart = true;
				return "Spline";
			}
			case Chart.ChartTypes.Bubble:
				return "Bubble";
			case Chart.ChartTypes.Area:
			{
				Chart.ChartSubTypes chartSubTypes = this.m_chart.SubType;
				if (chartSubTypes == Chart.ChartSubTypes.Stacked)
				{
					return "StackedArea";
				}
				if (chartSubTypes != Chart.ChartSubTypes.PercentStacked)
				{
					return "Area";
				}
				return "100%StackedArea";
			}
			case Chart.ChartTypes.Doughnut:
				return "Doughnut";
			case Chart.ChartTypes.Stock:
				if (Chart.ChartSubTypes.Candlestick == this.m_chart.SubType)
				{
					return "CandleStick";
				}
				return "Stock";
			default:
				return "Column";
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06000621 RID: 1569 RVA: 0x00015607 File Offset: 0x00013807
		private bool IsCylinder
		{
			get
			{
				return this.m_chart.ThreeDProperties != null && this.m_chart.ThreeDProperties.Enabled && !this.m_chart.ThreeDProperties.DrawingStyleCube;
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06000622 RID: 1570 RVA: 0x0001563D File Offset: 0x0001383D
		private bool IsExploded
		{
			get
			{
				return Chart.ChartSubTypes.Exploded == this.m_chart.SubType;
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06000623 RID: 1571 RVA: 0x00015650 File Offset: 0x00013850
		private LightStyle LightStyle
		{
			get
			{
				if (this.m_chart.ThreeDProperties == null || !this.m_chart.ThreeDProperties.Enabled)
				{
					return 0;
				}
				ThreeDProperties.ShadingTypes shading = this.m_chart.ThreeDProperties.Shading;
				if (shading == ThreeDProperties.ShadingTypes.Simple)
				{
					return 1;
				}
				if (shading == ThreeDProperties.ShadingTypes.Real)
				{
					return 2;
				}
				return 0;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06000624 RID: 1572 RVA: 0x0001569C File Offset: 0x0001389C
		private ChartColorPalette DundasPalette
		{
			get
			{
				switch (this.m_chart.Palette)
				{
				case Chart.ChartPalette.EarthTones:
					return 6;
				case Chart.ChartPalette.Excel:
					return 3;
				case Chart.ChartPalette.GrayScale:
					return 2;
				case Chart.ChartPalette.Light:
					return 4;
				case Chart.ChartPalette.Pastel:
					return 5;
				case Chart.ChartPalette.SemiTransparent:
					return 7;
				default:
					return 1;
				}
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06000625 RID: 1573 RVA: 0x000156E4 File Offset: 0x000138E4
		private LegendDocking DundasLegendDocking
		{
			get
			{
				if (this.m_chart.Legend == null)
				{
					return 1;
				}
				switch (this.m_chart.Legend.Position)
				{
				case Legend.Positions.TopLeft:
				case Legend.Positions.TopCenter:
				case Legend.Positions.TopRight:
					return 0;
				case Legend.Positions.LeftTop:
				case Legend.Positions.LeftCenter:
				case Legend.Positions.LeftBottom:
					return 3;
				case Legend.Positions.BottomLeft:
				case Legend.Positions.BottomCenter:
				case Legend.Positions.BottomRight:
					return 2;
				}
				return 1;
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06000626 RID: 1574 RVA: 0x00015750 File Offset: 0x00013950
		private StringAlignment DundasLegendAlignment
		{
			get
			{
				if (this.m_chart.Legend == null)
				{
					return StringAlignment.Near;
				}
				switch (this.m_chart.Legend.Position)
				{
				case Legend.Positions.TopCenter:
				case Legend.Positions.LeftCenter:
				case Legend.Positions.RightCenter:
				case Legend.Positions.BottomCenter:
					return StringAlignment.Center;
				case Legend.Positions.TopRight:
				case Legend.Positions.LeftBottom:
				case Legend.Positions.RightBottom:
				case Legend.Positions.BottomRight:
					return StringAlignment.Far;
				}
				return StringAlignment.Near;
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06000627 RID: 1575 RVA: 0x000157B4 File Offset: 0x000139B4
		private LegendStyle DundasLegendStyle
		{
			get
			{
				if (this.m_chart.Legend == null)
				{
					return 0;
				}
				Legend.LegendLayout layout = this.m_chart.Legend.Layout;
				if (layout == Legend.LegendLayout.Row)
				{
					return 1;
				}
				if (layout != Legend.LegendLayout.Table)
				{
					return 0;
				}
				return 2;
			}
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x000157F0 File Offset: 0x000139F0
		private StringAlignment GetTitleAlignment(ChartTitle title)
		{
			if (title != null)
			{
				ChartTitle.Positions position = title.Position;
				if (position == ChartTitle.Positions.Near)
				{
					return StringAlignment.Near;
				}
				if (position == ChartTitle.Positions.Far)
				{
					return StringAlignment.Far;
				}
			}
			return StringAlignment.Center;
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x00015816 File Offset: 0x00013A16
		private TickMarkStyle GetTickMarkStyle(Axis.TickMarks tickmark)
		{
			switch (tickmark)
			{
			case Axis.TickMarks.Inside:
				return 2;
			case Axis.TickMarks.Outside:
				return 1;
			case Axis.TickMarks.Cross:
				return 3;
			default:
				return 0;
			}
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x00015835 File Offset: 0x00013A35
		private MarkerStyle DundasMarkerStyle(ChartDataPoint.MarkerTypes type)
		{
			switch (type)
			{
			case ChartDataPoint.MarkerTypes.Square:
				return 1;
			case ChartDataPoint.MarkerTypes.Circle:
				return 2;
			case ChartDataPoint.MarkerTypes.Diamond:
				return 3;
			case ChartDataPoint.MarkerTypes.Triangle:
				return 4;
			case ChartDataPoint.MarkerTypes.Cross:
				return 5;
			default:
				return 0;
			}
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x00015860 File Offset: 0x00013A60
		private string GetCaption(ChartTitleInstance instance)
		{
			if (instance == null)
			{
				return null;
			}
			return instance.Caption;
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x00015870 File Offset: 0x00013A70
		private Font GetFont(Microsoft.ReportingServices.ReportProcessing.Style styleDef, object[] styleAttributeValues)
		{
			string text = (string)Microsoft.ReportingServices.ReportRendering.Style.GetStyleValue("FontFamily", styleDef, styleAttributeValues);
			ReportSize reportSize = (ReportSize)Microsoft.ReportingServices.ReportRendering.Style.GetStyleValue("FontSize", styleDef, styleAttributeValues);
			string text2 = (string)Microsoft.ReportingServices.ReportRendering.Style.GetStyleValue("FontStyle", styleDef, styleAttributeValues);
			string text3 = (string)Microsoft.ReportingServices.ReportRendering.Style.GetStyleValue("FontWeight", styleDef, styleAttributeValues);
			string text4 = (string)Microsoft.ReportingServices.ReportRendering.Style.GetStyleValue("TextDecoration", styleDef, styleAttributeValues);
			Global.Tracer.Assert(reportSize != null);
			float num = (float)reportSize.ToPoints();
			FontStyle fontStyle = FontStyle.Regular;
			if (Validator.CompareWithInvariantCulture(text2, "Italic"))
			{
				fontStyle |= FontStyle.Italic;
			}
			if (Validator.CompareWithInvariantCulture(text3, "Bold") || Validator.CompareWithInvariantCulture(text3, "Bolder") || Validator.CompareWithInvariantCulture(text3, "700") || Validator.CompareWithInvariantCulture(text3, "800") || Validator.CompareWithInvariantCulture(text3, "900"))
			{
				fontStyle |= FontStyle.Bold;
			}
			if (Validator.CompareWithInvariantCulture(text4, "Underline"))
			{
				fontStyle |= FontStyle.Underline;
			}
			if (Validator.CompareWithInvariantCulture(text4, "LineThrough"))
			{
				fontStyle |= FontStyle.Strikeout;
			}
			return new Font(text, num, fontStyle);
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x00015975 File Offset: 0x00013B75
		private Color GetColor(ReportColor color)
		{
			Global.Tracer.Assert(color != null);
			return color.ToColor();
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x0001598B File Offset: 0x00013B8B
		private Color GetTextColor(Microsoft.ReportingServices.ReportProcessing.Style styleDef, object[] styleAttributeValues)
		{
			return this.GetColor((ReportColor)Microsoft.ReportingServices.ReportRendering.Style.GetStyleValue("Color", styleDef, styleAttributeValues));
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x000159A4 File Offset: 0x00013BA4
		private Color GetShadowColor(Microsoft.ReportingServices.ReportProcessing.Style styleDef, object[] styleAttributeValues)
		{
			return Color.Black;
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x000159AB File Offset: 0x00013BAB
		private int GetShadowOffset(Microsoft.ReportingServices.ReportProcessing.Style styleDef, object[] styleAttributeValues)
		{
			return 0;
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x000159AE File Offset: 0x00013BAE
		private Color GetFillColor(Microsoft.ReportingServices.ReportProcessing.Style styleDef, object[] styleAttributeValues)
		{
			return this.GetColor((ReportColor)Microsoft.ReportingServices.ReportRendering.Style.GetStyleValue("BackgroundColor", styleDef, styleAttributeValues));
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x000159C7 File Offset: 0x00013BC7
		private Color GetChartAreaFillColor(Microsoft.ReportingServices.ReportProcessing.Style styleDef, object[] styleAttributeValues)
		{
			return this.GetColor((ReportColor)Microsoft.ReportingServices.ReportRendering.Style.GetStyleValue("BackgroundColor", styleDef, styleAttributeValues));
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x000159E0 File Offset: 0x00013BE0
		private Color GetFillEndColor(Microsoft.ReportingServices.ReportProcessing.Style styleDef, object[] styleAttributeValues)
		{
			return this.GetColor((ReportColor)Microsoft.ReportingServices.ReportRendering.Style.GetStyleValue("BackgroundGradientEndColor", styleDef, styleAttributeValues));
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x000159F9 File Offset: 0x00013BF9
		private GradientType GetGradientType(Microsoft.ReportingServices.ReportProcessing.Style styleDef, object[] styleAttributeValues)
		{
			return (GradientType)Enum.Parse(typeof(GradientType), Microsoft.ReportingServices.ReportRendering.Style.GetStyleValue("BackgroundGradientType", styleDef, styleAttributeValues) as string, true);
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x00015A21 File Offset: 0x00013C21
		private Color GetBorderColor(Microsoft.ReportingServices.ReportProcessing.Style styleDef, object[] styleAttributeValues)
		{
			return this.GetColor((ReportColor)Microsoft.ReportingServices.ReportRendering.Style.GetStyleValue("BorderColor", styleDef, styleAttributeValues));
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x00015A3A File Offset: 0x00013C3A
		private float GetBorderWidth(Microsoft.ReportingServices.ReportProcessing.Style styleDef, object[] styleAttributeValues)
		{
			return (float)(Microsoft.ReportingServices.ReportRendering.Style.GetStyleValue("BorderWidth", styleDef, styleAttributeValues) as ReportSize).ToPoints();
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x00015A53 File Offset: 0x00013C53
		private string GetWritingMode(Microsoft.ReportingServices.ReportProcessing.Style styleDef, object[] styleAttributeValues)
		{
			return (string)Microsoft.ReportingServices.ReportRendering.Style.GetStyleValue("WritingMode", styleDef, styleAttributeValues);
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x00015A68 File Offset: 0x00013C68
		private ChartDashStyle GetBorderStyle(Microsoft.ReportingServices.ReportProcessing.Style styleDef, object[] styleAttributeValues)
		{
			bool flag;
			return this.GetBorderStyle(styleDef, styleAttributeValues, true, out flag);
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x00015A80 File Offset: 0x00013C80
		private ChartDashStyle GetBorderStyle(Microsoft.ReportingServices.ReportProcessing.Style styleDef, object[] styleAttributeValues, bool returnDefaultStyle, out bool styleSpecified)
		{
			string text = Microsoft.ReportingServices.ReportRendering.Style.GetStyleValue("BorderStyle", styleDef, styleAttributeValues, returnDefaultStyle) as string;
			styleSpecified = text != null;
			if (Validator.CompareWithInvariantCulture(text, "Dashed"))
			{
				return 1;
			}
			if (Validator.CompareWithInvariantCulture(text, "Dotted"))
			{
				return 4;
			}
			if (Validator.CompareWithInvariantCulture(text, "None"))
			{
				return 0;
			}
			return 5;
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x00015AD8 File Offset: 0x00013CD8
		private string GetFormatCode(Microsoft.ReportingServices.ReportProcessing.Style styleDef, object[] styleAttributeValues)
		{
			string text = Microsoft.ReportingServices.ReportRendering.Style.GetStyleValue("Format", styleDef, styleAttributeValues) as string;
			if (text == null)
			{
				return "";
			}
			return text;
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x00015B01 File Offset: 0x00013D01
		private bool SetDataPointColorProperty(object rdlColor, out Color dundasColor)
		{
			dundasColor = Color.Black;
			if (rdlColor != null)
			{
				Global.Tracer.Assert(rdlColor is ReportColor);
				dundasColor = this.GetColor((ReportColor)rdlColor);
				return true;
			}
			return false;
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x00015B39 File Offset: 0x00013D39
		private bool AxisLabelRequiredForRendering(Chart.ChartTypes type, bool scalarMode)
		{
			switch (type)
			{
			case Chart.ChartTypes.Column:
			case Chart.ChartTypes.Bar:
			case Chart.ChartTypes.Line:
			case Chart.ChartTypes.Area:
			case Chart.ChartTypes.Stock:
				return scalarMode;
			case Chart.ChartTypes.Pie:
			case Chart.ChartTypes.Doughnut:
				return false;
			}
			return false;
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00015B6C File Offset: 0x00013D6C
		private void HatchStyleInitialize()
		{
			this.m_hatchPatternIndex = -1;
			this.m_uniquePaletteColors = 16;
			if (Chart.ChartPalette.Light == this.m_chart.Palette)
			{
				this.m_uniquePaletteColors = 10;
			}
			if (Chart.ChartPalette.GrayScale == this.m_chart.Palette)
			{
				this.m_hatchPatternIndex = 0;
			}
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x00015BA8 File Offset: 0x00013DA8
		private void HatchStyleIncrement(int seriesIndex, int categoryIndex)
		{
			bool flag = Chart.ChartTypes.Pie == this.m_chart.Type || Chart.ChartTypes.Doughnut == this.m_chart.Type;
			if (Chart.ChartPalette.GrayScale == this.m_chart.Palette)
			{
				if (categoryIndex == 0 || flag)
				{
					this.m_hatchPatternIndex++;
					return;
				}
			}
			else if (flag)
			{
				if (categoryIndex % this.m_uniquePaletteColors == 0)
				{
					this.m_hatchPatternIndex++;
					return;
				}
			}
			else if (categoryIndex == 0 && seriesIndex % this.m_uniquePaletteColors == 0)
			{
				this.m_hatchPatternIndex++;
			}
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x00015C34 File Offset: 0x00013E34
		private ChartHatchStyle GetCurrentHatchStyle()
		{
			if (!Enum.IsDefined(typeof(ChartHatchStyle), this.m_hatchPatternIndex))
			{
				if (Chart.ChartPalette.GrayScale != this.m_chart.Palette)
				{
					this.m_hatchPatternIndex = 0;
				}
				else
				{
					this.m_hatchPatternIndex = 1;
				}
			}
			return this.m_hatchPatternIndex;
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x00015C84 File Offset: 0x00013E84
		private MarkerStyle GetNextSeriesMarkerStyle()
		{
			this.m_markerStyleIndex++;
			if (!Enum.IsDefined(typeof(MarkerStyle), this.m_markerStyleIndex))
			{
				this.m_markerStyleIndex = 1;
			}
			this.m_currentAutoMarkerStyle = this.m_markerStyleIndex;
			return this.m_currentAutoMarkerStyle;
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x00015CD4 File Offset: 0x00013ED4
		private bool ChartTypeWithExplicitXValue(Chart.ChartTypes type)
		{
			return Chart.ChartTypes.Scatter == type || Chart.ChartTypes.Bubble == type;
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x00015CE4 File Offset: 0x00013EE4
		internal void RenderChartMapAreas(ref ImageMapAreasCollection[] imageMapAreasCollection)
		{
			if (imageMapAreasCollection != null)
			{
				return;
			}
			if (this.m_chart == null || this.ChartInstanceInfo == null || this.m_mapAreasCollection == null || this.m_mapAreasCollection.Count == 0)
			{
				return;
			}
			imageMapAreasCollection = new ImageMapAreasCollection[this.m_owner.DataPointCollection.SeriesCount * this.m_owner.DataPointCollection.CategoryCount];
			int count = this.m_mapAreasCollection.Count;
			int i = 0;
			while (i < count)
			{
				int num = this.m_mapAreasCollection[i].MapAreaID - this.c_offset;
				int num2 = this.m_mapAreasCollection[i].NonEmptyMapAreaID - this.c_offset;
				if (0 <= this.m_mapAreasCollection[i].MapAreaID)
				{
					goto IL_00B2;
				}
				if (num2 >= 0)
				{
					num = num2;
					goto IL_00B2;
				}
				IL_0124:
				i++;
				continue;
				IL_00B2:
				if (num >= 0)
				{
					Global.Tracer.Assert(num >= 0);
					Global.Tracer.Assert(num / this.m_owner.DataPointCollection.CategoryCount < this.m_owner.DataPointCollection.SeriesCount);
					if (imageMapAreasCollection[num] == null)
					{
						imageMapAreasCollection[num] = new ImageMapAreasCollection(4);
					}
					ImageMapArea imageMapArea = new ImageMapArea(this.m_mapAreasCollection[i], num);
					imageMapAreasCollection[num].Add(imageMapArea);
					goto IL_0124;
				}
				goto IL_0124;
			}
			this.m_mapAreasCollection = null;
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x00015E28 File Offset: 0x00014028
		private double ConvertToDundasDouble(object value)
		{
			ChartValueTypes chartValueTypes = 0;
			return this.ConvertToDundasDouble(value, ref chartValueTypes, DundasChart.YearConvertMode.None);
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x00015E44 File Offset: 0x00014044
		private double ConvertToDundasDouble(object value, ref ChartValueTypes axisType, DundasChart.YearConvertMode convertMode)
		{
			if (value != null)
			{
				DataAggregate.DataTypeCode dataTypeCode;
				ChartValueTypes chartValueTypes = this.DetermineDatatype(value, out dataTypeCode);
				if (chartValueTypes == null)
				{
					return double.NaN;
				}
				if (3 == chartValueTypes)
				{
					if (8 == axisType)
					{
						return this.ConvertToDundasDate((int)value, convertMode);
					}
					axisType = chartValueTypes;
					return DataTypeUtility.ConvertToDouble(dataTypeCode, value);
				}
				else
				{
					if (1 == chartValueTypes)
					{
						return DataTypeUtility.ConvertToDouble(dataTypeCode, value);
					}
					if (8 == chartValueTypes)
					{
						if (axisType == 0 || 8 == axisType)
						{
							axisType = 8;
							return ((DateTime)value).ToOADate();
						}
					}
				}
			}
			return double.NaN;
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x00015EC8 File Offset: 0x000140C8
		private double ConvertToDundasDate(int value, DundasChart.YearConvertMode mode)
		{
			double num;
			try
			{
				DateTime dateTime;
				if (mode == DundasChart.YearConvertMode.Min)
				{
					dateTime = new DateTime(value, 1, 1);
				}
				else if (DundasChart.YearConvertMode.Max == mode)
				{
					dateTime = new DateTime(value, 12, 31);
				}
				else
				{
					if (DundasChart.YearConvertMode.MidYear != mode)
					{
						return (double)value;
					}
					dateTime = new DateTime(value, 7, 1);
				}
				num = dateTime.ToOADate();
			}
			catch
			{
				num = double.NaN;
			}
			return num;
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x00015F34 File Offset: 0x00014134
		internal static object DetectAndConvertAxisValue(ExpressionInfo definition, object instanceValue)
		{
			if (definition == null)
			{
				Global.Tracer.Assert(instanceValue == null);
				return null;
			}
			if (instanceValue != null)
			{
				return instanceValue;
			}
			Global.Tracer.Assert(ExpressionInfo.Types.Constant == definition.Type);
			if (definition.Value == null)
			{
				return definition.IntValue;
			}
			double naN = double.NaN;
			if (double.TryParse(definition.Value, NumberStyles.Integer, CultureInfo.InvariantCulture.NumberFormat, out naN))
			{
				return (int)naN;
			}
			if (double.TryParse(definition.Value, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowTrailingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, CultureInfo.InvariantCulture.NumberFormat, out naN))
			{
				return naN;
			}
			object obj;
			try
			{
				obj = DateTime.Parse(definition.Value, CultureInfo.InvariantCulture.DateTimeFormat, DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite | DateTimeStyles.AllowInnerWhite | DateTimeStyles.AdjustToUniversal);
			}
			catch
			{
				obj = definition.Value;
			}
			return obj;
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0001600C File Offset: 0x0001420C
		private void RenderAxis(Axis dundasAxis, Axis axis, AxisInstance axisInstance, bool isAxisY, bool isScalarMode, ChartValueTypes axisType)
		{
			if (axis == null || axisInstance == null)
			{
				Global.Tracer.Trace(TraceLevel.Warning, "ChartRendering: there is no axis to render!");
				return;
			}
			double num = this.ConvertToDundasDouble(DundasChart.DetectAndConvertAxisValue(axis.MajorInterval, axisInstance.MajorIntervalValue));
			double num2 = this.ConvertToDundasDouble(DundasChart.DetectAndConvertAxisValue(axis.MinorInterval, axisInstance.MinorIntervalValue));
			if (num < 0.0)
			{
				num = double.NaN;
			}
			if (num2 < 0.0)
			{
				num2 = double.NaN;
			}
			double num3 = double.NaN;
			if (!axis.AutoCrossAt)
			{
				num3 = this.ConvertToDundasDouble(DundasChart.DetectAndConvertAxisValue(axis.CrossAt, axisInstance.CrossAtValue), ref axisType, DundasChart.YearConvertMode.MidYear);
			}
			double num4 = double.NaN;
			if (!axis.AutoScaleMin)
			{
				num4 = this.ConvertToDundasDouble(DundasChart.DetectAndConvertAxisValue(axis.Min, axisInstance.MinValue), ref axisType, DundasChart.YearConvertMode.Min);
			}
			else if (isAxisY)
			{
				Global.Tracer.Assert(this.m_owner.RenderingContext != null && this.m_owner.RenderingContext.IntermediateFormatVersion != null);
				if (this.m_owner.RenderingContext.IntermediateFormatVersion.IsRS2000_WithNewChartYAxis)
				{
					dundasAxis.StartFromZero = false;
				}
			}
			double num5 = double.NaN;
			if (!axis.AutoScaleMax)
			{
				num5 = this.ConvertToDundasDouble(DundasChart.DetectAndConvertAxisValue(axis.Max, axisInstance.MaxValue), ref axisType, DundasChart.YearConvertMode.Max);
			}
			if (num4 >= num5)
			{
				num4 = double.NaN;
				num5 = double.NaN;
			}
			dundasAxis.Margin = axis.Margin;
			if (!isAxisY && isScalarMode)
			{
				dundasAxis.LabelStyle.ShowEndLabels = !axis.Margin;
			}
			if (isAxisY || isScalarMode)
			{
				dundasAxis.Crossing = num3;
				dundasAxis.Minimum = num4;
				dundasAxis.Maximum = num5;
			}
			dundasAxis.Enabled = 1;
			dundasAxis.Reverse = axis.Reverse;
			dundasAxis.Interlaced = axis.Interlaced;
			dundasAxis.PositiveOnlyScale = true;
			Microsoft.ReportingServices.ReportProcessing.Style style = axis.StyleClass;
			object[] array = axisInstance.StyleAttributeValues;
			dundasAxis.LineColor = this.GetBorderColor(style, array);
			dundasAxis.LineStyle = this.GetBorderStyle(style, array);
			dundasAxis.LineWidth = (int)Math.Round((double)this.GetBorderWidth(style, array));
			if (axis.Title != null)
			{
				string caption = this.GetCaption(axisInstance.Title);
				if (caption != null)
				{
					dundasAxis.Title = caption;
					dundasAxis.TitleAlignment = this.GetTitleAlignment(axis.Title);
					dundasAxis.TitleColor = this.GetTextColor(axis.Title.StyleClass, axisInstance.Title.StyleAttributeValues);
					dundasAxis.TitleFont = this.GetFont(axis.Title.StyleClass, axisInstance.Title.StyleAttributeValues);
				}
			}
			if (!isAxisY && !isScalarMode)
			{
				double num6;
				if (!double.IsNaN(num))
				{
					num6 = (double.IsNaN(num2) ? num : Math.Min(num, num2));
				}
				else if (!double.IsNaN(num2))
				{
					num6 = num2;
				}
				else
				{
					num6 = 1.0;
				}
				dundasAxis.LabelStyle.Interval = num6;
				dundasAxis.Interval = num6;
			}
			if (num > 0.0 && (isAxisY || isScalarMode))
			{
				dundasAxis.LabelStyle.Interval = num;
				dundasAxis.Interval = num;
			}
			if (axis.MajorGridLines != null)
			{
				style = axis.MajorGridLines.StyleClass;
				array = axisInstance.MajorGridLinesStyleAttributeValues;
			}
			else
			{
				style = null;
				array = null;
			}
			dundasAxis.MajorGrid.Enabled = false;
			dundasAxis.MajorTickMark.Style = this.GetTickMarkStyle(axis.MajorTickMarks);
			Color color = this.GetBorderColor(style, array);
			ChartDashStyle chartDashStyle = this.GetBorderStyle(style, array);
			int num7 = (int)Math.Round((double)this.GetBorderWidth(style, array));
			if (axis.MajorTickMarks != Axis.TickMarks.None)
			{
				if (num > 0.0)
				{
					dundasAxis.MajorTickMark.Interval = num;
				}
				else
				{
					dundasAxis.MajorTickMark.IntervalType = 0;
				}
				dundasAxis.MajorTickMark.LineColor = color;
				dundasAxis.MajorTickMark.LineStyle = chartDashStyle;
				dundasAxis.MajorTickMark.LineWidth = num7;
			}
			if (axis.MajorGridLines != null && axis.MajorGridLines.ShowGridLines)
			{
				dundasAxis.MajorGrid.Enabled = true;
				if (num > 0.0)
				{
					dundasAxis.MajorGrid.Interval = num;
				}
				else
				{
					dundasAxis.MajorGrid.IntervalType = 0;
				}
				dundasAxis.MajorGrid.LineColor = color;
				dundasAxis.MajorGrid.LineStyle = chartDashStyle;
				dundasAxis.MajorGrid.LineWidth = num7;
			}
			if (axis.MinorGridLines != null)
			{
				style = axis.MinorGridLines.StyleClass;
				array = axisInstance.MinorGridLinesStyleAttributeValues;
			}
			else
			{
				style = null;
				array = null;
			}
			dundasAxis.MinorGrid.Enabled = false;
			dundasAxis.MinorTickMark.Style = this.GetTickMarkStyle(axis.MinorTickMarks);
			color = this.GetBorderColor(style, array);
			chartDashStyle = this.GetBorderStyle(style, array);
			num7 = (int)Math.Round((double)this.GetBorderWidth(style, array));
			if (axis.MinorTickMarks != Axis.TickMarks.None)
			{
				if (num2 > 0.0)
				{
					dundasAxis.MinorTickMark.Interval = num2;
				}
				else
				{
					dundasAxis.MinorTickMark.IntervalType = 0;
					dundasAxis.MinorTickMark.Enabled = true;
				}
				dundasAxis.MinorTickMark.LineColor = color;
				dundasAxis.MinorTickMark.LineStyle = chartDashStyle;
				dundasAxis.MinorTickMark.LineWidth = num7;
			}
			if (axis.MinorGridLines != null && axis.MinorGridLines.ShowGridLines)
			{
				dundasAxis.MinorGrid.Enabled = true;
				if (num2 > 0.0)
				{
					dundasAxis.MinorGrid.Interval = num2;
				}
				else
				{
					dundasAxis.MinorGrid.IntervalType = 0;
				}
				dundasAxis.MinorGrid.LineColor = color;
				dundasAxis.MinorGrid.LineStyle = chartDashStyle;
				dundasAxis.MinorGrid.LineWidth = num7;
			}
			if (axis.LogScale && (isAxisY || isScalarMode))
			{
				dundasAxis.Logarithmic = axis.LogScale;
			}
			if (!axis.Visible)
			{
				dundasAxis.LabelStyle.Enabled = false;
				return;
			}
			style = axis.StyleClass;
			array = axisInstance.StyleAttributeValues;
			dundasAxis.LabelStyle.Enabled = true;
			dundasAxis.LabelStyle.FontColor = this.GetTextColor(style, array);
			dundasAxis.LabelStyle.Format = this.GetFormatCode(style, array);
			dundasAxis.LabelStyle.Font = this.GetFont(style, array);
			dundasAxis.LabelsAutoFit = !isAxisY;
			if (Chart.ChartTypes.Bar == this.m_chart.Type)
			{
				dundasAxis.LabelsAutoFit = !dundasAxis.LabelsAutoFit;
			}
			if (dundasAxis.LabelsAutoFit)
			{
				dundasAxis.LabelsAutoFitStyle = 100;
			}
			if (8 == axisType || 3 == axisType)
			{
				if (8 == axisType)
				{
					dundasAxis.LabelStyle.IntervalType = 0;
					dundasAxis.Logarithmic = false;
				}
				else
				{
					dundasAxis.LabelStyle.IntervalType = 1;
				}
				int dataPointSeriesCount = this.m_owner.DataPointSeriesCount;
				for (int i = 0; i < dataPointSeriesCount; i++)
				{
					if (isAxisY)
					{
						this.m_dundasChart.Series[i].YValueType = axisType;
					}
					else
					{
						this.m_dundasChart.Series[i].XValueType = axisType;
					}
				}
			}
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x000166F3 File Offset: 0x000148F3
		private string RenderLocalizedText(object sender, string text, int elementID, ChartElementType elementType)
		{
			return text;
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x000166F8 File Offset: 0x000148F8
		private string RenderNumbers(object sender, double value, string format, ChartValueTypes valueType, int elementID, ChartElementType elementType)
		{
			Microsoft.ReportingServices.ReportProcessing.Style styleClass = this.m_chart.StyleClass;
			object[] styleAttributeValues = this.ChartInstanceInfo.StyleAttributeValues;
			string text = null;
			bool flag = false;
			if (8 == valueType || 9 == valueType || 10 == valueType)
			{
				text = DateTime.FromOADate(value).ToString(format, CultureInfo.CurrentCulture);
			}
			else
			{
				try
				{
					if (ReportProcessing.CompareWithInvariantCulture(format, "x", true) == 0)
					{
						flag = true;
					}
					text = value.ToString(format, CultureInfo.CurrentCulture);
				}
				catch
				{
				}
			}
			if (!flag)
			{
				int num = (int)Microsoft.ReportingServices.ReportRendering.Style.GetStyleValue("NumeralVariant", styleClass, styleAttributeValues);
				if (num > 2)
				{
					bool flag2 = true;
					text = FormatDigitReplacement.FormatNumeralVariant(text, num, (this.m_numeralCulture == null) ? this.m_currentCulture : this.m_numeralCulture, this.m_currentCulture.NumberFormat.NumberDecimalSeparator, out flag2);
				}
			}
			if (text != null)
			{
				return text;
			}
			return "";
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x000167DC File Offset: 0x000149DC
		private void RenderChart()
		{
			Global.Tracer.Assert(this.m_chart != null);
			Global.Tracer.Assert(this.ChartInstanceInfo != null);
			Global.Tracer.Assert(this.m_dundasChart != null);
			Chart dundasChart = this.m_dundasChart;
			dundasChart.FormatNumberHandler = (FormatNumberHandler)Delegate.Combine(dundasChart.FormatNumberHandler, new FormatNumberHandler(this.RenderNumbers));
			if (this.m_chart.Title != null && this.ChartInstanceInfo.Title != null)
			{
				string caption = this.GetCaption(this.ChartInstanceInfo.Title);
				if (caption != null)
				{
					this.m_dundasChart.Titles.Add(caption, 0, this.GetFont(this.m_chart.Title.StyleClass, this.ChartInstanceInfo.Title.StyleAttributeValues), this.GetTextColor(this.m_chart.Title.StyleClass, this.ChartInstanceInfo.Title.StyleAttributeValues));
				}
			}
			this.HatchStyleInitialize();
			this.m_dundasChart.Palette = this.DundasPalette;
			this.m_dundasChart.BackColor = this.GetFillColor(this.m_chart.StyleClass, this.ChartInstanceInfo.StyleAttributeValues);
			this.m_dundasChart.BackGradientType = this.GetGradientType(this.m_chart.StyleClass, this.ChartInstanceInfo.StyleAttributeValues);
			if (this.m_dundasChart.BackGradientType != null)
			{
				Color fillEndColor = this.GetFillEndColor(this.m_chart.StyleClass, this.ChartInstanceInfo.StyleAttributeValues);
				if (Color.Transparent != fillEndColor)
				{
					this.m_dundasChart.BackGradientEndColor = fillEndColor;
				}
			}
			this.m_dundasChart.BorderStyle = 0;
			this.m_dundasChart.BorderLineStyle = 0;
			this.m_dundasChart.BorderLineWidth = 0;
			ChartArea chartArea = new ChartArea();
			chartArea.Name = this.m_chart.Name;
			Microsoft.ReportingServices.ReportProcessing.Style style = null;
			object[] array = null;
			if (this.m_chart.PlotArea != null)
			{
				style = this.m_chart.PlotArea.StyleClass;
				array = this.ChartInstanceInfo.PlotAreaStyleAttributeValues;
			}
			Color color = this.GetFillColor(style, array);
			if (color == Color.Transparent)
			{
				color = Color.FromArgb(1, 255, 255, 255);
			}
			chartArea.BackColor = color;
			chartArea.BackGradientType = this.GetGradientType(style, array);
			if (chartArea.BackGradientType != null)
			{
				chartArea.BackGradientEndColor = this.GetFillEndColor(style, array);
			}
			chartArea.BorderColor = this.GetBorderColor(style, array);
			chartArea.BorderStyle = this.GetBorderStyle(style, array);
			chartArea.ShadowColor = this.GetShadowColor(style, array);
			chartArea.ShadowOffset = this.GetShadowOffset(style, array);
			if (chartArea.BorderStyle == null)
			{
				chartArea.BorderWidth = 0f;
			}
			else
			{
				chartArea.BorderWidth = this.GetBorderWidth(style, array);
			}
			bool flag = false;
			if (this.m_chart.Legend != null && this.m_chart.Legend.Visible)
			{
				flag = true;
				Global.Tracer.Assert(this.m_dundasChart.Legends != null);
				Legend legend = this.m_dundasChart.Legends["Default"];
				Global.Tracer.Assert(legend != null);
				style = this.m_chart.Legend.StyleClass;
				array = this.ChartInstanceInfo.LegendStyleAttributeValues;
				legend.Enabled = true;
				legend.Position.Auto = true;
				if (this.m_chart.Legend.InsidePlotArea)
				{
					legend.DockToChartArea = chartArea.Name;
					legend.DockInsideChartArea = true;
				}
				legend.Docking = this.DundasLegendDocking;
				legend.Alignment = this.DundasLegendAlignment;
				legend.LegendStyle = this.DundasLegendStyle;
				legend.EquallySpacedItems = true;
				legend.Font = this.GetFont(style, array);
				legend.FontColor = this.GetTextColor(style, array);
				legend.BorderStyle = this.GetBorderStyle(style, array);
				legend.BorderColor = this.GetBorderColor(style, array);
				legend.BorderWidth = this.GetBorderWidth(style, array);
				legend.ShadowColor = this.GetShadowColor(style, array);
				legend.ShadowOffset = this.GetShadowOffset(style, array);
				legend.BackColor = this.GetFillColor(style, array);
				legend.BackGradientType = this.GetGradientType(style, array);
				if (legend.BackGradientType != null)
				{
					legend.BackGradientEndColor = this.GetFillEndColor(style, array);
				}
			}
			else
			{
				this.m_dundasChart.Legends.Clear();
			}
			bool flag2 = Chart.ChartSubTypes.Stacked == this.m_chart.SubType || Chart.ChartSubTypes.PercentStacked == this.m_chart.SubType;
			this.m_populateSeriesIndexes = flag && flag2;
			if (this.m_populateSeriesIndexes)
			{
				this.m_positiveSeriesIndexes = new IntList();
				this.m_negativeSeriesIndexes = new IntList();
			}
			if (this.m_chart.ThreeDProperties != null && this.m_chart.ThreeDProperties.Enabled)
			{
				chartArea.Area3DStyle.Enable3D = this.m_chart.ThreeDProperties.Enabled;
				chartArea.Area3DStyle.Light = this.LightStyle;
				chartArea.Area3DStyle.WallWidth = (int)Math.Round(0.3 * (double)this.m_chart.ThreeDProperties.WallThickness);
				chartArea.Area3DStyle.Perspective = this.m_chart.ThreeDProperties.Perspective;
				chartArea.Area3DStyle.XAngle = this.m_chart.ThreeDProperties.Rotation;
				chartArea.Area3DStyle.YAngle = this.m_chart.ThreeDProperties.Inclination;
				chartArea.Area3DStyle.RightAngleAxes = !this.m_chart.ThreeDProperties.PerspectiveProjectionMode;
				chartArea.Area3DStyle.Clustered = this.m_chart.ThreeDProperties.Clustered;
			}
			this.m_dundasChart.ChartAreas.Add(chartArea);
			BackgroundImage backgroundImage = null;
			if (this.m_owner.Style.SharedProperties != null)
			{
				backgroundImage = (BackgroundImage)this.m_owner.Style.SharedProperties["BackgroundImage"];
			}
			if (backgroundImage == null && this.m_owner.Style.NonSharedProperties != null)
			{
				backgroundImage = (BackgroundImage)this.m_owner.Style.NonSharedProperties["BackgroundImage"];
			}
			if (backgroundImage != null)
			{
				global::System.Drawing.Image image = null;
				if (backgroundImage.ImageData != null)
				{
					image = global::System.Drawing.Image.FromStream(new MemoryStream(backgroundImage.ImageData, false));
				}
				if (image != null)
				{
					((Bitmap)image).SetResolution(96f, 96f);
					this.m_dundasChart.Images.Add("BackgroundImage", image);
					this.m_dundasChart.ChartAreas[0].BackImage = "BackgroundImage";
					string text = (string)this.m_owner.Style.SharedProperties["BackgroundRepeat"];
					if (ReportProcessing.CompareWithInvariantCulture(text, "NoRepeat", true) == 0)
					{
						this.m_dundasChart.ChartAreas[0].BackImageMode = 4;
						this.m_dundasChart.ChartAreas[0].BackImageAlign = 8;
					}
				}
			}
			if (this.m_chart.CategoryAxis != null)
			{
				if (this.m_chart.CategoryAxis.Scalar)
				{
					this.m_isScalarMode = true;
				}
				else
				{
					this.m_X_axisType = 7;
					chartArea.AxisX.IntervalType = 0;
					chartArea.AxisX.LabelStyle.IntervalType = 0;
					if (!this.ChartTypeWithExplicitXValue(this.m_chart.Type))
					{
						chartArea.AxisX.LabelStyle.Interval = 1.0;
						chartArea.AxisX.Interval = 1.0;
					}
				}
			}
			if (Chart.ChartTypes.Pie == this.m_chart.Type || Chart.ChartTypes.Doughnut == this.m_chart.Type)
			{
				this.m_X_axisType = 1;
				this.m_Y_axisType = 1;
			}
			this.m_dundasChart.Series.Clear();
			ChartMemberCollection seriesMemberCollection = this.m_owner.SeriesMemberCollection;
			ChartMemberCollection categoryMemberCollection = this.m_owner.CategoryMemberCollection;
			Global.Tracer.Assert(seriesMemberCollection != null);
			Global.Tracer.Assert(categoryMemberCollection != null);
			this.m_categoryLabels = new ArrayList(this.m_owner.DataPointCollection.CategoryCount);
			if (Chart.ChartTypes.Pie == this.m_chart.Type || Chart.ChartTypes.Doughnut == this.m_chart.Type)
			{
				for (int i = 0; i < categoryMemberCollection.Count; i++)
				{
					this.GenerateFlattenedCategoryLabels(categoryMemberCollection[i], 1, "");
				}
			}
			else
			{
				bool flag3 = !this.m_isScalarMode && this.m_chart.CategoryAxis != null && this.m_chart.CategoryAxis.Visible;
				if (flag3)
				{
					switch (this.m_chart.Type)
					{
					case Chart.ChartTypes.Column:
					case Chart.ChartTypes.Bar:
					case Chart.ChartTypes.Line:
					case Chart.ChartTypes.Area:
					case Chart.ChartTypes.Stock:
						break;
					default:
						flag3 = false;
						break;
					}
				}
				string text2 = null;
				if (this.m_chart.CategoryAxis != null)
				{
					text2 = (string)Microsoft.ReportingServices.ReportRendering.Style.GetStyleValue("Format", this.m_chart.CategoryAxis.StyleClass, this.ChartInstanceInfo.CategoryAxis.StyleAttributeValues);
				}
				for (int j = 0; j < categoryMemberCollection.Count; j++)
				{
					this.GenerateCategoryLabels(1, categoryMemberCollection[j], flag3, text2, chartArea.AxisX.CustomLabels);
				}
			}
			for (int k = 0; k < seriesMemberCollection.Count; k++)
			{
				this.RenderSeries(seriesMemberCollection[k], k + 1, "", this.m_owner.DataPointCollection);
			}
			if (this.m_chart.CategoryAxis != null)
			{
				this.RenderAxis(chartArea.AxisX, this.m_chart.CategoryAxis, this.ChartInstanceInfo.CategoryAxis, false, this.m_isScalarMode, this.m_X_axisType);
			}
			else
			{
				chartArea.AxisX.LabelStyle.Enabled = false;
			}
			if (this.m_chart.ValueAxis != null)
			{
				this.RenderAxis(chartArea.AxisY, this.m_chart.ValueAxis, this.ChartInstanceInfo.ValueAxis, true, this.m_isScalarMode, this.m_Y_axisType);
			}
			else
			{
				chartArea.AxisY.LabelStyle.Enabled = false;
			}
			this.m_dundasChart.ApplyPaletteColors();
			foreach (object obj in this.m_dundasChart.Series)
			{
				Series series = (Series)obj;
				if (series.XValueIndexed || series.PlotAsLine)
				{
					int count = series.Points.Count;
					if (!flag2 || Chart.ChartTypes.Area != this.m_chart.Type)
					{
						int num = 0;
						while (num < count && series.Points[num].Empty)
						{
							series.Points[num].Color = Color.Transparent;
							series.Points[num].BorderColor = Color.Transparent;
							series.Points[num].BackGradientEndColor = Color.Transparent;
							num++;
						}
						int num2 = count - 1;
						while (num2 >= 0 && series.Points[num2].Empty)
						{
							series.Points[num2].Color = Color.Transparent;
							series.Points[num2].BorderColor = Color.Transparent;
							series.Points[num2].BackGradientEndColor = Color.Transparent;
							num2--;
						}
					}
				}
				series.EmptyPointStyle.Color = series.Color;
				series.EmptyPointStyle.BorderColor = series.BorderColor;
				series.EmptyPointStyle.BorderWidth = series.BorderWidth;
				series.EmptyPointStyle.BorderStyle = series.BorderStyle;
				series.EmptyPointStyle.BackGradientEndColor = series.BackGradientEndColor;
				series.EmptyPointStyle.BackHatchStyle = series.BackHatchStyle;
			}
			if (this.m_populateSeriesIndexes && this.m_dundasChart.Series.Count != 0)
			{
				this.ReorderStackedLegend(chartArea);
			}
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x00017434 File Offset: 0x00015634
		private void ReorderStackedLegend(ChartArea area)
		{
			int count = this.m_dundasChart.Series.Count;
			Global.Tracer.Assert(0 <= count && this.m_negativeSeriesIndexes.Count + this.m_positiveSeriesIndexes.Count == count);
			bool flag = false;
			if ((Chart.ChartTypes.Area == this.m_chart.Type || this.m_chart.Type == Chart.ChartTypes.Column) && !area.AxisX.Reverse)
			{
				flag = true;
			}
			if (Chart.ChartTypes.Bar == this.m_chart.Type && area.AxisY.Reverse)
			{
				flag = true;
			}
			int num = 0;
			IntList intList;
			if (flag)
			{
				intList = this.m_positiveSeriesIndexes;
			}
			else
			{
				intList = this.m_negativeSeriesIndexes;
			}
			for (int i = intList.Count; i > 0; i--)
			{
				int num2 = intList[i - 1];
				this.CustomLegendAppend(this.m_dundasChart.Series[num2], num);
				num++;
			}
			if (flag)
			{
				intList = this.m_negativeSeriesIndexes;
			}
			else
			{
				intList = this.m_positiveSeriesIndexes;
			}
			for (int j = 0; j < intList.Count; j++)
			{
				int num3 = intList[j];
				this.CustomLegendAppend(this.m_dundasChart.Series[num3], num);
				num++;
			}
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x00017574 File Offset: 0x00015774
		private void CustomLegendAppend(Series series, int position)
		{
			series.ShowInLegend = false;
			this.m_dundasChart.Legends["Default"].CustomItems.Add(series.Color, series.LegendText);
			this.m_dundasChart.Legends["Default"].CustomItems[position].BorderColor = series.BorderColor;
			this.m_dundasChart.Legends["Default"].CustomItems[position].BorderStyle = series.BorderStyle;
			this.m_dundasChart.Legends["Default"].CustomItems[position].BorderWidth = series.BorderWidth;
			this.m_dundasChart.Legends["Default"].CustomItems[position].BackGradientEndColor = series.BackGradientEndColor;
			this.m_dundasChart.Legends["Default"].CustomItems[position].BackGradientType = series.BackGradientType;
			this.m_dundasChart.Legends["Default"].CustomItems[position].BackHatchStyle = series.BackHatchStyle;
			this.m_dundasChart.Legends["Default"].CustomItems[position].ShadowColor = series.ShadowColor;
			this.m_dundasChart.Legends["Default"].CustomItems[position].ShadowOffset = series.ShadowOffset;
			this.m_dundasChart.Legends["Default"].CustomItems[position].MarkerBorderColor = series.MarkerBorderColor;
			this.m_dundasChart.Legends["Default"].CustomItems[position].MarkerColor = series.MarkerColor;
			this.m_dundasChart.Legends["Default"].CustomItems[position].MarkerSize = series.MarkerSize;
			this.m_dundasChart.Legends["Default"].CustomItems[position].MarkerStyle = series.MarkerStyle;
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x000177B8 File Offset: 0x000159B8
		private void RenderSeries(ChartMember series, int index, string flattenedSeriesLabel, ChartDataPointCollection dataPoints)
		{
			object memberLabel = series.MemberLabel;
			string text = ((memberLabel == null) ? (RPRes.rsPropertyNameSeries + " " + index.ToString(CultureInfo.InvariantCulture)) : memberLabel.ToString());
			if (flattenedSeriesLabel.Length > 0)
			{
				flattenedSeriesLabel = flattenedSeriesLabel + " - " + text;
			}
			else
			{
				flattenedSeriesLabel = text;
			}
			if (series.IsInnerMostMember)
			{
				this.RenderSeriesDataPoints(series, flattenedSeriesLabel, dataPoints);
				return;
			}
			ChartMemberCollection children = series.Children;
			Global.Tracer.Assert(children != null);
			for (int i = 0; i < children.Count; i++)
			{
				this.RenderSeries(children[i], i + 1, flattenedSeriesLabel, dataPoints);
			}
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x0001785C File Offset: 0x00015A5C
		private void RenderSeriesDataPoints(ChartMember seriesHeader, string flattenedSeriesLabel, ChartDataPointCollection dataPoints)
		{
			Global.Tracer.Assert(seriesHeader != null);
			Global.Tracer.Assert(seriesHeader.IsInnerMostMember);
			int memberDataPointIndex = seriesHeader.MemberDataPointIndex;
			int categoryCount = this.m_owner.DataPointCollection.CategoryCount;
			if (categoryCount == 0)
			{
				return;
			}
			Series series = new Series();
			bool flag;
			series.ChartType = this.DundasChartType(seriesHeader, out flag);
			series.YValueType = 0;
			series.PlotAsLine = seriesHeader.IsPlotTypeLine();
			series.PlotAsLine = seriesHeader.IsPlotTypeLine();
			if (Chart.ChartPalette.GrayScale == this.m_chart.Palette)
			{
				series.BorderWidth = 1f;
				series.BorderStyle = 5;
				series.BorderColor = Color.Black;
			}
			series.Name = "Series " + memberDataPointIndex.ToString();
			if (flattenedSeriesLabel != null)
			{
				series.LegendText = flattenedSeriesLabel;
			}
			if (this.m_isScalarMode)
			{
				series.XValueIndexed = false;
			}
			else
			{
				series.XValueType = 7;
				if (Chart.ChartTypes.Pie != this.m_chart.Type && Chart.ChartTypes.Doughnut != this.m_chart.Type && !this.ChartTypeWithExplicitXValue(this.m_chart.Type))
				{
					series.XValueIndexed = true;
				}
			}
			if (!series.PlotAsLine && (this.m_chart.Type == Chart.ChartTypes.Column || Chart.ChartTypes.Bar == this.m_chart.Type))
			{
				if (this.m_chart.PointWidth != 0)
				{
					series["PointWidth"] = ((double)this.m_chart.PointWidth * 0.01).ToString(CultureInfo.InvariantCulture);
				}
				else
				{
					series["PointWidth"] = "0.55";
				}
			}
			else if (flag)
			{
				series.BorderWidth = 3f;
			}
			else if (Chart.ChartTypes.Pie == this.m_chart.Type || Chart.ChartTypes.Doughnut == this.m_chart.Type)
			{
				series.BorderStyle = 5;
				series.BorderWidth = 1f;
				series.BorderColor = Color.Black;
			}
			if (this.IsCylinder)
			{
				series["DrawingStyle"] = "Cylinder";
			}
			if (Chart.ChartSubTypes.HighLowClose == this.m_chart.SubType)
			{
				series["ShowOpenClose"] = "Close";
			}
			series.Font = this.GetFont(null, null);
			series.FontColor = this.GetTextColor(null, null);
			series.ShowLabelAsValue = false;
			if (Chart.ChartTypes.Bubble == this.m_chart.Type)
			{
				series.MarkerSize = 4;
				series.MarkerStyle = 2;
			}
			Chart.ChartTypes type = this.m_chart.Type;
			if (type != Chart.ChartTypes.Bubble)
			{
				if (type == Chart.ChartTypes.Stock)
				{
					series.YValuesPerPoint = 4;
				}
			}
			else
			{
				series.YValuesPerPoint = 2;
			}
			this.m_dundasChart.Series.Add(series);
			bool flag2 = true;
			for (int i = 0; i < categoryCount; i++)
			{
				this.HatchStyleIncrement(memberDataPointIndex, i);
				this.RenderDataPoint(ref flag2, flag, series, seriesHeader, dataPoints[memberDataPointIndex, i], this.m_categoryLabels[i], this.EncodeDataPointID(memberDataPointIndex, i), memberDataPointIndex);
				if (this.m_populateSeriesIndexes && i == categoryCount - 1 && flag2)
				{
					this.m_positiveSeriesIndexes.Add(memberDataPointIndex);
				}
			}
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x00017B4D File Offset: 0x00015D4D
		private int EncodeDataPointID(int seriesIndex, int categoryIndex)
		{
			return seriesIndex * this.m_owner.DataPointCollection.CategoryCount + categoryIndex;
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x00017B63 File Offset: 0x00015D63
		private void DecodeDataPointID(int id, out int seriesIndex, out int categoryIndex)
		{
			Global.Tracer.Assert(id >= 0);
			categoryIndex = id % this.m_owner.DataPointCollection.CategoryCount;
			seriesIndex = id / this.m_owner.DataPointCollection.CategoryCount;
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x00017BA0 File Offset: 0x00015DA0
		private void RenderDataPoint(ref bool firstValidDataPointOfSeries, bool isLineChart, Series series, ChartMember seriesHeader, ChartDataPoint dataPoint, object axisLabel, int dataPointID, int seriesIndex)
		{
			DataPoint dataPoint2 = new DataPoint(series);
			Color empty = Color.Empty;
			if (dataPoint == null || dataPoint.InstanceInfo == null)
			{
				if (axisLabel == null)
				{
					dataPoint2.EmptyX = true;
				}
				else
				{
					dataPoint2.SetValueXY(axisLabel, new object[] { 0 });
				}
				dataPoint2.Empty = true;
				series.Points.Add(dataPoint2);
				return;
			}
			ChartDataPoint chartDataPoint = this.m_chart.ChartDataPoints[dataPoint.InstanceInfo.DataPointIndex];
			dataPoint2.ElementID = dataPointID;
			if (chartDataPoint.Action != null)
			{
				dataPoint2.Href = "X";
				dataPoint2.MapAreaID = dataPointID + this.c_offset;
			}
			if (chartDataPoint.DataLabel != null && chartDataPoint.DataLabel.Visible)
			{
				dataPoint2.Font = this.GetFont(chartDataPoint.DataLabel.StyleClass, dataPoint.InstanceInfo.DataLabelStyleAttributeValues);
				dataPoint2.FontColor = this.GetTextColor(chartDataPoint.DataLabel.StyleClass, dataPoint.InstanceInfo.DataLabelStyleAttributeValues);
				dataPoint2.FontAngle = chartDataPoint.DataLabel.Rotation;
				dataPoint2.LabelFormat = this.GetFormatCode(chartDataPoint.DataLabel.StyleClass, dataPoint.InstanceInfo.DataLabelStyleAttributeValues);
				if (Chart.ChartTypes.Pie == this.m_chart.Type || Chart.ChartTypes.Doughnut == this.m_chart.Type)
				{
					if (chartDataPoint.DataLabel.Position == ChartDataLabel.Positions.Auto || ChartDataLabel.Positions.Center == chartDataPoint.DataLabel.Position)
					{
						dataPoint2["LabelStyle"] = "Inside";
					}
					else
					{
						dataPoint2["LabelStyle"] = "Outside";
					}
				}
				else if (chartDataPoint.DataLabel.Position != ChartDataLabel.Positions.Auto)
				{
					dataPoint2["LabelStyle"] = chartDataPoint.DataLabel.Position.ToString();
				}
				else
				{
					series.SmartLabels.Enabled = true;
					series.SmartLabels.AllowOutsidePlotArea = 1;
					series.SmartLabels.CalloutLineStyle = 5;
					series.SmartLabels.CalloutLineColor = Color.FromArgb(140, 0, 0, 0);
					series.SmartLabels.CalloutLineWidth = 1;
				}
				string text = null;
				if (chartDataPoint.DataLabel.Value != null)
				{
					if (ExpressionInfo.Types.Constant == chartDataPoint.DataLabel.Value.Type)
					{
						text = chartDataPoint.DataLabel.Value.Value;
					}
					else
					{
						text = dataPoint.InstanceInfo.DataLabelValue;
					}
				}
				if (text == null)
				{
					dataPoint2.ShowLabelAsValue = true;
				}
				else
				{
					dataPoint2.Label = text;
				}
			}
			else if (firstValidDataPointOfSeries && (Chart.ChartTypes.Pie == this.m_chart.Type || Chart.ChartTypes.Doughnut == this.m_chart.Type))
			{
				series["LabelStyle"] = "Disabled";
			}
			if (this.IsExploded)
			{
				dataPoint2["Exploded"] = "true";
			}
			if (firstValidDataPointOfSeries && ChartDataPoint.MarkerTypes.Auto == chartDataPoint.MarkerType)
			{
				this.GetNextSeriesMarkerStyle();
			}
			if (chartDataPoint.MarkerType != ChartDataPoint.MarkerTypes.None)
			{
				double num = 0.0;
				if (chartDataPoint.MarkerSize != null)
				{
					Validator.ParseSize(chartDataPoint.MarkerSize, out num);
				}
				dataPoint2.MarkerSize = (int)Math.Round(num * 96.0 * 0.03937007874);
				if (ChartDataPoint.MarkerTypes.Auto == chartDataPoint.MarkerType)
				{
					dataPoint2.MarkerStyle = this.m_currentAutoMarkerStyle;
				}
				else
				{
					dataPoint2.MarkerStyle = this.DundasMarkerStyle(chartDataPoint.MarkerType);
				}
				if (firstValidDataPointOfSeries)
				{
					series.MarkerStyle = dataPoint2.MarkerStyle;
					series.MarkerSize = dataPoint2.MarkerSize;
				}
				if (this.SetDataPointColorProperty(Microsoft.ReportingServices.ReportRendering.Style.GetStyleValue("BackgroundColor", chartDataPoint.MarkerStyleClass, dataPoint.InstanceInfo.MarkerStyleAttributeValues, false), out empty))
				{
					dataPoint2.MarkerColor = empty;
					if (firstValidDataPointOfSeries)
					{
						series.MarkerColor = empty;
					}
				}
				if (this.SetDataPointColorProperty(Microsoft.ReportingServices.ReportRendering.Style.GetStyleValue("BorderColor", chartDataPoint.MarkerStyleClass, dataPoint.InstanceInfo.MarkerStyleAttributeValues, false), out empty))
				{
					dataPoint2.MarkerBorderColor = empty;
					if (firstValidDataPointOfSeries)
					{
						series.MarkerBorderColor = empty;
					}
				}
			}
			bool flag = true;
			bool flag2 = false;
			string text2 = (isLineChart ? "BorderColor" : "BackgroundColor");
			if (this.SetDataPointColorProperty(Microsoft.ReportingServices.ReportRendering.Style.GetStyleValue(text2, chartDataPoint.StyleClass, dataPoint.InstanceInfo.StyleAttributeValues, false), out empty))
			{
				flag = false;
				dataPoint2.Color = empty;
				if (firstValidDataPointOfSeries)
				{
					series.Color = empty;
				}
			}
			if (this.SetDataPointColorProperty(Microsoft.ReportingServices.ReportRendering.Style.GetStyleValue("BackgroundGradientEndColor", chartDataPoint.StyleClass, dataPoint.InstanceInfo.StyleAttributeValues, false), out empty))
			{
				flag2 = true;
				dataPoint2.BackGradientEndColor = empty;
				if (firstValidDataPointOfSeries)
				{
					series.BackGradientEndColor = empty;
				}
			}
			dataPoint2.BackGradientType = this.GetGradientType(chartDataPoint.StyleClass, dataPoint.InstanceInfo.StyleAttributeValues);
			if (firstValidDataPointOfSeries)
			{
				series.BackGradientType = dataPoint2.BackGradientType;
			}
			if (dataPoint2.BackGradientType != null)
			{
				flag = false;
			}
			if (flag)
			{
				dataPoint2.BackHatchStyle = this.GetCurrentHatchStyle();
				if (firstValidDataPointOfSeries)
				{
					series.BackHatchStyle = this.GetCurrentHatchStyle();
					if (!flag2)
					{
						if (Chart.ChartPalette.GrayScale == this.m_chart.Palette)
						{
							series.BackGradientEndColor = Color.DimGray;
						}
						else
						{
							series.BackGradientEndColor = Color.Black;
						}
					}
				}
			}
			if (this.SetDataPointColorProperty(Microsoft.ReportingServices.ReportRendering.Style.GetStyleValue("BorderColor", chartDataPoint.StyleClass, dataPoint.InstanceInfo.StyleAttributeValues, false), out empty))
			{
				dataPoint2.BorderColor = empty;
				if (firstValidDataPointOfSeries)
				{
					series.BorderColor = empty;
				}
			}
			ReportSize reportSize = Microsoft.ReportingServices.ReportRendering.Style.GetStyleValue("BorderWidth", chartDataPoint.StyleClass, dataPoint.InstanceInfo.StyleAttributeValues, false) as ReportSize;
			if (reportSize != null)
			{
				dataPoint2.BorderWidth = (float)reportSize.ToPoints();
				if (firstValidDataPointOfSeries)
				{
					series.BorderWidth = dataPoint2.BorderWidth;
				}
			}
			bool flag3;
			ChartDashStyle borderStyle = this.GetBorderStyle(chartDataPoint.StyleClass, dataPoint.InstanceInfo.StyleAttributeValues, false, out flag3);
			if (flag3)
			{
				dataPoint2.BorderStyle = borderStyle;
				if (firstValidDataPointOfSeries)
				{
					series.BorderStyle = borderStyle;
				}
			}
			if (dataPoint.InstanceInfo.DataValues == null || (axisLabel == null && this.AxisLabelRequiredForRendering(this.m_chart.Type, this.m_isScalarMode)))
			{
				if (axisLabel == null)
				{
					dataPoint2.EmptyX = true;
				}
				else
				{
					dataPoint2.SetValueXY(axisLabel, new object[] { 0 });
				}
				dataPoint2.Empty = true;
				dataPoint2.MapAreaID *= -1;
				series.Points.Add(dataPoint2);
				return;
			}
			bool flag4 = false;
			switch (this.m_chart.Type)
			{
			case Chart.ChartTypes.Column:
			case Chart.ChartTypes.Bar:
			case Chart.ChartTypes.Line:
			case Chart.ChartTypes.Area:
				Global.Tracer.Assert(series.YValuesPerPoint <= dataPoint.InstanceInfo.DataValues.Length);
				flag4 = this.ValidateAndAddDataPoint(this.m_chart.Type, series, dataPoint2, this.m_isScalarMode, axisLabel, series.YValuesPerPoint, dataPoint.InstanceInfo.DataValues);
				break;
			case Chart.ChartTypes.Pie:
			case Chart.ChartTypes.Doughnut:
				Global.Tracer.Assert(series.YValuesPerPoint <= dataPoint.InstanceInfo.DataValues.Length);
				flag4 = this.ValidateAndAddDataPoint(this.m_chart.Type, series, dataPoint2, this.m_isScalarMode, axisLabel, series.YValuesPerPoint, dataPoint.InstanceInfo.DataValues);
				break;
			case Chart.ChartTypes.Scatter:
				Global.Tracer.Assert(series.YValuesPerPoint < dataPoint.InstanceInfo.DataValues.Length);
				flag4 = this.ValidateAndAddDataPoint(this.m_chart.Type, series, dataPoint2, this.m_isScalarMode, axisLabel, 2, dataPoint.InstanceInfo.DataValues);
				break;
			case Chart.ChartTypes.Bubble:
				Global.Tracer.Assert(series.YValuesPerPoint < dataPoint.InstanceInfo.DataValues.Length);
				flag4 = this.ValidateAndAddDataPoint(this.m_chart.Type, series, dataPoint2, this.m_isScalarMode, axisLabel, 3, dataPoint.InstanceInfo.DataValues);
				break;
			case Chart.ChartTypes.Stock:
				if (Chart.ChartSubTypes.HighLowClose == this.m_chart.SubType)
				{
					Global.Tracer.Assert(series.YValuesPerPoint <= dataPoint.InstanceInfo.DataValues.Length + 1);
					flag4 = this.ValidateAndAddDataPoint(this.m_chart.Type, series, dataPoint2, this.m_isScalarMode, axisLabel, 3, dataPoint.InstanceInfo.DataValues);
				}
				else
				{
					Global.Tracer.Assert(series.YValuesPerPoint <= dataPoint.InstanceInfo.DataValues.Length);
					flag4 = this.ValidateAndAddDataPoint(this.m_chart.Type, series, dataPoint2, this.m_isScalarMode, axisLabel, 4, dataPoint.InstanceInfo.DataValues);
				}
				break;
			}
			if (flag4 & firstValidDataPointOfSeries)
			{
				firstValidDataPointOfSeries = false;
				if (this.m_populateSeriesIndexes)
				{
					Global.Tracer.Assert(dataPoint.InstanceInfo.DataValues != null && dataPoint.InstanceInfo.DataValues.Length != 0);
					bool flag5;
					int num2 = ReportProcessing.CompareTo(dataPoint.InstanceInfo.DataValues[0], 0, false, this.m_compareInfo, CompareOptions.None, false, false, out flag5);
					if (flag5 && num2 < 0)
					{
						this.m_negativeSeriesIndexes.Add(seriesIndex);
						return;
					}
					this.m_positiveSeriesIndexes.Add(seriesIndex);
				}
			}
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00018478 File Offset: 0x00016678
		private bool ValidateAndAddDataPoint(Chart.ChartTypes type, Series series, DataPoint point, bool isScalarMode, object axisLabel, int count, object[] dataValues)
		{
			Global.Tracer.Assert(series != null);
			Global.Tracer.Assert(point != null);
			Global.Tracer.Assert(dataValues != null);
			bool flag = true;
			if (dataValues.Length < count)
			{
				flag = false;
			}
			int num = 0;
			while (num < count && flag)
			{
				if (dataValues[num] == null)
				{
					flag = false;
				}
				num++;
			}
			if (axisLabel == null && !isScalarMode && this.ChartTypeWithExplicitXValue(this.m_chart.Type))
			{
				axisLabel = dataValues[0];
			}
			if (flag)
			{
				if (this.m_X_axisType == null)
				{
					ChartValueTypes chartValueTypes = this.DetermineDatatype(this.ChartTypeWithExplicitXValue(this.m_chart.Type) ? dataValues[0] : axisLabel);
					if (isScalarMode && chartValueTypes == 3 && !this.XAxisPropertiesHasIntValue())
					{
						this.m_X_axisType = 0;
					}
					else
					{
						this.m_X_axisType = chartValueTypes;
					}
				}
				if (this.m_Y_axisType == null)
				{
					this.m_Y_axisType = this.DetermineDatatype(this.ChartTypeWithExplicitXValue(this.m_chart.Type) ? dataValues[1] : dataValues[0]);
				}
			}
			if (axisLabel == null)
			{
				axisLabel = " ";
				if (this.AxisLabelRequiredForRendering(this.m_chart.Type, isScalarMode))
				{
					point.EmptyX = true;
				}
			}
			if (!isScalarMode)
			{
				if (axisLabel is string)
				{
					point.AxisLabel = (string)axisLabel;
				}
				else
				{
					string text = null;
					if (this.m_chart.CategoryAxis != null && this.m_chart.CategoryAxis.StyleClass != null)
					{
						text = (string)Microsoft.ReportingServices.ReportRendering.Style.GetStyleValue("Format", this.m_chart.CategoryAxis.StyleClass, this.ChartInstanceInfo.CategoryAxis.StyleAttributeValues);
					}
					this.GenerateCategoryString(axisLabel, text);
				}
			}
			if (Chart.ChartTypes.Pie == this.m_chart.Type || Chart.ChartTypes.Doughnut == this.m_chart.Type)
			{
				point.LegendText = point.AxisLabel;
			}
			if (flag)
			{
				switch (count)
				{
				case 1:
					if (isScalarMode)
					{
						point.SetValueXY(axisLabel, new object[] { dataValues[0] });
					}
					else
					{
						point.SetValueY(new object[] { dataValues[0] });
					}
					break;
				case 2:
					Global.Tracer.Assert(flag);
					if (Chart.ChartTypes.Scatter == type)
					{
						point.SetValueXY(dataValues[0], new object[] { dataValues[1] });
					}
					break;
				case 3:
					Global.Tracer.Assert(flag);
					if (Chart.ChartTypes.Bubble == type)
					{
						point.SetValueXY(dataValues[0], new object[]
						{
							dataValues[1],
							dataValues[2]
						});
					}
					else if (Chart.ChartTypes.Stock == type)
					{
						if (isScalarMode)
						{
							point.SetValueXY(axisLabel, new object[]
							{
								dataValues[0],
								dataValues[1],
								dataValues[2],
								dataValues[2]
							});
						}
						else
						{
							point.SetValueY(new object[]
							{
								dataValues[0],
								dataValues[1],
								dataValues[2],
								dataValues[2]
							});
						}
					}
					break;
				case 4:
					Global.Tracer.Assert(flag);
					if (Chart.ChartTypes.Stock == type)
					{
						if (isScalarMode)
						{
							point.SetValueXY(axisLabel, new object[]
							{
								dataValues[0],
								dataValues[1],
								dataValues[2],
								dataValues[3]
							});
						}
						else
						{
							point.SetValueY(new object[]
							{
								dataValues[0],
								dataValues[1],
								dataValues[2],
								dataValues[3]
							});
						}
					}
					break;
				default:
					Global.Tracer.Assert(false, string.Empty);
					break;
				}
				series.Points.Add(point);
				return true;
			}
			if ((Chart.ChartSubTypes.Stacked != this.m_chart.SubType && Chart.ChartSubTypes.PercentStacked != this.m_chart.SubType && (Chart.ChartTypes.Area == type && isScalarMode)) || Chart.ChartTypes.Scatter == type || (Chart.ChartTypes.Line == type && isScalarMode))
			{
				return false;
			}
			if (!point.EmptyX && axisLabel != null)
			{
				point.SetValueXY(axisLabel, new object[] { 0 });
			}
			point.Empty = true;
			point.MapAreaID *= -1;
			series.Points.Add(point);
			return false;
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x00018860 File Offset: 0x00016A60
		private void GenerateFlattenedCategoryLabels(ChartMember category, int index, string flattenedCategoryLabel)
		{
			object memberLabel = category.MemberLabel;
			string text = ((memberLabel == null) ? ("Category " + index.ToString(CultureInfo.InvariantCulture)) : memberLabel.ToString());
			if (flattenedCategoryLabel.Length > 0)
			{
				flattenedCategoryLabel = flattenedCategoryLabel + " - " + text;
			}
			else
			{
				flattenedCategoryLabel = text;
			}
			if (category.IsInnerMostMember)
			{
				this.m_categoryLabels.Add(flattenedCategoryLabel);
				return;
			}
			ChartMemberCollection children = category.Children;
			Global.Tracer.Assert(children != null);
			for (int i = 0; i < children.Count; i++)
			{
				this.GenerateFlattenedCategoryLabels(children[i], i + 1, flattenedCategoryLabel);
			}
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x00018900 File Offset: 0x00016B00
		private void GenerateCategoryLabels(int level, ChartMember category, bool generateGroupingLabels, string categoryFormatString, CustomLabelsCollection groupingLabels)
		{
			object memberLabel = category.MemberLabel;
			if (!category.IsInnerMostMember)
			{
				ChartMemberCollection children = category.Children;
				Global.Tracer.Assert(children != null);
				if (generateGroupingLabels && memberLabel != null)
				{
					groupingLabels.Add(0.6 + (double)category.MemberDataPointIndex, 0.4 + ((double)category.MemberDataPointIndex + (double)category.MemberHeadingSpan), this.GenerateCategoryString(memberLabel, categoryFormatString), this.m_owner.CategoryGroupingLevels - level, 2);
				}
				for (int i = 0; i < children.Count; i++)
				{
					this.GenerateCategoryLabels(level + 1, children[i], generateGroupingLabels, categoryFormatString, groupingLabels);
				}
				return;
			}
			if (this.m_isScalarMode)
			{
				this.m_categoryLabels.Add(this.GenerateCategoryScalar(memberLabel));
				return;
			}
			this.m_categoryLabels.Add(this.GenerateCategoryString(memberLabel, categoryFormatString));
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x000189D8 File Offset: 0x00016BD8
		private object GenerateCategoryScalar(object categoryValue)
		{
			object obj = 0.0;
			if (categoryValue == null)
			{
				return null;
			}
			if (categoryValue is string)
			{
				double num;
				if (!double.TryParse((string)categoryValue, NumberStyles.Any, CultureInfo.CurrentCulture.NumberFormat, out num))
				{
					Global.Tracer.Trace(TraceLevel.Error, "Could not convert category axis value (based on grouping) into a scalar value.");
					return null;
				}
				obj = num;
			}
			else
			{
				obj = categoryValue;
			}
			return obj;
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x00018A40 File Offset: 0x00016C40
		private string GenerateCategoryString(object categoryValue, string categoryFormatString)
		{
			if (categoryValue == null)
			{
				Global.Tracer.Trace(TraceLevel.Warning, "The grouping expression for a category grouping returned a null value.");
				return null;
			}
			ChartValueTypes chartValueTypes = this.DetermineDatatype(categoryValue);
			if (8 == chartValueTypes)
			{
				return this.RenderNumbers(null, ((DateTime)categoryValue).ToOADate(), categoryFormatString, chartValueTypes, -1, 10);
			}
			if (3 == chartValueTypes)
			{
				return this.RenderNumbers(null, ((IConvertible)categoryValue).ToDouble(this.m_currentCulture), categoryFormatString, chartValueTypes, -1, 10);
			}
			if (1 == chartValueTypes)
			{
				if (categoryValue is double)
				{
					return this.RenderNumbers(null, (double)categoryValue, categoryFormatString, chartValueTypes, -1, 10);
				}
				return this.RenderNumbers(null, ((IConvertible)categoryValue).ToDouble(this.m_currentCulture), categoryFormatString, chartValueTypes, -1, 10);
			}
			else
			{
				if (categoryValue is IFormattable)
				{
					return ((IFormattable)categoryValue).ToString(categoryFormatString, Thread.CurrentThread.CurrentCulture);
				}
				return categoryValue.ToString();
			}
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x00018B10 File Offset: 0x00016D10
		private ChartValueTypes DetermineDatatype(object value)
		{
			DataAggregate.DataTypeCode dataTypeCode;
			return this.DetermineDatatype(value, out dataTypeCode);
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00018B26 File Offset: 0x00016D26
		private ChartValueTypes DetermineDatatype(object value, out DataAggregate.DataTypeCode typeCode)
		{
			typeCode = DataAggregate.DataTypeCode.Null;
			if (value == null)
			{
				return 0;
			}
			typeCode = DataAggregate.GetTypeCode(value);
			if (DataAggregate.DataTypeCode.Decimal == typeCode || DataAggregate.DataTypeCode.Double == typeCode || DataAggregate.DataTypeCode.Single == typeCode)
			{
				return 1;
			}
			if (DataAggregate.DataTypeCode.DateTime == typeCode)
			{
				return 8;
			}
			if (DataTypeUtility.IsNumeric(typeCode))
			{
				return 3;
			}
			return 0;
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00018B60 File Offset: 0x00016D60
		private bool XAxisPropertiesHasIntValue()
		{
			if (!this.m_X_AxisTypeDetermined)
			{
				this.m_X_AxisTypeDetermined = true;
				Axis categoryAxis = this.m_chart.CategoryAxis;
				this.m_X_AxisHasIntValue = (!categoryAxis.AutoCrossAt && DundasChart.DetectAndConvertAxisValue(categoryAxis.CrossAt, null) is int) || (!categoryAxis.AutoScaleMin && DundasChart.DetectAndConvertAxisValue(categoryAxis.Min, null) is int) || (!categoryAxis.AutoScaleMax && DundasChart.DetectAndConvertAxisValue(categoryAxis.Max, null) is int) || DundasChart.DetectAndConvertAxisValue(categoryAxis.MajorInterval, null) is int || DundasChart.DetectAndConvertAxisValue(categoryAxis.MinorInterval, null) is int;
			}
			return this.m_X_AxisHasIntValue;
		}

		// Token: 0x0400018C RID: 396
		private Chart m_dundasChart;

		// Token: 0x0400018D RID: 397
		private Chart m_owner;

		// Token: 0x0400018E RID: 398
		private Chart m_chart;

		// Token: 0x0400018F RID: 399
		private ChartInstanceInfo m_chartInstanceInfo;

		// Token: 0x04000190 RID: 400
		private ChartValueTypes m_X_axisType;

		// Token: 0x04000191 RID: 401
		private ChartValueTypes m_Y_axisType;

		// Token: 0x04000192 RID: 402
		private bool m_X_AxisHasIntValue;

		// Token: 0x04000193 RID: 403
		private bool m_X_AxisTypeDetermined;

		// Token: 0x04000194 RID: 404
		private bool m_isScalarMode;

		// Token: 0x04000195 RID: 405
		private ArrayList m_categoryLabels;

		// Token: 0x04000196 RID: 406
		private int m_hatchPatternIndex = -1;

		// Token: 0x04000197 RID: 407
		private int m_uniquePaletteColors;

		// Token: 0x04000198 RID: 408
		private int m_markerStyleIndex;

		// Token: 0x04000199 RID: 409
		private MarkerStyle m_currentAutoMarkerStyle;

		// Token: 0x0400019A RID: 410
		private CompareInfo m_compareInfo;

		// Token: 0x0400019B RID: 411
		private bool m_populateSeriesIndexes;

		// Token: 0x0400019C RID: 412
		private IntList m_positiveSeriesIndexes;

		// Token: 0x0400019D RID: 413
		private IntList m_negativeSeriesIndexes;

		// Token: 0x0400019E RID: 414
		private MapAreasCollection m_mapAreasCollection;

		// Token: 0x0400019F RID: 415
		private CultureInfo m_currentCulture;

		// Token: 0x040001A0 RID: 416
		private CultureInfo m_numeralCulture;

		// Token: 0x040001A1 RID: 417
		private int c_offset = 10;

		// Token: 0x02000913 RID: 2323
		private enum YearConvertMode
		{
			// Token: 0x04003EFC RID: 16124
			Min,
			// Token: 0x04003EFD RID: 16125
			MidYear,
			// Token: 0x04003EFE RID: 16126
			Max,
			// Token: 0x04003EFF RID: 16127
			None
		}
	}
}
