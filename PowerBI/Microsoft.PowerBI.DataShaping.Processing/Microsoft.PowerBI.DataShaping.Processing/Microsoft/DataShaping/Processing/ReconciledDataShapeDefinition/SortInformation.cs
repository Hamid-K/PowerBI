using System;

namespace Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition
{
	// Token: 0x02000050 RID: 80
	internal sealed class SortInformation
	{
		// Token: 0x06000212 RID: 530 RVA: 0x0000634F File Offset: 0x0000454F
		internal SortInformation(int sortIndex, SortDirection sortDirection)
		{
			this._sortIndex = sortIndex;
			this._sortDirection = sortDirection;
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000213 RID: 531 RVA: 0x00006365 File Offset: 0x00004565
		internal int SortIndex
		{
			get
			{
				return this._sortIndex;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000214 RID: 532 RVA: 0x0000636D File Offset: 0x0000456D
		internal SortDirection SortDirection
		{
			get
			{
				return this._sortDirection;
			}
		}

		// Token: 0x04000141 RID: 321
		private readonly int _sortIndex;

		// Token: 0x04000142 RID: 322
		private readonly SortDirection _sortDirection;
	}
}
