using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000068 RID: 104
	public sealed class DataCellCollection
	{
		// Token: 0x060006E2 RID: 1762 RVA: 0x0001A5D8 File Offset: 0x000187D8
		internal DataCellCollection(CustomReportItem owner, int rowsCount, int columnsCount)
		{
			this.m_owner = owner;
			this.m_rowsCount = rowsCount;
			this.m_columnsCount = columnsCount;
		}

		// Token: 0x17000525 RID: 1317
		public DataCell this[int row, int column]
		{
			get
			{
				if (row < 0 || row >= this.m_rowsCount)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { row, 0, this.m_rowsCount });
				}
				if (column < 0 || column >= this.m_columnsCount)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { column, 0, this.m_columnsCount });
				}
				DataCell dataCell = null;
				if (row == 0 && column == 0)
				{
					dataCell = this.m_firstCell;
				}
				else if (row == 0)
				{
					if (this.m_firstRowCells != null)
					{
						dataCell = this.m_firstRowCells[column - 1];
					}
				}
				else if (column == 0)
				{
					if (this.m_firstColumnCells != null)
					{
						dataCell = this.m_firstColumnCells[row - 1];
					}
				}
				else if (this.m_cells != null && this.m_cells[row - 1] != null)
				{
					dataCell = this.m_cells[row - 1][column - 1];
				}
				if (dataCell == null)
				{
					dataCell = new DataCell(this.m_owner, row, column);
					if (this.m_owner.UseCache)
					{
						if (row == 0 && column == 0)
						{
							this.m_firstCell = dataCell;
						}
						else if (row == 0)
						{
							if (this.m_firstRowCells == null)
							{
								this.m_firstRowCells = new DataRowCells(this.m_columnsCount - 1);
							}
							this.m_firstRowCells[column - 1] = dataCell;
						}
						else if (column == 0)
						{
							if (this.m_firstColumnCells == null)
							{
								this.m_firstColumnCells = new DataRowCells(this.m_rowsCount - 1);
							}
							this.m_firstColumnCells[row - 1] = dataCell;
						}
						else
						{
							if (this.m_cells == null)
							{
								this.m_cells = new DataRowCells[this.m_rowsCount - 1];
							}
							if (this.m_cells[row - 1] == null)
							{
								this.m_cells[row - 1] = new DataRowCells(this.m_columnsCount - 1);
							}
							this.m_cells[row - 1][column - 1] = dataCell;
						}
					}
				}
				return dataCell;
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x0001A7DA File Offset: 0x000189DA
		public int Count
		{
			get
			{
				return this.m_rowsCount * this.m_columnsCount;
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x060006E5 RID: 1765 RVA: 0x0001A7E9 File Offset: 0x000189E9
		public int RowCount
		{
			get
			{
				return this.m_rowsCount;
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x060006E6 RID: 1766 RVA: 0x0001A7F1 File Offset: 0x000189F1
		public int ColumnCount
		{
			get
			{
				return this.m_columnsCount;
			}
		}

		// Token: 0x040001E1 RID: 481
		private CustomReportItem m_owner;

		// Token: 0x040001E2 RID: 482
		private int m_columnsCount;

		// Token: 0x040001E3 RID: 483
		private int m_rowsCount;

		// Token: 0x040001E4 RID: 484
		private DataCell m_firstCell;

		// Token: 0x040001E5 RID: 485
		private DataRowCells m_firstColumnCells;

		// Token: 0x040001E6 RID: 486
		private DataRowCells m_firstRowCells;

		// Token: 0x040001E7 RID: 487
		private DataRowCells[] m_cells;
	}
}
