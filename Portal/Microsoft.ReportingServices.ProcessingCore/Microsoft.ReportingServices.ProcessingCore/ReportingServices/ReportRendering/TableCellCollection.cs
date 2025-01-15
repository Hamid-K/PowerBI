using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000040 RID: 64
	internal sealed class TableCellCollection
	{
		// Token: 0x0600056E RID: 1390 RVA: 0x00012444 File Offset: 0x00010644
		internal TableCellCollection(Table table, TableRow rowDef, TableRowInstance rowInstance)
		{
			this.m_table = table;
			this.m_rowDef = rowDef;
			this.m_rowInstance = rowInstance;
		}

		// Token: 0x1700044E RID: 1102
		public TableCell this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				TableCell tableCell;
				if (this.m_cells == null || this.m_cells[index] == null)
				{
					tableCell = new TableCell((Table)this.m_table.ReportItemDef, index, this);
					if (this.m_table.RenderingContext.CacheState)
					{
						if (this.m_cells == null)
						{
							this.m_cells = new TableCell[this.Count];
						}
						this.m_cells[index] = tableCell;
					}
				}
				else
				{
					tableCell = this.m_cells[index];
				}
				return tableCell;
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x0001251B File Offset: 0x0001071B
		public int Count
		{
			get
			{
				return this.m_rowDef.ReportItems.Count;
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x00012530 File Offset: 0x00010730
		internal ReportItemCollection ReportItems
		{
			get
			{
				if (this.m_cellReportItems == null)
				{
					this.m_cellReportItems = new ReportItemCollection(this.m_rowDef.ReportItems, (this.m_rowInstance == null) ? null : this.m_rowInstance.TableRowReportItemColInstance, this.m_table.RenderingContext, null);
				}
				return this.m_cellReportItems;
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x00012583 File Offset: 0x00010783
		internal IntList ColSpans
		{
			get
			{
				return this.m_rowDef.ColSpans;
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x00012590 File Offset: 0x00010790
		internal TableRow RowDef
		{
			get
			{
				return this.m_rowDef;
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x00012598 File Offset: 0x00010798
		internal RenderingContext RenderingContext
		{
			get
			{
				return this.m_table.RenderingContext;
			}
		}

		// Token: 0x04000136 RID: 310
		private Table m_table;

		// Token: 0x04000137 RID: 311
		private TableCell[] m_cells;

		// Token: 0x04000138 RID: 312
		private ReportItemCollection m_cellReportItems;

		// Token: 0x04000139 RID: 313
		private TableRow m_rowDef;

		// Token: 0x0400013A RID: 314
		private TableRowInstance m_rowInstance;
	}
}
