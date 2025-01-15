using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200022C RID: 556
	internal sealed class ShimChartDataPoint : Microsoft.ReportingServices.OnDemandReportRendering.ChartDataPoint
	{
		// Token: 0x0600152F RID: 5423 RVA: 0x00055789 File Offset: 0x00053989
		internal ShimChartDataPoint(Microsoft.ReportingServices.OnDemandReportRendering.Chart owner, int rowIndex, int colIndex, ShimChartMember seriesParentMember, ShimChartMember categoryParentMember)
			: base(owner, rowIndex, colIndex)
		{
			this.m_dataValues = null;
			this.m_seriesParentMember = seriesParentMember;
			this.m_categoryParentMember = categoryParentMember;
		}

		// Token: 0x17000B70 RID: 2928
		// (get) Token: 0x06001530 RID: 5424 RVA: 0x000557AB File Offset: 0x000539AB
		public override string DataElementName
		{
			get
			{
				return this.CachedRenderDataPoint.DataElementName;
			}
		}

		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x06001531 RID: 5425 RVA: 0x000557B8 File Offset: 0x000539B8
		public override DataElementOutputTypes DataElementOutput
		{
			get
			{
				if (this.CachedRenderDataPoint.DataElementOutput == DataElementOutputTypes.Output)
				{
					return DataElementOutputTypes.ContentsOnly;
				}
				return DataElementOutputTypes.NoOutput;
			}
		}

		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x06001532 RID: 5426 RVA: 0x000557CC File Offset: 0x000539CC
		internal DataValueCollection DataValues
		{
			get
			{
				if (this.m_dataValues == null)
				{
					this.m_dataValues = new DataValueCollection(this.m_owner.RenderingContext, this.CachedRenderDataPoint);
				}
				else if (this.m_dataValueUpdateNeeded)
				{
					this.m_dataValueUpdateNeeded = false;
					this.m_dataValues.UpdateChartDataValues(this.CachedRenderDataPoint.DataValues);
				}
				return this.m_dataValues;
			}
		}

		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x06001533 RID: 5427 RVA: 0x0005582A File Offset: 0x00053A2A
		public override ChartDataPointValues DataPointValues
		{
			get
			{
				if (this.m_dataPointValues == null)
				{
					this.m_dataPointValues = new ChartDataPointValues(this, base.ChartDef);
				}
				return this.m_dataPointValues;
			}
		}

		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x06001534 RID: 5428 RVA: 0x0005584C File Offset: 0x00053A4C
		public override ChartItemInLegend ItemInLegend
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x06001535 RID: 5429 RVA: 0x0005584F File Offset: 0x00053A4F
		public override ActionInfo ActionInfo
		{
			get
			{
				if (this.m_actionInfo == null && this.CachedRenderDataPoint.ActionInfo != null)
				{
					this.m_actionInfo = new ActionInfo(this.m_owner.RenderingContext, this.CachedRenderDataPoint.ActionInfo);
				}
				return this.m_actionInfo;
			}
		}

		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x06001536 RID: 5430 RVA: 0x00055890 File Offset: 0x00053A90
		public override CustomPropertyCollection CustomProperties
		{
			get
			{
				if (this.m_customProperties == null && this.CachedRenderDataPoint.CustomProperties != null && 0 < this.CachedRenderDataPoint.CustomProperties.Count)
				{
					this.m_customProperties = new CustomPropertyCollection(this.m_owner.RenderingContext, this.CachedRenderDataPoint.CustomProperties);
				}
				return this.m_customProperties;
			}
		}

		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x06001537 RID: 5431 RVA: 0x000558EC File Offset: 0x00053AEC
		public override Microsoft.ReportingServices.OnDemandReportRendering.Style Style
		{
			get
			{
				if (this.m_style == null)
				{
					this.m_style = new Microsoft.ReportingServices.OnDemandReportRendering.Style(this.CachedDataPoint.StyleClass, this.CachedRenderDataPoint.InstanceInfo.StyleAttributeValues, base.ChartDef.RenderingContext);
				}
				return this.m_style;
			}
		}

		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x06001538 RID: 5432 RVA: 0x00055938 File Offset: 0x00053B38
		public override ChartMarker Marker
		{
			get
			{
				if (this.m_marker == null)
				{
					this.m_marker = new ChartMarker(this, base.ChartDef);
				}
				return this.m_marker;
			}
		}

		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x06001539 RID: 5433 RVA: 0x0005595A File Offset: 0x00053B5A
		public override Microsoft.ReportingServices.OnDemandReportRendering.ChartDataLabel DataLabel
		{
			get
			{
				if (this.m_dataLabel == null)
				{
					this.m_dataLabel = new Microsoft.ReportingServices.OnDemandReportRendering.ChartDataLabel(this, base.ChartDef);
				}
				return this.m_dataLabel;
			}
		}

		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x0600153A RID: 5434 RVA: 0x0005597C File Offset: 0x00053B7C
		public override ReportVariantProperty AxisLabel
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B7B RID: 2939
		// (get) Token: 0x0600153B RID: 5435 RVA: 0x0005597F File Offset: 0x00053B7F
		public override ReportStringProperty ToolTip
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B7C RID: 2940
		// (get) Token: 0x0600153C RID: 5436 RVA: 0x00055982 File Offset: 0x00053B82
		internal override Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint DataPointDef
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B7D RID: 2941
		// (get) Token: 0x0600153D RID: 5437 RVA: 0x00055985 File Offset: 0x00053B85
		internal override Microsoft.ReportingServices.ReportRendering.ChartDataPoint RenderItem
		{
			get
			{
				return this.CachedRenderDataPoint;
			}
		}

		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x0600153E RID: 5438 RVA: 0x0005598D File Offset: 0x00053B8D
		internal override Microsoft.ReportingServices.ReportProcessing.ChartDataPoint RenderDataPointDef
		{
			get
			{
				return this.CachedDataPoint;
			}
		}

		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x0600153F RID: 5439 RVA: 0x00055998 File Offset: 0x00053B98
		private Microsoft.ReportingServices.ReportRendering.ChartDataPoint CachedRenderDataPoint
		{
			get
			{
				if (this.m_renderDataPoint == null)
				{
					int cachedMemberDataPointIndex = this.m_seriesParentMember.CurrentRenderChartMember.CachedMemberDataPointIndex;
					int cachedMemberDataPointIndex2 = this.m_categoryParentMember.CurrentRenderChartMember.CachedMemberDataPointIndex;
					this.m_renderDataPoint = this.m_owner.RenderChart.DataPointCollection[cachedMemberDataPointIndex, cachedMemberDataPointIndex2];
					if (this.m_actionInfo != null)
					{
						this.m_actionInfo.Update(this.m_renderDataPoint.ActionInfo);
					}
					if (this.m_customProperties != null)
					{
						this.m_customProperties.UpdateCustomProperties(this.m_renderDataPoint.CustomProperties);
					}
				}
				return this.m_renderDataPoint;
			}
		}

		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x06001540 RID: 5440 RVA: 0x00055A30 File Offset: 0x00053C30
		private Microsoft.ReportingServices.ReportProcessing.ChartDataPoint CachedDataPoint
		{
			get
			{
				if (this.m_cachedDataPoint == null)
				{
					int memberCellIndex = this.m_seriesParentMember.MemberCellIndex;
					int memberCellIndex2 = this.m_categoryParentMember.MemberCellIndex;
					this.m_cachedDataPoint = ((Microsoft.ReportingServices.ReportProcessing.Chart)this.m_owner.RenderChart.ReportItemDef).GetDataPoint(memberCellIndex, memberCellIndex2);
				}
				return this.m_cachedDataPoint;
			}
		}

		// Token: 0x06001541 RID: 5441 RVA: 0x00055A85 File Offset: 0x00053C85
		internal override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_dataValues != null)
			{
				this.m_dataValues.SetNewContext();
			}
			this.m_renderDataPoint = null;
			this.m_dataValueUpdateNeeded = true;
			this.m_cachedDataPoint = null;
		}

		// Token: 0x040009F3 RID: 2547
		private Microsoft.ReportingServices.ReportRendering.ChartDataPoint m_renderDataPoint;

		// Token: 0x040009F4 RID: 2548
		private bool m_dataValueUpdateNeeded;

		// Token: 0x040009F5 RID: 2549
		private DataValueCollection m_dataValues;

		// Token: 0x040009F6 RID: 2550
		private ShimChartMember m_seriesParentMember;

		// Token: 0x040009F7 RID: 2551
		private ShimChartMember m_categoryParentMember;

		// Token: 0x040009F8 RID: 2552
		private Microsoft.ReportingServices.ReportProcessing.ChartDataPoint m_cachedDataPoint;
	}
}
