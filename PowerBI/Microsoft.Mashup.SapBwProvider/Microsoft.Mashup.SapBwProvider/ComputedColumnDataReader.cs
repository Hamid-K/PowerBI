using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;

namespace Microsoft.Mashup.SapBwProvider
{
	// Token: 0x02000006 RID: 6
	internal sealed class ComputedColumnDataReader : DbDataReader
	{
		// Token: 0x0600003D RID: 61 RVA: 0x000025F8 File Offset: 0x000007F8
		public ComputedColumnDataReader(BapiDataReader reader, List<IComputedColumn> computedColumns)
		{
			this.reader = reader;
			this.computedColumns = computedColumns;
			this.computedColumnIndices = new Dictionary<string, int>();
			for (int i = 0; i < this.computedColumns.Count; i++)
			{
				this.computedColumnIndices[this.computedColumns[i].Name] = i;
			}
			this.fieldCount = this.reader.FieldCount + this.computedColumns.Count;
		}

		// Token: 0x1700001F RID: 31
		public override object this[int ordinal]
		{
			get
			{
				IComputedColumn computedColumn;
				if (!this.TryGetComputedColumn(ordinal, out computedColumn))
				{
					return this.reader[ordinal];
				}
				return computedColumn.GetValue(this.reader, this.reader.OutputTable.CurrentIndex);
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000026B8 File Offset: 0x000008B8
		private T GetValue<T>(int ordinal, Func<T> getTypedValue)
		{
			IComputedColumn computedColumn;
			if (!this.TryGetComputedColumn(ordinal, out computedColumn))
			{
				return getTypedValue();
			}
			object value = computedColumn.GetValue(this.reader, this.reader.OutputTable.CurrentIndex);
			if (value == null || value == DBNull.Value)
			{
				throw new SapBwException(Resources.NullValueMessage);
			}
			return (T)((object)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture));
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000272C File Offset: 0x0000092C
		private bool TryGetComputedColumn(int ordinal, out IComputedColumn column)
		{
			int num = ordinal - this.reader.FieldCount;
			if (this.reader.OutputTable.CurrentIndex < 0 || num < 0)
			{
				column = null;
				return false;
			}
			if (num >= this.computedColumns.Count)
			{
				throw new SapBwException(Resources.TableIndexOutOfRange(this.reader.OutputTable, this.reader.OutputTable.ElementCount + this.computedColumns.Count, ordinal));
			}
			column = this.computedColumns[num];
			return true;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000027C4 File Offset: 0x000009C4
		public override DataTable GetSchemaTable()
		{
			if (this.schemaTable == null)
			{
				this.schemaTable = this.reader.GetSchemaTable();
				int num = this.schemaTable.Rows.Count;
				foreach (IComputedColumn computedColumn in this.computedColumns)
				{
					DataRow dataRow = this.schemaTable.NewRow();
					dataRow[0] = computedColumn.Name;
					dataRow[1] = num;
					dataRow[2] = computedColumn.Length;
					dataRow[3] = computedColumn.Decimals;
					dataRow[4] = 0;
					dataRow[5] = computedColumn.Name;
					dataRow[6] = this.reader.OutputTableName;
					dataRow[7] = computedColumn.Type;
					dataRow[8] = computedColumn.RfcDataType;
					dataRow[9] = computedColumn.RfcDataType.ToString();
					dataRow[10] = true;
					this.schemaTable.Rows.Add(dataRow);
					dataRow.AcceptChanges();
					num++;
				}
				this.schemaTable.AcceptChanges();
			}
			return this.schemaTable;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002930 File Offset: 0x00000B30
		public override bool NextResult()
		{
			return this.reader.NextResult();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000293D File Offset: 0x00000B3D
		public override bool Read()
		{
			return this.reader.Read();
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000044 RID: 68 RVA: 0x0000294A File Offset: 0x00000B4A
		public override int Depth
		{
			get
			{
				return this.reader.Depth;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002957 File Offset: 0x00000B57
		public override bool IsClosed
		{
			get
			{
				return this.reader.IsClosed;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002964 File Offset: 0x00000B64
		public override int RecordsAffected
		{
			get
			{
				return this.reader.RecordsAffected;
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002974 File Offset: 0x00000B74
		public override bool GetBoolean(int ordinal)
		{
			return this.GetValue<bool>(ordinal, () => this.reader.GetBoolean(ordinal));
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000029B0 File Offset: 0x00000BB0
		public override byte GetByte(int ordinal)
		{
			return this.GetValue<byte>(ordinal, () => this.reader.GetByte(ordinal));
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000029EC File Offset: 0x00000BEC
		public override long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length)
		{
			return this.GetValue<long>(ordinal, () => this.reader.GetBytes(ordinal, dataOffset, buffer, bufferOffset, length));
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002A44 File Offset: 0x00000C44
		public override char GetChar(int ordinal)
		{
			return this.GetValue<char>(ordinal, () => this.reader.GetChar(ordinal));
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002A80 File Offset: 0x00000C80
		public override long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length)
		{
			return this.GetValue<long>(ordinal, () => this.reader.GetChars(ordinal, dataOffset, buffer, bufferOffset, length));
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002AD8 File Offset: 0x00000CD8
		public override Guid GetGuid(int ordinal)
		{
			return this.GetValue<Guid>(ordinal, () => this.reader.GetGuid(ordinal));
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002B14 File Offset: 0x00000D14
		public override short GetInt16(int ordinal)
		{
			return this.GetValue<short>(ordinal, () => this.reader.GetInt16(ordinal));
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002B50 File Offset: 0x00000D50
		public override int GetInt32(int ordinal)
		{
			return this.GetValue<int>(ordinal, () => this.reader.GetInt32(ordinal));
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002B8C File Offset: 0x00000D8C
		public override long GetInt64(int ordinal)
		{
			return this.GetValue<long>(ordinal, () => this.reader.GetInt64(ordinal));
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002BC8 File Offset: 0x00000DC8
		public override DateTime GetDateTime(int ordinal)
		{
			return this.GetValue<DateTime>(ordinal, () => this.reader.GetDateTime(ordinal));
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002C04 File Offset: 0x00000E04
		public override string GetString(int ordinal)
		{
			return this.GetValue<string>(ordinal, () => this.reader.GetString(ordinal));
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002C40 File Offset: 0x00000E40
		public override decimal GetDecimal(int ordinal)
		{
			return this.GetValue<decimal>(ordinal, () => this.reader.GetDecimal(ordinal));
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002C7C File Offset: 0x00000E7C
		public override double GetDouble(int ordinal)
		{
			return this.GetValue<double>(ordinal, () => this.reader.GetDouble(ordinal));
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002CB8 File Offset: 0x00000EB8
		public override float GetFloat(int ordinal)
		{
			return this.GetValue<float>(ordinal, () => this.reader.GetFloat(ordinal));
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002CF4 File Offset: 0x00000EF4
		public override string GetName(int ordinal)
		{
			IComputedColumn computedColumn;
			if (!this.TryGetComputedColumn(ordinal, out computedColumn))
			{
				return this.reader.GetName(ordinal);
			}
			return computedColumn.Name;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002D20 File Offset: 0x00000F20
		public override int GetValues(object[] values)
		{
			int num = ((values.Length < this.FieldCount) ? values.Length : this.FieldCount);
			for (int i = 0; i < num; i++)
			{
				values[i] = this[i];
			}
			return num;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002D5B File Offset: 0x00000F5B
		public override bool IsDBNull(int ordinal)
		{
			return this[ordinal] == DBNull.Value;
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002D6B File Offset: 0x00000F6B
		public override int FieldCount
		{
			get
			{
				return this.fieldCount;
			}
		}

		// Token: 0x17000024 RID: 36
		public override object this[string name]
		{
			get
			{
				int num;
				if (this.computedColumnIndices.TryGetValue(name, out num))
				{
					return this[num + this.reader.FieldCount];
				}
				return this.reader[name];
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002DB1 File Offset: 0x00000FB1
		public override bool HasRows
		{
			get
			{
				return this.reader.HasRows;
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002DC0 File Offset: 0x00000FC0
		public override int GetOrdinal(string name)
		{
			int num;
			if (this.computedColumnIndices.TryGetValue(name, out num))
			{
				return num + this.reader.FieldCount;
			}
			return this.reader.GetOrdinal(name);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002DF8 File Offset: 0x00000FF8
		public override string GetDataTypeName(int ordinal)
		{
			IComputedColumn computedColumn;
			if (this.TryGetComputedColumn(ordinal, out computedColumn))
			{
				return computedColumn.RfcDataType.ToString();
			}
			return this.reader.GetDataTypeName(ordinal);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002E34 File Offset: 0x00001034
		public override Type GetFieldType(int ordinal)
		{
			IComputedColumn computedColumn;
			if (!this.TryGetComputedColumn(ordinal, out computedColumn))
			{
				return this.reader.GetFieldType(ordinal);
			}
			return computedColumn.Type;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002E5F File Offset: 0x0000105F
		public override object GetValue(int ordinal)
		{
			return this[ordinal];
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002E68 File Offset: 0x00001068
		public override IEnumerator GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002E6F File Offset: 0x0000106F
		public override void Close()
		{
			this.reader.Close();
		}

		// Token: 0x04000006 RID: 6
		private readonly BapiDataReader reader;

		// Token: 0x04000007 RID: 7
		private readonly List<IComputedColumn> computedColumns;

		// Token: 0x04000008 RID: 8
		private readonly Dictionary<string, int> computedColumnIndices;

		// Token: 0x04000009 RID: 9
		private readonly int fieldCount;

		// Token: 0x0400000A RID: 10
		private DataTable schemaTable;
	}
}
