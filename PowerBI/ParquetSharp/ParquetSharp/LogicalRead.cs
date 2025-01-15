using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x0200005C RID: 92
	public static class LogicalRead<[Nullable(2)] TLogical, [IsUnmanaged] TPhysical> where TPhysical : struct
	{
		// Token: 0x06000255 RID: 597 RVA: 0x000088AC File Offset: 0x00006AAC
		[NullableContext(2)]
		public static Delegate GetDirectReader()
		{
			if (typeof(TLogical) == typeof(bool) || typeof(TLogical) == typeof(int) || typeof(TLogical) == typeof(long) || typeof(TLogical) == typeof(Int96) || typeof(TLogical) == typeof(float) || typeof(TLogical) == typeof(double))
			{
				return LogicalRead.GetDirectReader<TPhysical, TPhysical>();
			}
			if (typeof(TLogical) == typeof(uint))
			{
				return LogicalRead.GetDirectReader<uint, int>();
			}
			if (typeof(TLogical) == typeof(ulong))
			{
				return LogicalRead.GetDirectReader<ulong, long>();
			}
			return null;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x000089C0 File Offset: 0x00006BC0
		[NullableContext(1)]
		public static Delegate GetConverter(ColumnDescriptor columnDescriptor, ColumnChunkMetaData columnChunkMetaData)
		{
			if (typeof(TLogical) == typeof(bool) || typeof(TLogical) == typeof(int) || typeof(TLogical) == typeof(long) || typeof(TLogical) == typeof(Int96) || typeof(TLogical) == typeof(float) || typeof(TLogical) == typeof(double))
			{
				return LogicalRead.GetNativeConverter<TPhysical, TPhysical>();
			}
			if (typeof(TLogical) == typeof(bool?) || typeof(TLogical) == typeof(int?) || typeof(TLogical) == typeof(long?) || typeof(TLogical) == typeof(Int96?) || typeof(TLogical) == typeof(float?) || typeof(TLogical) == typeof(double?))
			{
				return LogicalRead.GetNullableNativeConverter<TPhysical, TPhysical>();
			}
			if (typeof(TLogical) == typeof(sbyte))
			{
				return new LogicalRead<sbyte, int>.Converter(delegate(ReadOnlySpan<int> s, ReadOnlySpan<short> _, Span<sbyte> d, short _)
				{
					LogicalRead.ConvertInt8(s, d);
				});
			}
			if (typeof(TLogical) == typeof(sbyte?))
			{
				return new LogicalRead<sbyte?, int>.Converter(LogicalRead.ConvertInt8);
			}
			if (typeof(TLogical) == typeof(byte))
			{
				return new LogicalRead<byte, int>.Converter(delegate(ReadOnlySpan<int> s, ReadOnlySpan<short> _, Span<byte> d, short _)
				{
					LogicalRead.ConvertUInt8(s, d);
				});
			}
			if (typeof(TLogical) == typeof(byte?))
			{
				return new LogicalRead<byte?, int>.Converter(LogicalRead.ConvertUInt8);
			}
			if (typeof(TLogical) == typeof(short))
			{
				return new LogicalRead<short, int>.Converter(delegate(ReadOnlySpan<int> s, ReadOnlySpan<short> _, Span<short> d, short _)
				{
					LogicalRead.ConvertInt16(s, d);
				});
			}
			if (typeof(TLogical) == typeof(short?))
			{
				return new LogicalRead<short?, int>.Converter(LogicalRead.ConvertInt16);
			}
			if (typeof(TLogical) == typeof(ushort))
			{
				return new LogicalRead<ushort, int>.Converter(delegate(ReadOnlySpan<int> s, ReadOnlySpan<short> _, Span<ushort> d, short _)
				{
					LogicalRead.ConvertUInt16(s, d);
				});
			}
			if (typeof(TLogical) == typeof(ushort?))
			{
				return new LogicalRead<ushort?, int>.Converter(LogicalRead.ConvertUInt16);
			}
			if (typeof(TLogical) == typeof(uint))
			{
				return LogicalRead.GetNativeConverter<uint, int>();
			}
			if (typeof(TLogical) == typeof(uint?))
			{
				return LogicalRead.GetNullableNativeConverter<uint, int>();
			}
			if (typeof(TLogical) == typeof(ulong))
			{
				return LogicalRead.GetNativeConverter<ulong, long>();
			}
			if (typeof(TLogical) == typeof(ulong?))
			{
				return LogicalRead.GetNullableNativeConverter<ulong, long>();
			}
			if (typeof(TLogical) == typeof(decimal))
			{
				decimal multiplier2 = Decimal128.GetScaleMultiplier(columnDescriptor.TypeScale);
				if (typeof(TPhysical) == typeof(int))
				{
					return new LogicalRead<decimal, int>.Converter(delegate(ReadOnlySpan<int> s, ReadOnlySpan<short> _, Span<decimal> d, short _)
					{
						LogicalRead.ConvertDecimal32(s, d, multiplier2);
					});
				}
				if (typeof(TPhysical) == typeof(long))
				{
					return new LogicalRead<decimal, long>.Converter(delegate(ReadOnlySpan<long> s, ReadOnlySpan<short> _, Span<decimal> d, short _)
					{
						LogicalRead.ConvertDecimal64(s, d, multiplier2);
					});
				}
				if (typeof(TPhysical) == typeof(FixedLenByteArray))
				{
					return new LogicalRead<decimal, FixedLenByteArray>.Converter(delegate(ReadOnlySpan<FixedLenByteArray> s, ReadOnlySpan<short> _, Span<decimal> d, short _)
					{
						LogicalRead.ConvertDecimal128(s, d, multiplier2);
					});
				}
			}
			if (typeof(TLogical) == typeof(decimal?))
			{
				decimal multiplier = Decimal128.GetScaleMultiplier(columnDescriptor.TypeScale);
				if (typeof(TPhysical) == typeof(int))
				{
					return new LogicalRead<decimal?, int>.Converter(delegate(ReadOnlySpan<int> s, ReadOnlySpan<short> dl, Span<decimal?> d, short del)
					{
						LogicalRead.ConvertDecimal32(s, dl, d, multiplier, del);
					});
				}
				if (typeof(TPhysical) == typeof(long))
				{
					return new LogicalRead<decimal?, long>.Converter(delegate(ReadOnlySpan<long> s, ReadOnlySpan<short> dl, Span<decimal?> d, short del)
					{
						LogicalRead.ConvertDecimal64(s, dl, d, multiplier, del);
					});
				}
				if (typeof(TPhysical) == typeof(FixedLenByteArray))
				{
					return new LogicalRead<decimal?, FixedLenByteArray>.Converter(delegate(ReadOnlySpan<FixedLenByteArray> s, ReadOnlySpan<short> dl, Span<decimal?> d, short del)
					{
						LogicalRead.ConvertDecimal128(s, dl, d, multiplier, del);
					});
				}
			}
			if (typeof(TLogical) == typeof(Guid))
			{
				return new LogicalRead<Guid, FixedLenByteArray>.Converter(delegate(ReadOnlySpan<FixedLenByteArray> s, ReadOnlySpan<short> _, Span<Guid> d, short _)
				{
					LogicalRead.ConvertUuid(s, d);
				});
			}
			if (typeof(TLogical) == typeof(Guid?))
			{
				return new LogicalRead<Guid?, FixedLenByteArray>.Converter(LogicalRead.ConvertUuid);
			}
			if (typeof(TLogical) == typeof(Date))
			{
				return LogicalRead.GetNativeConverter<Date, int>();
			}
			if (typeof(TLogical) == typeof(Date?))
			{
				return LogicalRead.GetNullableNativeConverter<Date, int>();
			}
			Delegate @delegate;
			using (LogicalType logicalType = columnDescriptor.LogicalType)
			{
				if (typeof(TLogical) == typeof(DateTime))
				{
					TimestampLogicalType timestampLogicalType = (TimestampLogicalType)logicalType;
					bool flag;
					DateTimeKind kind2;
					if (AppContext.TryGetSwitch("ParquetSharp.ReadDateTimeKindAsUnspecified", out flag) && flag)
					{
						kind2 = DateTimeKind.Unspecified;
					}
					else
					{
						kind2 = (timestampLogicalType.IsAdjustedToUtc ? DateTimeKind.Utc : DateTimeKind.Unspecified);
					}
					TimeUnit timeUnit = timestampLogicalType.TimeUnit;
					if (timeUnit == TimeUnit.Millis)
					{
						return new LogicalRead<DateTime, long>.Converter(delegate(ReadOnlySpan<long> s, ReadOnlySpan<short> _, Span<DateTime> d, short _)
						{
							LogicalRead.ConvertDateTimeMillis(s, d, kind2);
						});
					}
					if (timeUnit == TimeUnit.Micros)
					{
						return new LogicalRead<DateTime, long>.Converter(delegate(ReadOnlySpan<long> s, ReadOnlySpan<short> _, Span<DateTime> d, short _)
						{
							LogicalRead.ConvertDateTimeMicros(s, d, kind2);
						});
					}
				}
				if (typeof(TLogical) == typeof(DateTimeNanos))
				{
					@delegate = LogicalRead.GetNativeConverter<DateTimeNanos, long>();
				}
				else
				{
					if (typeof(TLogical) == typeof(DateTime?))
					{
						TimestampLogicalType timestampLogicalType2 = (TimestampLogicalType)logicalType;
						bool flag2;
						DateTimeKind kind;
						if (AppContext.TryGetSwitch("ParquetSharp.ReadDateTimeKindAsUnspecified", out flag2) && flag2)
						{
							kind = DateTimeKind.Unspecified;
						}
						else
						{
							kind = (timestampLogicalType2.IsAdjustedToUtc ? DateTimeKind.Utc : DateTimeKind.Unspecified);
						}
						switch (timestampLogicalType2.TimeUnit)
						{
						case TimeUnit.Millis:
							return new LogicalRead<DateTime?, long>.Converter(delegate(ReadOnlySpan<long> source, ReadOnlySpan<short> rep, Span<DateTime?> dest, short def)
							{
								LogicalRead.ConvertDateTimeMillis(source, rep, dest, def, kind);
							});
						case TimeUnit.Micros:
							return new LogicalRead<DateTime?, long>.Converter(delegate(ReadOnlySpan<long> source, ReadOnlySpan<short> rep, Span<DateTime?> dest, short def)
							{
								LogicalRead.ConvertDateTimeMicros(source, rep, dest, def, kind);
							});
						case TimeUnit.Nanos:
							return new LogicalRead<TPhysical?, TPhysical>.Converter(LogicalRead.ConvertNative<TPhysical>);
						}
					}
					if (typeof(TLogical) == typeof(DateTimeNanos?))
					{
						@delegate = LogicalRead.GetNullableNativeConverter<DateTimeNanos, long>();
					}
					else
					{
						if (typeof(TLogical) == typeof(TimeSpan))
						{
							TimeUnit timeUnit = ((TimeLogicalType)logicalType).TimeUnit;
							if (timeUnit == TimeUnit.Millis)
							{
								return new LogicalRead<TimeSpan, int>.Converter(delegate(ReadOnlySpan<int> s, ReadOnlySpan<short> _, Span<TimeSpan> d, short _)
								{
									LogicalRead.ConvertTimeSpanMillis(s, d);
								});
							}
							if (timeUnit == TimeUnit.Micros)
							{
								return new LogicalRead<TimeSpan, long>.Converter(delegate(ReadOnlySpan<long> s, ReadOnlySpan<short> _, Span<TimeSpan> d, short _)
								{
									LogicalRead.ConvertTimeSpanMicros(s, d);
								});
							}
						}
						if (typeof(TLogical) == typeof(TimeSpanNanos))
						{
							@delegate = LogicalRead.GetNativeConverter<TimeSpanNanos, long>();
						}
						else
						{
							if (typeof(TLogical) == typeof(TimeSpan?))
							{
								TimeUnit timeUnit2 = ((TimeLogicalType)logicalType).TimeUnit;
								if (timeUnit2 == TimeUnit.Millis)
								{
									return new LogicalRead<TimeSpan?, int>.Converter(LogicalRead.ConvertTimeSpanMillis);
								}
								if (timeUnit2 == TimeUnit.Micros)
								{
									return new LogicalRead<TimeSpan?, long>.Converter(LogicalRead.ConvertTimeSpanMicros);
								}
							}
							if (typeof(TLogical) == typeof(TimeSpanNanos?))
							{
								@delegate = LogicalRead.GetNullableNativeConverter<TimeSpanNanos, long>();
							}
							else if (typeof(TLogical) == typeof(string))
							{
								ByteArrayReaderCache<TPhysical, TLogical> byteArrayCache = new ByteArrayReaderCache<TPhysical, TLogical>(columnChunkMetaData);
								@delegate = (byteArrayCache.IsUsable ? delegate(ReadOnlySpan<ByteArray> s, ReadOnlySpan<short> dl, [Nullable(new byte[] { 0, 2 })] Span<string> d, short del)
								{
									LogicalRead.ConvertString(s, dl, d, del, (ByteArrayReaderCache<ByteArray, string>)byteArrayCache);
								} : new LogicalRead<string, ByteArray>.Converter(LogicalRead.ConvertString));
							}
							else
							{
								if (!(typeof(TLogical) == typeof(byte[])))
								{
									throw new NotSupportedException(string.Format("unsupported logical system type {0} with logical type {1}", typeof(TLogical), logicalType));
								}
								@delegate = new LogicalRead<byte[], ByteArray>.Converter(LogicalRead.ConvertByteArray);
							}
						}
					}
				}
			}
			return @delegate;
		}

		// Token: 0x040000BA RID: 186
		[Nullable(1)]
		private const string UseDateTimeKindUnspecifiedSwitchName = "ParquetSharp.ReadDateTimeKindAsUnspecified";

		// Token: 0x02000110 RID: 272
		// (Invoke) Token: 0x06000959 RID: 2393
		public delegate long DirectReader([Nullable(new byte[] { 1, 0 })] ColumnReader<TPhysical> columnReader, [Nullable(new byte[] { 0, 1 })] Span<TLogical> destination);

		// Token: 0x02000111 RID: 273
		// (Invoke) Token: 0x0600095D RID: 2397
		public delegate void Converter(ReadOnlySpan<TPhysical> source, ReadOnlySpan<short> defLevels, [Nullable(new byte[] { 0, 1 })] Span<TLogical> destination, short definedLevel);
	}
}
