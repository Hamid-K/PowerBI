using System;
using System.Data.SqlTypes;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.Data.SqlTypes;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x0200004F RID: 79
	internal sealed class SqlBuffer
	{
		// Token: 0x060007BF RID: 1983 RVA: 0x000027D1 File Offset: 0x000009D1
		internal SqlBuffer()
		{
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x0001106B File Offset: 0x0000F26B
		private SqlBuffer(SqlBuffer value)
		{
			this._isNull = value._isNull;
			this._type = value._type;
			this._value = value._value;
			this._object = value._object;
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x060007C1 RID: 1985 RVA: 0x000110A3 File Offset: 0x0000F2A3
		internal bool IsEmpty
		{
			get
			{
				return this._type == SqlBuffer.StorageType.Empty;
			}
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x060007C2 RID: 1986 RVA: 0x000110AE File Offset: 0x0000F2AE
		internal bool IsNull
		{
			get
			{
				return this._isNull;
			}
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x060007C3 RID: 1987 RVA: 0x000110B6 File Offset: 0x0000F2B6
		internal SqlBuffer.StorageType VariantInternalStorageType
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x060007C4 RID: 1988 RVA: 0x000110BE File Offset: 0x0000F2BE
		// (set) Token: 0x060007C5 RID: 1989 RVA: 0x000110E6 File Offset: 0x0000F2E6
		internal bool Boolean
		{
			get
			{
				this.ThrowIfNull();
				if (SqlBuffer.StorageType.Boolean == this._type)
				{
					return this._value._boolean;
				}
				return (bool)this.Value;
			}
			set
			{
				this._value._boolean = value;
				this._type = SqlBuffer.StorageType.Boolean;
				this._isNull = false;
			}
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x060007C6 RID: 1990 RVA: 0x00011102 File Offset: 0x0000F302
		// (set) Token: 0x060007C7 RID: 1991 RVA: 0x0001112A File Offset: 0x0000F32A
		internal byte Byte
		{
			get
			{
				this.ThrowIfNull();
				if (SqlBuffer.StorageType.Byte == this._type)
				{
					return this._value._byte;
				}
				return (byte)this.Value;
			}
			set
			{
				this._value._byte = value;
				this._type = SqlBuffer.StorageType.Byte;
				this._isNull = false;
			}
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x060007C8 RID: 1992 RVA: 0x00011148 File Offset: 0x0000F348
		internal byte[] ByteArray
		{
			get
			{
				this.ThrowIfNull();
				return this.SqlBinary.Value;
			}
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x060007C9 RID: 1993 RVA: 0x0001116C File Offset: 0x0000F36C
		internal DateTime DateTime
		{
			get
			{
				this.ThrowIfNull();
				if (SqlBuffer.StorageType.Date == this._type)
				{
					return DateTime.MinValue.AddDays((double)this._value._int32);
				}
				if (SqlBuffer.StorageType.DateTime2 == this._type)
				{
					return new DateTime(SqlBuffer.GetTicksFromDateTime2Info(this._value._dateTime2Info));
				}
				if (SqlBuffer.StorageType.DateTime == this._type)
				{
					return SqlTypeWorkarounds.SqlDateTimeToDateTime(this._value._dateTimeInfo._daypart, this._value._dateTimeInfo._timepart);
				}
				return (DateTime)this.Value;
			}
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x060007CA RID: 1994 RVA: 0x00011200 File Offset: 0x0000F400
		internal decimal Decimal
		{
			get
			{
				this.ThrowIfNull();
				if (SqlBuffer.StorageType.Decimal == this._type)
				{
					if (this._value._numericInfo._data4 != 0 || this._value._numericInfo._scale > 28)
					{
						if (this._value._numericInfo._scale > 0)
						{
							int num2;
							int num = SqlBuffer.FindTrailingZerosAndPrec((uint)this._value._numericInfo._data1, (uint)this._value._numericInfo._data2, (uint)this._value._numericInfo._data3, (uint)this._value._numericInfo._data4, this._value._numericInfo._scale, out num2);
							int num3 = (int)this._value._numericInfo._scale - num;
							if (num > 0 && num3 <= 28 && num2 <= 29)
							{
								SqlDecimal sqlDecimal = new SqlDecimal(this._value._numericInfo._precision, this._value._numericInfo._scale, this._value._numericInfo._positive, this._value._numericInfo._data1, this._value._numericInfo._data2, this._value._numericInfo._data3, this._value._numericInfo._data4);
								int num4 = num2 - num3;
								int num5 = 29;
								if (num4 != 1 && num2 != 29)
								{
									num5 = 28;
								}
								try
								{
									return SqlDecimal.ConvertToPrecScale(sqlDecimal, num5, num5 - num4).Value;
								}
								catch (OverflowException)
								{
									throw new OverflowException(SQLResource.ConversionOverflowMessage);
								}
							}
						}
						throw new OverflowException(SQLResource.ConversionOverflowMessage);
					}
					return new decimal(this._value._numericInfo._data1, this._value._numericInfo._data2, this._value._numericInfo._data3, !this._value._numericInfo._positive, this._value._numericInfo._scale);
				}
				else
				{
					if (SqlBuffer.StorageType.Money == this._type)
					{
						long num6 = this._value._int64;
						bool flag = false;
						if (num6 < 0L)
						{
							flag = true;
							num6 = -num6;
						}
						return new decimal((int)(num6 & (long)((ulong)(-1))), (int)(num6 >> 32), 0, flag, 4);
					}
					return (decimal)this.Value;
				}
			}
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x00011458 File Offset: 0x0000F658
		private unsafe static int FindTrailingZerosAndPrec(uint data1, uint data2, uint data3, uint data4, byte scale, out int valuablePrecision)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)16];
			*intPtr = (int)data1;
			*(intPtr + 4) = (int)data2;
			*(intPtr + (IntPtr)2 * 4) = (int)data3;
			*(intPtr + (IntPtr)3 * 4) = (int)data4;
			Span<uint> span = new Span<uint>(intPtr, 4);
			Span<uint> span2 = span;
			int num = 0;
			int num2 = 0;
			uint num3 = 0U;
			int num4 = 4;
			while (num4 > 1 || *span2[0] != 0U)
			{
				SqlBuffer.SqlDecimalDivBy(span2, ref num4, 10U, out num3);
				if (num3 == 0U && num2 == 0)
				{
					num++;
				}
				else
				{
					num2++;
				}
			}
			if (num3 == 0U)
			{
				num = (int)scale;
			}
			if (num + num2 <= (int)scale)
			{
				num2 = (int)scale - num + 1;
			}
			valuablePrecision = num2;
			return num;
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x000114E0 File Offset: 0x0000F6E0
		private unsafe static void SqlDecimalDivBy(Span<uint> data, ref int len, uint divisor, out uint remainder)
		{
			uint num = 0U;
			ulong num2 = (ulong)divisor;
			int i = len;
			while (i > 0)
			{
				i--;
				ulong num3 = ((ulong)num << 32) + (ulong)(*data[i]);
				*data[i] = (uint)(num3 / num2);
				num = (uint)(num3 - (ulong)(*data[i]) * num2);
			}
			remainder = num;
			while (len > 1 && *data[len - 1] == 0U)
			{
				len--;
			}
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x060007CD RID: 1997 RVA: 0x0001154B File Offset: 0x0000F74B
		// (set) Token: 0x060007CE RID: 1998 RVA: 0x00011573 File Offset: 0x0000F773
		internal double Double
		{
			get
			{
				this.ThrowIfNull();
				if (SqlBuffer.StorageType.Double == this._type)
				{
					return this._value._double;
				}
				return (double)this.Value;
			}
			set
			{
				this._value._double = value;
				this._type = SqlBuffer.StorageType.Double;
				this._isNull = false;
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x060007CF RID: 1999 RVA: 0x00011590 File Offset: 0x0000F790
		// (set) Token: 0x060007D0 RID: 2000 RVA: 0x000115E2 File Offset: 0x0000F7E2
		internal Guid Guid
		{
			get
			{
				this.ThrowIfNull();
				if (SqlBuffer.StorageType.Guid == this._type)
				{
					return this._value._guid;
				}
				if (SqlBuffer.StorageType.SqlGuid == this._type)
				{
					return ((SqlGuid)this._object).Value;
				}
				return (Guid)this.Value;
			}
			set
			{
				this._type = SqlBuffer.StorageType.Guid;
				this._value._guid = value;
				this._isNull = false;
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x060007D1 RID: 2001 RVA: 0x000115FF File Offset: 0x0000F7FF
		// (set) Token: 0x060007D2 RID: 2002 RVA: 0x00011627 File Offset: 0x0000F827
		internal short Int16
		{
			get
			{
				this.ThrowIfNull();
				if (SqlBuffer.StorageType.Int16 == this._type)
				{
					return this._value._int16;
				}
				return (short)this.Value;
			}
			set
			{
				this._value._int16 = value;
				this._type = SqlBuffer.StorageType.Int16;
				this._isNull = false;
			}
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x060007D3 RID: 2003 RVA: 0x00011643 File Offset: 0x0000F843
		// (set) Token: 0x060007D4 RID: 2004 RVA: 0x0001166B File Offset: 0x0000F86B
		internal int Int32
		{
			get
			{
				this.ThrowIfNull();
				if (SqlBuffer.StorageType.Int32 == this._type)
				{
					return this._value._int32;
				}
				return (int)this.Value;
			}
			set
			{
				this._value._int32 = value;
				this._type = SqlBuffer.StorageType.Int32;
				this._isNull = false;
			}
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x060007D5 RID: 2005 RVA: 0x00011687 File Offset: 0x0000F887
		// (set) Token: 0x060007D6 RID: 2006 RVA: 0x000116AF File Offset: 0x0000F8AF
		internal long Int64
		{
			get
			{
				this.ThrowIfNull();
				if (SqlBuffer.StorageType.Int64 == this._type)
				{
					return this._value._int64;
				}
				return (long)this.Value;
			}
			set
			{
				this._value._int64 = value;
				this._type = SqlBuffer.StorageType.Int64;
				this._isNull = false;
			}
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x060007D7 RID: 2007 RVA: 0x000116CB File Offset: 0x0000F8CB
		// (set) Token: 0x060007D8 RID: 2008 RVA: 0x000116F4 File Offset: 0x0000F8F4
		internal float Single
		{
			get
			{
				this.ThrowIfNull();
				if (SqlBuffer.StorageType.Single == this._type)
				{
					return this._value._single;
				}
				return (float)this.Value;
			}
			set
			{
				this._value._single = value;
				this._type = SqlBuffer.StorageType.Single;
				this._isNull = false;
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x060007D9 RID: 2009 RVA: 0x00011714 File Offset: 0x0000F914
		internal string String
		{
			get
			{
				this.ThrowIfNull();
				if (SqlBuffer.StorageType.String == this._type)
				{
					return (string)this._object;
				}
				if (SqlBuffer.StorageType.SqlCachedBuffer == this._type)
				{
					return ((SqlCachedBuffer)this._object).ToString();
				}
				return (string)this.Value;
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x060007DA RID: 2010 RVA: 0x00011764 File Offset: 0x0000F964
		internal string Sql2008DateTimeString
		{
			get
			{
				this.ThrowIfNull();
				if (SqlBuffer.StorageType.Date == this._type)
				{
					return this.DateTime.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);
				}
				if (SqlBuffer.StorageType.Time == this._type)
				{
					byte scale = this._value._timeInfo._scale;
					return new DateTime(this._value._timeInfo._ticks).ToString(SqlBuffer.s_sql2008TimeFormatByScale[(int)scale], DateTimeFormatInfo.InvariantInfo);
				}
				if (SqlBuffer.StorageType.DateTime2 == this._type)
				{
					byte scale2 = this._value._dateTime2Info._timeInfo._scale;
					return this.DateTime.ToString(SqlBuffer.s_sql2008DateTime2FormatByScale[(int)scale2], DateTimeFormatInfo.InvariantInfo);
				}
				if (SqlBuffer.StorageType.DateTimeOffset == this._type)
				{
					DateTimeOffset dateTimeOffset = this.DateTimeOffset;
					byte scale3 = this._value._dateTimeOffsetInfo._dateTime2Info._timeInfo._scale;
					return dateTimeOffset.ToString(SqlBuffer.s_sql2008DateTimeOffsetFormatByScale[(int)scale3], DateTimeFormatInfo.InvariantInfo);
				}
				return (string)this.Value;
			}
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x060007DB RID: 2011 RVA: 0x00011868 File Offset: 0x0000FA68
		internal SqlString Sql2008DateTimeSqlString
		{
			get
			{
				if (SqlBuffer.StorageType.Date != this._type && SqlBuffer.StorageType.Time != this._type && SqlBuffer.StorageType.DateTime2 != this._type && SqlBuffer.StorageType.DateTimeOffset != this._type)
				{
					return (SqlString)this.SqlValue;
				}
				if (this.IsNull)
				{
					return SqlString.Null;
				}
				return new SqlString(this.Sql2008DateTimeString);
			}
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x060007DC RID: 2012 RVA: 0x000118C2 File Offset: 0x0000FAC2
		internal TimeSpan Time
		{
			get
			{
				this.ThrowIfNull();
				if (SqlBuffer.StorageType.Time == this._type)
				{
					return new TimeSpan(this._value._timeInfo._ticks);
				}
				return (TimeSpan)this.Value;
			}
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x060007DD RID: 2013 RVA: 0x000118F8 File Offset: 0x0000FAF8
		internal DateTimeOffset DateTimeOffset
		{
			get
			{
				this.ThrowIfNull();
				if (SqlBuffer.StorageType.DateTimeOffset == this._type)
				{
					TimeSpan timeSpan = new TimeSpan(0, (int)this._value._dateTimeOffsetInfo._offset, 0);
					return new DateTimeOffset(SqlBuffer.GetTicksFromDateTime2Info(this._value._dateTimeOffsetInfo._dateTime2Info) + timeSpan.Ticks, timeSpan);
				}
				return (DateTimeOffset)this.Value;
			}
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x0001195D File Offset: 0x0000FB5D
		private static long GetTicksFromDateTime2Info(SqlBuffer.DateTime2Info dateTime2Info)
		{
			return (long)dateTime2Info._date * 864000000000L + dateTime2Info._timeInfo._ticks;
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x060007DF RID: 2015 RVA: 0x0001197C File Offset: 0x0000FB7C
		// (set) Token: 0x060007E0 RID: 2016 RVA: 0x000119AD File Offset: 0x0000FBAD
		internal SqlBinary SqlBinary
		{
			get
			{
				if (SqlBuffer.StorageType.SqlBinary != this._type)
				{
					return (SqlBinary)this.SqlValue;
				}
				if (this.IsNull)
				{
					return SqlBinary.Null;
				}
				return (SqlBinary)this._object;
			}
			set
			{
				this._object = value;
				this._type = SqlBuffer.StorageType.SqlBinary;
				this._isNull = value.IsNull;
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x060007E1 RID: 2017 RVA: 0x000119D0 File Offset: 0x0000FBD0
		internal SqlBoolean SqlBoolean
		{
			get
			{
				if (SqlBuffer.StorageType.Boolean != this._type)
				{
					return (SqlBoolean)this.SqlValue;
				}
				if (this.IsNull)
				{
					return SqlBoolean.Null;
				}
				return new SqlBoolean(this._value._boolean);
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x060007E2 RID: 2018 RVA: 0x00011A05 File Offset: 0x0000FC05
		internal SqlByte SqlByte
		{
			get
			{
				if (SqlBuffer.StorageType.Byte != this._type)
				{
					return (SqlByte)this.SqlValue;
				}
				if (this.IsNull)
				{
					return SqlByte.Null;
				}
				return new SqlByte(this._value._byte);
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x060007E3 RID: 2019 RVA: 0x00011A3A File Offset: 0x0000FC3A
		// (set) Token: 0x060007E4 RID: 2020 RVA: 0x00011A6B File Offset: 0x0000FC6B
		internal SqlCachedBuffer SqlCachedBuffer
		{
			get
			{
				if (SqlBuffer.StorageType.SqlCachedBuffer != this._type)
				{
					return (SqlCachedBuffer)this.SqlValue;
				}
				if (this.IsNull)
				{
					return SqlCachedBuffer.Null;
				}
				return (SqlCachedBuffer)this._object;
			}
			set
			{
				this._object = value;
				this._type = SqlBuffer.StorageType.SqlCachedBuffer;
				this._isNull = value.IsNull;
			}
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x060007E5 RID: 2021 RVA: 0x00011A88 File Offset: 0x0000FC88
		// (set) Token: 0x060007E6 RID: 2022 RVA: 0x00011AB9 File Offset: 0x0000FCB9
		internal SqlXml SqlXml
		{
			get
			{
				if (SqlBuffer.StorageType.SqlXml != this._type)
				{
					return (SqlXml)this.SqlValue;
				}
				if (this.IsNull)
				{
					return SqlXml.Null;
				}
				return (SqlXml)this._object;
			}
			set
			{
				this._object = value;
				this._type = SqlBuffer.StorageType.SqlXml;
				this._isNull = value.IsNull;
			}
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x060007E7 RID: 2023 RVA: 0x00011AD8 File Offset: 0x0000FCD8
		internal SqlDateTime SqlDateTime
		{
			get
			{
				if (SqlBuffer.StorageType.DateTime != this._type)
				{
					return (SqlDateTime)this.SqlValue;
				}
				if (this.IsNull)
				{
					return SqlDateTime.Null;
				}
				return new SqlDateTime(this._value._dateTimeInfo._daypart, this._value._dateTimeInfo._timepart);
			}
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x060007E8 RID: 2024 RVA: 0x00011B30 File Offset: 0x0000FD30
		internal SqlDecimal SqlDecimal
		{
			get
			{
				if (SqlBuffer.StorageType.Decimal != this._type)
				{
					return (SqlDecimal)this.SqlValue;
				}
				if (this.IsNull)
				{
					return SqlDecimal.Null;
				}
				return new SqlDecimal(this._value._numericInfo._precision, this._value._numericInfo._scale, this._value._numericInfo._positive, this._value._numericInfo._data1, this._value._numericInfo._data2, this._value._numericInfo._data3, this._value._numericInfo._data4);
			}
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x060007E9 RID: 2025 RVA: 0x00011BD8 File Offset: 0x0000FDD8
		internal SqlDouble SqlDouble
		{
			get
			{
				if (SqlBuffer.StorageType.Double != this._type)
				{
					return (SqlDouble)this.SqlValue;
				}
				if (this.IsNull)
				{
					return SqlDouble.Null;
				}
				return new SqlDouble(this._value._double);
			}
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x060007EA RID: 2026 RVA: 0x00011C10 File Offset: 0x0000FE10
		// (set) Token: 0x060007EB RID: 2027 RVA: 0x00011C67 File Offset: 0x0000FE67
		internal SqlGuid SqlGuid
		{
			get
			{
				if (SqlBuffer.StorageType.Guid == this._type)
				{
					return new SqlGuid(this._value._guid);
				}
				if (SqlBuffer.StorageType.SqlGuid != this._type)
				{
					return (SqlGuid)this.SqlValue;
				}
				if (!this.IsNull)
				{
					return (SqlGuid)this._object;
				}
				return SqlGuid.Null;
			}
			set
			{
				this._object = value;
				this._type = SqlBuffer.StorageType.SqlGuid;
				this._isNull = value.IsNull;
			}
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x060007EC RID: 2028 RVA: 0x00011C8A File Offset: 0x0000FE8A
		internal SqlInt16 SqlInt16
		{
			get
			{
				if (SqlBuffer.StorageType.Int16 != this._type)
				{
					return (SqlInt16)this.SqlValue;
				}
				if (this.IsNull)
				{
					return SqlInt16.Null;
				}
				return new SqlInt16(this._value._int16);
			}
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x060007ED RID: 2029 RVA: 0x00011CBF File Offset: 0x0000FEBF
		internal SqlInt32 SqlInt32
		{
			get
			{
				if (SqlBuffer.StorageType.Int32 != this._type)
				{
					return (SqlInt32)this.SqlValue;
				}
				if (this.IsNull)
				{
					return SqlInt32.Null;
				}
				return new SqlInt32(this._value._int32);
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x060007EE RID: 2030 RVA: 0x00011CF4 File Offset: 0x0000FEF4
		internal SqlInt64 SqlInt64
		{
			get
			{
				if (SqlBuffer.StorageType.Int64 != this._type)
				{
					return (SqlInt64)this.SqlValue;
				}
				if (this.IsNull)
				{
					return SqlInt64.Null;
				}
				return new SqlInt64(this._value._int64);
			}
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x060007EF RID: 2031 RVA: 0x00011D29 File Offset: 0x0000FF29
		internal SqlMoney SqlMoney
		{
			get
			{
				if (SqlBuffer.StorageType.Money != this._type)
				{
					return (SqlMoney)this.SqlValue;
				}
				if (this.IsNull)
				{
					return SqlMoney.Null;
				}
				return SqlTypeWorkarounds.SqlMoneyCtor(this._value._int64, 1);
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x060007F0 RID: 2032 RVA: 0x00011D60 File Offset: 0x0000FF60
		internal SqlSingle SqlSingle
		{
			get
			{
				if (SqlBuffer.StorageType.Single != this._type)
				{
					return (SqlSingle)this.SqlValue;
				}
				if (this.IsNull)
				{
					return SqlSingle.Null;
				}
				return new SqlSingle(this._value._single);
			}
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x060007F1 RID: 2033 RVA: 0x00011D98 File Offset: 0x0000FF98
		internal SqlString SqlString
		{
			get
			{
				if (SqlBuffer.StorageType.String == this._type)
				{
					if (this.IsNull)
					{
						return SqlString.Null;
					}
					return new SqlString((string)this._object);
				}
				else
				{
					if (SqlBuffer.StorageType.SqlCachedBuffer != this._type)
					{
						return (SqlString)this.SqlValue;
					}
					SqlCachedBuffer sqlCachedBuffer = (SqlCachedBuffer)this._object;
					if (sqlCachedBuffer.IsNull)
					{
						return SqlString.Null;
					}
					return sqlCachedBuffer.ToSqlString();
				}
			}
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x060007F2 RID: 2034 RVA: 0x00011E04 File Offset: 0x00010004
		internal object SqlValue
		{
			get
			{
				switch (this._type)
				{
				case SqlBuffer.StorageType.Empty:
					return DBNull.Value;
				case SqlBuffer.StorageType.Boolean:
					return this.SqlBoolean;
				case SqlBuffer.StorageType.Byte:
					return this.SqlByte;
				case SqlBuffer.StorageType.DateTime:
					return this.SqlDateTime;
				case SqlBuffer.StorageType.Decimal:
					return this.SqlDecimal;
				case SqlBuffer.StorageType.Double:
					return this.SqlDouble;
				case SqlBuffer.StorageType.Int16:
					return this.SqlInt16;
				case SqlBuffer.StorageType.Int32:
					return this.SqlInt32;
				case SqlBuffer.StorageType.Int64:
					return this.SqlInt64;
				case SqlBuffer.StorageType.Guid:
					return this.SqlGuid;
				case SqlBuffer.StorageType.Money:
					return this.SqlMoney;
				case SqlBuffer.StorageType.Single:
					return this.SqlSingle;
				case SqlBuffer.StorageType.String:
					return this.SqlString;
				case SqlBuffer.StorageType.SqlBinary:
				case SqlBuffer.StorageType.SqlGuid:
					return this._object;
				case SqlBuffer.StorageType.SqlCachedBuffer:
				{
					SqlCachedBuffer sqlCachedBuffer = (SqlCachedBuffer)this._object;
					if (sqlCachedBuffer.IsNull)
					{
						return SqlXml.Null;
					}
					return sqlCachedBuffer.ToSqlXml();
				}
				case SqlBuffer.StorageType.SqlXml:
					if (this._isNull)
					{
						return SqlXml.Null;
					}
					return (SqlXml)this._object;
				case SqlBuffer.StorageType.Date:
				case SqlBuffer.StorageType.DateTime2:
					if (this._isNull)
					{
						return DBNull.Value;
					}
					return this.DateTime;
				case SqlBuffer.StorageType.DateTimeOffset:
					if (this._isNull)
					{
						return DBNull.Value;
					}
					return this.DateTimeOffset;
				case SqlBuffer.StorageType.Time:
					if (this._isNull)
					{
						return DBNull.Value;
					}
					return this.Time;
				default:
					return null;
				}
			}
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x060007F3 RID: 2035 RVA: 0x00011FA0 File Offset: 0x000101A0
		internal object Value
		{
			get
			{
				if (this.IsNull)
				{
					return DBNull.Value;
				}
				switch (this._type)
				{
				case SqlBuffer.StorageType.Empty:
					return DBNull.Value;
				case SqlBuffer.StorageType.Boolean:
					if (!this.Boolean)
					{
						return SqlBuffer.s_cachedFalseObject;
					}
					return SqlBuffer.s_cachedTrueObject;
				case SqlBuffer.StorageType.Byte:
					return this.Byte;
				case SqlBuffer.StorageType.DateTime:
					return this.DateTime;
				case SqlBuffer.StorageType.Decimal:
					return this.Decimal;
				case SqlBuffer.StorageType.Double:
					return this.Double;
				case SqlBuffer.StorageType.Int16:
					return this.Int16;
				case SqlBuffer.StorageType.Int32:
					return this.Int32;
				case SqlBuffer.StorageType.Int64:
					return this.Int64;
				case SqlBuffer.StorageType.Guid:
					return this.Guid;
				case SqlBuffer.StorageType.Money:
					return this.Decimal;
				case SqlBuffer.StorageType.Single:
					return this.Single;
				case SqlBuffer.StorageType.String:
					return this.String;
				case SqlBuffer.StorageType.SqlBinary:
					return this.ByteArray;
				case SqlBuffer.StorageType.SqlCachedBuffer:
					return ((SqlCachedBuffer)this._object).ToString();
				case SqlBuffer.StorageType.SqlGuid:
					return this.Guid;
				case SqlBuffer.StorageType.SqlXml:
				{
					SqlXml sqlXml = (SqlXml)this._object;
					return sqlXml.Value;
				}
				case SqlBuffer.StorageType.Date:
					return this.DateTime;
				case SqlBuffer.StorageType.DateTime2:
					return this.DateTime;
				case SqlBuffer.StorageType.DateTimeOffset:
					return this.DateTimeOffset;
				case SqlBuffer.StorageType.Time:
					return this.Time;
				default:
					return null;
				}
			}
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x00012124 File Offset: 0x00010324
		internal Type GetTypeFromStorageType(bool isSqlType)
		{
			if (isSqlType)
			{
				switch (this._type)
				{
				case SqlBuffer.StorageType.Empty:
					return null;
				case SqlBuffer.StorageType.Boolean:
					return typeof(SqlBoolean);
				case SqlBuffer.StorageType.Byte:
					return typeof(SqlByte);
				case SqlBuffer.StorageType.DateTime:
					return typeof(SqlDateTime);
				case SqlBuffer.StorageType.Decimal:
					return typeof(SqlDecimal);
				case SqlBuffer.StorageType.Double:
					return typeof(SqlDouble);
				case SqlBuffer.StorageType.Int16:
					return typeof(SqlInt16);
				case SqlBuffer.StorageType.Int32:
					return typeof(SqlInt32);
				case SqlBuffer.StorageType.Int64:
					return typeof(SqlInt64);
				case SqlBuffer.StorageType.Guid:
					return typeof(SqlGuid);
				case SqlBuffer.StorageType.Money:
					return typeof(SqlMoney);
				case SqlBuffer.StorageType.Single:
					return typeof(SqlSingle);
				case SqlBuffer.StorageType.String:
					return typeof(SqlString);
				case SqlBuffer.StorageType.SqlBinary:
					return typeof(object);
				case SqlBuffer.StorageType.SqlCachedBuffer:
					return typeof(SqlString);
				case SqlBuffer.StorageType.SqlGuid:
					return typeof(SqlGuid);
				case SqlBuffer.StorageType.SqlXml:
					return typeof(SqlXml);
				}
			}
			else
			{
				switch (this._type)
				{
				case SqlBuffer.StorageType.Empty:
					return null;
				case SqlBuffer.StorageType.Boolean:
					return typeof(bool);
				case SqlBuffer.StorageType.Byte:
					return typeof(byte);
				case SqlBuffer.StorageType.DateTime:
					return typeof(DateTime);
				case SqlBuffer.StorageType.Decimal:
					return typeof(decimal);
				case SqlBuffer.StorageType.Double:
					return typeof(double);
				case SqlBuffer.StorageType.Int16:
					return typeof(short);
				case SqlBuffer.StorageType.Int32:
					return typeof(int);
				case SqlBuffer.StorageType.Int64:
					return typeof(long);
				case SqlBuffer.StorageType.Guid:
					return typeof(Guid);
				case SqlBuffer.StorageType.Money:
					return typeof(decimal);
				case SqlBuffer.StorageType.Single:
					return typeof(float);
				case SqlBuffer.StorageType.String:
					return typeof(string);
				case SqlBuffer.StorageType.SqlBinary:
					return typeof(byte[]);
				case SqlBuffer.StorageType.SqlCachedBuffer:
					return typeof(string);
				case SqlBuffer.StorageType.SqlGuid:
					return typeof(Guid);
				case SqlBuffer.StorageType.SqlXml:
					return typeof(string);
				case SqlBuffer.StorageType.Date:
					return typeof(DateTime);
				case SqlBuffer.StorageType.DateTime2:
					return typeof(DateTime);
				case SqlBuffer.StorageType.DateTimeOffset:
					return typeof(DateTimeOffset);
				}
			}
			return null;
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x00012378 File Offset: 0x00010578
		internal static SqlBuffer[] CreateBufferArray(int length)
		{
			SqlBuffer[] array = new SqlBuffer[length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new SqlBuffer();
			}
			return array;
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x000123A4 File Offset: 0x000105A4
		internal static SqlBuffer[] CloneBufferArray(SqlBuffer[] values)
		{
			SqlBuffer[] array = new SqlBuffer[values.Length];
			for (int i = 0; i < values.Length; i++)
			{
				array[i] = new SqlBuffer(values[i]);
			}
			return array;
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x000123D4 File Offset: 0x000105D4
		internal static void Clear(SqlBuffer[] values)
		{
			if (values != null)
			{
				for (int i = 0; i < values.Length; i++)
				{
					values[i].Clear();
				}
			}
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x000123FA File Offset: 0x000105FA
		internal void Clear()
		{
			this._isNull = false;
			this._type = SqlBuffer.StorageType.Empty;
			this._object = null;
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x00012411 File Offset: 0x00010611
		internal void SetToDateTime(int daypart, int timepart)
		{
			this._value._dateTimeInfo._daypart = daypart;
			this._value._dateTimeInfo._timepart = timepart;
			this._type = SqlBuffer.StorageType.DateTime;
			this._isNull = false;
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x00012444 File Offset: 0x00010644
		internal void SetToDecimal(byte precision, byte scale, bool positive, int[] bits)
		{
			this._value._numericInfo._precision = precision;
			this._value._numericInfo._scale = scale;
			this._value._numericInfo._positive = positive;
			this._value._numericInfo._data1 = bits[0];
			this._value._numericInfo._data2 = bits[1];
			this._value._numericInfo._data3 = bits[2];
			this._value._numericInfo._data4 = bits[3];
			this._type = SqlBuffer.StorageType.Decimal;
			this._isNull = false;
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x000124E2 File Offset: 0x000106E2
		internal void SetToMoney(long value)
		{
			this._value._int64 = value;
			this._type = SqlBuffer.StorageType.Money;
			this._isNull = false;
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x000124FF File Offset: 0x000106FF
		internal void SetToNullOfType(SqlBuffer.StorageType storageType)
		{
			this._type = storageType;
			this._isNull = true;
			this._object = null;
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x00012516 File Offset: 0x00010716
		internal void SetToString(string value)
		{
			this._object = value;
			this._type = SqlBuffer.StorageType.String;
			this._isNull = false;
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x0001252E File Offset: 0x0001072E
		internal void SetToDate(ReadOnlySpan<byte> bytes)
		{
			this._type = SqlBuffer.StorageType.Date;
			this._value._int32 = SqlBuffer.GetDateFromByteArray(bytes);
			this._isNull = false;
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x00012550 File Offset: 0x00010750
		internal void SetToTime(ReadOnlySpan<byte> bytes, byte scale, byte denormalizedScale)
		{
			this._type = SqlBuffer.StorageType.Time;
			SqlBuffer.FillInTimeInfo(ref this._value._timeInfo, bytes, scale, denormalizedScale);
			this._isNull = false;
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x00012574 File Offset: 0x00010774
		internal void SetToTime(TimeSpan timeSpan, byte scale)
		{
			this._type = SqlBuffer.StorageType.Time;
			this._value._timeInfo._ticks = timeSpan.Ticks;
			this._value._timeInfo._scale = scale;
			this._isNull = false;
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x000125B0 File Offset: 0x000107B0
		internal void SetToDateTime2(ReadOnlySpan<byte> bytes, byte scale, byte denormalizedScale)
		{
			int length = bytes.Length;
			this._type = SqlBuffer.StorageType.DateTime2;
			SqlBuffer.FillInTimeInfo(ref this._value._dateTime2Info._timeInfo, bytes.Slice(0, length - 3), scale, denormalizedScale);
			this._value._dateTime2Info._date = SqlBuffer.GetDateFromByteArray(bytes.Slice(length - 3));
			this._isNull = false;
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x00012618 File Offset: 0x00010818
		internal unsafe void SetToDateTimeOffset(ReadOnlySpan<byte> bytes, byte scale, byte denormalizedScale)
		{
			int length = bytes.Length;
			this._type = SqlBuffer.StorageType.DateTimeOffset;
			SqlBuffer.FillInTimeInfo(ref this._value._dateTimeOffsetInfo._dateTime2Info._timeInfo, bytes.Slice(0, length - 5), scale, denormalizedScale);
			this._value._dateTimeOffsetInfo._dateTime2Info._date = SqlBuffer.GetDateFromByteArray(bytes.Slice(length - 5));
			this._value._dateTimeOffsetInfo._offset = (short)((int)(*bytes[length - 2]) + ((int)(*bytes[length - 1]) << 8));
			this._isNull = false;
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x000126B4 File Offset: 0x000108B4
		internal void SetToDateTimeOffset(DateTimeOffset dateTimeOffset, byte scale)
		{
			this._type = SqlBuffer.StorageType.DateTimeOffset;
			DateTime utcDateTime = dateTimeOffset.UtcDateTime;
			this._value._dateTimeOffsetInfo._dateTime2Info._timeInfo._ticks = utcDateTime.TimeOfDay.Ticks;
			this._value._dateTimeOffsetInfo._dateTime2Info._timeInfo._scale = scale;
			this._value._dateTimeOffsetInfo._dateTime2Info._date = utcDateTime.Subtract(DateTime.MinValue).Days;
			this._value._dateTimeOffsetInfo._offset = (short)dateTimeOffset.Offset.TotalMinutes;
			this._isNull = false;
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x00012768 File Offset: 0x00010968
		private unsafe static void FillInTimeInfo(ref SqlBuffer.TimeInfo timeInfo, ReadOnlySpan<byte> timeBytes, byte scale, byte denormalizedScale)
		{
			int length = timeBytes.Length;
			long num = (long)((ulong)(*timeBytes[0]) + ((ulong)(*timeBytes[1]) << 8) + ((ulong)(*timeBytes[2]) << 16));
			if (length > 3)
			{
				num += (long)((long)((ulong)(*timeBytes[3])) << 24);
			}
			if (length > 4)
			{
				num += (long)((long)((ulong)(*timeBytes[4])) << 32);
			}
			timeInfo._ticks = num * TdsEnums.TICKS_FROM_SCALE[(int)scale];
			timeInfo._scale = denormalizedScale;
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x000127E4 File Offset: 0x000109E4
		private unsafe static int GetDateFromByteArray(ReadOnlySpan<byte> buf)
		{
			byte b = *buf[2];
			return (int)(*buf[0]) + ((int)(*buf[1]) << 8) + ((int)b << 16);
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x00012815 File Offset: 0x00010A15
		private void ThrowIfNull()
		{
			if (this.IsNull)
			{
				throw new SqlNullValueException();
			}
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x00012825 File Offset: 0x00010A25
		internal T ByteAs<T>()
		{
			this.ThrowIfNull();
			return (T)((object)this._value._byte);
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x00012842 File Offset: 0x00010A42
		internal T BooleanAs<T>()
		{
			this.ThrowIfNull();
			return (T)((object)this._value._boolean);
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x0001285F File Offset: 0x00010A5F
		internal T Int32As<T>()
		{
			this.ThrowIfNull();
			return (T)((object)this._value._int32);
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x0001287C File Offset: 0x00010A7C
		internal T Int16As<T>()
		{
			this.ThrowIfNull();
			return (T)((object)this._value._int16);
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x00012899 File Offset: 0x00010A99
		internal T Int64As<T>()
		{
			this.ThrowIfNull();
			return (T)((object)this._value._int64);
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x000128B6 File Offset: 0x00010AB6
		internal T DoubleAs<T>()
		{
			this.ThrowIfNull();
			return (T)((object)this._value._double);
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x000128D3 File Offset: 0x00010AD3
		internal T SingleAs<T>()
		{
			this.ThrowIfNull();
			return (T)((object)this._value._single);
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x000128F0 File Offset: 0x00010AF0
		internal void SetToDate(DateTime date)
		{
			this._type = SqlBuffer.StorageType.Date;
			this._value._int32 = date.Subtract(DateTime.MinValue).Days;
			this._isNull = false;
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x0001292C File Offset: 0x00010B2C
		internal void SetToDateTime2(DateTime dateTime, byte scale)
		{
			this._type = SqlBuffer.StorageType.DateTime2;
			this._value._dateTime2Info._timeInfo._ticks = dateTime.TimeOfDay.Ticks;
			this._value._dateTime2Info._timeInfo._scale = scale;
			this._value._dateTime2Info._date = dateTime.Subtract(DateTime.MinValue).Days;
			this._isNull = false;
		}

		// Token: 0x04000117 RID: 279
		private bool _isNull;

		// Token: 0x04000118 RID: 280
		private SqlBuffer.StorageType _type;

		// Token: 0x04000119 RID: 281
		private SqlBuffer.Storage _value;

		// Token: 0x0400011A RID: 282
		private object _object;

		// Token: 0x0400011B RID: 283
		private static readonly string[] s_sql2008DateTimeOffsetFormatByScale = new string[] { "yyyy-MM-dd HH:mm:ss zzz", "yyyy-MM-dd HH:mm:ss.f zzz", "yyyy-MM-dd HH:mm:ss.ff zzz", "yyyy-MM-dd HH:mm:ss.fff zzz", "yyyy-MM-dd HH:mm:ss.ffff zzz", "yyyy-MM-dd HH:mm:ss.fffff zzz", "yyyy-MM-dd HH:mm:ss.ffffff zzz", "yyyy-MM-dd HH:mm:ss.fffffff zzz" };

		// Token: 0x0400011C RID: 284
		private static readonly string[] s_sql2008DateTime2FormatByScale = new string[] { "yyyy-MM-dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss.f", "yyyy-MM-dd HH:mm:ss.ff", "yyyy-MM-dd HH:mm:ss.fff", "yyyy-MM-dd HH:mm:ss.ffff", "yyyy-MM-dd HH:mm:ss.fffff", "yyyy-MM-dd HH:mm:ss.ffffff", "yyyy-MM-dd HH:mm:ss.fffffff" };

		// Token: 0x0400011D RID: 285
		private static readonly string[] s_sql2008TimeFormatByScale = new string[] { "HH:mm:ss", "HH:mm:ss.f", "HH:mm:ss.ff", "HH:mm:ss.fff", "HH:mm:ss.ffff", "HH:mm:ss.fffff", "HH:mm:ss.ffffff", "HH:mm:ss.fffffff" };

		// Token: 0x0400011E RID: 286
		private static readonly object s_cachedTrueObject = true;

		// Token: 0x0400011F RID: 287
		private static readonly object s_cachedFalseObject = false;

		// Token: 0x020001B2 RID: 434
		internal enum StorageType
		{
			// Token: 0x040012DC RID: 4828
			Empty,
			// Token: 0x040012DD RID: 4829
			Boolean,
			// Token: 0x040012DE RID: 4830
			Byte,
			// Token: 0x040012DF RID: 4831
			DateTime,
			// Token: 0x040012E0 RID: 4832
			Decimal,
			// Token: 0x040012E1 RID: 4833
			Double,
			// Token: 0x040012E2 RID: 4834
			Int16,
			// Token: 0x040012E3 RID: 4835
			Int32,
			// Token: 0x040012E4 RID: 4836
			Int64,
			// Token: 0x040012E5 RID: 4837
			Guid,
			// Token: 0x040012E6 RID: 4838
			Money,
			// Token: 0x040012E7 RID: 4839
			Single,
			// Token: 0x040012E8 RID: 4840
			String,
			// Token: 0x040012E9 RID: 4841
			SqlBinary,
			// Token: 0x040012EA RID: 4842
			SqlCachedBuffer,
			// Token: 0x040012EB RID: 4843
			SqlGuid,
			// Token: 0x040012EC RID: 4844
			SqlXml,
			// Token: 0x040012ED RID: 4845
			Date,
			// Token: 0x040012EE RID: 4846
			DateTime2,
			// Token: 0x040012EF RID: 4847
			DateTimeOffset,
			// Token: 0x040012F0 RID: 4848
			Time
		}

		// Token: 0x020001B3 RID: 435
		internal struct DateTimeInfo
		{
			// Token: 0x040012F1 RID: 4849
			internal int _daypart;

			// Token: 0x040012F2 RID: 4850
			internal int _timepart;
		}

		// Token: 0x020001B4 RID: 436
		internal struct NumericInfo
		{
			// Token: 0x040012F3 RID: 4851
			internal int _data1;

			// Token: 0x040012F4 RID: 4852
			internal int _data2;

			// Token: 0x040012F5 RID: 4853
			internal int _data3;

			// Token: 0x040012F6 RID: 4854
			internal int _data4;

			// Token: 0x040012F7 RID: 4855
			internal byte _precision;

			// Token: 0x040012F8 RID: 4856
			internal byte _scale;

			// Token: 0x040012F9 RID: 4857
			internal bool _positive;
		}

		// Token: 0x020001B5 RID: 437
		internal struct TimeInfo
		{
			// Token: 0x040012FA RID: 4858
			internal long _ticks;

			// Token: 0x040012FB RID: 4859
			internal byte _scale;
		}

		// Token: 0x020001B6 RID: 438
		internal struct DateTime2Info
		{
			// Token: 0x040012FC RID: 4860
			internal int _date;

			// Token: 0x040012FD RID: 4861
			internal SqlBuffer.TimeInfo _timeInfo;
		}

		// Token: 0x020001B7 RID: 439
		internal struct DateTimeOffsetInfo
		{
			// Token: 0x040012FE RID: 4862
			internal SqlBuffer.DateTime2Info _dateTime2Info;

			// Token: 0x040012FF RID: 4863
			internal short _offset;
		}

		// Token: 0x020001B8 RID: 440
		[StructLayout(LayoutKind.Explicit)]
		internal struct Storage
		{
			// Token: 0x04001300 RID: 4864
			[FieldOffset(0)]
			internal bool _boolean;

			// Token: 0x04001301 RID: 4865
			[FieldOffset(0)]
			internal byte _byte;

			// Token: 0x04001302 RID: 4866
			[FieldOffset(0)]
			internal SqlBuffer.DateTimeInfo _dateTimeInfo;

			// Token: 0x04001303 RID: 4867
			[FieldOffset(0)]
			internal double _double;

			// Token: 0x04001304 RID: 4868
			[FieldOffset(0)]
			internal SqlBuffer.NumericInfo _numericInfo;

			// Token: 0x04001305 RID: 4869
			[FieldOffset(0)]
			internal short _int16;

			// Token: 0x04001306 RID: 4870
			[FieldOffset(0)]
			internal int _int32;

			// Token: 0x04001307 RID: 4871
			[FieldOffset(0)]
			internal long _int64;

			// Token: 0x04001308 RID: 4872
			[FieldOffset(0)]
			internal Guid _guid;

			// Token: 0x04001309 RID: 4873
			[FieldOffset(0)]
			internal float _single;

			// Token: 0x0400130A RID: 4874
			[FieldOffset(0)]
			internal SqlBuffer.TimeInfo _timeInfo;

			// Token: 0x0400130B RID: 4875
			[FieldOffset(0)]
			internal SqlBuffer.DateTime2Info _dateTime2Info;

			// Token: 0x0400130C RID: 4876
			[FieldOffset(0)]
			internal SqlBuffer.DateTimeOffsetInfo _dateTimeOffsetInfo;
		}
	}
}
