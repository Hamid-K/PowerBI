using System;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200035D RID: 861
	internal sealed class ShimTableCell : ShimCell
	{
		// Token: 0x060020DF RID: 8415 RVA: 0x0007F7D4 File Offset: 0x0007D9D4
		internal ShimTableCell(Tablix owner, int rowIndex, int colIndex, int colSpan, ReportItem renderReportItem)
			: base(owner, rowIndex, colIndex, owner.InSubtotal)
		{
			this.m_colSpan = colSpan;
			this.m_renderReportItem = renderReportItem;
		}

		// Token: 0x1700128F RID: 4751
		// (get) Token: 0x060020E0 RID: 8416 RVA: 0x0007F7F5 File Offset: 0x0007D9F5
		public override CellContents CellContents
		{
			get
			{
				if (this.m_cellContents == null)
				{
					this.m_cellContents = new CellContents(this, this.m_inSubtotal, this.m_renderReportItem, 1, this.m_colSpan, this.m_owner.RenderingContext);
				}
				return this.m_cellContents;
			}
		}

		// Token: 0x060020E1 RID: 8417 RVA: 0x0007F830 File Offset: 0x0007DA30
		internal void SetCellContents(TableCell renderCellContents)
		{
			if (renderCellContents != null)
			{
				this.m_renderCellContents = renderCellContents;
				if (renderCellContents.ReportItem != null)
				{
					this.m_renderReportItem = renderCellContents.ReportItem;
				}
			}
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_cellContents != null)
			{
				this.m_cellContents.UpdateRenderReportItem((renderCellContents != null) ? renderCellContents.ReportItem : null);
			}
		}

		// Token: 0x04001082 RID: 4226
		private int m_colSpan;

		// Token: 0x04001083 RID: 4227
		private TableCell m_renderCellContents;

		// Token: 0x04001084 RID: 4228
		private ReportItem m_renderReportItem;
	}
}
