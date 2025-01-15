using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace ParquetSharp
{
	// Token: 0x02000073 RID: 115
	public static class LogicalWrite
	{
		// Token: 0x060002E0 RID: 736 RVA: 0x0000BCF4 File Offset: 0x00009EF4
		[return: Nullable(1)]
		public static Delegate GetNativeConverter<[IsUnmanaged] TTLogical, [IsUnmanaged] TTPhysical>() where TTLogical : struct where TTPhysical : struct
		{
			return new LogicalWrite<TTLogical, TTPhysical>.Converter(delegate(ReadOnlySpan<TTLogical> s, Span<short> _, Span<TTPhysical> d, short _)
			{
				LogicalWrite.ConvertNative<TTLogical>(s, MemoryMarshal.Cast<TTPhysical, TTLogical>(d));
			});
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000BD18 File Offset: 0x00009F18
		[return: Nullable(1)]
		public static Delegate GetNullableNativeConverter<[IsUnmanaged] TTLogical, [IsUnmanaged] TTPhysical>() where TTLogical : struct where TTPhysical : struct
		{
			return new LogicalWrite<TTLogical?, TTPhysical>.Converter(delegate(ReadOnlySpan<TTLogical?> s, Span<short> dl, Span<TTPhysical> d, short nl)
			{
				LogicalWrite.ConvertNative<TTLogical>(s, dl, MemoryMarshal.Cast<TTPhysical, TTLogical>(d), nl);
			});
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000BD3C File Offset: 0x00009F3C
		public static void ConvertNative<[IsUnmanaged] TValue>(ReadOnlySpan<TValue> source, Span<TValue> destination) where TValue : struct
		{
			source.CopyTo(destination);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000BD48 File Offset: 0x00009F48
		public unsafe static void ConvertNative<TValue>(ReadOnlySpan<TValue?> source, Span<short> defLevels, Span<TValue> destination, short nullLevel) where TValue : struct
		{
			int i = 0;
			int num = 0;
			while (i < source.Length)
			{
				TValue? tvalue = *source[i];
				if (tvalue == null)
				{
					*defLevels[i] = nullLevel;
				}
				else
				{
					*destination[num++] = tvalue.Value;
					*defLevels[i] = nullLevel + 1;
				}
				i++;
			}
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000BDC0 File Offset: 0x00009FC0
		public unsafe static void ConvertInt8(ReadOnlySpan<sbyte> source, Span<int> destination)
		{
			for (int i = 0; i < source.Length; i++)
			{
				*destination[i] = (int)(*source[i]);
			}
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000BDF8 File Offset: 0x00009FF8
		public unsafe static void ConvertInt8(ReadOnlySpan<sbyte?> source, Span<short> defLevels, Span<int> destination, short nullLevel)
		{
			int i = 0;
			int num = 0;
			while (i < source.Length)
			{
				sbyte? b = *source[i];
				if (b == null)
				{
					*defLevels[i] = nullLevel;
				}
				else
				{
					*destination[num++] = (int)b.Value;
					*defLevels[i] = nullLevel + 1;
				}
				i++;
			}
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000BE6C File Offset: 0x0000A06C
		public unsafe static void ConvertUInt8(ReadOnlySpan<byte> source, Span<int> destination)
		{
			for (int i = 0; i < source.Length; i++)
			{
				*destination[i] = (int)(*source[i]);
			}
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000BEA4 File Offset: 0x0000A0A4
		public unsafe static void ConvertUInt8(ReadOnlySpan<byte?> source, Span<short> defLevels, Span<int> destination, short nullLevel)
		{
			int i = 0;
			int num = 0;
			while (i < source.Length)
			{
				byte? b = *source[i];
				if (b == null)
				{
					*defLevels[i] = nullLevel;
				}
				else
				{
					*destination[num++] = (int)b.Value;
					*defLevels[i] = nullLevel + 1;
				}
				i++;
			}
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000BF18 File Offset: 0x0000A118
		public unsafe static void ConvertInt16(ReadOnlySpan<short> source, Span<int> destination)
		{
			for (int i = 0; i < source.Length; i++)
			{
				*destination[i] = (int)(*source[i]);
			}
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000BF50 File Offset: 0x0000A150
		public unsafe static void ConvertInt16(ReadOnlySpan<short?> source, Span<short> defLevels, Span<int> destination, short nullLevel)
		{
			int i = 0;
			int num = 0;
			while (i < source.Length)
			{
				short? num2 = *source[i];
				if (num2 == null)
				{
					*defLevels[i] = nullLevel;
				}
				else
				{
					*destination[num++] = (int)num2.Value;
					*defLevels[i] = nullLevel + 1;
				}
				i++;
			}
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000BFC4 File Offset: 0x0000A1C4
		public unsafe static void ConvertUInt16(ReadOnlySpan<ushort> source, Span<int> destination)
		{
			for (int i = 0; i < source.Length; i++)
			{
				*destination[i] = (int)(*source[i]);
			}
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000BFFC File Offset: 0x0000A1FC
		public unsafe static void ConvertUInt16(ReadOnlySpan<ushort?> source, Span<short> defLevels, Span<int> destination, short nullLevel)
		{
			int i = 0;
			int num = 0;
			while (i < source.Length)
			{
				ushort? num2 = *source[i];
				if (num2 == null)
				{
					*defLevels[i] = nullLevel;
				}
				else
				{
					*destination[num++] = (int)num2.Value;
					*defLevels[i] = nullLevel + 1;
				}
				i++;
			}
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000C070 File Offset: 0x0000A270
		public unsafe static void ConvertDecimal128(ReadOnlySpan<decimal> source, Span<FixedLenByteArray> destination, decimal multiplier, [Nullable(1)] ByteBuffer byteBuffer)
		{
			for (int i = 0; i < source.Length; i++)
			{
				*destination[i] = LogicalWrite.FromDecimal(*source[i], multiplier, byteBuffer);
			}
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000C0B8 File Offset: 0x0000A2B8
		public unsafe static void ConvertDecimal128(ReadOnlySpan<decimal?> source, Span<short> defLevels, Span<FixedLenByteArray> destination, decimal multiplier, short nullLevel, [Nullable(1)] ByteBuffer byteBuffer)
		{
			int i = 0;
			int num = 0;
			while (i < source.Length)
			{
				decimal? num2 = *source[i];
				if (num2 == null)
				{
					*defLevels[i] = nullLevel;
				}
				else
				{
					*destination[num++] = LogicalWrite.FromDecimal(num2.Value, multiplier, byteBuffer);
					*defLevels[i] = nullLevel + 1;
				}
				i++;
			}
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000C138 File Offset: 0x0000A338
		public unsafe static void ConvertUuid(ReadOnlySpan<Guid> source, Span<FixedLenByteArray> destination, [Nullable(1)] ByteBuffer byteBuffer)
		{
			for (int i = 0; i < source.Length; i++)
			{
				*destination[i] = LogicalWrite.FromUuid(*source[i], byteBuffer);
			}
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000C180 File Offset: 0x0000A380
		public unsafe static void ConvertUuid(ReadOnlySpan<Guid?> source, Span<short> defLevels, Span<FixedLenByteArray> destination, short nullLevel, [Nullable(1)] ByteBuffer byteBuffer)
		{
			int i = 0;
			int num = 0;
			while (i < source.Length)
			{
				Guid? guid = *source[i];
				if (guid == null)
				{
					*defLevels[i] = nullLevel;
				}
				else
				{
					*destination[num++] = LogicalWrite.FromUuid(guid.Value, byteBuffer);
					*defLevels[i] = nullLevel + 1;
				}
				i++;
			}
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000C200 File Offset: 0x0000A400
		public unsafe static void ConvertDateTimeMicros(ReadOnlySpan<DateTime> source, Span<long> destination)
		{
			for (int i = 0; i < source.Length; i++)
			{
				*destination[i] = LogicalWrite.FromDateTimeMicros(*source[i]);
			}
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000C244 File Offset: 0x0000A444
		public unsafe static void ConvertDateTimeMicros(ReadOnlySpan<DateTime?> source, Span<short> defLevels, Span<long> destination, short nullLevel)
		{
			int i = 0;
			int num = 0;
			while (i < source.Length)
			{
				DateTime? dateTime = *source[i];
				if (dateTime == null)
				{
					*defLevels[i] = nullLevel;
				}
				else
				{
					*destination[num++] = LogicalWrite.FromDateTimeMicros(dateTime.Value);
					*defLevels[i] = nullLevel + 1;
				}
				i++;
			}
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000C2BC File Offset: 0x0000A4BC
		public unsafe static void ConvertDateTimeMillis(ReadOnlySpan<DateTime> source, Span<long> destination)
		{
			for (int i = 0; i < source.Length; i++)
			{
				*destination[i] = LogicalWrite.FromDateTimeMillis(*source[i]);
			}
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000C300 File Offset: 0x0000A500
		public unsafe static void ConvertDateTimeMillis(ReadOnlySpan<DateTime?> source, Span<short> defLevels, Span<long> destination, short nullLevel)
		{
			int i = 0;
			int num = 0;
			while (i < source.Length)
			{
				DateTime? dateTime = *source[i];
				if (dateTime == null)
				{
					*defLevels[i] = nullLevel;
				}
				else
				{
					*destination[num++] = LogicalWrite.FromDateTimeMillis(dateTime.Value);
					*defLevels[i] = nullLevel + 1;
				}
				i++;
			}
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000C378 File Offset: 0x0000A578
		public unsafe static void ConvertTimeSpanMicros(ReadOnlySpan<TimeSpan> source, Span<long> destination)
		{
			for (int i = 0; i < source.Length; i++)
			{
				*destination[i] = LogicalWrite.FromTimeSpanMicros(*source[i]);
			}
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000C3BC File Offset: 0x0000A5BC
		public unsafe static void ConvertTimeSpanMicros(ReadOnlySpan<TimeSpan?> source, Span<short> defLevels, Span<long> destination, short nullLevel)
		{
			int i = 0;
			int num = 0;
			while (i < source.Length)
			{
				TimeSpan? timeSpan = *source[i];
				if (timeSpan == null)
				{
					*defLevels[i] = nullLevel;
				}
				else
				{
					*destination[num++] = LogicalWrite.FromTimeSpanMicros(timeSpan.Value);
					*defLevels[i] = nullLevel + 1;
				}
				i++;
			}
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000C434 File Offset: 0x0000A634
		public unsafe static void ConvertTimeSpanMillis(ReadOnlySpan<TimeSpan> source, Span<int> destination)
		{
			for (int i = 0; i < source.Length; i++)
			{
				*destination[i] = LogicalWrite.FromTimeSpanMillis(*source[i]);
			}
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000C478 File Offset: 0x0000A678
		public unsafe static void ConvertTimeSpanMillis(ReadOnlySpan<TimeSpan?> source, Span<short> defLevels, Span<int> destination, short nullLevel)
		{
			int i = 0;
			int num = 0;
			while (i < source.Length)
			{
				TimeSpan? timeSpan = *source[i];
				if (timeSpan == null)
				{
					*defLevels[i] = nullLevel;
				}
				else
				{
					*destination[num++] = LogicalWrite.FromTimeSpanMillis(timeSpan.Value);
					*defLevels[i] = nullLevel + 1;
				}
				i++;
			}
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000C4F0 File Offset: 0x0000A6F0
		public unsafe static void ConvertString([Nullable(new byte[] { 0, 1 })] ReadOnlySpan<string> source, Span<short> defLevels, Span<ByteArray> destination, short nullLevel, [Nullable(1)] ByteBuffer byteBuffer)
		{
			int i = 0;
			int num = 0;
			while (i < source.Length)
			{
				string text = *source[i];
				if (text == null)
				{
					if (defLevels.IsEmpty)
					{
						throw new ArgumentException("encountered null value despite column schema node repetition being marked as required");
					}
					*defLevels[i] = nullLevel;
				}
				else
				{
					*destination[num++] = LogicalWrite.FromString(text, byteBuffer);
					if (!defLevels.IsEmpty)
					{
						*defLevels[i] = nullLevel + 1;
					}
				}
				i++;
			}
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000C580 File Offset: 0x0000A780
		public unsafe static void ConvertByteArray([Nullable(new byte[] { 0, 1 })] ReadOnlySpan<byte[]> source, Span<short> defLevels, Span<ByteArray> destination, short nullLevel, [Nullable(1)] ByteBuffer byteBuffer)
		{
			int i = 0;
			int num = 0;
			while (i < source.Length)
			{
				byte[] array = *source[i];
				if (array == null)
				{
					if (defLevels.IsEmpty)
					{
						throw new ArgumentException("encountered null value despite column schema node repetition being marked as required");
					}
					*defLevels[i] = nullLevel;
				}
				else
				{
					*destination[num++] = LogicalWrite.FromByteArray(array, byteBuffer);
					if (!defLevels.IsEmpty)
					{
						*defLevels[i] = nullLevel + 1;
					}
				}
				i++;
			}
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000C610 File Offset: 0x0000A810
		[NullableContext(1)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static FixedLenByteArray FromDecimal(decimal source, decimal multiplier, ByteBuffer byteBuffer)
		{
			Decimal128 @decimal = new Decimal128(source, multiplier);
			return LogicalWrite.FromFixedLength<Decimal128>(in @decimal, byteBuffer);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000C634 File Offset: 0x0000A834
		[NullableContext(1)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static FixedLenByteArray FromUuid(Guid uuid, ByteBuffer byteBuffer)
		{
			FixedLenByteArray fixedLenByteArray = LogicalWrite.FromFixedLength<Guid>(in uuid, byteBuffer);
			byte* ptr = (byte*)(void*)fixedLenByteArray.Pointer;
			if (BitConverter.IsLittleEndian)
			{
				LogicalWrite.<FromUuid>g__Swap|27_0<byte>(ref *ptr, ref ptr[3]);
				LogicalWrite.<FromUuid>g__Swap|27_0<byte>(ref ptr[1], ref ptr[2]);
				LogicalWrite.<FromUuid>g__Swap|27_0<byte>(ref ptr[4], ref ptr[5]);
				LogicalWrite.<FromUuid>g__Swap|27_0<byte>(ref ptr[6], ref ptr[7]);
			}
			return fixedLenByteArray;
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000C690 File Offset: 0x0000A890
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long FromDateTimeMicros(DateTime source)
		{
			return (source.Ticks - 621355968000000000L) / 10L;
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000C6A8 File Offset: 0x0000A8A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long FromDateTimeMillis(DateTime source)
		{
			return (source.Ticks - 621355968000000000L) / 10000L;
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000C6C4 File Offset: 0x0000A8C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long FromTimeSpanMicros(TimeSpan source)
		{
			return source.Ticks / 10L;
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000C6D4 File Offset: 0x0000A8D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int FromTimeSpanMillis(TimeSpan source)
		{
			return (int)(source.Ticks / 10000L);
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000C6E8 File Offset: 0x0000A8E8
		[NullableContext(1)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static ByteArray FromString(string str, ByteBuffer byteBuffer)
		{
			Encoding utf = Encoding.UTF8;
			int byteCount = utf.GetByteCount(str);
			ByteArray byteArray = byteBuffer.Allocate(byteCount);
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

		// Token: 0x06000301 RID: 769 RVA: 0x0000C740 File Offset: 0x0000A940
		[NullableContext(1)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static ByteArray FromByteArray(byte[] array, ByteBuffer byteBuffer)
		{
			ByteArray byteArray = byteBuffer.Allocate(array.Length);
			fixed (byte[] array2 = array)
			{
				byte* ptr;
				if (array == null || array2.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array2[0];
				}
				Buffer.MemoryCopy((void*)ptr, (void*)byteArray.Pointer, (long)byteArray.Length, (long)byteArray.Length);
			}
			return byteArray;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000C7A0 File Offset: 0x0000A9A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static FixedLenByteArray FromFixedLength<[IsUnmanaged] TValue>(in TValue value, [Nullable(1)] ByteBuffer byteBuffer) where TValue : struct
		{
			ByteArray byteArray = byteBuffer.Allocate(sizeof(TValue));
			*(TValue*)(void*)byteArray.Pointer = value;
			return new FixedLenByteArray(byteArray.Pointer);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000C7D0 File Offset: 0x0000A9D0
		[CompilerGenerated]
		internal static void <FromUuid>g__Swap|27_0<T>(ref T lhs, ref T rhs)
		{
			T t = lhs;
			lhs = rhs;
			rhs = t;
		}

		// Token: 0x040000D7 RID: 215
		public const long DateTimeOffset = 621355968000000000L;
	}
}
