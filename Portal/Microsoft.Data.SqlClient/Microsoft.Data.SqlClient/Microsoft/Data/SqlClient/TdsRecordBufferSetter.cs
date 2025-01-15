using System;
using System.Data.SqlTypes;
using System.Diagnostics;
using Microsoft.Data.SqlClient.Server;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000BB RID: 187
	internal class TdsRecordBufferSetter : SmiRecordBuffer
	{
		// Token: 0x06000D74 RID: 3444 RVA: 0x0002B2D0 File Offset: 0x000294D0
		internal TdsRecordBufferSetter(TdsParserStateObject stateObj, SmiMetaData md)
		{
			this._fieldSetters = new TdsValueSetter[md.FieldMetaData.Count];
			for (int i = 0; i < md.FieldMetaData.Count; i++)
			{
				this._fieldSetters[i] = new TdsValueSetter(stateObj, md.FieldMetaData[i]);
			}
			this._stateObj = stateObj;
			this._metaData = md;
		}

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x06000D75 RID: 3445 RVA: 0x0001996E File Offset: 0x00017B6E
		internal override bool CanGet
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06000D76 RID: 3446 RVA: 0x0000EBAD File Offset: 0x0000CDAD
		internal override bool CanSet
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x0002B337 File Offset: 0x00029537
		public override void SetDBNull(SmiEventSink sink, int ordinal)
		{
			this._fieldSetters[ordinal].SetDBNull();
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x0002B346 File Offset: 0x00029546
		public override void SetBoolean(SmiEventSink sink, int ordinal, bool value)
		{
			this._fieldSetters[ordinal].SetBoolean(value);
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x0002B356 File Offset: 0x00029556
		public override void SetByte(SmiEventSink sink, int ordinal, byte value)
		{
			this._fieldSetters[ordinal].SetByte(value);
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x0002B366 File Offset: 0x00029566
		public override int SetBytes(SmiEventSink sink, int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			return this._fieldSetters[ordinal].SetBytes(fieldOffset, buffer, bufferOffset, length);
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x0002B37C File Offset: 0x0002957C
		public override void SetBytesLength(SmiEventSink sink, int ordinal, long length)
		{
			this._fieldSetters[ordinal].SetBytesLength(length);
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x0002B38C File Offset: 0x0002958C
		public override int SetChars(SmiEventSink sink, int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			return this._fieldSetters[ordinal].SetChars(fieldOffset, buffer, bufferOffset, length);
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x0002B3A2 File Offset: 0x000295A2
		public override void SetCharsLength(SmiEventSink sink, int ordinal, long length)
		{
			this._fieldSetters[ordinal].SetCharsLength(length);
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x0002B3B2 File Offset: 0x000295B2
		public override void SetString(SmiEventSink sink, int ordinal, string value, int offset, int length)
		{
			this._fieldSetters[ordinal].SetString(value, offset, length);
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x0002B3C6 File Offset: 0x000295C6
		public override void SetInt16(SmiEventSink sink, int ordinal, short value)
		{
			this._fieldSetters[ordinal].SetInt16(value);
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x0002B3D6 File Offset: 0x000295D6
		public override void SetInt32(SmiEventSink sink, int ordinal, int value)
		{
			this._fieldSetters[ordinal].SetInt32(value);
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x0002B3E6 File Offset: 0x000295E6
		public override void SetInt64(SmiEventSink sink, int ordinal, long value)
		{
			this._fieldSetters[ordinal].SetInt64(value);
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x0002B3F6 File Offset: 0x000295F6
		public override void SetSingle(SmiEventSink sink, int ordinal, float value)
		{
			this._fieldSetters[ordinal].SetSingle(value);
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x0002B406 File Offset: 0x00029606
		public override void SetDouble(SmiEventSink sink, int ordinal, double value)
		{
			this._fieldSetters[ordinal].SetDouble(value);
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x0002B416 File Offset: 0x00029616
		public override void SetSqlDecimal(SmiEventSink sink, int ordinal, SqlDecimal value)
		{
			this._fieldSetters[ordinal].SetSqlDecimal(value);
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x0002B426 File Offset: 0x00029626
		public override void SetDateTime(SmiEventSink sink, int ordinal, DateTime value)
		{
			this._fieldSetters[ordinal].SetDateTime(value);
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x0002B436 File Offset: 0x00029636
		public override void SetGuid(SmiEventSink sink, int ordinal, Guid value)
		{
			this._fieldSetters[ordinal].SetGuid(value);
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x0002B446 File Offset: 0x00029646
		public override void SetTimeSpan(SmiEventSink sink, int ordinal, TimeSpan value)
		{
			this._fieldSetters[ordinal].SetTimeSpan(value);
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x0002B456 File Offset: 0x00029656
		public override void SetDateTimeOffset(SmiEventSink sink, int ordinal, DateTimeOffset value)
		{
			this._fieldSetters[ordinal].SetDateTimeOffset(value);
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x0002B466 File Offset: 0x00029666
		public override void SetVariantMetaData(SmiEventSink sink, int ordinal, SmiMetaData metaData)
		{
			this._fieldSetters[ordinal].SetVariantType(metaData);
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x0002B476 File Offset: 0x00029676
		internal override void NewElement(SmiEventSink sink)
		{
			this._stateObj.WriteByte(1);
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x0002B484 File Offset: 0x00029684
		internal override void EndElements(SmiEventSink sink)
		{
			this._stateObj.WriteByte(0);
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x0000BB08 File Offset: 0x00009D08
		[Conditional("DEBUG")]
		private void CheckWritingToColumn(int ordinal)
		{
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x0000BB08 File Offset: 0x00009D08
		[Conditional("DEBUG")]
		private void SkipPossibleDefaultedColumns(int targetColumn)
		{
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x0000BB08 File Offset: 0x00009D08
		[Conditional("DEBUG")]
		internal void CheckSettingColumn(int ordinal)
		{
		}

		// Token: 0x040005DF RID: 1503
		private TdsValueSetter[] _fieldSetters;

		// Token: 0x040005E0 RID: 1504
		private TdsParserStateObject _stateObj;

		// Token: 0x040005E1 RID: 1505
		private SmiMetaData _metaData;
	}
}
