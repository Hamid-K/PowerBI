using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ParquetSharp
{
	// Token: 0x02000082 RID: 130
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ReaderProperties : IDisposable
	{
		// Token: 0x0600036A RID: 874 RVA: 0x0000DD60 File Offset: 0x0000BF60
		public static ReaderProperties GetDefaultReaderProperties()
		{
			return new ReaderProperties(ExceptionInfo.Return<IntPtr>(new ExceptionInfo.GetAction<IntPtr>(ReaderProperties.ReaderProperties_Get_Default_Reader_Properties)));
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000DD78 File Offset: 0x0000BF78
		internal ReaderProperties(IntPtr handle)
		{
			this.Handle = new ParquetHandle(handle, new Action<IntPtr>(ReaderProperties.ReaderProperties_Free));
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000DD98 File Offset: 0x0000BF98
		private ReaderProperties(ParquetHandle handle)
		{
			this.Handle = handle;
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000DDA8 File Offset: 0x0000BFA8
		public void Dispose()
		{
			this.Handle.Dispose();
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600036E RID: 878 RVA: 0x0000DDB8 File Offset: 0x0000BFB8
		public bool IsBufferedStreamEnabled
		{
			get
			{
				return ExceptionInfo.Return<bool>(this.Handle, new ExceptionInfo.GetFunction<bool>(ReaderProperties.ReaderProperties_Is_Buffered_Stream_Enabled));
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600036F RID: 879 RVA: 0x0000DDD4 File Offset: 0x0000BFD4
		// (set) Token: 0x06000370 RID: 880 RVA: 0x0000DDF0 File Offset: 0x0000BFF0
		public long BufferSize
		{
			get
			{
				return ExceptionInfo.Return<long>(this.Handle, new ExceptionInfo.GetFunction<long>(ReaderProperties.ReaderProperties_Get_Buffer_Size));
			}
			set
			{
				ExceptionInfo.Check(ReaderProperties.ReaderProperties_Set_Buffer_Size(this.Handle.IntPtr, value));
				GC.KeepAlive(this.Handle);
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000371 RID: 881 RVA: 0x0000DE14 File Offset: 0x0000C014
		// (set) Token: 0x06000372 RID: 882 RVA: 0x0000DE58 File Offset: 0x0000C058
		[Nullable(2)]
		public FileDecryptionProperties FileDecryptionProperties
		{
			[NullableContext(2)]
			get
			{
				IntPtr intPtr = ExceptionInfo.Return<IntPtr>(this.Handle, new ExceptionInfo.GetFunction<IntPtr>(ReaderProperties.ReaderProperties_Get_File_Decryption_Properties));
				if (!(intPtr == IntPtr.Zero))
				{
					return new FileDecryptionProperties(intPtr);
				}
				return null;
			}
			[NullableContext(2)]
			set
			{
				ExceptionInfo.Check(ReaderProperties.ReaderProperties_Set_File_Decryption_Properties(this.Handle.IntPtr, (value != null) ? value.Handle.IntPtr : IntPtr.Zero));
				GC.KeepAlive(this.Handle);
				GC.KeepAlive(value);
			}
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000DEAC File Offset: 0x0000C0AC
		public void EnableBufferedStream()
		{
			ExceptionInfo.Check(ReaderProperties.ReaderProperties_Enable_Buffered_Stream(this.Handle.IntPtr));
			GC.KeepAlive(this.Handle);
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000DED0 File Offset: 0x0000C0D0
		public void DisableBufferedStream()
		{
			ExceptionInfo.Check(ReaderProperties.ReaderProperties_Disable_Buffered_Stream(this.Handle.IntPtr));
			GC.KeepAlive(this.Handle);
		}

		// Token: 0x06000375 RID: 885
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ReaderProperties_Get_Default_Reader_Properties(out IntPtr readerProperties);

		// Token: 0x06000376 RID: 886
		[DllImport("ParquetSharpNative")]
		private static extern void ReaderProperties_Free(IntPtr readerProperties);

		// Token: 0x06000377 RID: 887
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ReaderProperties_Is_Buffered_Stream_Enabled(IntPtr readerProperties, [MarshalAs(UnmanagedType.I1)] out bool isBufferedStreamEnabled);

		// Token: 0x06000378 RID: 888
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ReaderProperties_Enable_Buffered_Stream(IntPtr readerProperties);

		// Token: 0x06000379 RID: 889
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ReaderProperties_Disable_Buffered_Stream(IntPtr readerProperties);

		// Token: 0x0600037A RID: 890
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ReaderProperties_Set_Buffer_Size(IntPtr readerProperties, long bufferSize);

		// Token: 0x0600037B RID: 891
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ReaderProperties_Get_Buffer_Size(IntPtr readerProperties, out long bufferSize);

		// Token: 0x0600037C RID: 892
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ReaderProperties_Set_File_Decryption_Properties(IntPtr readerProperties, IntPtr fileDecryptionProperties);

		// Token: 0x0600037D RID: 893
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ReaderProperties_Get_File_Decryption_Properties(IntPtr readerProperties, out IntPtr fileDecryptionProperties);

		// Token: 0x04000107 RID: 263
		internal readonly ParquetHandle Handle;
	}
}
