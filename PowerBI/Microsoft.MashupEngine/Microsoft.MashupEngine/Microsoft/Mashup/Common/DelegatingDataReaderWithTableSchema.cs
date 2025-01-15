using System;
using System.Data;
using Microsoft.Data.Serialization;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001BE4 RID: 7140
	public abstract class DelegatingDataReaderWithTableSchema : IDataReaderWithTableSchema, IDataReader, IDisposable, IDataRecord
	{
		// Token: 0x0600B247 RID: 45639 RVA: 0x002454D6 File Offset: 0x002436D6
		protected DelegatingDataReaderWithTableSchema(IDataReaderWithTableSchema reader)
		{
			this.reader = reader;
		}

		// Token: 0x17002CC5 RID: 11461
		// (get) Token: 0x0600B248 RID: 45640 RVA: 0x002454E5 File Offset: 0x002436E5
		public IDataReader Reader
		{
			get
			{
				return this.reader;
			}
		}

		// Token: 0x17002CC6 RID: 11462
		// (get) Token: 0x0600B249 RID: 45641 RVA: 0x002454ED File Offset: 0x002436ED
		public virtual TableSchema Schema
		{
			get
			{
				return this.reader.Schema;
			}
		}

		// Token: 0x0600B24A RID: 45642 RVA: 0x002454FA File Offset: 0x002436FA
		public virtual void Close()
		{
			this.reader.Close();
		}

		// Token: 0x17002CC7 RID: 11463
		// (get) Token: 0x0600B24B RID: 45643 RVA: 0x00245507 File Offset: 0x00243707
		public virtual int Depth
		{
			get
			{
				return this.reader.Depth;
			}
		}

		// Token: 0x17002CC8 RID: 11464
		// (get) Token: 0x0600B24C RID: 45644 RVA: 0x00245514 File Offset: 0x00243714
		public virtual bool IsClosed
		{
			get
			{
				return this.reader.IsClosed;
			}
		}

		// Token: 0x0600B24D RID: 45645 RVA: 0x00245521 File Offset: 0x00243721
		public virtual bool NextResult()
		{
			return this.reader.NextResult();
		}

		// Token: 0x0600B24E RID: 45646 RVA: 0x0024552E File Offset: 0x0024372E
		public virtual bool Read()
		{
			return this.reader.Read();
		}

		// Token: 0x17002CC9 RID: 11465
		// (get) Token: 0x0600B24F RID: 45647 RVA: 0x0024553B File Offset: 0x0024373B
		public virtual int RecordsAffected
		{
			get
			{
				return this.reader.RecordsAffected;
			}
		}

		// Token: 0x0600B250 RID: 45648 RVA: 0x00245548 File Offset: 0x00243748
		public virtual void Dispose()
		{
			this.reader.Dispose();
		}

		// Token: 0x17002CCA RID: 11466
		// (get) Token: 0x0600B251 RID: 45649 RVA: 0x00245555 File Offset: 0x00243755
		public virtual int FieldCount
		{
			get
			{
				return this.reader.FieldCount;
			}
		}

		// Token: 0x0600B252 RID: 45650 RVA: 0x00245562 File Offset: 0x00243762
		public virtual bool GetBoolean(int i)
		{
			return this.reader.GetBoolean(i);
		}

		// Token: 0x0600B253 RID: 45651 RVA: 0x00245570 File Offset: 0x00243770
		public virtual byte GetByte(int i)
		{
			return this.reader.GetByte(i);
		}

		// Token: 0x0600B254 RID: 45652 RVA: 0x0024557E File Offset: 0x0024377E
		public virtual long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
		{
			return this.reader.GetBytes(i, fieldOffset, buffer, bufferoffset, length);
		}

		// Token: 0x0600B255 RID: 45653 RVA: 0x00245592 File Offset: 0x00243792
		public virtual char GetChar(int i)
		{
			return this.reader.GetChar(i);
		}

		// Token: 0x0600B256 RID: 45654 RVA: 0x002455A0 File Offset: 0x002437A0
		public virtual long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
		{
			return this.reader.GetChars(i, fieldoffset, buffer, bufferoffset, length);
		}

		// Token: 0x0600B257 RID: 45655 RVA: 0x002455B4 File Offset: 0x002437B4
		public virtual IDataReader GetData(int i)
		{
			return this.reader.GetData(i);
		}

		// Token: 0x0600B258 RID: 45656 RVA: 0x002455C2 File Offset: 0x002437C2
		public virtual string GetDataTypeName(int i)
		{
			return this.reader.GetDataTypeName(i);
		}

		// Token: 0x0600B259 RID: 45657 RVA: 0x002455D0 File Offset: 0x002437D0
		public virtual DateTime GetDateTime(int i)
		{
			return this.reader.GetDateTime(i);
		}

		// Token: 0x0600B25A RID: 45658 RVA: 0x002455DE File Offset: 0x002437DE
		public virtual decimal GetDecimal(int i)
		{
			return this.reader.GetDecimal(i);
		}

		// Token: 0x0600B25B RID: 45659 RVA: 0x002455EC File Offset: 0x002437EC
		public virtual double GetDouble(int i)
		{
			return this.reader.GetDouble(i);
		}

		// Token: 0x0600B25C RID: 45660 RVA: 0x002455FA File Offset: 0x002437FA
		public virtual Type GetFieldType(int i)
		{
			return this.reader.GetFieldType(i);
		}

		// Token: 0x0600B25D RID: 45661 RVA: 0x00245608 File Offset: 0x00243808
		public virtual float GetFloat(int i)
		{
			return this.reader.GetFloat(i);
		}

		// Token: 0x0600B25E RID: 45662 RVA: 0x00245616 File Offset: 0x00243816
		public virtual Guid GetGuid(int i)
		{
			return this.reader.GetGuid(i);
		}

		// Token: 0x0600B25F RID: 45663 RVA: 0x00245624 File Offset: 0x00243824
		public virtual short GetInt16(int i)
		{
			return this.reader.GetInt16(i);
		}

		// Token: 0x0600B260 RID: 45664 RVA: 0x00245632 File Offset: 0x00243832
		public virtual int GetInt32(int i)
		{
			return this.reader.GetInt32(i);
		}

		// Token: 0x0600B261 RID: 45665 RVA: 0x00245640 File Offset: 0x00243840
		public virtual long GetInt64(int i)
		{
			return this.reader.GetInt64(i);
		}

		// Token: 0x0600B262 RID: 45666 RVA: 0x0024564E File Offset: 0x0024384E
		public virtual string GetName(int i)
		{
			return this.reader.GetName(i);
		}

		// Token: 0x0600B263 RID: 45667 RVA: 0x0024565C File Offset: 0x0024385C
		public virtual int GetOrdinal(string name)
		{
			return this.reader.GetOrdinal(name);
		}

		// Token: 0x0600B264 RID: 45668 RVA: 0x0024566A File Offset: 0x0024386A
		public virtual string GetString(int i)
		{
			return this.reader.GetString(i);
		}

		// Token: 0x0600B265 RID: 45669 RVA: 0x00245678 File Offset: 0x00243878
		public virtual object GetValue(int i)
		{
			return this.reader.GetValue(i);
		}

		// Token: 0x0600B266 RID: 45670 RVA: 0x00245686 File Offset: 0x00243886
		public virtual int GetValues(object[] values)
		{
			return this.reader.GetValues(values);
		}

		// Token: 0x0600B267 RID: 45671 RVA: 0x00245694 File Offset: 0x00243894
		public virtual bool IsDBNull(int i)
		{
			return this.reader.IsDBNull(i);
		}

		// Token: 0x17002CCB RID: 11467
		public virtual object this[string name]
		{
			get
			{
				return this.reader[name];
			}
		}

		// Token: 0x17002CCC RID: 11468
		public virtual object this[int i]
		{
			get
			{
				return this.reader[i];
			}
		}

		// Token: 0x0600B26A RID: 45674 RVA: 0x002456BE File Offset: 0x002438BE
		[Obsolete]
		public DataTable GetSchemaTable()
		{
			return this.Schema.ToDataTable();
		}

		// Token: 0x04005B34 RID: 23348
		private readonly IDataReaderWithTableSchema reader;
	}
}
