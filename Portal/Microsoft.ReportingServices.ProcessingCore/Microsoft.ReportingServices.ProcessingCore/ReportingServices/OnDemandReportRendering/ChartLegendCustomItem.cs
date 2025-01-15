using System;
using System.Collections.Generic;
using System.Drawing;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000238 RID: 568
	public sealed class ChartLegendCustomItem : ChartObjectCollectionItem<ChartLegendCustomItemInstance>, IROMStyleDefinitionContainer, IROMActionOwner
	{
		// Token: 0x060015C5 RID: 5573 RVA: 0x00057BBE File Offset: 0x00055DBE
		internal ChartLegendCustomItem(ChartLegendCustomItem chartLegendCustomItemDef, Microsoft.ReportingServices.OnDemandReportRendering.Chart chart)
		{
			this.m_chartLegendCustomItemDef = chartLegendCustomItemDef;
			this.m_chart = chart;
		}

		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x060015C6 RID: 5574 RVA: 0x00057BD4 File Offset: 0x00055DD4
		public Microsoft.ReportingServices.OnDemandReportRendering.Style Style
		{
			get
			{
				if (this.m_style == null && !this.m_chart.IsOldSnapshot && this.m_chartLegendCustomItemDef.StyleClass != null)
				{
					this.m_style = new Microsoft.ReportingServices.OnDemandReportRendering.Style(this.m_chart, this.m_chart, this.m_chartLegendCustomItemDef, this.m_chart.RenderingContext);
				}
				return this.m_style;
			}
		}

		// Token: 0x17000BE5 RID: 3045
		// (get) Token: 0x060015C7 RID: 5575 RVA: 0x00057C34 File Offset: 0x00055E34
		public string UniqueName
		{
			get
			{
				return this.m_chart.ChartDef.UniqueName + "x" + this.m_chartLegendCustomItemDef.ID.ToString();
			}
		}

		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x060015C8 RID: 5576 RVA: 0x00057C70 File Offset: 0x00055E70
		public ActionInfo ActionInfo
		{
			get
			{
				if (this.m_actionInfo == null && !this.m_chart.IsOldSnapshot && this.m_chartLegendCustomItemDef.Action != null)
				{
					this.m_actionInfo = new ActionInfo(this.m_chart.RenderingContext, this.m_chart, this.m_chartLegendCustomItemDef.Action, this.m_chart.ChartDef, this.m_chart, ObjectType.Chart, this.m_chartLegendCustomItemDef.Name, this);
				}
				return this.m_actionInfo;
			}
		}

		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x060015C9 RID: 5577 RVA: 0x00057CEB File Offset: 0x00055EEB
		public List<string> FieldsUsedInValueExpression
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x060015CA RID: 5578 RVA: 0x00057CF0 File Offset: 0x00055EF0
		public ChartMarker Marker
		{
			get
			{
				if (this.m_marker == null && !this.m_chart.IsOldSnapshot && this.m_chartLegendCustomItemDef.Marker != null)
				{
					this.m_marker = new ChartMarker(this.m_chartLegendCustomItemDef.Marker, this.m_chart);
				}
				return this.m_marker;
			}
		}

		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x060015CB RID: 5579 RVA: 0x00057D44 File Offset: 0x00055F44
		public ReportEnumProperty<ChartSeparators> Separator
		{
			get
			{
				if (this.m_separator == null && !this.m_chart.IsOldSnapshot && this.m_chartLegendCustomItemDef.Separator != null)
				{
					this.m_separator = new ReportEnumProperty<ChartSeparators>(this.m_chartLegendCustomItemDef.Separator.IsExpression, this.m_chartLegendCustomItemDef.Separator.OriginalText, EnumTranslator.TranslateChartSeparator(this.m_chartLegendCustomItemDef.Separator.StringValue, null));
				}
				return this.m_separator;
			}
		}

		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x060015CC RID: 5580 RVA: 0x00057DBC File Offset: 0x00055FBC
		public ReportColorProperty SeparatorColor
		{
			get
			{
				if (this.m_separatorColor == null && !this.m_chart.IsOldSnapshot && this.m_chartLegendCustomItemDef.SeparatorColor != null)
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo separatorColor = this.m_chartLegendCustomItemDef.SeparatorColor;
					this.m_separatorColor = new ReportColorProperty(separatorColor.IsExpression, separatorColor.OriginalText, separatorColor.IsExpression ? null : new ReportColor(separatorColor.StringValue.Trim(), true), separatorColor.IsExpression ? new ReportColor("", Color.Empty, true) : null);
				}
				return this.m_separatorColor;
			}
		}

		// Token: 0x17000BEB RID: 3051
		// (get) Token: 0x060015CD RID: 5581 RVA: 0x00057E4B File Offset: 0x0005604B
		public ReportStringProperty ToolTip
		{
			get
			{
				if (this.m_toolTip == null && !this.m_chart.IsOldSnapshot && this.m_chartLegendCustomItemDef.ToolTip != null)
				{
					this.m_toolTip = new ReportStringProperty(this.m_chartLegendCustomItemDef.ToolTip);
				}
				return this.m_toolTip;
			}
		}

		// Token: 0x17000BEC RID: 3052
		// (get) Token: 0x060015CE RID: 5582 RVA: 0x00057E8B File Offset: 0x0005608B
		public ChartLegendCustomItemCellCollection LegendCustomItemCells
		{
			get
			{
				if (this.m_chartLegendCustomItemCells == null && !this.m_chart.IsOldSnapshot && this.ChartLegendCustomItemDef.LegendCustomItemCells != null)
				{
					this.m_chartLegendCustomItemCells = new ChartLegendCustomItemCellCollection(this, this.m_chart);
				}
				return this.m_chartLegendCustomItemCells;
			}
		}

		// Token: 0x17000BED RID: 3053
		// (get) Token: 0x060015CF RID: 5583 RVA: 0x00057EC7 File Offset: 0x000560C7
		internal Microsoft.ReportingServices.OnDemandReportRendering.Chart ChartDef
		{
			get
			{
				return this.m_chart;
			}
		}

		// Token: 0x17000BEE RID: 3054
		// (get) Token: 0x060015D0 RID: 5584 RVA: 0x00057ECF File Offset: 0x000560CF
		internal ChartLegendCustomItem ChartLegendCustomItemDef
		{
			get
			{
				return this.m_chartLegendCustomItemDef;
			}
		}

		// Token: 0x17000BEF RID: 3055
		// (get) Token: 0x060015D1 RID: 5585 RVA: 0x00057ED7 File Offset: 0x000560D7
		public ChartLegendCustomItemInstance Instance
		{
			get
			{
				if (this.m_chart.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new ChartLegendCustomItemInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x060015D2 RID: 5586 RVA: 0x00057F08 File Offset: 0x00056108
		internal override void SetNewContext()
		{
			base.SetNewContext();
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
			if (this.m_chartLegendCustomItemCells != null)
			{
				this.m_chartLegendCustomItemCells.SetNewContext();
			}
		}

		// Token: 0x04000A57 RID: 2647
		private Microsoft.ReportingServices.OnDemandReportRendering.Chart m_chart;

		// Token: 0x04000A58 RID: 2648
		private ChartLegendCustomItem m_chartLegendCustomItemDef;

		// Token: 0x04000A59 RID: 2649
		private Microsoft.ReportingServices.OnDemandReportRendering.Style m_style;

		// Token: 0x04000A5A RID: 2650
		private ActionInfo m_actionInfo;

		// Token: 0x04000A5B RID: 2651
		private ChartMarker m_marker;

		// Token: 0x04000A5C RID: 2652
		private ReportEnumProperty<ChartSeparators> m_separator;

		// Token: 0x04000A5D RID: 2653
		private ReportColorProperty m_separatorColor;

		// Token: 0x04000A5E RID: 2654
		private ReportStringProperty m_toolTip;

		// Token: 0x04000A5F RID: 2655
		private ChartLegendCustomItemCellCollection m_chartLegendCustomItemCells;
	}
}
