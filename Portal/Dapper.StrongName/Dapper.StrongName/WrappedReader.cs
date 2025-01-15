using System;
using System.Data;

namespace Dapper
{
	// Token: 0x02000013 RID: 19
	internal class WrappedReader : IWrappedDataReader, IDataReader, IDisposable, IDataRecord
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600012E RID: 302 RVA: 0x000097E4 File Offset: 0x000079E4
		public IDataReader Reader
		{
			get
			{
				IDataReader tmp = this.reader;
				if (tmp == null)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				return tmp;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00009810 File Offset: 0x00007A10
		IDbCommand IWrappedDataReader.Command
		{
			get
			{
				IDbCommand tmp = this.cmd;
				if (tmp == null)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				return tmp;
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00009839 File Offset: 0x00007A39
		public WrappedReader(IDbCommand cmd, IDataReader reader)
		{
			this.cmd = cmd;
			this.reader = reader;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000984F File Offset: 0x00007A4F
		void IDataReader.Close()
		{
			IDataReader dataReader = this.reader;
			if (dataReader == null)
			{
				return;
			}
			dataReader.Close();
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00009861 File Offset: 0x00007A61
		int IDataReader.Depth
		{
			get
			{
				return this.Reader.Depth;
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000986E File Offset: 0x00007A6E
		DataTable IDataReader.GetSchemaTable()
		{
			return this.Reader.GetSchemaTable();
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000134 RID: 308 RVA: 0x0000987B File Offset: 0x00007A7B
		bool IDataReader.IsClosed
		{
			get
			{
				IDataReader dataReader = this.reader;
				return dataReader == null || dataReader.IsClosed;
			}
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000988E File Offset: 0x00007A8E
		bool IDataReader.NextResult()
		{
			return this.Reader.NextResult();
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000989B File Offset: 0x00007A9B
		bool IDataReader.Read()
		{
			return this.Reader.Read();
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000137 RID: 311 RVA: 0x000098A8 File Offset: 0x00007AA8
		int IDataReader.RecordsAffected
		{
			get
			{
				return this.Reader.RecordsAffected;
			}
		}

		// Token: 0x06000138 RID: 312 RVA: 0x000098B8 File Offset: 0x00007AB8
		void IDisposable.Dispose()
		{
			IDataReader dataReader = this.reader;
			if (dataReader != null)
			{
				dataReader.Close();
			}
			IDataReader dataReader2 = this.reader;
			if (dataReader2 != null)
			{
				dataReader2.Dispose();
			}
			this.reader = null;
			IDbCommand dbCommand = this.cmd;
			if (dbCommand != null)
			{
				dbCommand.Dispose();
			}
			this.cmd = null;
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00009906 File Offset: 0x00007B06
		int IDataRecord.FieldCount
		{
			get
			{
				return this.Reader.FieldCount;
			}
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00009913 File Offset: 0x00007B13
		bool IDataRecord.GetBoolean(int i)
		{
			return this.Reader.GetBoolean(i);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00009921 File Offset: 0x00007B21
		byte IDataRecord.GetByte(int i)
		{
			return this.Reader.GetByte(i);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000992F File Offset: 0x00007B2F
		long IDataRecord.GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
		{
			return this.Reader.GetBytes(i, fieldOffset, buffer, bufferoffset, length);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00009943 File Offset: 0x00007B43
		char IDataRecord.GetChar(int i)
		{
			return this.Reader.GetChar(i);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00009951 File Offset: 0x00007B51
		long IDataRecord.GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
		{
			return this.Reader.GetChars(i, fieldoffset, buffer, bufferoffset, length);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00009965 File Offset: 0x00007B65
		IDataReader IDataRecord.GetData(int i)
		{
			return this.Reader.GetData(i);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00009973 File Offset: 0x00007B73
		string IDataRecord.GetDataTypeName(int i)
		{
			return this.Reader.GetDataTypeName(i);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00009981 File Offset: 0x00007B81
		DateTime IDataRecord.GetDateTime(int i)
		{
			return this.Reader.GetDateTime(i);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0000998F File Offset: 0x00007B8F
		decimal IDataRecord.GetDecimal(int i)
		{
			return this.Reader.GetDecimal(i);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000999D File Offset: 0x00007B9D
		double IDataRecord.GetDouble(int i)
		{
			return this.Reader.GetDouble(i);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x000099AB File Offset: 0x00007BAB
		Type IDataRecord.GetFieldType(int i)
		{
			return this.Reader.GetFieldType(i);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x000099B9 File Offset: 0x00007BB9
		float IDataRecord.GetFloat(int i)
		{
			return this.Reader.GetFloat(i);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x000099C7 File Offset: 0x00007BC7
		Guid IDataRecord.GetGuid(int i)
		{
			return this.Reader.GetGuid(i);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x000099D5 File Offset: 0x00007BD5
		short IDataRecord.GetInt16(int i)
		{
			return this.Reader.GetInt16(i);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x000099E3 File Offset: 0x00007BE3
		int IDataRecord.GetInt32(int i)
		{
			return this.Reader.GetInt32(i);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000099F1 File Offset: 0x00007BF1
		long IDataRecord.GetInt64(int i)
		{
			return this.Reader.GetInt64(i);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x000099FF File Offset: 0x00007BFF
		string IDataRecord.GetName(int i)
		{
			return this.Reader.GetName(i);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00009A0D File Offset: 0x00007C0D
		int IDataRecord.GetOrdinal(string name)
		{
			return this.Reader.GetOrdinal(name);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00009A1B File Offset: 0x00007C1B
		string IDataRecord.GetString(int i)
		{
			return this.Reader.GetString(i);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00009A29 File Offset: 0x00007C29
		object IDataRecord.GetValue(int i)
		{
			return this.Reader.GetValue(i);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00009A37 File Offset: 0x00007C37
		int IDataRecord.GetValues(object[] values)
		{
			return this.Reader.GetValues(values);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00009A45 File Offset: 0x00007C45
		bool IDataRecord.IsDBNull(int i)
		{
			return this.Reader.IsDBNull(i);
		}

		// Token: 0x17000024 RID: 36
		object IDataRecord.this[string name]
		{
			get
			{
				return this.Reader[name];
			}
		}

		// Token: 0x17000025 RID: 37
		object IDataRecord.this[int i]
		{
			get
			{
				return this.Reader[i];
			}
		}

		// Token: 0x0400004E RID: 78
		private IDataReader reader;

		// Token: 0x0400004F RID: 79
		private IDbCommand cmd;
	}
}
