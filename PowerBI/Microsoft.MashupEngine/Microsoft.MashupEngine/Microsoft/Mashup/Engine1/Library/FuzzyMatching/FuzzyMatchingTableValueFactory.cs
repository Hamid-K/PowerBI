using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Library.FuzzyMatching
{
	// Token: 0x02000B4D RID: 2893
	internal class FuzzyMatchingTableValueFactory
	{
		// Token: 0x06005028 RID: 20520 RVA: 0x0010C71C File Offset: 0x0010A91C
		public static TableValue FuzzyJoin(IEngineHost host, TableValue leftTable, Value leftKey, TableValue rightTable, Value rightKey, TableTypeAlgebra.JoinKind joinKind, FuzzyJoinAlgorithm fuzzyJoinAlgorithm, Value keyEqualityComparers, FuzzyJoinOptions fuzzyJoinOptions)
		{
			int[] columns = TableValue.GetColumns(leftTable.Columns, leftKey);
			int[] columns2 = TableValue.GetColumns(rightTable.Columns, rightKey);
			FunctionValue[] keyEqualityComparers2 = TableValue.GetKeyEqualityComparers(columns.Length, keyEqualityComparers);
			if (columns.Length != columns2.Length)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.RelationalAlgebra_JoinMustHaveSameColumnCountAndType, rightKey, null);
			}
			return FuzzyMatchingTableValueFactory.FuzzyJoin(host, leftTable, columns, rightTable, columns2, joinKind, fuzzyJoinAlgorithm, keyEqualityComparers2, fuzzyJoinOptions);
		}

		// Token: 0x06005029 RID: 20521 RVA: 0x0010C778 File Offset: 0x0010A978
		public static TableValue FuzzyJoin(IEngineHost host, TableValue leftTable, int[] leftKeyColumns, TableValue rightTable, int[] rightKeyColumns, TableTypeAlgebra.JoinKind joinKind, FuzzyJoinAlgorithm fuzzyJoinAlgorithm, FunctionValue[] keyEqualityComparers, FuzzyJoinOptions fuzzyJoinOptions)
		{
			Keys columns = leftTable.Columns;
			Keys columns2 = rightTable.Columns;
			IList<string> joinOverlap = TableValue.GetJoinOverlap(columns, leftKeyColumns, columns2, rightKeyColumns, joinKind);
			if (joinOverlap != null)
			{
				throw ValueException.NewExpressionError<Message1>(Strings.RelationalAlgebra_JoinMustNotHaveColumnOverlap(joinOverlap[0]), rightTable.Type, null);
			}
			Keys joinKeys = TableValue.GetJoinKeys(columns, columns2);
			JoinColumn[] joinColumns = TableValue.GetJoinColumns(joinKeys, columns, columns2);
			Query query = FuzzyJoinQuery.FuzzyJoin(host, RowCount.Infinite, leftTable.Query, leftKeyColumns, rightTable.Query, rightKeyColumns, joinKind, joinKeys, joinColumns, fuzzyJoinAlgorithm, fuzzyJoinOptions);
			IList<RelatedTable> list = RelatedTables.Join(leftTable.Columns, leftTable.RelatedTables, rightTable.Columns, rightTable.RelatedTables, joinColumns);
			ColumnIdentity[] array = ColumnIdentities.Join(leftTable.ColumnIdentities, rightTable.ColumnIdentities, joinColumns);
			IList<Relationship> list2 = Relationships.Join(leftTable.Columns, leftKeyColumns, leftTable.Relationships, rightTable.Columns, rightTable.ColumnIdentities, rightKeyColumns, rightTable.Relationships, joinColumns);
			return RelatedTablesTableValue.New(new QueryTableValue(query), list, array, list2);
		}

		// Token: 0x0600502A RID: 20522 RVA: 0x0010C864 File Offset: 0x0010AA64
		public static TableValue FuzzyNestedJoin(IEngineHost host, TableValue leftQuery, Value leftKey, TableValue rightTable, Value rightKey, Value joinKindValue, TextValue newColumn, Value keyEqualityComparers, FuzzyJoinOptions fuzzyJoinOptions)
		{
			TableTypeAlgebra.JoinKind joinKind = (joinKindValue.IsNull ? TableTypeAlgebra.JoinKind.LeftOuter : TableTypeAlgebra.GetJoinKind(joinKindValue));
			int[] columns = TableValue.GetColumns(leftQuery.Columns, leftKey);
			ListValue listValue = (rightKey.IsList ? rightKey.AsList : ListValue.New(new Value[] { rightKey }));
			if (!rightTable.IsFunction)
			{
				rightTable = rightTable.AsTable;
				TableValue.GetColumns(rightTable.AsTable.Columns, rightKey);
			}
			if (columns.Length != listValue.Count)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.RelationalAlgebra_JoinMustHaveSameColumnCountAndType, listValue, null);
			}
			KeysBuilder keysBuilder = default(KeysBuilder);
			for (int i = 0; i < listValue.Count; i++)
			{
				keysBuilder.Add(listValue[i].AsString);
			}
			KeysBuilder keysBuilder2 = default(KeysBuilder);
			keysBuilder2.Union(leftQuery.Columns);
			keysBuilder2.Add(newColumn.String);
			FunctionValue[] keyEqualityComparers2 = TableValue.GetKeyEqualityComparers(columns.Length, keyEqualityComparers);
			return FuzzyMatchingTableValueFactory.FuzzyNestedJoin(host, leftQuery, columns, rightTable, keysBuilder.ToKeys(), joinKind, newColumn.String, keysBuilder2.ToKeys(), keyEqualityComparers2, fuzzyJoinOptions);
		}

		// Token: 0x0600502B RID: 20523 RVA: 0x0010C974 File Offset: 0x0010AB74
		public static TableValue FuzzyNestedJoin(IEngineHost host, TableValue leftQuery, int[] leftKeyColumns, Value rightTable, Keys rightKey, TableTypeAlgebra.JoinKind joinKind, string newColumn, Keys joinKeys, FunctionValue[] keyEqualityComparers, FuzzyJoinOptions fuzzyJoinOptions)
		{
			return RelatedTablesTableValue.New(new QueryTableValue(FuzzyNestedJoinQuery.FuzzyNestedJoin(host, leftQuery.Query, leftKeyColumns, rightTable, rightKey, joinKind, newColumn, joinKeys, keyEqualityComparers, fuzzyJoinOptions)), RelatedTables.NestedJoin(leftQuery.RelatedTables, leftQuery.Columns.Length, rightTable), ColumnIdentities.AddColumns(leftQuery.ColumnIdentities, 1), Relationships.NestedJoin(leftQuery.Relationships, leftKeyColumns, rightTable, rightKey));
		}
	}
}
