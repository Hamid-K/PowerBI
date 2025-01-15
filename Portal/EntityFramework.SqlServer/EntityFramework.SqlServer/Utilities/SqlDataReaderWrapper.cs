using System;
using System.Collections;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace System.Data.Entity.SqlServer.Utilities
{
	// Token: 0x02000021 RID: 33
	internal class SqlDataReaderWrapper : MarshalByRefObject
	{
		// Token: 0x06000377 RID: 887 RVA: 0x0000EBC3 File Offset: 0x0000CDC3
		protected SqlDataReaderWrapper()
		{
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000EBCB File Offset: 0x0000CDCB
		public SqlDataReaderWrapper(SqlDataReader sqlDataReader)
		{
			this._sqlDataReader = sqlDataReader;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000EBDA File Offset: 0x0000CDDA
		public virtual IDataReader GetData(int i)
		{
			return ((IDataRecord)this._sqlDataReader).GetData(i);
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000EBE8 File Offset: 0x0000CDE8
		public virtual void Dispose()
		{
			this._sqlDataReader.Dispose();
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0000EBF5 File Offset: 0x0000CDF5
		public virtual Task<T> GetFieldValueAsync<T>(int ordinal)
		{
			return this._sqlDataReader.GetFieldValueAsync<T>(ordinal);
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000EC03 File Offset: 0x0000CE03
		public virtual Task<bool> IsDBNullAsync(int ordinal)
		{
			return this._sqlDataReader.IsDBNullAsync(ordinal);
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000EC11 File Offset: 0x0000CE11
		public virtual Task<bool> ReadAsync()
		{
			return this._sqlDataReader.ReadAsync();
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000EC1E File Offset: 0x0000CE1E
		public virtual Task<bool> NextResultAsync()
		{
			return this._sqlDataReader.NextResultAsync();
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000EC2B File Offset: 0x0000CE2B
		public virtual void Close()
		{
			this._sqlDataReader.Close();
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000EC38 File Offset: 0x0000CE38
		public virtual string GetDataTypeName(int i)
		{
			return this._sqlDataReader.GetDataTypeName(i);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000EC46 File Offset: 0x0000CE46
		public virtual IEnumerator GetEnumerator()
		{
			return this._sqlDataReader.GetEnumerator();
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000EC53 File Offset: 0x0000CE53
		public virtual Type GetFieldType(int i)
		{
			return this._sqlDataReader.GetFieldType(i);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000EC61 File Offset: 0x0000CE61
		public virtual string GetName(int i)
		{
			return this._sqlDataReader.GetName(i);
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000EC6F File Offset: 0x0000CE6F
		public virtual Type GetProviderSpecificFieldType(int i)
		{
			return this._sqlDataReader.GetProviderSpecificFieldType(i);
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000EC7D File Offset: 0x0000CE7D
		public virtual int GetOrdinal(string name)
		{
			return this._sqlDataReader.GetOrdinal(name);
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000EC8B File Offset: 0x0000CE8B
		public virtual object GetProviderSpecificValue(int i)
		{
			return this._sqlDataReader.GetProviderSpecificValue(i);
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000EC99 File Offset: 0x0000CE99
		public virtual int GetProviderSpecificValues(object[] values)
		{
			return this._sqlDataReader.GetProviderSpecificValues(values);
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000ECA7 File Offset: 0x0000CEA7
		public virtual DataTable GetSchemaTable()
		{
			return this._sqlDataReader.GetSchemaTable();
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000ECB4 File Offset: 0x0000CEB4
		public virtual bool GetBoolean(int i)
		{
			return this._sqlDataReader.GetBoolean(i);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000ECC2 File Offset: 0x0000CEC2
		public virtual XmlReader GetXmlReader(int i)
		{
			return this._sqlDataReader.GetXmlReader(i);
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000ECD0 File Offset: 0x0000CED0
		public virtual Stream GetStream(int i)
		{
			return this._sqlDataReader.GetStream(i);
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000ECDE File Offset: 0x0000CEDE
		public virtual byte GetByte(int i)
		{
			return this._sqlDataReader.GetByte(i);
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000ECEC File Offset: 0x0000CEEC
		public virtual long GetBytes(int i, long dataIndex, byte[] buffer, int bufferIndex, int length)
		{
			return this._sqlDataReader.GetBytes(i, dataIndex, buffer, bufferIndex, length);
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000ED00 File Offset: 0x0000CF00
		public virtual TextReader GetTextReader(int i)
		{
			return this._sqlDataReader.GetTextReader(i);
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000ED0E File Offset: 0x0000CF0E
		public virtual char GetChar(int i)
		{
			return this._sqlDataReader.GetChar(i);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000ED1C File Offset: 0x0000CF1C
		public virtual long GetChars(int i, long dataIndex, char[] buffer, int bufferIndex, int length)
		{
			return this._sqlDataReader.GetChars(i, dataIndex, buffer, bufferIndex, length);
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000ED30 File Offset: 0x0000CF30
		public virtual DateTime GetDateTime(int i)
		{
			return this._sqlDataReader.GetDateTime(i);
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000ED3E File Offset: 0x0000CF3E
		public virtual decimal GetDecimal(int i)
		{
			return this._sqlDataReader.GetDecimal(i);
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000ED4C File Offset: 0x0000CF4C
		public virtual double GetDouble(int i)
		{
			return this._sqlDataReader.GetDouble(i);
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000ED5A File Offset: 0x0000CF5A
		public virtual float GetFloat(int i)
		{
			return this._sqlDataReader.GetFloat(i);
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000ED68 File Offset: 0x0000CF68
		public virtual Guid GetGuid(int i)
		{
			return this._sqlDataReader.GetGuid(i);
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000ED76 File Offset: 0x0000CF76
		public virtual short GetInt16(int i)
		{
			return this._sqlDataReader.GetInt16(i);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000ED84 File Offset: 0x0000CF84
		public virtual int GetInt32(int i)
		{
			return this._sqlDataReader.GetInt32(i);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000ED92 File Offset: 0x0000CF92
		public virtual long GetInt64(int i)
		{
			return this._sqlDataReader.GetInt64(i);
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000EDA0 File Offset: 0x0000CFA0
		public virtual SqlBoolean GetSqlBoolean(int i)
		{
			return this._sqlDataReader.GetSqlBoolean(i);
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000EDAE File Offset: 0x0000CFAE
		public virtual SqlBinary GetSqlBinary(int i)
		{
			return this._sqlDataReader.GetSqlBinary(i);
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000EDBC File Offset: 0x0000CFBC
		public virtual SqlByte GetSqlByte(int i)
		{
			return this._sqlDataReader.GetSqlByte(i);
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000EDCA File Offset: 0x0000CFCA
		public virtual SqlBytes GetSqlBytes(int i)
		{
			return this._sqlDataReader.GetSqlBytes(i);
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000EDD8 File Offset: 0x0000CFD8
		public virtual SqlChars GetSqlChars(int i)
		{
			return this._sqlDataReader.GetSqlChars(i);
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000EDE6 File Offset: 0x0000CFE6
		public virtual SqlDateTime GetSqlDateTime(int i)
		{
			return this._sqlDataReader.GetSqlDateTime(i);
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000EDF4 File Offset: 0x0000CFF4
		public virtual SqlDecimal GetSqlDecimal(int i)
		{
			return this._sqlDataReader.GetSqlDecimal(i);
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000EE02 File Offset: 0x0000D002
		public virtual SqlGuid GetSqlGuid(int i)
		{
			return this._sqlDataReader.GetSqlGuid(i);
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000EE10 File Offset: 0x0000D010
		public virtual SqlDouble GetSqlDouble(int i)
		{
			return this._sqlDataReader.GetSqlDouble(i);
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000EE1E File Offset: 0x0000D01E
		public virtual SqlInt16 GetSqlInt16(int i)
		{
			return this._sqlDataReader.GetSqlInt16(i);
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000EE2C File Offset: 0x0000D02C
		public virtual SqlInt32 GetSqlInt32(int i)
		{
			return this._sqlDataReader.GetSqlInt32(i);
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000EE3A File Offset: 0x0000D03A
		public virtual SqlInt64 GetSqlInt64(int i)
		{
			return this._sqlDataReader.GetSqlInt64(i);
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000EE48 File Offset: 0x0000D048
		public virtual SqlMoney GetSqlMoney(int i)
		{
			return this._sqlDataReader.GetSqlMoney(i);
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000EE56 File Offset: 0x0000D056
		public virtual SqlSingle GetSqlSingle(int i)
		{
			return this._sqlDataReader.GetSqlSingle(i);
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000EE64 File Offset: 0x0000D064
		public virtual SqlString GetSqlString(int i)
		{
			return this._sqlDataReader.GetSqlString(i);
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000EE72 File Offset: 0x0000D072
		public virtual SqlXml GetSqlXml(int i)
		{
			return this._sqlDataReader.GetSqlXml(i);
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000EE80 File Offset: 0x0000D080
		public virtual object GetSqlValue(int i)
		{
			return this._sqlDataReader.GetSqlValue(i);
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0000EE8E File Offset: 0x0000D08E
		public virtual int GetSqlValues(object[] values)
		{
			return this._sqlDataReader.GetSqlValues(values);
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000EE9C File Offset: 0x0000D09C
		public virtual string GetString(int i)
		{
			return this._sqlDataReader.GetString(i);
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000EEAA File Offset: 0x0000D0AA
		public virtual T GetFieldValue<T>(int i)
		{
			return this._sqlDataReader.GetFieldValue<T>(i);
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000EEB8 File Offset: 0x0000D0B8
		public virtual object GetValue(int i)
		{
			return this._sqlDataReader.GetValue(i);
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0000EEC6 File Offset: 0x0000D0C6
		public virtual TimeSpan GetTimeSpan(int i)
		{
			return this._sqlDataReader.GetTimeSpan(i);
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000EED4 File Offset: 0x0000D0D4
		public virtual DateTimeOffset GetDateTimeOffset(int i)
		{
			return this._sqlDataReader.GetDateTimeOffset(i);
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0000EEE2 File Offset: 0x0000D0E2
		public virtual int GetValues(object[] values)
		{
			return this._sqlDataReader.GetValues(values);
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000EEF0 File Offset: 0x0000D0F0
		public virtual bool IsDBNull(int i)
		{
			return this._sqlDataReader.IsDBNull(i);
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000EEFE File Offset: 0x0000D0FE
		public virtual bool NextResult()
		{
			return this._sqlDataReader.NextResult();
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000EF0B File Offset: 0x0000D10B
		public virtual bool Read()
		{
			return this._sqlDataReader.Read();
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000EF18 File Offset: 0x0000D118
		public virtual Task<bool> NextResultAsync(CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			return this._sqlDataReader.NextResultAsync(cancellationToken);
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0000EF2D File Offset: 0x0000D12D
		public virtual Task<bool> ReadAsync(CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			return this._sqlDataReader.ReadAsync(cancellationToken);
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000EF42 File Offset: 0x0000D142
		public virtual Task<bool> IsDBNullAsync(int i, CancellationToken cancellationToken)
		{
			return this._sqlDataReader.IsDBNullAsync(i, cancellationToken);
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000EF51 File Offset: 0x0000D151
		public virtual Task<T> GetFieldValueAsync<T>(int i, CancellationToken cancellationToken)
		{
			return this._sqlDataReader.GetFieldValueAsync<T>(i, cancellationToken);
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x0000EF60 File Offset: 0x0000D160
		public virtual int Depth
		{
			get
			{
				return this._sqlDataReader.Depth;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x0000EF6D File Offset: 0x0000D16D
		public virtual int FieldCount
		{
			get
			{
				return this._sqlDataReader.FieldCount;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060003BA RID: 954 RVA: 0x0000EF7A File Offset: 0x0000D17A
		public virtual bool HasRows
		{
			get
			{
				return this._sqlDataReader.HasRows;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060003BB RID: 955 RVA: 0x0000EF87 File Offset: 0x0000D187
		public virtual bool IsClosed
		{
			get
			{
				return this._sqlDataReader.IsClosed;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060003BC RID: 956 RVA: 0x0000EF94 File Offset: 0x0000D194
		public virtual int RecordsAffected
		{
			get
			{
				return this._sqlDataReader.RecordsAffected;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060003BD RID: 957 RVA: 0x0000EFA1 File Offset: 0x0000D1A1
		public virtual int VisibleFieldCount
		{
			get
			{
				return this._sqlDataReader.VisibleFieldCount;
			}
		}

		// Token: 0x1700009F RID: 159
		public virtual object this[int i]
		{
			get
			{
				return this._sqlDataReader[i];
			}
		}

		// Token: 0x170000A0 RID: 160
		public virtual object this[string name]
		{
			get
			{
				return this._sqlDataReader[name];
			}
		}

		// Token: 0x040000CC RID: 204
		private readonly SqlDataReader _sqlDataReader;
	}
}
