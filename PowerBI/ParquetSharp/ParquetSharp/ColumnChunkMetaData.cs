using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ParquetSharp
{
	// Token: 0x02000014 RID: 20
	[NullableContext(2)]
	[Nullable(0)]
	public sealed class ColumnChunkMetaData : IDisposable
	{
		// Token: 0x06000037 RID: 55 RVA: 0x00003154 File Offset: 0x00001354
		internal ColumnChunkMetaData(IntPtr handle)
		{
			this._handle = new ParquetHandle(handle, new Action<IntPtr>(ColumnChunkMetaData.ColumnChunkMetaData_Free));
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003174 File Offset: 0x00001374
		public void Dispose()
		{
			this._handle.Dispose();
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00003184 File Offset: 0x00001384
		public Compression Compression
		{
			get
			{
				return ExceptionInfo.Return<Compression>(this._handle, new ExceptionInfo.GetFunction<Compression>(ColumnChunkMetaData.ColumnChunkMetaData_Compression));
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600003A RID: 58 RVA: 0x000031A0 File Offset: 0x000013A0
		public ColumnCryptoMetaData CryptoMetadata
		{
			get
			{
				IntPtr intPtr = ExceptionInfo.Return<IntPtr>(this._handle, new ExceptionInfo.GetFunction<IntPtr>(ColumnChunkMetaData.ColumnChunkMetaData_CryptoMetadata));
				if (!(intPtr == IntPtr.Zero))
				{
					return new ColumnCryptoMetaData(intPtr);
				}
				return null;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600003B RID: 59 RVA: 0x000031E4 File Offset: 0x000013E4
		[Nullable(1)]
		public unsafe Encoding[] Encodings
		{
			[NullableContext(1)]
			get
			{
				ulong num = ExceptionInfo.Return<ulong>(this._handle, new ExceptionInfo.GetFunction<ulong>(ColumnChunkMetaData.ColumnChunkMetaData_Encodings_Count));
				Encoding* ptr = (Encoding*)(void*)ExceptionInfo.Return<IntPtr>(this._handle, new ExceptionInfo.GetFunction<IntPtr>(ColumnChunkMetaData.ColumnChunkMetaData_Encodings));
				Encoding[] array2;
				Encoding[] array = (array2 = new Encoding[num]);
				Encoding* ptr2;
				if (array == null || array2.Length == 0)
				{
					ptr2 = null;
				}
				else
				{
					ptr2 = &array2[0];
				}
				Buffer.MemoryCopy((void*)ptr, (void*)ptr2, num * 4UL, num * 4UL);
				array2 = null;
				return array;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00003264 File Offset: 0x00001464
		public long FileOffset
		{
			get
			{
				return ExceptionInfo.Return<long>(this._handle, new ExceptionInfo.GetFunction<long>(ColumnChunkMetaData.ColumnChunkMetaData_File_Offset));
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00003280 File Offset: 0x00001480
		public bool IsStatsSet
		{
			get
			{
				return ExceptionInfo.Return<bool>(this._handle, new ExceptionInfo.GetFunction<bool>(ColumnChunkMetaData.ColumnChunkMetaData_Is_Stats_Set));
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600003E RID: 62 RVA: 0x0000329C File Offset: 0x0000149C
		public long NumValues
		{
			get
			{
				return ExceptionInfo.Return<long>(this._handle, new ExceptionInfo.GetFunction<long>(ColumnChunkMetaData.ColumnChunkMetaData_Num_Values));
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600003F RID: 63 RVA: 0x000032B8 File Offset: 0x000014B8
		public long TotalCompressedSize
		{
			get
			{
				return ExceptionInfo.Return<long>(this._handle, new ExceptionInfo.GetFunction<long>(ColumnChunkMetaData.ColumnChunkMetaData_Total_Compressed_Size));
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000032D4 File Offset: 0x000014D4
		public long TotalUncompressedSize
		{
			get
			{
				return ExceptionInfo.Return<long>(this._handle, new ExceptionInfo.GetFunction<long>(ColumnChunkMetaData.ColumnChunkMetaData_Total_Uncompressed_Size));
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000032F0 File Offset: 0x000014F0
		public Statistics Statistics
		{
			get
			{
				return Statistics.Create(ExceptionInfo.Return<IntPtr>(this._handle, new ExceptionInfo.GetFunction<IntPtr>(ColumnChunkMetaData.ColumnChunkMetaData_Statistics)));
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00003310 File Offset: 0x00001510
		public PhysicalType Type
		{
			get
			{
				return ExceptionInfo.Return<PhysicalType>(this._handle, new ExceptionInfo.GetFunction<PhysicalType>(ColumnChunkMetaData.ColumnChunkMetaData_Type));
			}
		}

		// Token: 0x06000043 RID: 67
		[DllImport("ParquetSharpNative")]
		private static extern void ColumnChunkMetaData_Free(IntPtr columnChunkMetaData);

		// Token: 0x06000044 RID: 68
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnChunkMetaData_Compression(IntPtr columnChunkMetaData, out Compression compression);

		// Token: 0x06000045 RID: 69
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnChunkMetaData_CryptoMetadata(IntPtr columnChunkMetaData, out IntPtr columnCryptoMetaData);

		// Token: 0x06000046 RID: 70
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnChunkMetaData_Encodings(IntPtr columnChunkMetaData, out IntPtr encodings);

		// Token: 0x06000047 RID: 71
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnChunkMetaData_Encodings_Count(IntPtr columnChunkMetaData, out ulong count);

		// Token: 0x06000048 RID: 72
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnChunkMetaData_File_Offset(IntPtr columnChunkMetaData, out long fileOffset);

		// Token: 0x06000049 RID: 73
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnChunkMetaData_Is_Stats_Set(IntPtr columnChunkMetaData, [MarshalAs(UnmanagedType.I1)] out bool isStatsSet);

		// Token: 0x0600004A RID: 74
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnChunkMetaData_Num_Values(IntPtr columnChunkMetaData, out long numValues);

		// Token: 0x0600004B RID: 75
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnChunkMetaData_Statistics(IntPtr columnChunkMetaData, out IntPtr statistics);

		// Token: 0x0600004C RID: 76
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnChunkMetaData_Total_Compressed_Size(IntPtr columnChunkMetaData, out long totalCompressedSize);

		// Token: 0x0600004D RID: 77
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnChunkMetaData_Total_Uncompressed_Size(IntPtr columnChunkMetaData, out long totalUncompressedSize);

		// Token: 0x0600004E RID: 78
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnChunkMetaData_Type(IntPtr columnChunkMetaData, out PhysicalType type);

		// Token: 0x0400002B RID: 43
		[Nullable(1)]
		private readonly ParquetHandle _handle;
	}
}
