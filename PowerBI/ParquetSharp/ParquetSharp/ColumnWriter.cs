using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ParquetSharp
{
	// Token: 0x02000026 RID: 38
	public abstract class ColumnWriter : IDisposable
	{
		// Token: 0x060000F9 RID: 249 RVA: 0x00004940 File Offset: 0x00002B40
		[NullableContext(1)]
		internal static ColumnWriter Create(IntPtr handle, RowGroupWriter rowGroupWriter, int columnIndex)
		{
			PhysicalType physicalType = ExceptionInfo.Return<PhysicalType>(handle, new ExceptionInfo.GetFunction<PhysicalType>(ColumnWriter.ColumnWriter_Type));
			switch (physicalType)
			{
			case PhysicalType.Boolean:
				return new BoolColumnWriter(handle, rowGroupWriter, columnIndex);
			case PhysicalType.Int32:
				return new Int32ColumnWriter(handle, rowGroupWriter, columnIndex);
			case PhysicalType.Int64:
				return new Int64ColumnWriter(handle, rowGroupWriter, columnIndex);
			case PhysicalType.Int96:
				return new Int96ColumnWriter(handle, rowGroupWriter, columnIndex);
			case PhysicalType.Float:
				return new FloatColumnWriter(handle, rowGroupWriter, columnIndex);
			case PhysicalType.Double:
				return new DoubleColumnWriter(handle, rowGroupWriter, columnIndex);
			case PhysicalType.ByteArray:
				return new ByteArrayColumnWriter(handle, rowGroupWriter, columnIndex);
			case PhysicalType.FixedLenByteArray:
				return new FixedLenByteArrayColumnWriter(handle, rowGroupWriter, columnIndex);
			default:
				throw new NotSupportedException(string.Format("Physical type {0} is not supported", physicalType));
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000049EC File Offset: 0x00002BEC
		[NullableContext(1)]
		internal ColumnWriter(IntPtr handle, RowGroupWriter rowGroupWriter, int columnIndex)
		{
			this.Handle = handle;
			this.RowGroupWriter = rowGroupWriter;
			this.ColumnIndex = columnIndex;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00004A0C File Offset: 0x00002C0C
		public void Dispose()
		{
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00004A10 File Offset: 0x00002C10
		public long Close()
		{
			return ExceptionInfo.Return<long>(this.Handle, new ExceptionInfo.GetFunction<long>(ColumnWriter.ColumnWriter_Close));
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00004A2C File Offset: 0x00002C2C
		public int ColumnIndex { get; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00004A34 File Offset: 0x00002C34
		[Nullable(1)]
		public LogicalTypeFactory LogicalTypeFactory
		{
			[NullableContext(1)]
			get
			{
				return this.RowGroupWriter.ParquetFileWriter.LogicalTypeFactory;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00004A48 File Offset: 0x00002C48
		[Nullable(1)]
		public LogicalWriteConverterFactory LogicalWriteConverterFactory
		{
			[NullableContext(1)]
			get
			{
				return this.RowGroupWriter.ParquetFileWriter.LogicalWriteConverterFactory;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00004A5C File Offset: 0x00002C5C
		[Nullable(1)]
		public ColumnDescriptor ColumnDescriptor
		{
			[NullableContext(1)]
			get
			{
				return new ColumnDescriptor(ExceptionInfo.Return<IntPtr>(this.Handle, new ExceptionInfo.GetFunction<IntPtr>(ColumnWriter.ColumnWriter_Descr)));
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00004A7C File Offset: 0x00002C7C
		public long RowWritten
		{
			get
			{
				return ExceptionInfo.Return<long>(this.Handle, new ExceptionInfo.GetFunction<long>(ColumnWriter.ColumnWriter_Rows_Written));
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00004A98 File Offset: 0x00002C98
		public PhysicalType Type
		{
			get
			{
				return ExceptionInfo.Return<PhysicalType>(this.Handle, new ExceptionInfo.GetFunction<PhysicalType>(ColumnWriter.ColumnWriter_Type));
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00004AB4 File Offset: 0x00002CB4
		[Nullable(1)]
		public WriterProperties WriterProperties
		{
			[NullableContext(1)]
			get
			{
				return new WriterProperties(ExceptionInfo.Return<IntPtr>(this.Handle, new ExceptionInfo.GetFunction<IntPtr>(ColumnWriter.ColumnWriter_Properties)));
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000104 RID: 260
		[Nullable(1)]
		public abstract Type ElementType
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x06000105 RID: 261
		[NullableContext(1)]
		public abstract TReturn Apply<[Nullable(2)] TReturn>(IColumnWriterVisitor<TReturn> visitor);

		// Token: 0x06000106 RID: 262 RVA: 0x00004AD4 File Offset: 0x00002CD4
		[NullableContext(1)]
		public LogicalColumnWriter LogicalWriter(int bufferLength = 4096)
		{
			return LogicalColumnWriter.Create(this, bufferLength, null);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00004AE0 File Offset: 0x00002CE0
		[NullableContext(1)]
		public LogicalColumnWriter<TElement> LogicalWriter<[Nullable(2)] TElement>(int bufferLength = 4096)
		{
			return LogicalColumnWriter.Create<TElement>(this, bufferLength, null);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00004AEC File Offset: 0x00002CEC
		[NullableContext(1)]
		public LogicalColumnWriter<TElement> LogicalWriterOverride<[Nullable(2)] TElement>(int bufferLength = 4096)
		{
			return LogicalColumnWriter.Create<TElement>(this, bufferLength, typeof(TElement));
		}

		// Token: 0x06000109 RID: 265
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnWriter_Close(IntPtr columnWriter, out long columnSize);

		// Token: 0x0600010A RID: 266
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnWriter_Descr(IntPtr columnWriter, out IntPtr columnDescriptor);

		// Token: 0x0600010B RID: 267
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnWriter_Properties(IntPtr columnWriter, out IntPtr writerProperties);

		// Token: 0x0600010C RID: 268
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnWriter_Rows_Written(IntPtr columnWriter, out long rowsWritten);

		// Token: 0x0600010D RID: 269
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnWriter_Type(IntPtr columnWriter, out PhysicalType type);

		// Token: 0x0600010E RID: 270
		[DllImport("ParquetSharpNative")]
		protected unsafe static extern IntPtr TypedColumnWriter_WriteBatch_Bool(IntPtr columnWriter, long numValues, short* defLevels, short* repLevels, bool* values);

		// Token: 0x0600010F RID: 271
		[DllImport("ParquetSharpNative")]
		protected unsafe static extern IntPtr TypedColumnWriter_WriteBatch_Int32(IntPtr columnWriter, long numValues, short* defLevels, short* repLevels, int* values);

		// Token: 0x06000110 RID: 272
		[DllImport("ParquetSharpNative")]
		protected unsafe static extern IntPtr TypedColumnWriter_WriteBatch_Int64(IntPtr columnWriter, long numValues, short* defLevels, short* repLevels, long* values);

		// Token: 0x06000111 RID: 273
		[DllImport("ParquetSharpNative")]
		protected unsafe static extern IntPtr TypedColumnWriter_WriteBatch_Int96(IntPtr columnWriter, long numValues, short* defLevels, short* repLevels, Int96* values);

		// Token: 0x06000112 RID: 274
		[DllImport("ParquetSharpNative")]
		protected unsafe static extern IntPtr TypedColumnWriter_WriteBatch_Float(IntPtr columnWriter, long numValues, short* defLevels, short* repLevels, float* values);

		// Token: 0x06000113 RID: 275
		[DllImport("ParquetSharpNative")]
		protected unsafe static extern IntPtr TypedColumnWriter_WriteBatch_Double(IntPtr columnWriter, long numValues, short* defLevels, short* repLevels, double* values);

		// Token: 0x06000114 RID: 276
		[DllImport("ParquetSharpNative")]
		protected unsafe static extern IntPtr TypedColumnWriter_WriteBatch_ByteArray(IntPtr columnWriter, long numValues, short* defLevels, short* repLevels, ByteArray* values);

		// Token: 0x06000115 RID: 277
		[DllImport("ParquetSharpNative")]
		protected unsafe static extern IntPtr TypedColumnWriter_WriteBatch_FixedLenByteArray(IntPtr columnWriter, long numValues, short* defLevels, short* repLevels, FixedLenByteArray* values);

		// Token: 0x06000116 RID: 278
		[DllImport("ParquetSharpNative")]
		protected unsafe static extern IntPtr TypedColumnWriter_WriteBatchSpaced_Bool(IntPtr columnWriter, long numValues, short* defLevels, short* repLevels, byte* validBits, long validBitsOffset, bool* values);

		// Token: 0x06000117 RID: 279
		[DllImport("ParquetSharpNative")]
		protected unsafe static extern IntPtr TypedColumnWriter_WriteBatchSpaced_Int32(IntPtr columnWriter, long numValues, short* defLevels, short* repLevels, byte* validBits, long validBitsOffset, int* values);

		// Token: 0x06000118 RID: 280
		[DllImport("ParquetSharpNative")]
		protected unsafe static extern IntPtr TypedColumnWriter_WriteBatchSpaced_Int64(IntPtr columnWriter, long numValues, short* defLevels, short* repLevels, byte* validBits, long validBitsOffset, long* values);

		// Token: 0x06000119 RID: 281
		[DllImport("ParquetSharpNative")]
		protected unsafe static extern IntPtr TypedColumnWriter_WriteBatchSpaced_Int96(IntPtr columnWriter, long numValues, short* defLevels, short* repLevels, byte* validBits, long validBitsOffset, Int96* values);

		// Token: 0x0600011A RID: 282
		[DllImport("ParquetSharpNative")]
		protected unsafe static extern IntPtr TypedColumnWriter_WriteBatchSpaced_Float(IntPtr columnWriter, long numValues, short* defLevels, short* repLevels, byte* validBits, long validBitsOffset, float* values);

		// Token: 0x0600011B RID: 283
		[DllImport("ParquetSharpNative")]
		protected unsafe static extern IntPtr TypedColumnWriter_WriteBatchSpaced_Double(IntPtr columnWriter, long numValues, short* defLevels, short* repLevels, byte* validBits, long validBitsOffset, double* values);

		// Token: 0x0600011C RID: 284
		[DllImport("ParquetSharpNative")]
		protected unsafe static extern IntPtr TypedColumnWriter_WriteBatchSpaced_ByteArray(IntPtr columnWriter, long numValues, short* defLevels, short* repLevels, byte* validBits, long validBitsOffset, ByteArray* values);

		// Token: 0x0600011D RID: 285
		[DllImport("ParquetSharpNative")]
		protected unsafe static extern IntPtr TypedColumnWriter_WriteBatchSpaced_FixedLenByteArray(IntPtr columnWriter, long numValues, short* defLevels, short* repLevels, byte* validBits, long validBitsOffset, FixedLenByteArray* values);

		// Token: 0x0400003B RID: 59
		protected readonly IntPtr Handle;

		// Token: 0x0400003C RID: 60
		[Nullable(1)]
		internal readonly RowGroupWriter RowGroupWriter;
	}
}
