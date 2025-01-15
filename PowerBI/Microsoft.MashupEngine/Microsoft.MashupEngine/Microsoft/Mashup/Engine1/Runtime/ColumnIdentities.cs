using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine1.Language.Query;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001630 RID: 5680
	public static class ColumnIdentities
	{
		// Token: 0x06008F1D RID: 36637 RVA: 0x001DCC58 File Offset: 0x001DAE58
		public static ColumnIdentity[] New(Keys columns, ColumnIdentity[] columnIdentities, string identity)
		{
			if (columnIdentities != null && Array.IndexOf<ColumnIdentity>(columnIdentities, null) == -1)
			{
				return columnIdentities;
			}
			identity = ColumnIdentity.Escape(identity);
			ColumnIdentity[] array = new ColumnIdentity[columns.Length];
			for (int i = 0; i < array.Length; i++)
			{
				if (columnIdentities != null && columnIdentities[i] != null)
				{
					array[i] = columnIdentities[i];
				}
				else
				{
					array[i] = ColumnIdentity.New(identity, columns[i], i);
				}
			}
			return array;
		}

		// Token: 0x06008F1E RID: 36638 RVA: 0x001DCCB8 File Offset: 0x001DAEB8
		public static ColumnIdentity[] SelectColumns(ColumnIdentity[] columnIdentities, ColumnSelection columnSelection)
		{
			if (columnIdentities != null)
			{
				ColumnIdentity[] array = new ColumnIdentity[columnSelection.Keys.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = columnIdentities[columnSelection.GetColumn(i)];
				}
				return array;
			}
			return null;
		}

		// Token: 0x06008F1F RID: 36639 RVA: 0x001DCCF8 File Offset: 0x001DAEF8
		public static ColumnIdentity[] AddColumns(ColumnIdentity[] columnIdentities, int count)
		{
			if (columnIdentities != null)
			{
				ColumnIdentity[] array = new ColumnIdentity[columnIdentities.Length + count];
				columnIdentities.CopyTo(array, 0);
				return array;
			}
			return null;
		}

		// Token: 0x06008F20 RID: 36640 RVA: 0x001DCD20 File Offset: 0x001DAF20
		public static ColumnIdentity[] TransformColumns(ColumnIdentity[] columnIdentities, ColumnTransforms columnTransforms)
		{
			if (columnIdentities != null)
			{
				ColumnIdentity[] array = (ColumnIdentity[])columnIdentities.Clone();
				foreach (int num in columnTransforms.Dictionary.Keys)
				{
					array[num] = null;
				}
				return array;
			}
			return null;
		}

		// Token: 0x06008F21 RID: 36641 RVA: 0x001DCD84 File Offset: 0x001DAF84
		public static ColumnIdentity[] Join(ColumnIdentity[] leftIdentities, ColumnIdentity[] rightIdentities, JoinColumn[] joinColumns)
		{
			if (leftIdentities != null || rightIdentities != null)
			{
				ColumnIdentity[] array = new ColumnIdentity[joinColumns.Length];
				for (int i = 0; i < array.Length; i++)
				{
					if (leftIdentities != null && joinColumns[i].Left)
					{
						array[i] = leftIdentities[joinColumns[i].LeftColumn];
					}
					else if (rightIdentities != null && joinColumns[i].Right)
					{
						array[i] = rightIdentities[joinColumns[i].RightColumn];
					}
				}
				return array;
			}
			return null;
		}

		// Token: 0x06008F22 RID: 36642 RVA: 0x001DCDF7 File Offset: 0x001DAFF7
		public static ColumnIdentity[] ExpandListColumn(ColumnIdentity[] columnIdentities, int columnIndex, bool singleOrDefault)
		{
			if (singleOrDefault || columnIdentities == null)
			{
				return columnIdentities;
			}
			ColumnIdentity[] array = (ColumnIdentity[])columnIdentities.Clone();
			array[columnIndex] = null;
			return array;
		}

		// Token: 0x06008F23 RID: 36643 RVA: 0x001DCE10 File Offset: 0x001DB010
		public static ColumnIdentity[] ExpandRecordColumn(IList<RelatedTable> relatedTables, int columnCount, ColumnIdentity[] columnIdentities, int columnToExpand, Keys fieldsToProject)
		{
			bool flag = false;
			ColumnIdentity[] array = new ColumnIdentity[columnCount + fieldsToProject.Length - 1];
			if (columnIdentities != null)
			{
				int num = columnIdentities.Length - columnToExpand - 1;
				Array.Copy(columnIdentities, 0, array, 0, columnToExpand);
				Array.Copy(columnIdentities, columnToExpand + 1, array, columnToExpand + fieldsToProject.Length, num);
				flag = true;
			}
			RelatedTable relatedTable = relatedTables.Where((RelatedTable r) => r.Column == columnToExpand).SingleOrDefault<RelatedTable>();
			if (relatedTable != null)
			{
				ColumnIdentity[] columnIdentities2 = relatedTable.Table.ColumnIdentities;
				if (columnIdentities2 != null)
				{
					int[] columnsOrNull = TableValue.GetColumnsOrNull(relatedTable.Table.Columns, fieldsToProject);
					if (columnsOrNull != null)
					{
						for (int i = 0; i < columnsOrNull.Length; i++)
						{
							ColumnIdentity columnIdentity = columnIdentities2[columnsOrNull[i]];
							array[columnToExpand + i] = columnIdentity;
							flag |= columnIdentity != null;
						}
					}
				}
			}
			if (!flag)
			{
				return null;
			}
			return array;
		}
	}
}
