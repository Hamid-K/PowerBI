using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes
{
	// Token: 0x02000142 RID: 322
	internal static class ConceptualResultTypeBuilder
	{
		// Token: 0x06000838 RID: 2104 RVA: 0x00011108 File Offset: 0x0000F308
		internal static ConceptualTypeColumn Column(this ConceptualResultType type, string name, string edmName = null)
		{
			ConceptualPrimitiveResultType conceptualPrimitiveResultType = type as ConceptualPrimitiveResultType;
			if (conceptualPrimitiveResultType == null)
			{
				throw new ArgumentOutOfRangeException("The parameter provided should be of type ConceptualPrimitiveResultType");
			}
			return new ConceptualTypeColumn(conceptualPrimitiveResultType, name, edmName);
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x00011132 File Offset: 0x0000F332
		internal static ConceptualRowType Row(this IReadOnlyList<ConceptualTypeColumn> columns)
		{
			return new ConceptualRowType(columns);
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x0001113A File Offset: 0x0000F33A
		internal static ConceptualRowType Row(this ConceptualTypeColumn column)
		{
			return new ConceptualRowType(new ConceptualTypeColumn[] { column });
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x0001114B File Offset: 0x0000F34B
		internal static ConceptualRowType Extend(this ConceptualRowType row, IReadOnlyList<ConceptualTypeColumn> additionalColumns, ColumnMergingBehavior mergingBehavior)
		{
			if (additionalColumns.Count == 0)
			{
				return row;
			}
			List<ConceptualTypeColumn> list = new List<ConceptualTypeColumn>(row.Columns.Count + additionalColumns.Count);
			list.AddRange(row.Columns);
			ConceptualResultTypeBuilder.AddColumns(list, additionalColumns, mergingBehavior);
			return new ConceptualRowType(list);
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x00011187 File Offset: 0x0000F387
		internal static ConceptualRowType Merge(this ConceptualRowType left, ConceptualRowType right, ColumnMergingBehavior mergingBehavior)
		{
			return left.Extend(right.Columns, mergingBehavior);
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x00011198 File Offset: 0x0000F398
		internal static ConceptualRowType MergeRows(this ConceptualResultType left, ConceptualResultType right, ColumnMergingBehavior mergingBehavior)
		{
			ConceptualRowType conceptualRowType = ConceptualResultTypeBuilder.AssertRowType(left);
			ConceptualRowType conceptualRowType2 = ConceptualResultTypeBuilder.AssertRowType(right);
			return conceptualRowType.Merge(conceptualRowType2, mergingBehavior);
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x000111BC File Offset: 0x0000F3BC
		internal static ConceptualRowType MergeRows(this IEnumerable<ConceptualResultType> conceptualTypes, ColumnMergingBehavior mergingBehavior)
		{
			List<ConceptualTypeColumn> list = new List<ConceptualTypeColumn>();
			int num = 0;
			foreach (ConceptualResultType conceptualResultType in conceptualTypes)
			{
				ConceptualRowType conceptualRowType = ConceptualResultTypeBuilder.AssertRowType(conceptualResultType);
				if (num == 0)
				{
					list.AddRange(conceptualRowType.Columns);
				}
				else
				{
					ConceptualResultTypeBuilder.AddColumns(list, conceptualRowType.Columns, mergingBehavior);
				}
				num++;
			}
			return list.Row();
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x00011234 File Offset: 0x0000F434
		internal static ConceptualRowType MergeRows(this ConceptualResultType left, IEnumerable<ConceptualResultType> conceptualTypes, ColumnMergingBehavior mergingBehavior)
		{
			ConceptualRowType conceptualRowType = ConceptualResultTypeBuilder.AssertRowType(left);
			List<ConceptualTypeColumn> list = new List<ConceptualTypeColumn>();
			list.AddRange(conceptualRowType.Columns);
			foreach (ConceptualResultType conceptualResultType in conceptualTypes)
			{
				ConceptualRowType conceptualRowType2 = ConceptualResultTypeBuilder.AssertRowType(conceptualResultType);
				ConceptualResultTypeBuilder.AddColumns(list, conceptualRowType2.Columns, mergingBehavior);
			}
			return list.Row();
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x000112A8 File Offset: 0x0000F4A8
		internal static ConceptualTableType Table(this IReadOnlyList<ConceptualTypeColumn> columns)
		{
			return columns.Row().Table();
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x000112B5 File Offset: 0x0000F4B5
		internal static ConceptualTableType Table(this ConceptualResultType input)
		{
			return new ConceptualTableType(ConceptualResultTypeBuilder.AssertRowType(input));
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x000112C4 File Offset: 0x0000F4C4
		private static void AddColumns(List<ConceptualTypeColumn> newColumns, IReadOnlyList<ConceptualTypeColumn> columnsToAdd, ColumnMergingBehavior mergingBehavior)
		{
			using (IEnumerator<ConceptualTypeColumn> enumerator = columnsToAdd.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ConceptualTypeColumn column = enumerator.Current;
					ConceptualTypeColumn conceptualTypeColumn = newColumns.Find((ConceptualTypeColumn c) => object.Equals(c.Name, column.Name));
					if (mergingBehavior != ColumnMergingBehavior.Disallow)
					{
						if (mergingBehavior != ColumnMergingBehavior.Merge)
						{
							throw new InvalidOperationException("Unexpected enum value " + mergingBehavior.ToString());
						}
						if (conceptualTypeColumn != null)
						{
							if (conceptualTypeColumn.PrimitiveType != column.PrimitiveType)
							{
								throw new ArgumentException(StringUtil.FormatInvariant("Duplicate columns with different data types found: {0}.", column.Name));
							}
						}
						else
						{
							newColumns.Add(column);
						}
					}
					else
					{
						newColumns.Add(column);
					}
				}
			}
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x0001139C File Offset: 0x0000F59C
		private static ConceptualRowType AssertRowType(ConceptualResultType item)
		{
			ConceptualRowType conceptualRowType = item as ConceptualRowType;
			if (conceptualRowType == null)
			{
				throw new ArgumentOutOfRangeException("The parameter provided should be of type ConceptualRowType");
			}
			return conceptualRowType;
		}
	}
}
