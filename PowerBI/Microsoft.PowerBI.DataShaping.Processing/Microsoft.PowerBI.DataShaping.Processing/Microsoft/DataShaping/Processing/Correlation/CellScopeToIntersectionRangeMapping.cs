using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.Processing.Correlation
{
	// Token: 0x020000A2 RID: 162
	internal sealed class CellScopeToIntersectionRangeMapping
	{
		// Token: 0x06000438 RID: 1080 RVA: 0x0000D584 File Offset: 0x0000B784
		internal CellScopeToIntersectionRangeMapping()
		{
			this._mapping = new List<IndexRange>();
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0000D597 File Offset: 0x0000B797
		internal IndexRange GetIntersectionRange(int scopeIndex)
		{
			Contract.RetailAssert(scopeIndex < this._mapping.Count, "Invalid scope index");
			return this._mapping[scopeIndex];
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0000D5C0 File Offset: 0x0000B7C0
		internal IndexRange AddCellScope()
		{
			IndexRange indexRange = new IndexRange();
			this._mapping.Add(indexRange);
			return indexRange;
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0000D5E0 File Offset: 0x0000B7E0
		internal int GetActiveCellScopeIndex()
		{
			Contract.RetailAssert(!this._mapping.IsNullOrEmpty<IndexRange>(), "No active cell scope index");
			return this._mapping.Count - 1;
		}

		// Token: 0x04000230 RID: 560
		private readonly List<IndexRange> _mapping;
	}
}
