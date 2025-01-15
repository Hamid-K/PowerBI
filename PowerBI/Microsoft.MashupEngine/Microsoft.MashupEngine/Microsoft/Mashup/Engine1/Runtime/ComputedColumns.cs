using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Language.Query;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001636 RID: 5686
	public static class ComputedColumns
	{
		// Token: 0x06008F3E RID: 36670 RVA: 0x001DD4E0 File Offset: 0x001DB6E0
		public static IList<ComputedColumn> SelectColumns(IList<ComputedColumn> computedColumns, RecordTypeValue rowType, ColumnSelection columnSelection)
		{
			if (computedColumns.Count == 0)
			{
				return ComputedColumns.None;
			}
			ColumnSelection.SelectMap selectMap = columnSelection.CreateSelectMap(rowType.FieldKeys);
			Func<int, bool> func = (int column) => selectMap.MapColumn(column) != -1;
			Func<int, int> func2 = (int column) => selectMap.MapColumn(column);
			return ComputedColumns.MapComputedColumns(computedColumns, rowType, func, func2, columnSelection.Keys);
		}

		// Token: 0x06008F3F RID: 36671 RVA: 0x001DD53C File Offset: 0x001DB73C
		public static IList<ComputedColumn> Join(IList<ComputedColumn> leftComputedColumns, IList<ComputedColumn> rightComputedColumns, RecordTypeValue leftRowType, RecordTypeValue rightRowType, Keys joinKeys, JoinColumn[] joinColumns)
		{
			if (leftComputedColumns.Count == 0 && rightComputedColumns.Count == 0)
			{
				return ComputedColumns.None;
			}
			if (rightComputedColumns.Count == 0)
			{
				return leftComputedColumns;
			}
			List<ComputedColumn> list = new List<ComputedColumn>();
			list.AddRange(leftComputedColumns);
			JoinQuery.JoinMap joinMap = new JoinQuery.JoinMap(leftRowType.FieldKeys, rightRowType.FieldKeys, joinColumns);
			Func<int, bool> func = (int column) => joinMap.MapRightColumn(column) != -1;
			Func<int, int> func2 = (int column) => joinMap.MapRightColumn(column);
			list.AddRange(ComputedColumns.MapComputedColumns(rightComputedColumns, rightRowType, func, func2, joinKeys));
			if (list.Count <= 0)
			{
				return ComputedColumns.None;
			}
			return list.ToArray();
		}

		// Token: 0x06008F40 RID: 36672 RVA: 0x001DD5DC File Offset: 0x001DB7DC
		public static IList<ComputedColumn> Group(IList<ComputedColumn> computedColumns, RecordTypeValue rowType, Grouping grouping)
		{
			if (computedColumns.Count == 0)
			{
				return ComputedColumns.None;
			}
			Grouping.GroupMap groupMap = grouping.CreateGroupMap(rowType);
			Func<int, bool> func = (int column) => groupMap.MapColumn(column) != -1;
			Func<int, int> func2 = (int column) => groupMap.MapColumn(column);
			return ComputedColumns.MapComputedColumns(computedColumns, rowType, func, func2, grouping.ResultKeys);
		}

		// Token: 0x06008F41 RID: 36673 RVA: 0x001DD634 File Offset: 0x001DB834
		public static IList<ComputedColumn> ExpandListColumn(IList<ComputedColumn> computedColumns, RecordTypeValue rowType, int columnIndex)
		{
			if (computedColumns.Count == 0)
			{
				return ComputedColumns.None;
			}
			Func<int, bool> func = (int column) => column != columnIndex;
			Func<int, int> func2 = (int column) => column;
			return ComputedColumns.MapComputedColumns(computedColumns, rowType, func, func2, rowType.FieldKeys);
		}

		// Token: 0x06008F42 RID: 36674 RVA: 0x001DD698 File Offset: 0x001DB898
		public static IList<ComputedColumn> ExpandRecordColumn(IList<ComputedColumn> computedColumns, RecordTypeValue rowType, int columnToExpand, int fieldsToProjectCount, Keys newColumns)
		{
			if (computedColumns.Count == 0)
			{
				return ComputedColumns.None;
			}
			Func<int, bool> func = (int column) => column < columnToExpand || column >= columnToExpand + fieldsToProjectCount;
			Func<int, int> func2 = delegate(int column)
			{
				if (column < columnToExpand)
				{
					return column;
				}
				if (column >= columnToExpand + fieldsToProjectCount)
				{
					return column + fieldsToProjectCount;
				}
				return -1;
			};
			return ComputedColumns.MapComputedColumns(computedColumns, rowType, func, func2, newColumns);
		}

		// Token: 0x06008F43 RID: 36675 RVA: 0x001DD6EC File Offset: 0x001DB8EC
		private static IList<ComputedColumn> MapComputedColumns(IList<ComputedColumn> computedColumns, RecordTypeValue rowType, Func<int, bool> access, Func<int, int> adjust, Keys newColumns)
		{
			List<ComputedColumn> list = new List<ComputedColumn>();
			foreach (ComputedColumn computedColumn in computedColumns)
			{
				int num = adjust(computedColumn.Column);
				if (num != -1)
				{
					QueryExpression queryExpression = QueryExpressionBuilder.ToQueryExpression(rowType, computedColumn.Function);
					if (queryExpression.AllAccess(ArgumentAccess.Safe, access))
					{
						QueryExpression queryExpression2 = queryExpression.AdjustColumnAccess(adjust);
						FunctionValue functionValue = QueryExpressionAssembler.Assemble(newColumns, queryExpression2);
						list.Add(computedColumn.SelectColumns(num, functionValue));
					}
				}
			}
			if (list.Count <= 0)
			{
				return ComputedColumns.None;
			}
			return list.ToArray();
		}

		// Token: 0x04004D7F RID: 19839
		public static readonly IList<ComputedColumn> None = new ComputedColumn[0];
	}
}
