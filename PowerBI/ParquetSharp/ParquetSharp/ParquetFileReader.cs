using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ParquetSharp.IO;

namespace ParquetSharp
{
	// Token: 0x0200007C RID: 124
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ParquetFileReader : IDisposable
	{
		// Token: 0x0600031E RID: 798 RVA: 0x0000CA40 File Offset: 0x0000AC40
		public ParquetFileReader(string path)
			: this(path, null)
		{
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000CA4C File Offset: 0x0000AC4C
		public ParquetFileReader(string path, bool memoryMap)
			: this(path, memoryMap, null)
		{
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000CA58 File Offset: 0x0000AC58
		public ParquetFileReader(RandomAccessFile randomAccessFile)
			: this(randomAccessFile, null)
		{
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000CA64 File Offset: 0x0000AC64
		public ParquetFileReader(string path, [Nullable(2)] ReaderProperties readerProperties)
			: this(path, false, readerProperties)
		{
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000CA70 File Offset: 0x0000AC70
		public ParquetFileReader(string path, AuthenticationContext authenticationContext)
			: this(path, authenticationContext, null)
		{
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000CA7C File Offset: 0x0000AC7C
		public ParquetFileReader(Stream stream, bool leaveOpen = false)
			: this(stream, null, leaveOpen)
		{
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000CA88 File Offset: 0x0000AC88
		public ParquetFileReader(string path, bool memoryMap, [Nullable(2)] ReaderProperties readerProperties)
		{
			this.LogicalTypeFactory = LogicalTypeFactory.Default;
			this.LogicalReadConverterFactory = LogicalReadConverterFactory.Default;
			base..ctor();
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			path = LongPath.EnsureLongPathSafe(path);
			if (ParquetFileReader.IsLakeFile(path))
			{
				throw new ArgumentException("Path is a lake path", "path");
			}
			using (ReaderProperties readerProperties2 = ((readerProperties == null) ? ReaderProperties.GetDefaultReaderProperties() : null))
			{
				ReaderProperties readerProperties3 = readerProperties ?? readerProperties2;
				using (ByteBuffer byteBuffer = new ByteBuffer(0, 0))
				{
					IntPtr intPtr;
					ExceptionInfo.Check(ParquetFileReader.ParquetFileReader_OpenFile(StringUtil.ToCStringUtf8(path, byteBuffer), memoryMap, readerProperties3.Handle.IntPtr, out intPtr));
					this._handle = new ParquetHandle(intPtr, new Action<IntPtr>(ParquetFileReader.ParquetFileReader_Free));
					GC.KeepAlive(readerProperties);
				}
			}
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000CB84 File Offset: 0x0000AD84
		public ParquetFileReader(string path, AuthenticationContext authenticationContext, [Nullable(2)] ReaderProperties readerProperties)
		{
			this.LogicalTypeFactory = LogicalTypeFactory.Default;
			this.LogicalReadConverterFactory = LogicalReadConverterFactory.Default;
			base..ctor();
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			path = LongPath.EnsureLongPathSafe(path);
			if (!ParquetFileReader.IsLakeFile(path))
			{
				throw new ArgumentException("Path is not a lake path", "path");
			}
			using (ReaderProperties readerProperties2 = ((readerProperties == null) ? ReaderProperties.GetDefaultReaderProperties() : null))
			{
				ReaderProperties readerProperties3 = readerProperties ?? readerProperties2;
				LakeFile lakeFile = new LakeFile(path, authenticationContext);
				this._handle = new ParquetHandle(ExceptionInfo.Return<IntPtr, IntPtr>(lakeFile.Handle, readerProperties3.Handle.IntPtr, new ExceptionInfo.GetFunction<IntPtr, IntPtr>(ParquetFileReader.ParquetFileReader_Open)), new Action<IntPtr>(ParquetFileReader.ParquetFileReader_Free));
				this._randomAccessFile = lakeFile;
				this._ownedFile = true;
				GC.KeepAlive(readerProperties);
			}
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000CC74 File Offset: 0x0000AE74
		public ParquetFileReader(RandomAccessFile randomAccessFile, [Nullable(2)] ReaderProperties readerProperties)
		{
			this.LogicalTypeFactory = LogicalTypeFactory.Default;
			this.LogicalReadConverterFactory = LogicalReadConverterFactory.Default;
			base..ctor();
			if (randomAccessFile == null)
			{
				throw new ArgumentNullException("randomAccessFile");
			}
			if (randomAccessFile.Handle == null)
			{
				throw new ArgumentNullException("Handle");
			}
			using (ReaderProperties readerProperties2 = ((readerProperties == null) ? ReaderProperties.GetDefaultReaderProperties() : null))
			{
				ReaderProperties readerProperties3 = readerProperties ?? readerProperties2;
				this._handle = new ParquetHandle(ExceptionInfo.Return<IntPtr, IntPtr>(randomAccessFile.Handle, readerProperties3.Handle.IntPtr, new ExceptionInfo.GetFunction<IntPtr, IntPtr>(ParquetFileReader.ParquetFileReader_Open)), new Action<IntPtr>(ParquetFileReader.ParquetFileReader_Free));
				this._randomAccessFile = randomAccessFile;
				GC.KeepAlive(readerProperties);
			}
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000CD48 File Offset: 0x0000AF48
		public ParquetFileReader(Stream stream, [Nullable(2)] ReaderProperties readerProperties, bool leaveOpen = false)
		{
			this.LogicalTypeFactory = LogicalTypeFactory.Default;
			this.LogicalReadConverterFactory = LogicalReadConverterFactory.Default;
			base..ctor();
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			using (ReaderProperties readerProperties2 = ((readerProperties == null) ? ReaderProperties.GetDefaultReaderProperties() : null))
			{
				ReaderProperties readerProperties3 = readerProperties ?? readerProperties2;
				ManagedRandomAccessFile managedRandomAccessFile = new ManagedRandomAccessFile(stream, leaveOpen);
				this._handle = new ParquetHandle(ExceptionInfo.Return<IntPtr, IntPtr>(managedRandomAccessFile.Handle, readerProperties3.Handle.IntPtr, new ExceptionInfo.GetFunction<IntPtr, IntPtr>(ParquetFileReader.ParquetFileReader_Open)), new Action<IntPtr>(ParquetFileReader.ParquetFileReader_Free));
				this._randomAccessFile = managedRandomAccessFile;
				this._ownedFile = true;
				GC.KeepAlive(readerProperties);
			}
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000CE18 File Offset: 0x0000B018
		public void Dispose()
		{
			FileMetaData fileMetaData = this._fileMetaData;
			if (fileMetaData != null)
			{
				fileMetaData.Dispose();
			}
			this._handle.Dispose();
			if (this._ownedFile)
			{
				RandomAccessFile randomAccessFile = this._randomAccessFile;
				if (randomAccessFile == null)
				{
					return;
				}
				randomAccessFile.Dispose();
			}
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000CE6C File Offset: 0x0000B06C
		public void Close()
		{
			ExceptionInfo.Check(ParquetFileReader.ParquetFileReader_Close(this._handle.IntPtr));
			GC.KeepAlive(this._handle);
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600032A RID: 810 RVA: 0x0000CE90 File Offset: 0x0000B090
		// (set) Token: 0x0600032B RID: 811 RVA: 0x0000CE98 File Offset: 0x0000B098
		public LogicalTypeFactory LogicalTypeFactory { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600032C RID: 812 RVA: 0x0000CEA4 File Offset: 0x0000B0A4
		// (set) Token: 0x0600032D RID: 813 RVA: 0x0000CEAC File Offset: 0x0000B0AC
		public LogicalReadConverterFactory LogicalReadConverterFactory { get; set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600032E RID: 814 RVA: 0x0000CEB8 File Offset: 0x0000B0B8
		public FileMetaData FileMetaData
		{
			get
			{
				FileMetaData fileMetaData;
				if ((fileMetaData = this._fileMetaData) == null)
				{
					fileMetaData = (this._fileMetaData = new FileMetaData(ExceptionInfo.Return<IntPtr>(this._handle, new ExceptionInfo.GetFunction<IntPtr>(ParquetFileReader.ParquetFileReader_MetaData))));
				}
				return fileMetaData;
			}
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000CEFC File Offset: 0x0000B0FC
		public RowGroupReader RowGroup(int i)
		{
			return new RowGroupReader(ExceptionInfo.Return<int, IntPtr>(this._handle, i, new ExceptionInfo.GetFunction<int, IntPtr>(ParquetFileReader.ParquetFileReader_RowGroup)), this);
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000CF1C File Offset: 0x0000B11C
		public static bool IsLakeFile(string path)
		{
			string text = path.ToLower();
			return text.StartsWith("http://") || text.StartsWith("https://");
		}

		// Token: 0x06000331 RID: 817
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ParquetFileReader_OpenFile(IntPtr path, [MarshalAs(UnmanagedType.I1)] bool memoryMap, IntPtr readerProperties, out IntPtr reader);

		// Token: 0x06000332 RID: 818
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ParquetFileReader_Open(IntPtr readableFileInterface, IntPtr readerProperties, out IntPtr reader);

		// Token: 0x06000333 RID: 819
		[DllImport("ParquetSharpNative")]
		private static extern void ParquetFileReader_Free(IntPtr reader);

		// Token: 0x06000334 RID: 820
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ParquetFileReader_Close(IntPtr reader);

		// Token: 0x06000335 RID: 821
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ParquetFileReader_MetaData(IntPtr reader, out IntPtr fileMetaData);

		// Token: 0x06000336 RID: 822
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ParquetFileReader_RowGroup(IntPtr reader, int i, out IntPtr rowGroupReader);

		// Token: 0x040000E5 RID: 229
		private readonly ParquetHandle _handle;

		// Token: 0x040000E6 RID: 230
		[Nullable(2)]
		private FileMetaData _fileMetaData;

		// Token: 0x040000E7 RID: 231
		[Nullable(2)]
		private readonly RandomAccessFile _randomAccessFile;

		// Token: 0x040000E8 RID: 232
		private readonly bool _ownedFile;
	}
}
