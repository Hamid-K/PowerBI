using System;
using System.Data.SqlTypes;

namespace Microsoft.Data.SqlClient.Server
{
	// Token: 0x0200011B RID: 283
	internal sealed class MemoryRecordBuffer : SmiRecordBuffer
	{
		// Token: 0x06001636 RID: 5686 RVA: 0x0005F110 File Offset: 0x0005D310
		internal MemoryRecordBuffer(SmiMetaData[] metaData)
		{
			this._buffer = new SqlRecordBuffer[metaData.Length];
			for (int i = 0; i < this._buffer.Length; i++)
			{
				this._buffer[i] = new SqlRecordBuffer(metaData[i]);
			}
		}

		// Token: 0x06001637 RID: 5687 RVA: 0x0005F154 File Offset: 0x0005D354
		public override bool IsDBNull(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].IsNull;
		}

		// Token: 0x06001638 RID: 5688 RVA: 0x0005F163 File Offset: 0x0005D363
		public override SmiMetaData GetVariantType(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].VariantType;
		}

		// Token: 0x06001639 RID: 5689 RVA: 0x0005F172 File Offset: 0x0005D372
		public override bool GetBoolean(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].Boolean;
		}

		// Token: 0x0600163A RID: 5690 RVA: 0x0005F181 File Offset: 0x0005D381
		public override byte GetByte(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].Byte;
		}

		// Token: 0x0600163B RID: 5691 RVA: 0x0005F190 File Offset: 0x0005D390
		public override long GetBytesLength(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].BytesLength;
		}

		// Token: 0x0600163C RID: 5692 RVA: 0x0005F19F File Offset: 0x0005D39F
		public override int GetBytes(SmiEventSink sink, int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			return this._buffer[ordinal].GetBytes(fieldOffset, buffer, bufferOffset, length);
		}

		// Token: 0x0600163D RID: 5693 RVA: 0x0005F1B5 File Offset: 0x0005D3B5
		public override long GetCharsLength(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].CharsLength;
		}

		// Token: 0x0600163E RID: 5694 RVA: 0x0005F1C4 File Offset: 0x0005D3C4
		public override int GetChars(SmiEventSink sink, int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			return this._buffer[ordinal].GetChars(fieldOffset, buffer, bufferOffset, length);
		}

		// Token: 0x0600163F RID: 5695 RVA: 0x0005F1DA File Offset: 0x0005D3DA
		public override string GetString(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].String;
		}

		// Token: 0x06001640 RID: 5696 RVA: 0x0005F1E9 File Offset: 0x0005D3E9
		public override short GetInt16(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].Int16;
		}

		// Token: 0x06001641 RID: 5697 RVA: 0x0005F1F8 File Offset: 0x0005D3F8
		public override int GetInt32(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].Int32;
		}

		// Token: 0x06001642 RID: 5698 RVA: 0x0005F207 File Offset: 0x0005D407
		public override long GetInt64(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].Int64;
		}

		// Token: 0x06001643 RID: 5699 RVA: 0x0005F216 File Offset: 0x0005D416
		public override float GetSingle(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].Single;
		}

		// Token: 0x06001644 RID: 5700 RVA: 0x0005F225 File Offset: 0x0005D425
		public override double GetDouble(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].Double;
		}

		// Token: 0x06001645 RID: 5701 RVA: 0x0005F234 File Offset: 0x0005D434
		public override SqlDecimal GetSqlDecimal(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].SqlDecimal;
		}

		// Token: 0x06001646 RID: 5702 RVA: 0x0005F243 File Offset: 0x0005D443
		public override DateTime GetDateTime(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].DateTime;
		}

		// Token: 0x06001647 RID: 5703 RVA: 0x0005F252 File Offset: 0x0005D452
		public override Guid GetGuid(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].Guid;
		}

		// Token: 0x06001648 RID: 5704 RVA: 0x0005F261 File Offset: 0x0005D461
		public override TimeSpan GetTimeSpan(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].TimeSpan;
		}

		// Token: 0x06001649 RID: 5705 RVA: 0x0005F270 File Offset: 0x0005D470
		public override DateTimeOffset GetDateTimeOffset(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].DateTimeOffset;
		}

		// Token: 0x0600164A RID: 5706 RVA: 0x0005F27F File Offset: 0x0005D47F
		public override void SetDBNull(SmiEventSink sink, int ordinal)
		{
			this._buffer[ordinal].SetNull();
		}

		// Token: 0x0600164B RID: 5707 RVA: 0x0005F28E File Offset: 0x0005D48E
		public override void SetBoolean(SmiEventSink sink, int ordinal, bool value)
		{
			this._buffer[ordinal].Boolean = value;
		}

		// Token: 0x0600164C RID: 5708 RVA: 0x0005F29E File Offset: 0x0005D49E
		public override void SetByte(SmiEventSink sink, int ordinal, byte value)
		{
			this._buffer[ordinal].Byte = value;
		}

		// Token: 0x0600164D RID: 5709 RVA: 0x0005F2AE File Offset: 0x0005D4AE
		public override int SetBytes(SmiEventSink sink, int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			return this._buffer[ordinal].SetBytes(fieldOffset, buffer, bufferOffset, length);
		}

		// Token: 0x0600164E RID: 5710 RVA: 0x0005F2C4 File Offset: 0x0005D4C4
		public override void SetBytesLength(SmiEventSink sink, int ordinal, long length)
		{
			this._buffer[ordinal].BytesLength = length;
		}

		// Token: 0x0600164F RID: 5711 RVA: 0x0005F2D4 File Offset: 0x0005D4D4
		public override int SetChars(SmiEventSink sink, int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			return this._buffer[ordinal].SetChars(fieldOffset, buffer, bufferOffset, length);
		}

		// Token: 0x06001650 RID: 5712 RVA: 0x0005F2EA File Offset: 0x0005D4EA
		public override void SetCharsLength(SmiEventSink sink, int ordinal, long length)
		{
			this._buffer[ordinal].CharsLength = length;
		}

		// Token: 0x06001651 RID: 5713 RVA: 0x0005F2FA File Offset: 0x0005D4FA
		public override void SetString(SmiEventSink sink, int ordinal, string value, int offset, int length)
		{
			this._buffer[ordinal].String = value.Substring(offset, length);
		}

		// Token: 0x06001652 RID: 5714 RVA: 0x0005F313 File Offset: 0x0005D513
		public override void SetInt16(SmiEventSink sink, int ordinal, short value)
		{
			this._buffer[ordinal].Int16 = value;
		}

		// Token: 0x06001653 RID: 5715 RVA: 0x0005F323 File Offset: 0x0005D523
		public override void SetInt32(SmiEventSink sink, int ordinal, int value)
		{
			this._buffer[ordinal].Int32 = value;
		}

		// Token: 0x06001654 RID: 5716 RVA: 0x0005F333 File Offset: 0x0005D533
		public override void SetInt64(SmiEventSink sink, int ordinal, long value)
		{
			this._buffer[ordinal].Int64 = value;
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x0005F343 File Offset: 0x0005D543
		public override void SetSingle(SmiEventSink sink, int ordinal, float value)
		{
			this._buffer[ordinal].Single = value;
		}

		// Token: 0x06001656 RID: 5718 RVA: 0x0005F353 File Offset: 0x0005D553
		public override void SetDouble(SmiEventSink sink, int ordinal, double value)
		{
			this._buffer[ordinal].Double = value;
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x0005F363 File Offset: 0x0005D563
		public override void SetSqlDecimal(SmiEventSink sink, int ordinal, SqlDecimal value)
		{
			this._buffer[ordinal].SqlDecimal = value;
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x0005F373 File Offset: 0x0005D573
		public override void SetDateTime(SmiEventSink sink, int ordinal, DateTime value)
		{
			this._buffer[ordinal].DateTime = value;
		}

		// Token: 0x06001659 RID: 5721 RVA: 0x0005F383 File Offset: 0x0005D583
		public override void SetGuid(SmiEventSink sink, int ordinal, Guid value)
		{
			this._buffer[ordinal].Guid = value;
		}

		// Token: 0x0600165A RID: 5722 RVA: 0x0005F393 File Offset: 0x0005D593
		public override void SetTimeSpan(SmiEventSink sink, int ordinal, TimeSpan value)
		{
			this._buffer[ordinal].TimeSpan = value;
		}

		// Token: 0x0600165B RID: 5723 RVA: 0x0005F3A3 File Offset: 0x0005D5A3
		public override void SetDateTimeOffset(SmiEventSink sink, int ordinal, DateTimeOffset value)
		{
			this._buffer[ordinal].DateTimeOffset = value;
		}

		// Token: 0x0600165C RID: 5724 RVA: 0x0005F3B3 File Offset: 0x0005D5B3
		public override void SetVariantMetaData(SmiEventSink sink, int ordinal, SmiMetaData metaData)
		{
			this._buffer[ordinal].VariantType = metaData;
		}

		// Token: 0x040008FD RID: 2301
		private SqlRecordBuffer[] _buffer;
	}
}
