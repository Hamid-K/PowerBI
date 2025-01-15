using System;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.DataShaping.Processing.Analytics
{
	// Token: 0x020000BB RID: 187
	internal sealed class SortInformation : ISortInformation
	{
		// Token: 0x060004C0 RID: 1216 RVA: 0x0000E2DA File Offset: 0x0000C4DA
		internal SortInformation(int sortIndex, SortDirection sortDirection)
		{
			this._sortIndex = sortIndex;
			this._sortDirection = sortDirection;
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x0000E2F0 File Offset: 0x0000C4F0
		public int SortIndex
		{
			get
			{
				return this._sortIndex;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x0000E2F8 File Offset: 0x0000C4F8
		public SortDirection SortDirection
		{
			get
			{
				return this._sortDirection;
			}
		}

		// Token: 0x04000267 RID: 615
		private readonly int _sortIndex;

		// Token: 0x04000268 RID: 616
		private readonly SortDirection _sortDirection;
	}
}
