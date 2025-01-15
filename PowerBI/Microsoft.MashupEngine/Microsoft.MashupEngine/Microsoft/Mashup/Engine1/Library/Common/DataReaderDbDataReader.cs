using System;
using System.Collections;
using Microsoft.Data.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001046 RID: 4166
	internal class DataReaderDbDataReader : DbDataReaderWithTableSchema
	{
		// Token: 0x06006CA5 RID: 27813 RVA: 0x001761E6 File Offset: 0x001743E6
		public DataReaderDbDataReader(IDataReaderWithTableSchema reader)
		{
			this.reader = reader;
		}

		// Token: 0x17001EEA RID: 7914
		// (get) Token: 0x06006CA6 RID: 27814 RVA: 0x001761F5 File Offset: 0x001743F5
		public override int Depth
		{
			get
			{
				return this.reader.Depth;
			}
		}

		// Token: 0x17001EEB RID: 7915
		// (get) Token: 0x06006CA7 RID: 27815 RVA: 0x00176202 File Offset: 0x00174402
		public override int FieldCount
		{
			get
			{
				return this.reader.FieldCount;
			}
		}

		// Token: 0x17001EEC RID: 7916
		// (get) Token: 0x06006CA8 RID: 27816 RVA: 0x00176210 File Offset: 0x00174410
		public override bool HasRows
		{
			get
			{
				if (this.IsClosed)
				{
					throw new InvalidOperationException();
				}
				if (this.hasRows == null)
				{
					this.isRead = this.Read();
					this.hasRows = new bool?(this.isRead);
					this.skipReadOnce = true;
				}
				return this.hasRows.Value;
			}
		}

		// Token: 0x17001EED RID: 7917
		// (get) Token: 0x06006CA9 RID: 27817 RVA: 0x00176267 File Offset: 0x00174467
		public override bool IsClosed
		{
			get
			{
				return this.reader.IsClosed;
			}
		}

		// Token: 0x17001EEE RID: 7918
		// (get) Token: 0x06006CAA RID: 27818 RVA: 0x00176274 File Offset: 0x00174474
		public override int RecordsAffected
		{
			get
			{
				return this.reader.RecordsAffected;
			}
		}

		// Token: 0x17001EEF RID: 7919
		// (get) Token: 0x06006CAB RID: 27819 RVA: 0x00176281 File Offset: 0x00174481
		public override TableSchema Schema
		{
			get
			{
				return this.reader.Schema;
			}
		}

		// Token: 0x06006CAC RID: 27820 RVA: 0x0017628E File Offset: 0x0017448E
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.reader.Dispose();
			}
		}

		// Token: 0x06006CAD RID: 27821 RVA: 0x0017629E File Offset: 0x0017449E
		public override void Close()
		{
			this.hasRows = null;
			this.reader.Close();
		}

		// Token: 0x06006CAE RID: 27822 RVA: 0x001762B7 File Offset: 0x001744B7
		public override bool GetBoolean(int ordinal)
		{
			return this.reader.GetBoolean(ordinal);
		}

		// Token: 0x06006CAF RID: 27823 RVA: 0x001762C5 File Offset: 0x001744C5
		public override byte GetByte(int ordinal)
		{
			return this.reader.GetByte(ordinal);
		}

		// Token: 0x06006CB0 RID: 27824 RVA: 0x001762D3 File Offset: 0x001744D3
		public override long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length)
		{
			return this.reader.GetBytes(ordinal, dataOffset, buffer, bufferOffset, length);
		}

		// Token: 0x06006CB1 RID: 27825 RVA: 0x001762E7 File Offset: 0x001744E7
		public override char GetChar(int ordinal)
		{
			return this.reader.GetChar(ordinal);
		}

		// Token: 0x06006CB2 RID: 27826 RVA: 0x001762F5 File Offset: 0x001744F5
		public override long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length)
		{
			return this.reader.GetChars(ordinal, dataOffset, buffer, bufferOffset, length);
		}

		// Token: 0x06006CB3 RID: 27827 RVA: 0x00176309 File Offset: 0x00174509
		public override string GetDataTypeName(int ordinal)
		{
			return this.reader.GetDataTypeName(ordinal);
		}

		// Token: 0x06006CB4 RID: 27828 RVA: 0x00176317 File Offset: 0x00174517
		public override DateTime GetDateTime(int ordinal)
		{
			return this.reader.GetDateTime(ordinal);
		}

		// Token: 0x06006CB5 RID: 27829 RVA: 0x00176325 File Offset: 0x00174525
		public override decimal GetDecimal(int ordinal)
		{
			return this.reader.GetDecimal(ordinal);
		}

		// Token: 0x06006CB6 RID: 27830 RVA: 0x00176333 File Offset: 0x00174533
		public override double GetDouble(int ordinal)
		{
			return this.reader.GetDouble(ordinal);
		}

		// Token: 0x06006CB7 RID: 27831 RVA: 0x000091AE File Offset: 0x000073AE
		public override IEnumerator GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006CB8 RID: 27832 RVA: 0x00176341 File Offset: 0x00174541
		public override Type GetFieldType(int ordinal)
		{
			return this.reader.GetFieldType(ordinal);
		}

		// Token: 0x06006CB9 RID: 27833 RVA: 0x0017634F File Offset: 0x0017454F
		public override float GetFloat(int ordinal)
		{
			return this.reader.GetFloat(ordinal);
		}

		// Token: 0x06006CBA RID: 27834 RVA: 0x0017635D File Offset: 0x0017455D
		public override Guid GetGuid(int ordinal)
		{
			return this.reader.GetGuid(ordinal);
		}

		// Token: 0x06006CBB RID: 27835 RVA: 0x0017636B File Offset: 0x0017456B
		public override short GetInt16(int ordinal)
		{
			return this.reader.GetInt16(ordinal);
		}

		// Token: 0x06006CBC RID: 27836 RVA: 0x00176379 File Offset: 0x00174579
		public override int GetInt32(int ordinal)
		{
			return this.reader.GetInt32(ordinal);
		}

		// Token: 0x06006CBD RID: 27837 RVA: 0x00176387 File Offset: 0x00174587
		public override long GetInt64(int ordinal)
		{
			return this.reader.GetInt64(ordinal);
		}

		// Token: 0x06006CBE RID: 27838 RVA: 0x00176395 File Offset: 0x00174595
		public override string GetName(int ordinal)
		{
			return this.reader.GetName(ordinal);
		}

		// Token: 0x06006CBF RID: 27839 RVA: 0x001763A3 File Offset: 0x001745A3
		public override int GetOrdinal(string name)
		{
			return this.reader.GetOrdinal(name);
		}

		// Token: 0x06006CC0 RID: 27840 RVA: 0x001763B1 File Offset: 0x001745B1
		public override string GetString(int ordinal)
		{
			return this.reader.GetString(ordinal);
		}

		// Token: 0x06006CC1 RID: 27841 RVA: 0x000091AE File Offset: 0x000073AE
		public override Type GetProviderSpecificFieldType(int ordinal)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006CC2 RID: 27842 RVA: 0x000091AE File Offset: 0x000073AE
		public override object GetProviderSpecificValue(int ordinal)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006CC3 RID: 27843 RVA: 0x001763BF File Offset: 0x001745BF
		public override object GetValue(int ordinal)
		{
			return this.reader.GetValue(ordinal);
		}

		// Token: 0x06006CC4 RID: 27844 RVA: 0x001763CD File Offset: 0x001745CD
		public override int GetValues(object[] values)
		{
			return this.reader.GetValues(values);
		}

		// Token: 0x06006CC5 RID: 27845 RVA: 0x001763DB File Offset: 0x001745DB
		public override bool IsDBNull(int ordinal)
		{
			return this.reader.IsDBNull(ordinal);
		}

		// Token: 0x06006CC6 RID: 27846 RVA: 0x001763E9 File Offset: 0x001745E9
		public override bool NextResult()
		{
			return this.reader.NextResult();
		}

		// Token: 0x06006CC7 RID: 27847 RVA: 0x001763F8 File Offset: 0x001745F8
		public override bool Read()
		{
			if (this.skipReadOnce)
			{
				this.skipReadOnce = false;
				this.hasRows = new bool?(this.isRead);
			}
			else
			{
				this.hasRows = new bool?(this.reader.Read());
			}
			return this.hasRows.Value;
		}

		// Token: 0x17001EF0 RID: 7920
		public override object this[string name]
		{
			get
			{
				return this.reader[name];
			}
		}

		// Token: 0x17001EF1 RID: 7921
		public override object this[int ordinal]
		{
			get
			{
				return this.reader[ordinal];
			}
		}

		// Token: 0x04003C6A RID: 15466
		private readonly IDataReaderWithTableSchema reader;

		// Token: 0x04003C6B RID: 15467
		private bool? hasRows;

		// Token: 0x04003C6C RID: 15468
		private bool isRead;

		// Token: 0x04003C6D RID: 15469
		private bool skipReadOnce;
	}
}
