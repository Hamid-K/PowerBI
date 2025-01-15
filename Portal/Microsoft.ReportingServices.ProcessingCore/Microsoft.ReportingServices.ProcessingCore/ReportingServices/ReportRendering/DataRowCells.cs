using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000069 RID: 105
	internal sealed class DataRowCells
	{
		// Token: 0x060006E7 RID: 1767 RVA: 0x0001A7F9 File Offset: 0x000189F9
		internal DataRowCells(int count)
		{
			this.m_count = count;
		}

		// Token: 0x17000529 RID: 1321
		internal DataCell this[int index]
		{
			get
			{
				if (index < 0 || index >= this.m_count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.m_count });
				}
				if (this.m_rowCells != null)
				{
					return this.m_rowCells[index];
				}
				return null;
			}
			set
			{
				if (index < 0 || index >= this.m_count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.m_count });
				}
				if (this.m_rowCells == null)
				{
					this.m_rowCells = new DataCell[this.m_count];
				}
				this.m_rowCells[index] = value;
			}
		}

		// Token: 0x040001E8 RID: 488
		private int m_count;

		// Token: 0x040001E9 RID: 489
		private DataCell[] m_rowCells;
	}
}
