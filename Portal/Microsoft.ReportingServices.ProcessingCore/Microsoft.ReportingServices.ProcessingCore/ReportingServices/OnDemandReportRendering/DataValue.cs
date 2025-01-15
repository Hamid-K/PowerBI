using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002AF RID: 687
	public sealed class DataValue
	{
		// Token: 0x06001A4F RID: 6735 RVA: 0x0006A4C7 File Offset: 0x000686C7
		internal DataValue(RenderingContext renderingContext, object chartDataValue)
		{
			this.m_isChartValue = true;
			this.m_name = new ReportStringProperty();
			this.m_value = new ReportVariantProperty(true);
			this.m_instance = new ShimDataValueInstance(null, chartDataValue);
			this.m_renderingContext = renderingContext;
		}

		// Token: 0x06001A50 RID: 6736 RVA: 0x0006A504 File Offset: 0x00068704
		internal DataValue(RenderingContext renderingContext, Microsoft.ReportingServices.ReportRendering.DataValue dataValue)
		{
			this.m_isChartValue = false;
			string text = ((dataValue != null) ? dataValue.Name : null);
			object obj = ((dataValue != null) ? dataValue.Value : null);
			this.m_name = new ReportStringProperty(true, null, null);
			this.m_value = new ReportVariantProperty(true);
			this.m_instance = new ShimDataValueInstance(text, obj);
			this.m_renderingContext = renderingContext;
		}

		// Token: 0x06001A51 RID: 6737 RVA: 0x0006A568 File Offset: 0x00068768
		internal DataValue(IReportScope reportScope, RenderingContext renderingContext, Microsoft.ReportingServices.ReportIntermediateFormat.DataValue dataValue, bool isChart, IInstancePath instancePath, string objectName)
		{
			this.m_isChartValue = isChart;
			this.m_instancePath = instancePath;
			this.m_dataValue = dataValue;
			this.m_name = new ReportStringProperty(dataValue.Name);
			this.m_value = new ReportVariantProperty(dataValue.Value);
			this.m_instance = new InternalDataValueInstance(reportScope, this);
			this.m_renderingContext = renderingContext;
			this.m_objectName = objectName;
		}

		// Token: 0x17000F00 RID: 3840
		// (get) Token: 0x06001A52 RID: 6738 RVA: 0x0006A5D0 File Offset: 0x000687D0
		public ReportStringProperty Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17000F01 RID: 3841
		// (get) Token: 0x06001A53 RID: 6739 RVA: 0x0006A5D8 File Offset: 0x000687D8
		public ReportVariantProperty Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x17000F02 RID: 3842
		// (get) Token: 0x06001A54 RID: 6740 RVA: 0x0006A5E0 File Offset: 0x000687E0
		internal Microsoft.ReportingServices.ReportIntermediateFormat.DataValue DataValueDef
		{
			get
			{
				return this.m_dataValue;
			}
		}

		// Token: 0x17000F03 RID: 3843
		// (get) Token: 0x06001A55 RID: 6741 RVA: 0x0006A5E8 File Offset: 0x000687E8
		internal RenderingContext RenderingContext
		{
			get
			{
				return this.m_renderingContext;
			}
		}

		// Token: 0x17000F04 RID: 3844
		// (get) Token: 0x06001A56 RID: 6742 RVA: 0x0006A5F0 File Offset: 0x000687F0
		internal bool IsChart
		{
			get
			{
				return this.m_isChartValue;
			}
		}

		// Token: 0x17000F05 RID: 3845
		// (get) Token: 0x06001A57 RID: 6743 RVA: 0x0006A5F8 File Offset: 0x000687F8
		internal IInstancePath InstancePath
		{
			get
			{
				return this.m_instancePath;
			}
		}

		// Token: 0x17000F06 RID: 3846
		// (get) Token: 0x06001A58 RID: 6744 RVA: 0x0006A600 File Offset: 0x00068800
		internal string ObjectName
		{
			get
			{
				return this.m_objectName;
			}
		}

		// Token: 0x17000F07 RID: 3847
		// (get) Token: 0x06001A59 RID: 6745 RVA: 0x0006A608 File Offset: 0x00068808
		public DataValueInstance Instance
		{
			get
			{
				if (this.m_renderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				return this.m_instance;
			}
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x0006A61F File Offset: 0x0006881F
		internal void UpdateChartDataValue(object dataValue)
		{
			if (this.m_dataValue != null || !this.m_isChartValue)
			{
				return;
			}
			((ShimDataValueInstance)this.m_instance).Update(null, dataValue);
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x0006A644 File Offset: 0x00068844
		internal void UpdateDataCellValue(Microsoft.ReportingServices.ReportRendering.DataValue dataValue)
		{
			if (this.m_dataValue != null || this.m_isChartValue)
			{
				return;
			}
			string text = ((dataValue != null) ? dataValue.Name : null);
			object obj = ((dataValue != null) ? dataValue.Value : null);
			((ShimDataValueInstance)this.m_instance).Update(text, obj);
		}

		// Token: 0x06001A5C RID: 6748 RVA: 0x0006A68E File Offset: 0x0006888E
		internal void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}

		// Token: 0x04000D1E RID: 3358
		private bool m_isChartValue;

		// Token: 0x04000D1F RID: 3359
		private Microsoft.ReportingServices.ReportIntermediateFormat.DataValue m_dataValue;

		// Token: 0x04000D20 RID: 3360
		private ReportStringProperty m_name;

		// Token: 0x04000D21 RID: 3361
		private ReportVariantProperty m_value;

		// Token: 0x04000D22 RID: 3362
		private DataValueInstance m_instance;

		// Token: 0x04000D23 RID: 3363
		private IInstancePath m_instancePath;

		// Token: 0x04000D24 RID: 3364
		private RenderingContext m_renderingContext;

		// Token: 0x04000D25 RID: 3365
		private string m_objectName;
	}
}
