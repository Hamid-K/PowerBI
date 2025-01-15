using System;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000356 RID: 854
	internal sealed class ShimListRow : TablixRow
	{
		// Token: 0x060020B4 RID: 8372 RVA: 0x0007F197 File Offset: 0x0007D397
		internal ShimListRow(Tablix owner)
			: base(owner, 0)
		{
			this.m_cell = new ShimListCell(owner);
		}

		// Token: 0x17001271 RID: 4721
		public override TablixCell this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				return this.m_cell;
			}
		}

		// Token: 0x17001272 RID: 4722
		// (get) Token: 0x060020B6 RID: 8374 RVA: 0x0007F201 File Offset: 0x0007D401
		public override int Count
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17001273 RID: 4723
		// (get) Token: 0x060020B7 RID: 8375 RVA: 0x0007F204 File Offset: 0x0007D404
		public override ReportSize Height
		{
			get
			{
				if (this.m_height == null)
				{
					this.m_height = new ReportSize(this.m_owner.RenderList.Height);
				}
				return this.m_height;
			}
		}

		// Token: 0x060020B8 RID: 8376 RVA: 0x0007F22F File Offset: 0x0007D42F
		internal void UpdateCells(ListContent renderContents)
		{
			this.m_cell.SetCellContents(renderContents);
		}

		// Token: 0x0400106F RID: 4207
		private ReportSize m_height;

		// Token: 0x04001070 RID: 4208
		private ShimListCell m_cell;
	}
}
