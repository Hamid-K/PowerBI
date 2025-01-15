using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000241 RID: 577
	public sealed class ChartTitle : ChartObjectCollectionItem<ChartTitleInstance>, IROMStyleDefinitionContainer, IROMActionOwner
	{
		// Token: 0x06001630 RID: 5680 RVA: 0x0005977D File Offset: 0x0005797D
		internal ChartTitle(Microsoft.ReportingServices.ReportIntermediateFormat.ChartTitle chartTitleDef, Microsoft.ReportingServices.OnDemandReportRendering.Chart chart)
		{
			this.m_chart = chart;
			this.m_chartTitleDef = chartTitleDef;
		}

		// Token: 0x06001631 RID: 5681 RVA: 0x00059793 File Offset: 0x00057993
		internal ChartTitle(Microsoft.ReportingServices.ReportProcessing.ChartTitle renderChartTitleDef, ChartTitleInstance renderChartTitleInstance, Microsoft.ReportingServices.OnDemandReportRendering.Chart chart)
		{
			this.m_chart = chart;
			this.m_renderChartTitleDef = renderChartTitleDef;
			this.m_renderChartTitleInstance = renderChartTitleInstance;
		}

		// Token: 0x17000C3B RID: 3131
		// (get) Token: 0x06001632 RID: 5682 RVA: 0x000597B0 File Offset: 0x000579B0
		public string Name
		{
			get
			{
				if (this.m_chart.IsOldSnapshot)
				{
					return "Default";
				}
				return this.m_chartTitleDef.TitleName;
			}
		}

		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x06001633 RID: 5683 RVA: 0x000597D0 File Offset: 0x000579D0
		public ReportStringProperty Caption
		{
			get
			{
				if (this.m_caption == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						if (this.m_renderChartTitleDef.Caption != null)
						{
							this.m_caption = new ReportStringProperty(this.m_renderChartTitleDef.Caption);
						}
					}
					else if (this.m_chartTitleDef.Caption != null)
					{
						this.m_caption = new ReportStringProperty(this.m_chartTitleDef.Caption);
					}
				}
				return this.m_caption;
			}
		}

		// Token: 0x17000C3D RID: 3133
		// (get) Token: 0x06001634 RID: 5684 RVA: 0x00059840 File Offset: 0x00057A40
		public Microsoft.ReportingServices.OnDemandReportRendering.Style Style
		{
			get
			{
				if (this.m_style == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						this.m_style = new Microsoft.ReportingServices.OnDemandReportRendering.Style(this.m_renderChartTitleDef.StyleClass, this.m_renderChartTitleInstance.StyleAttributeValues, this.m_chart.RenderingContext);
					}
					else if (this.m_chartTitleDef != null)
					{
						this.m_style = new Microsoft.ReportingServices.OnDemandReportRendering.Style(this.m_chart, this.m_chart, this.m_chartTitleDef, this.m_chart.RenderingContext);
					}
				}
				return this.m_style;
			}
		}

		// Token: 0x17000C3E RID: 3134
		// (get) Token: 0x06001635 RID: 5685 RVA: 0x000598C8 File Offset: 0x00057AC8
		public ReportEnumProperty<ChartTitlePositions> Position
		{
			get
			{
				if (this.m_position == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						ChartTitlePositions chartTitlePositions = ChartTitlePositions.TopCenter;
						Microsoft.ReportingServices.ReportProcessing.ChartTitle.Positions position = this.m_renderChartTitleDef.Position;
						if (position <= Microsoft.ReportingServices.ReportProcessing.ChartTitle.Positions.Far)
						{
							chartTitlePositions = ChartTitlePositions.TopCenter;
						}
						this.m_position = new ReportEnumProperty<ChartTitlePositions>(chartTitlePositions);
					}
					else if (this.m_chartTitleDef.Position != null)
					{
						this.m_position = new ReportEnumProperty<ChartTitlePositions>(this.m_chartTitleDef.Position.IsExpression, this.m_chartTitleDef.Position.OriginalText, EnumTranslator.TranslateChartTitlePosition(this.m_chartTitleDef.Position.StringValue, null));
					}
				}
				return this.m_position;
			}
		}

		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x06001636 RID: 5686 RVA: 0x00059960 File Offset: 0x00057B60
		public string UniqueName
		{
			get
			{
				return this.m_chart.ChartDef.UniqueName + "xChartTitle_" + this.Name;
			}
		}

		// Token: 0x17000C40 RID: 3136
		// (get) Token: 0x06001637 RID: 5687 RVA: 0x00059984 File Offset: 0x00057B84
		public ActionInfo ActionInfo
		{
			get
			{
				if (this.m_actionInfo == null && !this.m_chart.IsOldSnapshot && this.m_chartTitleDef.Action != null)
				{
					this.m_actionInfo = new ActionInfo(this.m_chart.RenderingContext, this.m_chart, this.m_chartTitleDef.Action, this.m_chart.ChartDef, this.m_chart, ObjectType.Chart, this.m_chartTitleDef.Name, this);
				}
				return this.m_actionInfo;
			}
		}

		// Token: 0x17000C41 RID: 3137
		// (get) Token: 0x06001638 RID: 5688 RVA: 0x000599FF File Offset: 0x00057BFF
		public List<string> FieldsUsedInValueExpression
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C42 RID: 3138
		// (get) Token: 0x06001639 RID: 5689 RVA: 0x00059A02 File Offset: 0x00057C02
		public ReportBoolProperty Hidden
		{
			get
			{
				if (this.m_hidden == null && !this.m_chart.IsOldSnapshot && this.m_chartTitleDef.Hidden != null)
				{
					this.m_hidden = new ReportBoolProperty(this.m_chartTitleDef.Hidden);
				}
				return this.m_hidden;
			}
		}

		// Token: 0x17000C43 RID: 3139
		// (get) Token: 0x0600163A RID: 5690 RVA: 0x00059A42 File Offset: 0x00057C42
		public string DockToChartArea
		{
			get
			{
				if (this.m_chart.IsOldSnapshot)
				{
					return null;
				}
				return this.m_chartTitleDef.DockToChartArea;
			}
		}

		// Token: 0x17000C44 RID: 3140
		// (get) Token: 0x0600163B RID: 5691 RVA: 0x00059A60 File Offset: 0x00057C60
		public ReportIntProperty DockOffset
		{
			get
			{
				if (this.m_dockOffset == null && !this.m_chart.IsOldSnapshot && this.m_chartTitleDef.DockOffset != null)
				{
					this.m_dockOffset = new ReportIntProperty(this.m_chartTitleDef.DockOffset.IsExpression, this.m_chartTitleDef.DockOffset.OriginalText, this.m_chartTitleDef.DockOffset.IntValue, 0);
				}
				return this.m_dockOffset;
			}
		}

		// Token: 0x17000C45 RID: 3141
		// (get) Token: 0x0600163C RID: 5692 RVA: 0x00059AD1 File Offset: 0x00057CD1
		public ReportBoolProperty DockOutsideChartArea
		{
			get
			{
				if (this.m_dockOutsideChartArea == null && !this.m_chart.IsOldSnapshot && this.m_chartTitleDef.DockOutsideChartArea != null)
				{
					this.m_dockOutsideChartArea = new ReportBoolProperty(this.m_chartTitleDef.DockOutsideChartArea);
				}
				return this.m_dockOutsideChartArea;
			}
		}

		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x0600163D RID: 5693 RVA: 0x00059B11 File Offset: 0x00057D11
		public ReportStringProperty ToolTip
		{
			get
			{
				if (this.m_toolTip == null && !this.m_chart.IsOldSnapshot && this.m_chartTitleDef.ToolTip != null)
				{
					this.m_toolTip = new ReportStringProperty(this.m_chartTitleDef.ToolTip);
				}
				return this.m_toolTip;
			}
		}

		// Token: 0x17000C47 RID: 3143
		// (get) Token: 0x0600163E RID: 5694 RVA: 0x00059B54 File Offset: 0x00057D54
		public ReportEnumProperty<TextOrientations> TextOrientation
		{
			get
			{
				if (this.m_textOrientation == null && !this.m_chart.IsOldSnapshot && this.m_chartTitleDef.TextOrientation != null)
				{
					this.m_textOrientation = new ReportEnumProperty<TextOrientations>(this.m_chartTitleDef.TextOrientation.IsExpression, this.m_chartTitleDef.TextOrientation.OriginalText, EnumTranslator.TranslateTextOrientations(this.m_chartTitleDef.TextOrientation.StringValue, null));
				}
				return this.m_textOrientation;
			}
		}

		// Token: 0x17000C48 RID: 3144
		// (get) Token: 0x0600163F RID: 5695 RVA: 0x00059BCC File Offset: 0x00057DCC
		public ChartElementPosition ChartElementPosition
		{
			get
			{
				if (this.m_chartElementPosition == null && !this.m_chart.IsOldSnapshot && this.m_chartTitleDef.ChartElementPosition != null)
				{
					this.m_chartElementPosition = new ChartElementPosition(this.m_chartTitleDef.ChartElementPosition, this.m_chart);
				}
				return this.m_chartElementPosition;
			}
		}

		// Token: 0x17000C49 RID: 3145
		// (get) Token: 0x06001640 RID: 5696 RVA: 0x00059C1D File Offset: 0x00057E1D
		internal Microsoft.ReportingServices.OnDemandReportRendering.Chart ChartDef
		{
			get
			{
				return this.m_chart;
			}
		}

		// Token: 0x17000C4A RID: 3146
		// (get) Token: 0x06001641 RID: 5697 RVA: 0x00059C25 File Offset: 0x00057E25
		internal Microsoft.ReportingServices.ReportIntermediateFormat.ChartTitle ChartTitleDef
		{
			get
			{
				return this.m_chartTitleDef;
			}
		}

		// Token: 0x17000C4B RID: 3147
		// (get) Token: 0x06001642 RID: 5698 RVA: 0x00059C2D File Offset: 0x00057E2D
		internal ChartTitleInstance RenderChartTitleInstance
		{
			get
			{
				return this.m_renderChartTitleInstance;
			}
		}

		// Token: 0x17000C4C RID: 3148
		// (get) Token: 0x06001643 RID: 5699 RVA: 0x00059C35 File Offset: 0x00057E35
		public ChartTitleInstance Instance
		{
			get
			{
				if (this.m_chart.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new ChartTitleInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x06001644 RID: 5700 RVA: 0x00059C68 File Offset: 0x00057E68
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
			if (this.m_chartElementPosition != null)
			{
				this.m_chartElementPosition.SetNewContext();
			}
		}

		// Token: 0x04000AA7 RID: 2727
		private Microsoft.ReportingServices.OnDemandReportRendering.Chart m_chart;

		// Token: 0x04000AA8 RID: 2728
		private Microsoft.ReportingServices.ReportIntermediateFormat.ChartTitle m_chartTitleDef;

		// Token: 0x04000AA9 RID: 2729
		private Microsoft.ReportingServices.OnDemandReportRendering.Style m_style;

		// Token: 0x04000AAA RID: 2730
		private ReportStringProperty m_caption;

		// Token: 0x04000AAB RID: 2731
		private Microsoft.ReportingServices.ReportProcessing.ChartTitle m_renderChartTitleDef;

		// Token: 0x04000AAC RID: 2732
		private ChartTitleInstance m_renderChartTitleInstance;

		// Token: 0x04000AAD RID: 2733
		private ReportEnumProperty<ChartTitlePositions> m_position;

		// Token: 0x04000AAE RID: 2734
		private ActionInfo m_actionInfo;

		// Token: 0x04000AAF RID: 2735
		private ReportBoolProperty m_hidden;

		// Token: 0x04000AB0 RID: 2736
		private ReportIntProperty m_dockOffset;

		// Token: 0x04000AB1 RID: 2737
		private ReportBoolProperty m_dockOutsideChartArea;

		// Token: 0x04000AB2 RID: 2738
		private ReportStringProperty m_toolTip;

		// Token: 0x04000AB3 RID: 2739
		private ReportEnumProperty<TextOrientations> m_textOrientation;

		// Token: 0x04000AB4 RID: 2740
		private ChartElementPosition m_chartElementPosition;
	}
}
