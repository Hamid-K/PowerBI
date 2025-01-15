using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.HostIntegration.Common;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x0200084E RID: 2126
	public class Converter
	{
		// Token: 0x17001008 RID: 4104
		// (get) Token: 0x0600436C RID: 17260 RVA: 0x000E26BC File Offset: 0x000E08BC
		// (set) Token: 0x0600436D RID: 17261 RVA: 0x000E26C4 File Offset: 0x000E08C4
		public string SqlToDb2DateMask
		{
			get
			{
				return this._sqlToDb2DateMask;
			}
			set
			{
				this._sqlToDb2DateMask = value;
			}
		}

		// Token: 0x17001009 RID: 4105
		// (get) Token: 0x0600436E RID: 17262 RVA: 0x000E26CD File Offset: 0x000E08CD
		// (set) Token: 0x0600436F RID: 17263 RVA: 0x000E26D5 File Offset: 0x000E08D5
		public string SqlToDb2TimeMask
		{
			get
			{
				return this._sqlToDb2TimeMask;
			}
			set
			{
				this._sqlToDb2TimeMask = value;
			}
		}

		// Token: 0x1700100A RID: 4106
		// (get) Token: 0x06004370 RID: 17264 RVA: 0x000E26DE File Offset: 0x000E08DE
		// (set) Token: 0x06004371 RID: 17265 RVA: 0x000E26E6 File Offset: 0x000E08E6
		public string SqlToDb2DatetimeMask
		{
			get
			{
				return this._sqlToDb2DatetimeMask;
			}
			set
			{
				this._sqlToDb2DatetimeMask = value;
			}
		}

		// Token: 0x1700100B RID: 4107
		// (get) Token: 0x06004372 RID: 17266 RVA: 0x000E26EF File Offset: 0x000E08EF
		// (set) Token: 0x06004373 RID: 17267 RVA: 0x000E26F8 File Offset: 0x000E08F8
		public List<DateTimeMask> DateTimeMasks
		{
			get
			{
				return this._dateTimeMasks;
			}
			set
			{
				if (this._dateTimeMasks != value)
				{
					this._db2ToSqlDateMasks.Clear();
					this._db2ToSqlTimeMasks.Clear();
					this._db2ToSqlDatetimeMasks.Clear();
					foreach (DateTimeMask dateTimeMask in value)
					{
						if (dateTimeMask.Db2ToSql)
						{
							if (dateTimeMask.Type == DateTimeMaskType.Date)
							{
								this._db2ToSqlDateMasks.Add(dateTimeMask.Mask);
							}
							else if (dateTimeMask.Type == DateTimeMaskType.Time)
							{
								this._db2ToSqlTimeMasks.Add(dateTimeMask.Mask);
							}
							else if (dateTimeMask.Type == DateTimeMaskType.DateTime)
							{
								this._db2ToSqlDatetimeMasks.Add(dateTimeMask.Mask);
							}
						}
						else if (dateTimeMask.Type == DateTimeMaskType.Date)
						{
							this._sqlToDb2DateMask = dateTimeMask.Mask;
						}
						else if (dateTimeMask.Type == DateTimeMaskType.Time)
						{
							this._sqlToDb2TimeMask = dateTimeMask.Mask;
						}
						else if (dateTimeMask.Type == DateTimeMaskType.DateTime)
						{
							this._sqlToDb2DatetimeMask = dateTimeMask.Mask;
						}
					}
					this._dateMaskLength = 0;
					this._dateTimeMasks = value;
				}
			}
		}

		// Token: 0x1700100C RID: 4108
		// (get) Token: 0x06004374 RID: 17268 RVA: 0x000E2820 File Offset: 0x000E0A20
		private BasePrimitiveConverter PrimitiveConverter
		{
			get
			{
				if (this.IsAs400)
				{
					if (this._systemiConverter == null)
					{
						this._systemiConverter = new SystemIPrimitiveConverter(1208);
						if (this._ccsid != null)
						{
							this._systemiConverter.SetCodePage(this._ccsid._ccsiddbc);
							this._systemiConverter.SetCodePage(this._ccsid._ccsidmbc);
							this._systemiConverter.SetCodePage(this._ccsid._ccsidsbc);
						}
					}
					return this._systemiConverter;
				}
				return this._primitiveConverter;
			}
		}

		// Token: 0x1700100D RID: 4109
		// (get) Token: 0x06004375 RID: 17269 RVA: 0x000E28A4 File Offset: 0x000E0AA4
		// (set) Token: 0x06004376 RID: 17270 RVA: 0x000E28AC File Offset: 0x000E0AAC
		public bool IsAs400 { get; set; }

		// Token: 0x06004377 RID: 17271 RVA: 0x000E28B5 File Offset: 0x000E0AB5
		public Converter(List<DateTimeMask> dateTimeMasks)
			: this(dateTimeMasks, null)
		{
		}

		// Token: 0x06004378 RID: 17272 RVA: 0x000E28C0 File Offset: 0x000E0AC0
		public Converter(List<DateTimeMask> dateTimeMasks, object tracePoint)
		{
			this.DateTimeMasks = dateTimeMasks;
			this.IsAs400 = false;
			this._systemiConverter = null;
			try
			{
				this._primitiveConverter = new BasePrimitiveConverter(1208);
			}
			catch (Exception ex)
			{
				Logger.LogException(this._tracePoint, 0, "Failed to create primitive converter.", ex);
				throw;
			}
		}

		// Token: 0x06004379 RID: 17273 RVA: 0x000E2990 File Offset: 0x000E0B90
		private void EnsurePrimitiveConverterEncoding(int encoding)
		{
			if (this.lastEncoding != encoding)
			{
				this.PrimitiveConverter.SetCodePage(encoding);
				this.lastEncoding = encoding;
			}
		}

		// Token: 0x0600437A RID: 17274 RVA: 0x000E29AE File Offset: 0x000E0BAE
		public static short ToInt16(byte[] bytes, int index)
		{
			return Converter.ToInt16(bytes, index, EndianType.BigEndian);
		}

		// Token: 0x0600437B RID: 17275 RVA: 0x000E29B8 File Offset: 0x000E0BB8
		public static ushort ToUInt16(byte[] bytes, int index, EndianType endianType)
		{
			if (endianType == EndianType.BigEndian)
			{
				return Converter.BigEndianBytesToUInt16(bytes, index);
			}
			if (endianType != EndianType.LittleEndian)
			{
				throw new ArgumentException();
			}
			return Converter.LittleEndianBytesToUInt16(bytes, index);
		}

		// Token: 0x0600437C RID: 17276 RVA: 0x000E29D9 File Offset: 0x000E0BD9
		public static short ToInt16(byte[] bytes, int index, EndianType endianType)
		{
			if (endianType == EndianType.BigEndian)
			{
				return Converter.BigEndianBytesToInt16(bytes, index);
			}
			if (endianType != EndianType.LittleEndian)
			{
				throw new ArgumentException();
			}
			return Converter.LittleEndianBytesToInt16(bytes, index);
		}

		// Token: 0x0600437D RID: 17277 RVA: 0x000E29FA File Offset: 0x000E0BFA
		public static int ToInt32(byte[] bytes, int index)
		{
			return Converter.ToInt32(bytes, index, EndianType.BigEndian);
		}

		// Token: 0x0600437E RID: 17278 RVA: 0x000E2A04 File Offset: 0x000E0C04
		public static int ToInt32(byte[] bytes, int index, EndianType endianType)
		{
			if (endianType == EndianType.BigEndian)
			{
				return Converter.BigEndianBytesToInt32(bytes, index);
			}
			if (endianType != EndianType.LittleEndian)
			{
				throw new ArgumentException();
			}
			return Converter.LittleEndianBytesToInt32(bytes, index);
		}

		// Token: 0x0600437F RID: 17279 RVA: 0x000E2A25 File Offset: 0x000E0C25
		public static long ToInt48(byte[] bytes, int index)
		{
			return Converter.ToInt48(bytes, index, EndianType.BigEndian);
		}

		// Token: 0x06004380 RID: 17280 RVA: 0x000E2A2F File Offset: 0x000E0C2F
		public static long ToInt48(byte[] bytes, int index, EndianType endianType)
		{
			if (endianType == EndianType.BigEndian)
			{
				return Converter.BigEndianBytesToInt48(bytes, index);
			}
			if (endianType != EndianType.LittleEndian)
			{
				throw new ArgumentException();
			}
			return Converter.LittleEndianBytesToInt48(bytes, index);
		}

		// Token: 0x06004381 RID: 17281 RVA: 0x000E2A50 File Offset: 0x000E0C50
		public static long ToInt64(byte[] bytes, int index)
		{
			return Converter.ToInt64(bytes, index, EndianType.BigEndian);
		}

		// Token: 0x06004382 RID: 17282 RVA: 0x000E2A5A File Offset: 0x000E0C5A
		public static long ToInt64(byte[] bytes, int index, EndianType endianType)
		{
			if (endianType == EndianType.BigEndian)
			{
				return Converter.BigEndianBytesToInt64(bytes, index);
			}
			if (endianType != EndianType.LittleEndian)
			{
				throw new ArgumentException();
			}
			return Converter.LittleEndianBytesToInt64(bytes, index);
		}

		// Token: 0x06004383 RID: 17283 RVA: 0x000E2A7B File Offset: 0x000E0C7B
		public static short BigEndianBytesToInt16(byte[] bytes, int index)
		{
			return (short)(((int)bytes[index] << 8) + (int)bytes[index + 1]);
		}

		// Token: 0x06004384 RID: 17284 RVA: 0x000E2A89 File Offset: 0x000E0C89
		public static ushort BigEndianBytesToUInt16(byte[] bytes, int index)
		{
			return (ushort)(((int)bytes[index] << 8) + (int)bytes[index + 1]);
		}

		// Token: 0x06004385 RID: 17285 RVA: 0x000E2A97 File Offset: 0x000E0C97
		public static short LittleEndianBytesToInt16(byte[] bytes, int index)
		{
			return (short)((int)bytes[index] + ((int)bytes[index + 1] << 8));
		}

		// Token: 0x06004386 RID: 17286 RVA: 0x000E2AA5 File Offset: 0x000E0CA5
		public static ushort LittleEndianBytesToUInt16(byte[] bytes, int index)
		{
			return (ushort)((int)bytes[index] + ((int)bytes[index + 1] << 8));
		}

		// Token: 0x06004387 RID: 17287 RVA: 0x000E2AB3 File Offset: 0x000E0CB3
		public static int BigEndianBytesToInt32(byte[] bytes, int index)
		{
			return ((int)bytes[index] << 24) + ((int)bytes[index + 1] << 16) + ((int)bytes[index + 2] << 8) + (int)bytes[index + 3];
		}

		// Token: 0x06004388 RID: 17288 RVA: 0x000E2AD2 File Offset: 0x000E0CD2
		public static int LittleEndianBytesToInt32(byte[] bytes, int index)
		{
			return (int)bytes[index] + ((int)bytes[index + 1] << 8) + ((int)bytes[index + 2] << 16) + ((int)bytes[index + 3] << 24);
		}

		// Token: 0x06004389 RID: 17289 RVA: 0x000E2AF4 File Offset: 0x000E0CF4
		public static long BigEndianBytesToInt48(byte[] bytes, int index)
		{
			return (long)((((ulong)bytes[index + 2] & 255UL) << 40) + (((ulong)bytes[index + 3] & 255UL) << 32) + (((ulong)bytes[index + 4] & 255UL) << 24) + (((ulong)bytes[index + 5] & 255UL) << 16) + (((ulong)bytes[index + 6] & 255UL) << 8) + ((ulong)bytes[index + 7] & 255UL));
		}

		// Token: 0x0600438A RID: 17290 RVA: 0x000E2B64 File Offset: 0x000E0D64
		public static long LittleEndianBytesToInt48(byte[] bytes, int index)
		{
			return (long)(((ulong)bytes[index] & 255UL) + (((ulong)bytes[index + 1] & 255UL) << 8) + (((ulong)bytes[index + 2] & 255UL) << 16) + (((ulong)bytes[index + 3] & 255UL) << 24) + (((ulong)bytes[index + 4] & 255UL) << 32) + (((ulong)bytes[index + 5] & 255UL) << 40));
		}

		// Token: 0x0600438B RID: 17291 RVA: 0x000E2BD0 File Offset: 0x000E0DD0
		public static long BigEndianBytesToInt64(byte[] bytes, int index)
		{
			return (long)((((ulong)bytes[index] & 255UL) << 56) + (((ulong)bytes[index + 1] & 255UL) << 48) + (((ulong)bytes[index + 2] & 255UL) << 40) + (((ulong)bytes[index + 3] & 255UL) << 32) + (((ulong)bytes[index + 4] & 255UL) << 24) + (((ulong)bytes[index + 5] & 255UL) << 16) + (((ulong)bytes[index + 6] & 255UL) << 8) + ((ulong)bytes[index + 7] & 255UL));
		}

		// Token: 0x0600438C RID: 17292 RVA: 0x000E2C60 File Offset: 0x000E0E60
		public static long LittleEndianBytesToInt64(byte[] bytes, int index)
		{
			return (long)(((ulong)bytes[index] & 255UL) + (((ulong)bytes[index + 1] & 255UL) << 8) + (((ulong)bytes[index + 2] & 255UL) << 16) + (((ulong)bytes[index + 3] & 255UL) << 24) + (((ulong)bytes[index + 4] & 255UL) << 32) + (((ulong)bytes[index + 5] & 255UL) << 40) + (((ulong)bytes[index + 6] & 255UL) << 48) + (((ulong)bytes[index + 7] & 255UL) << 56));
		}

		// Token: 0x0600438D RID: 17293 RVA: 0x000E2CEE File Offset: 0x000E0EEE
		public static void IntToBytes(long val, byte[] bytes, int length, int index)
		{
			Converter.IntToBytes(val, bytes, length, index, EndianType.LittleEndian);
		}

		// Token: 0x0600438E RID: 17294 RVA: 0x000E2CFC File Offset: 0x000E0EFC
		public static void IntToBytes(long val, byte[] bytes, int length, int index, EndianType endianType)
		{
			int num = index + length - 1;
			int num2 = (length - 1) * 8;
			for (int i = 0; i < length; i++)
			{
				if (endianType == EndianType.LittleEndian)
				{
					bytes[num--] = (byte)(BitUtils.UnsignedRightShift(val, num2) & 255L);
				}
				else
				{
					bytes[index++] = (byte)(BitUtils.UnsignedRightShift(val, num2) & 255L);
				}
				num2 -= 8;
			}
		}

		// Token: 0x0600438F RID: 17295 RVA: 0x000E2D5C File Offset: 0x000E0F5C
		public static long BigEndianBytesToUnsignedInteger(byte[] bytes, int index, int length)
		{
			long num = 0L;
			int num2 = index;
			int num3 = (length - 1) * 8;
			for (int i = 0; i < length; i++)
			{
				num += (long)((long)((ulong)bytes[index + num2++] & 255UL) << num3);
				num3 -= 8;
			}
			return num;
		}

		// Token: 0x06004390 RID: 17296 RVA: 0x000E2DA0 File Offset: 0x000E0FA0
		public static long LittleEndianBytesToUnsignedInteger(byte[] bytes, int index, int length)
		{
			long num = 0L;
			int num2 = index;
			int num3 = 0;
			for (int i = 0; i < length; i++)
			{
				num += (long)((long)((ulong)bytes[index + num2++] & 255UL) << num3);
				num3 += 8;
			}
			return num;
		}

		// Token: 0x06004391 RID: 17297 RVA: 0x000E2DDE File Offset: 0x000E0FDE
		internal void PackDecimal(decimal value, byte[] buffer, int precision, int scale)
		{
			this.PackData(DrdaType.DECIMAL, value, buffer, precision, scale);
		}

		// Token: 0x06004392 RID: 17298 RVA: 0x000E2DF4 File Offset: 0x000E0FF4
		internal void PackDecimalFloat(decimal value, byte[] buffer, int length)
		{
			this._typeEncoding.CvtEncoding = Convert.ToInt32((length == 8) ? ConvertedDataType.CEDAR_DECIMAL_FLOAT_64 : ConvertedDataType.CEDAR_DECIMAL_FLOAT_128);
			int num;
			this.PrimitiveConverter.SizeOfRemoteType(DataType.Decimal, this._typeEncoding, out num);
			this.PrimitiveConverter.PackDecimal(value, ref buffer[0], ref this.packedDataLength, num, this._typeEncoding);
		}

		// Token: 0x06004393 RID: 17299 RVA: 0x000E2E55 File Offset: 0x000E1055
		internal void PackFloat(float value, byte[] buffer, string typDefnam)
		{
			this.PackData(DrdaType.FLOAT, value, buffer, 0, 0, typDefnam);
		}

		// Token: 0x06004394 RID: 17300 RVA: 0x000E2E69 File Offset: 0x000E1069
		internal void PackDouble(double value, byte[] buffer, string typDefnam)
		{
			this.PackData(DrdaType.DOUBLE, value, buffer, 0, 0, typDefnam);
		}

		// Token: 0x06004395 RID: 17301 RVA: 0x000E2E80 File Offset: 0x000E1080
		internal void PackDate(DateTime ival, int codePage, byte[] buffer)
		{
			bool flag = false;
			this.EnsurePrimitiveConverterEncoding(codePage);
			this.SetupEncoding(DrdaType.DATE, 0, 0);
			foreach (DateTimeMask dateTimeMask in this._dateTimeMasks)
			{
				if (!dateTimeMask.Db2ToSql && dateTimeMask.Type == DateTimeMaskType.Date)
				{
					this.PrimitiveConverter.PackEditedDateTime(ival, ref buffer[0], ref this.packedDataLength, dateTimeMask.Mask.Length, dateTimeMask.Mask);
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				this.PrimitiveConverter.PackDate(ival, ref buffer[0], ref this.packedDataLength, this.resultLen, this._typeEncoding);
			}
		}

		// Token: 0x06004396 RID: 17302 RVA: 0x000E2F48 File Offset: 0x000E1148
		internal void PackTime(TimeSpan ival, int codePage, byte[] buffer)
		{
			bool flag = false;
			this.EnsurePrimitiveConverterEncoding(codePage);
			this.SetupEncoding(DrdaType.TIME, 0, 0);
			foreach (DateTimeMask dateTimeMask in this._dateTimeMasks)
			{
				if (!dateTimeMask.Db2ToSql && dateTimeMask.Type == DateTimeMaskType.Time)
				{
					this.PrimitiveConverter.PackEditedTimeSpan(ival, ref buffer[0], ref this.packedDataLength, dateTimeMask.Mask.Length, dateTimeMask.Mask);
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				this.PrimitiveConverter.PackTime(ival, ref buffer[0], ref this.packedDataLength, this.resultLen, this._typeEncoding);
			}
		}

		// Token: 0x06004397 RID: 17303 RVA: 0x000E3010 File Offset: 0x000E1210
		internal void PackTimestamp(DateTime ival, int codePage, byte[] buffer)
		{
			bool flag = false;
			this.EnsurePrimitiveConverterEncoding(codePage);
			this.SetupEncoding(DrdaType.TIMESTAMP, 0, 0);
			foreach (DateTimeMask dateTimeMask in this._dateTimeMasks)
			{
				if (!dateTimeMask.Db2ToSql && dateTimeMask.Type == DateTimeMaskType.DateTime)
				{
					this.PrimitiveConverter.PackEditedDateTime(ival, ref buffer[0], ref this.packedDataLength, dateTimeMask.Mask.Length, dateTimeMask.Mask);
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				this.PrimitiveConverter.PackDate(ival, ref buffer[0], ref this.packedDataLength, this.resultLen, this._typeEncoding);
			}
		}

		// Token: 0x06004398 RID: 17304 RVA: 0x000E30D8 File Offset: 0x000E12D8
		private void SetupEncoding(DrdaType drdaType, int precision, int scale)
		{
			this.SetupEncoding(drdaType, precision, scale, "QTDSQL370");
		}

		// Token: 0x06004399 RID: 17305 RVA: 0x000E30E8 File Offset: 0x000E12E8
		private void SetupEncoding(DrdaType drdaType, int precision, int scale, string typDefnam)
		{
			this.packedDataLength = 0;
			this.resultLen = 0;
			if (drdaType == DrdaType.TIMESTAMP)
			{
				this._typeEncoding.DrdaType = DrdaTypes.DRDA_TYPE_TIMESTAMP;
				this.dataType = DataType.Date;
				return;
			}
			if (drdaType == DrdaType.TIME)
			{
				this._typeEncoding.DrdaType = DrdaTypes.DRDA_TYPE_TIME;
				this.dataType = DataType.Time;
				return;
			}
			if (drdaType == DrdaType.DATE)
			{
				this._typeEncoding.DrdaType = DrdaTypes.DRDA_TYPE_DATE;
				this.dataType = DataType.Date;
				return;
			}
			if (drdaType == DrdaType.FLOAT)
			{
				this._typeEncoding.DrdaType = DrdaTypes.DRDA_TYPE_FLOAT4;
				this.dataType = DataType.Single;
				return;
			}
			if (drdaType == DrdaType.DOUBLE)
			{
				this._typeEncoding.DrdaType = DrdaTypes.DRDA_TYPE_FLOAT8;
				this.dataType = DataType.Double;
				return;
			}
			if (drdaType == DrdaType.DECIMAL)
			{
				this._typeEncoding.DrdaType = DrdaTypes.DRDA_TYPE_DECIMAL;
				this.dataType = DataType.Decimal;
				this._typeEncoding.nPrecision = (short)precision;
				this._typeEncoding.nScale = (short)scale;
				return;
			}
			if (drdaType == DrdaType.ZONEDDECIMAL)
			{
				this._typeEncoding.DrdaType = DrdaTypes.DRDA_TYPE_ZDECIMAL;
				this.dataType = DataType.Decimal;
				this._typeEncoding.nPrecision = (short)precision;
				this._typeEncoding.nScale = (short)scale;
			}
		}

		// Token: 0x0600439A RID: 17306 RVA: 0x000E31EF File Offset: 0x000E13EF
		private void PackData(DrdaType drdaType, object valObj, byte[] outBuf, int precision, int scale)
		{
			this.PackData(drdaType, valObj, outBuf, precision, scale, "QTDSQL370");
		}

		// Token: 0x0600439B RID: 17307 RVA: 0x000E3204 File Offset: 0x000E1404
		private void PackData(DrdaType drdaType, object valObj, byte[] outBuf, int precision, int scale, string typDefnam)
		{
			this.SetupEncoding(drdaType, precision, scale, typDefnam);
			this.PrimitiveConverter.SizeOfRemoteType(this.dataType, this._typeEncoding, out this.resultLen);
			if (drdaType == DrdaType.FLOAT)
			{
				this.PrimitiveConverter.PackFloat((float)valObj, ref outBuf[0], ref this.packedDataLength, this.resultLen, this._typeEncoding);
				return;
			}
			if (drdaType == DrdaType.DOUBLE)
			{
				this.PrimitiveConverter.PackDouble((double)valObj, ref outBuf[0], ref this.packedDataLength, this.resultLen, this._typeEncoding);
				return;
			}
			if (drdaType == DrdaType.DECIMAL)
			{
				this.PrimitiveConverter.PackDecimal((decimal)valObj, ref outBuf[0], ref this.packedDataLength, this.resultLen, this._typeEncoding);
			}
		}

		// Token: 0x0600439C RID: 17308 RVA: 0x000E32CA File Offset: 0x000E14CA
		internal decimal UnpackDecimal(byte[] buffer, int precision, int scale)
		{
			return this.InternalUnpackDecimal(buffer, precision, scale, DrdaType.DECIMAL);
		}

		// Token: 0x0600439D RID: 17309 RVA: 0x000E32D7 File Offset: 0x000E14D7
		internal decimal UnpackZonedDecimal(byte[] buffer, int precision, int scale)
		{
			return this.InternalUnpackDecimal(buffer, precision, scale, DrdaType.ZONEDDECIMAL);
		}

		// Token: 0x0600439E RID: 17310 RVA: 0x000E32E4 File Offset: 0x000E14E4
		internal decimal UnpackDecimalFloat(byte[] buffer)
		{
			decimal num = 0m;
			this._typeEncoding.CvtEncoding = Convert.ToInt32((buffer.Length == 8) ? ConvertedDataType.CEDAR_DECIMAL_FLOAT_64 : ConvertedDataType.CEDAR_DECIMAL_FLOAT_128);
			int num2;
			this.PrimitiveConverter.SizeOfRemoteType(DataType.Decimal, this._typeEncoding, out num2);
			this.PrimitiveConverter.UnpackDecimal(ref buffer[0], ref num, ref this.packedDataLength, num2, this._typeEncoding);
			return num;
		}

		// Token: 0x0600439F RID: 17311 RVA: 0x000E3354 File Offset: 0x000E1554
		private decimal InternalUnpackDecimal(byte[] buffer, int precision, int scale, DrdaType drdaType)
		{
			decimal num = 0.0m;
			int num2 = 0;
			if (precision > 29)
			{
				if (scale > 29)
				{
					throw new ArgumentOutOfRangeException("Invalid decimal scale: " + scale.ToString());
				}
				for (int i = 0; i < buffer.Length - 15; i++)
				{
					if (buffer[i] != 0)
					{
						throw new ArgumentOutOfRangeException(string.Format("Invalid decimal data format. It may be out of the range of .NET decimal value.", Array.Empty<object>()));
					}
				}
				precision = 29;
				num2 = buffer.Length - 15;
			}
			this.SetupEncoding(drdaType, precision, scale);
			this.PrimitiveConverter.SizeOfRemoteType(this.dataType, this._typeEncoding, out this.resultLen);
			this.PrimitiveConverter.UnpackDecimal(ref buffer[num2], ref num, ref this.packedDataLength, this.resultLen, this._typeEncoding);
			return num;
		}

		// Token: 0x060043A0 RID: 17312 RVA: 0x000E3414 File Offset: 0x000E1614
		internal TimeSpan UnpackTime(byte[] buffer, int codePage)
		{
			bool flag = false;
			string empty = string.Empty;
			this.UnpackString(buffer, 0, buffer.Length, codePage, ref empty, true, DrdaTypes.DRDA_TYPE_CHAR);
			return this.UnpackTime(empty, ref flag);
		}

		// Token: 0x060043A1 RID: 17313 RVA: 0x000E3444 File Offset: 0x000E1644
		internal string UnpackTimeDateTimeAsChar(byte[] buffer, int codepage)
		{
			bool flag = false;
			return this.UnpackTimestampDateTimeAsChar(buffer, codepage, ref flag);
		}

		// Token: 0x060043A2 RID: 17314 RVA: 0x000E3460 File Offset: 0x000E1660
		internal DateTime UnpackDate(byte[] buffer, int codePage)
		{
			bool flag = false;
			string empty = string.Empty;
			this.UnpackString(buffer, 0, buffer.Length, codePage, ref empty, true, DrdaTypes.DRDA_TYPE_CHAR);
			return this.UnpackDate(empty, ref flag);
		}

		// Token: 0x060043A3 RID: 17315 RVA: 0x000E3490 File Offset: 0x000E1690
		internal string UnpackDateDateTimeAsChar(byte[] buffer, int codepage)
		{
			bool flag = false;
			return this.UnpackTimestampDateTimeAsChar(buffer, codepage, ref flag);
		}

		// Token: 0x060043A4 RID: 17316 RVA: 0x000E34AC File Offset: 0x000E16AC
		internal DateTime UnpackTimestamp(byte[] buffer, int codepage)
		{
			bool flag = false;
			return this.UnpackTimestamp(buffer, codepage, ref flag);
		}

		// Token: 0x060043A5 RID: 17317 RVA: 0x000E34C8 File Offset: 0x000E16C8
		internal string UnpackTimestampDateTimeAsChar(byte[] buffer, int codepage)
		{
			bool flag = false;
			return this.UnpackTimestampDateTimeAsChar(buffer, codepage, ref flag);
		}

		// Token: 0x060043A6 RID: 17318 RVA: 0x000E34E4 File Offset: 0x000E16E4
		public string UnpackTimestampDateTimeAsChar(byte[] buffer, int codePage, ref bool succeed)
		{
			string empty = string.Empty;
			this.UnpackString(buffer, 0, buffer.Length, codePage, ref empty, true, DrdaTypes.DRDA_TYPE_CHAR);
			return empty;
		}

		// Token: 0x060043A7 RID: 17319 RVA: 0x000E350C File Offset: 0x000E170C
		internal float UnpackFloat(byte[] buffer, string typDefnam)
		{
			float num = 0f;
			DrdaType drdaType = DrdaType.FLOAT;
			this.SetupEncoding(drdaType, 0, 0, typDefnam);
			if (typDefnam == "QTDSQL370" || typDefnam == "QTDSQL400")
			{
				this._typeEncoding.nCvtType = 3;
			}
			else if (typDefnam == "QTDSQLASC")
			{
				this._typeEncoding.nCvtType = 78;
			}
			else
			{
				this._typeEncoding.nCvtType = 74;
			}
			this.PrimitiveConverter.SizeOfRemoteType(this.dataType, this._typeEncoding, out this.resultLen);
			this.PrimitiveConverter.UnpackFloat(ref buffer[0], ref num, ref this.packedDataLength, this.resultLen, this._typeEncoding);
			return num;
		}

		// Token: 0x060043A8 RID: 17320 RVA: 0x000E35C4 File Offset: 0x000E17C4
		internal double UnpackDouble(byte[] buffer, string typDefnam)
		{
			double num = 0.0;
			DrdaType drdaType = DrdaType.DOUBLE;
			this.SetupEncoding(drdaType, 0, 0, typDefnam);
			if (typDefnam == "QTDSQL370" || typDefnam == "QTDSQL400")
			{
				this._typeEncoding.nCvtType = 4;
			}
			else if (typDefnam == "QTDSQLASC")
			{
				this._typeEncoding.nCvtType = 79;
			}
			else
			{
				this._typeEncoding.nCvtType = 75;
			}
			this.PrimitiveConverter.SizeOfRemoteType(this.dataType, this._typeEncoding, out this.resultLen);
			this.PrimitiveConverter.UnpackDouble(ref buffer[0], ref num, ref this.packedDataLength, this.resultLen, this._typeEncoding);
			return num;
		}

		// Token: 0x060043A9 RID: 17321 RVA: 0x000E3680 File Offset: 0x000E1880
		public void UnpackString(byte[] buffer, int position, int size, int encoding, ref string strVal, bool padded)
		{
			this._unpackStringEncoding.DrdaType = DrdaTypes.DRDA_TYPE_CHAR;
			this._unpackStringEncoding.nPad = 0;
			this._unpackStringEncoding.nAsIs = 1;
			this.EnsurePrimitiveConverterEncoding(encoding);
			this.PrimitiveConverter.UnpackString(ref buffer[position], ref strVal, ref size, size, false, size, this._unpackStringEncoding);
		}

		// Token: 0x060043AA RID: 17322 RVA: 0x000E36DC File Offset: 0x000E18DC
		public void UnpackString(byte[] buffer, int position, int size, int encoding, ref string strVal, bool padded, DrdaTypes drdaType)
		{
			this._unpackStringEncoding.DrdaType = drdaType;
			this._unpackStringEncoding.nPad = 0;
			this._unpackStringEncoding.nAsIs = 1;
			this.EnsurePrimitiveConverterEncoding(encoding);
			this.PrimitiveConverter.UnpackString(ref buffer[position], ref strVal, ref size, size, false, size, this._unpackStringEncoding);
		}

		// Token: 0x060043AB RID: 17323 RVA: 0x000E3738 File Offset: 0x000E1938
		public void UnpackStringXML(byte[] buffer, int position, int size, int encoding, ref string strVal, bool padded, DrdaTypes drdaType)
		{
			this._unpackStringEncoding.DrdaType = drdaType;
			this._unpackStringEncoding.nPad = 0;
			this._unpackStringEncoding.nAsIs = 1;
			this.PrimitiveConverter.SetCodePage(encoding);
			this.PrimitiveConverter.UnpackString(ref buffer[position], ref strVal, ref size, size, false, size, this._unpackStringEncoding);
		}

		// Token: 0x060043AC RID: 17324 RVA: 0x000E3798 File Offset: 0x000E1998
		public void PackString(string s, int encoding, byte[] buffer, ref int writeLen, int charCount, DrdaTypes drdaType)
		{
			if (s == null)
			{
				throw new Exception("PackStringDBCS string is null");
			}
			int num = ((s.Length < charCount) ? charCount : s.Length);
			this._packStringEncoding.DrdaType = drdaType;
			this._packStringEncoding.nTRE = 0;
			this._packStringEncoding.nPad = 0;
			this._packStringEncoding.nAsIs = 0;
			this.EnsurePrimitiveConverterEncoding(encoding);
			this.PrimitiveConverter.PackString(s, ref buffer[0], ref writeLen, num, false, num * 2, this._packStringEncoding);
		}

		// Token: 0x060043AD RID: 17325 RVA: 0x000E3824 File Offset: 0x000E1A24
		public void PackStringAsIs(string s, int encoding, byte[] buffer, ref int writeLen, DrdaTypes drdaType)
		{
			if (s == null)
			{
				throw new Exception("PackStringAsIs string is null");
			}
			this._packStringEncoding.DrdaType = drdaType;
			this._packStringEncoding.nTRE = 0;
			this._packStringEncoding.nPad = 0;
			this._packStringEncoding.nAsIs = 1;
			this.EnsurePrimitiveConverterEncoding(encoding);
			this.PrimitiveConverter.PackString(s, ref buffer[0], ref writeLen, buffer.Length, true, buffer.Length, this._packStringEncoding);
		}

		// Token: 0x060043AE RID: 17326 RVA: 0x000E389C File Offset: 0x000E1A9C
		internal void SetCodePage(Ccsid ccsid)
		{
			if (this._ccsid != ccsid)
			{
				this._ccsid = ccsid;
				this.PrimitiveConverter.SetCodePage(this._ccsid._ccsiddbc);
				this.PrimitiveConverter.SetCodePage(this._ccsid._ccsidmbc);
				this.PrimitiveConverter.SetCodePage(this._ccsid._ccsidsbc);
			}
		}

		// Token: 0x060043AF RID: 17327 RVA: 0x000E38FC File Offset: 0x000E1AFC
		public DateTime UnpackTimestamp(byte[] buffer, int codePage, ref bool succeed)
		{
			string empty = string.Empty;
			this.UnpackString(buffer, 0, buffer.Length, codePage, ref empty, true, DrdaTypes.DRDA_TYPE_CHAR);
			return this.UnpackTimestamp(empty, ref succeed);
		}

		// Token: 0x060043B0 RID: 17328 RVA: 0x000E3928 File Offset: 0x000E1B28
		public DateTime UnpackTimestamp(string strVal, ref bool succeed)
		{
			DateTime now = DateTime.Now;
			succeed = false;
			strVal = this.ConvertDB2DateTimeStringToSQL(strVal);
			foreach (string text in this._db2ToSqlDatetimeMasks)
			{
				if (this.PrimitiveConverter.UnpackEditedDateTime(strVal, out now, text))
				{
					succeed = true;
					break;
				}
			}
			return now;
		}

		// Token: 0x060043B1 RID: 17329 RVA: 0x000E39A0 File Offset: 0x000E1BA0
		public DateTime UnpackTimestamp(string strVal, string datetimeMask, ref bool succeed)
		{
			DateTime now = DateTime.Now;
			succeed = false;
			strVal = this.ConvertDB2DateTimeStringToSQL(strVal);
			if (this.PrimitiveConverter.UnpackEditedDateTime(strVal, out now, datetimeMask))
			{
				succeed = true;
			}
			return now;
		}

		// Token: 0x060043B2 RID: 17330 RVA: 0x000E39D4 File Offset: 0x000E1BD4
		private string ConvertDB2DateTimeStringToSQL(string strVal)
		{
			if (strVal.Length == 26)
			{
				string text = strVal.Substring(11, 2);
				if (!char.IsDigit(text[0]) || !char.IsDigit(text[1]))
				{
					return strVal;
				}
				int num = int.Parse(text);
				if (num >= 24)
				{
					num %= 24;
					string text2 = num.ToString();
					if (num < 10)
					{
						text2 = text2.PadLeft(2, '0');
					}
					bool flag = false;
					DateTime dateTime = this.UnpackDate(strVal.Substring(0, 10), ref flag);
					if (!flag)
					{
						return strVal;
					}
					return dateTime.AddDays(1.0).ToString("yyyy-MM-dd") + strVal[10].ToString() + text2 + strVal.Substring(13);
				}
			}
			else
			{
				if (strVal.Length == 19)
				{
					return strVal + ".000000";
				}
				if (strVal.Length == 8)
				{
					string text3 = strVal.Substring(0, 2);
					if (!char.IsDigit(text3[0]) || !char.IsDigit(text3[1]))
					{
						return strVal;
					}
					int num2 = int.Parse(strVal.Substring(0, 2));
					bool flag2 = false;
					if (char.ToUpper(strVal[7]) == 'M')
					{
						if (char.ToUpper(strVal[6]) == 'P')
						{
							if (num2 < 12)
							{
								num2 += 12;
							}
						}
						else if (num2 == 12 && char.ToUpper(strVal[6]) == 'A')
						{
							num2 = 0;
						}
						flag2 = true;
					}
					if (num2 >= 24 || flag2)
					{
						num2 %= 24;
						string text4 = num2.ToString();
						if (num2 < 10)
						{
							text4 = text4.PadLeft(2, '0');
						}
						if (flag2)
						{
							StringBuilder stringBuilder = new StringBuilder();
							stringBuilder.Append(text4);
							stringBuilder.Append(strVal.Substring(2, 3));
							stringBuilder.Append(strVal[2]);
							stringBuilder.Append("00");
							return stringBuilder.ToString();
						}
						return text4 + strVal.Substring(2);
					}
				}
			}
			return strVal;
		}

		// Token: 0x060043B3 RID: 17331 RVA: 0x000E3BC4 File Offset: 0x000E1DC4
		public bool UnpackTimestamp(string strVal, ref string retVal)
		{
			DateTime now = DateTime.Now;
			strVal = this.ConvertDB2DateTimeStringToSQL(strVal);
			foreach (string text in this._db2ToSqlDatetimeMasks)
			{
				if (this.PrimitiveConverter.UnpackEditedDateTime(strVal, out now, text))
				{
					retVal = now.ToString("yyyy-MM-dd HH:mm:ss.ffffff");
					return true;
				}
			}
			return false;
		}

		// Token: 0x060043B4 RID: 17332 RVA: 0x000E3C48 File Offset: 0x000E1E48
		public DateTime UnpackDate(string strVal, ref bool succeed)
		{
			DateTime now = DateTime.Now;
			succeed = false;
			foreach (string text in this._db2ToSqlDateMasks)
			{
				if (this.PrimitiveConverter.UnpackEditedDateTime(strVal, out now, text))
				{
					succeed = true;
					break;
				}
			}
			return now;
		}

		// Token: 0x060043B5 RID: 17333 RVA: 0x000E3CB4 File Offset: 0x000E1EB4
		public bool UnpackDate(string strVal, ref string retVal)
		{
			DateTime now = DateTime.Now;
			foreach (string text in this._db2ToSqlDateMasks)
			{
				if (this.PrimitiveConverter.UnpackEditedDateTime(strVal, out now, text))
				{
					retVal = now.ToString("yyyy-MM-dd");
					return true;
				}
			}
			return false;
		}

		// Token: 0x060043B6 RID: 17334 RVA: 0x000E3D2C File Offset: 0x000E1F2C
		public TimeSpan UnpackTime(string strVal, ref bool succeed)
		{
			strVal = this.ConvertDB2DateTimeStringToSQL(strVal);
			TimeSpan zero = TimeSpan.Zero;
			succeed = false;
			foreach (string text in this._db2ToSqlTimeMasks)
			{
				if (this.PrimitiveConverter.UnpackEditedTimeSpan(strVal, out zero, text))
				{
					succeed = true;
					return zero;
				}
			}
			return TimeSpan.Zero;
		}

		// Token: 0x060043B7 RID: 17335 RVA: 0x000E3DAC File Offset: 0x000E1FAC
		public TimeSpan UnpackTime(string strVal, string timeMask, ref bool succeed)
		{
			strVal = this.ConvertDB2DateTimeStringToSQL(strVal);
			TimeSpan zero = TimeSpan.Zero;
			succeed = false;
			if (this.PrimitiveConverter.UnpackEditedTimeSpan(strVal, out zero, timeMask))
			{
				succeed = true;
				return zero;
			}
			return TimeSpan.Zero;
		}

		// Token: 0x060043B8 RID: 17336 RVA: 0x000E3DE8 File Offset: 0x000E1FE8
		public bool UnpackTime(string strVal, ref string retVal)
		{
			strVal = this.ConvertDB2DateTimeStringToSQL(strVal);
			TimeSpan zero = TimeSpan.Zero;
			foreach (string text in this._db2ToSqlTimeMasks)
			{
				if (this.PrimitiveConverter.UnpackEditedTimeSpan(strVal, out zero, text))
				{
					retVal = zero.ToString("hh\\:mm\\:ss");
					return true;
				}
			}
			return false;
		}

		// Token: 0x060043B9 RID: 17337 RVA: 0x000E3E6C File Offset: 0x000E206C
		internal void UnpackDSString(byte[] buffer, int position, int byteCount, int encoding, ref string strVal, DrdaTypes drdaType)
		{
			this._unpackDSSStringEncoding.DrdaType = drdaType;
			this._unpackDSSStringEncoding.nPad = 0;
			this._unpackDSSStringEncoding.nAsIs = 1;
			this.EnsurePrimitiveConverterEncoding(encoding);
			this.PrimitiveConverter.UnpackString(ref buffer[position], ref strVal, ref byteCount, byteCount / 2, false, byteCount, this._unpackDSSStringEncoding);
		}

		// Token: 0x1700100E RID: 4110
		// (get) Token: 0x060043BA RID: 17338 RVA: 0x000E3EC8 File Offset: 0x000E20C8
		public int DateMaskLength
		{
			get
			{
				if (this._dateMaskLength == 0)
				{
					foreach (DateTimeMask dateTimeMask in this._dateTimeMasks)
					{
						if (!dateTimeMask.Db2ToSql && dateTimeMask.Type == DateTimeMaskType.Date)
						{
							return dateTimeMask.Mask.Length;
						}
					}
					this._dateMaskLength = 10;
				}
				return this._dateMaskLength;
			}
		}

		// Token: 0x060043BB RID: 17339 RVA: 0x000E3F4C File Offset: 0x000E214C
		public bool TryConvertToDateTime(string strVal, int length, out string dateTimeValue)
		{
			bool flag = false;
			dateTimeValue = string.Empty;
			try
			{
				if (length == 10)
				{
					DateTime dateTime = this.UnpackDate(strVal, ref flag);
					if (flag)
					{
						dateTimeValue = dateTime.ToString("yyyy-MM-dd");
					}
				}
				else if (length == 26)
				{
					DateTime dateTime2 = this.UnpackTimestamp(strVal, ref flag);
					if (flag)
					{
						dateTimeValue = dateTime2.ToString("yyyy-MM-dd HH:mm:ss.ffffff");
					}
				}
				else if (length == 8)
				{
					TimeSpan timeSpan = this.UnpackTime(strVal, ref flag);
					if (flag)
					{
						string text = timeSpan.ToString();
						dateTimeValue = text.Substring(0, 8);
					}
				}
			}
			catch (Exception)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x04002F8D RID: 12173
		private const int DATE_LENGTH = 10;

		// Token: 0x04002F8E RID: 12174
		private const int TIME_LENGTH = 8;

		// Token: 0x04002F8F RID: 12175
		private const int DATETIME_LENGTH = 26;

		// Token: 0x04002F90 RID: 12176
		private BasePrimitiveConverter _primitiveConverter;

		// Token: 0x04002F91 RID: 12177
		private SystemIPrimitiveConverter _systemiConverter;

		// Token: 0x04002F92 RID: 12178
		private CEDAR_TYPE_ENCODING _typeEncoding = new CEDAR_TYPE_ENCODING();

		// Token: 0x04002F93 RID: 12179
		private CEDAR_TYPE_ENCODING _unpackStringEncoding = new CEDAR_TYPE_ENCODING(184549381);

		// Token: 0x04002F94 RID: 12180
		private CEDAR_TYPE_ENCODING _packStringEncoding = new CEDAR_TYPE_ENCODING(167772165);

		// Token: 0x04002F95 RID: 12181
		private CEDAR_TYPE_ENCODING _unpackDSSStringEncoding = new CEDAR_TYPE_ENCODING(184549381);

		// Token: 0x04002F96 RID: 12182
		private DataType dataType = DataType.Void;

		// Token: 0x04002F97 RID: 12183
		private int packedDataLength;

		// Token: 0x04002F98 RID: 12184
		private int resultLen;

		// Token: 0x04002F99 RID: 12185
		private List<DateTimeMask> _dateTimeMasks;

		// Token: 0x04002F9A RID: 12186
		private int lastEncoding = 1208;

		// Token: 0x04002F9B RID: 12187
		private int _dateMaskLength;

		// Token: 0x04002F9C RID: 12188
		private List<string> _db2ToSqlDatetimeMasks = new List<string>();

		// Token: 0x04002F9D RID: 12189
		private List<string> _db2ToSqlDateMasks = new List<string>();

		// Token: 0x04002F9E RID: 12190
		private List<string> _db2ToSqlTimeMasks = new List<string>();

		// Token: 0x04002F9F RID: 12191
		private string _sqlToDb2DateMask;

		// Token: 0x04002FA0 RID: 12192
		private Ccsid _ccsid;

		// Token: 0x04002FA1 RID: 12193
		private object _tracePoint;

		// Token: 0x04002FA2 RID: 12194
		private string _sqlToDb2TimeMask;

		// Token: 0x04002FA3 RID: 12195
		private string _sqlToDb2DatetimeMask;
	}
}
