using System;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x020017D4 RID: 6100
	internal abstract class FoldingQueryVisitor : QueryVisitor
	{
		// Token: 0x06009A34 RID: 39476 RVA: 0x001FEB68 File Offset: 0x001FCD68
		protected override Query NewAddColumns(ColumnsConstructor columnsConstructor, Query innerQuery)
		{
			return innerQuery.AddColumns(columnsConstructor);
		}

		// Token: 0x06009A35 RID: 39477 RVA: 0x001FEB71 File Offset: 0x001FCD71
		protected override Query NewSelectColumns(ColumnSelection columnSelection, Query innerQuery)
		{
			return innerQuery.SelectColumns(columnSelection);
		}

		// Token: 0x06009A36 RID: 39478 RVA: 0x001FEB7A File Offset: 0x001FCD7A
		protected override Query NewRenameReorderColumns(ColumnSelection columnSelection, Query innerQuery)
		{
			return innerQuery.RenameReorderColumns(columnSelection);
		}

		// Token: 0x06009A37 RID: 39479 RVA: 0x001FEB83 File Offset: 0x001FCD83
		protected override Query NewSkipTake(RowRange rowRange, bool floating, Query innerQuery)
		{
			if (floating)
			{
				return base.NewSkipTake(rowRange, floating, innerQuery);
			}
			return innerQuery.Skip(rowRange.SkipCount).Take(rowRange.TakeCount);
		}

		// Token: 0x06009A38 RID: 39480 RVA: 0x001FEBAB File Offset: 0x001FCDAB
		protected override Query NewSelectRows(FunctionValue condition, Query innerQuery)
		{
			return innerQuery.SelectRows(condition);
		}

		// Token: 0x06009A39 RID: 39481 RVA: 0x001FEBB4 File Offset: 0x001FCDB4
		protected override Query NewSort(TableSortOrder sortOrder, RowCount takeCount, Query innerQuery)
		{
			return innerQuery.Sort(sortOrder).Take(takeCount);
		}

		// Token: 0x06009A3A RID: 39482 RVA: 0x001FEBC3 File Offset: 0x001FCDC3
		protected override Query NewDistinct(TableDistinct distinctCriteria, bool floating, Query innerQuery)
		{
			if (floating)
			{
				return base.NewDistinct(distinctCriteria, floating, innerQuery);
			}
			return innerQuery.Distinct(distinctCriteria);
		}

		// Token: 0x06009A3B RID: 39483 RVA: 0x001FEBD9 File Offset: 0x001FCDD9
		protected override Query NewCombine(Query[] queries, TypeValue[] columnTypes, TableSortOrder sortOrder, int disjointColumn, RowCount takeCount)
		{
			return Query.Combine(queries, columnTypes, sortOrder, disjointColumn, takeCount);
		}

		// Token: 0x06009A3C RID: 39484 RVA: 0x001FEBE7 File Offset: 0x001FCDE7
		protected override Query NewGroup(Grouping grouping, bool floating, Query innerQuery)
		{
			if (floating)
			{
				return base.NewGroup(grouping, floating, innerQuery);
			}
			return innerQuery.Group(grouping);
		}

		// Token: 0x06009A3D RID: 39485 RVA: 0x001FEC00 File Offset: 0x001FCE00
		protected override Query NewJoin(RowCount take, Query leftQuery, int[] leftKeyColumns, Query rightQuery, int[] rightKeyColumns, TableTypeAlgebra.JoinKind joinKind, Keys joinKeys, JoinColumn[] joinColumns, JoinAlgorithm joinAlgorithm, FunctionValue[] keyEqualityComparers)
		{
			return Query.Join(take, leftQuery, leftKeyColumns, rightQuery, rightKeyColumns, joinKind, joinKeys, joinColumns, joinAlgorithm, keyEqualityComparers);
		}

		// Token: 0x06009A3E RID: 39486 RVA: 0x001FEC23 File Offset: 0x001FCE23
		protected override Query NewNestedJoin(Query leftQuery, int[] leftKeyColumns, Value rightTable, Keys rightKey, TableTypeAlgebra.JoinKind joinKind, string newColumnName, Keys joinKeys, FunctionValue[] keyEqualityComparers, TypeValue columnType)
		{
			return leftQuery.NestedJoin(leftKeyColumns, rightTable, rightKey, joinKind, newColumnName, joinKeys, keyEqualityComparers);
		}

		// Token: 0x06009A3F RID: 39487 RVA: 0x001FEC37 File Offset: 0x001FCE37
		protected override Query NewExpandListColumn(int columnIndex, bool singleOrDefault, TypeValue columnType, Query innerQuery)
		{
			return innerQuery.ExpandListColumn(columnIndex, singleOrDefault);
		}

		// Token: 0x06009A40 RID: 39488 RVA: 0x001FEC42 File Offset: 0x001FCE42
		protected override Query NewExpandRecordColumn(int columnToExpand, Keys fieldsToProject, Keys newColumns, TypeValue[] columnTypes, Query innerQuery)
		{
			return innerQuery.ExpandRecordColumn(columnToExpand, fieldsToProject, newColumns);
		}

		// Token: 0x06009A41 RID: 39489 RVA: 0x001FEC4E File Offset: 0x001FCE4E
		protected override Query NewPivot(Query innerQuery, TableTypeValue inputTableType, TableTypeValue outputTableType, string[] pivotValues, string attributeColumn, string valueColumn, FunctionValue aggregateFunction)
		{
			return innerQuery.Pivot(inputTableType, outputTableType, pivotValues, attributeColumn, valueColumn, aggregateFunction);
		}

		// Token: 0x06009A42 RID: 39490 RVA: 0x001FEC60 File Offset: 0x001FCE60
		protected override Query NewUnpivot(Query innerQuery, TableTypeValue inputTableType, TableTypeValue outputTableType, string[] toPivot, string attributeColumn, string valueColumn)
		{
			return innerQuery.Unpivot(inputTableType, outputTableType, toPivot, attributeColumn, valueColumn);
		}
	}
}
