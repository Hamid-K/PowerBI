using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Parquet;
using Microsoft.OleDb;
using ParquetSharp;

namespace Microsoft.Mashup.Engine1.Library.Parquet
{
	// Token: 0x02001F1C RID: 7964
	internal static class LogicalTypeConversions
	{
		// Token: 0x06010C32 RID: 68658 RVA: 0x0039BE7A File Offset: 0x0039A07A
		public static int MaxPrecision(int length)
		{
			return (int)Math.Floor(Math.Log10(Math.Pow(2.0, (double)(8 * length - 1)) - 1.0));
		}

		// Token: 0x06010C33 RID: 68659 RVA: 0x0039BEA8 File Offset: 0x0039A0A8
		public static int RequiredLength(int precision)
		{
			return (int)Math.Ceiling((Math.Log(Math.Pow(10.0, (double)precision) - 1.0, 2.0) + 1.0) / 8.0);
		}

		// Token: 0x06010C34 RID: 68660 RVA: 0x0039BEF8 File Offset: 0x0039A0F8
		public static decimal MaxOfDecimalRange(int precision, int scale)
		{
			if (precision - scale > 28)
			{
				return decimal.MaxValue;
			}
			int num;
			return LogicalTypeConversions.TryUnscaleDecimal(1m, precision - scale, out num) - 1m;
		}

		// Token: 0x06010C35 RID: 68661 RVA: 0x00004FAE File Offset: 0x000031AE
		public static T None<T>(T value)
		{
			return value;
		}

		// Token: 0x06010C36 RID: 68662 RVA: 0x0000E945 File Offset: 0x0000CB45
		public static T None<T>(IAllocator allocator, T value)
		{
			return value;
		}

		// Token: 0x06010C37 RID: 68663 RVA: 0x0039BF30 File Offset: 0x0039A130
		public static byte[] ByteArrayToBytes(ByteArray byteArray)
		{
			byte[] array = new byte[byteArray.Length];
			Marshal.Copy(byteArray.Pointer, array, 0, byteArray.Length);
			return array;
		}

		// Token: 0x06010C38 RID: 68664 RVA: 0x0039BF60 File Offset: 0x0039A160
		public static ByteArray ByteArrayFromBytes(IAllocator allocator, byte[] bytes)
		{
			ByteArray byteArray = allocator.Allocate(bytes.Length);
			Marshal.Copy(bytes, 0, byteArray.Pointer, byteArray.Length);
			return byteArray;
		}

		// Token: 0x06010C39 RID: 68665 RVA: 0x0039BF8B File Offset: 0x0039A18B
		public static Func<FixedLenByteArray, byte[]> FixedLenByteArrayToBytes(int length)
		{
			return delegate(FixedLenByteArray fixedLenByteArray)
			{
				byte[] array = new byte[length];
				Marshal.Copy(fixedLenByteArray.Pointer, array, 0, length);
				return array;
			};
		}

		// Token: 0x06010C3A RID: 68666 RVA: 0x0039BFA4 File Offset: 0x0039A1A4
		public static Func<IAllocator, byte[], FixedLenByteArray> FixedLenByteArrayFromBytes(int length)
		{
			return delegate(IAllocator allocator, byte[] bytes)
			{
				if (bytes.Length != length)
				{
					throw ValueException.NewDataFormatError<Message0>(Resources.ExpectedFixedLengthError, NumberValue.New(bytes.Length), null);
				}
				return new FixedLenByteArray(LogicalTypeConversions.ByteArrayFromBytes(allocator, bytes).Pointer);
			};
		}

		// Token: 0x06010C3B RID: 68667 RVA: 0x0039BFBD File Offset: 0x0039A1BD
		public unsafe static string ByteArrayToString(ByteArray byteArray)
		{
			return new string((sbyte*)(void*)byteArray.Pointer, 0, byteArray.Length, global::System.Text.Encoding.UTF8);
		}

		// Token: 0x06010C3C RID: 68668 RVA: 0x0039BFDC File Offset: 0x0039A1DC
		public unsafe static ByteArray ByteArrayFromString(IAllocator allocator, string str)
		{
			global::System.Text.Encoding utf = global::System.Text.Encoding.UTF8;
			int byteCount = utf.GetByteCount(str);
			ByteArray byteArray = allocator.Allocate(byteCount);
			fixed (string text = str)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				utf.GetBytes(ptr, str.Length, (byte*)(void*)byteArray.Pointer, byteCount);
			}
			return byteArray;
		}

		// Token: 0x06010C3D RID: 68669 RVA: 0x0039C02C File Offset: 0x0039A22C
		public unsafe static Guid FixedLenByteArrayToUuid(FixedLenByteArray fixedLenByteArray)
		{
			byte* ptr = (byte*)(void*)fixedLenByteArray.Pointer;
			return new Guid(LogicalTypeConversions.ReadInt32BigEndian(ptr), LogicalTypeConversions.ReadInt16BigEndian(ptr + 4), LogicalTypeConversions.ReadInt16BigEndian(ptr + 6), ptr[8], ptr[9], ptr[10], ptr[11], ptr[12], ptr[13], ptr[14], ptr[15]);
		}

