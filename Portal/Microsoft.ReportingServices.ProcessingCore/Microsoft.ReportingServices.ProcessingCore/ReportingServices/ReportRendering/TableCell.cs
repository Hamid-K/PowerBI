using System;
using System.Globalization;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000041 RID: 65
	internal sealed class TableCell
	{
		// Token: 0x06000575 RID: 1397 RVA: 0x000125A5 File Offset: 0x000107A5
		internal TableCell(Table tableDef, int index, TableCellCollection cells)
		{
			this.m_tableDef = tableDef;
			this.m_index = index;
			this.m_cells = cells;
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x000125C4 File Offset: 0x000107C4
		public ReportItem ReportItem
		{
			get
			{
				ReportItemCollection reportItems = this.m_cells.ReportItems;
				if (reportItems == null)
				{
					return null;
				}
				return reportItems[this.m_index];
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x000125EE File Offset: 0x000107EE
		public int ColSpan
		{
			get
			{
				return this.m_cells.ColSpans[this.m_index];
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x00012608 File Offset: 0x00010808
		public string ID
		{
			get
			{
				string[] array = this.m_cells.RowDef.RenderingModelIDs;
				if (array == null)
				{
					array = new string[this.m_cells.RowDef.IDs.Count];
					this.m_cells.RowDef.RenderingModelIDs = array;
				}
				if (array[this.m_index] == null)
				{
					array[this.m_index] = this.m_cells.RowDef.IDs[this.m_index].ToString(CultureInfo.InvariantCulture);
				}
				return array[this.m_index];
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x00012698 File Offset: 0x00010898
		// (set) Token: 0x0600057A RID: 1402 RVA: 0x000126E4 File Offset: 0x000108E4
		public object SharedRenderingInfo
		{
			get
			{
				int num = this.m_cells.RowDef.IDs[this.m_index];
				return this.m_cells.RenderingContext.RenderingInfoManager.SharedRenderingInfo[num];
			}
			set
			{
				int num = this.m_cells.RowDef.IDs[this.m_index];
				this.m_cells.RenderingContext.RenderingInfoManager.SharedRenderingInfo[num] = value;
			}
		}

		// Token: 0x0400013B RID: 315
		private Table m_tableDef;

		// Token: 0x0400013C RID: 316
		private int m_index;

		// Token: 0x0400013D RID: 317
		private TableCellCollection m_cells;
	}
}
