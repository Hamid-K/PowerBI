using System;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200035E RID: 862
	internal sealed class ShimMatrixCell : ShimCell
	{
		// Token: 0x060020E2 RID: 8418 RVA: 0x0007F88D File Offset: 0x0007DA8D
		internal ShimMatrixCell(Tablix owner, int rowIndex, int colIndex, ShimMatrixMember rowParentMember, ShimMatrixMember colParentMember, bool inSubtotal)
			: base(owner, rowIndex, colIndex, inSubtotal)
		{
			this.m_rowParentMember = rowParentMember;
			this.m_colParentMember = colParentMember;
		}

		// Token: 0x17001290 RID: 4752
		// (get) Token: 0x060020E3 RID: 8419 RVA: 0x0007F8AC File Offset: 0x0007DAAC
		public override CellContents CellContents
		{
			get
			{
				if (this.m_cellContents == null)
				{
					this.m_cellContents = new CellContents(this, this.m_inSubtotal, this.CachedRenderReportItem, 1, 1, this.m_owner.RenderingContext);
				}
				else if (this.m_renderReportItem == null)
				{
					this.m_cellContents.UpdateRenderReportItem(this.CachedRenderReportItem);
				}
				return this.m_cellContents;
			}
		}

		// Token: 0x17001291 RID: 4753
		// (get) Token: 0x060020E4 RID: 8420 RVA: 0x0007F908 File Offset: 0x0007DB08
		private ReportItem CachedRenderReportItem
		{
			get
			{
				if (this.m_renderReportItem == null)
				{
					int cachedMemberCellIndex = this.m_rowParentMember.CurrentRenderMatrixMember.CachedMemberCellIndex;
					int cellIndex = this.m_colParentMember.CurrentMatrixMemberCellIndexes.GetCellIndex(this.m_colParentMember);
					MatrixCell matrixCell = this.m_owner.RenderMatrix.CellCollection[cachedMemberCellIndex, cellIndex];
					if (matrixCell != null)
					{
						this.m_renderReportItem = matrixCell.ReportItem;
					}
				}
				return this.m_renderReportItem;
			}
		}

		// Token: 0x060020E5 RID: 8421 RVA: 0x0007F972 File Offset: 0x0007DB72
		internal void ResetCellContents()
		{
			this.m_renderReportItem = null;
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}

		// Token: 0x04001085 RID: 4229
		private ReportItem m_renderReportItem;

		// Token: 0x04001086 RID: 4230
		private ShimMatrixMember m_rowParentMember;

		// Token: 0x04001087 RID: 4231
		private ShimMatrixMember m_colParentMember;
	}
}
