using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x0200006A RID: 106
	public sealed class DataCell
	{
		// Token: 0x060006EA RID: 1770 RVA: 0x0001A8D8 File Offset: 0x00018AD8
		internal DataCell(CustomReportItem owner, int rowIndex, int columnIndex)
		{
			this.m_owner = owner;
			this.m_rowIndex = rowIndex;
			this.m_columnIndex = columnIndex;
			if (!owner.CustomData.NoRows)
			{
				CustomReportItemCellInstancesList cells = this.m_owner.CriInstance.Cells;
				this.m_cellInstance = cells[rowIndex][columnIndex];
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x060006EB RID: 1771 RVA: 0x0001A934 File Offset: 0x00018B34
		public DataValueCollection DataValues
		{
			get
			{
				if (this.m_cellInstance == null)
				{
					return null;
				}
				DataValueCollection dataValueCollection = this.m_dataValueCollection;
				if (this.m_dataValueCollection == null)
				{
					dataValueCollection = new DataValueCollection(this.GetCellDefinition(), this.m_cellInstance.DataValueInstances);
					if (this.m_owner.UseCache)
					{
						this.m_dataValueCollection = dataValueCollection;
					}
				}
				return dataValueCollection;
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x060006EC RID: 1772 RVA: 0x0001A986 File Offset: 0x00018B86
		internal int ColumnIndex
		{
			get
			{
				if (this.m_cellInstance == null)
				{
					return -1;
				}
				return this.m_cellInstance.ColumnIndex;
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x060006ED RID: 1773 RVA: 0x0001A99D File Offset: 0x00018B9D
		internal int RowIndex
		{
			get
			{
				if (this.m_cellInstance == null)
				{
					return -1;
				}
				return this.m_cellInstance.RowIndex;
			}
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x0001A9B4 File Offset: 0x00018BB4
		private DataValueCRIList GetCellDefinition()
		{
			Global.Tracer.Assert(!this.m_owner.CustomData.NoRows && this.m_owner.CriDefinition.DataRowCells != null && this.m_cellInstance.RowIndex < this.m_owner.CriDefinition.DataRowCells.Count && this.m_cellInstance.ColumnIndex < this.m_owner.CriDefinition.DataRowCells[this.m_cellInstance.RowIndex].Count && 0 < this.m_owner.CriDefinition.DataRowCells[this.m_cellInstance.RowIndex][this.m_cellInstance.ColumnIndex].Count);
			return this.m_owner.CriDefinition.DataRowCells[this.m_cellInstance.RowIndex][this.m_cellInstance.ColumnIndex];
		}

		// Token: 0x040001EA RID: 490
		private CustomReportItem m_owner;

		// Token: 0x040001EB RID: 491
		private int m_rowIndex;

		// Token: 0x040001EC RID: 492
		private int m_columnIndex;

		// Token: 0x040001ED RID: 493
		private CustomReportItemCellInstance m_cellInstance;

		// Token: 0x040001EE RID: 494
		private DataValueCollection m_dataValueCollection;
	}
}