		// Token: 0x06010C3E RID: 68670 RVA: 0x0039C088 File Offset: 0x0039A288
		public unsafe static FixedLenByteArray FixedLenByteArrayFromUuid(IAllocator allocator, Guid guid)
		{
			ByteArray byteArray = allocator.Allocate(16);
			FixedLenByteArray fixedLenByteArray = new FixedLenByteArray(byteArray.Pointer);
			byte* ptr = (byte*)(&guid);
			byte* ptr2 = (byte*)(void*)fixedLenByteArray.Pointer;
			LogicalTypeConversions.WriteInt32BigEndian(ptr2, *(int*)ptr);
			LogicalTypeConversions.WriteInt16BigEndian(ptr2 + 4, *(short*)(ptr + 4));
			LogicalTypeConversions.WriteInt16BigEndian(ptr2 + 6, *(short*)(ptr + 6));
			*(long*)(ptr2 + 8) = *(long*)(ptr + 8);
			return fixedLenByteArray;
		}

		// Token: 0x06010C3F RID: 68671 RVA: 0x0039C0E4 File Offset: 0x0039A2E4
		public static Func<int, decimal> Int32ToDecimal(int precision, int scale)
		{
			Func<int, decimal> func;
			if (!LogicalTypeConversions.ValidateDecimalType<int, decimal>(precision, scale, 9, out func))
			{
				return func;
			}
			return (int value) => LogicalTypeConversions.NewDecimal(new decimal(value), scale);
		}

		// Token: 0x06010C40 RID: 68672 RVA: 0x0039C120 File Offset: 0x0039A320
		public static Func<IAllocator, decimal, int> Int32FromDecimal(int precision, int scale)
		{
			decimal max = LogicalTypeConversions.MaxOfDecimalRange(precision, scale);
			decimal min = -max;
			return delegate(IAllocator allocator, decimal value)
			{
				LogicalTypeConversions.ValidateInRange(value, min, max, precision, scale);
				return (int)LogicalTypeConversions.TryUnscaleDecimal(value, scale, out scale);
			};
		}

		// Token: 0x06010C41 RID: 68673 RVA: 0x0039C178 File Offset: 0x0039A378
		public static Func<long, decimal> Int64ToDecimal(int precision, int scale)
		{
			Func<long, decimal> func;
			if (!LogicalTypeConversions.ValidateDecimalType<long, decimal>(precision, scale, 18, out func))
			{
				return func;
			}
			return (long value) => LogicalTypeConversions.NewDecimal(new decimal(value), scale);
		}

		// Token: 0x06010C42 RID: 68674 RVA: 0x0039C1B4 File Offset: 0x0039A3B4
		public static Func<IAllocator, decimal, long> Int64FromDecimal(int precision, int scale)
		{
			decimal max = LogicalTypeConversions.MaxOfDecimalRange(precision, scale);
			decimal min = -max;
			return delegate(IAllocator allocator, decimal value)
			{
				LogicalTypeConversions.ValidateInRange(value, min, max, precision, scale);
				return (long)LogicalTypeConversions.TryUnscaleDecimal(value, scale, out scale);
			};
		}

		// Token: 0x06010C43 RID: 68675 RVA: 0x0039C20C File Offset: 0x0039A40C
		public static Func<FixedLenByteArray, decimal> FixedLenByteArrayToDecimalNoOverflow(int length, int precision, int scale)
		{
			Func<FixedLenByteArray, decimal> func;
			if (!LogicalTypeConversions.ValidateDecimalType<FixedLenByteArray, decimal>(precision, scale, LogicalTypeConversions.MaxPrecision(length), out func))
			{
				return func;
			}
			if (length < 0)
			{
				return delegate(FixedLenByteArray value)
				{
					throw ValueException.NewDataFormatError<Message1>(Resources.LoadError(Resources.NegativeBinaryLength), NumberValue.New(length), null);
				};
			}
			if (length == 0)
			{
				return (FixedLenByteArray value) => 0m;
			}
			if (length <= 12)
			{
				return (FixedLenByteArray value) => LogicalTypeConversions.ReadParquetDecimalShort(value.Pointer, length, precision, scale);
			}
			return (FixedLenByteArray value) => LogicalTypeConversions.ReadParquetDecimalNoOverflow(value.Pointer, length, precision, scale);
		}

		// Token: 0x06010C44 RID: 68676 RVA: 0x0039C2B8 File Offset: 0x0039A4B8
		public static Func<FixedLenByteArray, Number> FixedLenByteArrayToDecimalWithOverflow(int length, int precision, int scale)
		{
			Func<FixedLenByteArray, Number> func;
			if (!LogicalTypeConversions.ValidateDecimalType<FixedLenByteArray, Number>(precision, scale, LogicalTypeConversions.MaxPrecision(length), out func))
			{
				return func;
			}
			if (length < 0)
			{
				return delegate(FixedLenByteArray value)
				{
					throw ValueException.NewDataFormatError<Message1>(Resources.LoadError(Resources.NegativeBinaryLength), NumberValue.New(length), null);
				};
			}
			if (length == 0)
			{
				return (FixedLenByteArray value) => new Number(0m);
			}
			if (length <= 12)
			{
				return (FixedLenByteArray value) => new Number(LogicalTypeConversions.ReadParquetDecimalShort(value.Pointer, length, precision, scale));
			}
			return (FixedLenByteArray value) => LogicalTypeConversions.ReadParquetDecimalWithOverflow(value.Pointer, length, precision, scale);
		}

