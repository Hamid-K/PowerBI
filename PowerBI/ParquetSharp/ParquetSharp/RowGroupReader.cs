using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ParquetSharp
{
	// Token: 0x02000085 RID: 133
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class RowGroupReader : IDisposable
	{
		// Token: 0x06000389 RID: 905 RVA: 0x0000DFBC File Offset: 0x0000C1BC
		internal RowGroupReader(IntPtr handle, ParquetFileReader parquetFileReader)
		{
			this._handle = new ParquetHandle(handle, new Action<IntPtr>(RowGroupReader.RowGroupReader_Free));
			this.ParquetFileReader = parquetFileReader;
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000DFE4 File Offset: 0x0000C1E4
		public void Dispose()
		{
			this._handle.Dispose();
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600038B RID: 907 RVA: 0x0000DFF4 File Offset: 0x0000C1F4
		public RowGroupMetaData MetaData
		{
			get
			{
				RowGroupMetaData rowGroupMetaData;
				if ((rowGroupMetaData = this._metaData) == null)
				{
					rowGroupMetaData = (this._metaData = new RowGroupMetaData(ExceptionInfo.Return<IntPtr>(this._handle, new ExceptionInfo.GetFunction<IntPtr>(RowGroupReader.RowGroupReader_Metadata))));
				}
				return rowGroupMetaData;
			}
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000E038 File Offset: 0x0000C238
		public ColumnReader Column(int i)
		{
			return ColumnReader.Create(ExceptionInfo.Return<int, IntPtr>(this._handle, i, new ExceptionInfo.GetFunction<int, IntPtr>(RowGroupReader.RowGroupReader_Column)), this, this.MetaData.GetColumnChunkMetaData(i), i);
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000E068 File Offset: 0x0000C268
		public ColumnReader ColumnWithExposeEncoding(int i, ExposedEncoding encodingToExpose)
		{
			return ColumnReader.Create(ExceptionInfo.Return<int, ExposedEncoding, IntPtr>(this._handle, i, encodingToExpose, new ExceptionInfo.GetFunction<int, ExposedEncoding, IntPtr>(RowGroupReader.RowGroupReader_ColumnWithExposeEncoding)), this, this.MetaData.GetColumnChunkMetaData(i), i);
		}

		// Token: 0x0600038E RID: 910
		[DllImport("ParquetSharpNative")]
		private static extern void RowGroupReader_Free(IntPtr rowGroupReader);

		// Token: 0x0600038F RID: 911
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr RowGroupReader_Column(IntPtr rowGroupReader, int i, out IntPtr columnReader);

		// Token: 0x06000390 RID: 912
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr RowGroupReader_ColumnWithExposeEncoding(IntPtr rowGroupReader, int i, ExposedEncoding encodingToExpose, out IntPtr columnReader);

		// Token: 0x06000391 RID: 913
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr RowGroupReader_Metadata(IntPtr rowGroupReader, out IntPtr rowGroupMetaData);

		// Token: 0x0400010F RID: 271
		private readonly ParquetHandle _handle;

		// Token: 0x04000110 RID: 272
		internal readonly ParquetFileReader ParquetFileReader;

		// Token: 0x04000111 RID: 273
		[Nullable(2)]
		private RowGroupMetaData _metaData;
	}
}
