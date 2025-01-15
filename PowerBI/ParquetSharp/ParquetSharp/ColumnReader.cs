using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ParquetSharp
{
	// Token: 0x0200001C RID: 28
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class ColumnReader : IDisposable
	{
		// Token: 0x060000B1 RID: 177 RVA: 0x00003D14 File Offset: 0x00001F14
		internal static ColumnReader Create(IntPtr handle, RowGroupReader rowGroupReader, ColumnChunkMetaData columnChunkMetaData, int columnIndex)
		{
			ParquetHandle parquetHandle = new ParquetHandle(handle, new Action<IntPtr>(ColumnReader.ColumnReader_Free));
			ColumnReader columnReader;
			try
			{
				PhysicalType physicalType = ExceptionInfo.Return<PhysicalType>(handle, new ExceptionInfo.GetFunction<PhysicalType>(ColumnReader.ColumnReader_Type));
				switch (physicalType)
				{
				case PhysicalType.Boolean:
					columnReader = new BoolColumnReader(parquetHandle, rowGroupReader, columnChunkMetaData, columnIndex);
					break;
				case PhysicalType.Int32:
					columnReader = new Int32ColumnReader(parquetHandle, rowGroupReader, columnChunkMetaData, columnIndex);
					break;
				case PhysicalType.Int64:
					columnReader = new Int64ColumnReader(parquetHandle, rowGroupReader, columnChunkMetaData, columnIndex);
					break;
				case PhysicalType.Int96:
					columnReader = new Int96ColumnReader(parquetHandle, rowGroupReader, columnChunkMetaData, columnIndex);
					break;
				case PhysicalType.Float:
					columnReader = new FloatColumnReader(parquetHandle, rowGroupReader, columnChunkMetaData, columnIndex);
					break;
				case PhysicalType.Double:
					columnReader = new DoubleColumnReader(parquetHandle, rowGroupReader, columnChunkMetaData, columnIndex);
					break;
				case PhysicalType.ByteArray:
					columnReader = new ByteArrayColumnReader(parquetHandle, rowGroupReader, columnChunkMetaData, columnIndex);
					break;
				case PhysicalType.FixedLenByteArray:
					columnReader = new FixedLenByteArrayColumnReader(parquetHandle, rowGroupReader, columnChunkMetaData, columnIndex);
					break;
				default:
					throw new NotSupportedException(string.Format("Physical type {0} is not supported", physicalType));
				}
			}
			catch
			{
				parquetHandle.Dispose();
				throw;
			}
			return columnReader;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003E1C File Offset: 0x0000201C
		internal ColumnReader(ParquetHandle handle, RowGroupReader rowGroupReader, ColumnChunkMetaData columnChunkMetaData, int columnIndex)
		{
			this.Handle = handle;
			this.RowGroupReader = rowGroupReader;
			this.ColumnChunkMetaData = columnChunkMetaData;
			this.ColumnIndex = columnIndex;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003E44 File Offset: 0x00002044
		public void Dispose()
		{
			this.ColumnChunkMetaData.Dispose();
			this.Handle.Dispose();
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00003E5C File Offset: 0x0000205C
		public int ColumnIndex { get; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00003E64 File Offset: 0x00002064
		public LogicalTypeFactory LogicalTypeFactory
		{
			get
			{
				return this.RowGroupReader.ParquetFileReader.LogicalTypeFactory;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00003E78 File Offset: 0x00002078
		public LogicalReadConverterFactory LogicalReadConverterFactory
		{
			get
			{
				return this.RowGroupReader.ParquetFileReader.LogicalReadConverterFactory;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00003E8C File Offset: 0x0000208C
		public ColumnDescriptor ColumnDescriptor
		{
			get
			{
				return new ColumnDescriptor(ExceptionInfo.Return<IntPtr>(this.Handle, new ExceptionInfo.GetFunction<IntPtr>(ColumnReader.ColumnReader_Descr)));
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00003EAC File Offset: 0x000020AC
		public bool HasNext
		{
			get
			{
				return ExceptionInfo.Return<bool>(this.Handle, new ExceptionInfo.GetFunction<bool>(ColumnReader.ColumnReader_HasNext));
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00003EC8 File Offset: 0x000020C8
		public PhysicalType Type
		{
			get
			{
				return ExceptionInfo.Return<PhysicalType>(this.Handle, new ExceptionInfo.GetFunction<PhysicalType>(ColumnReader.ColumnReader_Type));
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00003EE4 File Offset: 0x000020E4
		public ExposedEncoding ExposedEncoding
		{
			get
			{
				return ExceptionInfo.Return<ExposedEncoding>(this.Handle, new ExceptionInfo.GetFunction<ExposedEncoding>(ColumnReader.ColumnReader_ExposedEncoding));
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000BB RID: 187
		public abstract Type ElementType { get; }

		// Token: 0x060000BC RID: 188
		public abstract TReturn Apply<[Nullable(2)] TReturn>(IColumnReaderVisitor<TReturn> visitor);

		// Token: 0x060000BD RID: 189
		public abstract long Skip(long numRowsToSkip);

		// Token: 0x060000BE RID: 190 RVA: 0x00003F00 File Offset: 0x00002100
		public LogicalColumnReader LogicalReader(int bufferLength = 4096)
		{
			return LogicalColumnReader.Create(this, bufferLength, null, false);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003F0C File Offset: 0x0000210C
		public LogicalColumnReader LogicalReader(bool useNesting, int bufferLength = 4096)
		{
			return LogicalColumnReader.Create(this, bufferLength, null, useNesting);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003F18 File Offset: 0x00002118
		public LogicalColumnReader<TElement> LogicalReader<[Nullable(2)] TElement>(int bufferLength = 4096)
		{
			return LogicalColumnReader.Create<TElement>(this, bufferLength, null);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003F24 File Offset: 0x00002124
		public LogicalColumnReader<TElement> LogicalReaderOverride<[Nullable(2)] TElement>(int bufferLength = 4096)
		{
			return LogicalColumnReader.Create<TElement>(this, bufferLength, typeof(TElement));
		}

		// Token: 0x060000C2 RID: 194
		[DllImport("ParquetSharpNative")]
		private static extern void ColumnReader_Free(IntPtr columnReader);

		// Token: 0x060000C3 RID: 195
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnReader_Descr(IntPtr columnReader, out IntPtr columnDescriptor);

		// Token: 0x060000C4 RID: 196
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnReader_HasNext(IntPtr columnReader, [MarshalAs(UnmanagedType.I1)] out bool hasNext);

		// Token: 0x060000C5 RID: 197
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnReader_ExposedEncoding(IntPtr columnReader, out ExposedEncoding exposedEncoding);

		// Token: 0x060000C6 RID: 198
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnReader_Type(IntPtr columnReader, out PhysicalType type);

		// Token: 0x060000C7 RID: 199
		[NullableContext(0)]
		[DllImport("ParquetSharpNative")]
		protected unsafe static extern IntPtr TypedColumnReader_ReadDictionary_Bool(IntPtr columnReader, out bool* dictionaryValues, out int valuesRead);

		// Token: 0x060000C8 RID: 200
		[NullableContext(0)]
		[DllImport("ParquetSharpNative")]
		protected unsafe static extern IntPtr TypedColumnReader_ReadDictionary_Int32(IntPtr columnReader, out int* dictionaryValues, out int valuesRead);

		// Token: 0x060000C9 RID: 201
		[NullableContext(0)]
		[DllImport("ParquetSharpNative")]
		protected unsafe static extern IntPtr TypedColumnReader_ReadDictionary_Int64(IntPtr columnReader, out long* dictionaryValues, out int valuesRead);

		// Token: 0x060000CA RID: 202
		[NullableContext(0)]
		[DllImport("ParquetSharpNative")]
		protected unsafe static extern IntPtr TypedColumnReader_ReadDictionary_Int96(IntPtr columnReader, out Int96* dictionaryValues, out int valuesRead);

		// Token: 0x060000CB RID: 203
		[NullableContext(0)]
		[DllImport("ParquetSharpNative")]
		protected unsafe static extern IntPtr TypedColumnReader_ReadDictionary_Float(IntPtr columnReader, out float* dictionaryValues, out int valuesRead);

		// Token: 0x060000CC RID: 204
		[NullableContext(0)]
		[DllImport("ParquetSharpNative")]
		protected unsafe static extern IntPtr TypedColumnReader_ReadDictionary_Double(IntPtr columnReader, out double* dictionaryValues, out int valuesRead);

		// Token: 0x060000CD RID: 205
		[NullableContext(0)]
		[DllImport("ParquetSharpNative")]
		protected unsafe static extern IntPtr TypedColumnReader_ReadDictionary_ByteArray(IntPtr columnReader, out ByteArray* dictionaryValues, out int valuesRead);

		// Token: 0x060000CE RID: 206
		[NullableContext(0)]
		[DllImport("ParquetSharpNative")]
		protected unsafe static extern IntPtr TypedColumnReader_ReadDictionary_FixedLenByteArray(IntPtr columnReader, out FixedLenByteArray* dictionaryValues, out int valuesRead);

		// Token: 0x060000CF RID: 207
		[DllImport("ParquetSharpNative")]
		protected static extern IntPtr TypedColumnReader_ReadEncodedData_Bool(IntPtr columnReader, ColumnReader.GetNewPage getNewPage);

		// Token: 0x060000D0 RID: 208
		[DllImport("ParquetSharpNative")]
		protected static extern IntPtr TypedColumnReader_ReadEncodedData_Int32(IntPtr columnReader, ColumnReader.GetNewPage getNewPage);

		// Token: 0x060000D1 RID: 209
		[DllImport("ParquetSharpNative")]
		protected static extern IntPtr TypedColumnReader_ReadEncodedData_Int64(IntPtr columnReader, ColumnReader.GetNewPage getNewPage);

		// Token: 0x060000D2 RID: 210
		[DllImport("ParquetSharpNative")]
		protected static extern IntPtr TypedColumnReader_ReadEncodedData_Int96(IntPtr columnReader, ColumnReader.GetNewPage getNewPage);

		// Token: 0x060000D3 RID: 211
		[DllImport("ParquetSharpNative")]
		protected static extern IntPtr TypedColumnReader_ReadEncodedData_Float(IntPtr columnReader, ColumnReader.GetNewPage getNewPage);

		// Token: 0x060000D4 RID: 212
		[DllImport("ParquetSharpNative")]
		protected static extern IntPtr TypedColumnReader_ReadEncodedData_Double(IntPtr columnReader, ColumnReader.GetNewPage getNewPage);

		// Token: 0x060000D5 RID: 213
		[DllImport("ParquetSharpNative")]
		protected static extern IntPtr TypedColumnReader_ReadEncodedData_ByteArray(IntPtr columnReader, ColumnReader.GetNewPage getNewPage);

		// Token: 0x060000D6 RID: 214
		[DllImport("ParquetSharpNative")]
		protected static extern IntPtr TypedColumnReader_ReadEncodedData_FixedLenByteArray(IntPtr columnReader, ColumnReader.GetNewPage getNewPage);

		// Token: 0x060000D7 RID: 215
		[NullableContext(0)]
		[DllImport("ParquetSharpNative")]
		protected unsafe static extern IntPtr TypedColumnReader_ReadBatch_Bool(IntPtr columnReader, long batchSize, short* defLevels, short* repLevels, bool* values, out long valuesRead, out long levelsRead);

		// Token: 0x060000D8 RID: 216
		[NullableContext(0)]
		[DllImport("ParquetSharpNative")]
		protected unsafe static extern IntPtr TypedColumnReader_ReadBatch_Int32(IntPtr columnReader, long batchSize, short* defLevels, short* repLevels, int* values, out long valuesRead, out long levelsRead);

		// Token: 0x060000D9 RID: 217
		[NullableContext(0)]
		[DllImport("ParquetSharpNative")]
		protected unsafe static extern IntPtr TypedColumnReader_ReadBatch_Int64(IntPtr columnReader, long batchSize, short* defLevels, short* repLevels, long* values, out long valuesRead, out long levelsRead);

		// Token: 0x060000DA RID: 218
		[NullableContext(0)]
		[DllImport("ParquetSharpNative")]
		protected unsafe static extern IntPtr TypedColumnReader_ReadBatch_Int96(IntPtr columnReader, long batchSize, short* defLevels, short* repLevels, Int96* values, out long valuesRead, out long levelsRead);

		// Token: 0x060000DB RID: 219
		[NullableContext(0)]
		[DllImport("ParquetSharpNative")]
		protected unsafe static extern IntPtr TypedColumnReader_ReadBatch_Float(IntPtr columnReader, long batchSize, short* defLevels, short* repLevels, float* values, out long valuesRead, out long levelsRead);

		// Token: 0x060000DC RID: 220
		[NullableContext(0)]
		[DllImport("ParquetSharpNative")]
		protected unsafe static extern IntPtr TypedColumnReader_ReadBatch_Double(IntPtr columnReader, long batchSize, short* defLevels, short* repLevels, double* values, out long valuesRead, out long levelsRead);

		// Token: 0x060000DD RID: 221
		[NullableContext(0)]
		[DllImport("ParquetSharpNative")]
		protected unsafe static extern IntPtr TypedColumnReader_ReadBatch_ByteArray(IntPtr columnReader, long batchSize, short* defLevels, short* repLevels, ByteArray* values, out long valuesRead, out long levelsRead);

		// Token: 0x060000DE RID: 222
		[NullableContext(0)]
		[DllImport("ParquetSharpNative")]
		protected unsafe static extern IntPtr TypedColumnReader_ReadBatch_FixedLenByteArray(IntPtr columnReader, long batchSize, short* defLevels, short* repLevels, FixedLenByteArray* values, out long valuesRead, out long levelsRead);

		// Token: 0x060000DF RID: 223
		[DllImport("ParquetSharpNative")]
		protected static extern IntPtr TypedColumnReader_Skip_Bool(IntPtr columnReader, long numRowsToSkip, out long levelsSkipped);

		// Token: 0x060000E0 RID: 224
		[DllImport("ParquetSharpNative")]
		protected static extern IntPtr TypedColumnReader_Skip_Int32(IntPtr columnReader, long numRowsToSkip, out long levelsSkipped);

		// Token: 0x060000E1 RID: 225
		[DllImport("ParquetSharpNative")]
		protected static extern IntPtr TypedColumnReader_Skip_Int64(IntPtr columnReader, long numRowsToSkip, out long levelsSkipped);

		// Token: 0x060000E2 RID: 226
		[DllImport("ParquetSharpNative")]
		protected static extern IntPtr TypedColumnReader_Skip_Int96(IntPtr columnReader, long numRowsToSkip, out long levelsSkipped);

		// Token: 0x060000E3 RID: 227
		[DllImport("ParquetSharpNative")]
		protected static extern IntPtr TypedColumnReader_Skip_Float(IntPtr columnReader, long numRowsToSkip, out long levelsSkipped);

		// Token: 0x060000E4 RID: 228
		[DllImport("ParquetSharpNative")]
		protected static extern IntPtr TypedColumnReader_Skip_Double(IntPtr columnReader, long numRowsToSkip, out long levelsSkipped);

		// Token: 0x060000E5 RID: 229
		[DllImport("ParquetSharpNative")]
		protected static extern IntPtr TypedColumnReader_Skip_ByteArray(IntPtr columnReader, long numRowsToSkip, out long levelsSkipped);

		// Token: 0x060000E6 RID: 230
		[DllImport("ParquetSharpNative")]
		protected static extern IntPtr TypedColumnReader_Skip_FixedLenByteArray(IntPtr columnReader, long numRowsToSkip, out long levelsSkipped);

		// Token: 0x04000037 RID: 55
		internal readonly ParquetHandle Handle;

		// Token: 0x04000038 RID: 56
		internal readonly RowGroupReader RowGroupReader;

		// Token: 0x04000039 RID: 57
		internal readonly ColumnChunkMetaData ColumnChunkMetaData;

		// Token: 0x020000FF RID: 255
		// (Invoke) Token: 0x0600091E RID: 2334
		[NullableContext(0)]
		public delegate ParquetDataPageDescriptor GetNewPage();
	}
}
