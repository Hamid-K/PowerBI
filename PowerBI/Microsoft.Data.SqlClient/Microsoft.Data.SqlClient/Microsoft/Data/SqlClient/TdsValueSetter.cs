using System;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Text;
using Microsoft.Data.SqlClient.Server;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000BD RID: 189
	internal class TdsValueSetter
	{
		// Token: 0x06000D99 RID: 3481 RVA: 0x0002B953 File Offset: 0x00029B53
		internal TdsValueSetter(TdsParserStateObject stateObj, SmiMetaData md)
		{
			this._stateObj = stateObj;
			this._metaData = md;
			this._isPlp = MetaDataUtilsSmi.IsPlpFormat(md);
			this._plpUnknownSent = false;
			this._encoder = null;
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x0002B984 File Offset: 0x00029B84
		internal void SetDBNull()
		{
			if (this._isPlp)
			{
				this._stateObj.Parser.WriteUnsignedLong(ulong.MaxValue, this._stateObj);
				return;
			}
			switch (this._metaData.SqlDbType)
			{
			case SqlDbType.BigInt:
			case SqlDbType.Bit:
			case SqlDbType.DateTime:
			case SqlDbType.Decimal:
			case SqlDbType.Float:
			case SqlDbType.Int:
			case SqlDbType.Money:
			case SqlDbType.Real:
			case SqlDbType.UniqueIdentifier:
			case SqlDbType.SmallDateTime:
			case SqlDbType.SmallInt:
			case SqlDbType.SmallMoney:
			case SqlDbType.TinyInt:
			case SqlDbType.Date:
			case SqlDbType.Time:
			case SqlDbType.DateTime2:
			case SqlDbType.DateTimeOffset:
				this._stateObj.WriteByte(0);
				return;
			case SqlDbType.Binary:
			case SqlDbType.Char:
			case SqlDbType.Image:
			case SqlDbType.NChar:
			case SqlDbType.NText:
			case SqlDbType.NVarChar:
			case SqlDbType.Text:
			case SqlDbType.Timestamp:
			case SqlDbType.VarBinary:
			case SqlDbType.VarChar:
				this._stateObj.Parser.WriteShort(65535, this._stateObj);
				return;
			case SqlDbType.Variant:
				this._stateObj.Parser.WriteInt(0, this._stateObj);
				break;
			case (SqlDbType)24:
			case SqlDbType.Xml:
			case (SqlDbType)26:
			case (SqlDbType)27:
			case (SqlDbType)28:
			case SqlDbType.Udt:
			case SqlDbType.Structured:
				break;
			default:
				return;
			}
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x0002BA94 File Offset: 0x00029C94
		internal void SetBoolean(bool value)
		{
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				this._stateObj.Parser.WriteSqlVariantHeader(3, 50, 0, this._stateObj);
			}
			else
			{
				this._stateObj.WriteByte((byte)this._metaData.MaxLength);
			}
			if (value)
			{
				this._stateObj.WriteByte(1);
				return;
			}
			this._stateObj.WriteByte(0);
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x0002BB00 File Offset: 0x00029D00
		internal void SetByte(byte value)
		{
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				this._stateObj.Parser.WriteSqlVariantHeader(3, 48, 0, this._stateObj);
			}
			else
			{
				this._stateObj.WriteByte((byte)this._metaData.MaxLength);
			}
			this._stateObj.WriteByte(value);
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x0002BB5B File Offset: 0x00029D5B
		internal int SetBytes(long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			this.SetBytesNoOffsetHandling(fieldOffset, buffer, bufferOffset, length);
			return length;
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x0002BB6C File Offset: 0x00029D6C
		private void SetBytesNoOffsetHandling(long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			if (this._isPlp)
			{
				if (!this._plpUnknownSent)
				{
					this._stateObj.Parser.WriteUnsignedLong(18446744073709551614UL, this._stateObj);
					this._plpUnknownSent = true;
				}
				this._stateObj.Parser.WriteInt(length, this._stateObj);
				this._stateObj.WriteByteArray(buffer, length, bufferOffset, true, null);
				return;
			}
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				this._stateObj.Parser.WriteSqlVariantHeader(4 + length, 165, 2, this._stateObj);
			}
			this._stateObj.Parser.WriteShort(length, this._stateObj);
			this._stateObj.WriteByteArray(buffer, length, bufferOffset, true, null);
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x0002BC30 File Offset: 0x00029E30
		internal void SetBytesLength(long length)
		{
			if (length == 0L)
			{
				if (this._isPlp)
				{
					this._stateObj.Parser.WriteLong(0L, this._stateObj);
					this._plpUnknownSent = true;
				}
				else
				{
					if (SqlDbType.Variant == this._metaData.SqlDbType)
					{
						this._stateObj.Parser.WriteSqlVariantHeader(4, 165, 2, this._stateObj);
					}
					this._stateObj.Parser.WriteShort(0, this._stateObj);
				}
			}
			if (this._plpUnknownSent)
			{
				this._stateObj.Parser.WriteInt(0, this._stateObj);
				this._plpUnknownSent = false;
			}
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x0002BCD4 File Offset: 0x00029ED4
		internal int SetChars(long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			if (MetaDataUtilsSmi.IsAnsiType(this._metaData.SqlDbType))
			{
				if (this._encoder == null)
				{
					this._encoder = this._stateObj.Parser._defaultEncoding.GetEncoder();
				}
				byte[] array = new byte[this._encoder.GetByteCount(buffer, bufferOffset, length, false)];
				this._encoder.GetBytes(buffer, bufferOffset, length, array, 0, false);
				this.SetBytesNoOffsetHandling(fieldOffset, array, 0, array.Length);
			}
			else if (this._isPlp)
			{
				if (!this._plpUnknownSent)
				{
					this._stateObj.Parser.WriteUnsignedLong(18446744073709551614UL, this._stateObj);
					this._plpUnknownSent = true;
				}
				this._stateObj.Parser.WriteInt(length * 2, this._stateObj);
				this._stateObj.Parser.WriteCharArray(buffer, length, bufferOffset, this._stateObj, true);
			}
			else if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				this._stateObj.Parser.WriteSqlVariantValue(new string(buffer, bufferOffset, length), length, 0, this._stateObj, true);
			}
			else
			{
				this._stateObj.Parser.WriteShort(length * 2, this._stateObj);
				this._stateObj.Parser.WriteCharArray(buffer, length, bufferOffset, this._stateObj, true);
			}
			return length;
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x0002BE28 File Offset: 0x0002A028
		internal void SetCharsLength(long length)
		{
			if (length == 0L)
			{
				if (this._isPlp)
				{
					this._stateObj.Parser.WriteLong(0L, this._stateObj);
					this._plpUnknownSent = true;
				}
				else
				{
					this._stateObj.Parser.WriteShort(0, this._stateObj);
				}
			}
			if (this._plpUnknownSent)
			{
				this._stateObj.Parser.WriteInt(0, this._stateObj);
				this._plpUnknownSent = false;
			}
			this._encoder = null;
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x0002BEA8 File Offset: 0x0002A0A8
		internal void SetString(string value, int offset, int length)
		{
			if (MetaDataUtilsSmi.IsAnsiType(this._metaData.SqlDbType))
			{
				byte[] array;
				if (offset == 0 && value.Length <= length)
				{
					array = this._stateObj.Parser._defaultEncoding.GetBytes(value);
				}
				else
				{
					char[] array2 = value.ToCharArray(offset, length);
					array = this._stateObj.Parser._defaultEncoding.GetBytes(array2);
				}
				this.SetBytes(0L, array, 0, array.Length);
				this.SetBytesLength((long)array.Length);
				return;
			}
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				SqlCollation sqlCollation = SqlCollation.FromLCIDAndSort(checked((int)this._variantType.LocaleId), this._variantType.CompareOptions);
				if (length * 2 > 8000)
				{
					byte[] array3;
					if (offset == 0 && value.Length <= length)
					{
						array3 = this._stateObj.Parser._defaultEncoding.GetBytes(value);
					}
					else
					{
						array3 = this._stateObj.Parser._defaultEncoding.GetBytes(value.ToCharArray(offset, length));
					}
					this._stateObj.Parser.WriteSqlVariantHeader(9 + array3.Length, 167, 7, this._stateObj);
					this._stateObj.Parser.WriteUnsignedInt(sqlCollation._info, this._stateObj);
					this._stateObj.WriteByte(sqlCollation._sortId);
					this._stateObj.Parser.WriteShort(array3.Length, this._stateObj);
					this._stateObj.WriteByteArray(array3, array3.Length, 0, true, null);
				}
				else
				{
					this._stateObj.Parser.WriteSqlVariantHeader(9 + length * 2, 231, 7, this._stateObj);
					this._stateObj.Parser.WriteUnsignedInt(sqlCollation._info, this._stateObj);
					this._stateObj.WriteByte(sqlCollation._sortId);
					this._stateObj.Parser.WriteShort(length * 2, this._stateObj);
					this._stateObj.Parser.WriteString(value, length, offset, this._stateObj, true);
				}
				this._variantType = null;
				return;
			}
			if (this._isPlp)
			{
				this._stateObj.Parser.WriteLong((long)(length * 2), this._stateObj);
				this._stateObj.Parser.WriteInt(length * 2, this._stateObj);
				this._stateObj.Parser.WriteString(value, length, offset, this._stateObj, true);
				if (length != 0)
				{
					this._stateObj.Parser.WriteInt(0, this._stateObj);
					return;
				}
			}
			else
			{
				this._stateObj.Parser.WriteShort(length * 2, this._stateObj);
				this._stateObj.Parser.WriteString(value, length, offset, this._stateObj, true);
			}
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x0002C154 File Offset: 0x0002A354
		internal void SetInt16(short value)
		{
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				this._stateObj.Parser.WriteSqlVariantHeader(4, 52, 0, this._stateObj);
			}
			else
			{
				this._stateObj.WriteByte((byte)this._metaData.MaxLength);
			}
			this._stateObj.Parser.WriteShort((int)value, this._stateObj);
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x0002C1BC File Offset: 0x0002A3BC
		internal void SetInt32(int value)
		{
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				this._stateObj.Parser.WriteSqlVariantHeader(6, 56, 0, this._stateObj);
			}
			else
			{
				this._stateObj.WriteByte((byte)this._metaData.MaxLength);
			}
			this._stateObj.Parser.WriteInt(value, this._stateObj);
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x0002C224 File Offset: 0x0002A424
		internal void SetInt64(long value)
		{
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				if (this._variantType == null)
				{
					this._stateObj.Parser.WriteSqlVariantHeader(10, 127, 0, this._stateObj);
					this._stateObj.Parser.WriteLong(value, this._stateObj);
					return;
				}
				this._stateObj.Parser.WriteSqlVariantHeader(10, 60, 0, this._stateObj);
				this._stateObj.Parser.WriteInt((int)(value >> 32), this._stateObj);
				this._stateObj.Parser.WriteInt((int)value, this._stateObj);
				this._variantType = null;
				return;
			}
			else
			{
				this._stateObj.WriteByte((byte)this._metaData.MaxLength);
				if (SqlDbType.SmallMoney == this._metaData.SqlDbType)
				{
					this._stateObj.Parser.WriteInt((int)value, this._stateObj);
					return;
				}
				if (SqlDbType.Money == this._metaData.SqlDbType)
				{
					this._stateObj.Parser.WriteInt((int)(value >> 32), this._stateObj);
					this._stateObj.Parser.WriteInt((int)value, this._stateObj);
					return;
				}
				this._stateObj.Parser.WriteLong(value, this._stateObj);
				return;
			}
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x0002C370 File Offset: 0x0002A570
		internal void SetSingle(float value)
		{
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				this._stateObj.Parser.WriteSqlVariantHeader(6, 59, 0, this._stateObj);
			}
			else
			{
				this._stateObj.WriteByte((byte)this._metaData.MaxLength);
			}
			this._stateObj.Parser.WriteFloat(value, this._stateObj);
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x0002C3D8 File Offset: 0x0002A5D8
		internal void SetDouble(double value)
		{
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				this._stateObj.Parser.WriteSqlVariantHeader(10, 62, 0, this._stateObj);
			}
			else
			{
				this._stateObj.WriteByte((byte)this._metaData.MaxLength);
			}
			this._stateObj.Parser.WriteDouble(value, this._stateObj);
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x0002C440 File Offset: 0x0002A640
		internal void SetSqlDecimal(SqlDecimal value)
		{
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				this._stateObj.Parser.WriteSqlVariantHeader(21, 108, 2, this._stateObj);
				this._stateObj.WriteByte(value.Precision);
				this._stateObj.WriteByte(value.Scale);
				this._stateObj.Parser.WriteSqlDecimal(value, this._stateObj);
				return;
			}
			this._stateObj.WriteByte(checked((byte)MetaType.MetaDecimal.FixedLength));
			this._stateObj.Parser.WriteSqlDecimal(SqlDecimal.ConvertToPrecScale(value, (int)this._metaData.Precision, (int)this._metaData.Scale), this._stateObj);
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x0002C4FC File Offset: 0x0002A6FC
		internal void SetDateTime(DateTime value)
		{
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				if (this._variantType != null && this._variantType.SqlDbType == SqlDbType.DateTime2)
				{
					this._stateObj.Parser.WriteSqlVariantDateTime2(value, this._stateObj);
				}
				else if (this._variantType != null && this._variantType.SqlDbType == SqlDbType.Date)
				{
					this._stateObj.Parser.WriteSqlVariantDate(value, this._stateObj);
				}
				else
				{
					TdsDateTime tdsDateTime = MetaType.FromDateTime(value, 8);
					this._stateObj.Parser.WriteSqlVariantHeader(10, 61, 0, this._stateObj);
					this._stateObj.Parser.WriteInt(tdsDateTime.days, this._stateObj);
					this._stateObj.Parser.WriteInt(tdsDateTime.time, this._stateObj);
				}
				this._variantType = null;
				return;
			}
			this._stateObj.WriteByte((byte)this._metaData.MaxLength);
			if (SqlDbType.SmallDateTime == this._metaData.SqlDbType)
			{
				TdsDateTime tdsDateTime2 = MetaType.FromDateTime(value, (byte)this._metaData.MaxLength);
				this._stateObj.Parser.WriteShort(tdsDateTime2.days, this._stateObj);
				this._stateObj.Parser.WriteShort(tdsDateTime2.time, this._stateObj);
				return;
			}
			if (SqlDbType.DateTime == this._metaData.SqlDbType)
			{
				TdsDateTime tdsDateTime3 = MetaType.FromDateTime(value, (byte)this._metaData.MaxLength);
				this._stateObj.Parser.WriteInt(tdsDateTime3.days, this._stateObj);
				this._stateObj.Parser.WriteInt(tdsDateTime3.time, this._stateObj);
				return;
			}
			int days = value.Subtract(DateTime.MinValue).Days;
			if (SqlDbType.DateTime2 == this._metaData.SqlDbType)
			{
				long num = value.TimeOfDay.Ticks / TdsEnums.TICKS_FROM_SCALE[(int)this._metaData.Scale];
				this._stateObj.WriteByteArray(BitConverter.GetBytes(num), (int)this._metaData.MaxLength - 3, 0, true, null);
			}
			this._stateObj.WriteByteArray(BitConverter.GetBytes(days), 3, 0, true, null);
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x0002C730 File Offset: 0x0002A930
		internal void SetGuid(Guid value)
		{
			byte[] array = value.ToByteArray();
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				this._stateObj.Parser.WriteSqlVariantHeader(18, 36, 0, this._stateObj);
			}
			else
			{
				this._stateObj.WriteByte((byte)this._metaData.MaxLength);
			}
			this._stateObj.WriteByteArray(array, array.Length, 0, true, null);
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x0002C79C File Offset: 0x0002A99C
		internal void SetTimeSpan(TimeSpan value)
		{
			byte b;
			byte b2;
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				b = SmiMetaData.DefaultTime.Scale;
				b2 = (byte)SmiMetaData.DefaultTime.MaxLength;
				this._stateObj.Parser.WriteSqlVariantHeader(8, 41, 1, this._stateObj);
				this._stateObj.WriteByte(b);
			}
			else
			{
				b = this._metaData.Scale;
				b2 = (byte)this._metaData.MaxLength;
				this._stateObj.WriteByte(b2);
			}
			long num = value.Ticks / TdsEnums.TICKS_FROM_SCALE[(int)b];
			this._stateObj.WriteByteArray(BitConverter.GetBytes(num), (int)b2, 0, true, null);
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x0002C844 File Offset: 0x0002AA44
		internal void SetDateTimeOffset(DateTimeOffset value)
		{
			byte b;
			byte b2;
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				SmiMetaData defaultDateTimeOffset = SmiMetaData.DefaultDateTimeOffset;
				b = MetaType.MetaDateTimeOffset.Scale;
				b2 = (byte)defaultDateTimeOffset.MaxLength;
				this._stateObj.Parser.WriteSqlVariantHeader(13, 43, 1, this._stateObj);
				this._stateObj.WriteByte(b);
			}
			else
			{
				b = this._metaData.Scale;
				b2 = (byte)this._metaData.MaxLength;
				this._stateObj.WriteByte(b2);
			}
			DateTime utcDateTime = value.UtcDateTime;
			long num = utcDateTime.TimeOfDay.Ticks / TdsEnums.TICKS_FROM_SCALE[(int)b];
			int days = utcDateTime.Subtract(DateTime.MinValue).Days;
			short num2 = (short)value.Offset.TotalMinutes;
			this._stateObj.WriteByteArray(BitConverter.GetBytes(num), (int)(b2 - 5), 0, true, null);
			this._stateObj.WriteByteArray(BitConverter.GetBytes(days), 3, 0, true, null);
			this._stateObj.WriteByte((byte)(num2 & 255));
			this._stateObj.WriteByte((byte)((num2 >> 8) & 255));
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x0002C96D File Offset: 0x0002AB6D
		internal void SetVariantType(SmiMetaData value)
		{
			this._variantType = value;
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x0000BB08 File Offset: 0x00009D08
		[Conditional("DEBUG")]
		private void CheckSettingOffset(long offset)
		{
		}

		// Token: 0x040005EA RID: 1514
		private TdsParserStateObject _stateObj;

		// Token: 0x040005EB RID: 1515
		private SmiMetaData _metaData;

		// Token: 0x040005EC RID: 1516
		private bool _isPlp;

		// Token: 0x040005ED RID: 1517
		private bool _plpUnknownSent;

		// Token: 0x040005EE RID: 1518
		private Encoder _encoder;

		// Token: 0x040005EF RID: 1519
		private SmiMetaData _variantType;
	}
}
