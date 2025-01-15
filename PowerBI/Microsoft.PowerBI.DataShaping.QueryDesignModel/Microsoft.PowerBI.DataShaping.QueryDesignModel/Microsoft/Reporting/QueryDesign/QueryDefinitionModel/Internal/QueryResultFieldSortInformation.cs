using System;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x02000116 RID: 278
	internal sealed class QueryResultFieldSortInformation
	{
		// Token: 0x06001000 RID: 4096 RVA: 0x0002C2BA File Offset: 0x0002A4BA
		internal QueryResultFieldSortInformation(int index, SortDirection direction)
		{
			this._index = index;
			this._direction = direction;
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06001001 RID: 4097 RVA: 0x0002C2D0 File Offset: 0x0002A4D0
		internal int Index
		{
			get
			{
				return this._index;
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06001002 RID: 4098 RVA: 0x0002C2D8 File Offset: 0x0002A4D8
		internal SortDirection Direction
		{
			get
			{
				return this._direction;
			}
		}

		// Token: 0x04000A54 RID: 2644
		private readonly int _index;

		// Token: 0x04000A55 RID: 2645
		private readonly SortDirection _direction;
	}
}