		// Token: 0x06010C45 RID: 68677 RVA: 0x0039C364 File Offset: 0x0039A564
		public static Func<IAllocator, decimal, FixedLenByteArray> FixedLenByteArrayFromDecimalNoOverflow(int length, int precision, int scale)
		{
			Func<decimal, FixedLenByteArray> errorConversion;
			if (!LogicalTypeConversions.ValidateDecimalType<decimal, FixedLenByteArray>(precision, scale, LogicalTypeConversions.MaxPrecision(length), out errorConversion))
			{
				return (IAllocator allocator, decimal value) => errorConversion(value);
			}
			decimal max = LogicalTypeConversions.MaxOfDecimalRange(precision, scale);
			decimal min = -max;
			if (length < 0)
			{
				return delegate(IAllocator allocator, decimal value)
				{
					throw ValueException.NewDataFormatError<Message1>(Resources.WriteError(Resources.NegativeBinaryLength), NumberValue.New(length), null);
				};
			}
			if (length == 0)
			{
				return delegate(IAllocator allocator, decimal value)
				{
					LogicalTypeConversions.ValidateInRange(value, min, max, precision, scale);
					return new FixedLenByteArray(IntPtr.Zero);
				};
			}
			return delegate(IAllocator allocator, decimal value)
			{
				LogicalTypeConversions.ValidateInRange(value, min, max, precision, scale);
				FixedLenByteArray fixedLenByteArray = new FixedLenByteArray(allocator.Allocate(length).Pointer);
				LogicalTypeConversions.WriteFixedLengthParquetDecimal(fixedLenByteArray.Pointer, length, precision, scale, value);
				return fixedLenByteArray;
			};
		}

		// Token: 0x06010C46 RID: 68678 RVA: 0x0039C41C File Offset: 0x0039A61C
		public static Func<IAllocator, Number, FixedLenByteArray> FixedLenByteArrayFromDecimalWithOverflow(int length, int precision, int scale)
		{
			Func<IAllocator, decimal, FixedLenByteArray> func = LogicalTypeConversions.FixedLenByteArrayFromDecimalNoOverflow(length, precision, scale);
			return (IAllocator allocator, Number value) => func(allocator, value.ToDecimal());
		}

		// Token: 0x06010C47 RID: 68679 RVA: 0x0039C43C File Offset: 0x0039A63C
		public static Func<ByteArray, decimal> ByteArrayToDecimalNoOverflow(int precision, int scale)
		{
			Func<ByteArray, decimal> func;
			if (!LogicalTypeConversions.ValidateDecimalType<ByteArray, decimal>(precision, scale, 2147483647, out func))
			{
				return func;
			}
			return delegate(ByteArray value)
			{
				if (value.Length < 0)
				{
					throw ValueException.NewDataFormatError<Message1>(Resources.LoadError(Resources.NegativeBinaryLength), NumberValue.New(value.Length), null);
				}
				if (value.Length == 0)
				{
					return 0m;
				}
				if (value.Length <= 12)
				{
					return LogicalTypeConversions.ReadParquetDecimalShort(value.Pointer, value.Length, precision, scale);
				}
				return LogicalTypeConversions.ReadParquetDecimalNoOverflow(value.Pointer, value.Length, precision, scale);
			};
		}

		// Token: 0x06010C48 RID: 68680 RVA: 0x0039C488 File Offset: 0x0039A688
		public static Func<ByteArray, Number> ByteArrayToDecimalWithOverflow(int precision, int scale)
		{
			Func<ByteArray, Number> func;
			if (!LogicalTypeConversions.ValidateDecimalType<ByteArray, Number>(precision, scale, 2147483647, out func))
			{
				return func;
			}
			return delegate(ByteArray value)
			{
				if (value.Length < 0)
				{
					throw ValueException.NewDataFormatError<Message1>(Resources.LoadError(Resources.NegativeBinaryLength), NumberValue.New(value.Length), null);
				}
				if (value.Length == 0)
				{
					return new Number(0m);
				}
				if (value.Length <= 12)
				{
					return new Number(LogicalTypeConversions.ReadParquetDecimalShort(value.Pointer, value.Length, precision, scale));
				}
				return LogicalTypeConversions.ReadParquetDecimalWithOverflow(value.Pointer, value.Length, precision, scale);
			};
		}

