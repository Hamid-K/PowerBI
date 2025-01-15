using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Xml
{
	// Token: 0x0200028F RID: 655
	internal class XmlTableColumn : XmlColumn
	{
		// Token: 0x06001AA0 RID: 6816 RVA: 0x00035B59 File Offset: 0x00033D59
		public XmlTableColumn(string name)
			: base(name)
		{
		}

		// Token: 0x06001AA1 RID: 6817 RVA: 0x00035B6D File Offset: 0x00033D6D
		public void AddRow(RecordValue row)
		{
			this.rows.Add(row);
		}

		// Token: 0x06001AA2 RID: 6818 RVA: 0x00035B7C File Offset: 0x00033D7C
		public override Value Reduce(XmlTableOptions options)
		{
			if (this.rows.Count != 1)
			{
				return XmlTableColumn.ToValue(this.rows, options);
			}
			RecordValue recordValue = this.rows[0];
			if (recordValue.Keys.Length == 1 && recordValue.Keys[0] == "Element:Text")
			{
				return recordValue[0];
			}
			return XmlTableColumn.ToValue(recordValue, options);
		}

		// Token: 0x06001AA3 RID: 6819 RVA: 0x00035BE8 File Offset: 0x00033DE8
		private static Value ToValue(RecordValue record, XmlTableOptions options)
		{
			Keys keys = record.Keys;
			XmlTableColumn.ColumnShape[] array = new XmlTableColumn.ColumnShape[keys.Length];
			XmlTableColumn.AccumulateShape(array, keys, record);
			Value value = XmlTableColumn.CleanRow(keys, array, record);
			TableTypeValue type = XmlTableColumn.GetType(keys, array);
			return ListValue.New(new Value[] { value }).ToTable(type);
		}

		// Token: 0x06001AA4 RID: 6820 RVA: 0x00035C38 File Offset: 0x00033E38
		private static Value ToValue(List<RecordValue> records, XmlTableOptions options)
		{
			Keys keys = XmlTableColumn.GetKeys(records);
			XmlTableColumn.ColumnShape[] array = new XmlTableColumn.ColumnShape[keys.Length];
			for (int i = 0; i < records.Count; i++)
			{
				XmlTableColumn.AccumulateShape(array, keys, records[i]);
			}
			Value[] array2 = new Value[records.Count];
			for (int j = 0; j < records.Count; j++)
			{
				array2[j] = XmlTableColumn.CleanRow(keys, array, records[j]);
			}
			TableTypeValue type = XmlTableColumn.GetType(keys, array);
			return ListValue.New(array2).ToTable(type);
		}

		// Token: 0x06001AA5 RID: 6821 RVA: 0x00035CC4 File Offset: 0x00033EC4
		private static Keys GetKeys(List<RecordValue> records)
		{
			KeysBuilder keysBuilder = default(KeysBuilder);
			foreach (RecordValue recordValue in records)
			{
				keysBuilder.Union(recordValue.Keys);
			}
			return keysBuilder.ToKeys();
		}

		// Token: 0x06001AA6 RID: 6822 RVA: 0x00035D28 File Offset: 0x00033F28
		private static TableTypeValue GetType(Keys keys, XmlTableColumn.ColumnShape[] shapes)
		{
			Value[] array = new Value[keys.Length];
			for (int i = 0; i < keys.Length; i++)
			{
				array[i] = XmlTableColumn.descForShape[(int)shapes[i]];
			}
			return XmlTableValue.TypeForTable(keys, array);
		}

		// Token: 0x06001AA7 RID: 6823 RVA: 0x00035D68 File Offset: 0x00033F68
		private static RecordValue CleanRow(Keys keys, XmlTableColumn.ColumnShape[] shapes, RecordValue record)
		{
			Value[] array = null;
			bool flag = record.Keys.Length == keys.Length;
			for (int i = 0; i < keys.Length; i++)
			{
				string text = keys[i];
				Value value;
				if (!record.TryGetValue(text, out value))
				{
					if (array == null)
					{
						array = new Value[keys.Length];
					}
					array[i] = Value.Null;
				}
				else if (shapes[i] == XmlTableColumn.ColumnShape.Mixed && value.IsText)
				{
					if (array == null)
					{
						array = new Value[keys.Length];
					}
					array[i] = ListValue.New(new Value[] { RecordValue.New(XmlTableColumn.textSingletonKeys, new Value[] { value }) }).ToTable(XmlTableColumn.textSingletonTableType);
				}
				else if (flag && record.Keys[i] != text)
				{
					flag = false;
				}
			}
			if (array == null && flag)
			{
				return record;
			}
			if (array == null)
			{
				array = new Value[keys.Length];
			}
			for (int j = 0; j < keys.Length; j++)
			{
				if (array[j] == null && !record.TryGetValue(keys[j], out array[j]))
				{
					throw new InvalidOperationException();
				}
			}
			return RecordValue.New(keys, array);
		}

		// Token: 0x06001AA8 RID: 6824 RVA: 0x00035E90 File Offset: 0x00034090
		private static void AccumulateShape(XmlTableColumn.ColumnShape[] shapes, Keys keys, RecordValue record)
		{
			for (int i = 0; i < keys.Length; i++)
			{
				string text = keys[i];
				Value @null;
				if (!record.TryGetValue(text, out @null))
				{
					@null = Value.Null;
				}
				XmlTableColumn.AccumulateShape(ref shapes[i], @null);
			}
		}

		// Token: 0x06001AA9 RID: 6825 RVA: 0x00035ED4 File Offset: 0x000340D4
		private static void AccumulateShape(ref XmlTableColumn.ColumnShape shape, Value cell)
		{
			switch (shape)
			{
			case XmlTableColumn.ColumnShape.Indeterminate:
			{
				ValueKind valueKind = cell.Kind;
				if (valueKind == ValueKind.Null)
				{
					shape = XmlTableColumn.ColumnShape.Null;
					return;
				}
				if (valueKind == ValueKind.Text)
				{
					shape = XmlTableColumn.ColumnShape.Text;
					return;
				}
				if (valueKind != ValueKind.Table)
				{
					return;
				}
				shape = XmlTableColumn.ColumnShape.Table;
				return;
			}
			case XmlTableColumn.ColumnShape.Null:
			{
				ValueKind valueKind = cell.Kind;
				if (valueKind == ValueKind.Text)
				{
					shape = XmlTableColumn.ColumnShape.NullableText;
					return;
				}
				if (valueKind != ValueKind.Table)
				{
					return;
				}
				shape = XmlTableColumn.ColumnShape.NullableTable;
				return;
			}
			case XmlTableColumn.ColumnShape.Text:
			{
				ValueKind valueKind = cell.Kind;
				if (valueKind == ValueKind.Null)
				{
					shape = XmlTableColumn.ColumnShape.NullableText;
					return;
				}
				if (valueKind != ValueKind.Table)
				{
					return;
				}
				shape = XmlTableColumn.ColumnShape.Mixed;
				return;
			}
			case XmlTableColumn.ColumnShape.NullableText:
				if (cell.Kind == ValueKind.Table)
				{
					shape = XmlTableColumn.ColumnShape.Mixed;
					return;
				}
				break;
			case XmlTableColumn.ColumnShape.Table:
			{
				ValueKind valueKind = cell.Kind;
				if (valueKind == ValueKind.Null)
				{
					shape = XmlTableColumn.ColumnShape.NullableTable;
					return;
				}
				if (valueKind != ValueKind.Text)
				{
					return;
				}
				shape = XmlTableColumn.ColumnShape.Mixed;
				return;
			}
			case XmlTableColumn.ColumnShape.NullableTable:
				if (cell.Kind == ValueKind.Text)
				{
					shape = XmlTableColumn.ColumnShape.Mixed;
					return;
				}
				break;
			case XmlTableColumn.ColumnShape.Mixed:
				break;
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x040007EB RID: 2027
		public const string TextColumnName = "Element:Text";

		// Token: 0x040007EC RID: 2028
		public const string AttributeColumnPrefix = "Attribute:";

		// Token: 0x040007ED RID: 2029
		public const string NamespaceColumnPrefix = "Namespace:";

		// Token: 0x040007EE RID: 2030
		private static readonly Keys textSingletonKeys = Keys.New("Element:Text");

		// Token: 0x040007EF RID: 2031
		private static readonly TableTypeValue textSingletonTableType = TableTypeValue.New(RecordTypeValue.New(RecordValue.New(XmlTableColumn.textSingletonKeys, new Value[] { RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
		{
			TypeValue.Text,
			LogicalValue.False
		}) })));

		// Token: 0x040007F0 RID: 2032
		private List<RecordValue> rows = new List<RecordValue>();

		// Token: 0x040007F1 RID: 2033
		private static RecordValue[] descForShape = new RecordValue[]
		{
			XmlTableValue.FieldDescriptor(TypeValue.Any),
			XmlTableValue.FieldDescriptor(TypeValue.Null),
			XmlTableValue.FieldDescriptor(DataSource.SerializedTextType),
			XmlTableValue.FieldDescriptor(DataSource.NullableSerializedTextType),
			XmlTableValue.FieldDescriptor(TypeValue.Table),
			XmlTableValue.FieldDescriptor(NullableTypeValue.Table),
			XmlTableValue.FieldDescriptor(TypeValue.Any)
		};

		// Token: 0x02000290 RID: 656
		private enum ColumnShape
		{
			// Token: 0x040007F3 RID: 2035
			Indeterminate,
			// Token: 0x040007F4 RID: 2036
			Null,
			// Token: 0x040007F5 RID: 2037
			Text,
			// Token: 0x040007F6 RID: 2038
			NullableText,
			// Token: 0x040007F7 RID: 2039
			Table,
			// Token: 0x040007F8 RID: 2040
			NullableTable,
			// Token: 0x040007F9 RID: 2041
			Mixed
		}
	}
}
