using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x0200003A RID: 58
	internal sealed class MatrixCellCollection
	{
		// Token: 0x06000512 RID: 1298 RVA: 0x00010229 File Offset: 0x0000E429
		internal MatrixCellCollection(Matrix owner, int rowsCount, int columnsCount)
		{
			this.m_owner = owner;
			this.m_rowsCount = rowsCount;
			this.m_columnsCount = columnsCount;
		}

		// Token: 0x17000409 RID: 1033
		public MatrixCell this[int row, int column]
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
				MatrixCell matrixCell = null;
				if (row == 0 && column == 0)
				{
					matrixCell = this.m_firstCell;
				}
				else if (row == 0)
				{
					if (this.m_firstMatrixRowCells != null)
					{
						matrixCell = this.m_firstMatrixRowCells[column - 1];
					}
				}
				else if (column == 0)
				{
					if (this.m_firstMatrixColumnCells != null)
					{
						matrixCell = this.m_firstMatrixColumnCells[row - 1];
					}
				}
				else if (this.m_cells != null && this.m_cells[row - 1] != null)
				{
					matrixCell = this.m_cells[row - 1][column - 1];
				}
				if (matrixCell == null)
				{
					matrixCell = new MatrixCell(this.m_owner, row, column);
					if (this.m_owner.RenderingContext.CacheState)
					{
						if (row == 0 && column == 0)
						{
							this.m_firstCell = matrixCell;
						}
						else if (row == 0)
						{
							if (this.m_firstMatrixRowCells == null)
							{
								this.m_firstMatrixRowCells = new MatrixRowCells(this.m_columnsCount - 1);
							}
							this.m_firstMatrixRowCells[column - 1] = matrixCell;
						}
						else if (column == 0)
						{
							if (this.m_firstMatrixColumnCells == null)
							{
								this.m_firstMatrixColumnCells = new MatrixRowCells(this.m_rowsCount - 1);
							}
							this.m_firstMatrixColumnCells[row - 1] = matrixCell;
						}
						else
						{
							if (this.m_cells == null)
							{
								this.m_cells = new MatrixRowCells[this.m_rowsCount - 1];
							}
							if (this.m_cells[row - 1] == null)
							{
								this.m_cells[row - 1] = new MatrixRowCells(this.m_columnsCount - 1);
							}
							this.m_cells[row - 1][column - 1] = matrixCell;
						}
					}
				}
				return matrixCell;
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x0001042F File Offset: 0x0000E62F
		public int Count
		{
			get
			{
				return this.m_rowsCount * this.m_columnsCount;
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x0001043E File Offset: 0x0000E63E
		public int RowCount
		{
			get
			{
				return this.m_rowsCount;
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x00010446 File Offset: 0x0000E646
		public int ColumnCount
		{
			get
			{
				return this.m_columnsCount;
			}
		}

		// Token: 0x0400010D RID: 269
		private Matrix m_owner;

		// Token: 0x0400010E RID: 270
		private int m_columnsCount;

		// Token: 0x0400010F RID: 271
		private int m_rowsCount;

		// Token: 0x04000110 RID: 272
		private MatrixCell m_firstCell;

		// Token: 0x04000111 RID: 273
		private MatrixRowCells m_firstMatrixColumnCells;

		// Token: 0x04000112 RID: 274
		private MatrixRowCells m_firstMatrixRowCells;

		// Token: 0x04000113 RID: 275
		private MatrixRowCells[] m_cells;
	}
}