		// Token: 0x06010C49 RID: 68681 RVA: 0x0039C4D4 File Offset: 0x0039A6D4
		public static Func<IAllocator, decimal, ByteArray> ByteArrayFromDecimalNoOverflow(int precision, int scale)
		{
			Func<decimal, ByteArray> errorConversion;
			if (!LogicalTypeConversions.ValidateDecimalType<decimal, ByteArray>(precision, scale, 2147483647, out errorConversion))
			{
				return (IAllocator allocator, decimal value) => errorConversion(value);
			}
			decimal max = LogicalTypeConversions.MaxOfDecimalRange(precision, scale);
			decimal min = -max;
			return delegate(IAllocator allocator, decimal value)
			{
				LogicalTypeConversions.ValidateInRange(value, min, max, precision, scale);
				BigInteger bigInteger = LogicalTypeConversions.UnscaleDecimalAsBigInteger(value, scale);
				byte[] array = bigInteger.ToByteArray();
				ByteArray byteArray = allocator.Allocate(array.Length);
				LogicalTypeConversions.WriteBigIntegerBigEndian(byteArray.Pointer, array.Length, bigInteger);
				return byteArray;
			};
		}

		// Token: 0x06010C4A RID: 68682 RVA: 0x0039C554 File Offset: 0x0039A754
		public static Func<IAllocator, Number, ByteArray> ByteArrayFromDecimalWithOverflow(int precision, int scale)
		{
			Func<IAllocator, decimal, ByteArray> func = LogicalTypeConversions.ByteArrayFromDecimalNoOverflow(precision, scale);
			return (IAllocator allocator, Number value) => func(allocator, value.ToDecimal());
		}

		// Token: 0x06010C4B RID: 68683 RVA: 0x0039C574 File Offset: 0x0039A774
		public static Microsoft.OleDb.Date Int32ToDate(int value)
		{
			return new Microsoft.OleDb.Date(new global::ParquetSharp.Date(value).DateTime);
		}

		// Token: 0x06010C4C RID: 68684 RVA: 0x0039C594 File Offset: 0x0039A794
		public static int Int32FromDate(IAllocator allocator, Microsoft.OleDb.Date date)
		{
			return new global::ParquetSharp.Date(date.DateTime).Days;
		}

		// Token: 0x06010C4D RID: 68685 RVA: 0x0039C5A7 File Offset: 0x0039A7A7
		public static Time Int32ToTimeMillis(int value)
		{
			return new Time(new TimeSpan(0, 0, 0, 0, value));
		}

		// Token: 0x06010C4E RID: 68686 RVA: 0x0039C5B8 File Offset: 0x0039A7B8
		public static int Int32FromTimeMillis(IAllocator allocator, Time time)
		{
			return checked((int)(time.TimeSpan.Ticks / 10000L));
		}

		// Token: 0x06010C4F RID: 68687 RVA: 0x0039C5DC File Offset: 0x0039A7DC
		public static Time Int64ToTimeMicros(long value)
		{
			return new Time(new TimeSpan(value * 10L));
		}

		// Token: 0x06010C50 RID: 68688 RVA: 0x0039C5F0 File Offset: 0x0039A7F0
		public static long Int64FromTimeMicros(IAllocator allocator, Time time)
		{
			return time.TimeSpan.Ticks / 10L;
		}

		// Token: 0x06010C51 RID: 68689 RVA: 0x0039C610 File Offset: 0x0039A810
		public static Time Int64ToTimeNanos(long value)
		{
			return new Time(new TimeSpanNanos(value).TimeSpan);
		}

		// Token: 0x06010C52 RID: 68690 RVA: 0x0039C630 File Offset: 0x0039A830
		public static long Int64FromTimeNanos(IAllocator allocator, Time time)
		{
			return new TimeSpanNanos(time.TimeSpan).Ticks;
		}

		// Token: 0x06010C53 RID: 68691 RVA: 0x0039C644 File Offset: 0x0039A844
		public static DateTime Int64ToTimestampMillis(long value)
		{
			return LogicalTypeConversions.UnixEpoch.AddTicks(value * 10000L);
		}

		// Token: 0x06010C54 RID: 68692 RVA: 0x0039C668 File Offset: 0x0039A868
		public static long Int64FromTimestampMillis(IAllocator allocator, DateTime dateTime)
		{
			return (dateTime - LogicalTypeConversions.UnixEpoch).Ticks / 10000L;
		}

		// Token: 0x06010C55 RID: 68693 RVA: 0x0039C690 File Offset: 0x0039A890
		public static DateTime Int64ToTimestampMicros(long value)
		{
			return LogicalTypeConversions.UnixEpoch.AddTicks(value * 10L);
		}

		// Token: 0x06010C56 RID: 68694 RVA: 0x0039C6B0 File Offset: 0x0039A8B0
		public static long Int64FromTimestampMicros(IAllocator allocator, DateTime dateTime)
		{
			return (dateTime - LogicalTypeConversions.UnixEpoch).Ticks / 10L;
		}

		// Token: 0x06010C57 RID: 68695 RVA: 0x0039C6D4 File Offset: 0x0039A8D4
		public static DateTime Int64ToTimestampNanos(long value)
		{
			return LogicalTypeConversions.UnixEpoch.Add(new TimeSpanNanos(value).TimeSpan);
		}

		// Token: 0x06010C58 RID: 68696 RVA: 0x0039C6FC File Offset: 0x0039A8FC
		public static long Int64FromTimestampNanos(IAllocator allocator, DateTime dateTime)
		{
			return new TimeSpanNanos(dateTime - LogicalTypeConversions.UnixEpoch).Ticks;
		}

