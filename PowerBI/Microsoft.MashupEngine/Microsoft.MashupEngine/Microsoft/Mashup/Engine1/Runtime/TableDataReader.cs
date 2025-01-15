using System;
using System.Data;
using Microsoft.Data.Serialization;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001620 RID: 5664
	internal class TableDataReader : IDataReaderWithTableSchema, IDataReader, IDisposable, IDataRecord
	{
		// Token: 0x06008EA1 RID: 36513 RVA: 0x001DBCE6 File Offset: 0x001D9EE6
		public TableDataReader(TableTypeValue tableType, ITableReader reader, TableDataMapper mapper = null)
		{
			this.mapper = mapper ?? TableDataMapper.Instance;
			this.reader = reader;
			this.schema = this.mapper.MapTableType(tableType);
			this.isClosed = false;
		}

		// Token: 0x06008EA2 RID: 36514 RVA: 0x001DBD1E File Offset: 0x001D9F1E
		public void Dispose()
		{
			this.reader.Dispose();
			this.isClosed = true;
		}

		// Token: 0x06008EA3 RID: 36515 RVA: 0x001DBD1E File Offset: 0x001D9F1E
		public void Close()
		{
			this.reader.Dispose();
			this.isClosed = true;
		}

		// Token: 0x1700255F RID: 9567
		// (get) Token: 0x06008EA4 RID: 36516 RVA: 0x0000EE09 File Offset: 0x0000D009
		public int Depth
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17002560 RID: 9568
		// (get) Token: 0x06008EA5 RID: 36517 RVA: 0x001DBD32 File Offset: 0x001D9F32
		public int FieldCount
		{
			get
			{
				return this.reader.Columns;
			}
		}

		// Token: 0x17002561 RID: 9569
		// (get) Token: 0x06008EA6 RID: 36518 RVA: 0x001DBD3F File Offset: 0x001D9F3F
		public TableSchema Schema
		{
			get
			{
				return this.schema;
			}
		}

		// Token: 0x06008EA7 RID: 36519 RVA: 0x001DBD47 File Offset: 0x001D9F47
		[Obsolete]
		public DataTable GetSchemaTable()
		{
			return this.schema.ToDataTable();
		}

		// Token: 0x06008EA8 RID: 36520 RVA: 0x001DBD54 File Offset: 0x001D9F54
		public bool GetBoolean(int ordinal)
		{
			if (this.GetFieldType(ordinal) != typeof(bool))
			{
				throw new InvalidCastException();
			}
			return this.reader[ordinal].AsBoolean;
		}

		// Token: 0x06008EA9 RID: 36521 RVA: 0x001DBD85 File Offset: 0x001D9F85
		public byte GetByte(int ordinal)
		{
			if (this.GetFieldType(ordinal) != typeof(bool))
			{
				throw new InvalidCastException();
			}
			return (byte)this.reader[ordinal].AsNumber.ToInt32();
		}

		// Token: 0x06008EAA RID: 36522 RVA: 0x0000EE09 File Offset: 0x0000D009
		public long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06008EAB RID: 36523 RVA: 0x0000EE09 File Offset: 0x0000D009
		public char GetChar(int ordinal)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06008EAC RID: 36524 RVA: 0x0000EE09 File Offset: 0x0000D009
		public long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06008EAD RID: 36525 RVA: 0x001DBDBC File Offset: 0x001D9FBC
		public string GetDataTypeName(int ordinal)
		{
			return this.schema.GetColumn(ordinal).DataTypeName;
		}

		// Token: 0x06008EAE RID: 36526 RVA: 0x001DBDCF File Offset: 0x001D9FCF
		public DateTime GetDateTime(int ordinal)
		{
			if (this.GetFieldType(ordinal) != typeof(DateTime))
			{
				throw new InvalidCastException();
			}
			return this.reader[ordinal].AsDateTime.AsClrDateTime;
		}

		// Token: 0x06008EAF RID: 36527 RVA: 0x001DBE05 File Offset: 0x001DA005
		public decimal GetDecimal(int ordinal)
		{
			if (this.GetFieldType(ordinal) != typeof(decimal))
			{
				throw new InvalidCastException();
			}
			return this.reader[ordinal].AsNumber.AsDecimal;
		}

		// Token: 0x06008EB0 RID: 36528 RVA: 0x001DBE3B File Offset: 0x001DA03B
		public double GetDouble(int ordinal)
		{
			if (this.GetFieldType(ordinal) != typeof(double))
			{
				throw new InvalidCastException();
			}
			return this.reader[ordinal].AsNumber.AsDouble;
		}

		// Token: 0x06008EB1 RID: 36529 RVA: 0x001DBE71 File Offset: 0x001DA071
		public Type GetFieldType(int ordinal)
		{
			return this.schema.GetColumn(ordinal).DataType;
		}

		// Token: 0x06008EB2 RID: 36530 RVA: 0x001DBE84 File Offset: 0x001DA084
		public float GetFloat(int ordinal)
		{
			if (this.GetFieldType(ordinal) != typeof(float))
			{
				throw new InvalidCastException();
			}
			return (float)this.reader[ordinal].AsNumber.AsDouble;
		}

		// Token: 0x06008EB3 RID: 36531 RVA: 0x0000EE09 File Offset: 0x0000D009
		public Guid GetGuid(int ordinal)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06008EB4 RID: 36532 RVA: 0x001DBEBB File Offset: 0x001DA0BB
		public short GetInt16(int ordinal)
		{
			if (this.GetFieldType(ordinal) != typeof(short))
			{
				throw new InvalidCastException();
			}
			return (short)this.reader[ordinal].AsNumber.ToInt32();
		}

		// Token: 0x06008EB5 RID: 36533 RVA: 0x001DBEF2 File Offset: 0x001DA0F2
		public int GetInt32(int ordinal)
		{
			if (this.GetFieldType(ordinal) != typeof(int))
			{
				throw new InvalidCastException();
			}
			return this.reader[ordinal].AsNumber.ToInt32();
		}

		// Token: 0x06008EB6 RID: 36534 RVA: 0x001DBF28 File Offset: 0x001DA128
		public long GetInt64(int ordinal)
		{
			if (this.GetFieldType(ordinal) != typeof(long))
			{
				throw new InvalidCastException();
			}
			return this.reader[ordinal].AsNumber.ToInt64();
		}

		// Token: 0x06008EB7 RID: 36535 RVA: 0x001DBF5E File Offset: 0x001DA15E
		public string GetName(int ordinal)
		{
			return this.schema.GetColumn(ordinal).Name;
		}

		// Token: 0x06008EB8 RID: 36536 RVA: 0x001DBF74 File Offset: 0x001DA174
		public int GetOrdinal(string name)
		{
			int num;
			if (!this.schema.TryGetColumn(name, out num))
			{
				throw new IndexOutOfRangeException();
			}
			return num;
		}

		// Token: 0x06008EB9 RID: 36537 RVA: 0x001DBF98 File Offset: 0x001DA198
		public string GetString(int ordinal)
		{
			if (this.GetFieldType(ordinal) != typeof(string))
			{
				throw new InvalidCastException();
			}
			return this.reader[ordinal].AsString;
		}

		// Token: 0x06008EBA RID: 36538 RVA: 0x001DBFCC File Offset: 0x001DA1CC
		public object GetValue(int ordinal)
		{
			Value value = this.reader[ordinal];
			return this.mapper.ConvertValue(value, this.schema.GetColumn(ordinal));
		}

		// Token: 0x06008EBB RID: 36539 RVA: 0x001DC000 File Offset: 0x001DA200
		public int GetValues(object[] values)
		{
			int num = Math.Min(this.schema.ColumnCount, values.Length);
			for (int i = 0; i < num; i++)
			{
				values[i] = this.GetValue(i);
			}
			return num;
		}

		// Token: 0x17002562 RID: 9570
		// (get) Token: 0x06008EBC RID: 36540 RVA: 0x001DC038 File Offset: 0x001DA238
		public bool IsClosed
		{
			get
			{
				return this.isClosed;
			}
		}

		// Token: 0x06008EBD RID: 36541 RVA: 0x001DC040 File Offset: 0x001DA240
		public bool IsDBNull(int ordinal)
		{
			return this.GetValue(ordinal) == DBNull.Value;
		}

		// Token: 0x06008EBE RID: 36542 RVA: 0x00002105 File Offset: 0x00000305
		public bool NextResult()
		{
			return false;
		}

		// Token: 0x06008EBF RID: 36543 RVA: 0x001DC050 File Offset: 0x001DA250
		public bool Read()
		{
			return this.reader.MoveNext();
		}

		// Token: 0x17002563 RID: 9571
		// (get) Token: 0x06008EC0 RID: 36544 RVA: 0x0000EE09 File Offset: 0x0000D009
		public int RecordsAffected
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17002564 RID: 9572
		public object this[string name]
		{
			get
			{
				return this[this.GetOrdinal(name)];
			}
		}

		// Token: 0x17002565 RID: 9573
		public object this[int ordinal]
		{
			get
			{
				return this.GetValue(ordinal);
			}
		}

		// Token: 0x06008EC3 RID: 36547 RVA: 0x0000EE09 File Offset: 0x0000D009
		public IDataReader GetData(int i)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x04004D5B RID: 19803
		private readonly ITableReader reader;

		// Token: 0x04004D5C RID: 19804
		private readonly TableSchema schema;

		// Token: 0x04004D5D RID: 19805
		private readonly TableDataMapper mapper;

		// Token: 0x04004D5E RID: 19806
		private bool isClosed;
	}
}
