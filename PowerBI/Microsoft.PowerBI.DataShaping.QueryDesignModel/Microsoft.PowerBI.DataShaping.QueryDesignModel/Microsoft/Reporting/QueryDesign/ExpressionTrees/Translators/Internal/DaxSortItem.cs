using System;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal
{
	// Token: 0x02000144 RID: 324
	internal struct DaxSortItem
	{
		// Token: 0x06001194 RID: 4500 RVA: 0x00030F77 File Offset: 0x0002F177
		internal DaxSortItem(DaxColumnRef column, SortDirection direction)
		{
			this._column = column;
			this._direction = direction;
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06001195 RID: 4501 RVA: 0x00030F87 File Offset: 0x0002F187
		internal DaxColumnRef Column
		{
			get
			{
				return this._column;
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06001196 RID: 4502 RVA: 0x00030F8F File Offset: 0x0002F18F
		internal SortDirection Direction
		{
			get
			{
				return this._direction;
			}
		}

		// Token: 0x04000ADC RID: 2780
		private readonly DaxColumnRef _column;

		// Token: 0x04000ADD RID: 2781
		private readonly SortDirection _direction;
	}
}
