using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ParquetSharp
{
	// Token: 0x02000084 RID: 132
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class RowGroupMetaData
	{
		// Token: 0x0600037E RID: 894 RVA: 0x0000DEF4 File Offset: 0x0000C0F4
		internal RowGroupMetaData(IntPtr handle)
		{
			this._handle = handle;
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600037F RID: 895 RVA: 0x0000DF04 File Offset: 0x0000C104
		public int NumColumns
		{
			get
			{
				return ExceptionInfo.Return<int>(this._handle, new ExceptionInfo.GetFunction<int>(RowGroupMetaData.RowGroupMetaData_Num_Columns));
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000380 RID: 896 RVA: 0x0000DF20 File Offset: 0x0000C120
		public long NumRows
		{
			get
			{
				return ExceptionInfo.Return<long>(this._handle, new ExceptionInfo.GetFunction<long>(RowGroupMetaData.RowGroupMetaData_Num_Rows));
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000381 RID: 897 RVA: 0x0000DF3C File Offset: 0x0000C13C
		public SchemaDescriptor Schema
		{
			get
			{
				SchemaDescriptor schemaDescriptor;
				if ((schemaDescriptor = this._schema) == null)
				{
					schemaDescriptor = (this._schema = new SchemaDescriptor(ExceptionInfo.Return<IntPtr>(this._handle, new ExceptionInfo.GetFunction<IntPtr>(RowGroupMetaData.RowGroupMetaData_Schema))));
				}
				return schemaDescriptor;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000382 RID: 898 RVA: 0x0000DF80 File Offset: 0x0000C180
		public long TotalByteSize
		{
			get
			{
				return ExceptionInfo.Return<long>(this._handle, new ExceptionInfo.GetFunction<long>(RowGroupMetaData.RowGroupMetaData_Total_Byte_Size));
			}
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000DF9C File Offset: 0x0000C19C
		public ColumnChunkMetaData GetColumnChunkMetaData(int i)
		{
			return new ColumnChunkMetaData(ExceptionInfo.Return<int, IntPtr>(this._handle, i, new ExceptionInfo.GetFunction<int, IntPtr>(RowGroupMetaData.RowGroupMetaData_Get_Column_Chunk_Meta_Data)));
		}

		// Token: 0x06000384 RID: 900
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr RowGroupMetaData_Get_Column_Chunk_Meta_Data(IntPtr rowGroupMetaData, int i, out IntPtr columnChunkMetaData);

		// Token: 0x06000385 RID: 901
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr RowGroupMetaData_Num_Columns(IntPtr rowGroupMetaData, out int numColumns);

		// Token: 0x06000386 RID: 902
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr RowGroupMetaData_Num_Rows(IntPtr rowGroupMetaData, out long numRows);

		// Token: 0x06000387 RID: 903
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr RowGroupMetaData_Schema(IntPtr rowGroupMetaData, out IntPtr schemaDescriptor);

		// Token: 0x06000388 RID: 904
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr RowGroupMetaData_Total_Byte_Size(IntPtr rowGroupMetaData, out long totalByteSize);

		// Token: 0x0400010D RID: 269
		private readonly IntPtr _handle;

		// Token: 0x0400010E RID: 270
		[Nullable(2)]
		private SchemaDescriptor _schema;
	}
}
