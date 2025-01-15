using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000358 RID: 856
	internal sealed class ShimMatrixRow : TablixRow
	{
		// Token: 0x060020BE RID: 8382 RVA: 0x0007F3D4 File Offset: 0x0007D5D4
		internal ShimMatrixRow(Tablix owner, int rowIndex, ShimMatrixMember rowParentMember, bool inSubtotalRow)
			: base(owner, rowIndex)
		{
			this.m_cells = new List<ShimMatrixCell>();
			this.GenerateMatrixCells(rowParentMember, null, owner.ColumnHierarchy.MemberCollection as ShimMatrixMemberCollection, inSubtotalRow, inSubtotalRow);
		}

		// Token: 0x060020BF RID: 8383 RVA: 0x0007F408 File Offset: 0x0007D608
		private void GenerateMatrixCells(ShimMatrixMember rowParentMember, ShimMatrixMember colParentMember, ShimMatrixMemberCollection columnMembers, bool inSubtotalRow, bool inSubtotalColumn)
		{
			if (columnMembers == null)
			{
				this.m_cells.Add(new ShimMatrixCell(this.m_owner, this.m_rowIndex, this.m_cells.Count, rowParentMember, colParentMember, inSubtotalRow || inSubtotalColumn));
				return;
			}
			int count = columnMembers.Count;
			for (int i = 0; i < count; i++)
			{
				ShimMatrixMember shimMatrixMember = columnMembers[i] as ShimMatrixMember;
				this.GenerateMatrixCells(rowParentMember, shimMatrixMember, shimMatrixMember.Children as ShimMatrixMemberCollection, inSubtotalRow, inSubtotalColumn || shimMatrixMember.CurrentRenderMatrixMember.IsTotal);
			}
		}

		// Token: 0x17001277 RID: 4727
		public override TablixCell this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				return this.m_cells[index];
			}
		}

		// Token: 0x17001278 RID: 4728
		// (get) Token: 0x060020C1 RID: 8385 RVA: 0x0007F4E7 File Offset: 0x0007D6E7
		public override int Count
		{
			get
			{
				return this.m_cells.Count;
			}
		}

		// Token: 0x17001279 RID: 4729
		// (get) Token: 0x060020C2 RID: 8386 RVA: 0x0007F4F4 File Offset: 0x0007D6F4
		public override ReportSize Height
		{
			get
			{
				if (this.m_height == null)
				{
					int num = this.m_owner.MatrixRowDefinitionMapping[this.m_rowIndex];
					this.m_height = new ReportSize(this.m_owner.RenderMatrix.CellHeights[num]);
				}
				return this.m_height;
			}
		}

		// Token: 0x04001074 RID: 4212
		private List<ShimMatrixCell> m_cells;

		// Token: 0x04001075 RID: 4213
		private ReportSize m_height;
	}
}