		// Token: 0x06010C59 RID: 68697 RVA: 0x0039C714 File Offset: 0x0039A914
		public unsafe static DateTime Int96ToTimestampNanos(Int96 int96)
		{
			DateTime dateTime;
			try
			{
				long num = LogicalTypeConversions.ReadInt64LittleEndian((byte*)(&int96));
				int num2 = LogicalTypeConversions.ReadInt32LittleEndian((byte*)(&int96) + 8) - LogicalTypeConversions.UnixEpochJulianDay;
				dateTime = LogicalTypeConversions.UnixEpoch + new TimeSpan(num2, 0, 0, 0) + new TimeSpanNanos(num).TimeSpan;
			}
			catch (ArgumentOutOfRangeException ex)
			{
				throw ValueException.NumberOutOfRange<Message0>(Strings.DateTime_OutOfRangeError, Value.Null, ex);
			}
			return dateTime;
		}

		// Token: 0x06010C5A RID: 68698 RVA: 0x0039C78C File Offset: 0x0039A98C
		public unsafe static Int96 Int96FromTimestampNanos(IAllocator allocator, DateTime dateTime)
		{
			DateTime date = dateTime.Date;
			int days = (dateTime.Date - LogicalTypeConversions.UnixEpoch).Days;
			int num = LogicalTypeConversions.UnixEpochJulianDay + days;
			TimeSpanNanos timeSpanNanos = new TimeSpanNanos(dateTime.TimeOfDay);
			Int96 @int = default(Int96);
			LogicalTypeConversions.WriteInt64LittleEndian((byte*)(&@int), timeSpanNanos.Ticks);
			LogicalTypeConversions.WriteInt32LittleEndian((byte*)(&@int) + 8, num);
			return @int;
		}

		// Token: 0x06010C5B RID: 68699 RVA: 0x0039C7F4 File Offset: 0x0039A9F4
		public unsafe static ParquetInterval FixedLenByteArrayToInterval(FixedLenByteArray fixedLenByteArray)
		{
			if ((long)fixedLenByteArray.Pointer % (long)sizeof(ParquetInterval) == 0L)
			{
				return *(ParquetInterval*)(void*)fixedLenByteArray.Pointer;
			}
			byte* ptr = (byte*)(void*)fixedLenByteArray.Pointer;
			return new ParquetInterval((uint)LogicalTypeConversions.ReadInt32LittleEndian(ptr), (uint)LogicalTypeConversions.ReadInt32LittleEndian(ptr + 4), (uint)LogicalTypeConversions.ReadInt32LittleEndian(ptr + 8));
		}

		// Token: 0x06010C5C RID: 68700 RVA: 0x0039C850 File Offset: 0x0039AA50
		public unsafe static FixedLenByteArray FixedLenByteArrayFromInterval(IAllocator allocator, ParquetInterval interval)
		{
			ByteArray byteArray = allocator.Allocate(12);
			FixedLenByteArray fixedLenByteArray = new FixedLenByteArray(byteArray.Pointer);
			if ((long)fixedLenByteArray.Pointer % (long)sizeof(ParquetInterval) == 0L)
			{
				*(ParquetInterval*)(void*)fixedLenByteArray.Pointer = interval;
				return fixedLenByteArray;
			}
			byte* ptr = (byte*)(void*)fixedLenByteArray.Pointer;
			LogicalTypeConversions.WriteInt32LittleEndian(ptr, (int)interval.Months);
			LogicalTypeConversions.WriteInt32LittleEndian(ptr + 4, (int)interval.Days);
			LogicalTypeConversions.WriteInt32LittleEndian(ptr + 8, (int)interval.Milliseconds);
			return fixedLenByteArray;
		}

		// Token: 0x06010C5D RID: 68701 RVA: 0x0039C8D0 File Offset: 0x0039AAD0
		private unsafe static decimal ReadParquetDecimalShort(IntPtr ptr, int length, int precision, int scale)
		{
			Int96 @int;
			byte* ptr2 = (byte*)(&@int);
			int num = 12 - length;
			for (int i = 0; i < length; i++)
			{
				ptr2[num + i] = ((byte*)(void*)ptr)[i];
			}
			bool flag = length > 0 && (*(byte*)(void*)ptr & 128) == 128;
			if (flag)
			{
				int num2 = (length + 3) / 4 * 4;
				int num3 = 12 - num2;
				for (int j = num - 1; j >= num3; j--)
				{
					ptr2[j] = byte.MaxValue;
				}
				length = num2;
				num = num3;
			}
			Int96 int2 = new Int96(LogicalTypeConversions.ReadInt32BigEndian(ptr2), LogicalTypeConversions.ReadInt32BigEndian(ptr2 + 4), LogicalTypeConversions.ReadInt32BigEndian(ptr2 + 8));
			ptr2 = (byte*)(&int2);
			if (flag)
			{
				int num4 = length / 4;
				LogicalTypeConversions.TwosComplementBigEndian((uint*)(ptr2 + num), num4);
			}
			return new decimal(int2.C, int2.B, int2.A, flag, (byte)scale);
		}

