using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001626 RID: 5670
	internal static class TableTypeMetadata
	{
		// Token: 0x06008EF2 RID: 36594 RVA: 0x001DC460 File Offset: 0x001DA660
		public static IList<TableKey> GetTableKeys(Keys columns, ListValue list)
		{
			List<TableKey> list2 = new List<TableKey>();
			bool flag = false;
			foreach (IValueReference valueReference in list)
			{
				TableKey tableKey = TableTypeMetadata.GetTableKey(valueReference.Value.AsRecord, columns);
				if (tableKey.Primary)
				{
					if (flag)
					{
						throw ValueException.NewExpressionError<Message0>(Strings.TableAddKey_PrimaryKeyAlreadyExists, list, null);
					}
					flag = true;
				}
				list2.Add(tableKey);
			}
			return list2.ToArray();
		}

		// Token: 0x06008EF3 RID: 36595 RVA: 0x001DC4E0 File Offset: 0x001DA6E0
		public static ListValue GetTableKeys(IList<TableKey> tableKeys, Keys columns)
		{
			Value[] array = new Value[tableKeys.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = TableTypeMetadata.GetTableKey(tableKeys[i], columns);
			}
			return ListValue.New(array);
		}

		// Token: 0x06008EF4 RID: 36596 RVA: 0x001DC520 File Offset: 0x001DA720
		private static RecordValue GetTableKey(TableKey tableKey, Keys keys)
		{
			Value[] array = new Value[tableKey.Columns.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = TextValue.New(keys[tableKey.Columns[i]]);
			}
			KeysBuilder keysBuilder = default(KeysBuilder);
			List<Value> list = new List<Value>();
			keysBuilder.Add("Columns");
			list.Add(ListValue.New(array));
			keysBuilder.Add("Primary");
			list.Add(LogicalValue.New(tableKey.Primary));
			return RecordValue.New(keysBuilder.ToKeys(), list.ToArray());
		}

		// Token: 0x06008EF5 RID: 36597 RVA: 0x001DC5B4 File Offset: 0x001DA7B4
		private static TableKey GetTableKey(RecordValue record, Keys keys)
		{
			Value value = record["Columns"];
			int[] columns = TableValue.GetColumns(keys, value);
			bool asBoolean = record["Primary"].AsBoolean;
			return new TableKey(columns, asBoolean);
		}

		// Token: 0x04004D69 RID: 19817
		private const string TableKeysColumns = "Columns";

		// Token: 0x04004D6A RID: 19818
		private const string TableKeysPrimary = "Primary";
	}
}
