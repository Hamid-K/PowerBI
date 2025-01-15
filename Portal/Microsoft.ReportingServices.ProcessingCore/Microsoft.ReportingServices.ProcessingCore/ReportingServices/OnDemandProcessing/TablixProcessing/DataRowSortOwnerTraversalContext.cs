using System;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008D4 RID: 2260
	internal class DataRowSortOwnerTraversalContext : ITraversalContext
	{
		// Token: 0x06007BAA RID: 31658 RVA: 0x001FC65A File Offset: 0x001FA85A
		internal DataRowSortOwnerTraversalContext(IDataRowSortOwner sortOwner)
		{
			this.m_sortOwner = sortOwner;
		}

		// Token: 0x1700288A RID: 10378
		// (get) Token: 0x06007BAB RID: 31659 RVA: 0x001FC669 File Offset: 0x001FA869
		internal IDataRowSortOwner SortOwner
		{
			get
			{
				return this.m_sortOwner;
			}
		}

		// Token: 0x04003D7F RID: 15743
		private IDataRowSortOwner m_sortOwner;
	}
}
