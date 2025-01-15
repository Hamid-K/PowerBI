using System;
using System.Data;
using System.Data.SqlTypes;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Data.Common;

namespace Microsoft.Data.SqlClient.Server
{
	// Token: 0x02000140 RID: 320
	internal sealed class SqlRecordBuffer
	{
		// Token: 0x06001885 RID: 6277 RVA: 0x0006626C File Offset: 0x0006446C
		internal SqlRecordBuffer(SmiMetaData metaData)
		{
			this._isNull = true;
		}

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x06001886 RID: 6278 RVA: 0x0006627B File Offset: 0x0006447B
		internal bool IsNull
		{
			get
			{
				return this._isNull;
			}
		}

		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x06001887 RID: 6279 RVA: 0x00066283 File Offset: 0x00064483
		// (set) Token: 0x06001888 RID: 6280 RVA: 0x00066290 File Offset: 0x00064490
		internal bool Boolean
		{
			get
			{
				return this._value._boolean;
			}
			set
			{
				this._value._boolean = value;
				this._type = SqlRecordBuffer.StorageType.Boolean;
				this._isNull = false;
			}
		}

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x06001889 RID: 6281 RVA: 0x000662AC File Offset: 0x000644AC
		// (set) Token: 0x0600188A RID: 6282 RVA: 0x000662B9 File Offset: 0x000644B9
		internal byte Byte
		{
			get
			{
				return this._value._byte;
			}
			set
			{
				this._value._byte = value;
				this._type = SqlRecordBuffer.StorageType.Byte;
				this._isNull = false;
			}
		}

		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x0600188B RID: 6283 RVA: 0x000662D5 File Offset: 0x000644D5
		// (set) Token: 0x0600188C RID: 6284 RVA: 0x000662E2 File Offset: 0x000644E2
		internal DateTime DateTime
		{
			get
			{
				return this._value._dateTime;
			}
			set
			{
				this._value._dateTime = value;
				this._type = SqlRecordBuffer.StorageType.DateTime;
				this._isNull = false;
				if (this._isMetaSet)
				{
					this._isMetaSet = false;
					return;
				}
				this._metadata = null;
			}
		}

		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x0600188D RID: 6285 RVA: 0x00066315 File Offset: 0x00064515
		// (set) Token: 0x0600188E RID: 6286 RVA: 0x00066322 File Offset: 0x00064522
		internal DateTimeOffset DateTimeOffset
		{
			get
			{
				return this._value._dateTimeOffset;
			}
			set
			{
				this._value._dateTimeOffset = value;
				this._type = SqlRecordBuffer.StorageType.DateTimeOffset;
				this._isNull = false;
			}
		}

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x0600188F RID: 6287 RVA: 0x0006633E File Offset: 0x0006453E
		// (set) Token: 0x06001890 RID: 6288 RVA: 0x0006634B File Offset: 0x0006454B
		internal double Double
		{
			get
			{
				return this._value._double;
			}
			set
			{
				this._value._double = value;
				this._type = SqlRecordBuffer.StorageType.Double;
				this._isNull = false;
			}
		}

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x06001891 RID: 6289 RVA: 0x00066367 File Offset: 0x00064567
		// (set) Token: 0x06001892 RID: 6290 RVA: 0x00066374 File Offset: 0x00064574
		internal Guid Guid
		{
			get
			{
				return this._value._guid;
			}
			set
			{
				this._value._guid = value;
				this._type = SqlRecordBuffer.StorageType.Guid;
				this._isNull = false;
			}
		}

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x06001893 RID: 6291 RVA: 0x00066390 File Offset: 0x00064590
		// (set) Token: 0x06001894 RID: 6292 RVA: 0x0006639D File Offset: 0x0006459D
		internal short Int16
		{
			get
			{
				return this._value._int16;
			}
			set
			{
				this._value._int16 = value;
				this._type = SqlRecordBuffer.StorageType.Int16;
				this._isNull = false;
			}
		}

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x06001895 RID: 6293 RVA: 0x000663B9 File Offset: 0x000645B9
		// (set) Token: 0x06001896 RID: 6294 RVA: 0x000663C6 File Offset: 0x000645C6
		internal int Int32
		{
			get
			{
				return this._value._int32;
			}
			set
			{
				this._value._int32 = value;
				this._type = SqlRecordBuffer.StorageType.Int32;
				this._isNull = false;
			}
		}

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x06001897 RID: 6295 RVA: 0x000663E3 File Offset: 0x000645E3
		// (set) Token: 0x06001898 RID: 6296 RVA: 0x000663F0 File Offset: 0x000645F0
		internal long Int64
		{
			get
			{
				return this._value._int64;
			}
			set
			{
				this._value._int64 = value;
				this._type = SqlRecordBuffer.StorageType.Int64;
				this._isNull = false;
				if (this._isMetaSet)
				{
					this._isMetaSet = false;
					return;
				}
				this._metadata = null;
			}
		}

		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x06001899 RID: 6297 RVA: 0x00066424 File Offset: 0x00064624
		// (set) Token: 0x0600189A RID: 6298 RVA: 0x00066431 File Offset: 0x00064631
		internal float Single
		{
			get
			{
				return this._value._single;
			}
			set
			{
				this._value._single = value;
				this._type = SqlRecordBuffer.StorageType.Single;
				this._isNull = false;
			}
		}

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x0600189B RID: 6299 RVA: 0x00066450 File Offset: 0x00064650
		// (set) Token: 0x0600189C RID: 6300 RVA: 0x000664B4 File Offset: 0x000646B4
		internal string String
		{
			get
			{
				if (SqlRecordBuffer.StorageType.String == this._type)
				{
					return (string)this._object;
				}
				if (SqlRecordBuffer.StorageType.CharArray == this._type)
				{
					return new string((char[])this._object, 0, (int)this.CharsLength);
				}
				Stream stream = new MemoryStream((byte[])this._object, false);
				return new SqlXml(stream).Value;
			}
			set
			{
				this._object = value;
				this._value._int64 = (long)value.Length;
				this._type = SqlRecordBuffer.StorageType.String;
				this._isNull = false;
				if (this._isMetaSet)
				{
					this._isMetaSet = false;
					return;
				}
				this._metadata = null;
			}
		}

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x0600189D RID: 6301 RVA: 0x00066500 File Offset: 0x00064700
		// (set) Token: 0x0600189E RID: 6302 RVA: 0x0006650D File Offset: 0x0006470D
		internal SqlDecimal SqlDecimal
		{
			get
			{
				return (SqlDecimal)this._object;
			}
			set
			{
				this._object = value;
				this._type = SqlRecordBuffer.StorageType.SqlDecimal;
				this._isNull = false;
			}
		}

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x0600189F RID: 6303 RVA: 0x0006652A File Offset: 0x0006472A
		// (set) Token: 0x060018A0 RID: 6304 RVA: 0x00066537 File Offset: 0x00064737
		internal TimeSpan TimeSpan
		{
			get
			{
				return this._value._timeSpan;
			}
			set
			{
				this._value._timeSpan = value;
				this._type = SqlRecordBuffer.StorageType.TimeSpan;
				this._isNull = false;
			}
		}

		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x060018A1 RID: 6305 RVA: 0x00066554 File Offset: 0x00064754
		// (set) Token: 0x060018A2 RID: 6306 RVA: 0x00066571 File Offset: 0x00064771
		internal long BytesLength
		{
			get
			{
				if (SqlRecordBuffer.StorageType.String == this._type)
				{
					this.ConvertXmlStringToByteArray();
				}
				return this._value._int64;
			}
			set
			{
				if (value == 0L)
				{
					this._value._int64 = value;
					this._object = Array.Empty<byte>();
					this._type = SqlRecordBuffer.StorageType.ByteArray;
					this._isNull = false;
					return;
				}
				this._value._int64 = value;
			}
		}

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x060018A3 RID: 6307 RVA: 0x000663E3 File Offset: 0x000645E3
		// (set) Token: 0x060018A4 RID: 6308 RVA: 0x000665A8 File Offset: 0x000647A8
		internal long CharsLength
		{
			get
			{
				return this._value._int64;
			}
			set
			{
				if (value == 0L)
				{
					this._value._int64 = value;
					this._object = Array.Empty<char>();
					this._type = SqlRecordBuffer.StorageType.CharArray;
					this._isNull = false;
					return;
				}
				this._value._int64 = value;
			}
		}

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x060018A5 RID: 6309 RVA: 0x000665E0 File Offset: 0x000647E0
		// (set) Token: 0x060018A6 RID: 6310 RVA: 0x000666E2 File Offset: 0x000648E2
		internal SmiMetaData VariantType
		{
			get
			{
				switch (this._type)
				{
				case SqlRecordBuffer.StorageType.Boolean:
					return SmiMetaData.DefaultBit;
				case SqlRecordBuffer.StorageType.Byte:
					return SmiMetaData.DefaultTinyInt;
				case SqlRecordBuffer.StorageType.ByteArray:
					return SmiMetaData.DefaultVarBinary;
				case SqlRecordBuffer.StorageType.CharArray:
					return SmiMetaData.DefaultNVarChar;
				case SqlRecordBuffer.StorageType.DateTime:
					return this._metadata ?? SmiMetaData.DefaultDateTime;
				case SqlRecordBuffer.StorageType.DateTimeOffset:
					return SmiMetaData.DefaultDateTimeOffset;
				case SqlRecordBuffer.StorageType.Double:
					return SmiMetaData.DefaultFloat;
				case SqlRecordBuffer.StorageType.Guid:
					return SmiMetaData.DefaultUniqueIdentifier;
				case SqlRecordBuffer.StorageType.Int16:
					return SmiMetaData.DefaultSmallInt;
				case SqlRecordBuffer.StorageType.Int32:
					return SmiMetaData.DefaultInt;
				case SqlRecordBuffer.StorageType.Int64:
					return this._metadata ?? SmiMetaData.DefaultBigInt;
				case SqlRecordBuffer.StorageType.Single:
					return SmiMetaData.DefaultReal;
				case SqlRecordBuffer.StorageType.String:
					return this._metadata ?? SmiMetaData.DefaultNVarChar;
				case SqlRecordBuffer.StorageType.SqlDecimal:
					return new SmiMetaData(SqlDbType.Decimal, 17L, ((SqlDecimal)this._object).Precision, ((SqlDecimal)this._object).Scale, 0L, SqlCompareOptions.None, null);
				case SqlRecordBuffer.StorageType.TimeSpan:
					return SmiMetaData.DefaultTime;
				default:
					return null;
				}
			}
			set
			{
				this._metadata = value;
				this._isMetaSet = true;
			}
		}

