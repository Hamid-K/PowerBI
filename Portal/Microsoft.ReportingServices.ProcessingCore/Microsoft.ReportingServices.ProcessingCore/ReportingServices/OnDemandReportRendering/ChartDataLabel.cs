using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200022D RID: 557
	public sealed class ChartDataLabel : IROMStyleDefinitionContainer, IROMActionOwner
	{
		// Token: 0x06001542 RID: 5442 RVA: 0x00055AB5 File Offset: 0x00053CB5
		internal ChartDataLabel(Microsoft.ReportingServices.OnDemandReportRendering.ChartDataPoint dataPoint, Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataLabel chartDataLabelDef, Microsoft.ReportingServices.OnDemandReportRendering.Chart chart)
		{
			this.m_dataPoint = dataPoint;
			this.m_chartDataLabelDef = chartDataLabelDef;
			this.m_chart = chart;
		}

		// Token: 0x06001543 RID: 5443 RVA: 0x00055AD2 File Offset: 0x00053CD2
		internal ChartDataLabel(InternalChartSeries series, Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataLabel chartDataLabelDef, Microsoft.ReportingServices.OnDemandReportRendering.Chart chart)
		{
			this.m_chartSeries = series;
			this.m_chartDataLabelDef = chartDataLabelDef;
			this.m_chart = chart;
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x00055AEF File Offset: 0x00053CEF
		internal ChartDataLabel(Microsoft.ReportingServices.OnDemandReportRendering.ChartDataPoint dataPoint, Microsoft.ReportingServices.OnDemandReportRendering.Chart chart)
		{
			this.m_dataPoint = dataPoint;
			this.m_chart = chart;
		}

		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x06001545 RID: 5445 RVA: 0x00055B08 File Offset: 0x00053D08
		public Microsoft.ReportingServices.OnDemandReportRendering.Style Style
		{
			get
			{
				if (this.m_style == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						this.m_style = new Microsoft.ReportingServices.OnDemandReportRendering.Style(this.m_dataPoint.RenderDataPointDef.DataLabel.StyleClass, this.m_dataPoint.RenderItem.InstanceInfo.DataLabelStyleAttributeValues, this.m_chart.RenderingContext);
					}
					else if (this.m_chartDataLabelDef.StyleClass != null)
					{
						this.m_style = new Microsoft.ReportingServices.OnDemandReportRendering.Style(this.m_chart, this.ReportScope, this.m_chartDataLabelDef, this.m_chart.RenderingContext);
					}
				}
				return this.m_style;
			}
		}

		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x06001546 RID: 5446 RVA: 0x00055BAA File Offset: 0x00053DAA
		public string UniqueName
		{
			get
			{
				return this.InstancePath.UniqueName + "xDataLabel";
			}
		}

		// Token: 0x17000B83 RID: 2947
		// (get) Token: 0x06001547 RID: 5447 RVA: 0x00055BC4 File Offset: 0x00053DC4
		public ActionInfo ActionInfo
		{
			get
			{
				if (this.m_actionInfo == null && !this.m_chart.IsOldSnapshot && this.m_chartDataLabelDef.Action != null)
				{
					this.m_actionInfo = new ActionInfo(this.m_chart.RenderingContext, this.ReportScope, this.m_chartDataLabelDef.Action, this.InstancePath, this.m_chart, ObjectType.Chart, this.m_chartDataLabelDef.Name, this);
				}
				return this.m_actionInfo;
			}
		}

		// Token: 0x17000B84 RID: 2948
		// (get) Token: 0x06001548 RID: 5448 RVA: 0x00055C3A File Offset: 0x00053E3A
		public List<string> FieldsUsedInValueExpression
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x06001549 RID: 5449 RVA: 0x00055C40 File Offset: 0x00053E40
		public ReportStringProperty Label
		{
			get
			{
				if (this.m_value == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						this.m_value = new ReportStringProperty(this.m_dataPoint.RenderDataPointDef.DataLabel.Value);
					}
					else if (this.m_chartDataLabelDef.Label != null)
					{
						this.m_value = new ReportStringProperty(this.m_chartDataLabelDef.Label);
					}
				}
				return this.m_value;
			}
		}

		// Token: 0x17000B86 RID: 2950
		// (get) Token: 0x0600154A RID: 5450 RVA: 0x00055CAD File Offset: 0x00053EAD
		public ReportBoolProperty UseValueAsLabel
		{
			get
			{
				if (this.m_useValueAsLabel == null && !this.m_chart.IsOldSnapshot && this.m_chartDataLabelDef.UseValueAsLabel != null)
				{
					this.m_useValueAsLabel = new ReportBoolProperty(this.m_chartDataLabelDef.UseValueAsLabel);
				}
				return this.m_useValueAsLabel;
			}
		}

		// Token: 0x17000B87 RID: 2951
		// (get) Token: 0x0600154B RID: 5451 RVA: 0x00055CF0 File Offset: 0x00053EF0
		public ReportBoolProperty Visible
		{
			get
			{
				if (this.m_visible == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						this.m_visible = new ReportBoolProperty(this.m_dataPoint.RenderDataPointDef.DataLabel.Visible);
					}
					else if (this.m_chartDataLabelDef.Visible != null)
					{
						this.m_visible = new ReportBoolProperty(this.m_chartDataLabelDef.Visible);
					}
				}
				return this.m_visible;
			}
		}

		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x0600154C RID: 5452 RVA: 0x00055D60 File Offset: 0x00053F60
		public ReportIntProperty Rotation
		{
			get
			{
				if (this.m_rotation == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						this.m_rotation = new ReportIntProperty(this.m_dataPoint.RenderDataPointDef.DataLabel.Rotation);
					}
					else if (this.m_chartDataLabelDef.Rotation != null)
					{
						this.m_rotation = new ReportIntProperty(this.m_chartDataLabelDef.Rotation);
					}
				}
				return this.m_rotation;
			}
		}

		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x0600154D RID: 5453 RVA: 0x00055DCD File Offset: 0x00053FCD
		public ReportStringProperty ToolTip
		{
			get
			{
				if (this.m_toolTip == null && !this.m_chart.IsOldSnapshot && this.m_chartDataLabelDef.ToolTip != null)
				{
					this.m_toolTip = new ReportStringProperty(this.m_chartDataLabelDef.ToolTip);
				}
				return this.m_toolTip;
			}
		}

		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x0600154E RID: 5454 RVA: 0x00055E10 File Offset: 0x00054010
		public ReportEnumProperty<ChartDataLabelPositions> Position
		{
			get
			{
				if (this.m_chart.IsOldSnapshot)
				{
					ChartDataLabelPositions chartDataLabelPositions = ChartDataLabelPositions.Auto;
					switch (this.m_dataPoint.RenderDataPointDef.DataLabel.Position)
					{
					case Microsoft.ReportingServices.ReportProcessing.ChartDataLabel.Positions.Auto:
						chartDataLabelPositions = ChartDataLabelPositions.Auto;
						break;
					case Microsoft.ReportingServices.ReportProcessing.ChartDataLabel.Positions.Top:
						chartDataLabelPositions = ChartDataLabelPositions.Top;
						break;
					case Microsoft.ReportingServices.ReportProcessing.ChartDataLabel.Positions.TopLeft:
						chartDataLabelPositions = ChartDataLabelPositions.TopLeft;
						break;
					case Microsoft.ReportingServices.ReportProcessing.ChartDataLabel.Positions.TopRight:
						chartDataLabelPositions = ChartDataLabelPositions.TopRight;
						break;
					case Microsoft.ReportingServices.ReportProcessing.ChartDataLabel.Positions.Left:
						chartDataLabelPositions = ChartDataLabelPositions.Left;
						break;
					case Microsoft.ReportingServices.ReportProcessing.ChartDataLabel.Positions.Center:
						chartDataLabelPositions = ChartDataLabelPositions.Center;
						break;
					case Microsoft.ReportingServices.ReportProcessing.ChartDataLabel.Positions.Right:
						chartDataLabelPositions = ChartDataLabelPositions.Right;
						break;
					case Microsoft.ReportingServices.ReportProcessing.ChartDataLabel.Positions.BottomRight:
						chartDataLabelPositions = ChartDataLabelPositions.BottomRight;
						break;
					case Microsoft.ReportingServices.ReportProcessing.ChartDataLabel.Positions.Bottom:
						chartDataLabelPositions = ChartDataLabelPositions.Bottom;
						break;
					case Microsoft.ReportingServices.ReportProcessing.ChartDataLabel.Positions.BottomLeft:
						chartDataLabelPositions = ChartDataLabelPositions.BottomLeft;
						break;
					}
					this.m_position = new ReportEnumProperty<ChartDataLabelPositions>(chartDataLabelPositions);
				}
				else if (this.m_chartDataLabelDef.Position != null)
				{
					this.m_position = new ReportEnumProperty<ChartDataLabelPositions>(this.m_chartDataLabelDef.Position.IsExpression, this.m_chartDataLabelDef.Position.OriginalText, EnumTranslator.TranslateChartDataLabelPosition(this.m_chartDataLabelDef.Position.StringValue, null));
				}
				return this.m_position;
			}
		}

		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x0600154F RID: 5455 RVA: 0x00055EFB File Offset: 0x000540FB
		internal IReportScope ReportScope
		{
			get
			{
				if (this.m_dataPoint != null)
				{
					return this.m_dataPoint;
				}
				if (this.m_chartSeries != null)
				{
					return this.m_chartSeries.ReportScope;
				}
				return this.m_chart;
			}
		}

		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x06001550 RID: 5456 RVA: 0x00055F26 File Offset: 0x00054126
		private IInstancePath InstancePath
		{
			get
			{
				if (this.m_dataPoint != null)
				{
					return this.m_dataPoint.DataPointDef;
				}
				if (this.m_chartSeries != null)
				{
					return this.m_chartSeries.ChartSeriesDef;
				}
				return this.m_chart.ChartDef;
			}
		}

		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x06001551 RID: 5457 RVA: 0x00055F5B File Offset: 0x0005415B
		internal Microsoft.ReportingServices.OnDemandReportRendering.Chart ChartDef
		{
			get
			{
				return this.m_chart;
			}
		}

		// Token: 0x17000B8E RID: 2958
		// (get) Token: 0x06001552 RID: 5458 RVA: 0x00055F63 File Offset: 0x00054163
		internal Microsoft.ReportingServices.OnDemandReportRendering.ChartDataPoint ChartDataPoint
		{
			get
			{
				return this.m_dataPoint;
			}
		}

		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x06001553 RID: 5459 RVA: 0x00055F6B File Offset: 0x0005416B
		internal Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataLabel ChartDataLabelDef
		{
			get
			{
				return this.m_chartDataLabelDef;
			}
		}

		// Token: 0x17000B90 RID: 2960
		// (get) Token: 0x06001554 RID: 5460 RVA: 0x00055F73 File Offset: 0x00054173
		public ChartDataLabelInstance Instance
		{
			get
			{
				if (this.m_chart.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new ChartDataLabelInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x00055FA3 File Offset: 0x000541A3
		internal void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
			if (this.m_actionInfo != null)
			{
				this.m_actionInfo.SetNewContext();
			}
		}

		// Token: 0x040009F9 RID: 2553
		private Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataLabel m_chartDataLabelDef;

		// Token: 0x040009FA RID: 2554
		private Microsoft.ReportingServices.OnDemandReportRendering.Chart m_chart;

		// Token: 0x040009FB RID: 2555
		private ChartDataLabelInstance m_instance;

		// Token: 0x040009FC RID: 2556
		private Microsoft.ReportingServices.OnDemandReportRendering.Style m_style;

		// Token: 0x040009FD RID: 2557
		private ReportStringProperty m_value;

		// Token: 0x040009FE RID: 2558
		private Microsoft.ReportingServices.OnDemandReportRendering.ChartDataPoint m_dataPoint;

		// Token: 0x040009FF RID: 2559
		private InternalChartSeries m_chartSeries;

		// Token: 0x04000A00 RID: 2560
		private ReportBoolProperty m_visible;

		// Token: 0x04000A01 RID: 2561
		private ReportEnumProperty<ChartDataLabelPositions> m_position;

		// Token: 0x04000A02 RID: 2562
		private ReportIntProperty m_rotation;

		// Token: 0x04000A03 RID: 2563
		private ActionInfo m_actionInfo;

		// Token: 0x04000A04 RID: 2564
		private ReportBoolProperty m_useValueAsLabel;

		// Token: 0x04000A05 RID: 2565
		private ReportStringProperty m_toolTip;
	}
}
