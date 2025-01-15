using System;
using System.Data;

namespace Microsoft.Data.Serialization
{
	// Token: 0x0200014B RID: 331
	public sealed class DataRecord : IDataRecord
	{
		// Token: 0x060005B7 RID: 1463 RVA: 0x0000912F File Offset: 0x0000732F
		public DataRecord(TableSchema schema, object[] values)
		{
			this.schema = schema;
			this.values = values;
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00009145 File Offset: 0x00007345
		public DataRecord(string[] names, object[] values)
		{
			this.schema = TableSchema.FromData(names, values);
			this.values = values;
		}

		// Token: 0x170001FD RID: 509
		public object this[string name]
		{
			get
			{
				return this.values[this.GetOrdinal(name)];
			}
		}

		// Token: 0x170001FE RID: 510
		public object this[int i]
		{
			get
			{
				return this.values[i];
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x0000917B File Offset: 0x0000737B
		public int FieldCount
		{
			get
			{
				return this.schema.ColumnCount;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x00009188 File Offset: 0x00007388
		public TableSchema Schema
		{
			get
			{
				return this.schema;
			}
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x00009190 File Offset: 0x00007390
		public bool GetBoolean(int i)
		{
			return (bool)this.values[i];
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x0000919F File Offset: 0x0000739F
		public byte GetByte(int i)
		{
			return (byte)this.values[i];
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x000091AE File Offset: 0x000073AE
		public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x000091B5 File Offset: 0x000073B5
		public char GetChar(int i)
		{
			return (char)this.values[i];
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x000091AE File Offset: 0x000073AE
		public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x000091AE File Offset: 0x000073AE
		public IDataReader GetData(int i)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x000091C4 File Offset: 0x000073C4
		public string GetDataTypeName(int i)
		{
			return this.schema.GetColumn(i).DataTypeName;
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x000091D7 File Offset: 0x000073D7
		public DateTime GetDateTime(int i)
		{
			return (DateTime)this.values[i];
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x000091E6 File Offset: 0x000073E6
		public decimal GetDecimal(int i)
		{
			return (decimal)this.values[i];
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x000091F5 File Offset: 0x000073F5
		public double GetDouble(int i)
		{
			return (double)this.values[i];
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x00009204 File Offset: 0x00007404
		public Type GetFieldType(int i)
		{
			Type type;
			if ((type = this.schema.GetColumn(i).DataType) == null)
			{
				object obj = this.values[i];
				type = ((obj != null) ? obj.GetType() : null) ?? typeof(DBNull);
			}
			return type;
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x0000923D File Offset: 0x0000743D
		public float GetFloat(int i)
		{
			return (float)this.values[i];
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0000924C File Offset: 0x0000744C
		public Guid GetGuid(int i)
		{
			return (Guid)this.values[i];
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0000925B File Offset: 0x0000745B
		public short GetInt16(int i)
		{
			return (short)this.values[i];
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x0000926A File Offset: 0x0000746A
		public int GetInt32(int i)
		{
			return (int)this.values[i];
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x00009279 File Offset: 0x00007479
		public long GetInt64(int i)
		{
			return (long)this.values[i];
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x00009288 File Offset: 0x00007488
		public string GetName(int i)
		{
			return this.schema.GetColumn(i).Name;
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x0000929C File Offset: 0x0000749C
		public int GetOrdinal(string name)
		{
			int num;
			if (!this.schema.TryGetColumn(name, out num))
			{
				throw new IndexOutOfRangeException();
			}
			return num;
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x000092C0 File Offset: 0x000074C0
		public string GetString(int i)
		{
			return (string)this.values[i];
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00009171 File Offset: 0x00007371
		public object GetValue(int i)
		{
			return this.values[i];
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x000092D0 File Offset: 0x000074D0
		public int GetValues(object[] values)
		{
			int num = Math.Min(values.Length, this.values.Length);
			Array.Copy(this.values, values, num);
			return num;
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x000092FC File Offset: 0x000074FC
		public bool IsDBNull(int i)
		{
			return this.values[i] == DBNull.Value || this.values[i] == null;
		}

		// Token: 0x040003C9 RID: 969
		public static readonly IDataRecord Empty = new DataRecord(new string[0], new object[0]);

		// Token: 0x040003CA RID: 970
		private readonly TableSchema schema;

		// Token: 0x040003CB RID: 971
		private readonly object[] values;
	}
}
