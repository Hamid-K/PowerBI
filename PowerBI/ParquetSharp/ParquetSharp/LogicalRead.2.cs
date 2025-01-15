using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace ParquetSharp
{
	// Token: 0x0200005D RID: 93
	public static class LogicalRead
	{
		// Token: 0x06000257 RID: 599 RVA: 0x00009398 File Offset: 0x00007598
		[return: Nullable(1)]
		public static Delegate GetDirectReader<[IsUnmanaged] TTLogical, [IsUnmanaged] TTPhysical>() where TTLogical : struct where TTPhysical : struct
		{
			return new LogicalRead<TTLogical, TTPhysical>.DirectReader(([Nullable(new byte[] { 1, 0 })] ColumnReader<TTPhysical> r, Span<TTLogical> d) => LogicalRead.ReadDirect<TTPhysical>(r, MemoryMarshal.Cast<TTLogical, TTPhysical>(d)));
		}

		// Token: 0x06000258 RID: 600 RVA: 0x000093BC File Offset: 0x000075BC
		[return: Nullable(1)]
		public static Delegate GetNativeConverter<[IsUnmanaged] TTLogical, [IsUnmanaged] TTPhysical>() where TTLogical : struct where TTPhysical : struct
		{
			return new LogicalRead<TTLogical, TTPhysical>.Converter(delegate(ReadOnlySpan<TTPhysical> s, ReadOnlySpan<short> _, Span<TTLogical> d, short _)
			{
				LogicalRead.ConvertNative<TTLogical>(MemoryMarshal.Cast<TTPhysical, TTLogical>(s), d);
			});
		}

		// Token: 0x06000259 RID: 601 RVA: 0x000093E0 File Offset: 0x000075E0
		[return: Nullable(1)]
		public static Delegate GetNullableNativeConverter<[IsUnmanaged] TTLogical, [IsUnmanaged] TTPhysical>() where TTLogical : struct where TTPhysical : struct
		{
			return new LogicalRead<TTLogical?, TTPhysical>.Converter(delegate(ReadOnlySpan<TTPhysical> s, ReadOnlySpan<short> dl, Span<TTLogical?> d, short del)
			{
				LogicalRead.ConvertNative<TTLogical>(MemoryMarshal.Cast<TTPhysical, TTLogical>(s), dl, d, del);
			});
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00009404 File Offset: 0x00007604
		public static long ReadDirect<[IsUnmanaged] TPhys>([Nullable(new byte[] { 1, 0 })] ColumnReader<TPhys> r, Span<TPhys> d) where TPhys : struct
		{
			long num2;
			long num = r.ReadBatch((long)d.Length, d, out num2);
			if (num != num2)
			{
				throw new Exception(string.Format("returned values do not match ({0} != {1}", num, num2));
			}
			return num;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000944C File Offset: 0x0000764C
		public static void ConvertNative<[IsUnmanaged] TValue>(ReadOnlySpan<TValue> source, Span<TValue> destination) where TValue : struct
		{
			source.CopyTo(destination);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00009458 File Offset: 0x00007658
		public unsafe static void ConvertNative<[IsUnmanaged] TValue>(ReadOnlySpan<TValue> source, ReadOnlySpan<short> defLevels, Span<TValue?> destination, short definedLevel) where TValue : struct
		{
			int i = 0;
			int num = 0;
			while (i < destination.Length)
			{
				*destination[i] = ((defLevels.IsEmpty || *defLevels[i] == definedLevel) ? new TValue?(*source[num++]) : null);
				i++;
			}
		}

		// Token: 0x0600025D RID: 605 RVA: 0x000094CC File Offset: 0x000076CC
		public unsafe static void ConvertInt8(ReadOnlySpan<int> source, Span<sbyte> destination)
		{
			for (int i = 0; i < destination.Length; i++)
			{
				*destination[i] = (sbyte)(*source[i]);
			}
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00009508 File Offset: 0x00007708
		public unsafe static void ConvertInt8(ReadOnlySpan<int> source, ReadOnlySpan<short> defLevels, Span<sbyte?> destination, short definedLevel)
		{
			int i = 0;
			int num = 0;
			while (i < destination.Length)
			{
				*destination[i] = ((*defLevels[i] != definedLevel) ? null : new sbyte?((sbyte)(*source[num++])));
				i++;
			}
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00009570 File Offset: 0x00007770
		public unsafe static void ConvertUInt8(ReadOnlySpan<int> source, Span<byte> destination)
		{
			for (int i = 0; i < destination.Length; i++)
			{
				*destination[i] = (byte)(*source[i]);
			}
		}

		// Token: 0x06000260 RID: 608 RVA: 0x000095AC File Offset: 0x000077AC
		public unsafe static void ConvertUInt8(ReadOnlySpan<int> source, ReadOnlySpan<short> defLevels, Span<byte?> destination, short definedLevel)
		{
			int i = 0;
			int num = 0;
			while (i < destination.Length)
			{
				*destination[i] = ((*defLevels[i] != definedLevel) ? null : new byte?((byte)(*source[num++])));
				i++;
			}
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00009614 File Offset: 0x00007814
		public unsafe static void ConvertInt16(ReadOnlySpan<int> source, Span<short> destination)
		{
			for (int i = 0; i < destination.Length; i++)
			{
				*destination[i] = (short)(*source[i]);
			}
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00009650 File Offset: 0x00007850
		public unsafe static void ConvertInt16(ReadOnlySpan<int> source, ReadOnlySpan<short> defLevels, Span<short?> destination, short definedLevel)
		{
			int i = 0;
			int num = 0;
			while (i < destination.Length)
			{
				*destination[i] = ((*defLevels[i] != definedLevel) ? null : new short?((short)(*source[num++])));
				i++;
			}
		}

		// Token: 0x06000263 RID: 611 RVA: 0x000096B8 File Offset: 0x000078B8
		public unsafe static void ConvertUInt16(ReadOnlySpan<int> source, Span<ushort> destination)
		{
			for (int i = 0; i < destination.Length; i++)
			{
				*destination[i] = (ushort)(*source[i]);
			}
		}

		// Token: 0x06000264 RID: 612 RVA: 0x000096F4 File Offset: 0x000078F4
		public unsafe static void ConvertUInt16(ReadOnlySpan<int> source, ReadOnlySpan<short> defLevels, Span<ushort?> destination, short definedLevel)
		{
			int i = 0;
			int num = 0;
			while (i < destination.Length)
			{
				*destination[i] = ((*defLevels[i] != definedLevel) ? null : new ushort?((ushort)(*source[num++])));
				i++;
			}
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000975C File Offset: 0x0000795C
		public unsafe static void ConvertDecimal32(ReadOnlySpan<int> source, Span<decimal> destination, decimal multiplier)
		{
			for (int i = 0; i < destination.Length; i++)
			{
				*destination[i] = *source[i] / multiplier;
			}
		}

		// Token: 0x06000266 RID: 614 RVA: 0x000097A4 File Offset: 0x000079A4
		public unsafe static void ConvertDecimal32(ReadOnlySpan<int> source, ReadOnlySpan<short> defLevels, Span<decimal?> destination, decimal multiplier, short definedLevel)
		{
			int i = 0;
			int num = 0;
			while (i < destination.Length)
			{
				*destination[i] = ((*defLevels[i] != definedLevel) ? null : new decimal?(*source[num++] / multiplier));
				i++;
			}
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00009814 File Offset: 0x00007A14
		public unsafe static void ConvertDecimal64(ReadOnlySpan<long> source, Span<decimal> destination, decimal multiplier)
		{
			for (int i = 0; i < destination.Length; i++)
			{
				*destination[i] = *source[i] / multiplier;
			}
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000985C File Offset: 0x00007A5C
		public unsafe static void ConvertDecimal64(ReadOnlySpan<long> source, ReadOnlySpan<short> defLevels, Span<decimal?> destination, decimal multiplier, short definedLevel)
		{
			int i = 0;
			int num = 0;
			while (i < destination.Length)
			{
				*destination[i] = ((*defLevels[i] != definedLevel) ? null : new decimal?(*source[num++] / multiplier));
				i++;
			}
		}

		// Token: 0x06000269 RID: 617 RVA: 0x000098CC File Offset: 0x00007ACC
		public unsafe static void ConvertDecimal128(ReadOnlySpan<FixedLenByteArray> source, Span<decimal> destination, decimal multiplier)
		{
			for (int i = 0; i < destination.Length; i++)
			{
				*destination[i] = LogicalRead.ToDecimal(*source[i], multiplier);
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00009914 File Offset: 0x00007B14
		public unsafe static void ConvertDecimal128(ReadOnlySpan<FixedLenByteArray> source, ReadOnlySpan<short> defLevels, Span<decimal?> destination, decimal multiplier, short definedLevel)
		{
			int i = 0;
			int num = 0;
			while (i < destination.Length)
			{
				*destination[i] = ((*defLevels[i] != definedLevel) ? null : new decimal?(LogicalRead.ToDecimal(*source[num++], multiplier)));
				i++;
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00009984 File Offset: 0x00007B84
		public unsafe static void ConvertUuid(ReadOnlySpan<FixedLenByteArray> source, Span<Guid> destination)
		{
			for (int i = 0; i < destination.Length; i++)
			{
				*destination[i] = LogicalRead.ToUuid(*source[i]);
			}
		}

		// Token: 0x0600026C RID: 620 RVA: 0x000099CC File Offset: 0x00007BCC
		public unsafe static void ConvertUuid(ReadOnlySpan<FixedLenByteArray> source, ReadOnlySpan<short> defLevels, Span<Guid?> destination, short definedLevel)
		{
			int i = 0;
			int num = 0;
			while (i < destination.Length)
			{
				*destination[i] = ((*defLevels[i] != definedLevel) ? null : new Guid?(LogicalRead.ToUuid(*source[num++])));
				i++;
			}
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00009A3C File Offset: 0x00007C3C
		public unsafe static void ConvertDateTimeMicros(ReadOnlySpan<long> source, Span<DateTime> destination, DateTimeKind kind = DateTimeKind.Unspecified)
		{
			for (int i = 0; i < destination.Length; i++)
			{
				*destination[i] = new DateTime(LogicalRead.ToDateTimeMicrosTicks(*source[i]), kind);
			}
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00009A84 File Offset: 0x00007C84
		public unsafe static void ConvertDateTimeMicros(ReadOnlySpan<long> source, ReadOnlySpan<short> defLevels, Span<DateTime?> destination, short definedLevel, DateTimeKind kind = DateTimeKind.Unspecified)
		{
			int i = 0;
			int num = 0;
			while (i < destination.Length)
			{
				*destination[i] = ((*defLevels[i] != definedLevel) ? null : new DateTime?(new DateTime(LogicalRead.ToDateTimeMicrosTicks(*source[num++]), kind)));
				i++;
			}
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00009AF4 File Offset: 0x00007CF4
		public unsafe static void ConvertDateTimeMillis(ReadOnlySpan<long> source, Span<DateTime> destination, DateTimeKind kind = DateTimeKind.Unspecified)
		{
			for (int i = 0; i < destination.Length; i++)
			{
				*destination[i] = new DateTime(LogicalRead.ToDateTimeMillisTicks(*source[i]), kind);
			}
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00009B3C File Offset: 0x00007D3C
		public unsafe static void ConvertDateTimeMillis(ReadOnlySpan<long> source, ReadOnlySpan<short> defLevels, Span<DateTime?> destination, short definedLevel, DateTimeKind kind = DateTimeKind.Unspecified)
		{
			int i = 0;
			int num = 0;
			while (i < destination.Length)
			{
				*destination[i] = ((*defLevels[i] != definedLevel) ? null : new DateTime?(new DateTime(LogicalRead.ToDateTimeMillisTicks(*source[num++]), kind)));
				i++;
			}
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00009BAC File Offset: 0x00007DAC
		public unsafe static void ConvertTimeSpanMicros(ReadOnlySpan<long> source, Span<TimeSpan> destination)
		{
			for (int i = 0; i < destination.Length; i++)
			{
				*destination[i] = LogicalRead.ToTimeSpanMicros(*source[i]);
			}
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00009BF0 File Offset: 0x00007DF0
		public unsafe static void ConvertTimeSpanMicros(ReadOnlySpan<long> source, ReadOnlySpan<short> defLevels, Span<TimeSpan?> destination, short definedLevel)
		{
			int i = 0;
			int num = 0;
			while (i < destination.Length)
			{
				*destination[i] = ((*defLevels[i] != definedLevel) ? null : new TimeSpan?(LogicalRead.ToTimeSpanMicros(*source[num++])));
				i++;
			}
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00009C5C File Offset: 0x00007E5C
		public unsafe static void ConvertTimeSpanMillis(ReadOnlySpan<int> source, Span<TimeSpan> destination)
		{
			for (int i = 0; i < destination.Length; i++)
			{
				*destination[i] = LogicalRead.ToTimeSpanMillis(*source[i]);
			}
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00009CA0 File Offset: 0x00007EA0
		public unsafe static void ConvertTimeSpanMillis(ReadOnlySpan<int> source, ReadOnlySpan<short> defLevels, Span<TimeSpan?> destination, short definedLevel)
		{
			int i = 0;
			int num = 0;
			while (i < destination.Length)
			{
				*destination[i] = ((*defLevels[i] != definedLevel) ? null : new TimeSpan?(LogicalRead.ToTimeSpanMillis(*source[num++])));
				i++;
			}
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00009D0C File Offset: 0x00007F0C
		public unsafe static void ConvertString(ReadOnlySpan<ByteArray> source, ReadOnlySpan<short> defLevels, [Nullable(new byte[] { 0, 2 })] Span<string> destination, short definedLevel, [Nullable(1)] ByteArrayReaderCache<ByteArray, string> byteArrayCache)
		{
			int i = 0;
			int num = 0;
			while (i < destination.Length)
			{
				*destination[i] = ((defLevels.IsEmpty || *defLevels[i] == definedLevel) ? LogicalRead.ToString(*source[num++], byteArrayCache) : null);
				i++;
			}
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00009D78 File Offset: 0x00007F78
		public unsafe static void ConvertString(ReadOnlySpan<ByteArray> source, ReadOnlySpan<short> defLevels, [Nullable(new byte[] { 0, 2 })] Span<string> destination, short definedLevel)
		{
			int i = 0;
			int num = 0;
			while (i < destination.Length)
			{
				*destination[i] = ((defLevels.IsEmpty || *defLevels[i] == definedLevel) ? LogicalRead.ToString(*source[num++]) : null);
				i++;
			}
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00009DE0 File Offset: 0x00007FE0
		public unsafe static void ConvertByteArray(ReadOnlySpan<ByteArray> source, ReadOnlySpan<short> defLevels, [Nullable(new byte[] { 0, 2 })] Span<byte[]> destination, short definedLevel)
		{
			int i = 0;
			int num = 0;
			while (i < destination.Length)
			{
				*destination[i] = ((defLevels.IsEmpty || *defLevels[i] == definedLevel) ? LogicalRead.ToByteArray(*source[num++]) : null);
				i++;
			}
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00009E48 File Offset: 0x00008048
		[NullableContext(1)]
		public static string ToString(ByteArray byteArray, ByteArrayReaderCache<ByteArray, string> byteArrayCache)
		{
			string text;
			if (byteArrayCache.TryGetValue(byteArray, out text))
			{
				if (LogicalRead.IsCacheValid(byteArrayCache, byteArray, text))
				{
					return text;
				}
				byteArrayCache.Clear();
			}
			return byteArrayCache.Add(byteArray, LogicalRead.ToString(byteArray));
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00009E8C File Offset: 0x0000808C
		[NullableContext(1)]
		public unsafe static bool IsCacheValid(ByteArrayReaderCache<ByteArray, string> byteArrayCache, ByteArray byteArray, string str)
		{
			int byteCount = Encoding.UTF8.GetByteCount(str);
			byte[] scratchBuffer = byteArrayCache.GetScratchBuffer(byteCount);
			Encoding.UTF8.GetBytes(str, 0, str.Length, scratchBuffer, 0);
			ReadOnlySpan<byte> readOnlySpan = new ReadOnlySpan<byte>((void*)byteArray.Pointer, byteArray.Length);
			Span<byte> span = scratchBuffer.AsSpan(0, byteCount);
			return readOnlySpan.SequenceEqual(span);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00009EF0 File Offset: 0x000080F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static decimal ToDecimal(FixedLenByteArray source, decimal multiplier)
		{
			return ((Decimal128*)(void*)source.Pointer)->ToDecimal(multiplier);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00009F04 File Offset: 0x00008104
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static Guid ToUuid(FixedLenByteArray source)
		{
			byte* ptr = (byte*)(void*)source.Pointer;
			if (BitConverter.IsLittleEndian)
			{
				int num = ((int)(*ptr) << 24) | ((int)ptr[1] << 16) | ((int)ptr[2] << 8) | (int)ptr[3];
				short num2 = (short)(((int)ptr[4] << 8) | (int)ptr[5]);
				short num3 = (short)(((int)ptr[6] << 8) | (int)ptr[7]);
				return new Guid(num, num2, num3, ptr[8], ptr[9], ptr[10], ptr[11], ptr[12], ptr[13], ptr[14], ptr[15]);
			}
			int num4 = (int)(*ptr) | ((int)ptr[1] << 8) | ((int)ptr[2] << 16) | ((int)ptr[3] << 24);
			short num5 = (short)((int)ptr[4] | ((int)ptr[5] << 8));
			short num6 = (short)((int)ptr[6] | ((int)ptr[7] << 8));
			return new Guid(num4, num5, num6, ptr[8], ptr[9], ptr[10], ptr[11], ptr[12], ptr[13], ptr[14], ptr[15]);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00009FF0 File Offset: 0x000081F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DateTime ToDateTimeMicros(long source)
		{
			return new DateTime(LogicalRead.ToDateTimeMicrosTicks(source));
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000A000 File Offset: 0x00008200
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long ToDateTimeMicrosTicks(long source)
		{
			return 621355968000000000L + source * 10L;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000A014 File Offset: 0x00008214
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DateTime ToDateTimeMillis(long source)
		{
			return new DateTime(LogicalRead.ToDateTimeMillisTicks(source));
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000A024 File Offset: 0x00008224
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long ToDateTimeMillisTicks(long source)
		{
			return 621355968000000000L + source * 10000L;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000A038 File Offset: 0x00008238
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TimeSpan ToTimeSpanMicros(long source)
		{
			return TimeSpan.FromTicks(source * 10L);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000A044 File Offset: 0x00008244
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TimeSpan ToTimeSpanMillis(int source)
		{
			return TimeSpan.FromTicks((long)source * 10000L);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000A054 File Offset: 0x00008254
		[NullableContext(1)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static string ToString(ByteArray byteArray)
		{
			if (byteArray.Length != 0)
			{
				return Encoding.UTF8.GetString((byte*)(void*)byteArray.Pointer, byteArray.Length);
			}
			return string.Empty;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000A084 File Offset: 0x00008284
		[NullableContext(1)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static byte[] ToByteArray(ByteArray byteArray)
		{
			byte[] array = new byte[byteArray.Length];
			if (byteArray.Length != 0)
			{
				Marshal.Copy(byteArray.Pointer, array, 0, array.Length);
			}
			return array;
		}

		// Token: 0x040000BB RID: 187
		public const long DateTimeOffset = 621355968000000000L;
	}
}
