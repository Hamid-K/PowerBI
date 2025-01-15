using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000280 RID: 640
	internal sealed class InternalDataRow : DataRow
	{
		// Token: 0x060018EE RID: 6382 RVA: 0x00066638 File Offset: 0x00064838
		internal InternalDataRow(Microsoft.ReportingServices.OnDemandReportRendering.CustomReportItem owner, int rowIndex, CustomDataRow rowDef)
			: base(owner, rowIndex)
		{
			this.m_rowDef = rowDef;
		}

		// Token: 0x17000E46 RID: 3654
		public override DataCell this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				if (this.m_cachedDataCells == null)
				{
					this.m_cachedDataCells = new DataCell[this.Count];
				}
				if (this.m_cachedDataCells[index] == null)
				{
					this.m_cachedDataCells[index] = new InternalDataCell(this.m_owner, this.m_rowIndex, index, this.m_rowDef.DataCells[index]);
				}
				return this.m_cachedDataCells[index];
			}
		}

		// Token: 0x17000E47 RID: 3655
		// (get) Token: 0x060018F0 RID: 6384 RVA: 0x000666ED File Offset: 0x000648ED
		public override int Count
		{
			get
			{
				return this.m_rowDef.Cells.Count;
			}
		}

		// Token: 0x04000C93 RID: 3219
		private CustomDataRow m_rowDef;
	}
}
