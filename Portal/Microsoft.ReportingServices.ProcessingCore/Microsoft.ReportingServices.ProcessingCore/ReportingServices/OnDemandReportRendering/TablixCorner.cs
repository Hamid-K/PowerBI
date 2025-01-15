using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200035F RID: 863
	public sealed class TablixCorner
	{
		// Token: 0x060020E6 RID: 8422 RVA: 0x0007F98E File Offset: 0x0007DB8E
		internal TablixCorner(Tablix owner)
		{
			this.m_owner = owner;
		}

		// Token: 0x17001292 RID: 4754
		// (get) Token: 0x060020E7 RID: 8423 RVA: 0x0007F99D File Offset: 0x0007DB9D
		public TablixCornerRowCollection RowCollection
		{
			get
			{
				if (this.m_rowCollection == null)
				{
					this.m_rowCollection = new TablixCornerRowCollection(this.m_owner);
				}
				return this.m_rowCollection;
			}
		}

		// Token: 0x060020E8 RID: 8424 RVA: 0x0007F9BE File Offset: 0x0007DBBE
		internal void ResetContext()
		{
			if (this.m_rowCollection != null)
			{
				this.m_rowCollection.ResetContext();
			}
		}

		// Token: 0x060020E9 RID: 8425 RVA: 0x0007F9D3 File Offset: 0x0007DBD3
		internal void SetNewContext()
		{
			if (this.m_rowCollection != null)
			{
				this.m_rowCollection.SetNewContext();
			}
		}

		// Token: 0x04001088 RID: 4232
		private Tablix m_owner;

		// Token: 0x04001089 RID: 4233
		private TablixCornerRowCollection m_rowCollection;
	}
}
