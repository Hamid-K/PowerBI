using System;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200035C RID: 860
	internal sealed class ShimListCell : ShimCell
	{
		// Token: 0x060020DC RID: 8412 RVA: 0x0007F70D File Offset: 0x0007D90D
		internal ShimListCell(Tablix owner)
			: base(owner, 0, 0, owner.InSubtotal)
		{
			this.m_currentListContents = owner.RenderList.Contents[0];
		}

		// Token: 0x1700128E RID: 4750
		// (get) Token: 0x060020DD RID: 8413 RVA: 0x0007F738 File Offset: 0x0007D938
		public override CellContents CellContents
		{
			get
			{
				if (this.m_cellContents == null)
				{
					this.m_shimContainer = new Rectangle(this, this.m_inSubtotal, this.m_currentListContents, this.m_owner.RenderingContext);
					this.m_cellContents = new CellContents(this.m_shimContainer, 1, 1, this.m_owner.RenderingContext);
				}
				return this.m_cellContents;
			}
		}

		// Token: 0x060020DE RID: 8414 RVA: 0x0007F794 File Offset: 0x0007D994
		internal void SetCellContents(ListContent renderContents)
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			this.m_currentListContents = renderContents;
			if (this.m_shimContainer != null)
			{
				this.m_cellContents.SetNewContext();
				this.m_shimContainer.UpdateListContents(this.m_currentListContents);
			}
		}

		// Token: 0x04001080 RID: 4224
		private ListContent m_currentListContents;

		// Token: 0x04001081 RID: 4225
		private Rectangle m_shimContainer;
	}
}
