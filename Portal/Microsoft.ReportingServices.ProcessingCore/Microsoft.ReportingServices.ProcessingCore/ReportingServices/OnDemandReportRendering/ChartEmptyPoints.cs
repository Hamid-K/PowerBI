using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000246 RID: 582
	public sealed class ChartEmptyPoints : IROMStyleDefinitionContainer, IROMActionOwner
	{
		// Token: 0x0600167A RID: 5754 RVA: 0x0005A94C File Offset: 0x00058B4C
		internal ChartEmptyPoints(InternalChartSeries chartSeries, ChartEmptyPoints chartEmptyPointsDef, Microsoft.ReportingServices.OnDemandReportRendering.Chart chart)
		{
			this.m_chartEmptyPointsDef = chartEmptyPointsDef;
			this.m_chart = chart;
			this.m_chartSeries = chartSeries;
		}

		// Token: 0x17000C76 RID: 3190
		// (get) Token: 0x0600167B RID: 5755 RVA: 0x0005A96C File Offset: 0x00058B6C
		public Microsoft.ReportingServices.OnDemandReportRendering.Style Style
		{
			get
			{
				if (this.m_style == null && !this.m_chart.IsOldSnapshot && this.m_chartEmptyPointsDef.StyleClass != null)
				{
					this.m_style = new Microsoft.ReportingServices.OnDemandReportRendering.Style(this.m_chart, this.m_chartSeries.ReportScope, this.m_chartEmptyPointsDef, this.m_chart.RenderingContext);
				}
				return this.m_style;
			}
		}

		// Token: 0x17000C77 RID: 3191
		// (get) Token: 0x0600167C RID: 5756 RVA: 0x0005A9CE File Offset: 0x00058BCE
		public string UniqueName
		{
			get
			{
				return this.m_chartSeries.ChartSeriesDef.UniqueName + "xEmptyPoint";
			}
		}

		// Token: 0x17000C78 RID: 3192
		// (get) Token: 0x0600167D RID: 5757 RVA: 0x0005A9EC File Offset: 0x00058BEC
		public ActionInfo ActionInfo
		{
			get
			{
				if (this.m_actionInfo == null && !this.m_chart.IsOldSnapshot && this.m_chartEmptyPointsDef.Action != null)
				{
					this.m_actionInfo = new ActionInfo(this.m_chart.RenderingContext, this.m_chartSeries.ReportScope, this.m_chartEmptyPointsDef.Action, this.m_chartSeries.ChartSeriesDef, this.m_chart, ObjectType.Chart, this.m_chartEmptyPointsDef.Name, this);
				}
				return this.m_actionInfo;
			}
		}

		// Token: 0x17000C79 RID: 3193
		// (get) Token: 0x0600167E RID: 5758 RVA: 0x0005AA6C File Offset: 0x00058C6C
		public List<string> FieldsUsedInValueExpression
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x0600167F RID: 5759 RVA: 0x0005AA70 File Offset: 0x00058C70
		public ChartMarker Marker
		{
			get
			{
				if (this.m_marker == null && !this.m_chart.IsOldSnapshot && this.m_chartEmptyPointsDef.Marker != null)
				{
					this.m_marker = new ChartMarker(this.m_chartSeries, this.m_chartEmptyPointsDef.Marker, this.m_chart);
				}
				return this.m_marker;
			}
		}

		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x06001680 RID: 5760 RVA: 0x0005AAC8 File Offset: 0x00058CC8
		public Microsoft.ReportingServices.OnDemandReportRendering.ChartDataLabel DataLabel
		{
			get
			{
				if (this.m_dataLabel == null && !this.m_chart.IsOldSnapshot && this.m_chartEmptyPointsDef.DataLabel != null)
				{
					this.m_dataLabel = new Microsoft.ReportingServices.OnDemandReportRendering.ChartDataLabel(this.m_chartSeries, this.m_chartEmptyPointsDef.DataLabel, this.m_chart);
				}
				return this.m_dataLabel;
			}
		}

		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x06001681 RID: 5761 RVA: 0x0005AB1F File Offset: 0x00058D1F
		public ReportVariantProperty AxisLabel
		{
			get
			{
				if (this.m_axisLabel == null && !this.m_chart.IsOldSnapshot && this.m_chartEmptyPointsDef.AxisLabel != null)
				{
					this.m_axisLabel = new ReportVariantProperty(this.m_chartEmptyPointsDef.AxisLabel);
				}
				return this.m_axisLabel;
			}
		}

		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x06001682 RID: 5762 RVA: 0x0005AB5F File Offset: 0x00058D5F
		public ReportStringProperty ToolTip
		{
			get
			{
				if (this.m_toolTip == null)
				{
					bool isOldSnapshot = this.m_chart.IsOldSnapshot;
					if (this.m_chartEmptyPointsDef.ToolTip != null)
					{
						this.m_toolTip = new ReportStringProperty(this.m_chartEmptyPointsDef.ToolTip);
					}
				}
				return this.m_toolTip;
			}
		}

		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x06001683 RID: 5763 RVA: 0x0005ABA0 File Offset: 0x00058DA0
		public CustomPropertyCollection CustomProperties
		{
			get
			{
				if (this.m_chart.IsOldSnapshot)
				{
					return null;
				}
				if (this.m_customProperties == null)
				{
					this.m_customPropertiesReady = true;
					this.m_customProperties = new CustomPropertyCollection(this.m_chart.ReportScope.ReportScopeInstance, this.m_chart.RenderingContext, null, this.m_chartEmptyPointsDef, ObjectType.Chart, this.m_chart.Name);
				}
				else if (!this.m_customPropertiesReady)
				{
					this.m_customPropertiesReady = true;
					this.m_customProperties.UpdateCustomProperties(this.m_chartSeries.ReportScope.ReportScopeInstance, this.m_chartEmptyPointsDef, this.m_chart.RenderingContext.OdpContext, ObjectType.Chart, this.m_chart.Name);
				}
				return this.m_customProperties;
			}
		}

		// Token: 0x17000C7F RID: 3199
		// (get) Token: 0x06001684 RID: 5764 RVA: 0x0005AC5A File Offset: 0x00058E5A
		internal IReportScope ReportScope
		{
			get
			{
				return this.m_chartSeries.ReportScope;
			}
		}

		// Token: 0x17000C80 RID: 3200
		// (get) Token: 0x06001685 RID: 5765 RVA: 0x0005AC67 File Offset: 0x00058E67
		internal Microsoft.ReportingServices.OnDemandReportRendering.Chart ChartDef
		{
			get
			{
				return this.m_chart;
			}
		}

		// Token: 0x17000C81 RID: 3201
		// (get) Token: 0x06001686 RID: 5766 RVA: 0x0005AC6F File Offset: 0x00058E6F
		internal ChartEmptyPoints ChartEmptyPointsDef
		{
			get
			{
				return this.m_chartEmptyPointsDef;
			}
		}

		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x06001687 RID: 5767 RVA: 0x0005AC77 File Offset: 0x00058E77
		public ChartEmptyPointsInstance Instance
		{
			get
			{
				if (this.m_chart.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new ChartEmptyPointsInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x0005ACA8 File Offset: 0x00058EA8
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
			if (this.m_marker != null)
			{
				this.m_marker.SetNewContext();
			}
			if (this.m_dataLabel != null)
			{
				this.m_dataLabel.SetNewContext();
			}
			this.m_customPropertiesReady = false;
		}

		// Token: 0x04000ADE RID: 2782
		private Microsoft.ReportingServices.OnDemandReportRendering.Chart m_chart;

		// Token: 0x04000ADF RID: 2783
		private InternalChartSeries m_chartSeries;

		// Token: 0x04000AE0 RID: 2784
		private ChartEmptyPoints m_chartEmptyPointsDef;

		// Token: 0x04000AE1 RID: 2785
		private ChartEmptyPointsInstance m_instance;

		// Token: 0x04000AE2 RID: 2786
		private Microsoft.ReportingServices.OnDemandReportRendering.Style m_style;

		// Token: 0x04000AE3 RID: 2787
		private ActionInfo m_actionInfo;

		// Token: 0x04000AE4 RID: 2788
		private ChartMarker m_marker;

		// Token: 0x04000AE5 RID: 2789
		private Microsoft.ReportingServices.OnDemandReportRendering.ChartDataLabel m_dataLabel;

		// Token: 0x04000AE6 RID: 2790
		private ReportVariantProperty m_axisLabel;

		// Token: 0x04000AE7 RID: 2791
		private CustomPropertyCollection m_customProperties;

		// Token: 0x04000AE8 RID: 2792
		private bool m_customPropertiesReady;

		// Token: 0x04000AE9 RID: 2793
		private ReportStringProperty m_toolTip;
	}
}
