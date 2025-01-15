using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200022B RID: 555
	internal sealed class InternalChartDataPoint : Microsoft.ReportingServices.OnDemandReportRendering.ChartDataPoint, IROMActionOwner
	{
		// Token: 0x0600151C RID: 5404 RVA: 0x00055486 File Offset: 0x00053686
		internal InternalChartDataPoint(Microsoft.ReportingServices.OnDemandReportRendering.Chart owner, int rowIndex, int colIndex, Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint dataPointDef)
			: base(owner, rowIndex, colIndex)
		{
			this.m_dataPointDef = dataPointDef;
		}

		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x0600151D RID: 5405 RVA: 0x00055499 File Offset: 0x00053699
		public override string DataElementName
		{
			get
			{
				return this.m_dataPointDef.DataElementName;
			}
		}

		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x0600151E RID: 5406 RVA: 0x000554A6 File Offset: 0x000536A6
		public override DataElementOutputTypes DataElementOutput
		{
			get
			{
				return this.m_dataPointDef.DataElementOutput;
			}
		}

		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x0600151F RID: 5407 RVA: 0x000554B3 File Offset: 0x000536B3
		public override ChartDataPointValues DataPointValues
		{
			get
			{
				if (this.m_dataPointValues == null && this.m_dataPointDef.DataPointValues != null)
				{
					this.m_dataPointValues = new ChartDataPointValues(this, this.m_dataPointDef.DataPointValues, this.m_owner);
				}
				return this.m_dataPointValues;
			}
		}

		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x06001520 RID: 5408 RVA: 0x000554ED File Offset: 0x000536ED
		public override ChartItemInLegend ItemInLegend
		{
			get
			{
				if (this.m_dataPointDef.ItemInLegend != null)
				{
					this.m_itemInLegend = new ChartItemInLegend(this, this.m_dataPointDef.ItemInLegend, this.m_owner);
				}
				return this.m_itemInLegend;
			}
		}

		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x06001521 RID: 5409 RVA: 0x0005551F File Offset: 0x0005371F
		public string UniqueName
		{
			get
			{
				return this.m_dataPointDef.UniqueName;
			}
		}

		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x06001522 RID: 5410 RVA: 0x0005552C File Offset: 0x0005372C
		public override ActionInfo ActionInfo
		{
			get
			{
				if (this.m_actionInfo == null && this.m_dataPointDef.Action != null)
				{
					this.m_actionInfo = new ActionInfo(this.m_owner.RenderingContext, this, this.m_dataPointDef.Action, this.m_dataPointDef, this.m_owner, ObjectType.Chart, this.m_dataPointDef.Name, this);
				}
				return this.m_actionInfo;
			}
		}

		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x06001523 RID: 5411 RVA: 0x00055590 File Offset: 0x00053790
		public List<string> FieldsUsedInValueExpression
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x06001524 RID: 5412 RVA: 0x00055594 File Offset: 0x00053794
		public override CustomPropertyCollection CustomProperties
		{
			get
			{
				if (this.m_customProperties == null)
				{
					this.m_customPropertiesReady = true;
					this.m_customProperties = new CustomPropertyCollection(base.Instance, this.m_owner.RenderingContext, null, this.m_dataPointDef, ObjectType.Chart, this.m_owner.Name);
				}
				else if (!this.m_customPropertiesReady)
				{
					this.m_customPropertiesReady = true;
					this.m_customProperties.UpdateCustomProperties(base.Instance, this.m_dataPointDef, this.m_owner.RenderingContext.OdpContext, ObjectType.Chart, this.m_owner.Name);
				}
				return this.m_customProperties;
			}
		}

		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x06001525 RID: 5413 RVA: 0x0005562B File Offset: 0x0005382B
		public override Microsoft.ReportingServices.OnDemandReportRendering.Style Style
		{
			get
			{
				if (this.m_style == null && this.m_dataPointDef.StyleClass != null)
				{
					this.m_style = new Microsoft.ReportingServices.OnDemandReportRendering.Style(this.m_owner, this, this.m_dataPointDef, base.ChartDef.RenderingContext);
				}
				return this.m_style;
			}
		}

		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x06001526 RID: 5414 RVA: 0x0005566B File Offset: 0x0005386B
		public override ChartMarker Marker
		{
			get
			{
				if (this.m_marker == null && this.m_dataPointDef.Marker != null)
				{
					this.m_marker = new ChartMarker(this, this.m_dataPointDef.Marker, this.m_owner);
				}
				return this.m_marker;
			}
		}

		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x06001527 RID: 5415 RVA: 0x000556A5 File Offset: 0x000538A5
		public override Microsoft.ReportingServices.OnDemandReportRendering.ChartDataLabel DataLabel
		{
			get
			{
				if (this.m_dataLabel == null && this.m_dataPointDef.DataLabel != null)
				{
					this.m_dataLabel = new Microsoft.ReportingServices.OnDemandReportRendering.ChartDataLabel(this, this.m_dataPointDef.DataLabel, this.m_owner);
				}
				return this.m_dataLabel;
			}
		}

		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x06001528 RID: 5416 RVA: 0x000556DF File Offset: 0x000538DF
		public override ReportVariantProperty AxisLabel
		{
			get
			{
				if (this.m_axisLabel == null && this.m_dataPointDef.AxisLabel != null)
				{
					this.m_axisLabel = new ReportVariantProperty(this.m_dataPointDef.AxisLabel);
				}
				return this.m_axisLabel;
			}
		}

		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x06001529 RID: 5417 RVA: 0x00055712 File Offset: 0x00053912
		public override ReportStringProperty ToolTip
		{
			get
			{
				if (this.m_toolTip == null && this.m_dataPointDef.ToolTip != null)
				{
					this.m_toolTip = new ReportStringProperty(this.m_dataPointDef.ToolTip);
				}
				return this.m_toolTip;
			}
		}

		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x0600152A RID: 5418 RVA: 0x00055745 File Offset: 0x00053945
		internal override Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint DataPointDef
		{
			get
			{
				return this.m_dataPointDef;
			}
		}

		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x0600152B RID: 5419 RVA: 0x0005574D File Offset: 0x0005394D
		internal override Microsoft.ReportingServices.ReportRendering.ChartDataPoint RenderItem
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B6E RID: 2926
		// (get) Token: 0x0600152C RID: 5420 RVA: 0x00055750 File Offset: 0x00053950
		internal override Microsoft.ReportingServices.ReportProcessing.ChartDataPoint RenderDataPointDef
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B6F RID: 2927
		// (get) Token: 0x0600152D RID: 5421 RVA: 0x00055753 File Offset: 0x00053953
		internal override IRIFReportScope RIFReportScope
		{
			get
			{
				return this.m_dataPointDef;
			}
		}

		// Token: 0x0600152E RID: 5422 RVA: 0x0005575B File Offset: 0x0005395B
		internal override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_itemInLegend != null)
			{
				this.m_itemInLegend.SetNewContext();
			}
			if (this.m_dataPointDef != null)
			{
				this.m_dataPointDef.ClearStreamingScopeInstanceBinding();
			}
		}

		// Token: 0x040009EF RID: 2543
		private Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint m_dataPointDef;

		// Token: 0x040009F0 RID: 2544
		private ReportVariantProperty m_axisLabel;

		// Token: 0x040009F1 RID: 2545
		private ChartItemInLegend m_itemInLegend;

		// Token: 0x040009F2 RID: 2546
		private ReportStringProperty m_toolTip;
	}
}
