using System;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Globalization;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x02000404 RID: 1028
	public abstract class DbUpdatableDataRecord : DbDataRecord, IExtendedDataRecord, IDataRecord
	{
		// Token: 0x06002FCA RID: 12234 RVA: 0x000970E7 File Offset: 0x000952E7
		internal DbUpdatableDataRecord(ObjectStateEntry cacheEntry, StateManagerTypeMetadata metadata, object userObject)
		{
			this._cacheEntry = cacheEntry;
			this._userObject = userObject;
			this._metadata = metadata;
		}

		// Token: 0x06002FCB RID: 12235 RVA: 0x00097104 File Offset: 0x00095304
		internal DbUpdatableDataRecord(ObjectStateEntry cacheEntry)
			: this(cacheEntry, null, null)
		{
		}

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x06002FCC RID: 12236 RVA: 0x0009710F File Offset: 0x0009530F
		public override int FieldCount
		{
			get
			{
				return this._cacheEntry.GetFieldCount(this._metadata);
			}
		}

		// Token: 0x17000966 RID: 2406
		public override object this[int i]
		{
			get
			{
				return this.GetValue(i);
			}
		}

		// Token: 0x17000967 RID: 2407
		public override object this[string name]
		{
			get
			{
				return this.GetValue(this.GetOrdinal(name));
			}
		}

		// Token: 0x06002FCF RID: 12239 RVA: 0x0009713A File Offset: 0x0009533A
		public override bool GetBoolean(int i)
		{
			return (bool)this.GetValue(i);
		}

		// Token: 0x06002FD0 RID: 12240 RVA: 0x00097148 File Offset: 0x00095348
		public override byte GetByte(int i)
		{
			return (byte)this.GetValue(i);
		}

		// Token: 0x06002FD1 RID: 12241 RVA: 0x00097158 File Offset: 0x00095358
		public override long GetBytes(int i, long dataIndex, byte[] buffer, int bufferIndex, int length)
		{
			byte[] array = (byte[])this.GetValue(i);
			if (buffer == null)
			{
				return (long)array.Length;
			}
			int num = (int)dataIndex;
			int num2 = Math.Min(array.Length - num, length);
			if (num < 0)
			{
				throw new ArgumentOutOfRangeException("dataIndex", Strings.ADP_InvalidSourceBufferIndex(array.Length.ToString(CultureInfo.InvariantCulture), ((long)num).ToString(CultureInfo.InvariantCulture)));
			}
			if (bufferIndex < 0 || (bufferIndex > 0 && bufferIndex >= buffer.Length))
			{
				throw new ArgumentOutOfRangeException("bufferIndex", Strings.ADP_InvalidDestinationBufferIndex(buffer.Length.ToString(CultureInfo.InvariantCulture), bufferIndex.ToString(CultureInfo.InvariantCulture)));
			}
			if (0 < num2)
			{
				Array.Copy(array, dataIndex, buffer, (long)bufferIndex, (long)num2);
			}
			else
			{
				if (length < 0)
				{
					throw new IndexOutOfRangeException(Strings.ADP_InvalidDataLength(((long)length).ToString(CultureInfo.InvariantCulture)));
				}
				num2 = 0;
			}
			return (long)num2;
		}

		// Token: 0x06002FD2 RID: 12242 RVA: 0x00097233 File Offset: 0x00095433
		public override char GetChar(int i)
		{
			return (char)this.GetValue(i);
		}

		// Token: 0x06002FD3 RID: 12243 RVA: 0x00097244 File Offset: 0x00095444
		public override long GetChars(int i, long dataIndex, char[] buffer, int bufferIndex, int length)
		{
			char[] array = (char[])this.GetValue(i);
			if (buffer == null)
			{
				return (long)array.Length;
			}
			int num = (int)dataIndex;
			int num2 = Math.Min(array.Length - num, length);
			if (num < 0)
			{
				throw new ArgumentOutOfRangeException("dataIndex", Strings.ADP_InvalidSourceBufferIndex(array.Length.ToString(CultureInfo.InvariantCulture), ((long)num).ToString(CultureInfo.InvariantCulture)));
			}
			if (bufferIndex < 0 || (bufferIndex > 0 && bufferIndex >= buffer.Length))
			{
				throw new ArgumentOutOfRangeException("bufferIndex", Strings.ADP_InvalidDestinationBufferIndex(buffer.Length.ToString(CultureInfo.InvariantCulture), bufferIndex.ToString(CultureInfo.InvariantCulture)));
			}
			if (0 < num2)
			{
				Array.Copy(array, dataIndex, buffer, (long)bufferIndex, (long)num2);
			}
			else
			{
				if (length < 0)
				{
					throw new IndexOutOfRangeException(Strings.ADP_InvalidDataLength(((long)length).ToString(CultureInfo.InvariantCulture)));
				}
				num2 = 0;
			}
			return (long)num2;
		}

		// Token: 0x06002FD4 RID: 12244 RVA: 0x0009731F File Offset: 0x0009551F
		IDataReader IDataRecord.GetData(int ordinal)
		{
			return this.GetDbDataReader(ordinal);
		}

		// Token: 0x06002FD5 RID: 12245 RVA: 0x00097328 File Offset: 0x00095528
		protected override DbDataReader GetDbDataReader(int i)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002FD6 RID: 12246 RVA: 0x0009732F File Offset: 0x0009552F
		public override string GetDataTypeName(int i)
		{
			return this.GetFieldType(i).Name;
		}

		// Token: 0x06002FD7 RID: 12247 RVA: 0x0009733D File Offset: 0x0009553D
		public override DateTime GetDateTime(int i)
		{
			return (DateTime)this.GetValue(i);
		}

		// Token: 0x06002FD8 RID: 12248 RVA: 0x0009734B File Offset: 0x0009554B
		public override decimal GetDecimal(int i)
		{
			return (decimal)this.GetValue(i);
		}

		// Token: 0x06002FD9 RID: 12249 RVA: 0x00097359 File Offset: 0x00095559
		public override double GetDouble(int i)
		{
			return (double)this.GetValue(i);
		}

		// Token: 0x06002FDA RID: 12250 RVA: 0x00097367 File Offset: 0x00095567
		public override Type GetFieldType(int i)
		{
			return this._cacheEntry.GetFieldType(i, this._metadata);
		}

		// Token: 0x06002FDB RID: 12251 RVA: 0x0009737B File Offset: 0x0009557B
		public override float GetFloat(int i)
		{
			return (float)this.GetValue(i);
		}

		// Token: 0x06002FDC RID: 12252 RVA: 0x00097389 File Offset: 0x00095589
		public override Guid GetGuid(int i)
		{
			return (Guid)this.GetValue(i);
		}

		// Token: 0x06002FDD RID: 12253 RVA: 0x00097397 File Offset: 0x00095597
		public override short GetInt16(int i)
		{
			return (short)this.GetValue(i);
		}

		// Token: 0x06002FDE RID: 12254 RVA: 0x000973A5 File Offset: 0x000955A5
		public override int GetInt32(int i)
		{
			return (int)this.GetValue(i);
		}

		// Token: 0x06002FDF RID: 12255 RVA: 0x000973B3 File Offset: 0x000955B3
		public override long GetInt64(int i)
		{
			return (long)this.GetValue(i);
		}

		// Token: 0x06002FE0 RID: 12256 RVA: 0x000973C1 File Offset: 0x000955C1
		public override string GetName(int i)
		{
			return this._cacheEntry.GetCLayerName(i, this._metadata);
		}

		// Token: 0x06002FE1 RID: 12257 RVA: 0x000973D5 File Offset: 0x000955D5
		public override int GetOrdinal(string name)
		{
			int ordinalforCLayerName = this._cacheEntry.GetOrdinalforCLayerName(name, this._metadata);
			if (ordinalforCLayerName == -1)
			{
				throw new ArgumentOutOfRangeException("name");
			}
			return ordinalforCLayerName;
		}

		// Token: 0x06002FE2 RID: 12258 RVA: 0x000973F8 File Offset: 0x000955F8
		public override string GetString(int i)
		{
			return (string)this.GetValue(i);
		}

		// Token: 0x06002FE3 RID: 12259 RVA: 0x00097406 File Offset: 0x00095606
		public override object GetValue(int i)
		{
			return this.GetRecordValue(i);
		}

		// Token: 0x06002FE4 RID: 12260
		protected abstract object GetRecordValue(int ordinal);

		// Token: 0x06002FE5 RID: 12261 RVA: 0x00097410 File Offset: 0x00095610
		public override int GetValues(object[] values)
		{
			Check.NotNull<object[]>(values, "values");
			int num = Math.Min(values.Length, this.FieldCount);
			for (int i = 0; i < num; i++)
			{
				values[i] = this.GetValue(i);
			}
			return num;
		}

		// Token: 0x06002FE6 RID: 12262 RVA: 0x0009744F File Offset: 0x0009564F
		public override bool IsDBNull(int i)
		{
			return this.GetValue(i) == DBNull.Value;
		}

		// Token: 0x06002FE7 RID: 12263 RVA: 0x0009745F File Offset: 0x0009565F
		public void SetBoolean(int ordinal, bool value)
		{
			this.SetValue(ordinal, value);
		}

		// Token: 0x06002FE8 RID: 12264 RVA: 0x0009746E File Offset: 0x0009566E
		public void SetByte(int ordinal, byte value)
		{
			this.SetValue(ordinal, value);
		}

		// Token: 0x06002FE9 RID: 12265 RVA: 0x0009747D File Offset: 0x0009567D
		public void SetChar(int ordinal, char value)
		{
			this.SetValue(ordinal, value);
		}

		// Token: 0x06002FEA RID: 12266 RVA: 0x0009748C File Offset: 0x0009568C
		public void SetDataRecord(int ordinal, IDataRecord value)
		{
			this.SetValue(ordinal, value);
		}

		// Token: 0x06002FEB RID: 12267 RVA: 0x00097496 File Offset: 0x00095696
		public void SetDateTime(int ordinal, DateTime value)
		{
			this.SetValue(ordinal, value);
		}

		// Token: 0x06002FEC RID: 12268 RVA: 0x000974A5 File Offset: 0x000956A5
		public void SetDecimal(int ordinal, decimal value)
		{
			this.SetValue(ordinal, value);
		}

		// Token: 0x06002FED RID: 12269 RVA: 0x000974B4 File Offset: 0x000956B4
		public void SetDouble(int ordinal, double value)
		{
			this.SetValue(ordinal, value);
		}

		// Token: 0x06002FEE RID: 12270 RVA: 0x000974C3 File Offset: 0x000956C3
		public void SetFloat(int ordinal, float value)
		{
			this.SetValue(ordinal, value);
		}

		// Token: 0x06002FEF RID: 12271 RVA: 0x000974D2 File Offset: 0x000956D2
		public void SetGuid(int ordinal, Guid value)
		{
			this.SetValue(ordinal, value);
		}

		// Token: 0x06002FF0 RID: 12272 RVA: 0x000974E1 File Offset: 0x000956E1
		public void SetInt16(int ordinal, short value)
		{
			this.SetValue(ordinal, value);
		}

		// Token: 0x06002FF1 RID: 12273 RVA: 0x000974F0 File Offset: 0x000956F0
		public void SetInt32(int ordinal, int value)
		{
			this.SetValue(ordinal, value);
		}

		// Token: 0x06002FF2 RID: 12274 RVA: 0x000974FF File Offset: 0x000956FF
		public void SetInt64(int ordinal, long value)
		{
			this.SetValue(ordinal, value);
		}

		// Token: 0x06002FF3 RID: 12275 RVA: 0x0009750E File Offset: 0x0009570E
		public void SetString(int ordinal, string value)
		{
			this.SetValue(ordinal, value);
		}

		// Token: 0x06002FF4 RID: 12276 RVA: 0x00097518 File Offset: 0x00095718
		public void SetValue(int ordinal, object value)
		{
			this.SetRecordValue(ordinal, value);
		}

		// Token: 0x06002FF5 RID: 12277 RVA: 0x00097524 File Offset: 0x00095724
		public int SetValues(params object[] values)
		{
			int num = Math.Min(values.Length, this.FieldCount);
			for (int i = 0; i < num; i++)
			{
				this.SetRecordValue(i, values[i]);
			}
			return num;
		}

		// Token: 0x06002FF6 RID: 12278 RVA: 0x00097557 File Offset: 0x00095757
		public void SetDBNull(int ordinal)
		{
			this.SetRecordValue(ordinal, DBNull.Value);
		}

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x06002FF7 RID: 12279 RVA: 0x00097565 File Offset: 0x00095765
		public virtual DataRecordInfo DataRecordInfo
		{
			get
			{
				if (this._recordInfo == null)
				{
					this._recordInfo = this._cacheEntry.GetDataRecordInfo(this._metadata, this._userObject);
				}
				return this._recordInfo;
			}
		}

		// Token: 0x06002FF8 RID: 12280 RVA: 0x00097592 File Offset: 0x00095792
		public DbDataRecord GetDataRecord(int i)
		{
			return (DbDataRecord)this.GetValue(i);
		}

		// Token: 0x06002FF9 RID: 12281 RVA: 0x000975A0 File Offset: 0x000957A0
		public DbDataReader GetDataReader(int i)
		{
			return this.GetDbDataReader(i);
		}

		// Token: 0x06002FFA RID: 12282
		protected abstract void SetRecordValue(int ordinal, object value);

		// Token: 0x04001014 RID: 4116
		internal readonly StateManagerTypeMetadata _metadata;

		// Token: 0x04001015 RID: 4117
		internal readonly ObjectStateEntry _cacheEntry;

		// Token: 0x04001016 RID: 4118
		internal readonly object _userObject;

		// Token: 0x04001017 RID: 4119
		internal DataRecordInfo _recordInfo;
	}
}
