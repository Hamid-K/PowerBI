using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x02000072 RID: 114
	public static class LogicalWrite<[Nullable(2)] TLogical, [IsUnmanaged] TPhysical> where TPhysical : struct
	{
		// Token: 0x060002DE RID: 734 RVA: 0x0000B3C4 File Offset: 0x000095C4
		[NullableContext(1)]
		public static Delegate GetConverter(ColumnDescriptor columnDescriptor, [Nullable(2)] ByteBuffer byteBuffer)
		{
			if (typeof(TLogical) == typeof(bool) || typeof(TLogical) == typeof(int) || typeof(TLogical) == typeof(long) || typeof(TLogical) == typeof(Int96) || typeof(TLogical) == typeof(float) || typeof(TLogical) == typeof(double))
			{
				return LogicalWrite.GetNativeConverter<TPhysical, TPhysical>();
			}
			if (typeof(TLogical) == typeof(bool?) || typeof(TLogical) == typeof(int?) || typeof(TLogical) == typeof(long?) || typeof(TLogical) == typeof(Int96?) || typeof(TLogical) == typeof(float?) || typeof(TLogical) == typeof(double?))
			{
				return LogicalWrite.GetNullableNativeConverter<TPhysical, TPhysical>();
			}
			if (typeof(TLogical) == typeof(sbyte))
			{
				return new LogicalWrite<sbyte, int>.Converter(delegate(ReadOnlySpan<sbyte> s, Span<short> _, Span<int> d, short _)
				{
					LogicalWrite.ConvertInt8(s, d);
				});
			}
			if (typeof(TLogical) == typeof(sbyte?))
			{
				return new LogicalWrite<sbyte?, int>.Converter(LogicalWrite.ConvertInt8);
			}
			if (typeof(TLogical) == typeof(byte))
			{
				return new LogicalWrite<byte, int>.Converter(delegate(ReadOnlySpan<byte> s, Span<short> _, Span<int> d, short _)
				{
					LogicalWrite.ConvertUInt8(s, d);
				});
			}
			if (typeof(TLogical) == typeof(byte?))
			{
				return new LogicalWrite<byte?, int>.Converter(LogicalWrite.ConvertUInt8);
			}
			if (typeof(TLogical) == typeof(short))
			{
				return new LogicalWrite<short, int>.Converter(delegate(ReadOnlySpan<short> s, Span<short> _, Span<int> d, short _)
				{
					LogicalWrite.ConvertInt16(s, d);
				});
			}
			if (typeof(TLogical) == typeof(short?))
			{
				return new LogicalWrite<short?, int>.Converter(LogicalWrite.ConvertInt16);
			}
			if (typeof(TLogical) == typeof(ushort))
			{
				return new LogicalWrite<ushort, int>.Converter(delegate(ReadOnlySpan<ushort> s, Span<short> _, Span<int> d, short _)
				{
					LogicalWrite.ConvertUInt16(s, d);
				});
			}
			if (typeof(TLogical) == typeof(ushort?))
			{
				return new LogicalWrite<ushort?, int>.Converter(LogicalWrite.ConvertUInt16);
			}
			if (typeof(TLogical) == typeof(uint))
			{
				return LogicalWrite.GetNativeConverter<uint, int>();
			}
			if (typeof(TLogical) == typeof(uint?))
			{
				return LogicalWrite.GetNullableNativeConverter<uint, int>();
			}
			if (typeof(TLogical) == typeof(ulong))
			{
				return LogicalWrite.GetNativeConverter<ulong, long>();
			}
			if (typeof(TLogical) == typeof(ulong?))
			{
				return LogicalWrite.GetNullableNativeConverter<ulong, long>();
			}
			if (typeof(TLogical) == typeof(decimal))
			{
				LogicalWrite<TLogical, TPhysical>.ValidateDecimalColumn(columnDescriptor);
				if (byteBuffer == null)
				{
					throw new ArgumentNullException("byteBuffer");
				}
				decimal multiplier2 = Decimal128.GetScaleMultiplier(columnDescriptor.TypeScale);
				return new LogicalWrite<decimal, FixedLenByteArray>.Converter(delegate(ReadOnlySpan<decimal> s, Span<short> _, Span<FixedLenByteArray> d, short _)
				{
					LogicalWrite.ConvertDecimal128(s, d, multiplier2, byteBuffer);
				});
			}
			else if (typeof(TLogical) == typeof(decimal?))
			{
				LogicalWrite<TLogical, TPhysical>.ValidateDecimalColumn(columnDescriptor);
				if (byteBuffer == null)
				{
					throw new ArgumentNullException("byteBuffer");
				}
				decimal multiplier = Decimal128.GetScaleMultiplier(columnDescriptor.TypeScale);
				return new LogicalWrite<decimal?, FixedLenByteArray>.Converter(delegate(ReadOnlySpan<decimal?> s, Span<short> dl, Span<FixedLenByteArray> d, short nl)
				{
					LogicalWrite.ConvertDecimal128(s, dl, d, multiplier, nl, byteBuffer);
				});
			}
			else if (typeof(TLogical) == typeof(Guid))
			{
				if (byteBuffer == null)
				{
					throw new ArgumentNullException("byteBuffer");
				}
				return new LogicalWrite<Guid, FixedLenByteArray>.Converter(delegate(ReadOnlySpan<Guid> s, Span<short> _, Span<FixedLenByteArray> d, short _)
				{
					LogicalWrite.ConvertUuid(s, d, byteBuffer);
				});
			}
			else if (typeof(TLogical) == typeof(Guid?))
			{
				if (byteBuffer == null)
				{
					throw new ArgumentNullException("byteBuffer");
				}
				return new LogicalWrite<Guid?, FixedLenByteArray>.Converter(delegate(ReadOnlySpan<Guid?> s, Span<short> dl, Span<FixedLenByteArray> d, short nl)
				{
					LogicalWrite.ConvertUuid(s, dl, d, nl, byteBuffer);
				});
			}
			else
			{
				if (typeof(TLogical) == typeof(Date))
				{
					return LogicalWrite.GetNativeConverter<Date, int>();
				}
				if (typeof(TLogical) == typeof(Date?))
				{
					return LogicalWrite.GetNullableNativeConverter<Date, int>();
				}
				Delegate @delegate;
				using (LogicalType logicalType = columnDescriptor.LogicalType)
				{
					if (typeof(TLogical) == typeof(DateTime))
					{
						TimeUnit timeUnit = ((TimestampLogicalType)logicalType).TimeUnit;
						if (timeUnit == TimeUnit.Millis)
						{
							return new LogicalWrite<DateTime, long>.Converter(delegate(ReadOnlySpan<DateTime> s, Span<short> _, Span<long> d, short _)
							{
								LogicalWrite.ConvertDateTimeMillis(s, d);
							});
						}
						if (timeUnit == TimeUnit.Micros)
						{
							return new LogicalWrite<DateTime, long>.Converter(delegate(ReadOnlySpan<DateTime> s, Span<short> _, Span<long> d, short _)
							{
								LogicalWrite.ConvertDateTimeMicros(s, d);
							});
						}
					}
					if (typeof(TLogical) == typeof(DateTimeNanos))
					{
						@delegate = LogicalWrite.GetNativeConverter<DateTimeNanos, long>();
					}
					else
					{
						if (typeof(TLogical) == typeof(DateTime?))
						{
							TimeUnit timeUnit = ((TimestampLogicalType)logicalType).TimeUnit;
							if (timeUnit == TimeUnit.Millis)
							{
								return new LogicalWrite<DateTime?, long>.Converter(LogicalWrite.ConvertDateTimeMillis);
							}
							if (timeUnit == TimeUnit.Micros)
							{
								return new LogicalWrite<DateTime?, long>.Converter(LogicalWrite.ConvertDateTimeMicros);
							}
						}
						if (typeof(TLogical) == typeof(DateTimeNanos?))
						{
							@delegate = LogicalWrite.GetNullableNativeConverter<DateTimeNanos, long>();
						}
						else
						{
							if (typeof(TLogical) == typeof(TimeSpan))
							{
								TimeUnit timeUnit = ((TimeLogicalType)logicalType).TimeUnit;
								if (timeUnit == TimeUnit.Millis)
								{
									return new LogicalWrite<TimeSpan, int>.Converter(delegate(ReadOnlySpan<TimeSpan> s, Span<short> _, Span<int> d, short _)
									{
										LogicalWrite.ConvertTimeSpanMillis(s, d);
									});
								}
								if (timeUnit == TimeUnit.Micros)
								{
									return new LogicalWrite<TimeSpan, long>.Converter(delegate(ReadOnlySpan<TimeSpan> s, Span<short> _, Span<long> d, short _)
									{
										LogicalWrite.ConvertTimeSpanMicros(s, d);
									});
								}
							}
							if (typeof(TLogical) == typeof(TimeSpanNanos))
							{
								@delegate = LogicalWrite.GetNativeConverter<TimeSpanNanos, long>();
							}
							else
							{
								if (typeof(TLogical) == typeof(TimeSpan?))
								{
									TimeUnit timeUnit = ((TimeLogicalType)logicalType).TimeUnit;
									if (timeUnit == TimeUnit.Millis)
									{
										return new LogicalWrite<TimeSpan?, int>.Converter(LogicalWrite.ConvertTimeSpanMillis);
									}
									if (timeUnit == TimeUnit.Micros)
									{
										return new LogicalWrite<TimeSpan?, long>.Converter(LogicalWrite.ConvertTimeSpanMicros);
									}
								}
								if (typeof(TLogical) == typeof(TimeSpanNanos?))
								{
									@delegate = LogicalWrite.GetNullableNativeConverter<TimeSpanNanos, long>();
								}
								else if (typeof(TLogical) == typeof(string))
								{
									if (byteBuffer == null)
									{
										throw new ArgumentNullException("byteBuffer");
									}
									@delegate = new LogicalWrite<string, ByteArray>.Converter(delegate([Nullable(new byte[] { 0, 1 })] ReadOnlySpan<string> s, Span<short> dl, Span<ByteArray> d, short nl)
									{
										LogicalWrite.ConvertString(s, dl, d, nl, byteBuffer);
									});
								}
								else
								{
									if (!(typeof(TLogical) == typeof(byte[])))
									{
										throw new NotSupportedException(string.Format("unsupported logical system type {0} with logical type {1}", typeof(TLogical), logicalType));
									}
									if (byteBuffer == null)
									{
										throw new ArgumentNullException("byteBuffer");
									}
									@delegate = new LogicalWrite<byte[], ByteArray>.Converter(delegate([Nullable(new byte[] { 0, 1 })] ReadOnlySpan<byte[]> s, Span<short> dl, Span<ByteArray> d, short nl)
									{
										LogicalWrite.ConvertByteArray(s, dl, d, nl, byteBuffer);
									});
								}
							}
						}
					}
				}
				return @delegate;
			}
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000BC84 File Offset: 0x00009E84
		[NullableContext(1)]
		private unsafe static void ValidateDecimalColumn(ColumnDescriptor columnDescriptor)
		{
			if (typeof(TPhysical) != typeof(FixedLenByteArray))
			{
				throw new NotSupportedException("Writing decimal data is only supported with a fixed-length byte array physical type");
			}
			if (columnDescriptor.TypePrecision != 29)
			{
				throw new NotSupportedException("only 29 digits of precision is currently supported for decimal type");
			}
			if (columnDescriptor.TypeLength != sizeof(Decimal128))
			{
				throw new NotSupportedException("only 16 bytes of length is currently supported for decimal type ");
			}
		}

		// Token: 0x0200011C RID: 284
		// (Invoke) Token: 0x06000985 RID: 2437
		public delegate void Converter([Nullable(new byte[] { 0, 1 })] ReadOnlySpan<TLogical> source, Span<short> defLevels, Span<TPhysical> destination, short nullLevel);
	}
}
