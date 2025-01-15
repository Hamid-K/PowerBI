using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ParquetSharp
{
	// Token: 0x02000086 RID: 134
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class RowGroupWriter : IDisposable
	{
		// Token: 0x06000392 RID: 914 RVA: 0x0000E098 File Offset: 0x0000C298
		internal RowGroupWriter(IntPtr handle, ParquetFileWriter parquetFileWriter)
		{
			this._handle = handle;
			this.ParquetFileWriter = parquetFileWriter;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000E0B0 File Offset: 0x0000C2B0
		public void Dispose()
		{
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000E0B4 File Offset: 0x0000C2B4
		public void Close()
		{
			ExceptionInfo.Check(RowGroupWriter.RowGroupWriter_Close(this._handle));
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000395 RID: 917 RVA: 0x0000E0C8 File Offset: 0x0000C2C8
		public int CurrentColumn
		{
			get
			{
				return ExceptionInfo.Return<int>(this._handle, new ExceptionInfo.GetFunction<int>(RowGroupWriter.RowGroupWriter_Current_Column));
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000396 RID: 918 RVA: 0x0000E0E4 File Offset: 0x0000C2E4
		public int NumColumns
		{
			get
			{
				return ExceptionInfo.Return<int>(this._handle, new ExceptionInfo.GetFunction<int>(RowGroupWriter.RowGroupWriter_Num_Columns));
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000397 RID: 919 RVA: 0x0000E100 File Offset: 0x0000C300
		public long NumRows
		{
			get
			{
				return ExceptionInfo.Return<long>(this._handle, new ExceptionInfo.GetFunction<long>(RowGroupWriter.RowGroupWriter_Num_Rows));
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000398 RID: 920 RVA: 0x0000E11C File Offset: 0x0000C31C
		public long TotalBytesWritten
		{
			get
			{
				return ExceptionInfo.Return<long>(this._handle, new ExceptionInfo.GetFunction<long>(RowGroupWriter.RowGroupWriter_Total_Bytes_Written));
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000399 RID: 921 RVA: 0x0000E138 File Offset: 0x0000C338
		public long TotalCompressedBytes
		{
			get
			{
				return ExceptionInfo.Return<long>(this._handle, new ExceptionInfo.GetFunction<long>(RowGroupWriter.RowGroupWriter_Total_Compressed_Bytes));
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600039A RID: 922 RVA: 0x0000E154 File Offset: 0x0000C354
		public long EstimatedSize
		{
			get
			{
				return ExceptionInfo.Return<long>(this._handle, new ExceptionInfo.GetFunction<long>(RowGroupWriter.RowGroupWriter_Estimated_Size));
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600039B RID: 923 RVA: 0x0000E170 File Offset: 0x0000C370
		public long EstimatedMemoryUsage
		{
			get
			{
				return ExceptionInfo.Return<long>(this._handle, new ExceptionInfo.GetFunction<long>(RowGroupWriter.RowGroupWriter_Estimated_Memory_Usage));
			}
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000E18C File Offset: 0x0000C38C
		public long EstimatedMemoryUsageAddRows(long numRowsToAdd)
		{
			return ExceptionInfo.Return<long, long>(this._handle, numRowsToAdd, new ExceptionInfo.GetFunction<long, long>(RowGroupWriter.RowGroupWriter_Estimated_Memory_Usage_AddRows));
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000E1A8 File Offset: 0x0000C3A8
		public ColumnWriter Column(int i)
		{
			return ColumnWriter.Create(ExceptionInfo.Return<int, IntPtr>(this._handle, i, new ExceptionInfo.GetFunction<int, IntPtr>(RowGroupWriter.RowGroupWriter_Column)), this, i);
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000E1CC File Offset: 0x0000C3CC
		public ColumnWriter NextColumn()
		{
			return ColumnWriter.Create(ExceptionInfo.Return<IntPtr>(this._handle, new ExceptionInfo.GetFunction<IntPtr>(RowGroupWriter.RowGroupWriter_NextColumn)), this, this.CurrentColumn);
		}

		// Token: 0x0600039F RID: 927
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr RowGroupWriter_Close(IntPtr rowGroupWriter);

		// Token: 0x060003A0 RID: 928
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr RowGroupWriter_Column(IntPtr rowGroupWriter, int i, out IntPtr columnWriter);

		// Token: 0x060003A1 RID: 929
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr RowGroupWriter_Current_Column(IntPtr rowGroupWriter, out int currentColumn);

		// Token: 0x060003A2 RID: 930
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr RowGroupWriter_NextColumn(IntPtr rowGroupWriter, out IntPtr columnWriter);

		// Token: 0x060003A3 RID: 931
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr RowGroupWriter_Num_Columns(IntPtr rowGroupWriter, out int numColumns);

		// Token: 0x060003A4 RID: 932
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr RowGroupWriter_Num_Rows(IntPtr rowGroupWriter, out long numRows);

		// Token: 0x060003A5 RID: 933
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr RowGroupWriter_Total_Bytes_Written(IntPtr rowGroupWriter, out long totalBytesWritten);

		// Token: 0x060003A6 RID: 934
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr RowGroupWriter_Total_Compressed_Bytes(IntPtr rowGroupWriter, out long totalCompressedBytes);

		// Token: 0x060003A7 RID: 935
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr RowGroupWriter_Estimated_Size(IntPtr rowGroupWriter, out long estimatedSize);

		// Token: 0x060003A8 RID: 936
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr RowGroupWriter_Estimated_Memory_Usage(IntPtr rowGroupWriter, out long estimatedMemoryUsage);

		// Token: 0x060003A9 RID: 937
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr RowGroupWriter_Estimated_Memory_Usage_AddRows(IntPtr rowGroupWriter, long numRowsToAdd, out long estimatedMemoryUsage);

		// Token: 0x04000112 RID: 274
		private readonly IntPtr _handle;

		// Token: 0x04000113 RID: 275
		internal readonly ParquetFileWriter ParquetFileWriter;
	}
}
