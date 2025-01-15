using System;
using System.Data;
using Microsoft.Data.Serialization;

namespace Microsoft.Data.Mashup
{
	// Token: 0x02000012 RID: 18
	internal sealed class DataRecordWithMetadata : IDataRecordWithMetadata, IDataRecord
	{
		// Token: 0x060000A7 RID: 167 RVA: 0x00004C0B File Offset: 0x00002E0B
		public DataRecordWithMetadata(IDataRecord data)
		{
			this.data = data;
		}

		// Token: 0x17000023 RID: 35
		public object this[string name]
		{
			get
			{
				return this.GetValue(this.data.GetOrdinal(name));
			}
		}

		// Token: 0x17000024 RID: 36
		public object this[int i]
		{
			get
			{
				return this.GetValue(i);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00004C37 File Offset: 0x00002E37
		public int FieldCount
		{
			get
			{
				return this.data.FieldCount;
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004C44 File Offset: 0x00002E44
		public bool GetBoolean(int i)
		{
			return (bool)this.GetValue(i);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004C52 File Offset: 0x00002E52
		public byte GetByte(int i)
		{
			return (byte)this.GetValue(i);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00004C60 File Offset: 0x00002E60
		public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004C67 File Offset: 0x00002E67
		public char GetChar(int i)
		{
			return (char)this.GetValue(i);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00004C75 File Offset: 0x00002E75
		public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004C7C File Offset: 0x00002E7C
		public IDataReader GetData(int i)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004C83 File Offset: 0x00002E83
		public string GetDataTypeName(int i)
		{
			return this.data.GetDataTypeName(i);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004C91 File Offset: 0x00002E91
		public DateTime GetDateTime(int i)
		{
			return (DateTime)this.GetValue(i);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004C9F File Offset: 0x00002E9F
		public decimal GetDecimal(int i)
		{
			return (decimal)this.GetValue(i);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00004CAD File Offset: 0x00002EAD
		public double GetDouble(int i)
		{
			return (double)this.GetValue(i);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00004CBB File Offset: 0x00002EBB
		public Type GetFieldType(int i)
		{
			return this.data.GetFieldType(i);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004CC9 File Offset: 0x00002EC9
		public float GetFloat(int i)
		{
			return (float)this.GetValue(i);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00004CD7 File Offset: 0x00002ED7
		public Guid GetGuid(int i)
		{
			return (Guid)this.GetValue(i);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004CE5 File Offset: 0x00002EE5
		public short GetInt16(int i)
		{
			return (short)this.GetValue(i);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004CF3 File Offset: 0x00002EF3
		public int GetInt32(int i)
		{
			return (int)this.GetValue(i);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004D01 File Offset: 0x00002F01
		public long GetInt64(int i)
		{
			return (long)this.GetValue(i);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004D0F File Offset: 0x00002F0F
		public string GetName(int i)
		{
			return this.data.GetName(i);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004D1D File Offset: 0x00002F1D
		public int GetOrdinal(string name)
		{
			return this.data.GetOrdinal(name);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004D2B File Offset: 0x00002F2B
		public string GetString(int i)
		{
			return (string)this.GetValue(i);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00004D3C File Offset: 0x00002F3C
		public object GetValue(int i)
		{
			object value = this.data.GetValue(i);
			if (!(value is ValueWithMetadata))
			{
				return value;
			}
			return ((ValueWithMetadata)value).Value;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004D6C File Offset: 0x00002F6C
		public IDataRecordWithMetadata GetMetadata(int i)
		{
			object value = this.data.GetValue(i);
			if (!(value is ValueWithMetadata))
			{
				return DataRecordWithMetadata.Empty;
			}
			return new DataRecordWithMetadata(((ValueWithMetadata)value).Metadata);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004DA8 File Offset: 0x00002FA8
		public int GetValues(object[] values)
		{
			int num = Math.Min(values.Length, this.data.FieldCount);
			for (int i = 0; i < num; i++)
			{
				values[i] = this.GetValue(i);
			}
			return num;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004DE0 File Offset: 0x00002FE0
		public bool IsDBNull(int i)
		{
			object value = this.GetValue(i);
			return value == DBNull.Value || value == null;
		}

		// Token: 0x04000053 RID: 83
		public static readonly IDataRecordWithMetadata Empty = new DataRecordWithMetadata(DataRecord.Empty);

		// Token: 0x04000054 RID: 84
		private readonly IDataRecord data;
	}
}
