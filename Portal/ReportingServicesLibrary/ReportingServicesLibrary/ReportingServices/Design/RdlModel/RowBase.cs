using System;
using System.Collections;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x0200041B RID: 1051
	public abstract class RowBase
	{
		// Token: 0x06002170 RID: 8560 RVA: 0x00080FB6 File Offset: 0x0007F1B6
		protected RowBase()
		{
			this.m_cells = new ArrayList();
		}

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x06002171 RID: 8561 RVA: 0x00080FC9 File Offset: 0x0007F1C9
		// (set) Token: 0x06002172 RID: 8562 RVA: 0x00080FD1 File Offset: 0x0007F1D1
		public Unit Height
		{
			get
			{
				return this.m_height;
			}
			set
			{
				this.m_height = value;
			}
		}

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x06002173 RID: 8563 RVA: 0x00080FDA File Offset: 0x0007F1DA
		protected ArrayList Cells
		{
			get
			{
				return this.m_cells;
			}
		}

		// Token: 0x04000EA8 RID: 3752
		private Unit m_height;

		// Token: 0x04000EA9 RID: 3753
		private ArrayList m_cells;

		// Token: 0x02000527 RID: 1319
		public enum RowType
		{
			// Token: 0x040012BC RID: 4796
			None,
			// Token: 0x040012BD RID: 4797
			Header,
			// Token: 0x040012BE RID: 4798
			GroupHeader,
			// Token: 0x040012BF RID: 4799
			Detail,
			// Token: 0x040012C0 RID: 4800
			GroupFooter,
			// Token: 0x040012C1 RID: 4801
			Footer
		}
	}
}
