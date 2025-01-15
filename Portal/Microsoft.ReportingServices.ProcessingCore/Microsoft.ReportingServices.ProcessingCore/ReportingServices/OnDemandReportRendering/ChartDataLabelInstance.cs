using System;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000259 RID: 601
	public sealed class ChartDataLabelInstance : BaseInstance
	{
		// Token: 0x06001763 RID: 5987 RVA: 0x0005EA2C File Offset: 0x0005CC2C
		internal ChartDataLabelInstance(ChartDataLabel chartDataLabelDef)
			: base(chartDataLabelDef.ReportScope)
		{
			this.m_chartDataLabelDef = chartDataLabelDef;
		}

		// Token: 0x17000D24 RID: 3364
		// (get) Token: 0x06001764 RID: 5988 RVA: 0x0005EA41 File Offset: 0x0005CC41
		public StyleInstance Style
		{
			get
			{
				if (this.m_style == null)
				{
					this.m_style = new StyleInstance(this.m_chartDataLabelDef, this.m_chartDataLabelDef.ReportScope, this.m_chartDataLabelDef.ChartDef.RenderingContext);
				}
				return this.m_style;
			}
		}

		// Token: 0x17000D25 RID: 3365
		// (get) Token: 0x06001765 RID: 5989 RVA: 0x0005EA7D File Offset: 0x0005CC7D
		public object OriginalValue
		{
			get
			{
				return this.GetOriginalValue().Value;
			}
		}

		// Token: 0x17000D26 RID: 3366
		// (get) Token: 0x06001766 RID: 5990 RVA: 0x0005EA8C File Offset: 0x0005CC8C
		public string Label
		{
			get
			{
				if (this.m_formattedValue == null)
				{
					if (this.m_chartDataLabelDef.ChartDef.IsOldSnapshot)
					{
						this.m_formattedValue = this.GetOriginalValue().Value as string;
					}
					else
					{
						this.m_formattedValue = this.m_chartDataLabelDef.ChartDataLabelDef.GetFormattedValue(this.GetOriginalValue(), this.ReportScopeInstance, this.m_chartDataLabelDef.ChartDef.RenderingContext.OdpContext);
					}
				}
				return this.m_formattedValue;
			}
		}

		// Token: 0x17000D27 RID: 3367
		// (get) Token: 0x06001767 RID: 5991 RVA: 0x0005EB08 File Offset: 0x0005CD08
		public bool UseValueAsLabel
		{
			get
			{
				if (this.m_useValueAsLabel == null && !this.m_chartDataLabelDef.ChartDef.IsOldSnapshot)
				{
					this.m_useValueAsLabel = new bool?(this.m_chartDataLabelDef.ChartDataLabelDef.EvaluateUseValueAsLabel(this.ReportScopeInstance, this.m_chartDataLabelDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_useValueAsLabel.Value;
			}
		}

		// Token: 0x17000D28 RID: 3368
		// (get) Token: 0x06001768 RID: 5992 RVA: 0x0005EB78 File Offset: 0x0005CD78
		public string ToolTip
		{
			get
			{
				if (this.m_toolTip == null)
				{
					this.m_toolTip = this.m_chartDataLabelDef.ChartDataLabelDef.EvaluateToolTip(this.ReportScopeInstance, this.m_chartDataLabelDef.ChartDef.RenderingContext.OdpContext);
				}
				return this.m_toolTip;
			}
		}

		// Token: 0x17000D29 RID: 3369
		// (get) Token: 0x06001769 RID: 5993 RVA: 0x0005EBC4 File Offset: 0x0005CDC4
		public ChartDataLabelPositions Position
		{
			get
			{
				if (this.m_position == null && !this.m_chartDataLabelDef.ChartDef.IsOldSnapshot)
				{
					this.m_position = new ChartDataLabelPositions?(this.m_chartDataLabelDef.ChartDataLabelDef.EvaluatePosition(this.ReportScopeInstance, this.m_chartDataLabelDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_position.Value;
			}
		}

		// Token: 0x17000D2A RID: 3370
		// (get) Token: 0x0600176A RID: 5994 RVA: 0x0005EC34 File Offset: 0x0005CE34
		public int Rotation
		{
			get
			{
				if (this.m_rotation == null && !this.m_chartDataLabelDef.ChartDef.IsOldSnapshot)
				{
					this.m_rotation = new int?(this.m_chartDataLabelDef.ChartDataLabelDef.EvaluateRotation(this.ReportScopeInstance, this.m_chartDataLabelDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_rotation.Value;
			}
		}

		// Token: 0x17000D2B RID: 3371
		// (get) Token: 0x0600176B RID: 5995 RVA: 0x0005ECA4 File Offset: 0x0005CEA4
		public bool Visible
		{
			get
			{
				if (this.m_visible == null && !this.m_chartDataLabelDef.ChartDef.IsOldSnapshot)
				{
					this.m_visible = new bool?(this.m_chartDataLabelDef.ChartDataLabelDef.EvaluateVisible(this.ReportScopeInstance, this.m_chartDataLabelDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_visible.Value;
			}
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x0005ED14 File Offset: 0x0005CF14
		private Microsoft.ReportingServices.RdlExpressions.VariantResult GetOriginalValue()
		{
			if (this.m_originalValue == null)
			{
				if (this.m_chartDataLabelDef.ChartDef.IsOldSnapshot)
				{
					ChartDataLabel dataLabel = this.m_chartDataLabelDef.ChartDataPoint.RenderDataPointDef.DataLabel;
					if (dataLabel != null)
					{
						if (dataLabel.Value != null && !dataLabel.Value.IsExpression)
						{
							string value = dataLabel.Value.Value;
						}
						else
						{
							string dataLabelValue = this.m_chartDataLabelDef.ChartDataPoint.RenderItem.InstanceInfo.DataLabelValue;
						}
					}
					this.m_originalValue = new Microsoft.ReportingServices.RdlExpressions.VariantResult?(new Microsoft.ReportingServices.RdlExpressions.VariantResult(false, dataLabel));
				}
				else
				{
					this.m_originalValue = new Microsoft.ReportingServices.RdlExpressions.VariantResult?(this.m_chartDataLabelDef.ChartDataLabelDef.EvaluateLabel(this.ReportScopeInstance, this.m_chartDataLabelDef.ChartDef.RenderingContext.OdpContext));
				}
			}
			return this.m_originalValue.Value;
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x0005EDF0 File Offset: 0x0005CFF0
		protected override void ResetInstanceCache()
		{
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
			this.m_originalValue = null;
			this.m_formattedValue = null;
			this.m_useValueAsLabel = null;
			this.m_position = null;
			this.m_rotation = null;
			this.m_visible = null;
			this.m_toolTip = null;
		}

		// Token: 0x04000B88 RID: 2952
		private ChartDataLabel m_chartDataLabelDef;

		// Token: 0x04000B89 RID: 2953
		private StyleInstance m_style;

		// Token: 0x04000B8A RID: 2954
		private string m_formattedValue;

		// Token: 0x04000B8B RID: 2955
		private Microsoft.ReportingServices.RdlExpressions.VariantResult? m_originalValue;

		// Token: 0x04000B8C RID: 2956
		private bool? m_useValueAsLabel;

		// Token: 0x04000B8D RID: 2957
		private ChartDataLabelPositions? m_position;

		// Token: 0x04000B8E RID: 2958
		private int? m_rotation;

		// Token: 0x04000B8F RID: 2959
		private bool? m_visible;

		// Token: 0x04000B90 RID: 2960
		private string m_toolTip;
	}
}
