using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000053 RID: 83
	internal sealed class ChartSeriesDataPoints
	{
		// Token: 0x06000617 RID: 1559 RVA: 0x0001511C File Offset: 0x0001331C
		internal ChartSeriesDataPoints(int count)
		{
			this.m_count = count;
		}

		// Token: 0x170004C5 RID: 1221
		internal ChartDataPoint this[int index]
		{
			get
			{
				if (index < 0 || index >= this.m_count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.m_count });
				}
				if (this.m_seriesCells != null)
				{
					return this.m_seriesCells[index];
				}
				return null;
			}
			set
			{
				if (index < 0 || index >= this.m_count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.m_count });
				}
				if (this.m_seriesCells == null)
				{
					this.m_seriesCells = new ChartDataPoint[this.m_count];
				}
				this.m_seriesCells[index] = value;
			}
		}

		// Token: 0x0400018A RID: 394
		private int m_count;

		// Token: 0x0400018B RID: 395
		private ChartDataPoint[] m_seriesCells;
	}
}
