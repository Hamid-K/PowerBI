using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using Microsoft.Data.Common;
using Microsoft.Data.ProviderBase;

namespace Microsoft.Data.SqlClient.Server
{
	// Token: 0x02000130 RID: 304
	public class SqlDataRecord : IDataRecord
	{
		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x06001787 RID: 6023 RVA: 0x000622BC File Offset: 0x000604BC
		public virtual int FieldCount
		{
			get
			{
				return this._columnMetaData.Length;
			}
		}

		// Token: 0x06001788 RID: 6024 RVA: 0x000622C6 File Offset: 0x000604C6
		public virtual string GetName(int ordinal)
		{
			return this.GetSqlMetaData(ordinal).Name;
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x000622D4 File Offset: 0x000604D4
		public virtual string GetDataTypeName(int ordinal)
		{
			SqlMetaData sqlMetaData = this.GetSqlMetaData(ordinal);
			if (sqlMetaData.SqlDbType == SqlDbType.Udt)
			{
				return sqlMetaData.UdtTypeName;
			}
			return MetaType.GetMetaTypeFromSqlDbType(sqlMetaData.SqlDbType, false).TypeName;
		}

		// Token: 0x0600178A RID: 6026 RVA: 0x0006230B File Offset: 0x0006050B
		public virtual Type GetFieldType(int ordinal)
		{
			return this.GetFieldTypeFrameworkSpecific(ordinal);
		}

		// Token: 0x0600178B RID: 6027 RVA: 0x00062314 File Offset: 0x00060514
		public virtual object GetValue(int ordinal)
		{
			return this.GetValueFrameworkSpecific(ordinal);
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x00062320 File Offset: 0x00060520
		public virtual int GetValues(object[] values)
		{
			if (values == null)
			{
				throw ADP.ArgumentNull("values");
			}
			int num = ((values.Length < this.FieldCount) ? values.Length : this.FieldCount);
			for (int i = 0; i < num; i++)
			{
				values[i] = this.GetValue(i);
			}
			return num;
		}

		// Token: 0x0600178D RID: 6029 RVA: 0x0006236C File Offset: 0x0006056C
		public virtual int GetOrdinal(string name)
		{
			if (this._fieldNameLookup == null)
			{
				string[] array = new string[this.FieldCount];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this.GetSqlMetaData(i).Name;
				}
				this._fieldNameLookup = new FieldNameLookup(array, -1);
			}
			return this._fieldNameLookup.GetOrdinal(name);
		}

		// Token: 0x17000950 RID: 2384
		public virtual object this[int ordinal]
		{
			get
			{
				return this.GetValue(ordinal);
			}
		}

		// Token: 0x17000951 RID: 2385
		public virtual object this[string name]
		{
			get
			{
				return this.GetValue(this.GetOrdinal(name));
			}
		}

		// Token: 0x06001790 RID: 6032 RVA: 0x000623DB File Offset: 0x000605DB
		public virtual bool GetBoolean(int ordinal)
		{
			return ValueUtilsSmi.GetBoolean(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		// Token: 0x06001791 RID: 6033 RVA: 0x000623F6 File Offset: 0x000605F6
		public virtual byte GetByte(int ordinal)
		{
			return ValueUtilsSmi.GetByte(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		// Token: 0x06001792 RID: 6034 RVA: 0x00062414 File Offset: 0x00060614
		public virtual long GetBytes(int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			return ValueUtilsSmi.GetBytes(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), fieldOffset, buffer, bufferOffset, length, true);
		}

		// Token: 0x06001793 RID: 6035 RVA: 0x00025577 File Offset: 0x00023777
		public virtual char GetChar(int ordinal)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06001794 RID: 6036 RVA: 0x00062441 File Offset: 0x00060641
		public virtual long GetChars(int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			return ValueUtilsSmi.GetChars(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), fieldOffset, buffer, bufferOffset, length);
		}

		// Token: 0x06001795 RID: 6037 RVA: 0x00062462 File Offset: 0x00060662
		public virtual Guid GetGuid(int ordinal)
		{
			return ValueUtilsSmi.GetGuid(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x0006247D File Offset: 0x0006067D
		public virtual short GetInt16(int ordinal)
		{
			return ValueUtilsSmi.GetInt16(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		// Token: 0x06001797 RID: 6039 RVA: 0x00062498 File Offset: 0x00060698
		public virtual int GetInt32(int ordinal)
		{
			return ValueUtilsSmi.GetInt32(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		// Token: 0x06001798 RID: 6040 RVA: 0x000624B3 File Offset: 0x000606B3
		public virtual long GetInt64(int ordinal)
		{
			return ValueUtilsSmi.GetInt64(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		// Token: 0x06001799 RID: 6041 RVA: 0x000624CE File Offset: 0x000606CE
		public virtual float GetFloat(int ordinal)
		{
			return ValueUtilsSmi.GetSingle(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		// Token: 0x0600179A RID: 6042 RVA: 0x000624E9 File Offset: 0x000606E9
		public virtual double GetDouble(int ordinal)
		{
			return ValueUtilsSmi.GetDouble(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		// Token: 0x0600179B RID: 6043 RVA: 0x00062504 File Offset: 0x00060704
		public virtual string GetString(int ordinal)
		{
			SmiMetaData smiMetaData = this.GetSmiMetaData(ordinal);
			if (this._usesStringStorageForXml && smiMetaData.SqlDbType == SqlDbType.Xml)
			{
				return ValueUtilsSmi.GetString(this._eventSink, this._recordBuffer, ordinal, SqlDataRecord.s_maxNVarCharForXml);
			}
			return ValueUtilsSmi.GetString(this._eventSink, this._recordBuffer, ordinal, smiMetaData);
		}

		// Token: 0x0600179C RID: 6044 RVA: 0x00062556 File Offset: 0x00060756
		public virtual decimal GetDecimal(int ordinal)
		{
			return ValueUtilsSmi.GetDecimal(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		// Token: 0x0600179D RID: 6045 RVA: 0x00062571 File Offset: 0x00060771
		public virtual DateTime GetDateTime(int ordinal)
		{
			return ValueUtilsSmi.GetDateTime(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x0006258C File Offset: 0x0006078C
		public virtual DateTimeOffset GetDateTimeOffset(int ordinal)
		{
			return ValueUtilsSmi.GetDateTimeOffset(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x000625A7 File Offset: 0x000607A7
		public virtual TimeSpan GetTimeSpan(int ordinal)
		{
			return ValueUtilsSmi.GetTimeSpan(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x000625C2 File Offset: 0x000607C2
		public virtual bool IsDBNull(int ordinal)
		{
			this.ThrowIfInvalidOrdinal(ordinal);
			return ValueUtilsSmi.IsDBNull(this._eventSink, this._recordBuffer, ordinal);
		}

		// Token: 0x060017A1 RID: 6049 RVA: 0x000625DD File Offset: 0x000607DD
		public virtual SqlMetaData GetSqlMetaData(int ordinal)
		{
			this.ThrowIfInvalidOrdinal(ordinal);
			return this._columnMetaData[ordinal];
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x000625EE File Offset: 0x000607EE
		public virtual Type GetSqlFieldType(int ordinal)
		{
			return MetaType.GetMetaTypeFromSqlDbType(this.GetSqlMetaData(ordinal).SqlDbType, false).SqlType;
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x00062607 File Offset: 0x00060807
		public virtual object GetSqlValue(int ordinal)
		{
			return this.GetSqlValueFrameworkSpecific(ordinal);
		}

		// Token: 0x060017A4 RID: 6052 RVA: 0x00062610 File Offset: 0x00060810
		public virtual int GetSqlValues(object[] values)
		{
			if (values == null)
			{
				throw ADP.ArgumentNull("values");
			}
			int num = ((values.Length < this.FieldCount) ? values.Length : this.FieldCount);
			for (int i = 0; i < num; i++)
			{
				values[i] = this.GetSqlValue(i);
			}
			return num;
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x00062659 File Offset: 0x00060859
		public virtual SqlBinary GetSqlBinary(int ordinal)
		{
			return ValueUtilsSmi.GetSqlBinary(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		// Token: 0x060017A6 RID: 6054 RVA: 0x00062674 File Offset: 0x00060874
		public virtual SqlBytes GetSqlBytes(int ordinal)
		{
			return this.GetSqlBytesFrameworkSpecific(ordinal);
		}

		// Token: 0x060017A7 RID: 6055 RVA: 0x0006267D File Offset: 0x0006087D
		public virtual SqlXml GetSqlXml(int ordinal)
		{
			return this.GetSqlXmlFrameworkSpecific(ordinal);
		}

		// Token: 0x060017A8 RID: 6056 RVA: 0x00062686 File Offset: 0x00060886
		public virtual SqlBoolean GetSqlBoolean(int ordinal)
		{
			return ValueUtilsSmi.GetSqlBoolean(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		// Token: 0x060017A9 RID: 6057 RVA: 0x000626A1 File Offset: 0x000608A1
		public virtual SqlByte GetSqlByte(int ordinal)
		{
			return ValueUtilsSmi.GetSqlByte(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		// Token: 0x060017AA RID: 6058 RVA: 0x000626BC File Offset: 0x000608BC
		public virtual SqlChars GetSqlChars(int ordinal)
		{
			return this.GetSqlCharsFrameworkSpecific(ordinal);
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x000626C5 File Offset: 0x000608C5
		public virtual SqlInt16 GetSqlInt16(int ordinal)
		{
			return ValueUtilsSmi.GetSqlInt16(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		// Token: 0x060017AC RID: 6060 RVA: 0x000626E0 File Offset: 0x000608E0
		public virtual SqlInt32 GetSqlInt32(int ordinal)
		{
			return ValueUtilsSmi.GetSqlInt32(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		// Token: 0x060017AD RID: 6061 RVA: 0x000626FB File Offset: 0x000608FB
		public virtual SqlInt64 GetSqlInt64(int ordinal)
		{
			return ValueUtilsSmi.GetSqlInt64(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		// Token: 0x060017AE RID: 6062 RVA: 0x00062716 File Offset: 0x00060916
		public virtual SqlSingle GetSqlSingle(int ordinal)
		{
			return ValueUtilsSmi.GetSqlSingle(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		// Token: 0x060017AF RID: 6063 RVA: 0x00062731 File Offset: 0x00060931
		public virtual SqlDouble GetSqlDouble(int ordinal)
		{
			return ValueUtilsSmi.GetSqlDouble(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		// Token: 0x060017B0 RID: 6064 RVA: 0x0006274C File Offset: 0x0006094C
		public virtual SqlMoney GetSqlMoney(int ordinal)
		{
			return ValueUtilsSmi.GetSqlMoney(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x00062767 File Offset: 0x00060967
		public virtual SqlDateTime GetSqlDateTime(int ordinal)
		{
			return ValueUtilsSmi.GetSqlDateTime(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		// Token: 0x060017B2 RID: 6066 RVA: 0x00062782 File Offset: 0x00060982
		public virtual SqlDecimal GetSqlDecimal(int ordinal)
		{
			return ValueUtilsSmi.GetSqlDecimal(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		// Token: 0x060017B3 RID: 6067 RVA: 0x0006279D File Offset: 0x0006099D
		public virtual SqlString GetSqlString(int ordinal)
		{
			return ValueUtilsSmi.GetSqlString(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		// Token: 0x060017B4 RID: 6068 RVA: 0x000627B8 File Offset: 0x000609B8
		public virtual SqlGuid GetSqlGuid(int ordinal)
		{
			return ValueUtilsSmi.GetSqlGuid(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal));
		}

		// Token: 0x060017B5 RID: 6069 RVA: 0x000627D3 File Offset: 0x000609D3
		public virtual int SetValues(params object[] values)
		{
			return this.SetValuesFrameworkSpecific(values);
		}

		// Token: 0x060017B6 RID: 6070 RVA: 0x000627DC File Offset: 0x000609DC
		public virtual void SetValue(int ordinal, object value)
		{
			this.SetValueFrameworkSpecific(ordinal, value);
		}

		// Token: 0x060017B7 RID: 6071 RVA: 0x000627E6 File Offset: 0x000609E6
		public virtual void SetBoolean(int ordinal, bool value)
		{
			ValueUtilsSmi.SetBoolean(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		// Token: 0x060017B8 RID: 6072 RVA: 0x00062802 File Offset: 0x00060A02
		public virtual void SetByte(int ordinal, byte value)
		{
			ValueUtilsSmi.SetByte(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		// Token: 0x060017B9 RID: 6073 RVA: 0x0006281E File Offset: 0x00060A1E
		public virtual void SetBytes(int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			ValueUtilsSmi.SetBytes(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), fieldOffset, buffer, bufferOffset, length);
		}

		// Token: 0x060017BA RID: 6074 RVA: 0x00025577 File Offset: 0x00023777
		public virtual void SetChar(int ordinal, char value)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x060017BB RID: 6075 RVA: 0x00062840 File Offset: 0x00060A40
		public virtual void SetChars(int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			ValueUtilsSmi.SetChars(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), fieldOffset, buffer, bufferOffset, length);
		}

		// Token: 0x060017BC RID: 6076 RVA: 0x00062862 File Offset: 0x00060A62
		public virtual void SetInt16(int ordinal, short value)
		{
			ValueUtilsSmi.SetInt16(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		// Token: 0x060017BD RID: 6077 RVA: 0x0006287E File Offset: 0x00060A7E
		public virtual void SetInt32(int ordinal, int value)
		{
			ValueUtilsSmi.SetInt32(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		// Token: 0x060017BE RID: 6078 RVA: 0x0006289A File Offset: 0x00060A9A
		public virtual void SetInt64(int ordinal, long value)
		{
			ValueUtilsSmi.SetInt64(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		// Token: 0x060017BF RID: 6079 RVA: 0x000628B6 File Offset: 0x00060AB6
		public virtual void SetFloat(int ordinal, float value)
		{
			ValueUtilsSmi.SetSingle(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x000628D2 File Offset: 0x00060AD2
		public virtual void SetDouble(int ordinal, double value)
		{
			ValueUtilsSmi.SetDouble(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x000628EE File Offset: 0x00060AEE
		public virtual void SetString(int ordinal, string value)
		{
			ValueUtilsSmi.SetString(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		// Token: 0x060017C2 RID: 6082 RVA: 0x0006290A File Offset: 0x00060B0A
		public virtual void SetDecimal(int ordinal, decimal value)
		{
			ValueUtilsSmi.SetDecimal(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		// Token: 0x060017C3 RID: 6083 RVA: 0x00062926 File Offset: 0x00060B26
		public virtual void SetDateTime(int ordinal, DateTime value)
		{
			ValueUtilsSmi.SetDateTime(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		// Token: 0x060017C4 RID: 6084 RVA: 0x00062942 File Offset: 0x00060B42
		public virtual void SetTimeSpan(int ordinal, TimeSpan value)
		{
			this.SetTimeSpanFrameworkSpecific(ordinal, value);
		}

		// Token: 0x060017C5 RID: 6085 RVA: 0x0006294C File Offset: 0x00060B4C
		public virtual void SetDateTimeOffset(int ordinal, DateTimeOffset value)
		{
			this.SetDateTimeOffsetFrameworkSpecific(ordinal, value);
		}

		// Token: 0x060017C6 RID: 6086 RVA: 0x00062956 File Offset: 0x00060B56
		public virtual void SetDBNull(int ordinal)
		{
			this.ThrowIfInvalidOrdinal(ordinal);
			ValueUtilsSmi.SetDBNull(this._eventSink, this._recordBuffer, ordinal);
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x00062971 File Offset: 0x00060B71
		public virtual void SetGuid(int ordinal, Guid value)
		{
			ValueUtilsSmi.SetGuid(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		// Token: 0x060017C8 RID: 6088 RVA: 0x0006298D File Offset: 0x00060B8D
		public virtual void SetSqlBoolean(int ordinal, SqlBoolean value)
		{
			ValueUtilsSmi.SetSqlBoolean(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		// Token: 0x060017C9 RID: 6089 RVA: 0x000629A9 File Offset: 0x00060BA9
		public virtual void SetSqlByte(int ordinal, SqlByte value)
		{
			ValueUtilsSmi.SetSqlByte(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		// Token: 0x060017CA RID: 6090 RVA: 0x000629C5 File Offset: 0x00060BC5
		public virtual void SetSqlInt16(int ordinal, SqlInt16 value)
		{
			ValueUtilsSmi.SetSqlInt16(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		// Token: 0x060017CB RID: 6091 RVA: 0x000629E1 File Offset: 0x00060BE1
		public virtual void SetSqlInt32(int ordinal, SqlInt32 value)
		{
			ValueUtilsSmi.SetSqlInt32(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		// Token: 0x060017CC RID: 6092 RVA: 0x000629FD File Offset: 0x00060BFD
		public virtual void SetSqlInt64(int ordinal, SqlInt64 value)
		{
			ValueUtilsSmi.SetSqlInt64(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		// Token: 0x060017CD RID: 6093 RVA: 0x00062A19 File Offset: 0x00060C19
		public virtual void SetSqlSingle(int ordinal, SqlSingle value)
		{
			ValueUtilsSmi.SetSqlSingle(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		// Token: 0x060017CE RID: 6094 RVA: 0x00062A35 File Offset: 0x00060C35
		public virtual void SetSqlDouble(int ordinal, SqlDouble value)
		{
			ValueUtilsSmi.SetSqlDouble(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		// Token: 0x060017CF RID: 6095 RVA: 0x00062A51 File Offset: 0x00060C51
		public virtual void SetSqlMoney(int ordinal, SqlMoney value)
		{
			ValueUtilsSmi.SetSqlMoney(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		// Token: 0x060017D0 RID: 6096 RVA: 0x00062A6D File Offset: 0x00060C6D
		public virtual void SetSqlDateTime(int ordinal, SqlDateTime value)
		{
			ValueUtilsSmi.SetSqlDateTime(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x00062A89 File Offset: 0x00060C89
		public virtual void SetSqlXml(int ordinal, SqlXml value)
		{
			ValueUtilsSmi.SetSqlXml(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x00062AA5 File Offset: 0x00060CA5
		public virtual void SetSqlDecimal(int ordinal, SqlDecimal value)
		{
			ValueUtilsSmi.SetSqlDecimal(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x00062AC1 File Offset: 0x00060CC1
		public virtual void SetSqlString(int ordinal, SqlString value)
		{
			ValueUtilsSmi.SetSqlString(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		// Token: 0x060017D4 RID: 6100 RVA: 0x00062ADD File Offset: 0x00060CDD
		public virtual void SetSqlBinary(int ordinal, SqlBinary value)
		{
			ValueUtilsSmi.SetSqlBinary(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		// Token: 0x060017D5 RID: 6101 RVA: 0x00062AF9 File Offset: 0x00060CF9
		public virtual void SetSqlGuid(int ordinal, SqlGuid value)
		{
			ValueUtilsSmi.SetSqlGuid(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		// Token: 0x060017D6 RID: 6102 RVA: 0x00062B15 File Offset: 0x00060D15
		public virtual void SetSqlChars(int ordinal, SqlChars value)
		{
			ValueUtilsSmi.SetSqlChars(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		// Token: 0x060017D7 RID: 6103 RVA: 0x00062B31 File Offset: 0x00060D31
		public virtual void SetSqlBytes(int ordinal, SqlBytes value)
		{
			ValueUtilsSmi.SetSqlBytes(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value);
		}

		// Token: 0x060017D8 RID: 6104 RVA: 0x00062B50 File Offset: 0x00060D50
		public SqlDataRecord(params SqlMetaData[] metaData)
		{
			if (metaData == null)
			{
				throw ADP.ArgumentNull("metaData");
			}
			this._columnMetaData = new SqlMetaData[metaData.Length];
			this._columnSmiMetaData = new SmiExtendedMetaData[metaData.Length];
			ulong smiVersion = this.SmiVersion;
			for (int i = 0; i < this._columnSmiMetaData.Length; i++)
			{
				if (metaData[i] == null)
				{
					throw ADP.ArgumentNull(string.Format("{0}[{1}]", "metaData", i));
				}
				this._columnMetaData[i] = metaData[i];
				this._columnSmiMetaData[i] = MetaDataUtilsSmi.SqlMetaDataToSmiExtendedMetaData(this._columnMetaData[i]);
				if (!MetaDataUtilsSmi.IsValidForSmiVersion(this._columnSmiMetaData[i], smiVersion))
				{
					throw ADP.VersionDoesNotSupportDataType(this._columnSmiMetaData[i].TypeName);
				}
			}
			this._eventSink = new SmiEventSink_Default();
			if (InOutOfProcHelper.InProc)
			{
				this._recordContext = SmiContextFactory.Instance.GetCurrentContext();
				this._recordBuffer = this._recordContext.CreateRecordBuffer(this._columnSmiMetaData, this._eventSink);
				this._usesStringStorageForXml = false;
				return;
			}
			this._recordContext = null;
			SmiMetaData[] columnSmiMetaData = this._columnSmiMetaData;
			this._recordBuffer = new MemoryRecordBuffer(columnSmiMetaData);
			this._usesStringStorageForXml = true;
		}

		// Token: 0x060017D9 RID: 6105 RVA: 0x00062C72 File Offset: 0x00060E72
		internal SmiExtendedMetaData GetSmiMetaData(int ordinal)
		{
			this.ThrowIfInvalidOrdinal(ordinal);
			return this._columnSmiMetaData[ordinal];
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x00062C83 File Offset: 0x00060E83
		internal void ThrowIfInvalidOrdinal(int ordinal)
		{
			if (0 > ordinal || this.FieldCount <= ordinal)
			{
				throw ADP.IndexOutOfRange(ordinal);
			}
		}

		// Token: 0x060017DB RID: 6107 RVA: 0x00025577 File Offset: 0x00023777
		[EditorBrowsable(EditorBrowsableState.Never)]
		IDataReader IDataRecord.GetData(int ordinal)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x060017DC RID: 6108 RVA: 0x00062C9C File Offset: 0x00060E9C
		private Type GetFieldTypeFrameworkSpecific(int ordinal)
		{
			SqlMetaData sqlMetaData = this.GetSqlMetaData(ordinal);
			if (sqlMetaData.SqlDbType == SqlDbType.Udt)
			{
				return sqlMetaData.Type;
			}
			return MetaType.GetMetaTypeFromSqlDbType(sqlMetaData.SqlDbType, false).ClassType;
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x00062CD4 File Offset: 0x00060ED4
		private object GetValueFrameworkSpecific(int ordinal)
		{
			SmiMetaData smiMetaData = this.GetSmiMetaData(ordinal);
			if (this.SmiVersion >= 210UL)
			{
				return ValueUtilsSmi.GetValue200(this._eventSink, this._recordBuffer, ordinal, smiMetaData, this._recordContext);
			}
			return ValueUtilsSmi.GetValue(this._eventSink, this._recordBuffer, ordinal, smiMetaData, this._recordContext);
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x00062D2C File Offset: 0x00060F2C
		private object GetSqlValueFrameworkSpecific(int ordinal)
		{
			SmiMetaData smiMetaData = this.GetSmiMetaData(ordinal);
			if (this.SmiVersion >= 210UL)
			{
				return ValueUtilsSmi.GetSqlValue200(this._eventSink, this._recordBuffer, ordinal, smiMetaData, this._recordContext);
			}
			return ValueUtilsSmi.GetSqlValue(this._eventSink, this._recordBuffer, ordinal, smiMetaData, this._recordContext);
		}

		// Token: 0x060017DF RID: 6111 RVA: 0x00062D82 File Offset: 0x00060F82
		private SqlBytes GetSqlBytesFrameworkSpecific(int ordinal)
		{
			return ValueUtilsSmi.GetSqlBytes(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), this._recordContext);
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x00062DA3 File Offset: 0x00060FA3
		private SqlXml GetSqlXmlFrameworkSpecific(int ordinal)
		{
			return ValueUtilsSmi.GetSqlXml(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), this._recordContext);
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x00062DC4 File Offset: 0x00060FC4
		private SqlChars GetSqlCharsFrameworkSpecific(int ordinal)
		{
			return ValueUtilsSmi.GetSqlChars(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), this._recordContext);
		}

		// Token: 0x060017E2 RID: 6114 RVA: 0x00062DE8 File Offset: 0x00060FE8
		private int SetValuesFrameworkSpecific(params object[] values)
		{
			if (values == null)
			{
				throw ADP.ArgumentNull("values");
			}
			int num = ((values.Length > this.FieldCount) ? this.FieldCount : values.Length);
			ExtendedClrTypeCode[] array = new ExtendedClrTypeCode[num];
			for (int i = 0; i < num; i++)
			{
				SqlMetaData sqlMetaData = this.GetSqlMetaData(i);
				array[i] = MetaDataUtilsSmi.DetermineExtendedTypeCodeForUseWithSqlDbType(sqlMetaData.SqlDbType, false, values[i], sqlMetaData.Type, this.SmiVersion);
				if (array[i] == ExtendedClrTypeCode.Invalid)
				{
					throw ADP.InvalidCast();
				}
			}
			for (int j = 0; j < num; j++)
			{
				if (this.SmiVersion >= 210UL)
				{
					ValueUtilsSmi.SetCompatibleValueV200(this._eventSink, this._recordBuffer, j, this.GetSmiMetaData(j), values[j], array[j], 0, null);
				}
				else
				{
					ValueUtilsSmi.SetCompatibleValue(this._eventSink, this._recordBuffer, j, this.GetSmiMetaData(j), values[j], array[j], 0);
				}
			}
			return num;
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x00062ECC File Offset: 0x000610CC
		private void SetValueFrameworkSpecific(int ordinal, object value)
		{
			SqlMetaData sqlMetaData = this.GetSqlMetaData(ordinal);
			ExtendedClrTypeCode extendedClrTypeCode = MetaDataUtilsSmi.DetermineExtendedTypeCodeForUseWithSqlDbType(sqlMetaData.SqlDbType, false, value, sqlMetaData.Type, this.SmiVersion);
			if (extendedClrTypeCode == ExtendedClrTypeCode.Invalid)
			{
				throw ADP.InvalidCast();
			}
			if (this.SmiVersion >= 210UL)
			{
				ValueUtilsSmi.SetCompatibleValueV200(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value, extendedClrTypeCode, 0, null);
				return;
			}
			ValueUtilsSmi.SetCompatibleValue(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value, extendedClrTypeCode, 0);
		}

		// Token: 0x060017E4 RID: 6116 RVA: 0x00062F4D File Offset: 0x0006114D
		private void SetTimeSpanFrameworkSpecific(int ordinal, TimeSpan value)
		{
			ValueUtilsSmi.SetTimeSpan(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value, this.SmiVersion >= 210UL);
		}

		// Token: 0x060017E5 RID: 6117 RVA: 0x00062F7A File Offset: 0x0006117A
		private void SetDateTimeOffsetFrameworkSpecific(int ordinal, DateTimeOffset value)
		{
			ValueUtilsSmi.SetDateTimeOffset(this._eventSink, this._recordBuffer, ordinal, this.GetSmiMetaData(ordinal), value, this.SmiVersion >= 210UL);
		}

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x060017E6 RID: 6118 RVA: 0x00062FA7 File Offset: 0x000611A7
		private ulong SmiVersion
		{
			get
			{
				if (!InOutOfProcHelper.InProc)
				{
					return 210UL;
				}
				return SmiContextFactory.Instance.NegotiatedSmiVersion;
			}
		}

		// Token: 0x04000981 RID: 2433
		private readonly SmiRecordBuffer _recordBuffer;

		// Token: 0x04000982 RID: 2434
		private readonly SmiExtendedMetaData[] _columnSmiMetaData;

		// Token: 0x04000983 RID: 2435
		private readonly SmiEventSink_Default _eventSink;

		// Token: 0x04000984 RID: 2436
		private readonly SqlMetaData[] _columnMetaData;

		// Token: 0x04000985 RID: 2437
		private FieldNameLookup _fieldNameLookup;

		// Token: 0x04000986 RID: 2438
		private readonly bool _usesStringStorageForXml;

		// Token: 0x04000987 RID: 2439
		private static readonly SmiMetaData s_maxNVarCharForXml = new SmiMetaData(SqlDbType.NVarChar, -1L, SmiMetaData.DefaultNVarChar_NoCollation.Precision, SmiMetaData.DefaultNVarChar_NoCollation.Scale, SmiMetaData.DefaultNVarChar.LocaleId, SmiMetaData.DefaultNVarChar.CompareOptions, null);

		// Token: 0x04000988 RID: 2440
		private readonly SmiContext _recordContext;
	}
}
