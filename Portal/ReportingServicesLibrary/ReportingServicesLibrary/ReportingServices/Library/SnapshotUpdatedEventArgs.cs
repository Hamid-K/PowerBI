using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000288 RID: 648
	internal sealed class SnapshotUpdatedEventArgs : EventArgs
	{
		// Token: 0x060017A6 RID: 6054 RVA: 0x0005FAF0 File Offset: 0x0005DCF0
		public SnapshotUpdatedEventArgs(ReportSnapshot oldSnapshot, ReportSnapshot newSnapshot)
		{
			RSTrace.CatalogTrace.Assert(oldSnapshot != null, "oldSnapshot");
			RSTrace.CatalogTrace.Assert(newSnapshot != null, "newSnapshot");
			this.m_oldSnapshot = oldSnapshot;
			this.m_newSnapshot = newSnapshot;
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x060017A7 RID: 6055 RVA: 0x0005FB2C File Offset: 0x0005DD2C
		public ReportSnapshot NewSnapshot
		{
			get
			{
				return this.m_newSnapshot;
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x060017A8 RID: 6056 RVA: 0x0005FB34 File Offset: 0x0005DD34
		public ReportSnapshot OldSnapshot
		{
			get
			{
				return this.m_oldSnapshot;
			}
		}

		// Token: 0x0400088C RID: 2188
		private readonly ReportSnapshot m_newSnapshot;

		// Token: 0x0400088D RID: 2189
		private readonly ReportSnapshot m_oldSnapshot;
	}
}
