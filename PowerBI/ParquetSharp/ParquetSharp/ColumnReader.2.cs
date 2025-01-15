using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x0200001D RID: 29
	public class ColumnReader<[IsUnmanaged] TValue> : ColumnReader where TValue : struct
	{
		// Token: 0x060000E7 RID: 231 RVA: 0x00003F38 File Offset: 0x00002138
		[NullableContext(1)]
		internal ColumnReader(ParquetHandle handle, RowGroupReader rowGroupReader, ColumnChunkMetaData columnChunkMetaData, int columnIndex)
			: base(handle, rowGroupReader, columnChunkMetaData, columnIndex)
		{
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00003F48 File Offset: 0x00002148
		[Nullable(1)]
		public override Type ElementType
		{
			[NullableContext(1)]
			get
			{
				return typeof(TValue);
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00003F54 File Offset: 0x00002154
		[NullableContext(1)]
		public override TReturn Apply<[Nullable(2)] TReturn>(IColumnReaderVisitor<TReturn> visitor)
		{
			return visitor.OnColumnReader<TValue>(this);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00003F60 File Offset: 0x00002160
		public long ReadBatch(long batchSize, Span<TValue> values, out long valuesRead)
		{
			return this.ReadBatch(batchSize, null, null, values, out valuesRead);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00003F78 File Offset: 0x00002178
		public long ReadBatch(long batchSize, ArraySegment<TValue> values, out long valuesRead)
		{
			return this.ReadBatch(batchSize, values.AsSpan<TValue>(), out valuesRead);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00003F88 File Offset: 0x00002188
		public unsafe long ReadBatch(long batchSize, Span<short> defLevels, Span<short> repLevels, Span<TValue> values, out long valuesRead)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			if ((long)values.Length < batchSize)
			{
				throw new ArgumentOutOfRangeException("values", "batchSize is larger than length of values");
			}
			if (defLevels != null && (long)defLevels.Length < batchSize)
			{
				throw new ArgumentOutOfRangeException("defLevels", "batchSize is larger than length of defLevels");
			}
			if (repLevels != null && (long)repLevels.Length < batchSize)
			{
				throw new ArgumentOutOfRangeException("repLevels", "batchSize is larger than length of repLevels");
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
							long num;
							ExceptionInfo.Check(ColumnReader.TypedColumnReader_ReadBatch_Bool(this.Handle.IntPtr, batchSize, ptr, ptr2, (bool*)ptr3, out valuesRead, out num));
							GC.KeepAlive(this.Handle);
							return num;
						}
						if (typeFromHandle == typeof(int))
						{
							long num2;
							ExceptionInfo.Check(ColumnReader.TypedColumnReader_ReadBatch_Int32(this.Handle.IntPtr, batchSize, ptr, ptr2, (int*)ptr3, out valuesRead, out num2));
							GC.KeepAlive(this.Handle);
							return num2;
						}
						if (typeFromHandle == typeof(long))
						{
							long num3;
							ExceptionInfo.Check(ColumnReader.TypedColumnReader_ReadBatch_Int64(this.Handle.IntPtr, batchSize, ptr, ptr2, (long*)ptr3, out valuesRead, out num3));
							GC.KeepAlive(this.Handle);
							return num3;
						}
						if (typeFromHandle == typeof(Int96))
						{
							long num4;
							ExceptionInfo.Check(ColumnReader.TypedColumnReader_ReadBatch_Int96(this.Handle.IntPtr, batchSize, ptr, ptr2, (Int96*)ptr3, out valuesRead, out num4));
							GC.KeepAlive(this.Handle);
							return num4;
						}
						if (typeFromHandle == typeof(float))
						{
							long num5;
							ExceptionInfo.Check(ColumnReader.TypedColumnReader_ReadBatch_Float(this.Handle.IntPtr, batchSize, ptr, ptr2, (float*)ptr3, out valuesRead, out num5));
							GC.KeepAlive(this.Handle);
							return num5;
						}
						if (typeFromHandle == typeof(double))
						{
							long num6;
							ExceptionInfo.Check(ColumnReader.TypedColumnReader_ReadBatch_Double(this.Handle.IntPtr, batchSize, ptr, ptr2, (double*)ptr3, out valuesRead, out num6));
							GC.KeepAlive(this.Handle);
							return num6;
						}
						if (typeFromHandle == typeof(ByteArray))
						{
							long num7;
							ExceptionInfo.Check(ColumnReader.TypedColumnReader_ReadBatch_ByteArray(this.Handle.IntPtr, batchSize, ptr, ptr2, (ByteArray*)ptr3, out valuesRead, out num7));
							GC.KeepAlive(this.Handle);
							return num7;
						}
						if (typeFromHandle == typeof(FixedLenByteArray))
						{
							long num8;
							ExceptionInfo.Check(ColumnReader.TypedColumnReader_ReadBatch_FixedLenByteArray(this.Handle.IntPtr, batchSize, ptr, ptr2, (FixedLenByteArray*)ptr3, out valuesRead, out num8));
							GC.KeepAlive(this.Handle);
							return num8;
						}
						throw new NotSupportedException(string.Format("type {0} is not supported", typeFromHandle));
					}
				}
			}
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00004284 File Offset: 0x00002484
		public unsafe int ReadDictionary(out IntPtr values)
		{
			Type typeFromHandle = typeof(TValue);
			if (typeFromHandle == typeof(bool))
			{
				bool* ptr;
				int num;
				ExceptionInfo.Check(ColumnReader.TypedColumnReader_ReadDictionary_Bool(this.Handle.IntPtr, out ptr, out num));
				values = (IntPtr)((void*)ptr);
				GC.KeepAlive(this.Handle);
				return num;
			}
			if (typeFromHandle == typeof(int))
			{
				int* ptr2;
				int num2;
				ExceptionInfo.Check(ColumnReader.TypedColumnReader_ReadDictionary_Int32(this.Handle.IntPtr, out ptr2, out num2));
				values = (IntPtr)((void*)ptr2);
				GC.KeepAlive(this.Handle);
				return num2;
			}
			if (typeFromHandle == typeof(long))
			{
				long* ptr3;
				int num3;
				ExceptionInfo.Check(ColumnReader.TypedColumnReader_ReadDictionary_Int64(this.Handle.IntPtr, out ptr3, out num3));
				values = (IntPtr)((void*)ptr3);
				GC.KeepAlive(this.Handle);
				return num3;
			}
			if (typeFromHandle == typeof(Int96))
			{
				Int96* ptr4;
				int num4;
				ExceptionInfo.Check(ColumnReader.TypedColumnReader_ReadDictionary_Int96(this.Handle.IntPtr, out ptr4, out num4));
				values = (IntPtr)((void*)ptr4);
				GC.KeepAlive(this.Handle);
				return num4;
			}
			if (typeFromHandle == typeof(float))
			{
				float* ptr5;
				int num5;
				ExceptionInfo.Check(ColumnReader.TypedColumnReader_ReadDictionary_Float(this.Handle.IntPtr, out ptr5, out num5));
				values = (IntPtr)((void*)ptr5);
				GC.KeepAlive(this.Handle);
				return num5;
			}
			if (typeFromHandle == typeof(double))
			{
				double* ptr6;
				int num6;
				ExceptionInfo.Check(ColumnReader.TypedColumnReader_ReadDictionary_Double(this.Handle.IntPtr, out ptr6, out num6));
				values = (IntPtr)((void*)ptr6);
				GC.KeepAlive(this.Handle);
				return num6;
			}
			if (typeFromHandle == typeof(ByteArray))
			{
				ByteArray* ptr7;
				int num7;
				ExceptionInfo.Check(ColumnReader.TypedColumnReader_ReadDictionary_ByteArray(this.Handle.IntPtr, out ptr7, out num7));
				values = (IntPtr)((void*)ptr7);
				GC.KeepAlive(this.Handle);
				return num7;
			}
			if (typeFromHandle == typeof(FixedLenByteArray))
			{
				FixedLenByteArray* ptr8;
				int num8;
				ExceptionInfo.Check(ColumnReader.TypedColumnReader_ReadDictionary_FixedLenByteArray(this.Handle.IntPtr, out ptr8, out num8));
				values = (IntPtr)((void*)ptr8);
				GC.KeepAlive(this.Handle);
				return num8;
			}
			throw new NotSupportedException(string.Format("type {0} is not supported", typeFromHandle));
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000044D8 File Offset: 0x000026D8
		[NullableContext(1)]
		public void ReadEncodedData(ColumnReader.GetNewPage getNewPage)
		{
			Type typeFromHandle = typeof(TValue);
			if (typeFromHandle == typeof(bool))
			{
				ExceptionInfo.Check(ColumnReader.TypedColumnReader_ReadEncodedData_Bool(this.Handle.IntPtr, getNewPage));
				GC.KeepAlive(this.Handle);
				return;
			}
			if (typeFromHandle == typeof(int))
			{
				ExceptionInfo.Check(ColumnReader.TypedColumnReader_ReadEncodedData_Int32(this.Handle.IntPtr, getNewPage));
				GC.KeepAlive(this.Handle);
				return;
			}
			if (typeFromHandle == typeof(long))
			{
				ExceptionInfo.Check(ColumnReader.TypedColumnReader_ReadEncodedData_Int64(this.Handle.IntPtr, getNewPage));
				GC.KeepAlive(this.Handle);
				return;
			}
			if (typeFromHandle == typeof(Int96))
			{
				ExceptionInfo.Check(ColumnReader.TypedColumnReader_ReadEncodedData_Int96(this.Handle.IntPtr, getNewPage));
				GC.KeepAlive(this.Handle);
				return;
			}
			if (typeFromHandle == typeof(float))
			{
				ExceptionInfo.Check(ColumnReader.TypedColumnReader_ReadEncodedData_Float(this.Handle.IntPtr, getNewPage));
				GC.KeepAlive(this.Handle);
				return;
			}
			if (typeFromHandle == typeof(double))
			{
				ExceptionInfo.Check(ColumnReader.TypedColumnReader_ReadEncodedData_Double(this.Handle.IntPtr, getNewPage));
				GC.KeepAlive(this.Handle);
				return;
			}
			if (typeFromHandle == typeof(ByteArray))
			{
				ExceptionInfo.Check(ColumnReader.TypedColumnReader_ReadEncodedData_ByteArray(this.Handle.IntPtr, getNewPage));
				GC.KeepAlive(this.Handle);
				return;
			}
			if (typeFromHandle == typeof(FixedLenByteArray))
			{
				ExceptionInfo.Check(ColumnReader.TypedColumnReader_ReadEncodedData_FixedLenByteArray(this.Handle.IntPtr, getNewPage));
				GC.KeepAlive(this.Handle);
				return;
			}
			throw new NotSupportedException(string.Format("type {0} is not supported", typeFromHandle));
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000046BC File Offset: 0x000028BC
		public long ReadBatch(long batchSize, ArraySegment<short>? defLevels, ArraySegment<short>? repLevels, ArraySegment<TValue> values, out long valuesRead)
		{
			return this.ReadBatch(batchSize, (defLevels != null) ? defLevels.Value.AsSpan<short>() : null, (repLevels != null) ? repLevels.Value.AsSpan<short>() : null, values.AsSpan<TValue>(), out valuesRead);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004724 File Offset: 0x00002924
		public override long Skip(long numRowsToSkip)
		{
			Type typeFromHandle = typeof(TValue);
			if (typeFromHandle == typeof(bool))
			{
				return ExceptionInfo.Return<long, long>(this.Handle, numRowsToSkip, new ExceptionInfo.GetFunction<long, long>(ColumnReader.TypedColumnReader_Skip_Bool));
			}
			if (typeFromHandle == typeof(int))
			{
				return ExceptionInfo.Return<long, long>(this.Handle, numRowsToSkip, new ExceptionInfo.GetFunction<long, long>(ColumnReader.TypedColumnReader_Skip_Int32));
			}
			if (typeFromHandle == typeof(long))
			{
				return ExceptionInfo.Return<long, long>(this.Handle, numRowsToSkip, new ExceptionInfo.GetFunction<long, long>(ColumnReader.TypedColumnReader_Skip_Int64));
			}
			if (typeFromHandle == typeof(Int96))
			{
				return ExceptionInfo.Return<long, long>(this.Handle, numRowsToSkip, new ExceptionInfo.GetFunction<long, long>(ColumnReader.TypedColumnReader_Skip_Int96));
			}
			if (typeFromHandle == typeof(float))
			{
				return ExceptionInfo.Return<long, long>(this.Handle, numRowsToSkip, new ExceptionInfo.GetFunction<long, long>(ColumnReader.TypedColumnReader_Skip_Float));
			}
			if (typeFromHandle == typeof(double))
			{
				return ExceptionInfo.Return<long, long>(this.Handle, numRowsToSkip, new ExceptionInfo.GetFunction<long, long>(ColumnReader.TypedColumnReader_Skip_Double));
			}
			if (typeFromHandle == typeof(ByteArray))
			{
				return ExceptionInfo.Return<long, long>(this.Handle, numRowsToSkip, new ExceptionInfo.GetFunction<long, long>(ColumnReader.TypedColumnReader_Skip_ByteArray));
			}
			if (typeFromHandle == typeof(FixedLenByteArray))
			{
				return ExceptionInfo.Return<long, long>(this.Handle, numRowsToSkip, new ExceptionInfo.GetFunction<long, long>(ColumnReader.TypedColumnReader_Skip_FixedLenByteArray));
			}
			throw new NotSupportedException(string.Format("type {0} is not supported", typeFromHandle));
		}
	}
}
