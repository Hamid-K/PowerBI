using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine1.Language.Query;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001633 RID: 5683
	public static class Relationships
	{
		// Token: 0x06008F2A RID: 36650 RVA: 0x001DCF80 File Offset: 0x001DB180
		public static IList<Relationship> SelectColumns(IList<Relationship> relationships, Keys columns, ColumnSelection columnSelection)
		{
			if (relationships.Count == 0)
			{
				return Relationships.None;
			}
			ColumnSelection.SelectMap selectMap = columnSelection.CreateSelectMap(columns);
			List<Relationship> list = new List<Relationship>(relationships.Count);
			foreach (Relationship relationship in relationships)
			{
				int[] array = selectMap.MapColumns(relationship.LeftKeyColumns);
				if (array != null)
				{
					list.Add(relationship.SelectColumns(array));
				}
			}
			return list.ToArray();
		}

		// Token: 0x06008F2B RID: 36651 RVA: 0x001DD00C File Offset: 0x001DB20C
		public static IList<Relationship> TransformColumns(IList<Relationship> relationships, ColumnTransforms columnTransforms)
		{
			if (relationships.Count == 0)
			{
				return Relationships.None;
			}
			List<Relationship> list = new List<Relationship>(relationships.Count);
			foreach (Relationship relationship in relationships)
			{
				bool flag = false;
				int num = 0;
				while (!flag && num < relationship.LeftKeyColumns.Length)
				{
					flag = columnTransforms.Dictionary.ContainsKey(relationship.LeftKeyColumns[num]);
					num++;
				}
				if (!flag)
				{
					list.Add(relationship);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06008F2C RID: 36652 RVA: 0x001DD0A8 File Offset: 0x001DB2A8
		public static IList<Relationship> Join(Keys leftColumns, int[] leftKeyColumns, IList<Relationship> leftRelationships, Keys rightColumns, ColumnIdentity[] rightColumnIdentities, int[] rightKeyColumns, IList<Relationship> rightRelationships, JoinColumn[] joinColumns)
		{
			List<Relationship> list = new List<Relationship>();
			JoinQuery.JoinMap joinMap = new JoinQuery.JoinMap(leftColumns, rightColumns, joinColumns);
			Relationships.MapRelationships(list, joinMap, true, leftRelationships);
			Relationships.MapRelationships(list, joinMap, false, rightRelationships);
			if (rightColumnIdentities != null)
			{
				ColumnIdentity[] array = new ColumnIdentity[rightKeyColumns.Length];
				for (int i = 0; i < array.Length; i++)
				{
					ColumnIdentity columnIdentity = rightColumnIdentities[rightKeyColumns[i]];
					if (columnIdentity == null)
					{
						array = null;
						break;
					}
					array[i] = columnIdentity;
				}
				if (array != null)
				{
					list.Add(new Relationship(leftKeyColumns, array));
				}
			}
			return list;
		}

		// Token: 0x06008F2D RID: 36653 RVA: 0x001DD11C File Offset: 0x001DB31C
		public static IList<Relationship> NestedJoin(IList<Relationship> relationships, int[] leftKeyColumns, Value rightTable, Keys rightKey)
		{
			Relationship[] array = new Relationship[relationships.Count + 1];
			for (int i = 0; i < relationships.Count; i++)
			{
				array[i] = relationships[i];
			}
			array[array.Length - 1] = new Relationship(leftKeyColumns, rightTable, rightKey);
			return array;
		}

		// Token: 0x06008F2E RID: 36654 RVA: 0x001DD164 File Offset: 0x001DB364
		public static IList<Relationship> ExpandListColumn(IList<Relationship> relationships, int columnIndex, bool singleOrDefault)
		{
			if (singleOrDefault || relationships.Count == 0)
			{
				return relationships;
			}
			List<Relationship> list = new List<Relationship>();
			foreach (Relationship relationship in relationships)
			{
				if (Array.IndexOf<int>(relationship.LeftKeyColumns, columnIndex) == -1)
				{
					list.Add(relationship);
				}
			}
			return list;
		}

		// Token: 0x06008F2F RID: 36655 RVA: 0x001DD1D0 File Offset: 0x001DB3D0
		public static IList<Relationship> ExpandRecordColumn(IList<RelatedTable> relatedTables, IList<Relationship> relationships, int columnToExpand, Keys fieldsToProject)
		{
			List<Relationship> list = new List<Relationship>();
			foreach (Relationship relationship in relationships)
			{
				int[] array = TableKeys.ExpandRecordColumnColumns(relationship.LeftKeyColumns, columnToExpand, fieldsToProject.Length);
				if (array != null)
				{
					list.Add(relationship.SelectColumns(array));
				}
			}
			RelatedTable relatedTable = relatedTables.Where((RelatedTable r) => r.Column == columnToExpand).SingleOrDefault<RelatedTable>();
			if (relatedTable != null)
			{
				int[] columnsOrNull = TableValue.GetColumnsOrNull(relatedTable.Table.Columns, fieldsToProject);
				if (columnsOrNull != null)
				{
					ColumnSelection.SelectMap selectMap = new ColumnSelection(fieldsToProject, columnsOrNull).CreateSelectMap(relatedTable.Table.Columns);
					foreach (Relationship relationship2 in relatedTable.Table.Relationships)
					{
						int[] array2 = selectMap.MapColumns(relationship2.LeftKeyColumns);
						if (array2 != null)
						{
							for (int i = 0; i < array2.Length; i++)
							{
								array2[i] += columnToExpand;
							}
							list.Add(relationship2.SelectColumns(array2));
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06008F30 RID: 36656 RVA: 0x001DD32C File Offset: 0x001DB52C
		private static void MapRelationships(List<Relationship> newRelationships, JoinQuery.JoinMap joinMap, bool left, IList<Relationship> relationships)
		{
			for (int i = 0; i < relationships.Count; i++)
			{
				Relationship relationship = relationships[i];
				int[] array = joinMap.MapColumns(left, relationship.LeftKeyColumns);
				if (array != null)
				{
					newRelationships.Add(relationship.SelectColumns(array));
				}
			}
		}

		// Token: 0x04004D7A RID: 19834
		public static readonly IList<Relationship> None = new Relationship[0];
	}
}