		// Token: 0x060018A7 RID: 6311 RVA: 0x000666F4 File Offset: 0x000648F4
		internal int GetBytes(long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			int num = (int)fieldOffset;
			if (SqlRecordBuffer.StorageType.String == this._type)
			{
				this.ConvertXmlStringToByteArray();
			}
			Buffer.BlockCopy((byte[])this._object, num, buffer, bufferOffset, length);
			return length;
		}

		// Token: 0x060018A8 RID: 6312 RVA: 0x0006672C File Offset: 0x0006492C
		internal int GetChars(long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			int num = (int)fieldOffset;
			if (SqlRecordBuffer.StorageType.CharArray == this._type)
			{
				Array.Copy((char[])this._object, num, buffer, bufferOffset, length);
			}
			else
			{
				((string)this._object).CopyTo(num, buffer, bufferOffset, length);
			}
			return length;
		}

		// Token: 0x060018A9 RID: 6313 RVA: 0x00066774 File Offset: 0x00064974
		internal int SetBytes(long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			int num = (int)fieldOffset;
			if (this.IsNull || SqlRecordBuffer.StorageType.ByteArray != this._type)
			{
				if (num != 0)
				{
					throw ADP.ArgumentOutOfRange("fieldOffset");
				}
				this._object = new byte[length];
				this._type = SqlRecordBuffer.StorageType.ByteArray;
				this._isNull = false;
				this.BytesLength = (long)length;
			}
			else
			{
				if ((long)num > this.BytesLength)
				{
					throw ADP.ArgumentOutOfRange("fieldOffset");
				}
				if ((long)(num + length) > this.BytesLength)
				{
					int num2 = ((byte[])this._object).Length;
					if (num + length > num2)
					{
						byte[] array = new byte[Math.Max(num + length, 2 * num2)];
						Buffer.BlockCopy((byte[])this._object, 0, array, 0, (int)this.BytesLength);
						this._object = array;
					}
					this.BytesLength = (long)(num + length);
				}
			}
			Buffer.BlockCopy(buffer, bufferOffset, (byte[])this._object, num, length);
			return length;
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x00066858 File Offset: 0x00064A58
		internal int SetChars(long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			int num = (int)fieldOffset;
			if (this.IsNull || (SqlRecordBuffer.StorageType.CharArray != this._type && SqlRecordBuffer.StorageType.String != this._type))
			{
				if (num != 0)
				{
					throw ADP.ArgumentOutOfRange("fieldOffset");
				}
				this._object = new char[length];
				this._type = SqlRecordBuffer.StorageType.CharArray;
				this._isNull = false;
				this.CharsLength = (long)length;
			}
			else
			{
				if ((long)num > this.CharsLength)
				{
					throw ADP.ArgumentOutOfRange("fieldOffset");
				}
				if (SqlRecordBuffer.StorageType.String == this._type)
				{
					this._object = ((string)this._object).ToCharArray();
					this._type = SqlRecordBuffer.StorageType.CharArray;
				}
				if ((long)(num + length) > this.CharsLength)
				{
					int num2 = ((char[])this._object).Length;
					if (num + length > num2)
					{
						char[] array = new char[Math.Max(num + length, 2 * num2)];
						Array.Copy((char[])this._object, 0, array, 0, (int)this.CharsLength);
						this._object = array;
					}
					this.CharsLength = (long)(num + length);
				}
			}
			Array.Copy(buffer, bufferOffset, (char[])this._object, num, length);
			return length;
		}

		// Token: 0x060018AB RID: 6315 RVA: 0x0006696D File Offset: 0x00064B6D
		internal void SetNull()
		{
			this._isNull = true;
		}

		// Token: 0x060018AC RID: 6316 RVA: 0x00066978 File Offset: 0x00064B78
		private void ConvertXmlStringToByteArray()
		{
			string text = (string)this._object;
			byte[] array = new byte[2 + Encoding.Unicode.GetByteCount(text)];
			array[0] = byte.MaxValue;
			array[1] = 254;
			Encoding.Unicode.GetBytes(text, 0, text.Length, array, 2);
			this._object = array;
			this._value._int64 = (long)array.Length;
			this._type = SqlRecordBuffer.StorageType.ByteArray;
		}

		// Token: 0x040009B8 RID: 2488
		private bool _isNull;

		// Token: 0x040009B9 RID: 2489
		private SqlRecordBuffer.StorageType _type;

		// Token: 0x040009BA RID: 2490
		private SqlRecordBuffer.Storage _value;

		// Token: 0x040009BB RID: 2491
		private object _object;

		// Token: 0x040009BC RID: 2492
		private SmiMetaData _metadata;

		// Token: 0x040009BD RID: 2493
		private bool _isMetaSet;

		// Token: 0x02000269 RID: 617
		internal enum StorageType
		{
			// Token: 0x0400171F RID: 5919
			Boolean,
			// Token: 0x04001720 RID: 5920
			Byte,
			// Token: 0x04001721 RID: 5921
			ByteArray,
			// Token: 0x04001722 RID: 5922
			CharArray,
			// Token: 0x04001723 RID: 5923
			DateTime,
			// Token: 0x04001724 RID: 5924
			DateTimeOffset,
			// Token: 0x04001725 RID: 5925
			Double,
			// Token: 0x04001726 RID: 5926
			Guid,
			// Token: 0x04001727 RID: 5927
			Int16,
			// Token: 0x04001728 RID: 5928
			Int32,
			// Token: 0x04001729 RID: 5929
			Int64,
			// Token: 0x0400172A RID: 5930
			Single,
			// Token: 0x0400172B RID: 5931
			String,
			// Token: 0x0400172C RID: 5932
			SqlDecimal,
			// Token: 0x0400172D RID: 5933
			TimeSpan
		}

		// Token: 0x0200026A RID: 618
		[StructLayout(LayoutKind.Explicit)]
		internal struct Storage
		{
			// Token: 0x0400172E RID: 5934
			[FieldOffset(0)]
			internal bool _boolean;

			// Token: 0x0400172F RID: 5935
			[FieldOffset(0)]
			internal byte _byte;

			// Token: 0x04001730 RID: 5936
			[FieldOffset(0)]
			internal DateTime _dateTime;

			// Token: 0x04001731 RID: 5937
			[FieldOffset(0)]
			internal DateTimeOffset _dateTimeOffset;

			// Token: 0x04001732 RID: 5938
			[FieldOffset(0)]
			internal double _double;

			// Token: 0x04001733 RID: 5939
			[FieldOffset(0)]
			internal Guid _guid;

			// Token: 0x04001734 RID: 5940
			[FieldOffset(0)]
			internal short _int16;

			// Token: 0x04001735 RID: 5941
			[FieldOffset(0)]
			internal int _int32;

			// Token: 0x04001736 RID: 5942
			[FieldOffset(0)]
			internal long _int64;

			// Token: 0x04001737 RID: 5943
			[FieldOffset(0)]
			internal float _single;

			// Token: 0x04001738 RID: 5944
			[FieldOffset(0)]
			internal TimeSpan _timeSpan;
		}
	}
}
