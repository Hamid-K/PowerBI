using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000357 RID: 855
	internal sealed class ShimTableRow : TablixRow
	{
		// Token: 0x060020B9 RID: 8377 RVA: 0x0007F240 File Offset: 0x0007D440
		internal ShimTableRow(Tablix owner, int rowIndex, Microsoft.ReportingServices.ReportRendering.TableRow renderRow)
			: base(owner, rowIndex)
		{
			this.m_cells = new List<ShimTableCell>();
			this.m_height = new ReportSize(renderRow.Height);
			TableCellCollection tableCellCollection = renderRow.TableCellCollection;
			if (tableCellCollection != null)
			{
				int count = tableCellCollection.Count;
				this.m_rowCellDefinitionMapping = new int[owner.RenderTable.Columns.Count];
				int num = 0;
				for (int i = 0; i < count; i++)
				{
					int colSpan = tableCellCollection[i].ColSpan;
					for (int j = 0; j < colSpan; j++)
					{
						this.m_rowCellDefinitionMapping[num] = ((j == 0) ? i : (-1));
						num++;
					}
					this.m_cells.Add(new ShimTableCell(owner, rowIndex, i, colSpan, tableCellCollection[i].ReportItem));
				}
			}
		}

		// Token: 0x17001274 RID: 4724
		public override TablixCell this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				if (this.m_rowCellDefinitionMapping[index] < 0)
				{
					return null;
				}
				return this.m_cells[this.m_rowCellDefinitionMapping[index]];
			}
		}

		// Token: 0x17001275 RID: 4725
		// (get) Token: 0x060020BB RID: 8379 RVA: 0x0007F36F File Offset: 0x0007D56F
		public override int Count
		{
			get
			{
				return this.m_rowCellDefinitionMapping.Length;
			}
		}

		// Token: 0x17001276 RID: 4726
		// (get) Token: 0x060020BC RID: 8380 RVA: 0x0007F379 File Offset: 0x0007D579
		public override ReportSize Height
		{
			get
			{
				return this.m_height;
			}
		}

		// Token: 0x060020BD RID: 8381 RVA: 0x0007F384 File Offset: 0x0007D584
		internal void UpdateCells(Microsoft.ReportingServices.ReportRendering.TableRow renderRow)
		{
			int count = this.m_cells.Count;
			TableCellCollection tableCellCollection = ((renderRow != null) ? renderRow.TableCellCollection : null);
			for (int i = 0; i < count; i++)
			{
				this.m_cells[i].SetCellContents((tableCellCollection != null) ? tableCellCollection[i] : null);
			}
		}

		// Token: 0x04001071 RID: 4209
		private List<ShimTableCell> m_cells;

		// Token: 0x04001072 RID: 4210
		private ReportSize m_height;

		// Token: 0x04001073 RID: 4211
		private int[] m_rowCellDefinitionMapping;
	}
}