		// Token: 0x06010C5E RID: 68702 RVA: 0x0039C9B0 File Offset: 0x0039ABB0
		private static decimal ReadParquetDecimalNoOverflow(IntPtr ptr, int length, int precision, int scale)
		{
			BigInteger bigInteger = LogicalTypeConversions.ReadBigIntegerBigEndian(ptr, length);
			decimal num;
			if (!LogicalTypeConversions.TryScaleBigIntegerAsDecimal(bigInteger, scale, out num))
			{
				throw ValueException.NumberOutOfRange<Message0>(Strings.NumberOutOfRangeDecimal, RecordValue.New(LogicalTypeConversions.ReadDecimalOutOfRangeKeys, new Value[]
				{
					NumberValue.New(precision),
					NumberValue.New(scale),
					TextValue.New(bigInteger.ToString())
				}), null);
			}
			return num;
		}

		// Token: 0x06010C5F RID: 68703 RVA: 0x0039CA14 File Offset: 0x0039AC14
		private static Number ReadParquetDecimalWithOverflow(IntPtr ptr, int length, int precision, int scale)
		{
			BigInteger bigInteger = LogicalTypeConversions.ReadBigIntegerBigEndian(ptr, length);
			decimal num;
			if (LogicalTypeConversions.TryScaleBigIntegerAsDecimal(bigInteger, scale, out num))
			{
				return new Number(num);
			}
			return new Number((double)bigInteger / Math.Pow(10.0, (double)scale));
		}

		// Token: 0x06010C60 RID: 68704 RVA: 0x0039CA58 File Offset: 0x0039AC58
		private static void WriteFixedLengthParquetDecimal(IntPtr ptr, int length, int precision, int scale, decimal value)
		{
			BigInteger bigInteger = LogicalTypeConversions.UnscaleDecimalAsBigInteger(value, scale);
			LogicalTypeConversions.WriteBigIntegerBigEndian(ptr, length, bigInteger);
		}

		// Token: 0x06010C61 RID: 68705 RVA: 0x0039CA78 File Offset: 0x0039AC78
		private static bool TryScaleBigIntegerAsDecimal(BigInteger mantissa, int scale, out decimal result)
		{
			while (scale >= 0 && (mantissa > LogicalTypeConversions.MaxDecimal || mantissa < LogicalTypeConversions.MinDecimal || scale > 28))
			{
				mantissa = BigInteger.Divide(mantissa, LogicalTypeConversions.Ten);
				scale--;
			}
			if (scale < 0)
			{
				result = 0m;
				return false;
			}
			result = LogicalTypeConversions.NewDecimal((decimal)BigInteger.Abs(mantissa), scale, mantissa.Sign < 0);
			return true;
		}

		// Token: 0x06010C62 RID: 68706 RVA: 0x0039CAEC File Offset: 0x0039ACEC
		private static decimal TryUnscaleDecimal(decimal decimalValue, int targetScale, out int remainingScale)
		{
			int decimalScale = LogicalTypeConversions.GetDecimalScale(decimalValue);
			if (targetScale <= decimalScale)
			{
				remainingScale = 0;
				return LogicalTypeConversions.NewDecimal(decimalValue, decimalScale - targetScale);
			}
			decimal num = LogicalTypeConversions.NewDecimal(decimalValue, 0);
			targetScale -= decimalScale;
			decimal num2 = 7922816251426433759354395033.5m;
			decimal num3 = -7922816251426433759354395033.5m;
			while (targetScale > 0 && num3 <= num && num <= num2)
			{
				num *= 10m;
				targetScale--;
			}
			remainingScale = targetScale;
			return num;
		}

		// Token: 0x06010C63 RID: 68707 RVA: 0x0039CB68 File Offset: 0x0039AD68
		private static BigInteger UnscaleDecimalAsBigInteger(decimal decimalValue, int targetScale)
		{
			BigInteger bigInteger = (BigInteger)LogicalTypeConversions.TryUnscaleDecimal(decimalValue, targetScale, out targetScale);
			while (targetScale > 0)
			{
				bigInteger *= LogicalTypeConversions.Ten;
				targetScale--;
			}
			return bigInteger;
		}

		// Token: 0x06010C64 RID: 68708 RVA: 0x0039CB9C File Offset: 0x0039AD9C
		private static bool ValidateDecimalType<TIn, TOut>(int precision, int scale, int maxPrecision, out Func<TIn, TOut> errorConversion)
		{
			if (precision < 1 || precision > maxPrecision)
			{
				errorConversion = delegate(TIn value)
				{
					throw ValueException.NewDataFormatError<Message0>(Resources.DecimalPrecisionInvalid, NumberValue.New(precision), null);
				};
				return false;
			}
			if (scale < 0 || scale > precision)
			{
				errorConversion = delegate(TIn value)
				{
					throw ValueException.NewDataFormatError<Message0>(Resources.DecimalScaleInvalid, NumberValue.New(scale), null);
				};
				return false;
			}
			errorConversion = null;
			return true;
		}

