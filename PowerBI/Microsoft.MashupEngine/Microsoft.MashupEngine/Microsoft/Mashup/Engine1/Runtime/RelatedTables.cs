using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Language.Query;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001629 RID: 5673
	public static class RelatedTables
	{
		// Token: 0x06008F00 RID: 36608 RVA: 0x001DC7EC File Offset: 0x001DA9EC
		public static IList<RelatedTable> SelectColumns(IList<RelatedTable> relatedTables, Keys columns, ColumnSelection columnSelection)
		{
			ColumnSelection.SelectMap selectMap = columnSelection.CreateSelectMap(columns);
			return RelatedTables.Transform(relatedTables, (RelatedTable relatedTable) => relatedTable.SelectColumns(selectMap));
		}

		// Token: 0x06008F01 RID: 36609 RVA: 0x001DC820 File Offset: 0x001DAA20
		public static IList<RelatedTable> TransformColumns(IList<RelatedTable> relatedTables, ColumnTransforms columnTransforms)
		{
			return RelatedTables.Transform(relatedTables, (RelatedTable relatedTable) => relatedTable.TransformColumns(columnTransforms));
		}

		// Token: 0x06008F02 RID: 36610 RVA: 0x001DC84C File Offset: 0x001DAA4C
		public static IList<RelatedTable> Join(Keys leftColumns, IList<RelatedTable> leftRelatedTables, Keys rightColumns, IList<RelatedTable> rightRelatedTables, JoinColumn[] joinColumns)
		{
			List<RelatedTable> list = new List<RelatedTable>();
			JoinQuery.JoinMap joinMap = new JoinQuery.JoinMap(leftColumns, rightColumns, joinColumns);
			RelatedTables.MapRelatedTables(list, joinMap, true, leftRelatedTables);
			RelatedTables.MapRelatedTables(list, joinMap, false, rightRelatedTables);
			return list;
		}

		// Token: 0x06008F03 RID: 36611 RVA: 0x001DC87C File Offset: 0x001DAA7C
		public static IList<RelatedTable> NestedJoin(IList<RelatedTable> relatedTables, int newColumn, Value rightTable)
		{
			RelatedTable[] array = new RelatedTable[relatedTables.Count + 1];
			for (int i = 0; i < relatedTables.Count; i++)
			{
				array[i] = relatedTables[i];
			}
			array[array.Length - 1] = new RelatedTable(newColumn, false, rightTable);
			return array;
		}

		// Token: 0x06008F04 RID: 36612 RVA: 0x001DC8C4 File Offset: 0x001DAAC4
		public static IList<RelatedTable> ExpandListColumn(IList<RelatedTable> relatedTables, int columnIndex)
		{
			return RelatedTables.Transform(relatedTables, (RelatedTable relatedTable) => relatedTable.ExpandListColumn(columnIndex));
		}

		// Token: 0x06008F05 RID: 36613 RVA: 0x001DC8F0 File Offset: 0x001DAAF0
		public static IList<RelatedTable> ExpandRecordColumn(IList<RelatedTable> relatedTables, int columnToExpand, Keys fieldsToProject)
		{
			return RelatedTables.Transform(relatedTables, (RelatedTable relatedTable) => relatedTable.ExpandRecordColumn(columnToExpand, fieldsToProject));
		}

		// Token: 0x06008F06 RID: 36614 RVA: 0x001DC924 File Offset: 0x001DAB24
		private static IList<RelatedTable> Transform(IList<RelatedTable> relatedTables, Func<RelatedTable, RelatedTable> func)
		{
			return RelatedTables.Transform(relatedTables, delegate(RelatedTable relatedTable)
			{
				RelatedTable relatedTable2 = func(relatedTable);
				if (relatedTable2 != null)
				{
					return new RelatedTable[] { relatedTable2 };
				}
				return RelatedTables.None;
			});
		}

		// Token: 0x06008F07 RID: 36615 RVA: 0x001DC950 File Offset: 0x001DAB50
		private static IList<RelatedTable> Transform(IList<RelatedTable> relatedTables, Func<RelatedTable, IList<RelatedTable>> func)
		{
			if (relatedTables.Count == 0)
			{
				return RelatedTables.None;
			}
			List<RelatedTable> list = new List<RelatedTable>();
			foreach (RelatedTable relatedTable in relatedTables)
			{
				list.AddRange(func(relatedTable));
			}
			if (list.Count == 0)
			{
				return RelatedTables.None;
			}
			return list;
		}

		// Token: 0x06008F08 RID: 36616 RVA: 0x001DC9C4 File Offset: 0x001DABC4
		private static void MapRelatedTables(List<RelatedTable> newRelatedTables, JoinQuery.JoinMap joinMap, bool left, IList<RelatedTable> relatedTables)
		{
			for (int i = 0; i < relatedTables.Count; i++)
			{
				RelatedTable relatedTable = relatedTables[i];
				int num = joinMap.MapColumn(left, relatedTable.Column);
				if (num != -1)
				{
					newRelatedTables.Add(relatedTable.SelectColumns(num));
				}
			}
		}

		// Token: 0x04004D6E RID: 19822
		public static readonly IList<RelatedTable> None = new RelatedTable[0];
	}
}
