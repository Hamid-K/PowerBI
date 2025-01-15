using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002AE RID: 686
	public sealed class DataValueCollection : ReportElementCollectionBase<Microsoft.ReportingServices.OnDemandReportRendering.DataValue>
	{
		// Token: 0x06001A46 RID: 6726 RVA: 0x0006A1A8 File Offset: 0x000683A8
		internal DataValueCollection(Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext, Microsoft.ReportingServices.ReportRendering.ChartDataPoint dataPoint)
		{
			this.m_isChartValues = true;
			if (dataPoint.DataValues == null)
			{
				this.m_cachedDataValues = new Microsoft.ReportingServices.OnDemandReportRendering.DataValue[0];
				return;
			}
			int num = dataPoint.DataValues.Length;
			this.m_cachedDataValues = new Microsoft.ReportingServices.OnDemandReportRendering.DataValue[num];
			for (int i = 0; i < num; i++)
			{
				this.m_cachedDataValues[i] = new Microsoft.ReportingServices.OnDemandReportRendering.DataValue(renderingContext, dataPoint.DataValues[i]);
			}
		}

		// Token: 0x06001A47 RID: 6727 RVA: 0x0006A210 File Offset: 0x00068410
		internal DataValueCollection(Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext, Microsoft.ReportingServices.ReportRendering.DataCell dataCell)
		{
			this.m_isChartValues = false;
			if (dataCell.DataValues == null)
			{
				this.m_cachedDataValues = new Microsoft.ReportingServices.OnDemandReportRendering.DataValue[0];
				return;
			}
			int count = dataCell.DataValues.Count;
			this.m_cachedDataValues = new Microsoft.ReportingServices.OnDemandReportRendering.DataValue[count];
			for (int i = 0; i < count; i++)
			{
				this.m_cachedDataValues[i] = new Microsoft.ReportingServices.OnDemandReportRendering.DataValue(renderingContext, dataCell.DataValues[i]);
			}
		}

		// Token: 0x06001A48 RID: 6728 RVA: 0x0006A27D File Offset: 0x0006847D
		internal DataValueCollection(Cell cell, IReportScope reportScope, Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext, IList<Microsoft.ReportingServices.ReportIntermediateFormat.DataValue> dataValues, string objectName, bool isChart)
		{
			this.m_isChartValues = isChart;
			this.m_cell = cell;
			this.m_reportScope = reportScope;
			this.m_dataValues = dataValues;
			this.m_renderingContext = renderingContext;
			this.m_objectName = objectName;
		}

		// Token: 0x17000EFD RID: 3837
		public Microsoft.ReportingServices.OnDemandReportRendering.DataValue this[string name]
		{
			get
			{
				foreach (Microsoft.ReportingServices.OnDemandReportRendering.DataValue dataValue in this)
				{
					if (dataValue != null && string.CompareOrdinal(name, dataValue.Instance.Name) == 0)
					{
						return dataValue;
					}
				}
				return null;
			}
		}

		// Token: 0x17000EFE RID: 3838
		public override Microsoft.ReportingServices.OnDemandReportRendering.DataValue this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				Microsoft.ReportingServices.OnDemandReportRendering.DataValue dataValue = null;
				if (this.m_cachedDataValues == null)
				{
					this.m_cachedDataValues = new Microsoft.ReportingServices.OnDemandReportRendering.DataValue[this.m_dataValues.Count];
				}
				else
				{
					dataValue = this.m_cachedDataValues[index];
				}
				if (dataValue == null)
				{
					dataValue = new Microsoft.ReportingServices.OnDemandReportRendering.DataValue(this.m_reportScope, this.m_renderingContext, this.m_dataValues[index], this.m_isChartValues, this.m_cell, this.m_objectName);
					this.m_cachedDataValues[index] = dataValue;
				}
				return dataValue;
			}
		}

		// Token: 0x17000EFF RID: 3839
		// (get) Token: 0x06001A4B RID: 6731 RVA: 0x0006A3C7 File Offset: 0x000685C7
		public override int Count
		{
			get
			{
				if (this.m_cachedDataValues != null)
				{
					return this.m_cachedDataValues.Length;
				}
				if (this.m_dataValues != null)
				{
					return this.m_dataValues.Count;
				}
				return 0;
			}
		}

		// Token: 0x06001A4C RID: 6732 RVA: 0x0006A3F0 File Offset: 0x000685F0
		internal void UpdateChartDataValues(object[] datavalues)
		{
			if (!this.m_isChartValues)
			{
				return;
			}
			int num = this.m_cachedDataValues.Length;
			for (int i = 0; i < num; i++)
			{
				this.m_cachedDataValues[i].UpdateChartDataValue((datavalues == null) ? null : datavalues[i]);
			}
		}

		// Token: 0x06001A4D RID: 6733 RVA: 0x0006A434 File Offset: 0x00068634
		internal void UpdateDataCellValues(Microsoft.ReportingServices.ReportRendering.DataCell dataCell)
		{
			if (this.m_isChartValues)
			{
				return;
			}
			int num = this.m_cachedDataValues.Length;
			for (int i = 0; i < num; i++)
			{
				this.m_cachedDataValues[i].UpdateDataCellValue((dataCell == null || dataCell.DataValues == null) ? null : dataCell.DataValues[i]);
			}
		}

		// Token: 0x06001A4E RID: 6734 RVA: 0x0006A488 File Offset: 0x00068688
		internal void SetNewContext()
		{
			if (this.m_cachedDataValues != null)
			{
				for (int i = 0; i < this.m_cachedDataValues.Length; i++)
				{
					if (this.m_cachedDataValues[i] != null)
					{
						this.m_cachedDataValues[i].SetNewContext();
					}
				}
			}
		}

		// Token: 0x04000D17 RID: 3351
		private bool m_isChartValues;

		// Token: 0x04000D18 RID: 3352
		private Microsoft.ReportingServices.OnDemandReportRendering.DataValue[] m_cachedDataValues;

		// Token: 0x04000D19 RID: 3353
		private IList<Microsoft.ReportingServices.ReportIntermediateFormat.DataValue> m_dataValues;

		// Token: 0x04000D1A RID: 3354
		private IReportScope m_reportScope;

		// Token: 0x04000D1B RID: 3355
		private Cell m_cell;

		// Token: 0x04000D1C RID: 3356
		private Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext m_renderingContext;

		// Token: 0x04000D1D RID: 3357
		private string m_objectName;
	}
}