		// Token: 0x06010C65 RID: 68709 RVA: 0x0039CC0C File Offset: 0x0039AE0C
		private static void ValidateInRange(decimal value, decimal min, decimal max, int precision, int scale)
		{
			if (value < min || value > max)
			{
				throw ValueException.NumberOutOfRange<Message0>(Resources.DecimalOutOfRange, RecordValue.New(LogicalTypeConversions.WriteDecimalOutOfRangeKeys, new Value[]
				{
					NumberValue.New(precision),
					NumberValue.New(scale),
					NumberValue.New(value)
				}), null);
			}
		}

		// Token: 0x06010C66 RID: 68710 RVA: 0x0039CC63 File Offset: 0x0039AE63
		private static decimal NewDecimal(decimal signedUnscaled, int scale)
		{
			return LogicalTypeConversions.NewDecimal(signedUnscaled, scale, signedUnscaled < 0m);
		}

		// Token: 0x06010C67 RID: 68711 RVA: 0x0039CC78 File Offset: 0x0039AE78
		private unsafe static decimal NewDecimal(decimal unscaled, int scale, bool isNegative)
		{
			int* ptr = (int*)(&unscaled);
			*ptr = scale << 16;
			if (isNegative)
			{
				*ptr |= int.MinValue;
			}
			return unscaled;
		}

		// Token: 0x06010C68 RID: 68712 RVA: 0x0039CCA0 File Offset: 0x0039AEA0
		private unsafe static int GetDecimalScale(decimal decimalValue)
		{
			int* ptr = (int*)(&decimalValue);
			return (*ptr & int.MaxValue) >> 16;
		}

		// Token: 0x06010C69 RID: 68713 RVA: 0x0039CCBC File Offset: 0x0039AEBC
		private unsafe static short ReadInt16BigEndian(byte* bytes)
		{
			return BinaryFormat.Int16FromBigEndian((int)(*bytes), (int)bytes[1]);
		}

		// Token: 0x06010C6A RID: 68714 RVA: 0x0039CCC9 File Offset: 0x0039AEC9
		private unsafe static void WriteInt16BigEndian(byte* bytes, short value)
		{
			BinaryFormat.Int16ToBigEndian((int)value, out *bytes, out bytes[1]);
		}

		// Token: 0x06010C6B RID: 68715 RVA: 0x0039CCD5 File Offset: 0x0039AED5
		private unsafe static int ReadInt32BigEndian(byte* bytes)
		{
			return BinaryFormat.Int32FromBigEndian((int)(*bytes), (int)bytes[1], (int)bytes[2], (int)bytes[3]);
		}

		// Token: 0x06010C6C RID: 68716 RVA: 0x0039CCEA File Offset: 0x0039AEEA
		private unsafe static void WriteInt32BigEndian(byte* bytes, int value)
		{
			BinaryFormat.Int32ToBigEndian(value, out *bytes, out bytes[1], out bytes[2], out bytes[3]);
		}

		// Token: 0x06010C6D RID: 68717 RVA: 0x0039CCFC File Offset: 0x0039AEFC
		private unsafe static int ReadInt32LittleEndian(byte* bytes)
		{
			if ((long)((IntPtr)((void*)bytes)) % 4L == 0L)
			{
				return *(int*)bytes;
			}
			return BinaryFormat.Int32FromLittleEndian((int)(*bytes), (int)bytes[1], (int)bytes[2], (int)bytes[3]);
		}

		// Token: 0x06010C6E RID: 68718 RVA: 0x0039CD24 File Offset: 0x0039AF24
		private unsafe static void WriteInt32LittleEndian(byte* bytes, int value)
		{
			if ((long)((IntPtr)((void*)bytes)) % 4L == 0L)
			{
				*(int*)bytes = value;
				return;
			}
			BinaryFormat.Int32ToLittleEndian(value, out *bytes, out bytes[1], out bytes[2], out bytes[3]);
		}

		// Token: 0x06010C6F RID: 68719 RVA: 0x0039CD4C File Offset: 0x0039AF4C
		private unsafe static long ReadInt64BigEndian(byte* bytes)
		{
			return BinaryFormat.Int64FromBigEndian((long)((ulong)(*bytes)), (long)((ulong)bytes[1]), (long)((ulong)bytes[2]), (long)((ulong)bytes[3]), (long)((ulong)bytes[4]), (long)((ulong)bytes[5]), (long)((ulong)bytes[6]), (long)((ulong)bytes[7]));
		}

		// Token: 0x06010C70 RID: 68720 RVA: 0x0039CD84 File Offset: 0x0039AF84
		private unsafe static void WriteInt64BigEndian(byte* bytes, long value)
		{
			BinaryFormat.Int64ToBigEndian(value, out *bytes, out bytes[1], out bytes[2], out bytes[3], out bytes[4], out bytes[5], out bytes[6], out bytes[7]);
		}

		// Token: 0x06010C71 RID: 68721 RVA: 0x0039CDB0 File Offset: 0x0039AFB0
		private unsafe static long ReadInt64LittleEndian(byte* bytes)
		{
			if ((long)((IntPtr)((void*)bytes)) % 8L == 0L)
			{
				return *(long*)bytes;
			}
			return BinaryFormat.Int64FromLittleEndian((long)((ulong)(*bytes)), (long)((ulong)bytes[1]), (long)((ulong)bytes[2]), (long)((ulong)bytes[3]), (long)((ulong)bytes[4]), (long)((ulong)bytes[5]), (long)((ulong)bytes[6]), (long)((ulong)bytes[7]));
		}

