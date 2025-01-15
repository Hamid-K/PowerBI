using System;
using System.Collections;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000057 RID: 87
	internal sealed class OWCChartColumn
	{
		// Token: 0x06000666 RID: 1638 RVA: 0x00018E20 File Offset: 0x00017020
		internal OWCChartColumn(ChartColumn chartColumnDef, ArrayList columnData)
		{
			this.m_chartColumnDef = chartColumnDef;
			this.m_data = columnData;
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06000667 RID: 1639 RVA: 0x00018E36 File Offset: 0x00017036
		public string Name
		{
			get
			{
				return this.m_chartColumnDef.Name;
			}
		}

		// Token: 0x170004D5 RID: 1237
		public object this[int index]
		{
			get
			{
				if (0 > index || index >= this.m_data.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				if (this.m_chartColumnDef.Value.Type == ExpressionInfo.Types.Constant)
				{
					return this.m_chartColumnDef.Value.Value;
				}
				if (this.m_data != null)
				{
					return this.m_data[index];
				}
				return null;
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06000669 RID: 1641 RVA: 0x00018ECE File Offset: 0x000170CE
		public int Count
		{
			get
			{
				if (this.m_data != null)
				{
					return this.m_data.Count;
				}
				return 0;
			}
		}

		// Token: 0x040001A7 RID: 423
		private ChartColumn m_chartColumnDef;

		// Token: 0x040001A8 RID: 424
		private ArrayList m_data;
	}
}
