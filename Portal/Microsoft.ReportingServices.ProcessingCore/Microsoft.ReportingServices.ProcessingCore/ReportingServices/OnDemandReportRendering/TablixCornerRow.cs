using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000361 RID: 865
	public sealed class TablixCornerRow : ReportElementCollectionBase<TablixCornerCell>
	{
		// Token: 0x060020EF RID: 8431 RVA: 0x0007FBDB File Offset: 0x0007DDDB
		internal TablixCornerRow(Microsoft.ReportingServices.OnDemandReportRendering.Tablix owner, int rowIndex, List<TablixCornerCell> rowDef)
		{
			this.m_owner = owner;
			this.m_rowIndex = rowIndex;
			this.m_rowDef = rowDef;
			this.m_cellROMDefs = new TablixCornerCell[rowDef.Count];
		}

		// Token: 0x060020F0 RID: 8432 RVA: 0x0007FC09 File Offset: 0x0007DE09
		internal TablixCornerRow(Microsoft.ReportingServices.OnDemandReportRendering.Tablix owner, int rowIndex, Microsoft.ReportingServices.ReportRendering.ReportItem cornerDef)
		{
			this.m_owner = owner;
			this.m_rowIndex = rowIndex;
			this.m_cornerDef = cornerDef;
			this.m_cellROMDefs = new TablixCornerCell[this.m_owner.Rows];
		}

		// Token: 0x17001295 RID: 4757
		public override TablixCornerCell this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				if (this.m_cellROMDefs[index] == null)
				{
					if (this.m_owner.IsOldSnapshot)
					{
						if (this.m_rowIndex == 0 && index == 0)
						{
							this.m_cellROMDefs[index] = new TablixCornerCell(this.m_owner, this.m_rowIndex, index, this.m_cornerDef);
						}
					}
					else
					{
						TablixCornerCell tablixCornerCell = this.m_rowDef[index];
						if (tablixCornerCell.RowSpan > 0 && tablixCornerCell.ColSpan > 0)
						{
							this.m_cellROMDefs[index] = new TablixCornerCell(this.m_owner, this.m_rowIndex, index, tablixCornerCell);
						}
					}
				}
				return this.m_cellROMDefs[index];
			}
		}

		// Token: 0x060020F2 RID: 8434 RVA: 0x0007FD10 File Offset: 0x0007DF10
		internal void SetNewContext()
		{
			if (!this.m_owner.IsOldSnapshot)
			{
				for (int i = 0; i < this.m_cellROMDefs.Length; i++)
				{
					TablixCornerCell tablixCornerCell = this.m_cellROMDefs[i];
					if (tablixCornerCell != null)
					{
						tablixCornerCell.CellContents.SetNewContext();
					}
				}
			}
		}

		// Token: 0x17001296 RID: 4758
		// (get) Token: 0x060020F3 RID: 8435 RVA: 0x0007FD54 File Offset: 0x0007DF54
		public override int Count
		{
			get
			{
				if (!this.m_owner.IsOldSnapshot)
				{
					return this.m_rowDef.Count;
				}
				if (Microsoft.ReportingServices.OnDemandReportRendering.DataRegion.Type.Matrix == this.m_owner.SnapshotTablixType && this.m_owner.RenderMatrix.Corner != null)
				{
					return this.m_owner.Rows;
				}
				return 0;
			}
		}

		// Token: 0x060020F4 RID: 8436 RVA: 0x0007FDA7 File Offset: 0x0007DFA7
		internal void UpdateRenderReportItem(Microsoft.ReportingServices.ReportRendering.ReportItem cornerDef)
		{
			this.m_cornerDef = cornerDef;
		}

		// Token: 0x0400108C RID: 4236
		private Microsoft.ReportingServices.OnDemandReportRendering.Tablix m_owner;

		// Token: 0x0400108D RID: 4237
		private int m_rowIndex;

		// Token: 0x0400108E RID: 4238
		private List<TablixCornerCell> m_rowDef;

		// Token: 0x0400108F RID: 4239
		private Microsoft.ReportingServices.ReportRendering.ReportItem m_cornerDef;

		// Token: 0x04001090 RID: 4240
		private TablixCornerCell[] m_cellROMDefs;
	}
}
