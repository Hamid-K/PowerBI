using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000360 RID: 864
	public sealed class TablixCornerRowCollection : ReportElementCollectionBase<TablixCornerRow>
	{
		// Token: 0x060020EA RID: 8426 RVA: 0x0007F9E8 File Offset: 0x0007DBE8
		internal TablixCornerRowCollection(Tablix owner)
		{
			this.m_owner = owner;
		}

		// Token: 0x17001293 RID: 4755
		public override TablixCornerRow this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				if (this.m_cornerRows == null)
				{
					this.m_cornerRows = new TablixCornerRow[this.Count];
				}
				TablixCornerRow tablixCornerRow = this.m_cornerRows[index];
				if (tablixCornerRow == null)
				{
					if (this.m_owner.IsOldSnapshot)
					{
						tablixCornerRow = (this.m_cornerRows[index] = new TablixCornerRow(this.m_owner, index, this.m_owner.RenderMatrix.Corner));
					}
					else
					{
						tablixCornerRow = (this.m_cornerRows[index] = new TablixCornerRow(this.m_owner, index, this.m_owner.TablixDef.Corner[index]));
					}
				}
				return tablixCornerRow;
			}
		}

		// Token: 0x17001294 RID: 4756
		// (get) Token: 0x060020EC RID: 8428 RVA: 0x0007FACC File Offset: 0x0007DCCC
		public override int Count
		{
			get
			{
				if (this.m_owner.IsOldSnapshot)
				{
					if (DataRegion.Type.Matrix == this.m_owner.SnapshotTablixType && this.m_owner.RenderMatrix.Corner != null)
					{
						return this.m_owner.Columns;
					}
					return 0;
				}
				else
				{
					if (this.m_owner.TablixDef.Corner != null)
					{
						return this.m_owner.TablixDef.Corner.Count;
					}
					return 0;
				}
			}
		}

		// Token: 0x060020ED RID: 8429 RVA: 0x0007FB40 File Offset: 0x0007DD40
		internal void ResetContext()
		{
			if (this.m_owner.IsOldSnapshot && 0 < this.Count && this.m_cornerRows != null && this.m_cornerRows[0] != null)
			{
				this.m_cornerRows[0].UpdateRenderReportItem(this.m_owner.RenderMatrix.Corner);
			}
		}

		// Token: 0x060020EE RID: 8430 RVA: 0x0007FB94 File Offset: 0x0007DD94
		internal void SetNewContext()
		{
			if (!this.m_owner.IsOldSnapshot && this.m_cornerRows != null)
			{
				for (int i = 0; i < this.m_cornerRows.Length; i++)
				{
					TablixCornerRow tablixCornerRow = this.m_cornerRows[i];
					if (tablixCornerRow != null)
					{
						tablixCornerRow.SetNewContext();
					}
				}
			}
		}

		// Token: 0x0400108A RID: 4234
		private Tablix m_owner;

		// Token: 0x0400108B RID: 4235
		private TablixCornerRow[] m_cornerRows;
	}
}
