using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Microsoft.Data.Serialization
{
	// Token: 0x02000158 RID: 344
	public class TableSchema : IEnumerable<SchemaColumn>, IEnumerable
	{
		// Token: 0x0600061F RID: 1567 RVA: 0x000098A2 File Offset: 0x00007AA2
		public TableSchema(int columnCount)
			: this(new List<SchemaColumn>(columnCount))
		{
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x000098B0 File Offset: 0x00007AB0
		public TableSchema()
			: this(new List<SchemaColumn>())
		{
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x000098BD File Offset: 0x00007ABD
		private TableSchema(List<SchemaColumn> columns)
		{
			this.columns = columns;
			this.extendedProperties = new PropertyCollection();
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x000098D8 File Offset: 0x00007AD8
		public static TableSchema FromDataTable(DataTable schemaTable)
		{
			List<KeyValuePair<DataColumn, TableSchema.ColumnInfo>> list = new List<KeyValuePair<DataColumn, TableSchema.ColumnInfo>>(schemaTable.Columns.Count);
			foreach (object obj in schemaTable.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				TableSchema.ColumnInfo columnInfo;
				if (TableSchema.attributeMap.TryGetValue(dataColumn.ColumnName, out columnInfo))
				{
					list.Add(new KeyValuePair<DataColumn, TableSchema.ColumnInfo>(dataColumn, columnInfo));
				}
			}
			DataColumn dataColumn2 = schemaTable.Columns[InformationSchemaTableColumnName.ColumnName];
			TableSchema tableSchema = new TableSchema(schemaTable.Rows.Count);
			foreach (object obj2 in schemaTable.Rows)
			{
				DataRow dataRow = (DataRow)obj2;
				SchemaColumn schemaColumn = new SchemaColumn(dataRow[dataColumn2] as string);
				foreach (KeyValuePair<DataColumn, TableSchema.ColumnInfo> keyValuePair in list)
				{
					keyValuePair.Value.SetValue(schemaColumn, dataRow[keyValuePair.Key]);
				}
				tableSchema.AddColumn(schemaColumn);
			}
			TableSchema.CopyProperties(tableSchema.ExtendedProperties, schemaTable.ExtendedProperties);
			return tableSchema;
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00009A50 File Offset: 0x00007C50
		public static TableSchema FromDataReader(IDataReader reader)
		{
			IDataReaderWithTableSchema dataReaderWithTableSchema = reader as IDataReaderWithTableSchema;
			if (dataReaderWithTableSchema != null)
			{
				return dataReaderWithTableSchema.Schema;
			}
			if (reader.FieldCount == 0)
			{
				return new TableSchema(0);
			}
			return TableSchema.FromDataTable(reader.GetSchemaTable());
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00009A88 File Offset: 0x00007C88
		public static TableSchema FromData(string[] names, object[] values)
		{
			TableSchema tableSchema = new TableSchema(names.Length);
			for (int i = 0; i < names.Length; i++)
			{
				Type type = ((values[i] == null) ? typeof(DBNull) : values[i].GetType());
				tableSchema.AddColumn(names[i], type, true);
			}
			return tableSchema;
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x00009AD4 File Offset: 0x00007CD4
		public void Serialize(Stream stream)
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(stream))
			{
				binaryWriter.Write(1);
				this.Serialize(binaryWriter);
			}
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x00009B14 File Offset: 0x00007D14
		internal void Serialize(BinaryWriter writer)
		{
			writer.Write(this.columns.Count);
			for (int i = 0; i < this.columns.Count; i++)
			{
				this.columns[i].Serialize(writer);
			}
			writer.Write(false);
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00009B64 File Offset: 0x00007D64
		public static TableSchema Deserialize(Stream stream)
		{
			TableSchema tableSchema;
			using (BinaryReader binaryReader = new BinaryReader(stream))
			{
				if (binaryReader.ReadInt32() != 1)
				{
					throw new InvalidOperationException("Invalid serialization version");
				}
				tableSchema = TableSchema.Deserialize(binaryReader);
			}
			return tableSchema;
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x00009BB0 File Offset: 0x00007DB0
		internal static TableSchema Deserialize(BinaryReader reader)
		{
			int num = reader.ReadInt32();
			List<SchemaColumn> list = new List<SchemaColumn>(num);
			for (int i = 0; i < num; i++)
			{
				list.Add(SchemaColumn.Deserialize(reader));
			}
			if (reader.ReadBoolean())
			{
				throw new InvalidOperationException("extended properties not supported");
			}
			return new TableSchema(list);
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x00009BFC File Offset: 0x00007DFC
		public int ColumnCount
		{
			get
			{
				return this.columns.Count;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x0600062A RID: 1578 RVA: 0x00009C09 File Offset: 0x00007E09
		public PropertyCollection ExtendedProperties
		{
			get
			{
				return this.extendedProperties;
			}
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x00009C14 File Offset: 0x00007E14
		public DataTable ToDataTable()
		{
			DataTable dataTable = new DataTable
			{
				Locale = CultureInfo.InvariantCulture
			};
			dataTable.Columns.Add(InformationSchemaTableColumnName.ColumnName, typeof(string));
			List<object[]> list = new List<object[]>(TableSchema.attributeList.Length + 1);
			List<object[]> list2 = list;
			object[] array = this.columns.Select((SchemaColumn c) => c.Name).ToArray<string>();
			list2.Add(array);
			foreach (TableSchema.ColumnInfo columnInfo in TableSchema.attributeList)
			{
				object[] array3 = null;
				for (int j = 0; j < this.ColumnCount; j++)
				{
					object value = columnInfo.GetValue(this.columns[j]);
					if (value != null)
					{
						array3 = array3 ?? new object[this.ColumnCount];
						array3[j] = value;
					}
				}
				if (array3 != null)
				{
					dataTable.Columns.Add(columnInfo.Name, columnInfo.Type);
					list.Add(array3);
				}
			}
			for (int k = 0; k < this.ColumnCount; k++)
			{
				object[] array4 = new object[list.Count];
				for (int l = 0; l < array4.Length; l++)
				{
					array4[l] = list[l][k];
				}
				dataTable.Rows.Add(array4);
			}
			TableSchema.CopyProperties(dataTable.ExtendedProperties, this.ExtendedProperties);
			return dataTable;
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x00009D94 File Offset: 0x00007F94
		public TableSchema Copy()
		{
			TableSchema tableSchema = new TableSchema(this.ColumnCount);
			foreach (SchemaColumn schemaColumn in this.columns)
			{
				tableSchema.AddColumn(schemaColumn.Clone(null));
			}
			TableSchema.CopyProperties(tableSchema.ExtendedProperties, this.ExtendedProperties);
			return tableSchema;
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x00009E0C File Offset: 0x0000800C
		public SchemaColumn GetColumn(string name)
		{
			SchemaColumn schemaColumn;
			if (!this.TryGetColumn(name, out schemaColumn))
			{
				throw new IndexOutOfRangeException();
			}
			return schemaColumn;
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x00009E2B File Offset: 0x0000802B
		public SchemaColumn GetColumn(int index)
		{
			return this.columns[index];
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x00009E3C File Offset: 0x0000803C
		public bool TryGetColumn(string name, out SchemaColumn column)
		{
			int num;
			if (this.TryGetColumn(name, out num))
			{
				column = this.columns[num];
				return true;
			}
			column = null;
			return false;
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x00009E68 File Offset: 0x00008068
		public bool TryGetColumn(string name, out int index)
		{
			if (this.byColumnName == null)
			{
				this.byColumnName = this.MakeColumnNameIndex();
			}
			return this.byColumnName.TryGetValue(name, out index);
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x00009E8C File Offset: 0x0000808C
		public SchemaColumn AddColumn(string name, Type type, bool nullable)
		{
			SchemaColumn schemaColumn = new SchemaColumn(name)
			{
				DataType = type,
				Nullable = nullable
			};
			this.AddColumn(schemaColumn);
			return schemaColumn;
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x00009EB8 File Offset: 0x000080B8
		public SchemaColumn AddColumn(string name, int ordinal, Type type, bool nullable)
		{
			SchemaColumn schemaColumn = new SchemaColumn(name)
			{
				DataType = type,
				Ordinal = new int?(ordinal),
				Nullable = nullable
			};
			this.AddColumn(schemaColumn);
			return schemaColumn;
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x00009EEF File Offset: 0x000080EF
		public void AddColumn(SchemaColumn column)
		{
			this.columns.Add(column);
			this.byColumnName = null;
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x00009F04 File Offset: 0x00008104
		public IEnumerator<SchemaColumn> GetEnumerator()
		{
			return this.columns.GetEnumerator();
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x00009F04 File Offset: 0x00008104
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.columns.GetEnumerator();
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x00009F18 File Offset: 0x00008118
		private Dictionary<string, int> MakeColumnNameIndex()
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>(this.columns.Count);
			for (int i = this.columns.Count - 1; i >= 0; i--)
			{
				if (this.columns[i].Name != null)
				{
					dictionary[this.columns[i].Name] = i;
				}
			}
			return dictionary;
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x00009F7C File Offset: 0x0000817C
		private static int? GetIntOrNull(object value)
		{
			if (value is int)
			{
				return new int?((int)value);
			}
			if (value is IConvertible && !(value is DBNull))
			{
				try
				{
					return new int?(Convert.ToInt32(value, CultureInfo.InvariantCulture));
				}
				catch (Exception ex) when (SafeExceptions.IsSafeException(ex))
				{
				}
			}
			return null;
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x00009FF4 File Offset: 0x000081F4
		private static long? GetLongOrNull(object value)
		{
			if (value is long)
			{
				return new long?((long)value);
			}
			if (value is IConvertible && !(value is DBNull))
			{
				try
				{
					return new long?(Convert.ToInt64(value, CultureInfo.InvariantCulture));
				}
				catch (Exception ex) when (SafeExceptions.IsSafeException(ex))
				{
				}
			}
			return null;
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x0000A06C File Offset: 0x0000826C
		private static bool? GetBoolOrNull(object value)
		{
			if (value is bool)
			{
				return new bool?((bool)value);
			}
			if (value is IConvertible && !(value is DBNull))
			{
				try
				{
					return new bool?(Convert.ToBoolean(value, CultureInfo.InvariantCulture));
				}
				catch (Exception ex) when (SafeExceptions.IsSafeException(ex))
				{
				}
			}
			return null;
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0000A0E4 File Offset: 0x000082E4
		private static TableSchema GetSchemaOrNull(object value)
		{
			if (value is TableSchema)
			{
				return (TableSchema)value;
			}
			if (value is DataTable)
			{
				return TableSchema.FromDataTable((DataTable)value);
			}
			return null;
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0000A10C File Offset: 0x0000830C
		private static void CopyProperties(PropertyCollection destination, PropertyCollection source)
		{
			foreach (object obj in source)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				destination[dictionaryEntry.Key] = dictionaryEntry.Value;
			}
		}

		// Token: 0x040003E7 RID: 999
		private const int SerializationVersion = 1;

		// Token: 0x040003E8 RID: 1000
		private static readonly TableSchema.ColumnInfo[] attributeList = new TableSchema.ColumnInfo[]
		{
			new TableSchema.ColumnInfo(InformationSchemaTableColumnName.DataType, typeof(Type), (SchemaColumn col) => col.DataType, delegate(SchemaColumn col, object value)
			{
				col.DataType = value as Type;
			}),
			new TableSchema.ColumnInfo(InformationSchemaTableColumnName.ColumnOrdinal, typeof(int), (SchemaColumn col) => col.Ordinal, delegate(SchemaColumn col, object value)
			{
				col.Ordinal = TableSchema.GetIntOrNull(value);
			}),
			new TableSchema.ColumnInfo(InformationSchemaTableColumnName.AllowDBNull, typeof(bool), (SchemaColumn col) => col.Nullable, delegate(SchemaColumn col, object value)
			{
				col.Nullable = TableSchema.GetBoolOrNull(value).GetValueOrDefault(true);
			}),
			new TableSchema.ColumnInfo(InformationSchemaTableColumnName.IsKey, typeof(bool), (SchemaColumn col) => col.IsKey, delegate(SchemaColumn col, object value)
			{
				col.IsKey = TableSchema.GetBoolOrNull(value).GetValueOrDefault();
			}),
			new TableSchema.ColumnInfo(InformationSchemaTableColumnName.DataTypeName, typeof(string), (SchemaColumn col) => col.DataTypeName, delegate(SchemaColumn col, object value)
			{
				col.DataTypeName = value as string;
			}),
			new TableSchema.ColumnInfo(InformationSchemaTableColumnName.ColumnSize, typeof(long), (SchemaColumn col) => col.ColumnSize, delegate(SchemaColumn col, object value)
			{
				col.ColumnSize = TableSchema.GetLongOrNull(value);
			}),
			new TableSchema.ColumnInfo(InformationSchemaTableColumnName.NumericBase, typeof(int), (SchemaColumn col) => col.NumericBase, delegate(SchemaColumn col, object value)
			{
				col.NumericBase = TableSchema.GetIntOrNull(value);
			}),
			new TableSchema.ColumnInfo(InformationSchemaTableColumnName.NumericPrecision, typeof(int), (SchemaColumn col) => col.NumericPrecision, delegate(SchemaColumn col, object value)
			{
				col.NumericPrecision = TableSchema.GetIntOrNull(value);
			}),
			new TableSchema.ColumnInfo(InformationSchemaTableColumnName.NumericScale, typeof(int), (SchemaColumn col) => col.NumericScale, delegate(SchemaColumn col, object value)
			{
				col.NumericScale = TableSchema.GetIntOrNull(value);
			}),
			new TableSchema.ColumnInfo(InformationSchemaTableColumnName.IsUnsigned, typeof(bool), (SchemaColumn col) => col.IsUnsigned, delegate(SchemaColumn col, object value)
			{
				col.IsUnsigned = TableSchema.GetBoolOrNull(value);
			}),
			new TableSchema.ColumnInfo(InformationSchemaTableColumnName.ProviderType, typeof(int), (SchemaColumn col) => col.ProviderType, delegate(SchemaColumn col, object value)
			{
				col.ProviderType = TableSchema.GetIntOrNull(value);
			}),
			new TableSchema.ColumnInfo(InformationSchemaTableColumnName.ProviderSpecificDataType, typeof(Type), (SchemaColumn col) => col.ProviderSpecificDataType, delegate(SchemaColumn col, object value)
			{
				col.ProviderSpecificDataType = value as Type;
			}),
			new TableSchema.ColumnInfo("Schema", typeof(DataTable), delegate(SchemaColumn col)
			{
				TableSchema columnSchema = col.ColumnSchema;
				if (columnSchema == null)
				{
					return null;
				}
				return columnSchema.ToDataTable();
			}, delegate(SchemaColumn col, object value)
			{
				col.ColumnSchema = TableSchema.GetSchemaOrNull(value);
			})
		};

		// Token: 0x040003E9 RID: 1001
		private static readonly Dictionary<string, TableSchema.ColumnInfo> attributeMap = TableSchema.attributeList.ToDictionary((TableSchema.ColumnInfo c) => c.Name, (TableSchema.ColumnInfo c) => c);

		// Token: 0x040003EA RID: 1002
		private readonly List<SchemaColumn> columns;

		// Token: 0x040003EB RID: 1003
		private readonly PropertyCollection extendedProperties;

		// Token: 0x040003EC RID: 1004
		private Dictionary<string, int> byColumnName;

		// Token: 0x02000159 RID: 345
		private struct ColumnInfo
		{
			// Token: 0x0600063D RID: 1597 RVA: 0x0000A4BB File Offset: 0x000086BB
			public ColumnInfo(string name, Type type, Func<SchemaColumn, object> getValue, Action<SchemaColumn, object> setValue)
			{
				this.name = name;
				this.type = type;
				this.getValue = getValue;
				this.setValue = setValue;
			}

			// Token: 0x1700021E RID: 542
			// (get) Token: 0x0600063E RID: 1598 RVA: 0x0000A4DA File Offset: 0x000086DA
			public string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x1700021F RID: 543
			// (get) Token: 0x0600063F RID: 1599 RVA: 0x0000A4E2 File Offset: 0x000086E2
			public Type Type
			{
				get
				{
					return this.type;
				}
			}

			// Token: 0x06000640 RID: 1600 RVA: 0x0000A4EA File Offset: 0x000086EA
			public object GetValue(SchemaColumn column)
			{
				return this.getValue(column);
			}

			// Token: 0x06000641 RID: 1601 RVA: 0x0000A4F8 File Offset: 0x000086F8
			public void SetValue(SchemaColumn column, object value)
			{
				this.setValue(column, value);
			}

			// Token: 0x040003ED RID: 1005
			private readonly string name;

			// Token: 0x040003EE RID: 1006
			private readonly Type type;

			// Token: 0x040003EF RID: 1007
			private readonly Func<SchemaColumn, object> getValue;

			// Token: 0x040003F0 RID: 1008
			private readonly Action<SchemaColumn, object> setValue;
		}
	}
}