		// Token: 0x06010C72 RID: 68722 RVA: 0x0039CDFC File Offset: 0x0039AFFC
		private unsafe static void WriteInt64LittleEndian(byte* bytes, long value)
		{
			if ((long)((IntPtr)((void*)bytes)) % 8L == 0L)
			{
				*(long*)bytes = value;
				return;
			}
			BinaryFormat.Int64ToLittleEndian(value, out *bytes, out bytes[1], out bytes[2], out bytes[3], out bytes[4], out bytes[5], out bytes[6], out bytes[7]);
		}

		// Token: 0x06010C73 RID: 68723 RVA: 0x0039CE39 File Offset: 0x0039B039
		private static BigInteger ReadBigIntegerBigEndian(IntPtr bytes, int length)
		{
			return new BigInteger(LogicalTypeConversions.ReadBytesReversed(bytes, length));
		}

		// Token: 0x06010C74 RID: 68724 RVA: 0x0039CE48 File Offset: 0x0039B048
		private unsafe static void WriteBigIntegerBigEndian(IntPtr bytes, int length, BigInteger bigInt)
		{
			byte[] array = bigInt.ToByteArray();
			if (array.Length > length)
			{
				throw ValueException.NumberOutOfRange<Message0>(Resources.DecimalPrecisionInvalid, Value.Null, null);
			}
			int num = length - array.Length;
			byte* ptr = (byte*)(void*)bytes;
			byte b = ((bigInt.Sign < 0) ? byte.MaxValue : 0);
			for (int i = 0; i < num; i++)
			{
				ptr[i] = b;
			}
			LogicalTypeConversions.WriteBytesReversed(ptr + num, array.Length, array);
		}

		// Token: 0x06010C75 RID: 68725 RVA: 0x0039CEB8 File Offset: 0x0039B0B8
		private unsafe static byte[] ReadBytesReversed(IntPtr bytes, int length)
		{
			byte[] array = new byte[length];
			byte* ptr = (byte*)(void*)bytes;
			for (int i = 0; i < length; i++)
			{
				array[length - i - 1] = ptr[i];
			}
			return array;
		}

		// Token: 0x06010C76 RID: 68726 RVA: 0x0039CEEC File Offset: 0x0039B0EC
		private unsafe static void WriteBytesReversed(byte* bytes, int length, byte[] source)
		{
			for (int i = 0; i < length; i++)
			{
				bytes[i] = source[length - i - 1];
			}
		}

		// Token: 0x06010C77 RID: 68727 RVA: 0x0039CF10 File Offset: 0x0039B110
		private unsafe static void TwosComplementBigEndian(uint* words, int length)
		{
			if (words != null && length > 0)
			{
				words[length - 1] = ~words[length - 1] + 1U;
				int i = length - 2;
				while (words[i + 1] == 0U)
				{
					if (i < 0)
					{
						break;
					}
					words[i] = ~words[i] + 1U;
					i--;
				}
				while (i >= 0)
				{
					words[i] = ~words[i];
					i--;
				}
			}
		}

		// Token: 0x04006466 RID: 25702
		public const int UuidLength = 16;

		// Token: 0x04006467 RID: 25703
		public const int IntervalLength = 12;

		// Token: 0x04006468 RID: 25704
		public const int MaxNoOverflowPrecision = 28;

		// Token: 0x04006469 RID: 25705
		public const int MaxInt32Precision = 9;

		// Token: 0x0400646A RID: 25706
		public const int MaxInt64Precision = 18;

		// Token: 0x0400646B RID: 25707
		public const int MaxDecimalPrecision = 29;

		// Token: 0x0400646C RID: 25708
		public const int MaxDecimalScale = 28;

		// Token: 0x0400646D RID: 25709
		private const int MaxDecimalLength = 12;

		// Token: 0x0400646E RID: 25710
		private const int DecimalSignMask = -2147483648;

		// Token: 0x0400646F RID: 25711
		private const long TicksPerMicrosecond = 10L;

		// Token: 0x04006470 RID: 25712
		private static readonly Keys ReadDecimalOutOfRangeKeys = Keys.New("Precision", "Scale", "UnscaledNumber");

		// Token: 0x04006471 RID: 25713
		private static readonly Keys WriteDecimalOutOfRangeKeys = Keys.New("Precision", "Scale", "Value");

		// Token: 0x04006472 RID: 25714
		private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0);

		// Token: 0x04006473 RID: 25715
		private static readonly int UnixEpochJulianDay = 2440588;

		// Token: 0x04006474 RID: 25716
		private static readonly BigInteger MaxDecimal = new BigInteger(decimal.MaxValue);

		// Token: 0x04006475 RID: 25717
		private static readonly BigInteger MinDecimal = new BigInteger(decimal.MinValue);

		// Token: 0x04006476 RID: 25718
		private static readonly BigInteger Ten = new BigInteger(10);
	}
}
