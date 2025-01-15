using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using Microsoft.Data.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x020010B0 RID: 4272
	internal abstract class DelegatingDbDataReaderWithTableSchema : DbDataReaderWithTableSchema, IDataReaderWithTableSchema, IDataReader, IDisposable, IDataRecord
	{
		// Token: 0x06006FC3 RID: 28611 RVA: 0x0018162B File Offset: 0x0017F82B
		public DelegatingDbDataReaderWithTableSchema(DbDataReaderWithTableSchema reader)
		{
			this.reader = reader;
		}

		// Token: 0x17001F79 RID: 8057
		// (get) Token: 0x06006FC4 RID: 28612 RVA: 0x0018163A File Offset: 0x0017F83A
		public DbDataReaderWithTableSchema DataReader
		{
			get
			{
				return this.reader;
			}
		}

		// Token: 0x06006FC5 RID: 28613 RVA: 0x00181642 File Offset: 0x0017F842
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.reader.Dispose();
			}
		}

		// Token: 0x06006FC6 RID: 28614 RVA: 0x00181652 File Offset: 0x0017F852
		public override void Close()
		{
			this.reader.Close();
		}

		// Token: 0x17001F7A RID: 8058
		// (get) Token: 0x06006FC7 RID: 28615 RVA: 0x0018165F File Offset: 0x0017F85F
		public override int Depth
		{
			get
			{
				return this.reader.Depth;
			}
		}

		// Token: 0x17001F7B RID: 8059
		// (get) Token: 0x06006FC8 RID: 28616 RVA: 0x0018166C File Offset: 0x0017F86C
		public override int FieldCount
		{
			get
			{
				return this.reader.FieldCount;
			}
		}

		// Token: 0x17001F7C RID: 8060
		// (get) Token: 0x06006FC9 RID: 28617 RVA: 0x00181679 File Offset: 0x0017F879
		public override TableSchema Schema
		{
			get
			{
				return this.reader.Schema;
			}
		}

		// Token: 0x06006FCA RID: 28618 RVA: 0x00181686 File Offset: 0x0017F886
		public override bool GetBoolean(int ordinal)
		{
			return this.reader.GetBoolean(ordinal);
		}

		// Token: 0x06006FCB RID: 28619 RVA: 0x00181694 File Offset: 0x0017F894
		public override byte GetByte(int ordinal)
		{
			return this.reader.GetByte(ordinal);
		}

		// Token: 0x06006FCC RID: 28620 RVA: 0x001816A2 File Offset: 0x0017F8A2
		public override long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length)
		{
			return this.reader.GetBytes(ordinal, dataOffset, buffer, bufferOffset, length);
		}

		// Token: 0x06006FCD RID: 28621 RVA: 0x001816B6 File Offset: 0x0017F8B6
		public override char GetChar(int ordinal)
		{
			return this.reader.GetChar(ordinal);
		}

		// Token: 0x06006FCE RID: 28622 RVA: 0x001816C4 File Offset: 0x0017F8C4
		public override long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length)
		{
			return this.reader.GetChars(ordinal, dataOffset, buffer, bufferOffset, length);
		}

		// Token: 0x06006FCF RID: 28623 RVA: 0x001816D8 File Offset: 0x0017F8D8
		public override string GetDataTypeName(int ordinal)
		{
			return this.reader.GetDataTypeName(ordinal);
		}

		// Token: 0x06006FD0 RID: 28624 RVA: 0x001816E6 File Offset: 0x0017F8E6
		public override DateTime GetDateTime(int ordinal)
		{
			return this.reader.GetDateTime(ordinal);
		}

		// Token: 0x06006FD1 RID: 28625 RVA: 0x001816F4 File Offset: 0x0017F8F4
		public override decimal GetDecimal(int ordinal)
		{
			return this.reader.GetDecimal(ordinal);
		}

		// Token: 0x06006FD2 RID: 28626 RVA: 0x00181702 File Offset: 0x0017F902
		public override double GetDouble(int ordinal)
		{
			return this.reader.GetDouble(ordinal);
		}

		// Token: 0x06006FD3 RID: 28627 RVA: 0x00181710 File Offset: 0x0017F910
		public override IEnumerator GetEnumerator()
		{
			return this.reader.GetEnumerator();
		}

		// Token: 0x06006FD4 RID: 28628 RVA: 0x0018171D File Offset: 0x0017F91D
		public override Type GetFieldType(int ordinal)
		{
			return this.reader.GetFieldType(ordinal);
		}

		// Token: 0x06006FD5 RID: 28629 RVA: 0x0018172B File Offset: 0x0017F92B
		public override float GetFloat(int ordinal)
		{
			return this.reader.GetFloat(ordinal);
		}

		// Token: 0x06006FD6 RID: 28630 RVA: 0x00181739 File Offset: 0x0017F939
		public override Guid GetGuid(int ordinal)
		{
			return this.reader.GetGuid(ordinal);
		}

		// Token: 0x06006FD7 RID: 28631 RVA: 0x00181747 File Offset: 0x0017F947
		public override short GetInt16(int ordinal)
		{
			return this.reader.GetInt16(ordinal);
		}

		// Token: 0x06006FD8 RID: 28632 RVA: 0x00181755 File Offset: 0x0017F955
		public override int GetInt32(int ordinal)
		{
			return this.reader.GetInt32(ordinal);
		}

		// Token: 0x06006FD9 RID: 28633 RVA: 0x00181763 File Offset: 0x0017F963
		public override long GetInt64(int ordinal)
		{
			return this.reader.GetInt64(ordinal);
		}

		// Token: 0x06006FDA RID: 28634 RVA: 0x00181771 File Offset: 0x0017F971
		public override string GetName(int ordinal)
		{
			return this.reader.GetName(ordinal);
		}

		// Token: 0x06006FDB RID: 28635 RVA: 0x0018177F File Offset: 0x0017F97F
		public override int GetOrdinal(string name)
		{
			return this.reader.GetOrdinal(name);
		}

		// Token: 0x06006FDC RID: 28636 RVA: 0x0018178D File Offset: 0x0017F98D
		public override string GetString(int ordinal)
		{
			return this.reader.GetString(ordinal);
		}

		// Token: 0x06006FDD RID: 28637 RVA: 0x0018179B File Offset: 0x0017F99B
		public override Type GetProviderSpecificFieldType(int ordinal)
		{
			return this.reader.GetProviderSpecificFieldType(ordinal);
		}

		// Token: 0x06006FDE RID: 28638 RVA: 0x001817A9 File Offset: 0x0017F9A9
		public override object GetProviderSpecificValue(int ordinal)
		{
			return this.reader.GetProviderSpecificValue(ordinal);
		}

		// Token: 0x06006FDF RID: 28639 RVA: 0x001817B7 File Offset: 0x0017F9B7
		public override object GetValue(int ordinal)
		{
			return this.reader.GetValue(ordinal);
		}

		// Token: 0x06006FE0 RID: 28640 RVA: 0x001817C5 File Offset: 0x0017F9C5
		public override int GetValues(object[] values)
		{
			return this.reader.GetValues(values);
		}

		// Token: 0x17001F7D RID: 8061
		// (get) Token: 0x06006FE1 RID: 28641 RVA: 0x001817D3 File Offset: 0x0017F9D3
		public override bool HasRows
		{
			get
			{
				return this.reader.HasRows;
			}
		}

		// Token: 0x17001F7E RID: 8062
		// (get) Token: 0x06006FE2 RID: 28642 RVA: 0x001817E0 File Offset: 0x0017F9E0
		public override bool IsClosed
		{
			get
			{
				return this.reader.IsClosed;
			}
		}

		// Token: 0x06006FE3 RID: 28643 RVA: 0x001817ED File Offset: 0x0017F9ED
		public override bool IsDBNull(int ordinal)
		{
			return this.reader.IsDBNull(ordinal);
		}

		// Token: 0x06006FE4 RID: 28644 RVA: 0x001817FB File Offset: 0x0017F9FB
		public override bool NextResult()
		{
			return this.reader.NextResult();
		}

		// Token: 0x06006FE5 RID: 28645 RVA: 0x00181808 File Offset: 0x0017FA08
		public override bool Read()
		{
			return this.reader.Read();
		}

		// Token: 0x17001F7F RID: 8063
		// (get) Token: 0x06006FE6 RID: 28646 RVA: 0x00181815 File Offset: 0x0017FA15
		public override int RecordsAffected
		{
			get
			{
				return this.reader.RecordsAffected;
			}
		}

		// Token: 0x17001F80 RID: 8064
		public override object this[string name]
		{
			get
			{
				return this.reader[name];
			}
		}

		// Token: 0x17001F81 RID: 8065
		public override object this[int ordinal]
		{
			get
			{
				return this.reader[ordinal];
			}
		}

		// Token: 0x06006FE9 RID: 28649 RVA: 0x00181840 File Offset: 0x0017FA40
		public static DbDataReader Unwrap(DbDataReaderWithTableSchema reader)
		{
			for (;;)
			{
				DelegatingDbDataReaderWithTableSchema delegatingDbDataReaderWithTableSchema = reader as DelegatingDbDataReaderWithTableSchema;
				if (delegatingDbDataReaderWithTableSchema == null)
				{
					break;
				}
				reader = delegatingDbDataReaderWithTableSchema.DataReader;
			}
			return reader.Unwrap();
		}

		// Token: 0x06006FEA RID: 28650 RVA: 0x00009332 File Offset: 0x00007532
		[Obsolete]
		DataTable IDataReader.GetSchemaTable()
		{
			return this.Schema.ToDataTable();
		}

		// Token: 0x04003DF3 RID: 15859
		private readonly DbDataReaderWithTableSchema reader;
	}
}
