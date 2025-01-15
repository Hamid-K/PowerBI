using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization
{
	// Token: 0x020000E2 RID: 226
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public class PageReaderDataReader : IDataReader, IDisposable, IDataRecord
	{
		// Token: 0x06000454 RID: 1108 RVA: 0x0000D37B File Offset: 0x0000B57B
		public PageReaderDataReader(IPageReader reader)
			: this(reader, null)
		{
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0000D388 File Offset: 0x0000B588
		public PageReaderDataReader(IPageReader reader, Func<IDictionary<string, string>, Exception> propertyHandler)
		{
			this.reader = new SinglePageReader(reader);
			this.propertyHandler = propertyHandler;
			this.names = PageReaderDataReader.GetSchemaInfo<string>(reader.SchemaTable, "ColumnName");
			this.types = PageReaderDataReader.GetSchemaInfo<Type>(reader.SchemaTable, "DataType");
			this.page = this.reader.Page;
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x0000D3EB File Offset: 0x0000B5EB
		public int Depth
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x0000D3EE File Offset: 0x0000B5EE
		public bool IsClosed
		{
			get
			{
				return this.reader == null;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x0000D3F9 File Offset: 0x0000B5F9
		public int RecordsAffected
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x0000D3FC File Offset: 0x0000B5FC
		public int FieldCount
		{
			get
			{
				return this.names.Length;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x0000D406 File Offset: 0x0000B606
		public bool HasRows
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170000F3 RID: 243
		public object this[string name]
		{
			get
			{
				return this.GetValue(this.GetOrdinal(name));
			}
		}

		// Token: 0x170000F4 RID: 244
		public object this[int i]
		{
			get
			{
				return this.GetValue(i);
			}
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0000D425 File Offset: 0x0000B625
		public bool NextResult()
		{
			return false;
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0000D428 File Offset: 0x0000B628
		public bool GetBoolean(int ordinal)
		{
			return this.GetColumn(ordinal).GetBoolean(this.rowIndex);
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0000D43C File Offset: 0x0000B63C
		public byte GetByte(int ordinal)
		{
			return this.GetColumn(ordinal).GetByte(this.rowIndex);
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0000D450 File Offset: 0x0000B650
		public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0000D457 File Offset: 0x0000B657
		public char GetChar(int ordinal)
		{
			return (char)this.GetValue(ordinal);
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0000D465 File Offset: 0x0000B665
		public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0000D46C File Offset: 0x0000B66C
		public IDataReader GetData(int i)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0000D473 File Offset: 0x0000B673
		public string GetDataTypeName(int ordinal)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0000D47A File Offset: 0x0000B67A
		public DateTime GetDateTime(int ordinal)
		{
			return this.GetColumn(ordinal).GetDateTime(this.rowIndex);
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0000D48E File Offset: 0x0000B68E
		public decimal GetDecimal(int ordinal)
		{
			return this.GetColumn(ordinal).GetDecimal(this.rowIndex);
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0000D4A2 File Offset: 0x0000B6A2
		public double GetDouble(int ordinal)
		{
			return this.GetColumn(ordinal).GetDouble(this.rowIndex);
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0000D4B6 File Offset: 0x0000B6B6
		public Type GetFieldType(int ordinal)
		{
			return this.types[ordinal];
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0000D4C0 File Offset: 0x0000B6C0
		public float GetFloat(int ordinal)
		{
			return this.GetColumn(ordinal).GetFloat(this.rowIndex);
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0000D4D4 File Offset: 0x0000B6D4
		public Guid GetGuid(int ordinal)
		{
			return this.GetColumn(ordinal).GetGuid(this.rowIndex);
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0000D4E8 File Offset: 0x0000B6E8
		public short GetInt16(int ordinal)
		{
			return this.GetColumn(ordinal).GetInt16(this.rowIndex);
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0000D4FC File Offset: 0x0000B6FC
		public int GetInt32(int ordinal)
		{
			return this.GetColumn(ordinal).GetInt32(this.rowIndex);
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0000D510 File Offset: 0x0000B710
		public long GetInt64(int ordinal)
		{
			return this.GetColumn(ordinal).GetInt64(this.rowIndex);
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0000D524 File Offset: 0x0000B724
		public string GetName(int ordinal)
		{
			return this.names[ordinal];
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0000D530 File Offset: 0x0000B730
		public int GetOrdinal(string name)
		{
			if (this.ordinals == null)
			{
				Dictionary<string, int> dictionary = new Dictionary<string, int>(this.names.Length);
				for (int i = 0; i < this.names.Length; i++)
				{
					dictionary.Add(this.names[i], i);
				}
				this.ordinals = dictionary;
			}
			int num;
			if (!this.ordinals.TryGetValue(name, out num))
			{
				throw new IndexOutOfRangeException();
			}
			return num;
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0000D593 File Offset: 0x0000B793
		public string GetString(int ordinal)
		{
			return this.GetColumn(ordinal).GetString(this.rowIndex);
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0000D5A7 File Offset: 0x0000B7A7
		public object GetValue(int ordinal)
		{
			return this.GetColumn(ordinal).GetObject(this.rowIndex);
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0000D5BC File Offset: 0x0000B7BC
		public int GetValues(object[] values)
		{
			int num = Math.Min(values.Length, this.page.ColumnCount);
			for (int i = 0; i < num; i++)
			{
				values[i] = this.GetValue(i);
			}
			return num;
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0000D5F4 File Offset: 0x0000B7F4
		public bool IsDBNull(int ordinal)
		{
			return this.GetColumn(ordinal).IsNull(this.rowIndex);
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0000D608 File Offset: 0x0000B808
		public IEnumerator GetEnumerator()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0000D610 File Offset: 0x0000B810
		public bool Read()
		{
			for (;;)
			{
				this.rowIndex++;
				if (this.rowIndex < this.page.RowCount)
				{
					break;
				}
				this.reader.Read();
				this.page = this.reader.Page;
				if (this.page.RowCount == 0)
				{
					return false;
				}
				this.rowIndex = -1;
			}
			if (this.propertyHandler != null)
			{
				IExceptionRow exceptionRow;
				if (this.page.ExceptionRows.TryGetValue(this.rowIndex, out exceptionRow))
				{
					this.exceptions = exceptionRow.Exceptions;
				}
				else
				{
					this.exceptions = null;
				}
			}
			return true;
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0000D6AD File Offset: 0x0000B8AD
		public void Close()
		{
			this.Dispose();
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0000D6B8 File Offset: 0x0000B8B8
		private IColumn GetColumn(int ordinal)
		{
			IDictionary<string, string> dictionary;
			if (this.exceptions != null && this.exceptions.TryGetValue(ordinal, out dictionary))
			{
				throw this.propertyHandler(dictionary);
			}
			return this.page.GetColumn(ordinal);
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0000D6F6 File Offset: 0x0000B8F6
		public DataTable GetSchemaTable()
		{
			return this.reader.SchemaTable.Copy();
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0000D708 File Offset: 0x0000B908
		private static T[] GetSchemaInfo<[global::System.Runtime.CompilerServices.Nullable(2)] T>(DataTable schemaTable, string columnName)
		{
			DataColumn dataColumn = schemaTable.Columns[columnName];
			T[] array = new T[schemaTable.Rows.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (T)((object)schemaTable.Rows[i][dataColumn]);
			}
			return array;
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0000D760 File Offset: 0x0000B960
		public void Dispose()
		{
			this.reader.Dispose();
		}

		// Token: 0x040003E6 RID: 998
		private readonly SinglePageReader reader;

		// Token: 0x040003E7 RID: 999
		private readonly Func<IDictionary<string, string>, Exception> propertyHandler;

		// Token: 0x040003E8 RID: 1000
		private readonly string[] names;

		// Token: 0x040003E9 RID: 1001
		private readonly Type[] types;

		// Token: 0x040003EA RID: 1002
		private int rowIndex;

		// Token: 0x040003EB RID: 1003
		private IPage page;

		// Token: 0x040003EC RID: 1004
		private Dictionary<string, int> ordinals;

		// Token: 0x040003ED RID: 1005
		private IDictionary<int, IDictionary<string, string>> exceptions;
	}
}
