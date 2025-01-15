using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ParquetSharp.Schema;

namespace ParquetSharp
{
	// Token: 0x02000092 RID: 146
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class WriterProperties : IDisposable
	{
		// Token: 0x060003F3 RID: 1011 RVA: 0x0000EBBC File Offset: 0x0000CDBC
		public static WriterProperties GetDefaultWriterProperties()
		{
			return new WriterProperties(ExceptionInfo.Return<IntPtr>(new ExceptionInfo.GetAction<IntPtr>(WriterProperties.WriterProperties_Get_Default_Writer_Properties)));
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000EBD4 File Offset: 0x0000CDD4
		internal WriterProperties(IntPtr handle)
		{
			this.Handle = new ParquetHandle(handle, new Action<IntPtr>(WriterProperties.WriterProperties_Free));
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000EBF4 File Offset: 0x0000CDF4
		public void Dispose()
		{
			this.Handle.Dispose();
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x0000EC04 File Offset: 0x0000CE04
		public string CreatedBy
		{
			get
			{
				return ExceptionInfo.ReturnString(this.Handle, new ExceptionInfo.GetFunction<IntPtr>(WriterProperties.WriterProperties_Created_By), new Action<IntPtr>(WriterProperties.WriterProperties_Created_By_Free));
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x0000EC2C File Offset: 0x0000CE2C
		public long DataPageSize
		{
			get
			{
				return ExceptionInfo.Return<long>(this.Handle, new ExceptionInfo.GetFunction<long>(WriterProperties.WriterProperties_Data_Pagesize));
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x0000EC48 File Offset: 0x0000CE48
		public Encoding DictionaryIndexEncoding
		{
			get
			{
				return ExceptionInfo.Return<Encoding>(this.Handle, new ExceptionInfo.GetFunction<Encoding>(WriterProperties.WriterProperties_Dictionary_Index_Encoding));
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x0000EC64 File Offset: 0x0000CE64
		public Encoding DictionaryPageEncoding
		{
			get
			{
				return ExceptionInfo.Return<Encoding>(this.Handle, new ExceptionInfo.GetFunction<Encoding>(WriterProperties.WriterProperties_Dictionary_Page_Encoding));
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x0000EC80 File Offset: 0x0000CE80
		public long DictionaryPagesizeLimit
		{
			get
			{
				return ExceptionInfo.Return<long>(this.Handle, new ExceptionInfo.GetFunction<long>(WriterProperties.WriterProperties_Dictionary_Pagesize_Limit));
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x0000EC9C File Offset: 0x0000CE9C
		public FileEncryptionProperties FileEncryptionProperties
		{
			get
			{
				return new FileEncryptionProperties(ExceptionInfo.Return<IntPtr>(this.Handle, new ExceptionInfo.GetFunction<IntPtr>(WriterProperties.WriterProperties_File_Encryption_Properties)));
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x0000ECBC File Offset: 0x0000CEBC
		public long MaxRowGroupLength
		{
			get
			{
				return ExceptionInfo.Return<long>(this.Handle, new ExceptionInfo.GetFunction<long>(WriterProperties.WriterProperties_Max_Row_Group_Length));
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x0000ECD8 File Offset: 0x0000CED8
		public ParquetVersion Version
		{
			get
			{
				return ExceptionInfo.Return<ParquetVersion>(this.Handle, new ExceptionInfo.GetFunction<ParquetVersion>(WriterProperties.WriterProperties_Version));
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x0000ECF4 File Offset: 0x0000CEF4
		public long WriteBatchSize
		{
			get
			{
				return ExceptionInfo.Return<long>(this.Handle, new ExceptionInfo.GetFunction<long>(WriterProperties.WriterProperties_Write_Batch_Size));
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x0000ED10 File Offset: 0x0000CF10
		public bool WritePageIndex
		{
			get
			{
				return ExceptionInfo.Return<bool>(this.Handle, new ExceptionInfo.GetFunction<bool>(WriterProperties.WriterProperties_Write_Page_Index));
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x0000ED2C File Offset: 0x0000CF2C
		public bool ShuffleEnabled
		{
			get
			{
				return ExceptionInfo.Return<bool>(this.Handle, new ExceptionInfo.GetFunction<bool>(WriterProperties.WriterProperties_Shuffle_Enabled));
			}
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000ED48 File Offset: 0x0000CF48
		public Compression Compression(ColumnPath path)
		{
			return ExceptionInfo.Return<Compression>(this.Handle, path.Handle, new ExceptionInfo.GetFunction<IntPtr, Compression>(WriterProperties.WriterProperties_Compression));
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000ED68 File Offset: 0x0000CF68
		public int CompressionLevel(ColumnPath path)
		{
			return ExceptionInfo.Return<int>(this.Handle, path.Handle, new ExceptionInfo.GetFunction<IntPtr, int>(WriterProperties.WriterProperties_Compression_Level));
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000ED88 File Offset: 0x0000CF88
		public bool DictionaryEnabled(ColumnPath path)
		{
			return ExceptionInfo.Return<bool>(this.Handle, path.Handle, new ExceptionInfo.GetFunction<IntPtr, bool>(WriterProperties.WriterProperties_Dictionary_Enabled));
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000EDA8 File Offset: 0x0000CFA8
		public Encoding Encoding(ColumnPath path)
		{
			return ExceptionInfo.Return<Encoding>(this.Handle, path.Handle, new ExceptionInfo.GetFunction<IntPtr, Encoding>(WriterProperties.WriterProperties_Encoding));
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000EDC8 File Offset: 0x0000CFC8
		public bool StatisticsEnabled(ColumnPath path)
		{
			return ExceptionInfo.Return<bool>(this.Handle, path.Handle, new ExceptionInfo.GetFunction<IntPtr, bool>(WriterProperties.WriterProperties_Statistics_Enabled));
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000EDE8 File Offset: 0x0000CFE8
		public ulong MaxStatisticsSize(ColumnPath path)
		{
			return ExceptionInfo.Return<ulong>(this.Handle, path.Handle, new ExceptionInfo.GetFunction<IntPtr, ulong>(WriterProperties.WriterProperties_Max_Statistics_Size));
		}

		// Token: 0x06000407 RID: 1031
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr WriterProperties_Get_Default_Writer_Properties(out IntPtr writerProperties);

		// Token: 0x06000408 RID: 1032
		[DllImport("ParquetSharpNative")]
		private static extern void WriterProperties_Free(IntPtr writerProperties);

		// Token: 0x06000409 RID: 1033
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr WriterProperties_Created_By(IntPtr writerProperties, out IntPtr createdBy);

		// Token: 0x0600040A RID: 1034
		[DllImport("ParquetSharpNative")]
		private static extern void WriterProperties_Created_By_Free(IntPtr cstr);

		// Token: 0x0600040B RID: 1035
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr WriterProperties_Data_Pagesize(IntPtr writerProperties, out long dataPageSize);

		// Token: 0x0600040C RID: 1036
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr WriterProperties_Dictionary_Index_Encoding(IntPtr writerProperties, out Encoding encoding);

		// Token: 0x0600040D RID: 1037
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr WriterProperties_Dictionary_Page_Encoding(IntPtr writerProperties, out Encoding encoding);

		// Token: 0x0600040E RID: 1038
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr WriterProperties_Dictionary_Pagesize_Limit(IntPtr writerProperties, out long pagesizeLimit);

		// Token: 0x0600040F RID: 1039
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr WriterProperties_Max_Row_Group_Length(IntPtr writerProperties, out long length);

		// Token: 0x06000410 RID: 1040
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr WriterProperties_Version(IntPtr writerProperties, out ParquetVersion version);

		// Token: 0x06000411 RID: 1041
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr WriterProperties_Write_Batch_Size(IntPtr writerProperties, out long size);

		// Token: 0x06000412 RID: 1042
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr WriterProperties_Write_Page_Index(IntPtr writerProperties, out bool enabled);

		// Token: 0x06000413 RID: 1043
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr WriterProperties_Shuffle_Enabled(IntPtr writerProperties, [MarshalAs(UnmanagedType.I1)] out bool enabled);

		// Token: 0x06000414 RID: 1044
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr WriterProperties_Compression(IntPtr writerProperties, IntPtr path, out Compression compression);

		// Token: 0x06000415 RID: 1045
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr WriterProperties_Compression_Level(IntPtr writerProperties, IntPtr path, out int compressionLevel);

		// Token: 0x06000416 RID: 1046
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr WriterProperties_Dictionary_Enabled(IntPtr writerProperties, IntPtr path, [MarshalAs(UnmanagedType.I1)] out bool enabled);

		// Token: 0x06000417 RID: 1047
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr WriterProperties_Encoding(IntPtr writerProperties, IntPtr path, out Encoding encoding);

		// Token: 0x06000418 RID: 1048
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr WriterProperties_File_Encryption_Properties(IntPtr writerProperties, out IntPtr fileEncryptionProperties);

		// Token: 0x06000419 RID: 1049
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr WriterProperties_Statistics_Enabled(IntPtr writerProperties, IntPtr path, [MarshalAs(UnmanagedType.I1)] out bool enabled);

		// Token: 0x0600041A RID: 1050
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr WriterProperties_Max_Statistics_Size(IntPtr writerProperties, IntPtr path, [MarshalAs(UnmanagedType.I1)] out ulong maxStatisticsSize);

		// Token: 0x0400013A RID: 314
		internal readonly ParquetHandle Handle;
	}
}
