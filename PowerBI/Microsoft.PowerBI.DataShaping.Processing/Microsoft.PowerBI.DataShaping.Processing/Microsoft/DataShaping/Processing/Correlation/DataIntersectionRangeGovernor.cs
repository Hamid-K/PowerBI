using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.DataShaping.Processing.Correlation
{
	// Token: 0x020000A7 RID: 167
	internal sealed class DataIntersectionRangeGovernor
	{
		// Token: 0x06000450 RID: 1104 RVA: 0x0000D7BE File Offset: 0x0000B9BE
		internal DataIntersectionRangeGovernor(CellScopeToIntersectionRangeMapping cellScopeToIntersectionRangeMapping)
		{
			this._cellScopeToIntersectionRangeMapping = cellScopeToIntersectionRangeMapping;
			this._handledScopes = new Stack<bool>();
			this._handledScopes.Push(false);
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0000D7E4 File Offset: 0x0000B9E4
		internal void EnterCellScope()
		{
			this._handledScopes.Push(false);
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0000D7F2 File Offset: 0x0000B9F2
		internal void ExitCellScope()
		{
			this._handledScopes.Pop();
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0000D800 File Offset: 0x0000BA00
		internal bool IsCellScopeHandled()
		{
			return this._handledScopes.Peek();
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0000D810 File Offset: 0x0000BA10
		internal void AddColumnIndexMapping(int cellScopeIndex, long rowIndex)
		{
			IndexRange intersectionRange = this._cellScopeToIntersectionRangeMapping.GetIntersectionRange(cellScopeIndex);
			Util.AddToLazyList<IndexRange>(ref this._columnIndexToIntersectionRangeMapping, intersectionRange);
			Util.AddToLazyList<long>(ref this._columnIndexToRowIndexMapping, rowIndex);
			this._handledScopes.Pop();
			this._handledScopes.Push(true);
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0000D85A File Offset: 0x0000BA5A
		internal void SkipColumnIndex()
		{
			Util.AddToLazyList<IndexRange>(ref this._columnIndexToIntersectionRangeMapping, null);
			Util.AddToLazyList<long>(ref this._columnIndexToRowIndexMapping, -1L);
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0000D875 File Offset: 0x0000BA75
		internal bool IsValidColumnIndex(int columnIndex)
		{
			return columnIndex < this.ColumnCount && this._columnIndexToIntersectionRangeMapping[columnIndex] != null;
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0000D891 File Offset: 0x0000BA91
		internal IndexRange GetIntersectionRange(int columnIndex)
		{
			return this._columnIndexToIntersectionRangeMapping[columnIndex];
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0000D89F File Offset: 0x0000BA9F
		internal long GetRowIndex(int columnIndex)
		{
			return this._columnIndexToRowIndexMapping[columnIndex];
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x0000D8AD File Offset: 0x0000BAAD
		internal int ColumnCount
		{
			get
			{
				if (this._columnIndexToIntersectionRangeMapping == null)
				{
					return 0;
				}
				return this._columnIndexToIntersectionRangeMapping.Count;
			}
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0000D8C4 File Offset: 0x0000BAC4
		internal void InitializeForTest(int startIndex, int endIndex, params int[] missingLeafIndices)
		{
			this.EnterCellScope();
			IndexRange indexRange = new IndexRange();
			bool flag = missingLeafIndices.Length != 0;
			int num = 0;
			for (int i = startIndex; i <= endIndex; i++)
			{
				indexRange.ExtendRange(i);
				Util.AddToLazyList<IndexRange>(ref this._columnIndexToIntersectionRangeMapping, indexRange);
				if (!flag)
				{
					Util.AddToLazyList<long>(ref this._columnIndexToRowIndexMapping, (long)i);
				}
				else if (missingLeafIndices.Contains(i))
				{
					Util.AddToLazyList<long>(ref this._columnIndexToRowIndexMapping, -1L);
				}
				else
				{
					Util.AddToLazyList<long>(ref this._columnIndexToRowIndexMapping, (long)num++);
				}
			}
			this.ExitCellScope();
		}

		// Token: 0x0400023D RID: 573
		private readonly Stack<bool> _handledScopes;

		// Token: 0x0400023E RID: 574
		private readonly CellScopeToIntersectionRangeMapping _cellScopeToIntersectionRangeMapping;

		// Token: 0x0400023F RID: 575
		private List<IndexRange> _columnIndexToIntersectionRangeMapping;

		// Token: 0x04000240 RID: 576
		private List<long> _columnIndexToRowIndexMapping;
	}
}
