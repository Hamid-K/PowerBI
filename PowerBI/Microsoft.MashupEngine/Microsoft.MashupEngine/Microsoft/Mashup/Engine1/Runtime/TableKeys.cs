using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Language.Query;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001627 RID: 5671
	public static class TableKeys
	{
		// Token: 0x06008EF6 RID: 36598 RVA: 0x001DC5EC File Offset: 0x001DA7EC
		public static bool IsTableKey(IList<TableKey> tableKeys, int[] columns)
		{
			for (int i = 0; i < tableKeys.Count; i++)
			{
				TableKey tableKey = tableKeys[i];
				bool flag = columns.Length == tableKey.Columns.Length;
				int num = 0;
				while (flag && num < columns.Length)
				{
					flag = Array.IndexOf<int>(tableKey.Columns, columns[num]) != -1;
					num++;
				}
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06008EF7 RID: 36599 RVA: 0x001DC64C File Offset: 0x001DA84C
		public static IList<TableKey> SelectColumns(IList<TableKey> tableKeys, Keys columns, ColumnSelection columnSelection)
		{
			if (tableKeys.Count == 0)
			{
				return TableKeys.None;
			}
			ColumnSelection.SelectMap selectMap = columnSelection.CreateSelectMap(columns);
			List<TableKey> list = new List<TableKey>(tableKeys.Count);
			foreach (TableKey tableKey in tableKeys)
			{
				int[] array = selectMap.MapColumns(tableKey.Columns);
				if (array != null)
				{
					list.Add(tableKey.SelectColumns(array));
				}
			}
			if (list.Count <= 0)
			{
				return TableKeys.None;
			}
			return list.ToArray();
		}

		// Token: 0x06008EF8 RID: 36600 RVA: 0x001DC6E8 File Offset: 0x001DA8E8
		public static IList<TableKey> ExpandRecordColumn(IList<TableKey> tableKeys, int columnToExpand, int fieldsToProjectCount)
		{
			List<TableKey> list = new List<TableKey>(tableKeys.Count);
			foreach (TableKey tableKey in tableKeys)
			{
				int[] array = TableKeys.ExpandRecordColumnColumns(tableKey.Columns, columnToExpand, fieldsToProjectCount);
				if (array != null)
				{
					list.Add(tableKey.SelectColumns(array));
				}
			}
			return list.ToArray();
		}

		// Token: 0x06008EF9 RID: 36601 RVA: 0x001DC75C File Offset: 0x001DA95C
		public static int[] ExpandRecordColumnColumns(int[] columns, int columnToExpand, int fieldsToProjectCount)
		{
			int[] array = new int[columns.Length];
			for (int i = 0; i < array.Length; i++)
			{
				int num = TableKeys.ExpandRecordColumnColumn(columns[i], columnToExpand, fieldsToProjectCount);
				if (num == -1)
				{
					array = null;
					break;
				}
				array[i] = num;
			}
			return array;
		}

		// Token: 0x06008EFA RID: 36602 RVA: 0x001DC798 File Offset: 0x001DA998
		public static int ExpandRecordColumnColumn(int column, int columnToExpand, int fieldsToProjectCount)
		{
			if (column < columnToExpand)
			{
				return column;
			}
			if (column > columnToExpand)
			{
				return column + fieldsToProjectCount - 1;
			}
			return -1;
		}

		// Token: 0x04004D6B RID: 19819
		public static readonly IList<TableKey> None = new TableKey[0];
	}
}
