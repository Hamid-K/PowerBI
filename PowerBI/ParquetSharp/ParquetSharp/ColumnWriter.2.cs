using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x02000027 RID: 39
	public class ColumnWriter<[IsUnmanaged] TValue> : ColumnWriter where TValue : struct
	{
		// Token: 0x0600011E RID: 286 RVA: 0x00004B00 File Offset: 0x00002D00
		[NullableContext(1)]
		internal ColumnWriter(IntPtr handle, RowGroupWriter rowGroupWriter, int columnIndex)
			: base(handle, rowGroupWriter, columnIndex)
		{
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00004B0C File Offset: 0x00002D0C
		[Nullable(1)]
		public override Type ElementType
		{
			[NullableContext(1)]
			get
			{
				return typeof(TValue);
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00004B18 File Offset: 0x00002D18
		[NullableContext(1)]
		public override TReturn Apply<[Nullable(2)] TReturn>(IColumnWriterVisitor<TReturn> visitor)
		{
			return visitor.OnColumnWriter<TValue>(this);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00004B24 File Offset: 0x00002D24
		public void WriteBatch(ReadOnlySpan<TValue> values)
		{
			this.WriteBatch(values.Length, null, null, values);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00004B40 File Offset: 0x00002D40
		public unsafe void WriteBatch(int numValues, ReadOnlySpan<short> defLevels, ReadOnlySpan<short> repLevels, ReadOnlySpan<TValue> values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			if (defLevels != null && defLevels.Length < numValues)
			{
				throw new ArgumentOutOfRangeException("defLevels", "numValues is larger than length of defLevels");
			}
			if (repLevels != null && repLevels.Length < numValues)
			{
				throw new ArgumentOutOfRangeException("repLevels", "numValues is larger than length of repLevels");
			}
			Type typeFromHandle = typeof(TValue);
			fixed (short* pinnableReference = defLevels.GetPinnableReference())
			{
				short* ptr = pinnableReference;
				fixed (short* pinnableReference2 = repLevels.GetPinnableReference())
				{
					short* ptr2 = pinnableReference2;
					fixed (TValue* pinnableReference3 = values.GetPinnableReference())
					{
						TValue* ptr3 = pinnableReference3;
						if (typeFromHandle == typeof(bool))
						{
							ExceptionInfo.Check(ColumnWriter.TypedColumnWriter_WriteBatch_Bool(this.Handle, (long)numValues, ptr, ptr2, (bool*)ptr3));
							return;
						}
						if (typeFromHandle == typeof(int))
						{
							ExceptionInfo.Check(ColumnWriter.TypedColumnWriter_WriteBatch_Int32(this.Handle, (long)numValues, ptr, ptr2, (int*)ptr3));
							return;
						}
						if (typeFromHandle == typeof(long))
						{
							ExceptionInfo.Check(ColumnWriter.TypedColumnWriter_WriteBatch_Int64(this.Handle, (long)numValues, ptr, ptr2, (long*)ptr3));
							return;
						}
						if (typeFromHandle == typeof(Int96))
						{
							ExceptionInfo.Check(ColumnWriter.TypedColumnWriter_WriteBatch_Int96(this.Handle, (long)numValues, ptr, ptr2, (Int96*)ptr3));
							return;
						}
						if (typeFromHandle == typeof(float))
						{
							ExceptionInfo.Check(ColumnWriter.TypedColumnWriter_WriteBatch_Float(this.Handle, (long)numValues, ptr, ptr2, (float*)ptr3));
							return;
						}
						if (typeFromHandle == typeof(double))
						{
							ExceptionInfo.Check(ColumnWriter.TypedColumnWriter_WriteBatch_Double(this.Handle, (long)numValues, ptr, ptr2, (double*)ptr3));
							return;
						}
						if (typeFromHandle == typeof(ByteArray))
						{
							ExceptionInfo.Check(ColumnWriter.TypedColumnWriter_WriteBatch_ByteArray(this.Handle, (long)numValues, ptr, ptr2, (ByteArray*)ptr3));
							return;
						}
						if (typeFromHandle == typeof(FixedLenByteArray))
						{
							ExceptionInfo.Check(ColumnWriter.TypedColumnWriter_WriteBatch_FixedLenByteArray(this.Handle, (long)numValues, ptr, ptr2, (FixedLenByteArray*)ptr3));
							return;
						}
						throw new NotSupportedException(string.Format("type {0} is not supported", typeFromHandle));
					}
				}
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00004D74 File Offset: 0x00002F74
		public void WriteBatch(int numValues, ArraySegment<TValue> values)
		{
			this.WriteBatch(numValues, null, null, values);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00004D90 File Offset: 0x00002F90
		public void WriteBatch(int numValues, ArraySegment<short>? defLevels, ArraySegment<short>? repLevels, ArraySegment<TValue> values)
		{
			this.WriteBatch(numValues, (defLevels != null) ? defLevels.Value : null, (repLevels != null) ? repLevels.Value : null, values);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00004DF8 File Offset: 0x00002FF8
		public unsafe void WriteBatchSpaced(int numValues, ReadOnlySpan<short> defLevels, ReadOnlySpan<short> repLevels, ReadOnlySpan<byte> validBits, long validBitsOffset, ReadOnlySpan<TValue> values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			if (defLevels == null)
			{
				throw new ArgumentNullException("defLevels");
			}
			if (repLevels == null)
			{
				throw new ArgumentNullException("repLevels");
			}
			if (validBits == null)
			{
				throw new ArgumentNullException("validBits");
			}
			if (defLevels.Length < numValues)
			{
				throw new ArgumentOutOfRangeException("defLevels", "numValues is larger than length of defLevels");
			}
			if (repLevels.Length < numValues)
			{
				throw new ArgumentOutOfRangeException("repLevels", "numValues is larger than length of repLevels");
			}
			if ((long)validBits.Length < (validBitsOffset + (long)numValues + 7L) / 8L)
			{
				throw new ArgumentOutOfRangeException("validBits", "numValues is larger than the bit length of validBits");
			}
			Type typeFromHandle = typeof(TValue);
			fixed (short* pinnableReference = defLevels.GetPinnableReference())
			{
				short* ptr = pinnableReference;
				fixed (short* pinnableReference2 = repLevels.GetPinnableReference())
				{
					short* ptr2 = pinnableReference2;
					fixed (byte* pinnableReference3 = validBits.GetPinnableReference())
					{
						byte* ptr3 = pinnableReference3;
						fixed (TValue* pinnableReference4 = values.GetPinnableReference())
						{
							TValue* ptr4 = pinnableReference4;
							if (typeFromHandle == typeof(bool))
							{
								ExceptionInfo.Check(ColumnWriter.TypedColumnWriter_WriteBatchSpaced_Bool(this.Handle, (long)numValues, ptr, ptr2, ptr3, validBitsOffset, (bool*)ptr4));
								return;
							}
							if (typeFromHandle == typeof(int))
							{
								ExceptionInfo.Check(ColumnWriter.TypedColumnWriter_WriteBatchSpaced_Int32(this.Handle, (long)numValues, ptr, ptr2, ptr3, validBitsOffset, (int*)ptr4));
								return;
							}
							if (typeFromHandle == typeof(long))
							{
								ExceptionInfo.Check(ColumnWriter.TypedColumnWriter_WriteBatchSpaced_Int64(this.Handle, (long)numValues, ptr, ptr2, ptr3, validBitsOffset, (long*)ptr4));
								return;
							}
							if (typeFromHandle == typeof(Int96))
							{
								ExceptionInfo.Check(ColumnWriter.TypedColumnWriter_WriteBatchSpaced_Int96(this.Handle, (long)numValues, ptr, ptr2, ptr3, validBitsOffset, (Int96*)ptr4));
								return;
							}
							if (typeFromHandle == typeof(float))
							{
								ExceptionInfo.Check(ColumnWriter.TypedColumnWriter_WriteBatchSpaced_Float(this.Handle, (long)numValues, ptr, ptr2, ptr3, validBitsOffset, (float*)ptr4));
								return;
							}
							if (typeFromHandle == typeof(double))
							{
								ExceptionInfo.Check(ColumnWriter.TypedColumnWriter_WriteBatchSpaced_Double(this.Handle, (long)numValues, ptr, ptr2, ptr3, validBitsOffset, (double*)ptr4));
								return;
							}
							if (typeFromHandle == typeof(ByteArray))
							{
								ExceptionInfo.Check(ColumnWriter.TypedColumnWriter_WriteBatchSpaced_ByteArray(this.Handle, (long)numValues, ptr, ptr2, ptr3, validBitsOffset, (ByteArray*)ptr4));
								return;
							}
							if (typeFromHandle == typeof(FixedLenByteArray))
							{
								ExceptionInfo.Check(ColumnWriter.TypedColumnWriter_WriteBatchSpaced_FixedLenByteArray(this.Handle, (long)numValues, ptr, ptr2, ptr3, validBitsOffset, (FixedLenByteArray*)ptr4));
								return;
							}
							throw new NotSupportedException(string.Format("type {0} is not supported", typeFromHandle));
						}
					}
				}
			}
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000050B4 File Offset: 0x000032B4
		public void WriteBatchSpaced(int numValues, ArraySegment<short> defLevels, ArraySegment<short> repLevels, ArraySegment<byte> validBits, long validBitsOffset, ArraySegment<TValue> values)
		{
			this.WriteBatchSpaced(numValues, defLevels, repLevels, validBits, validBitsOffset, values);
		}
	}
}
